using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using VitureCommonLibrary.SlamV3;

namespace VitureCommonLibrary;

public static class VitureSlamV3
{
	private static readonly object _gate = new object();

	private static IntPtr _handler = IntPtr.Zero;

	private static bool _initialized = false;

	private static bool _started = false;

	private static string _currentSn = string.Empty;

	private static IntPtr _thermalArrayPtr = IntPtr.Zero;

	private static Device _cachedDevice;

	private static VitureSlamPoseCallback? _poseCb;

	private static VitureSlamSetPriorityCallback? _priorityCb;

	private static VitureSlamLoadCustomFileCallback? _loadCustomCb;

	private static VitureSlamSaveCustomFileCallback? _saveCustomCb;

	private static VitureSlamLoadDeviceInfoCallback? _loadDeviceCb;

	private static long _poseCallbackCount;

	private static long _imuFedCount;

	public static bool IsInitialized
	{
		get
		{
			lock (_gate)
			{
				return _initialized;
			}
		}
	}

	public static event Action<SlamPose>? OnPoseUpdate;

	public static void EnsureHandlerCreated(string configFilePath)
	{
		lock (_gate)
		{
			if (!(_handler != IntPtr.Zero))
			{
				try
				{
					_handler = VitureSlamV3Native.VitureSlamCreateHandler(configFilePath ?? string.Empty, Platform.kPC);
				}
				catch (Exception ex)
				{
					Logger.Error("VitureSlamV3.EnsureHandlerCreated: CreateHandler exception: " + ex.Message, ex.StackTrace);
					_handler = IntPtr.Zero;
					return;
				}
				if (_handler == IntPtr.Zero)
				{
					Logger.Error("VitureSlamV3.EnsureHandlerCreated: CreateHandler returned null (config=" + configFilePath + ")");
					return;
				}
				_poseCb = OnPoseCallbackInternal;
				_priorityCb = OnSetPriorityCallbackInternal;
				_loadDeviceCb = OnLoadDeviceInfoCallbackInternal;
				_loadCustomCb = OnLoadCustomFileCallbackInternal;
				_saveCustomCb = OnSaveCustomFileCallbackInternal;
				VitureSlamV3Native.VitureSlamRegisterPoseCallback(_handler, _poseCb);
				VitureSlamV3Native.VitureSlamRegisterSetPriorityCallback(_handler, _priorityCb);
				VitureSlamV3Native.VitureSlamRegisterLoadDeviceInfoCallback(_handler, _loadDeviceCb);
				VitureSlamV3Native.VitureSlamRegisterLoadCustomFileCallback(_handler, _loadCustomCb);
				VitureSlamV3Native.VitureSlamRegisterSaveCustomFileCallback(_handler, _saveCustomCb);
				Logger.Info($"VitureSlamV3.EnsureHandlerCreated: handler=0x{_handler.ToInt64():X} config={configFilePath}");
			}
		}
	}

	public static bool StartForS6(string sn)
	{
		lock (_gate)
		{
			if (_handler == IntPtr.Zero)
			{
				Logger.Warning("VitureSlamV3.StartForS6: handler is null; EnsureHandlerCreated() not called?");
				return false;
			}
			if (_started)
			{
				Logger.Warning("VitureSlamV3.StartForS6: already started; skipping (call StopForS6 first to restart)");
				return true;
			}
			_currentSn = sn ?? string.Empty;
			Interlocked.Exchange(ref _imuFedCount, 0L);
			Interlocked.Exchange(ref _poseCallbackCount, 0L);
			int num = VitureSlamV3Native.VitureSlamInitialize(_handler, DeviceModel.kS6);
			if (num != 0)
			{
				Logger.Error($"VitureSlamV3.StartForS6: Initialize(kS6) rc={num}");
				return false;
			}
			_initialized = true;
			num = VitureSlamV3Native.VitureSlamSetMode(_handler, SlamMode.k3DOF);
			if (num != 0)
			{
				Logger.Warning($"VitureSlamV3.StartForS6: SetMode(k3DOF) rc={num} (continuing)");
			}
			num = VitureSlamV3Native.VitureSlamStart(_handler);
			if (num != 0)
			{
				Logger.Error($"VitureSlamV3.StartForS6: Start rc={num}");
				return false;
			}
			_started = true;
			Logger.Info("VitureSlamV3.StartForS6: sn=" + sn + " started OK");
			return true;
		}
	}

