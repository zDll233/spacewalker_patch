using System;
using System.Collections.Generic;
using System.Threading;

namespace VitureCommonLibrary;

public class VitureHidDevice : IDisposable
{
	private List<LibUsbHidDevice> _devices = new List<LibUsbHidDevice>();

	private volatile bool _disposing;

	private List<Thread>? _recvThreads;

	private static object _lockObj = new object();

	private static readonly Lazy<VitureHidDevice> lazy = new Lazy<VitureHidDevice>(() => new VitureHidDevice());

	internal bool UsbCommunicationFailed { get; private set; }

	internal static VitureHidDevice Instance => lazy.Value;

	internal event Action<byte[]>? ReceivedHidData;

	public event Action<R6NewerHidMessage>? S6ImuFrameReceived;

	private VitureHidDevice()
	{
	}

	~VitureHidDevice()
	{
	}

	internal void Initialize()
	{
		if (_recvThreads != null)
		{
			Dispose();
		}
		lock (_lockObj)
		{
			_recvThreads = new List<Thread>();
			_devices = LibUsbHidHelper.EnumerateDevices(13770);
			_disposing = false;
			UsbCommunicationFailed = false;
			bool flag = false;
			int num = 0;
			foreach (LibUsbHidDevice _device in _devices)
			{
				if (_devices.Count > 2 && _device.ProductId != 4609 && _device.ProductId != 4608 && _device.ProductId != 4625 && _device.ProductId != 4624 && _device.ProductId != 4865 && _device.ProductId != 4864 && _device.ProductId != 4881 && _device.ProductId != 4880)
				{
					continue;
				}
				_device.OpenDevice();
				Logger.Info($"[HID-INIT] OpenDevice: {_device.DiagTag} IsOpen={_device.IsOpen} lastOpenErr={_device.LastOpenError} ts={DateTime.UtcNow:HH:mm:ss.fff}");
				num++;
				if (_device.IsOpen)
				{
					flag = true;
				}
				GlassesDeviceManager.Instance.ProductId = _device.ProductId;
				Thread thread = new Thread((ThreadStart)delegate
				{
					while (!_disposing && _device.IsOpen)
					{
						try
						{
							byte[] array = _device.Read();
							if (array != null)
							{
								if (array.Length == 64 && array[0] == 16)
								{
									R6NewerHidMessage r6NewerHidMessage = R6NewerHidMessage.FromBytes(array);
									if (r6NewerHidMessage.CRC == r6NewerHidMessage.GetCrc())
									{
										Logger.Debug($"[HID-RX] {_device.DiagTag} MsgID=0x{r6NewerHidMessage.MsgID:X4} SeqNum={r6NewerHidMessage.SeqNum} DataLen={r6NewerHidMessage.DataLen} CRC=0x{r6NewerHidMessage.CRC:X4} Ack={r6NewerHidMessage.Payload[0]:X2} ts={DateTime.UtcNow:HH:mm:ss.fff} hex={BitConverter.ToString(array)}");
										ParseR6Message(r6NewerHidMessage);
										this.ReceivedHidData?.Invoke(array);
									}
								}
								else if (array.Length == 64 && array[0] == byte.MaxValue)
								{
									HidMessage hidMessage = HidMessage.FromBytes(array);
									if (hidMessage.Data.Header1 == byte.MaxValue)
									{
										ParseHidMessage(hidMessage);
										this.ReceivedHidData?.Invoke(array);
									}
								}
							}
						}
						catch (Exception ex)
						{
							Logger.Error(ex.Message, ex.StackTrace);
						}
					}
					Logger.Info($"_disposing: {_disposing} _device {_device.DevicePath} IsOpen: {_device.IsOpen}");
				});
				thread.IsBackground = true;
				thread.Priority = ThreadPriority.Highest;
				_recvThreads.Add(thread);
				thread.Start();
			}
			UsbCommunicationFailed = num > 0 && !flag;
			if (UsbCommunicationFailed)
			{
				Logger.Warning("[HID-INIT] enumerated but no interface opened a usable channel (open/claim failed) → USB communication failure");
			}
		}
		Logger.Info("VitureHidDevice Initialize success");
	}

