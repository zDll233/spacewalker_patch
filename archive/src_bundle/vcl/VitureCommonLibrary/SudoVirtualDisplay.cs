using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using Microsoft.Win32.SafeHandles;
using Windows.Win32;
using Windows.Win32.Devices.DeviceAndDriverInstallation;
using Windows.Win32.Foundation;
using Windows.Win32.Storage.FileSystem;
using Windows.Win32.System.Threading;

namespace VitureCommonLibrary;

public class SudoVirtualDisplay : IDisposable
{
	internal struct VvdaProtocolVersion
	{
		public byte Major;

		public byte Minor;

		public byte Incremental;

		[MarshalAs(UnmanagedType.U1)]
		public bool TestBuild;
	}

	internal struct VirtualDisplayAddParams
	{
		public uint Width;

		public uint Height;

		public uint RefreshRate;

		public Guid MonitorGuid;

		public unsafe fixed byte DeviceName[14];

		public unsafe fixed byte SerialNumber[14];

		public unsafe fixed byte ClientId[128];
	}

	internal struct VirtualDisplayPingParams
	{
		public unsafe fixed byte ClientId[128];
	}

	internal struct DisplayEntry
	{
		public DisplayTargetToken Token;

		public VirtualDisplayAddParams Params;

		public unsafe string DeviceName
		{
			get
			{
				VirtualDisplayAddParams @params = Params;
				return GetUtf8(@params.DeviceName);
			}
		}

		public unsafe string SerialNumber
		{
			get
			{
				VirtualDisplayAddParams @params = Params;
				return GetUtf8(@params.SerialNumber);
			}
		}
	}

	internal struct VirtualDisplayRemoveParams
	{
		public Guid MonitorGuid;
	}

	internal struct VirtualDisplayAddOut
	{
		public LUID AdapterLuid;

		public uint TargetId;
	}

	internal struct VirtualDisplaySetRenderAdapterParams
	{
		public LUID AdapterLuid;
	}

	internal struct VirtualDisplayGetWatchdogOut
	{
		public uint Timeout;

		public uint Countdown;
	}

	internal struct VirtualDisplayGetProtocolVersionOut
	{
		public VvdaProtocolVersion Version;
	}

	public const string VirtualDisplayName = "VIT-VDD";

	internal const uint IOCTL_ADD_VIRTUAL_DISPLAY = 2236416u;

	internal const uint IOCTL_REMOVE_VIRTUAL_DISPLAY = 2236420u;

	internal const uint IOCTL_SET_RENDER_ADAPTER = 2236424u;

	internal const uint IOCTL_GET_WATCHDOG = 2236428u;

	internal const uint IOCTL_DRIVER_PING = 2236960u;

	internal const uint IOCTL_GET_PROTOCOL_VERSION = 2237436u;

	internal const string VVDA_HARDWARE_ID = "root\\viture\\VitureVDA";

	internal static readonly Guid VVDA_CLASS_GUID = new Guid(1295444328u, 58149, 4558, 191, 193, 8, 0, 43, 225, 3, 24);

	internal static readonly Guid VVDA_INTERFACE_GUID = new Guid(3854352948u, 7692, 16778, 160, 212, 239, 139, 117, 1, 65, 77);

	internal static readonly VvdaProtocolVersion VDAProtocolVersion = new VvdaProtocolVersion
	{
		Major = 0,
		Minor = 3,
		Incremental = 0,
		TestBuild = true
	};

	internal const int VVDA_CLIENTID_MAX = 128;

	internal const int VVDA_NAME_MAX = 14;

	private Thread? pingThread;

	private SafeFileHandle deviceHandle = new SafeFileHandle(IntPtr.Zero, ownsHandle: true);

	private readonly string clientId = CreateClientId();

	private readonly object syncRoot = new object();

	private readonly Dictionary<Guid, DisplayEntry> displays = new Dictionary<Guid, DisplayEntry>();

	private readonly CancellationTokenSource pingLoopCts = new CancellationTokenSource();

	private readonly object initLock = new object();

	private volatile bool opened;

	private int disposeState;

	private bool IsDisposing => Volatile.Read(ref disposeState) != 0;

	public bool IsActive
	{
		get
		{
			if (!IsDisposing && !deviceHandle.IsInvalid)
			{
				return !deviceHandle.IsClosed;
			}
			return false;
		}
	}