	public static bool ProcessImuFrame(R6NewerHidMessage r6Msg)
	{
		if (!S6ImuFrameParser.TryParse(r6Msg, out var parsed))
		{
			return false;
		}
		if (_handler == IntPtr.Zero || !_initialized)
		{
			return false;
		}
		VitureCommonLibrary.SlamV3.ImuData imuData = default(VitureCommonLibrary.SlamV3.ImuData);
		imuData.timestamp = parsed.SampleTimestampNs;
		imuData.temperature = parsed.TemperatureC;
		imuData.gyr = new Vector3
		{
			x = parsed.GyroX,
			y = parsed.GyroY,
			z = parsed.GyroZ
		};
		imuData.acc = new Vector3
		{
			x = parsed.AccX * 9.80665f,
			y = parsed.AccY * 9.80665f,
			z = parsed.AccZ * 9.80665f
		};
		imuData.mag = new Vector3
		{
			x = 0f,
			y = 0f,
			z = 0f
		};
		VitureCommonLibrary.SlamV3.ImuData imu_data = imuData;
		int num;
		lock (_gate)
		{
			if (_handler == IntPtr.Zero || !_initialized)
			{
				return false;
			}
			num = VitureSlamV3Native.VitureSlamProcessImuData(_handler, ref imu_data);
		}
		long num2 = Interlocked.Increment(ref _imuFedCount);
		if (num != 0)
		{
			if (num2 <= 5 || num2 == 50)
			{
				Logger.Warning($"VitureSlamV3.ProcessImuFrame: ProcessImuData rc={num} (frame#{num2})");
			}
			return false;
		}
		if (num2 <= 5 || num2 == 50 || num2 % 5000 == 0L)
		{
			Logger.Info($"VitureSlamV3.ProcessImuFrame#{num2} tsNs={parsed.SampleTimestampNs} " + $"gyr=[{parsed.GyroX:F4},{parsed.GyroY:F4},{parsed.GyroZ:F4}] " + $"acc=[{parsed.AccX:F3},{parsed.AccY:F3},{parsed.AccZ:F3}]");
		}
		return true;
	}

	public static void StopForS6()
	{
		lock (_gate)
		{
			if (_handler == IntPtr.Zero)
			{
				return;
			}
			if (_started)
			{
				int num = VitureSlamV3Native.VitureSlamStop(_handler);
				if (num != 0)
				{
					Logger.Warning($"VitureSlamV3.StopForS6: Stop rc={num}");
				}
				_started = false;
			}
			if (_initialized)
			{
				int num2 = VitureSlamV3Native.VitureSlamTerminate(_handler);
				if (num2 != 0)
				{
					Logger.Warning($"VitureSlamV3.StopForS6: Terminate rc={num2}");
				}
				_initialized = false;
			}
			S6DeviceInfoBuilder.FreeNative(ref _cachedDevice, ref _thermalArrayPtr);
			_cachedDevice = default(Device);
			Logger.Info($"VitureSlamV3.StopForS6: sn={_currentSn} imuFed={Interlocked.Read(ref _imuFedCount)} poseCb={Interlocked.Read(ref _poseCallbackCount)}");
			_currentSn = string.Empty;
		}
	}

	public static void DestroyHandlerIfAny()
	{
		lock (_gate)
		{
			if (!(_handler == IntPtr.Zero))
			{
				if (_started || _initialized)
				{
					StopForSafetyNoLock();
				}
				int num = VitureSlamV3Native.VitureSlamDestroyHandler(_handler);
				if (num != 0)
				{
					Logger.Warning($"VitureSlamV3.DestroyHandlerIfAny: DestroyHandler rc={num}");
				}
				_handler = IntPtr.Zero;
				_poseCb = null;
				_priorityCb = null;
				_loadDeviceCb = null;
				_loadCustomCb = null;
				_saveCustomCb = null;
				Logger.Info("VitureSlamV3.DestroyHandlerIfAny: done");
			}
		}
	}

	private static void StopForSafetyNoLock()
	{
		if (_started)
		{
			VitureSlamV3Native.VitureSlamStop(_handler);
			_started = false;
		}
		if (_initialized)
		{
			VitureSlamV3Native.VitureSlamTerminate(_handler);
			_initialized = false;
		}
		S6DeviceInfoBuilder.FreeNative(ref _cachedDevice, ref _thermalArrayPtr);
		_cachedDevice = default(Device);
	}

	private static void OnPoseCallbackInternal(IntPtr posePtr)
	{
		if (posePtr == IntPtr.Zero)
		{
			return;
		}
		try
		{
			SlamPose obj = Marshal.PtrToStructure<SlamPose>(posePtr);
			long num = Interlocked.Increment(ref _poseCallbackCount);
			if (num <= 3 || num == 50)
			{
				Logger.Info($"VitureSlamV3.OnPose#{num} mode={obj.slam_mode} tsNs={obj.timestamp} " + $"q=({obj.orientation.w:F4},{obj.orientation.x:F4},{obj.orientation.y:F4},{obj.orientation.z:F4})");
			}
			VitureSlamV3.OnPoseUpdate?.Invoke(obj);
		}
		catch (Exception ex)
		{
			Logger.Warning("VitureSlamV3.OnPoseCallback exception: " + ex.Message);
		}
	}

