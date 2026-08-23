using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using LibUsbDotNet;
using LibUsbDotNet.Main;

namespace VitureCommonLibrary;

public class VitureUsbDevice
{
	private const int BUFFER_LEN = 512;

	private const int OTA_BUFFER_LEN = 6;

	private int readBufferLen = 262;

	private bool readFirmwareState;

	private ManualResetEventSlim otaReadSlim = new ManualResetEventSlim(initialState: false);

	private ManualResetEventSlim otaWriteSlim = new ManualResetEventSlim(initialState: false);

	private static object lock_obj = new object();

	private static int deviceCount = 0;

	private static bool bootMode = false;

	private volatile bool _disposing;

	private List<Thread>? _recvThreads;

	private UsbDevice? _device;

	private UsbEndpointWriter? _writer;

	private UsbEndpointReader? _reader;

	private UsbEndpointWriter? _ota_writer;

	private UsbEndpointReader? _ota_reader;

	private int _writeErrorStreak;

	private const int WRITE_HEAL_EVERY = 3;

	private const int READ_HEAL_ATTEMPTS = 3;

	public byte[]? FirmwareReadCache;

	private static readonly Lazy<VitureUsbDevice> instance = new Lazy<VitureUsbDevice>(() => new VitureUsbDevice());

	public static VitureUsbDevice Instance => instance.Value;

	internal event Action<byte[]>? ReceivedUsbData;

	private VitureUsbDevice()
	{
		GlassesMonitorHelper.Start();
	}

	internal void Initialize()
	{
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Expected O, but got Unknown
		if (_recvThreads != null)
		{
			Dispose();
		}
		lock (lock_obj)
		{
			UsbRegDeviceList source = UsbDevice.AllDevices.FindAll((Predicate<UsbRegistry>)((UsbRegistry x) => x.Vid == 13770));
			GlassesDeviceManager glassesDeviceManager = GlassesDeviceManager.Instance;
			UsbRegistry? obj = ((IEnumerable<UsbRegistry>)source).FirstOrDefault();
			glassesDeviceManager.ProductId = ((obj != null) ? obj.Pid : 0);
			UsbDeviceFinder val = new UsbDeviceFinder(13770, GlassesDeviceManager.Instance.ProductId);
			_device = UsbDevice.OpenUsbDevice(val);
			if (_device == null)
			{
				return;
			}
			UsbDevice? device = _device;
			IUsbDevice val2 = (IUsbDevice)(object)((device is IUsbDevice) ? device : null);
			if (val2 != null)
			{
				val2.SetConfiguration((byte)1);
				val2.ClaimInterface(0);
			}
			_ota_writer = _device.OpenEndpointWriter((WriteEndpointID)4);
			_ota_reader = _device.OpenEndpointReader((ReadEndpointID)133);
			_ota_reader.ReadBufferSize = 6;
			if (!bootMode)
			{
				_writer = _device.OpenEndpointWriter((WriteEndpointID)6);
				_reader = _device.OpenEndpointReader((ReadEndpointID)135);
				_reader.ReadBufferSize = 512;
			}
			Logger.Info("OpenDevice: " + GlassesDeviceManager.Instance.ProductId.ToString("X"));
			_disposing = false;
			if (_recvThreads == null)
			{
				_recvThreads = new List<Thread>();
			}
			Thread thread = new Thread((ThreadStart)delegate
			{
				ReadUsbData(_ota_reader, isOta: true);
			});
			thread.Name = "OTA_Recv_Th";
			_recvThreads.Add(thread);
			thread.Start();
			if (!bootMode)
			{
				Thread thread2 = new Thread((ThreadStart)delegate
				{
					ReadUsbData(_reader);
				});
				thread2.Name = "USB_Recv_Th";
				_recvThreads.Add(thread2);
				thread2.Start();
			}
		}
		Logger.Info("VitureUsbDevice Initialize success");
	}