	public static SudoVirtualDisplay Instance { get; } = new SudoVirtualDisplay();


	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private unsafe static void SetUtf8(byte* dst, int cap, string? s)
	{
		if (cap <= 0)
		{
			return;
		}
		int num = 0;
		if (!string.IsNullOrEmpty(s))
		{
			byte[] bytes = Encoding.UTF8.GetBytes(s);
			num = ((bytes.Length > cap - 1) ? (cap - 1) : bytes.Length);
			while (num > 0 && num < bytes.Length && (bytes[num] & 0xC0) == 128)
			{
				num--;
			}
			for (int i = 0; i < num; i++)
			{
				dst[i] = bytes[i];
			}
		}
		for (int j = num; j < cap; j++)
		{
			dst[j] = 0;
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private unsafe static string GetUtf8(byte* src)
	{
		int i;
		for (i = 0; src[i] != 0; i++)
		{
		}
		if (i == 0)
		{
			return string.Empty;
		}
		byte[] array = new byte[i];
		Marshal.Copy((IntPtr)src, array, 0, i);
		return Encoding.UTF8.GetString(array);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private unsafe static SafeFileHandle OpenDevice(Guid interfaceGuid)
	{
		if (PInvoke.CM_Get_Device_Interface_List_Size(out var pulLen, in interfaceGuid, default(PWSTR), CM_GET_DEVICE_INTERFACE_LIST_FLAGS.CM_GET_DEVICE_INTERFACE_LIST_PRESENT) != 0 || pulLen <= 1)
		{
			return new SafeFileHandle(IntPtr.Zero, ownsHandle: true);
		}
		char[] array = new char[pulLen];
		CONFIGRET num;
		fixed (char* ptr = array)
		{
			num = PInvoke.CM_Get_Device_Interface_List(in interfaceGuid, default(PWSTR), ptr, pulLen, CM_GET_DEVICE_INTERFACE_LIST_FLAGS.CM_GET_DEVICE_INTERFACE_LIST_PRESENT);
		}
		if (num != 0)
		{
			return new SafeFileHandle(IntPtr.Zero, ownsHandle: true);
		}
		int num2 = 0;
		while (num2 < array.Length && array[num2] != 0)
		{
			int i;
			for (i = num2; i < array.Length && array[i] != 0; i++)
			{
			}
			SafeFileHandle safeFileHandle = PInvoke.CreateFile(new string(array, num2, i - num2), 3221225472u, FILE_SHARE_MODE.FILE_SHARE_READ | FILE_SHARE_MODE.FILE_SHARE_WRITE, null, FILE_CREATION_DISPOSITION.OPEN_EXISTING, FILE_FLAGS_AND_ATTRIBUTES.FILE_ATTRIBUTE_NORMAL | FILE_FLAGS_AND_ATTRIBUTES.FILE_FLAG_WRITE_THROUGH | FILE_FLAGS_AND_ATTRIBUTES.FILE_FLAG_OVERLAPPED | FILE_FLAGS_AND_ATTRIBUTES.FILE_FLAG_NO_BUFFERING);
			if (!safeFileHandle.IsInvalid)
			{
				return safeFileHandle;
			}
			num2 = i + 1;
		}
		return new SafeFileHandle(IntPtr.Zero, ownsHandle: true);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private unsafe static bool AddVirtualDisplay(SafeFileHandle deviceHandle, VirtualDisplayAddParams addParams, out VirtualDisplayAddOut addOut)
	{
		addOut = default(VirtualDisplayAddOut);
		if (deviceHandle.IsInvalid)
		{
			return false;
		}
		uint num = 0u;
		fixed (VirtualDisplayAddOut* lpOutBuffer = &addOut)
		{
			return PInvoke.DeviceIoControl((HANDLE)deviceHandle.DangerousGetHandle(), 2236416u, &addParams, (uint)sizeof(VirtualDisplayAddParams), lpOutBuffer, (uint)sizeof(VirtualDisplayAddOut), &num, null);
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private unsafe static bool RemoveVirtualDisplay(SafeFileHandle deviceHandle, VirtualDisplayRemoveParams removeParams)
	{
		if (deviceHandle.IsInvalid)
		{
			return false;
		}
		uint num = 0u;
		return PInvoke.DeviceIoControl((HANDLE)deviceHandle.DangerousGetHandle(), 2236420u, &removeParams, (uint)sizeof(VirtualDisplayRemoveParams), null, 0u, &num, null);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private unsafe static bool SetRenderAdapter(SafeFileHandle deviceHandle, VirtualDisplaySetRenderAdapterParams setParams)
	{
		if (deviceHandle.IsInvalid)
		{
			return false;
		}
		uint num = 0u;
		return PInvoke.DeviceIoControl((HANDLE)deviceHandle.DangerousGetHandle(), 2236424u, &setParams, (uint)sizeof(VirtualDisplaySetRenderAdapterParams), null, 0u, &num, null);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private unsafe static bool GetWatchdog(SafeFileHandle deviceHandle, out VirtualDisplayGetWatchdogOut watchdogOut)
	{
		watchdogOut = default(VirtualDisplayGetWatchdogOut);
		if (deviceHandle.IsInvalid)
		{
			return false;
		}
		uint num = 0u;
		fixed (VirtualDisplayGetWatchdogOut* lpOutBuffer = &watchdogOut)
		{
			return PInvoke.DeviceIoControl((HANDLE)deviceHandle.DangerousGetHandle(), 2236428u, null, 0u, lpOutBuffer, (uint)sizeof(VirtualDisplayGetWatchdogOut), &num, null);
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private unsafe static bool GetProtocolVersion(SafeFileHandle deviceHandle, out VirtualDisplayGetProtocolVersionOut versionOut)
	{
		versionOut = default(VirtualDisplayGetProtocolVersionOut);
		if (deviceHandle.IsInvalid)
		{
			return false;
		}
		uint num = 0u;
		fixed (VirtualDisplayGetProtocolVersionOut* lpOutBuffer = &versionOut)
		{
			return PInvoke.DeviceIoControl((HANDLE)deviceHandle.DangerousGetHandle(), 2237436u, null, 0u, lpOutBuffer, (uint)sizeof(VirtualDisplayGetProtocolVersionOut), &num, null);
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private unsafe static bool DriverPing(SafeFileHandle deviceHandle, string clientId)
	{
		if (deviceHandle.IsInvalid)
		{
			return false;
		}
		VirtualDisplayPingParams virtualDisplayPingParams = default(VirtualDisplayPingParams);
		SetUtf8(virtualDisplayPingParams.ClientId, 128, clientId);
		uint num = 0u;
		return PInvoke.DeviceIoControl((HANDLE)deviceHandle.DangerousGetHandle(), 2236960u, &virtualDisplayPingParams, (uint)sizeof(VirtualDisplayPingParams), null, 0u, &num, null);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static bool IsDriverCompatible(SafeFileHandle deviceHandle)
	{
		if (!GetProtocolVersion(deviceHandle, out var versionOut))
		{
			return false;
		}
		VvdaProtocolVersion version = versionOut.Version;
		if (version.Major == VDAProtocolVersion.Major && version.Minor == VDAProtocolVersion.Minor)
		{
			return version.Incremental == VDAProtocolVersion.Incremental;
		}
		return false;
	}

	private unsafe static void DisableCurrentThreadPowerThrottling()
	{
		THREAD_POWER_THROTTLING_STATE tHREAD_POWER_THROTTLING_STATE = default(THREAD_POWER_THROTTLING_STATE);
		tHREAD_POWER_THROTTLING_STATE.Version = 1u;
		tHREAD_POWER_THROTTLING_STATE.ControlMask = 1u;
		tHREAD_POWER_THROTTLING_STATE.StateMask = 0u;
		THREAD_POWER_THROTTLING_STATE tHREAD_POWER_THROTTLING_STATE2 = tHREAD_POWER_THROTTLING_STATE;
		if (!PInvoke.SetThreadInformation(PInvoke.GetCurrentThread(), THREAD_INFORMATION_CLASS.ThreadPowerThrottling, &tHREAD_POWER_THROTTLING_STATE2, (uint)sizeof(THREAD_POWER_THROTTLING_STATE)))
		{
			Logger.Warning($"[VDD] disable ping-thread power throttling failed (err={Marshal.GetLastWin32Error()})");
		}
	}

	private SudoVirtualDisplay()
	{
	}

	public void Init()
	{
		if (opened || IsDisposing)
		{
			return;
		}
		lock (initLock)
		{
			if (opened || IsDisposing)
			{
				return;
			}
			SafeFileHandle safeFileHandle = OpenDevice(VVDA_INTERFACE_GUID);
			if (safeFileHandle.IsInvalid)
			{
				safeFileHandle.Dispose();
				return;
			}
			deviceHandle = safeFileHandle;
			VirtualDisplayGetWatchdogOut watchdogOut;
			bool watchdog = GetWatchdog(deviceHandle, out watchdogOut);
			int watchdogMs = (int)((watchdog && watchdogOut.Timeout != 0) ? (watchdogOut.Timeout * 1000) : 3000);
			int pingIntervalMs = (int)((watchdog && watchdogOut.Timeout != 0) ? (watchdogOut.Timeout * 1000 / 2) : 1000);
			Logger.Info($"[VDD] driver watchdog: queried={watchdog} timeout={watchdogOut.Timeout}s countdown={watchdogOut.Countdown} → ping interval {pingIntervalMs}ms");
			CancellationToken token = pingLoopCts.Token;
			SafeFileHandle handle = deviceHandle;
			string id = clientId;
			pingThread = new Thread((ThreadStart)delegate
			{
				PingLoop(handle, id, pingIntervalMs, watchdogMs, token);
			})
			{
				IsBackground = true,
				Name = "VddPingLoop",
				Priority = ThreadPriority.Highest
			};
			pingThread.Start();
			opened = true;
		}
	}

	private void EnsureInit()
	{
		if (!opened && !IsDisposing)
		{
			Init();
		}
	}

	private void PingLoop(SafeFileHandle handle, string id, int pingIntervalMs, int watchdogMs, CancellationToken token)
	{
		DisableCurrentThreadPowerThrottling();
		Logger.Info($"[VDD] ping loop started: interval={pingIntervalMs}ms watchdog={watchdogMs}ms");
		Stopwatch stopwatch = Stopwatch.StartNew();
		long num = stopwatch.ElapsedMilliseconds;
		while (!token.IsCancellationRequested)
		{
			long elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
			if (elapsedMilliseconds - num > watchdogMs)
			{
				Logger.Warning($"[VDD] ping loop stalled {elapsedMilliseconds - num}ms (> watchdog {watchdogMs}ms)");
			}
			num = elapsedMilliseconds;
			if (!DriverPing(handle, id))
			{
				Logger.Error($"[VDD] ping failed (err={Marshal.GetLastWin32Error()})");
			}
			token.WaitHandle.WaitOne(pingIntervalMs);
		}
		Logger.Info("[VDD] ping loop exited");
	}

	private static string CreateClientId()
	{
		string arg;
		int num;
		try
		{
			using Process process = Process.GetCurrentProcess();
			arg = process.ProcessName;
			num = process.Id;
		}
		catch
		{
			arg = "client";
			num = 0;
		}
		return $"{arg}:{num}:{Guid.NewGuid():N}";
	}

	public static Guid CreateMonitorGuid(ulong random = 0uL)
	{
		long ticks = DateTime.UtcNow.Ticks;
		byte[] array = new byte[16];
		BitConverter.GetBytes(ticks).CopyTo(array, 0);
		if (random != 0L)
		{
			BitConverter.GetBytes(random).CopyTo(array, 8);
			return new Guid(array);
		}
		using (RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create())
		{
			randomNumberGenerator.GetBytes(array, 8, 8);
		}
		return new Guid(array);
	}

	public unsafe bool AddVirtualDisplay(uint width, uint height, uint refreshRate, Guid monitorGuid, string deviceName, string serialNumber, out DisplayTargetToken output)
	{
		EnsureInit();
		lock (syncRoot)
		{
			if (IsDisposing)
			{
				output = default(DisplayTargetToken);
				return false;
			}
			if (displays.ContainsKey(monitorGuid))
			{
				output = default(DisplayTargetToken);
				return false;
			}
			if (string.IsNullOrEmpty(deviceName))
			{
				deviceName = "VIT-VDD";
			}
			VirtualDisplayAddParams virtualDisplayAddParams = default(VirtualDisplayAddParams);
			virtualDisplayAddParams.Width = width;
			virtualDisplayAddParams.Height = height;
			virtualDisplayAddParams.RefreshRate = refreshRate;
			virtualDisplayAddParams.MonitorGuid = monitorGuid;
			VirtualDisplayAddParams virtualDisplayAddParams2 = virtualDisplayAddParams;
			SetUtf8(virtualDisplayAddParams2.DeviceName, 14, deviceName);
			SetUtf8(virtualDisplayAddParams2.SerialNumber, 14, serialNumber);
			SetUtf8(virtualDisplayAddParams2.ClientId, 128, clientId);
			VirtualDisplayAddOut addOut;
			bool num = AddVirtualDisplay(deviceHandle, virtualDisplayAddParams2, out addOut);
			output = new DisplayTargetToken(addOut.AdapterLuid.ToUInt64(), addOut.TargetId);
			if (num)
			{
				displays.Add(monitorGuid, new DisplayEntry
				{
					Token = output,
					Params = virtualDisplayAddParams2
				});
			}
			return num;
		}
	}

	public bool AddVirtualDisplay(uint width, uint height, uint refreshRate, string deviceName, string serialNumber, out (DisplayTargetToken Token, Guid MonitorGuid) output)
	{
		lock (syncRoot)
		{
			if (IsDisposing)
			{
				output = default((DisplayTargetToken, Guid));
				return false;
			}
			Guid guid = CreateMonitorGuid(0uL);
			DisplayTargetToken output2;
			bool result = AddVirtualDisplay(width, height, refreshRate, guid, deviceName, serialNumber, out output2);
			output = (Token: output2, MonitorGuid: guid);
			return result;
		}
	}

	public bool RemoveVirtualDisplay(Guid monitorGuid)
	{
		lock (syncRoot)
		{
			if (IsDisposing)
			{
				return false;
			}
			return RemoveVirtualDisplayCore(monitorGuid);
		}
	}

	public int RemoveVirtualDisplay(Func<(int Index, int Count, DisplayTargetToken Token, uint Width, uint Height, uint RefreshRate, Guid MonitorGuid, string DeviceName, string SerialNumber), bool>? predicate = null)
	{
		Func<(int Index, int Count, DisplayTargetToken Token, uint Width, uint Height, uint RefreshRate, Guid MonitorGuid, string DeviceName, string SerialNumber), bool> predicate2 = predicate;
		lock (syncRoot)
		{
			if (IsDisposing)
			{
				return 0;
			}
			KeyValuePair<Guid, DisplayEntry>[] obj = ((predicate2 == null) ? displays.ToArray() : displays.Where((KeyValuePair<Guid, DisplayEntry> kv, int i) => predicate2((i, displays.Count, kv.Value.Token, kv.Value.Params.Width, kv.Value.Params.Height, kv.Value.Params.RefreshRate, kv.Value.Params.MonitorGuid, kv.Value.DeviceName, kv.Value.SerialNumber))).ToArray());
			int num = 0;
			KeyValuePair<Guid, DisplayEntry>[] array = obj;
			foreach (KeyValuePair<Guid, DisplayEntry> keyValuePair in array)
			{
				num += (RemoveVirtualDisplayCore(keyValuePair.Key) ? 1 : 0);
			}
			return num;
		}
	}

	public IEnumerable<(DisplayTargetToken Token, uint Width, uint Height, uint RefreshRate, Guid MonitorGuid, string DeviceName, string SerialNumber)> GetVirtualDisplays()
	{
		lock (syncRoot)
		{
			if (IsDisposing)
			{
				return Enumerable.Empty<(DisplayTargetToken, uint, uint, uint, Guid, string, string)>();
			}
			return displays.Select((KeyValuePair<Guid, DisplayEntry> kv) => (Token: kv.Value.Token, Width: kv.Value.Params.Width, Height: kv.Value.Params.Height, RefreshRate: kv.Value.Params.RefreshRate, MonitorGuid: kv.Value.Params.MonitorGuid, DeviceName: kv.Value.DeviceName, SerialNumber: kv.Value.SerialNumber)).ToArray();
		}
	}

	public bool SetRenderAdapter(ulong adapterLuid)
	{
		lock (syncRoot)
		{
			if (IsDisposing)
			{
				return false;
			}
			return SetRenderAdapter(deviceHandle, new VirtualDisplaySetRenderAdapterParams
			{
				AdapterLuid = adapterLuid.ToLuid()
			});
		}
	}

	private bool RemoveVirtualDisplayCore(Guid monitorGuid)
	{
		bool num = RemoveVirtualDisplay(deviceHandle, new VirtualDisplayRemoveParams
		{
			MonitorGuid = monitorGuid
		});
		if (num)
		{
			displays.Remove(monitorGuid);
		}
		return num;
	}

	protected virtual void Dispose(bool disposing)
	{
		if (Interlocked.Exchange(ref disposeState, 1) != 0)
		{
			return;
		}
		try
		{
			pingLoopCts.Cancel();
		}
		catch
		{
		}
		if (disposing)
		{
			try
			{
				pingThread?.Join(2000);
			}
			catch
			{
			}
		}
		Action action = delegate
		{
			if (!deviceHandle.IsInvalid && !deviceHandle.IsClosed)
			{
				Guid[] array = displays.Keys.ToArray();
				foreach (Guid monitorGuid in array)
				{
					try
					{
						RemoveVirtualDisplay(deviceHandle, new VirtualDisplayRemoveParams
						{
							MonitorGuid = monitorGuid
						});
					}
					catch
					{
					}
				}
			}
		};
		if (disposing)
		{
			lock (syncRoot)
			{
				action();
			}
		}
		else
		{
			action();
		}
		if (disposing)
		{
			pingLoopCts.Dispose();
			deviceHandle.Dispose();
		}
	}

	~SudoVirtualDisplay()
	{
		Dispose(disposing: false);
	}

	public void Dispose()
	{
		Dispose(disposing: true);
		GC.SuppressFinalize(this);
	}
}