	private void ParseR6Message(R6NewerHidMessage r6Msg)
	{
		if ((ushort)(r6Msg.MsgID & 0xF000) == 40960)
		{
			R6NewerLongPacketReader.Instance.OnLongPacketResponse(r6Msg);
			return;
		}
		if (r6Msg.MsgID.Equal(R6NewerMsgId.TF_EVENT_IMU_MAG_REPORT))
		{
			try
			{
				this.S6ImuFrameReceived?.Invoke(r6Msg);
			}
			catch (Exception ex)
			{
				Logger.Warning("S6ImuFrameReceived subscriber threw: " + ex.Message);
			}
		}
		if (r6Msg.MsgID.Equal(R6NewerMsgId.TF_REQ_ID_APP_FW_VERSION_R))
		{
			string version = r6Msg.GetVersion();
			Logger.Info("Version: " + version);
			GlassesDeviceManager.Instance.FirmwareVersion = version;
		}
		if (r6Msg.MsgID.Equal(R6NewerMsgId.TF_REQ_ID_BOARD_SN_R))
		{
			string glassesSN = r6Msg.GetGlassesSN();
			Logger.Info("GlassesSN: " + glassesSN);
			GlassesDeviceManager.Instance.GlassesSN = glassesSN;
		}
		if (r6Msg.MsgID.Equal(R6NewerMsgId.TF_REQ_DISPLAY_SCREEN_SIZE_R) && (r6Msg.MsgID & 0xF000) == 20480)
		{
			if (r6Msg.GetAckSuceess() && r6Msg.DataLen >= 2)
			{
				GlassesDeviceManager.Instance.Native3DofScreenSize = r6Msg.Payload[1];
				Logger.Info($"R6ScreenSize: {r6Msg.Payload[1]}");
			}
			GlassesMsgSemaphore.ReleaseSemaphore(r6Msg.MsgID);
		}
		if (r6Msg.MsgID.Equal(R6NewerMsgId.TF_REQ_DISPLAY_BRIGHTNESS_R) && (r6Msg.MsgID & 0xF000) == 20480)
		{
			if (r6Msg.GetAckSuceess() && r6Msg.DataLen >= 2)
			{
				GlassesDeviceManager.Instance.BrightnessLevel = r6Msg.Payload[1];
				Logger.Info($"BrightnessLevel: {r6Msg.Payload[1]}");
			}
			GlassesMsgSemaphore.ReleaseSemaphore(r6Msg.MsgID);
		}
		if (r6Msg.MsgID.Equal(R6NewerMsgId.TF_EVENT_DISPLAY_BRIGHTNESS) && (r6Msg.MsgID & 0xF000) == 28672 && r6Msg.DataLen >= 1)
		{
			GlassesDeviceManager.Instance.BrightnessLevel = r6Msg.Payload[0];
			Logger.Info($"BrightnessLevel event: {r6Msg.Payload[0]}");
		}
		if (r6Msg.MsgID.Equal(R6NewerMsgId.TF_REQ_AUDIO_VOLUME_R) && (r6Msg.MsgID & 0xF000) == 20480)
		{
			if (r6Msg.GetAckSuceess() && r6Msg.DataLen >= 2)
			{
				GlassesDeviceManager.Instance.VolumeLevel = r6Msg.Payload[1];
				Logger.Info($"VolumeLevel: {r6Msg.Payload[1]}");
			}
			GlassesMsgSemaphore.ReleaseSemaphore(r6Msg.MsgID);
		}
		if (r6Msg.MsgID.Equal(R6NewerMsgId.TF_EVENT_AUDIO_VOLUME) && (r6Msg.MsgID & 0xF000) == 28672 && r6Msg.DataLen >= 1)
		{
			GlassesDeviceManager.Instance.VolumeLevel = r6Msg.Payload[0];
			Logger.Info($"VolumeLevel event: {r6Msg.Payload[0]}");
		}
		if (r6Msg.MsgID.Equal(R6NewerMsgId.TF_REQ_DISPLAY_DISTANCE) && (r6Msg.MsgID & 0xF000) == 20480)
		{
			if (r6Msg.GetAckSuceess() && r6Msg.DataLen >= 2)
			{
				GlassesDeviceManager.Instance.DistanceLevel = r6Msg.Payload[1];
				Logger.Info($"DistanceLevel: {r6Msg.Payload[1]}");
			}
			GlassesMsgSemaphore.ReleaseSemaphore(r6Msg.MsgID);
		}
		if (r6Msg.MsgID.Equal(R6NewerMsgId.TF_EVENT_DISPLAY_DISTANCE) && (r6Msg.MsgID & 0xF000) == 28672 && r6Msg.DataLen >= 1)
		{
			GlassesDeviceManager.Instance.DistanceLevel = r6Msg.Payload[0];
			Logger.Info($"DistanceLevel event: {r6Msg.Payload[0]}");
		}
		if (r6Msg.MsgID.Equal(R6NewerMsgId.TF_EVENT_DISPLAY_SCREEN_SIZE) && (r6Msg.MsgID & 0xF000) == 28672 && r6Msg.DataLen >= 1)
		{
			GlassesDeviceManager.Instance.Native3DofScreenSize = r6Msg.Payload[0];
			Logger.Info($"R6ScreenSize event: {r6Msg.Payload[0]}");
		}
		if (r6Msg.MsgID.Equal(R6NewerMsgId.TF_CMD_IMU_REPORT_FREQ_W))
		{
			Logger.Info($"R6 IMU Report Cmd ACK: {r6Msg.GetAckSuceess()} payload[0..2]=" + $"{r6Msg.Payload[0]:X2},{(byte)((r6Msg.DataLen >= 2) ? r6Msg.Payload[1] : 0):X2}");
			GlassesMsgSemaphore.ReleaseSemaphore(r6Msg.MsgID);
		}
	}