	private void ReadUsbData(UsbEndpointReader? reader, bool isOta = false)
	{
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Invalid comparison between Unknown and I4
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Invalid comparison between Unknown and I4
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		int num = 0;
		while (!_disposing)
		{
			UsbDevice? device = _device;
			if (device == null || !device.IsOpen || reader == null)
			{
				break;
			}
			byte[] array = new byte[0];
			if (isOta)
			{
				otaWriteSlim.Reset();
				otaWriteSlim.Wait();
				array = new byte[readFirmwareState ? readBufferLen : 6];
			}
			else
			{
				array = new byte[512];
			}
			int i = 0;
			int num2 = 0;
			ErrorCode val = (ErrorCode)0;
			try
			{
				for (; i < array.Length; i += num2)
				{
					if ((int)val != -16371 && (int)val != 0)
					{
						break;
					}
					if (_disposing)
					{
						break;
					}
					val = reader.Read(array, i, array.Length - i, 1000, ref num2);
				}
				if ((int)val != -16371 && (int)val != 0)
				{
					Logger.Error($"isOta: {isOta} Read Err: {val}");
					num++;
					if (num <= 3)
					{
						bool flag = ((UsbEndpointBase)reader).Reset();
						Logger.Warning($"[USB-HEAL] read isOta={isOta} streak={num} endpoint Reset ok={flag}");
						continue;
					}
					break;
				}
				num = 0;
				Logger.Info($"ReadUsbData isOta: {isOta} bytes: {BitConverter.ToString(array)}");
				if (isOta)
				{
					UsbOtaMessageAck usbOtaMessageAck = UsbOtaMessageAck.FromBytes(array);
					if (230 <= usbOtaMessageAck.Cmd && usbOtaMessageAck.Cmd <= 232 && usbOtaMessageAck.Status == 0)
					{
						if (readFirmwareState && usbOtaMessageAck.Len == usbOtaMessageAck.Data.Length)
						{
							FirmwareReadCache = usbOtaMessageAck.Data;
						}
						else
						{
							FirmwareReadCache = null;
						}
						GlassesMsgSemaphore.ReleaseSemaphore(usbOtaMessageAck.Cmd);
					}
					continue;
				}
				UsbMessage usbMessage = UsbMessage.FromBytes(array);
				if (usbMessage.Data.Header == UsbMessageData.HEADER_TYPE.DOWN || usbMessage.Data.Header == UsbMessageData.HEADER_TYPE.UP)
				{
					if (!isOta && IsP6sOtaMsg(usbMessage.Data.MsgID))
					{
						GlassesMsgSemaphore.ReleaseSemaphore(usbMessage.Data.MsgID);
					}
					ParseUsbMessage(usbMessage);
					this.ReceivedUsbData?.Invoke(array);
				}
			}
			catch (Exception ex)
			{
				Logger.Warning(ex.Message);
			}
		}
		object arg = _disposing;
		UsbDevice? device2 = _device;
		string arg2 = ((device2 != null) ? device2.DevicePath : null);
		UsbDevice? device3 = _device;
		Logger.Info($"_disposing: {arg} _device {arg2} IsOpen: {((device3 != null) ? new bool?(device3.IsOpen) : null)}");
	}

	private void ParseUsbMessage(UsbMessage usbMsg)
	{
		if (usbMsg.Data.MsgID.Equal(DeviceMsgId.MSG_R_MCU_APP_FW_VERSION))
		{
			string version = usbMsg.GetVersion();
			Logger.Info("Version: " + version);
			GlassesDeviceManager.Instance.FirmwareVersion = version;
		}
		if (usbMsg.Data.MsgID.Equal(DeviceMsgId.MSG_R_GLASSID))
		{
			string glassesSN = usbMsg.GetGlassesSN();
			Logger.Info("GlassesSN: " + glassesSN);
			GlassesDeviceManager.Instance.GlassesSN = glassesSN;
		}
		if (usbMsg.Data.MsgID.Equal(DeviceMsgId.MSG_R_PACKAGEID))
		{
			string pSN = usbMsg.GetPSN();
			Logger.Info("PackageSN: " + pSN);
			GlassesDeviceManager.Instance.PackageSN = pSN;
		}
		if (usbMsg.Data.MsgID.Equal(DeviceMsgId.MSG_W_DISPLAY_2D_3D))
		{
			GlassesDeviceManager.Instance.RefreshRate = ParseRefreshRate(usbMsg);
		}
		if (usbMsg.Data.MsgID == 5 || usbMsg.Data.MsgID == 6 || usbMsg.Data.MsgID == 769)
		{
			bool flag = usbMsg.Data.MsgID == 769;
			if (flag || usbMsg.Data.DataLength >= 2)
			{
				byte b = (flag ? usbMsg.Data.Payload[0] : usbMsg.Data.Payload[1]);
				Logger.Info($"brightnessLevel: {b}");
				GlassesDeviceManager.Instance.BrightnessLevel = b;
			}
			GlassesMsgSemaphore.ReleaseSemaphore(5);
		}
		if (usbMsg.Data.MsgID == 50 || usbMsg.Data.MsgID == 51 || usbMsg.Data.MsgID == 772)
		{
			bool flag2 = usbMsg.Data.MsgID == 772;
			if (flag2 || usbMsg.Data.DataLength >= 2)
			{
				byte b2 = (flag2 ? usbMsg.Data.Payload[0] : usbMsg.Data.Payload[1]);
				Logger.Info($"audioLevel: {b2}");
				GlassesDeviceManager.Instance.VolumeLevel = b2;
			}
			GlassesMsgSemaphore.ReleaseSemaphore(50);
		}
	}

	private int ParseRefreshRate(UsbMessage hidMessage)
	{
		byte b = hidMessage.Data.Payload[1];
		int num = 60;
		switch (b)
		{
		case 65:
			num = 60;
			break;
		case 67:
			num = 90;
			break;
		case 68:
			num = 120;
			break;
		}
		Logger.Info($"Refresh rate: {num} {b}");
		return num;
	}

