using System;
using System.Threading;

namespace VitureCommonLibrary;

internal class LibUsbHidDevice : IDisposable
{
	private IntPtr _devRef;

	private IntPtr _devHandle;

	private readonly object _handleLock = new object();

	private int _interfaceNumber;

	private byte _endpointIn;

	private byte _endpointOut;

	private ushort _maxPacketSize;

	private int _writeErrorStreak;

	private const int WRITE_HEAL_EVERY = 3;

	private int _readErrorStreak;

	private const int READ_HEAL_ATTEMPTS = 3;

	public int ProductId { get; }

	public string ProductHexId => $"0x{ProductId:X4}";

	public string DevicePath { get; }

	public bool IsOpen { get; private set; }

	public int LastOpenError { get; private set; }

	public int InterfaceNumber => _interfaceNumber;

	public byte EndpointIn => _endpointIn;

	public byte EndpointOut => _endpointOut;

	public ushort MaxPacketSize => _maxPacketSize;

	public string DiagTag => $"[pid={ProductHexId} iface={_interfaceNumber} epIn=0x{_endpointIn:X2} epOut=0x{_endpointOut:X2}]";

	internal LibUsbHidDevice(IntPtr devRef, int productId, string path, int interfaceNumber, byte epIn, byte epOut, ushort maxPacketSize)
	{
		_devRef = devRef;
		_devHandle = IntPtr.Zero;
		_interfaceNumber = interfaceNumber;
		_endpointIn = epIn;
		_endpointOut = epOut;
		_maxPacketSize = (ushort)((maxPacketSize > 0) ? maxPacketSize : 64);
		ProductId = productId;
		DevicePath = path;
		IsOpen = false;
	}

	internal void OpenDevice()
	{
		if (IsOpen || _devRef == IntPtr.Zero)
		{
			return;
		}
		for (int i = 1; i <= 3; i++)
		{
			Thread.Sleep(1000);
			TryOpenOnce(i, 3);
			if (IsOpen)
			{
				return;
			}
		}
		Logger.Error($"[HID-OPEN] {DiagTag} give up after {3} attempts, IsOpen=False ts={DateTime.UtcNow:HH:mm:ss.fff}");
	}

	private void TryOpenOnce(int attempt, int maxAttempts)
	{
		LibUsbHidHelper.EnsureInitialized();
		Logger.Info($"[HID-OPEN] {DiagTag} libusb_open begin attempt={attempt}/{maxAttempts} ts={DateTime.UtcNow:HH:mm:ss.fff}");
		int num2 = (LastOpenError = LibUsbNative.libusb_open(_devRef, out _devHandle));
		if (num2 != 0 || _devHandle == IntPtr.Zero)
		{
			Logger.Error($"[HID-OPEN] {DiagTag} libusb_open FAILED ret={num2} attempt={attempt}/{maxAttempts} ts={DateTime.UtcNow:HH:mm:ss.fff}");
			_devHandle = IntPtr.Zero;
			IsOpen = false;
			return;
		}
		Logger.Info($"[HID-OPEN] {DiagTag} libusb_open OK ts={DateTime.UtcNow:HH:mm:ss.fff}");
		int num3 = LibUsbNative.libusb_set_auto_detach_kernel_driver(_devHandle, 1);
		Logger.Info($"[HID-OPEN] {DiagTag} auto_detach_kernel_driver ret={num3}");
		num2 = LibUsbNative.libusb_claim_interface(_devHandle, _interfaceNumber);
		if (num2 != 0)
		{
			Logger.Error($"[HID-OPEN] {DiagTag} libusb_claim_interface FAILED ret={num2} attempt={attempt}/{maxAttempts} ts={DateTime.UtcNow:HH:mm:ss.fff}");
			LibUsbNative.libusb_close(_devHandle);
			_devHandle = IntPtr.Zero;
			IsOpen = false;
		}
		else
		{
			IsOpen = true;
			Logger.Info($"[HID-OPEN] {DiagTag} claim_interface OK, device READY maxPkt={_maxPacketSize} attempt={attempt}/{maxAttempts} ts={DateTime.UtcNow:HH:mm:ss.fff}");
		}
	}

