using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace VitureCommonLibrary;

public static class VitureSlam
{
	public delegate void VitureSlamCallback(ref PoseState data);

	public delegate int VitureSlamReadBias(IntPtr data, ref int size);

	public delegate int VitureSlamWriteBias(IntPtr data, int size);

	public delegate int VitureSlamSetPriority(int tid, int priority);

	public delegate int VitureSlamReadCarinaConfig(IntPtr data, ref int size);

	private const float G = 9.80665f;

	private static byte[]? gyroBias;

	private static byte[]? carinaYamlContent;

	private static byte[]? onlineBiasContent;

	private static PoseState cachePose;

	private static bool hasStart;

	private static bool hasSetBias;

	private static VitureSlamCallback _vitureSlamCallback;

	private static VitureSlamReadBias _vitureSlamReadBiasCallback;

	private static VitureSlamWriteBias _vitureSlamWriteBiasCallback;

	private static VitureSlamSetPriority _vitureSlamSetPriorityCallback;

	private static VitureSlamReadCarinaConfig _vitureSlamReadCarinaConfigCallback;

	private static IntPtr _handle;

	private static bool _disposed;

	private const string DllName = "Slam";

	private static int imuUpdateCount;

	private static int predictSuccessCount;

	private const int IMU_WARMUP_COUNT = 50;

	public static byte[] GyroBias
	{
		set
		{
			gyroBias = value;
		}
	}

	public static string CarinaYamlContent
	{
		set
		{
			carinaYamlContent = new UTF8Encoding().GetBytes(value);
		}
	}

	public static event Action<PoseState>? OnPoseUpdate;

	[DllImport("Slam", CallingConvention = CallingConvention.Cdecl)]
	private static extern IntPtr VitureSlamCreateHandle([MarshalAs(UnmanagedType.LPStr)] string config_file);

	[DllImport("Slam", CallingConvention = CallingConvention.Cdecl)]
	private static extern void VitureSlamDestroyHandle(IntPtr slam_handle);

	[DllImport("Slam", CallingConvention = CallingConvention.Cdecl)]
	private static extern void VitureSlamStart(IntPtr slam_handle);

	[DllImport("Slam", CallingConvention = CallingConvention.Cdecl)]
	private static extern void VitureSlamStop(IntPtr slam_handle);

	[DllImport("Slam", CallingConvention = CallingConvention.Cdecl)]
	private static extern void VitureSlamUpdateIMU(IntPtr slam_handle, ref ImuData imu_data);

	[DllImport("Slam", CallingConvention = CallingConvention.Cdecl)]
	private static extern int VitureSlamGetPoseState(IntPtr slam_handle, ref PoseState data);

	[DllImport("Slam", CallingConvention = CallingConvention.Cdecl)]
	private static extern int VitureSlamGetPredictPoseState(IntPtr slam_handle, ulong timestamp, ref PoseState data);

	[DllImport("Slam", CallingConvention = CallingConvention.Cdecl)]
	private static extern int VitureSlamSetParameters(IntPtr slam_handle, [MarshalAs(UnmanagedType.LPStr)] string key, [MarshalAs(UnmanagedType.LPStr)] string value);

	[DllImport("Slam", CallingConvention = CallingConvention.Cdecl)]
	private static extern int VitureSlamRegisterCallback(IntPtr slam_handle, int type, IntPtr callback);

	[DllImport("Slam", CallingConvention = CallingConvention.Cdecl)]
	private static extern int VitureSlamRecenterOrientation(IntPtr slam_handle);

	[DllImport("Slam", CallingConvention = CallingConvention.Cdecl)]
	private static extern int VitureSlamLockRoll(IntPtr slam_handle, int lock_roll = 1);

	[DllImport("Slam", CallingConvention = CallingConvention.Cdecl)]
	private static extern int VitureSlamSetZeroDoF(IntPtr slam_handle, int zero_dof = 0);

	[DllImport("Slam", CallingConvention = CallingConvention.Cdecl)]
	private static extern int VitureSlamSetCloudPlatformMode(IntPtr slam_handle, int mode = 0);

	private static void OnVitureSlamCallback(ref PoseState pose)
	{
		cachePose = pose;
		VitureSlam.OnPoseUpdate?.Invoke(pose);
	}