	private static void OnSetPriorityCallbackInternal(ulong threadId, ThreadPriorityLevel priority)
	{
		Logger.Info($"VitureSlamV3.OnSetPriorityCallback: threadId={threadId} priority={priority} (logged only)");
	}

	private static void OnLoadDeviceInfoCallbackInternal(IntPtr devicePtr)
	{
		if (devicePtr == IntPtr.Zero)
		{
			Logger.Warning("VitureSlamV3.OnLoadDeviceInfo: devicePtr is null");
			return;
		}
		S6Calibration current = S6CalibrationManager.Instance.Current;
		if (current == null)
		{
			Logger.Warning("VitureSlamV3.OnLoadDeviceInfo: S6CalibrationManager.Current is null — Device 全 0 写回");
			Device structure = default(Device);
			structure.sn = _currentSn ?? string.Empty;
			Marshal.StructureToPtr(structure, devicePtr, fDeleteOld: false);
			return;
		}
		if (_thermalArrayPtr != IntPtr.Zero)
		{
			S6DeviceInfoBuilder.FreeNative(ref _cachedDevice, ref _thermalArrayPtr);
		}
		if (!S6DeviceInfoBuilder.TryBuild(current, out _cachedDevice, out _thermalArrayPtr))
		{
			Logger.Warning("VitureSlamV3.OnLoadDeviceInfo: TryBuild failed — Device 不完整");
			return;
		}
		try
		{
			Marshal.StructureToPtr(_cachedDevice, devicePtr, fDeleteOld: false);
			Logger.Info("VitureSlamV3.OnLoadDeviceInfo: sn=" + _cachedDevice.sn + " " + $"thermal_n={_cachedDevice.imu.num_thermal_gyr_biases} num_cameras=0");
		}
		catch (Exception ex)
		{
			Logger.Error("VitureSlamV3.OnLoadDeviceInfo: StructureToPtr exception: " + ex.Message, ex.StackTrace);
		}
	}

	private static void OnLoadCustomFileCallbackInternal(IntPtr dataPtr, IntPtr sizePtr)
	{
		try
		{
			string customFilePath = GetCustomFilePath(_currentSn);
			if (!File.Exists(customFilePath))
			{
				if (sizePtr != IntPtr.Zero)
				{
					Marshal.WriteInt32(sizePtr, 0);
				}
				Logger.Info("VitureSlamV3.OnLoadCustomFile: no file at " + customFilePath + ", size=0");
				return;
			}
			byte[] array = File.ReadAllBytes(customFilePath);
			int num = ((sizePtr != IntPtr.Zero) ? Marshal.ReadInt32(sizePtr) : array.Length);
			int num2 = Math.Min(array.Length, Math.Max(0, num));
			if (num2 <= 0)
			{
				if (sizePtr != IntPtr.Zero)
				{
					Marshal.WriteInt32(sizePtr, 0);
				}
				Logger.Warning($"VitureSlamV3.OnLoadCustomFile: capacity={num} too small for {array.Length}B file");
				return;
			}
			Marshal.Copy(array, 0, dataPtr, num2);
			if (sizePtr != IntPtr.Zero)
			{
				Marshal.WriteInt32(sizePtr, num2);
			}
			Logger.Info($"VitureSlamV3.OnLoadCustomFile: loaded {num2}B from {customFilePath}");
		}
		catch (Exception ex)
		{
			Logger.Warning("VitureSlamV3.OnLoadCustomFile exception: " + ex.Message);
			if (sizePtr != IntPtr.Zero)
			{
				Marshal.WriteInt32(sizePtr, 0);
			}
		}
	}

	private static void OnSaveCustomFileCallbackInternal(IntPtr dataPtr, int size)
	{
		try
		{
			if (size <= 0 || dataPtr == IntPtr.Zero)
			{
				Logger.Warning($"VitureSlamV3.OnSaveCustomFile: ignored (size={size})");
				return;
			}
			string customFilePath = GetCustomFilePath(_currentSn);
			Directory.CreateDirectory(Path.GetDirectoryName(customFilePath));
			byte[] array = new byte[size];
			Marshal.Copy(dataPtr, array, 0, size);
			File.WriteAllBytes(customFilePath, array);
			Logger.Info($"VitureSlamV3.OnSaveCustomFile: wrote {size}B to {customFilePath}");
		}
		catch (Exception ex)
		{
			Logger.Warning("VitureSlamV3.OnSaveCustomFile exception: " + ex.Message);
		}
	}

	private static string GetCustomFilePath(string sn)
	{
		string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
		string text = (string.IsNullOrWhiteSpace(sn) ? "_unknown" : sn);
		return Path.Combine(folderPath, "VITURE", "SpaceWalker", "slam_custom_" + text + ".bin");
	}
}
