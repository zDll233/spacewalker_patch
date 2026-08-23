using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace VitureCommonLibrary;

internal static class GlassesMonitorHelper
{
	private sealed class CoalescingScanner
	{
		private readonly Action _scan;

		private readonly ConcurrentExclusiveSchedulerPair _schedulerPair = new ConcurrentExclusiveSchedulerPair();

		private int _state;

		internal CoalescingScanner(Action scan)
		{
			_scan = scan;
		}

		internal void Queue()
		{
			if (Interlocked.CompareExchange(ref _state, 1, 0) != 0)
			{
				Interlocked.CompareExchange(ref _state, 2, 1);
				return;
			}
			Task.Factory.StartNew(delegate
			{
				while (true)
				{
					try
					{
						_scan();
					}
					catch
					{
					}
					if (Interlocked.CompareExchange(ref _state, 0, 1) == 1)
					{
						break;
					}
					Interlocked.Exchange(ref _state, 1);
				}
			}, CancellationToken.None, TaskCreationOptions.DenyChildAttach, _schedulerPair.ExclusiveScheduler);
		}
	}

	private static int _deviceCount = 0;

	private static bool _bootMode = false;

	private static int _hidDeviceCount = 0;

	private static bool _hidBootMode = false;

	private static int _physicalMonitorCount = 0;

	internal static bool _vitureDisplayConnected;

	internal static bool _vitureDisplayActive;

	internal static bool _wideDisplayConnected;

	private static bool isStart = false;

	private static object lock_obj = new object();

	private static int _hotplugHandle = -1;

	private static LibUsbNative.libusb_hotplug_callback_fn _hotplugCb;

	private static CancellationTokenSource _eventCts = new CancellationTokenSource();

	private static Task _eventLoopTask = Task.CompletedTask;

	private static readonly CoalescingScanner _deviceScan = new CoalescingScanner(delegate
	{
		FilterHidDevices();
		FilterUsbDevices();
	});

	private static readonly CoalescingScanner _displayScan = new CoalescingScanner(FilterDisplayInfo);

	private static bool? _libusbHotplugCapability;

	internal static void Start()
	{
		lock (lock_obj)
		{
			if (!isStart)
			{
				if (!GlassesDeviceManager.IsRunInUnity)
				{
					DisplayManager2.Instance.DisplayChanged += _displayScan.Queue;
				}
				if (ProbeLibusbHotplugCapability())
				{
					StartLibusbHotplug();
				}
				_displayScan.Queue();
				isStart = true;
			}
		}
	}