	private static int OnVitureSlamReadBias(IntPtr data, ref int size)
	{
		Logger.Info($"OnVitureSlamReadBias called: gyroBias={gyroBias != null}, onlineBiasContent={onlineBiasContent != null} ({onlineBiasContent?.Length})");
		if (gyroBias == null)
		{
			Logger.Warning("Failed to read gyr bias. gyroBias is null");
			return -1;
		}
		if (onlineBiasContent == null)
		{
			Logger.Warning("Failed to read gyr bias. onlineBiasContent is null");
			return -2;
		}
		try
		{
			Marshal.Copy(gyroBias, 0, data, 12);
			byte[] array = new byte[24];
			Marshal.Copy(array, 0, data + 12, array.Length);
			Marshal.Copy(onlineBiasContent, 0, data + 36, onlineBiasContent.Length);
			size = 36 + onlineBiasContent.Length;
			hasSetBias = true;
			Logger.Info($"Successfully read gyr bias. Data size: {size}");
			return 0;
		}
		catch (Exception ex)
		{
			Logger.Warning("Failed to read gyr bias. Exception: " + ex.Message);
			return -3;
		}
	}

	private static int OnVitureSlamWriteBias(IntPtr data, int size)
	{
		try
		{
			onlineBiasContent = new byte[size];
			Marshal.Copy(data, onlineBiasContent, 0, size);
			return 0;
		}
		catch (Exception ex)
		{
			Logger.Warning(ex.Message);
			return -1;
		}
	}

	private static int OnVitureSlamReadCarinaConfig(IntPtr data, ref int size)
	{
		if (carinaYamlContent == null)
		{
			return -1;
		}
		try
		{
			Marshal.Copy(carinaYamlContent, 0, data, carinaYamlContent.Length);
			size = carinaYamlContent.Length;
			return 0;
		}
		catch (Exception ex)
		{
			Logger.Warning(ex.Message);
			return -2;
		}
	}

	private static int OnVitureSlamSetPriority(int tid, int priority)
	{
		return 0;
	}

	public static void Start(string configFile)
	{
		Logger.Info("VitureSlam.Start configFile=" + configFile);
		Logger.Info($"VitureSlam.Start gyroBias={gyroBias != null} ({gyroBias?.Length}), onlineBiasContent={onlineBiasContent != null} ({onlineBiasContent?.Length}), carinaYamlContent={carinaYamlContent != null} ({carinaYamlContent?.Length})");
		if (_handle == IntPtr.Zero)
		{
			_handle = VitureSlamCreateHandle(configFile);
		}
		if (_handle == IntPtr.Zero)
		{
			Logger.Error("Failed to create Viture SLAM handle");
		}
		_vitureSlamCallback = OnVitureSlamCallback;
		_vitureSlamReadBiasCallback = OnVitureSlamReadBias;
		_vitureSlamWriteBiasCallback = OnVitureSlamWriteBias;
		_vitureSlamSetPriorityCallback = OnVitureSlamSetPriority;
		_vitureSlamReadCarinaConfigCallback = OnVitureSlamReadCarinaConfig;
		VitureSlamRegisterCallback(_handle, 0, Marshal.GetFunctionPointerForDelegate(_vitureSlamCallback));
		VitureSlamRegisterCallback(_handle, 1, Marshal.GetFunctionPointerForDelegate(_vitureSlamReadBiasCallback));
		VitureSlamRegisterCallback(_handle, 2, Marshal.GetFunctionPointerForDelegate(_vitureSlamWriteBiasCallback));
		VitureSlamRegisterCallback(_handle, 3, Marshal.GetFunctionPointerForDelegate(_vitureSlamSetPriorityCallback));
		VitureSlamRegisterCallback(_handle, 4, Marshal.GetFunctionPointerForDelegate(_vitureSlamReadCarinaConfigCallback));
		VitureSlamStart(_handle);
		SetCloudPlatformMode();
		hasStart = true;
		imuUpdateCount = 0;
		predictSuccessCount = 0;
	}

	public static void Stop()
	{
		hasSetBias = false;
		hasStart = false;
		gyroBias = null;
		onlineBiasContent = null;
		carinaYamlContent = null;
		imuUpdateCount = 0;
		predictSuccessCount = 0;
		if (!(_handle == IntPtr.Zero))
		{
			VitureSlamStop(_handle);
			VitureSlamDestroyHandle(_handle);
			_handle = IntPtr.Zero;
		}
	}