	private void ParseHidMessage(HidMessage hidMsg)
	{
		if (hidMsg.Data.MsgID.Equal(DeviceMsgId.MSG_R_MCU_APP_FW_VERSION))
		{
			string version = hidMsg.GetVersion();
			Logger.Info("Version: " + version);
			GlassesDeviceManager.Instance.FirmwareVersion = version;
		}
		if (hidMsg.Data.MsgID.Equal(DeviceMsgId.MSG_R_GLASSID))
		{
			string glassesSN = hidMsg.GetGlassesSN();
			Logger.Info("GlassesSN: " + glassesSN);
			GlassesDeviceManager.Instance.GlassesSN = glassesSN;
		}
		if (hidMsg.Data.MsgID.Equal(DeviceMsgId.MSG_R_PACKAGEID))
		{
			string pSN = hidMsg.GetPSN();
			Logger.Info("PackageSN: " + pSN);
			GlassesDeviceManager.Instance.PackageSN = pSN;
		}
		if (hidMsg.Data.MsgID.Equal(DeviceMsgId.MSG_W_CONTROL_IMUOPEN))
		{
			Logger.Info($"OpenIMU ACK: {hidMsg.GetAckSuceess()}");
		}
		if (hidMsg.Data.MsgID.Equal(DeviceMsgId.MSG_W_CONTROL_IMU_REPORT_FQ))
		{
			GlassesDeviceManager.Instance.ImuFrameRate = ParseImuFrameRate(hidMsg);
			GlassesMsgSemaphore.ReleaseSemaphore(hidMsg.Data.MsgID);
		}
		if (hidMsg.Data.MsgID.Equal(DeviceMsgId.MSG_W_DISPLAY_2D_3D))
		{
			GlassesDeviceManager.Instance.RefreshRate = ParseRefreshRate(hidMsg);
		}
		if (hidMsg.Data.MsgID == 5 || hidMsg.Data.MsgID == 6 || hidMsg.Data.MsgID == 769)
		{
			bool flag = hidMsg.Data.MsgID == 769;
			if (flag || hidMsg.Data.DataLength >= 2)
			{
				byte b = (flag ? hidMsg.Data.Payload[0] : hidMsg.Data.Payload[1]);
				Logger.Info($"brightnessLevel: {b}");
				GlassesDeviceManager.Instance.BrightnessLevel = b;
			}
			GlassesMsgSemaphore.ReleaseSemaphore(5);
		}
		if (hidMsg.Data.MsgID == 50 || hidMsg.Data.MsgID == 51 || hidMsg.Data.MsgID == 772)
		{
			bool flag2 = hidMsg.Data.MsgID == 772;
			if (flag2 || hidMsg.Data.DataLength >= 2)
			{
				byte b2 = (flag2 ? hidMsg.Data.Payload[0] : hidMsg.Data.Payload[1]);
				Logger.Info($"audioLevel: {b2}");
				GlassesDeviceManager.Instance.VolumeLevel = b2;
			}
			GlassesMsgSemaphore.ReleaseSemaphore(50);
		}
	}