	internal void SendMsg(UsbMessage usbMsg)
	{
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		if (bootMode)
		{
			return;
		}
		lock (lock_obj)
		{
			UsbDevice? device = _device;
			if (device == null || !device.IsOpen || _writer == null)
			{
				return;
			}
			int i = 0;
			byte[] array = usbMsg.ToBytes();
			ErrorCode val = (ErrorCode)0;
			try
			{
				int num = default(int);
				for (; i < array.Length; i += num)
				{
					if ((int)val != 0)
					{
						break;
					}
					val = _writer.Write(array, i, array.Length - i, 500, ref num);
				}
				if ((int)val != 0)
				{
					Logger.Error($"Write USB bytes Err: {val}");
					HealAfterWrite(val, _writer);
				}
				else
				{
					HealAfterWrite(val, _writer);
					Logger.Info($"Write USB Bytes: {BitConverter.ToString(array)} ret: {val}");
				}
			}
			catch (Exception ex)
			{
				Logger.Error(ex.Message, ex.Message);
			}
		}
	}

	internal void SendMsg(UsbOtaMessage otaMsg)
	{
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		lock (lock_obj)
		{
			UsbDevice? device = _device;
			if (device == null || !device.IsOpen || _ota_writer == null)
			{
				return;
			}
			readFirmwareState = otaMsg.Cmd == 232;
			if (readFirmwareState)
			{
				readBufferLen = GetUInt16FromBytes(otaMsg.Data, 4, isLittleEndian: false) + 6;
			}
			int i = 0;
			byte[] array = otaMsg.ToBytes();
			ErrorCode val = (ErrorCode)0;
			try
			{
				int num = default(int);
				for (; i < array.Length; i += num)
				{
					if ((int)val != 0)
					{
						break;
					}
					val = _ota_writer.Write(array, i, array.Length - i, 500, ref num);
				}
				if ((int)val != 0)
				{
					Logger.Error($"Write USB OTA bytes Err: {val}");
					HealAfterWrite(val, _ota_writer);
				}
				else
				{
					HealAfterWrite(val, _ota_writer);
					otaWriteSlim.Set();
					Logger.Info($"Write OTA Bytes: {BitConverter.ToString(array)} ret: {val}");
				}
			}
			catch (Exception ex)
			{
				Logger.Error(ex.Message, ex.Message);
			}
		}
	}

	private void HealAfterWrite(ErrorCode ret, UsbEndpointWriter? writer)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		if ((int)ret == 0)
		{
			_writeErrorStreak = 0;
			return;
		}
		_writeErrorStreak++;
		if (_writeErrorStreak % 3 != 0)
		{
			return;
		}
		try
		{
			bool flag = writer != null && ((UsbEndpointBase)writer).Reset();
			Logger.Warning($"[USB-HEAL] endpoint Reset writer={flag} streak={_writeErrorStreak} lastErr={ret}");
		}
		catch (Exception ex)
		{
			Logger.Error("[USB-HEAL] endpoint Reset threw: " + ex.Message, ex.StackTrace);
		}
	}

	public void Dispose()
	{
		if (_disposing)
		{
			Logger.Info("VitureUsbDevice has Dispose");
			return;
		}
		Logger.Info("VitureUsbDevice Dispose begin");
		_disposing = true;
		otaWriteSlim.Set();
		lock (lock_obj)
		{
			if (_recvThreads != null)
			{
				foreach (Thread recvThread in _recvThreads)
				{
					recvThread?.Join(3000);
				}
				_recvThreads.Clear();
				_recvThreads = null;
			}
			if (!bootMode)
			{
				UsbEndpointWriter? writer = _writer;
				if (writer != null)
				{
					((UsbEndpointBase)writer).Dispose();
				}
				UsbEndpointReader? reader = _reader;
				if (reader != null)
				{
					((UsbEndpointBase)reader).Dispose();
				}
				_writer = null;
				_reader = null;
			}
			UsbEndpointWriter? ota_writer = _ota_writer;
			if (ota_writer != null)
			{
				((UsbEndpointBase)ota_writer).Dispose();
			}
			UsbEndpointReader? ota_reader = _ota_reader;
			if (ota_reader != null)
			{
				((UsbEndpointBase)ota_reader).Dispose();
			}
			_ota_writer = null;
			_ota_reader = null;
			UsbDevice? device = _device;
			if (device != null && device.IsOpen)
			{
				UsbDevice? device2 = _device;
				if (device2 != null)
				{
					device2.Close();
				}
				_device = null;
			}
		}
		Logger.Info("VitureUsbDevice Dispose success");
	}

	private static bool IsP6sOtaMsg(ushort msgId)
	{
		if (4098 <= msgId)
		{
			return msgId <= 4103;
		}
		return false;
	}

	private ushort GetUInt16FromBytes(byte[] data, int offset, bool isLittleEndian = true)
	{
		if (data == null)
		{
			throw new ArgumentNullException("data");
		}
		if (offset < 0 || offset + 2 > data.Length)
		{
			throw new ArgumentOutOfRangeException("offset");
		}
		if (isLittleEndian)
		{
			return (ushort)(data[offset] | (data[offset + 1] << 8));
		}
		return (ushort)((data[offset] << 8) | data[offset + 1]);
	}
}