	private static PoseState GetPoseState()
	{
		PoseState data = default(PoseState);
		if ((!hasStart && !hasSetBias && _handle == IntPtr.Zero) || onlineBiasContent == null)
		{
			return data;
		}
		if (VitureSlamGetPoseState(_handle, ref data) != 0)
		{
			Logger.Error("Failed to get pose state");
		}
		return data;
	}

	private static PoseState? GetPredictPoseState(ulong timestamp = 0uL, bool silent = false)
	{
		if (_handle == IntPtr.Zero)
		{
			return null;
		}
		PoseState data = default(PoseState);
		int num = VitureSlamGetPredictPoseState(_handle, timestamp, ref data);
		if (num != 0)
		{
			if (!silent)
			{
				Logger.Error($"Failed to get predict pose state: {num}");
			}
			return null;
		}
		return data;
	}

	public static void SetParameters(string key, string value)
	{
		if (!(_handle == IntPtr.Zero) && VitureSlamSetParameters(_handle, key, value) != 0)
		{
			Logger.Error("Failed to set parameters");
		}
	}

	public static void RegisterCallback(int type, IntPtr callback)
	{
		if (!(_handle == IntPtr.Zero) && VitureSlamRegisterCallback(_handle, type, callback) != 0)
		{
			Logger.Error("Failed to register callback");
		}
	}

	public static void RecenterOrientation()
	{
		if (!(_handle == IntPtr.Zero) && VitureSlamRecenterOrientation(_handle) != 0)
		{
			Logger.Error("Failed to recenter orientation");
		}
	}

	public static void LockRoll(int lockRoll = 1)
	{
		if (!(_handle == IntPtr.Zero) && VitureSlamLockRoll(_handle, lockRoll) != 0)
		{
			Logger.Error("Failed to lock roll");
		}
	}

	public static void SetZeroDoF(int zeroDof = 0)
	{
		if (!(_handle == IntPtr.Zero) && VitureSlamSetZeroDoF(_handle, zeroDof) != 0)
		{
			Logger.Error("Failed to set zero DoF");
		}
	}

	public static void SetCloudPlatformMode(bool enable = false)
	{
		if (!(_handle == IntPtr.Zero) && VitureSlamSetCloudPlatformMode(_handle, enable ? 1 : 0) != 0)
		{
			Logger.Error("Failed to set cloud platform mode");
		}
	}

