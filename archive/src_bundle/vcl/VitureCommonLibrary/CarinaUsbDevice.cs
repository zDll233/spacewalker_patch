using System;
using System.Collections.Generic;
using System.Threading;

namespace VitureCommonLibrary;

public class CarinaUsbDevice
{
	public const int BUFFER_LEN = 512;

	private static object lock_obj = new object();

	private volatile bool _disposing;

	private List<Thread>? _recvThreads;

	private static readonly Lazy<CarinaUsbDevice> instance = new Lazy<CarinaUsbDevice>(() => new CarinaUsbDevice());

	internal static CarinaUsbDevice Instance => instance.Value;

	internal event Action<byte[]>? ReceivedUsbData;

	private CarinaUsbDevice()
	{
		GlassesMonitorHelper.Start();
	}

	internal void Initialize(string config_file = "Assets/Configs/custom_config.yaml")
	{
		if (_recvThreads != null)
		{
			Dispose();
		}
		lock (lock_obj)
		{
			if (!CarinaNative.Init(config_file))
			{
				return;
			}
			_disposing = false;
			if (_recvThreads == null)
			{
				_recvThreads = new List<Thread>();
			}
			Thread thread = new Thread((ThreadStart)delegate
			{
				ReadUsbData();
			});
			thread.Priority = ThreadPriority.Highest;
			_recvThreads.Add(thread);
			thread.Start();
		}
		Logger.Info("CarinaUsbDevice Initialize success");
	}

	private void ReadUsbData(bool isOTA = false)
	{
		while (!_disposing)
		{
			try
			{
				byte[] array = CarinaNative.ReadData();
				if (array == null || (isOTA && array.Length != 6) || (!isOTA && array.Length != 512))
				{
					continue;
				}
				if (isOTA && array[0] == 250 && array[1] == 85)
				{
					UsbOtaMessageAck usbOtaMessageAck = UsbOtaMessageAck.FromBytes(array);
					if (usbOtaMessageAck.Cmd == 230 || (usbOtaMessageAck.Cmd == 231 && usbOtaMessageAck.Status == 0))
					{
						GlassesMsgSemaphore.ReleaseSemaphore(usbOtaMessageAck.Cmd);
					}
				}
				else if (!isOTA && (array[1] == 253 || array[1] == 254) && array[0] == byte.MaxValue)
				{
					UsbMessage usbMsg = UsbMessage.FromBytes(array);
					ParseUsbMessage(usbMsg);
					this.ReceivedUsbData?.Invoke(array);
				}
			}
			catch (Exception ex)
			{
				Logger.Warning(ex.Message);
			}
		}
		Logger.Info($"_disposing: {_disposing}");
		if (!_disposing)
		{
			Dispose();
		}
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
			byte b = ((usbMsg.Data.MsgID == 769) ? usbMsg.Data.Payload[0] : usbMsg.Data.Payload[1]);
			Logger.Info($"brightnessLevel: {b}");
			GlassesDeviceManager.Instance.BrightnessLevel = b;
			GlassesMsgSemaphore.ReleaseSemaphore(5);
		}
		if (usbMsg.Data.MsgID == 50 || usbMsg.Data.MsgID == 51 || usbMsg.Data.MsgID == 772)
		{
			byte b2 = ((usbMsg.Data.MsgID == 772) ? usbMsg.Data.Payload[0] : usbMsg.Data.Payload[1]);
			Logger.Info($"audioLevel: {b2}");
			GlassesDeviceManager.Instance.VolumeLevel = b2;
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
		lock (lock_obj)
		{
			if (!_disposing)
			{
				try
				{
					CarinaNative.WriteData(usbMsg.ToBytes());
					return;
				}
				catch (Exception ex)
				{
					Logger.Warning(ex.Message);
					return;
				}
			}
		}
	}

	internal void SendMsg(UsbOtaMessage otaMsg)
	{
		lock (lock_obj)
		{
			if (!_disposing)
			{
				try
				{
					CarinaNative.WriteData(otaMsg.ToBytes());
					return;
				}
				catch (Exception ex)
				{
					Logger.Warning(ex.Message);
					return;
				}
			}
		}
	}

	public void Dispose()
	{
		if (_disposing)
		{
			Logger.Info("CarinaUsbDevice has Dispose");
			return;
		}
		lock (lock_obj)
		{
			Logger.Info("CarinaUsbDevice Dispose begin");
			_disposing = true;
			if (_recvThreads != null)
			{
				_recvThreads.Clear();
				_recvThreads = null;
			}
			CarinaNative.Release();
		}
		Logger.Info("CarinaUsbDevice Dispose success");
	}
}