	private static void StartLibusbHotplug()
	{
		try
		{
			IntPtr context = LibUsbHidHelper.GetContext();
			_hotplugCb = OnHotplugEvent;
			int num = LibUsbNative.libusb_hotplug_register_callback(context, 3, 1, 13770, -1, -1, _hotplugCb, IntPtr.Zero, out _hotplugHandle);
			if (num != 0)
			{
				Logger.Error($"[GlassesMonitor] libusb_hotplug_register_callback failed: {num}");
				_hotplugHandle = -1;
			}
			_eventCts = new CancellationTokenSource();
			CancellationTokenSource cts = _eventCts;
			_eventLoopTask = Task.Factory.StartNew(delegate
			{
				EventLoopProc(cts.Token);
			}, cts.Token, TaskCreationOptions.LongRunning | TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
			Logger.Info("[GlassesMonitor] libusb hotplug registered and event loop started.");
		}
		catch (Exception ex)
		{
			Logger.Error("[GlassesMonitor] Failed to start libusb hotplug: " + ex.Message, ex.StackTrace);
		}
	}

	private static void EventLoopProc(CancellationToken token)
	{
		try
		{
			IntPtr context = LibUsbHidHelper.GetContext();
			while (!token.IsCancellationRequested)
			{
				try
				{
					int num = LibUsbNative.libusb_handle_events(context);
					if (num != 0 && num != -10)
					{
						Logger.Error($"[GlassesMonitor] libusb_handle_events error: {num}");
						token.WaitHandle.WaitOne(500);
					}
				}
				catch
				{
				}
			}
		}
		catch
		{
		}
	}

	private static int OnHotplugEvent(IntPtr ctx, IntPtr device, int hotplugEvent, IntPtr userData)
	{
		try
		{
			switch (hotplugEvent)
			{
			case 1:
				Logger.Info("[GlassesMonitor] libusb hotplug: device arrived.");
				break;
			case 2:
				Logger.Info("[GlassesMonitor] libusb hotplug: device left.");
				break;
			default:
				Logger.Info($"[GlassesMonitor] libusb hotplug: event={hotplugEvent}.");
				break;
			}
			_deviceScan.Queue();
		}
		catch (Exception ex)
		{
			Logger.Error(ex.Message, ex.StackTrace);
		}
		return 0;
	}

	internal static void Stop()
	{
		lock (lock_obj)
		{
			if (isStart)
			{
				DisplayManager2.Instance.DisplayChanged -= _displayScan.Queue;
				StopLibusbHotplug();
				isStart = false;
			}
		}
	}

	private static void StopLibusbHotplug()
	{
		if (_eventLoopTask.IsCompleted && _hotplugHandle == -1)
		{
			return;
		}
		try
		{
			IntPtr context = LibUsbHidHelper.GetContext();
			if (_hotplugHandle != -1)
			{
				LibUsbNative.libusb_hotplug_deregister_callback(context, _hotplugHandle);
				_hotplugHandle = -1;
			}
			_eventCts.Cancel();
			LibUsbNative.libusb_interrupt_event_handler(context);
			_eventLoopTask.Wait(1000);
			_eventCts.Dispose();
			_eventLoopTask = Task.CompletedTask;
			Logger.Info("[GlassesMonitor] libusb hotplug deregistered and event loop stopped.");
		}
		catch (Exception ex)
		{
			Logger.Error("[GlassesMonitor] Error stopping libusb hotplug: " + ex.Message, ex.StackTrace);
		}
	}

	private static void FilterDisplayInfo()
	{
		try
		{
			int num = DisplayManager2.Instance.GetPhysicalDisplays().Length;
			if (num != _physicalMonitorCount)
			{
				_physicalMonitorCount = num;
				DisplayManager2.Instance.PhysicalMonitorChanged?.Invoke(num);
			}
			DisplayInfo? displayInfo = DisplayManager2.Instance.GetVitureDisplays().FirstOrDefault();
			bool flag = displayInfo?.IsConnected ?? false;
			bool flag2 = displayInfo?.IsActive ?? false;
			if (flag != _vitureDisplayConnected || flag2 != _vitureDisplayActive)
			{
				Logger.Info($"[GlassesMonitor] VitureDisplay: connected={flag} active={flag2}");
				_vitureDisplayConnected = flag;
				_vitureDisplayActive = flag2;
				DisplayManager2.Instance.VitureDisplayConnectChanged?.Invoke(_vitureDisplayActive, _vitureDisplayConnected);
			}
			bool flag3 = DisplayManager2.Instance.GetWideDisplay() != null;
			if (flag3 != _wideDisplayConnected)
			{
				_wideDisplayConnected = flag3;
				DisplayManager2.Instance.WideDisplayConnectChanged?.Invoke(_wideDisplayConnected);
			}
		}
		catch (Exception ex)
		{
			Logger.Error("[GlassesMonitor] FilterDisplayInfo error: " + ex.Message, ex.StackTrace);
		}
	}

	private static void FilterHidDevices()
	{
		try
		{
			FilterHidDevicesInternal();
		}
		catch (Exception ex)
		{
			Logger.Error("[GlassesMonitor] FilterHidDevices error: " + ex.Message, ex.StackTrace);
		}
	}

	private static int PickNewerModelPid(List<LibUsbHidDeviceInfo>? devices, bool appMode)
	{
		if (devices == null)
		{
			return 0;
		}
		int[] array = new int[4] { 4609, 4625, 4865, 4881 };
		int[] array2 = new int[4] { 4608, 4624, 4864, 4880 };
		int[] primary = (appMode ? array : array2);
		int[] fallback = (appMode ? array2 : array);
		return (devices.FirstOrDefault((LibUsbHidDeviceInfo x) => primary.Contains(x.ProductId)) ?? devices.FirstOrDefault((LibUsbHidDeviceInfo x) => fallback.Contains(x.ProductId)))?.ProductId ?? 0;
	}

	private static void FilterHidDevicesInternal()
	{
		List<LibUsbHidDeviceInfo> list = LibUsbHidHelper.Enumerate(13770);
		list?.RemoveAll((LibUsbHidDeviceInfo d) => d.ProductId == 4354);
		int num = list?.Count ?? 0;
		string text = ((list == null) ? "<null>" : string.Join(",", list.Select((LibUsbHidDeviceInfo d) => $"0x{d.ProductId:X4}")));
		Logger.Info($"[GlassesMonitor] FilterHid: count={num} pids=[{text}] _hidDeviceCount={_hidDeviceCount} _hidBootMode={_hidBootMode}");
		if (num == 1 && _hidDeviceCount == 0)
		{
			int valueOrDefault = (list?.FirstOrDefault()?.ProductId).GetValueOrDefault();
			if (valueOrDefault == 4609 || valueOrDefault == 4625 || valueOrDefault == 4865 || valueOrDefault == 4881)
			{
				_hidBootMode = false;
				_hidDeviceCount = num;
				Logger.Info($"VITURE Glasses HID App Mode Inserted (single-iface, pid=0x{valueOrDefault:X4})");
				GlassesDeviceManager.Instance.IsConnected = true;
				GlassesDeviceManager.Instance.ProductId = valueOrDefault;
				GlassesDeviceManager.Instance.UseHidDevice = true;
				GlassesDeviceManager.Instance.AppMode = true;
				GlassesDeviceManager.Instance.DeviceConnectChanged?.Invoke(obj: true);
			}
			else if (!_hidBootMode && (valueOrDefault & 0xF000) == 4096 && valueOrDefault % 2 == 0)
			{
				_hidBootMode = true;
				_hidDeviceCount = 0;
				GlassesDeviceManager.Instance.IsConnected = true;
				GlassesDeviceManager.Instance.UseHidDevice = true;
				GlassesDeviceManager.Instance.AppMode = !_hidBootMode;
				GlassesDeviceManager.Instance.DeviceEnterBootMode?.Invoke(_hidBootMode);
			}
		}
		else if (_hidBootMode && num < 3)
		{
			_hidBootMode = false;
			GlassesDeviceManager.Instance.IsConnected = false;
			GlassesDeviceManager.Instance.UseHidDevice = true;
			GlassesDeviceManager.Instance.AppMode = !_hidBootMode;
			GlassesDeviceManager.Instance.DeviceEnterBootMode?.Invoke(_hidBootMode);
		}
		if (num >= 2 && _hidDeviceCount == 0)
		{
			int num2 = (list?.FirstOrDefault()?.ProductId).GetValueOrDefault();
			if (num > 2)
			{
				num2 = PickNewerModelPid(list, appMode: true);
			}
			if ((num2 & 0xF000) == 4096 && num2 % 2 == 1)
			{
				_hidBootMode = false;
				_hidDeviceCount = num;
				Logger.Info("VITURE Glasses HID App Mode Inserted");
				GlassesDeviceManager.Instance.IsConnected = true;
				GlassesDeviceManager.Instance.ProductId = num2;
				GlassesDeviceManager.Instance.UseHidDevice = true;
				GlassesDeviceManager.Instance.AppMode = true;
				GlassesDeviceManager.Instance.DeviceConnectChanged?.Invoke(obj: true);
			}
		}
		if (!_hidBootMode && num > 2)
		{
			int num3 = PickNewerModelPid(list, appMode: false);
			if ((num3 & 0xF000) == 4096 && num3 % 2 == 0 && num3 != 4354)
			{
				_hidBootMode = true;
				_hidDeviceCount = 0;
				Logger.Info("VITURE Glasses HID Boot Mode Inserted");
				GlassesDeviceManager.Instance.IsConnected = true;
				GlassesDeviceManager.Instance.ProductId = num3;
				GlassesDeviceManager.Instance.UseHidDevice = true;
				GlassesDeviceManager.Instance.AppMode = false;
				GlassesDeviceManager.Instance.DeviceEnterBootMode?.Invoke(obj: true);
			}
		}
		if (num == 0 && _hidDeviceCount >= 1)
		{
			_hidBootMode = false;
			_hidDeviceCount = num;
			Logger.Info("VITURE Glasses HID Removed");
			GlassesDeviceManager.Instance.IsConnected = false;
			GlassesDeviceManager.Instance.UseHidDevice = true;
			GlassesDeviceManager.Instance.AppMode = false;
			GlassesDeviceManager.Instance.DeviceConnectChanged?.Invoke(obj: false);
			GlassesDeviceManager.Instance.ProductId = 0;
		}
	}

	private static bool ProbeLibusbHotplugCapability()
	{
		if (_libusbHotplugCapability.HasValue)
		{
			return _libusbHotplugCapability.Value;
		}
		try
		{
			LibUsbHidHelper.EnsureInitialized();
			int num = LibUsbNative.libusb_has_capability(1u);
			_libusbHotplugCapability = num != 0;
			Logger.Info($"[LibUsb] LIBUSB_CAP_HAS_HOTPLUG (WINDOWS_HOTPLUG) supported: {_libusbHotplugCapability.Value}");
		}
		catch (Exception ex)
		{
			_libusbHotplugCapability = false;
			Logger.Error("[LibUsb] libusb_has_capability probe failed: " + ex.Message, ex.StackTrace);
		}
		return _libusbHotplugCapability.Value;
	}

	private static List<int> EnumerateUsbPids(int vendorId)
	{
		List<int> list = new List<int>();
		LibUsbHidHelper.EnsureInitialized();
		IntPtr list2;
		int num = LibUsbNative.libusb_get_device_list(LibUsbHidHelper.GetContext(), out list2);
		if (num < 0 || list2 == IntPtr.Zero)
		{
			return list;
		}
		try
		{
			for (int i = 0; i < num; i++)
			{
				IntPtr intPtr = Marshal.ReadIntPtr(list2, i * IntPtr.Size);
				if (intPtr == IntPtr.Zero)
				{
					break;
				}
				if (LibUsbNative.libusb_get_device_descriptor(intPtr, out var desc) == 0 && desc.idVendor == (ushort)vendorId && desc.idProduct != 4354)
				{
					list.Add(desc.idProduct);
				}
			}
		}
		finally
		{
			LibUsbNative.libusb_free_device_list(list2, 1);
		}
		return list;
	}

	private static void FilterUsbDevices()
	{
		try
		{
			FilterUsbDevicesInternal();
		}
		catch (Exception ex)
		{
			Logger.Error("[GlassesMonitor] FilterUsbDevices error: " + ex.Message, ex.StackTrace);
		}
	}

	private static void FilterUsbDevicesInternal()
	{
		List<int> list = EnumerateUsbPids(13770);
		int count = list.Count;
		if (count == 0 && _deviceCount != 0)
		{
			bool bootMode = _bootMode;
			_bootMode = false;
			Logger.Info("VITURE Glasses USB Removed");
			GlassesDeviceManager.Instance.IsConnected = false;
			if (bootMode)
			{
				GlassesDeviceManager.Instance.UseHidDevice = false;
				GlassesDeviceManager.Instance.AppMode = true;
				GlassesDeviceManager.Instance.DeviceEnterBootMode?.Invoke(obj: false);
			}
			else
			{
				GlassesDeviceManager.Instance.UseHidDevice = false;
				GlassesDeviceManager.Instance.AppMode = true;
				GlassesDeviceManager.Instance.DeviceConnectChanged?.Invoke(obj: false);
			}
			GlassesDeviceManager.Instance.ProductId = 0;
			_deviceCount = 0;
			_bootMode = false;
		}
		else
		{
			if (count <= 0)
			{
				return;
			}
			GlassesDeviceManager.Instance.ProductId = list.FirstOrDefault();
			if (list.All((int x) => x == 4352 || x == 4355))
			{
				if (!_bootMode)
				{
					_bootMode = true;
					_deviceCount = 1;
					GlassesDeviceManager.Instance.UseHidDevice = false;
					GlassesDeviceManager.Instance.AppMode = !_bootMode;
					GlassesDeviceManager.Instance.IsConnected = true;
					GlassesDeviceManager.Instance.DeviceEnterBootMode?.Invoke(obj: true);
				}
			}
			else if (list.All((int x) => x == 4353 || x == 4356) && _deviceCount == 0)
			{
				_bootMode = false;
				_deviceCount = 1;
				Logger.Info("VITURE Glasses USB Inserted");
				GlassesDeviceManager.Instance.UseHidDevice = false;
				GlassesDeviceManager.Instance.AppMode = !_bootMode;
				GlassesDeviceManager.Instance.IsConnected = true;
				GlassesDeviceManager.Instance.DeviceConnectChanged?.Invoke(obj: true);
			}
		}
	}
}