	public static void LoadOnlineBaisData(string sn = "")
	{
		hasSetBias = false;
		string text = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "VITURE", "SpaceWalker");
		if (!Directory.Exists(text))
		{
			Directory.CreateDirectory(text);
		}
		string empty = string.Empty;
		empty = (string.IsNullOrWhiteSpace(sn) ? Path.Combine(text, "slam_config_P6.yaml") : Path.Combine(text, "slam_config_P6_" + sn + ".yaml"));
		SetOnlineBaisData(empty);
	}

	public static void SaveOnlineBaisData(string sn = "")
	{
		string text = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "VITURE", "SpaceWalker");
		if (!Directory.Exists(text))
		{
			Directory.CreateDirectory(text);
		}
		string empty = string.Empty;
		empty = (string.IsNullOrWhiteSpace(sn) ? Path.Combine(text, "slam_config_P6.yaml") : Path.Combine(text, "slam_config_P6_" + sn + ".yaml"));
		string onlineBaisData = GetOnlineBaisData();
		Logger.Info("SaveOnlineBaisData: " + onlineBaisData);
		if (!string.IsNullOrWhiteSpace(onlineBaisData))
		{
			File.WriteAllText(empty, onlineBaisData, new UTF8Encoding(encoderShouldEmitUTF8Identifier: false));
		}
	}

	private static void SetOnlineBaisData(string baisFile)
	{
		if (string.IsNullOrWhiteSpace(baisFile) || !File.Exists(baisFile))
		{
			return;
		}
		string text = File.ReadAllText(baisFile, new UTF8Encoding(encoderShouldEmitUTF8Identifier: false));
		if (string.IsNullOrWhiteSpace(text))
		{
			return;
		}
		try
		{
			Logger.Info("SetOnlineBaisData: " + text);
			onlineBiasContent = Encoding.UTF8.GetBytes(text);
		}
		catch (Exception ex)
		{
			Logger.Warning(ex.Message);
		}
	}

	private static string GetOnlineBaisData()
	{
		string result = string.Empty;
		if (onlineBiasContent != null)
		{
			try
			{
				result = Encoding.UTF8.GetString(onlineBiasContent, 0, onlineBiasContent.Length);
			}
			catch (Exception ex)
			{
				Logger.Warning(ex.Message);
			}
		}
		return result;
	}

	public static PoseState Track(HidMessage hidMessage, bool use307 = false)
	{
		float[] array = new float[3];
		float[] array2 = new float[3];
		if (use307)
		{
			array = new float[3]
			{
				hidMessage.AccRaw.X,
				hidMessage.AccRaw.Y,
				hidMessage.AccRaw.Z
			};
			array2 = new float[3]
			{
				hidMessage.GyroRaw.X,
				hidMessage.GyroRaw.Y,
				hidMessage.GyroRaw.Z
			};
		}
		else
		{
			array = new float[3]
			{
				hidMessage.ImuMagData.AccX,
				hidMessage.ImuMagData.AccY,
				hidMessage.ImuMagData.AccZ
			};
			array2 = new float[3]
			{
				hidMessage.ImuMagData.GyroX,
				hidMessage.ImuMagData.GyroY,
				hidMessage.ImuMagData.GyroZ
			};
		}
		float[] array3 = new float[3];
		ulong num = hidMessage.DeviceTimestamp * 1000;
		float num2 = (use307 ? hidMessage.Temp : ((float)(int)hidMessage.Data.Payload[hidMessage.Data.Payload.Length - 1] * 0.2f));
		ImuData imuData = default(ImuData);
		imuData.timestamp = num;
		imuData.acc_x = array[0] * 9.80665f;
		imuData.acc_y = array[1] * 9.80665f;
		imuData.acc_z = array[2] * 9.80665f;
		imuData.gyr_x = array2[0];
		imuData.gyr_y = array2[1];
		imuData.gyr_z = array2[2];
		imuData.mag_x = array3[0];
		imuData.mag_y = array3[1];
		imuData.mag_z = array3[2];
		imuData.temperature = num2;
		ImuData imu_data = imuData;
		if (_handle != IntPtr.Zero)
		{
			VitureSlamUpdateIMU(_handle, ref imu_data);
		}
		imuUpdateCount++;
		if (imuUpdateCount <= 5 || imuUpdateCount == 50)
		{
			Logger.Info($"VitureSlam.Track imuUpdateCount={imuUpdateCount}, timeUs={num}, hasSetBias={hasSetBias}, acc=[{array[0]:F3},{array[1]:F3},{array[2]:F3}], gyro=[{array2[0]:F6},{array2[1]:F6},{array2[2]:F6}]");
		}
		if (GlassesDeviceManager.IsRunInUnity)
		{
			if (imuUpdateCount < 50)
			{
				return cachePose;
			}
			PoseState? predictPoseState = GetPredictPoseState(0uL, imuUpdateCount > 55);
			if (predictPoseState.HasValue)
			{
				if (imuUpdateCount == 50 || predictSuccessCount == 0)
				{
					Logger.Info($"GetPredictPoseState SUCCESS at imuUpdateCount={imuUpdateCount}, rw={predictPoseState.Value.rw:F4}");
				}
				predictSuccessCount++;
				return predictPoseState.Value;
			}
		}
		return cachePose;
	}

	public static void StartForS6(string sn)
	{
		Logger.Warning("VitureSlam.StartForS6 skipped (sn=" + sn + "): new Slam.dll API not yet integrated. Calibration is cached on disk; SDK invocation will be wired in M3.");
	}

	public static bool TrackS6(R6NewerHidMessage r6Msg)
	{
		if (!S6ImuFrameParser.TryParse(r6Msg, out var parsed))
		{
			return false;
		}
		imuUpdateCount++;
		if (imuUpdateCount <= 5 || imuUpdateCount == 50)
		{
			Logger.Info($"VitureSlam.TrackS6 (no-op) count={imuUpdateCount} " + $"tsNs={parsed.SampleTimestampNs} temp={parsed.TemperatureC:F1}°C " + $"gyro=[{parsed.GyroX:F4},{parsed.GyroY:F4},{parsed.GyroZ:F4}] " + $"acc=[{parsed.AccX:F3},{parsed.AccY:F3},{parsed.AccZ:F3}]");
		}
		return true;
	}

	public static void StopForS6()
	{
		Logger.Info($"VitureSlam.StopForS6 (no-op): SDK not yet integrated. imuUpdateCount={imuUpdateCount}");
		imuUpdateCount = 0;
		predictSuccessCount = 0;
	}
}