	private int ParseRefreshRate<MsgT>(MsgT msg)
	{
		byte b = 0;
		if (msg is HidMessage hidMessage)
		{
			b = hidMessage.Data.Payload[1];
		}
		else if (msg is R6NewerHidMessage r6NewerHidMessage)
		{
			b = r6NewerHidMessage.Payload[1];
		}
		int num = 60;
		switch (b)
		{
		case 49:
			num = 60;
			break;
		case 51:
			num = 90;
			break;
		case 52:
			num = 120;
			break;
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

	private int ParseImuFrameRate<MsgT>(MsgT msg)
	{
		byte b = 0;
		if (msg is HidMessage hidMessage)
		{
			b = hidMessage.Data.Payload[1];
		}
		else if (msg is R6NewerHidMessage r6NewerHidMessage)
		{
			b = r6NewerHidMessage.Payload[1];
		}
		int num = 60;
		num = b switch
		{
			1 => 90, 
			2 => 120, 
			3 => 240, 
			4 => 500, 
			_ => 60, 
		};
		Logger.Info($"IMU FrameRate: {num} {b}");
		return num;
	}

	internal void SendMsg(HidMessage hidMsg)
	{
		if (GlassesDeviceManager.Instance.Muted || !GlassesDeviceManager.Instance.UseHidDevice || GlassesDeviceManager.Instance.R6NewerModel)
		{
			return;
		}
		lock (_lockObj)
		{
			foreach (LibUsbHidDevice device in _devices)
			{
				if (device.IsOpen)
				{
					byte[] array = hidMsg.ToBytes();
					bool flag = device.Write(array);
					Logger.Info($"WriteReport: {BitConverter.ToString(array)} ret: {flag}");
				}
			}
		}
	}

	internal void SendMsg(R6NewerHidMessage r6Msg)
	{
		if (GlassesDeviceManager.Instance.Muted || !GlassesDeviceManager.Instance.UseHidDevice || !GlassesDeviceManager.Instance.R6NewerModel)
		{
			return;
		}
		lock (_lockObj)
		{
			byte[] array = r6Msg.ToBytes();
			Logger.Info($"[HID-SEND] R6Newer MsgID=0x{r6Msg.MsgID:X4} SeqNum={r6Msg.SeqNum} DataLen={r6Msg.DataLen} CRC=0x{r6Msg.CRC:X4} ts={DateTime.UtcNow:HH:mm:ss.fff} hex={BitConverter.ToString(array)}");
			foreach (LibUsbHidDevice device in _devices)
			{
				if ((_devices.Count != 3 || GlassesDeviceManager.Instance.IsNewerThanR6Model(device.ProductId)) && device.IsOpen)
				{
					bool flag = device.Write(array);
					Logger.Info($"[HID-SEND] → device {device.DiagTag} write ret={flag}");
				}
			}
		}
	}

	public void Dispose()
	{
		List<Thread> recvThreads;
		List<LibUsbHidDevice> devices;
		lock (_lockObj)
		{
			if (_disposing)
			{
				Logger.Info("VitureHidDevice has Dispose");
				return;
			}
			Logger.Info("VitureHidDevice Dispose begin");
			_disposing = true;
			recvThreads = _recvThreads;
			_recvThreads = null;
			devices = _devices;
			_devices = new List<LibUsbHidDevice>();
		}
		if (recvThreads != null)
		{
			foreach (Thread item in recvThreads)
			{
				try
				{
					if (item.IsAlive)
					{
						item.Join(1000);
					}
				}
				catch (Exception ex)
				{
					Logger.Warning("VitureHidDevice Dispose join thread threw: " + ex.Message);
				}
			}
		}
		foreach (LibUsbHidDevice item2 in devices)
		{
			item2.Dispose();
		}
		Logger.Info("VitureHidDevice Dispose success");
	}
}