	internal byte[]? Read(int timeoutMs = 100)
	{
		lock (_handleLock)
		{
			if (!IsOpen || _devHandle == IntPtr.Zero)
			{
				return null;
			}
			byte[] array = new byte[_maxPacketSize + 1];
			int actual_length;
			int num = LibUsbNative.libusb_interrupt_transfer(_devHandle, _endpointIn, array, array.Length, out actual_length, (uint)timeoutMs);
			if (num == 0 && actual_length > 1)
			{
				_readErrorStreak = 0;
				int num2 = actual_length - 1;
				byte[] array2 = new byte[num2];
				Array.Copy(array, 0, array2, 0, num2);
				return array2;
			}
			switch (num)
			{
			case -7:
				return null;
			case -4:
				IsOpen = false;
				return null;
			case -9:
			case -1:
				_readErrorStreak++;
				if (_readErrorStreak <= 3)
				{
					int num3 = LibUsbNative.libusb_clear_halt(_devHandle, _endpointIn);
					Logger.Warning($"[HID-HEAL] {DiagTag} read err ret={num} streak={_readErrorStreak} clear_halt(epIn) ret={num3}");
				}
				else
				{
					IsOpen = false;
				}
				break;
			}
			return null;
		}
	}

	internal bool Write(byte[] data)
	{
		if (!IsOpen || _devHandle == IntPtr.Zero)
		{
			return false;
		}
		int num;
		bool flag;
		if (_endpointOut != 0)
		{
			num = LibUsbNative.libusb_interrupt_transfer(_devHandle, _endpointOut, data, data.Length, out var actual_length, 500u);
			flag = num == 0;
			Logger.Info($"[HID-TX] {DiagTag} interrupt_transfer ret={num} actual={actual_length} ok={flag} ts={DateTime.UtcNow:HH:mm:ss.fff} data={BitConverter.ToString(data, 0, Math.Min(data.Length, 10))}...");
		}
		else
		{
			num = LibUsbNative.libusb_control_transfer(_devHandle, 33, 9, 512, (ushort)_interfaceNumber, data, (ushort)data.Length, 500u);
			flag = num >= 0;
			Logger.Info($"[HID-TX] {DiagTag} control_transfer(SET_REPORT) ret={num} ok={flag} ts={DateTime.UtcNow:HH:mm:ss.fff} data={BitConverter.ToString(data, 0, Math.Min(data.Length, 10))}...");
		}
		if (flag)
		{
			_writeErrorStreak = 0;
		}
		else if (IsRecoverableTransferError(num))
		{
			_writeErrorStreak++;
			if (_writeErrorStreak % 3 == 0)
			{
				ClearHaltEndpoints(num);
			}
		}
		return flag;
	}

	private static bool IsRecoverableTransferError(int ret)
	{
		if (ret != -7 && ret != -9 && ret != -1)
		{
			return ret == -99;
		}
		return true;
	}

	private void ClearHaltEndpoints(int lastErr)
	{
		lock (_handleLock)
		{
			if (!(_devHandle == IntPtr.Zero))
			{
				int num = ((_endpointOut != 0) ? LibUsbNative.libusb_clear_halt(_devHandle, _endpointOut) : 0);
				int num2 = LibUsbNative.libusb_clear_halt(_devHandle, _endpointIn);
				Logger.Warning($"[HID-HEAL] {DiagTag} clear_halt epOut ret={num} epIn ret={num2} streak={_writeErrorStreak} lastErr={lastErr}");
			}
		}
	}

	internal void CloseDevice()
	{
		lock (_handleLock)
		{
			if (_devHandle != IntPtr.Zero)
			{
				if (IsOpen)
				{
					LibUsbNative.libusb_release_interface(_devHandle, _interfaceNumber);
				}
				LibUsbNative.libusb_close(_devHandle);
				_devHandle = IntPtr.Zero;
			}
			IsOpen = false;
		}
	}

	public void Dispose()
	{
		lock (_handleLock)
		{
			CloseDevice();
			if (_devRef != IntPtr.Zero)
			{
				LibUsbNative.libusb_unref_device(_devRef);
				_devRef = IntPtr.Zero;
			}
		}
	}
}
