using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace VitureCommonLibrary;

public class GlassesDeviceManager : INotifyPropertyChanged
{
	public const int VITURE_VID = 13770;

	public const int N6 = 4113;

	public const int N6PS = 4121;

	public const int N6PL = 4125;

	public const int P6 = 4385;

	public const int P6C = 4401;

	public const int P6D = 4417;

	public const int P6X = 4433;

	public const int P6S_MIC = 4354;

	public const int R6_APP = 4609;

	public const int R6_BOOT = 4608;

	public const int R6D_APP = 4625;

	public const int R6D_BOOT = 4624;

	public const int S6_BOOT = 4864;

	public const int S6_APP = 4865;

	public const int S6P_BOOT = 4880;

	public const int S6P_APP = 4881;

	public const int P6S_BOOT = 4352;

	public const int P6S_APP = 4353;

	public const int P6SP_BOOT = 4355;

	public const int P6SP_APP = 4356;

	private ManualResetEventSlim msgWaiting = new ManualResetEventSlim(initialState: true);

	private static readonly Lazy<GlassesDeviceManager> instance = new Lazy<GlassesDeviceManager>(() => new GlassesDeviceManager());

	public Action<bool>? DeviceEnterBootMode;

	public Action<bool>? DeviceConnectChanged;

	private volatile bool _muted;

	private static bool? runinUnity = null;

	private static bool? _isUnityEditor;

	private int _native3DofScreenSize;

	private int brightnessLevel;

	private int volumeLevel;

	private int distanceLevel;

	public const int MinDistance = 0;

	public const int MaxDistance = 9;

	public const int MaxBrightness = 8;

	public const int MinBrightness = 0;

	public const int MaxVolumeR6 = 15;

	public const int MaxVolumeLegacy = 8;

	public const int MinVolume = 0;

	public static GlassesDeviceManager Instance => instance.Value;

	public bool UsbCommunicationFailed
	{
		get
		{
			if (UseHidDevice)
			{
				return VitureHidDevice.Instance.UsbCommunicationFailed;
			}
			return false;
		}
	}

	public int ProductId { get; internal set; }

	public string FirmwareVersion { get; internal set; } = string.Empty;


	public string GlassesSN { get; internal set; } = string.Empty;


	public string PackageSN { get; internal set; } = string.Empty;


	public bool IsConnected { get; internal set; }

	public bool Muted
	{
		get
		{
			return _muted;
		}
		set
		{
			_muted = value;
		}
	}

	public bool UseHidDevice { get; internal set; } = true;


	public bool IsN6
	{
		get
		{
			if (UseHidDevice)
			{
				return ProductId == 4113;
			}
			return false;
		}
	}

	public bool IsP6S
	{
		get
		{
			if (UseHidDevice)
			{
				if (ProductId != 4353)
				{
					return ProductId == 4356;
				}
				return true;
			}
			return false;
		}
	}

	public bool P6Series
	{
		get
		{
			if (UseHidDevice)
			{
				if (UseHidDevice)
				{
					if (ProductId != 4385 && ProductId != 4401 && ProductId != 4417)
					{
						return ProductId == 4433;
					}
					return true;
				}
				return false;
			}
			return true;
		}
	}

	private bool R6Series
	{
		get
		{
			if (UseHidDevice)
			{
				if (ProductId != 4609 && ProductId != 4608 && ProductId != 4625)
				{
					return ProductId == 4624;
				}
				return true;
			}
			return false;
		}
	}

	public bool S6Series
	{
		get
		{
			if (UseHidDevice)
			{
				if (ProductId != 4865 && ProductId != 4864 && ProductId != 4881)
				{
					return ProductId == 4880;
				}
				return true;
			}
			return false;
		}
	}

	public bool Support1200P => DisplaySettingExtensions.VitureDisplaySupports1200P().GetValueOrDefault();

	public bool SupportSplitProtocol
	{
		get
		{
			if (!S6Series)
			{
				if (R6Series && !string.IsNullOrEmpty(FirmwareVersion) && FirmwareVersion.Length >= 8)
				{
					return string.Compare(FirmwareVersion.Substring(FirmwareVersion.Length - 8, 8), "20260427") >= 0;
				}
				return false;
			}
			return true;
		}
	}

	public bool S6NeedsHostSlam
	{
		get
		{
			if (UseHidDevice)
			{
				return ProductId == 4865;
			}
			return false;
		}
	}

	public bool SupportNative3Dof
	{
		get
		{
			if (UseHidDevice)
			{
				if (ProductId != 4609 && ProductId != 4625)
				{
					return ProductId == 4881;
				}
				return true;
			}
			return false;
		}
	}

	public bool R6NewerModel => IsNewerThanR6Model(ProductId);

	public static bool IsRunInUnity => IsRunningInUnity();

	public static bool IsRunInUnityEditor
	{
		get
		{
			bool valueOrDefault = _isUnityEditor.GetValueOrDefault();
			if (!_isUnityEditor.HasValue)
			{
				valueOrDefault = AppDomain.CurrentDomain.GetAssemblies().Any((Assembly a) => a.FullName.StartsWith("UnityEditor,"));
				_isUnityEditor = valueOrDefault;
			}
			return _isUnityEditor.Value;
		}
	}

	public int Native3DofScreenSize
	{
		get
		{
			return _native3DofScreenSize;
		}
		set
		{
			_native3DofScreenSize = value;
			this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Native3DofScreenSize"));
		}
	}

	public int BrightnessLevel
	{
		get
		{
			return brightnessLevel;
		}
		set
		{
			brightnessLevel = value;
			this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("BrightnessLevel"));
		}
	}

	public int VolumeLevel
	{
		get
		{
			return volumeLevel;
		}
		set
		{
			volumeLevel = value;
			this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("VolumeLevel"));
		}
	}

	public int DistanceLevel
	{
		get
		{
			return distanceLevel;
		}
		set
		{
			distanceLevel = value;
			this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DistanceLevel"));
		}
	}

	public int ImuFrameRate { get; internal set; }

	public int RefreshRate { get; internal set; }

	public bool AppMode { get; internal set; }

	public event Action<byte[]>? ReceivedGlassesData;

	public event Action<R6NewerHidMessage>? S6ImuFrameReceived;

	public event PropertyChangedEventHandler? PropertyChanged;

	private GlassesDeviceManager()
	{
		VitureHidDevice.Instance.ReceivedHidData += Instance_ReceivedHidData;
		VitureHidDevice.Instance.S6ImuFrameReceived += Instance_S6ImuFrameReceived;
		if (IsRunInUnity)
		{
			CarinaUsbDevice.Instance.ReceivedUsbData += Instance_ReceivedUsbData;
		}
		else
		{
			VitureUsbDevice.Instance.ReceivedUsbData += Instance_ReceivedUsbData;
		}
	}

	~GlassesDeviceManager()
	{
		VitureHidDevice.Instance.ReceivedHidData -= Instance_ReceivedHidData;
		VitureHidDevice.Instance.S6ImuFrameReceived -= Instance_S6ImuFrameReceived;
		if (IsRunInUnity)
		{
			CarinaUsbDevice.Instance.ReceivedUsbData -= Instance_ReceivedUsbData;
		}
		else
		{
			VitureUsbDevice.Instance.ReceivedUsbData -= Instance_ReceivedUsbData;
		}
	}

	internal bool IsNewerThanR6Model(int pid)
	{
		if (UseHidDevice)
		{
			if (pid != 4609 && pid != 4608 && pid != 4625 && pid != 4624 && pid != 4865 && pid != 4864 && pid != 4881)
			{
				return pid == 4880;
			}
			return true;
		}
		return false;
	}

	private static bool IsRunningInUnity()
	{
		if (!runinUnity.HasValue)
		{
			runinUnity = AppDomain.CurrentDomain.GetAssemblies().Any((Assembly assembly) => assembly.FullName.StartsWith("UnityEngine,"));
		}
		return runinUnity.Value;
	}

	public void Initialize(string config_file = "Assets/Configs/custom_config.yaml")
	{
		if (UseHidDevice)
		{
			VitureHidDevice.Instance.Initialize();
		}
		else
		{
			if (IsRunInUnity)
			{
				CarinaUsbDevice.Instance.Initialize(config_file);
			}
			else
			{
				VitureUsbDevice.Instance.Initialize();
			}
			Thread.Sleep(500);
			UsbMessage usbMessage = new UsbMessage();
			usbMessage.MsgId = 4107;
			usbMessage.Data.PutValue((byte)1);
			SendMsg(usbMessage);
		}
		Thread.Sleep(500);
	}

	public void SendMsg<T>(T msg) where T : class
	{
		if (Muted)
		{
			Logger.Info("SendMsg: skipped — HID channel muted (LimitedMode / disconnected)");
			return;
		}
		if (!msgWaiting.Wait(2000))
		{
			Logger.Warning("SendMsg: msgWaiting timed out after 2s, forcing reset to prevent deadlock");
			msgWaiting.Reset();
		}
		else
		{
			msgWaiting.Reset();
		}
		try
		{
			if (UseHidDevice && msg is HidMessage hidMsg)
			{
				VitureHidDevice.Instance.SendMsg(hidMsg);
			}
			else if (UseHidDevice && msg is R6NewerHidMessage r6Msg)
			{
				VitureHidDevice.Instance.SendMsg(r6Msg);
			}
			else if (!UseHidDevice && msg is UsbMessage usbMsg)
			{
				if (IsRunInUnity)
				{
					CarinaUsbDevice.Instance.SendMsg(usbMsg);
				}
				else
				{
					VitureUsbDevice.Instance.SendMsg(usbMsg);
				}
			}
			else if (!UseHidDevice && msg is UsbOtaMessage otaMsg)
			{
				VitureUsbDevice.Instance.SendMsg(otaMsg);
			}
			else
			{
				Logger.Warning("msg param type error");
			}
		}
		finally
		{
			msgWaiting.Set();
		}
	}

	public int GetScreenSize()
	{
		if (SupportNative3Dof)
		{
			GlassesMsgSemaphore.SendMsgAndAwaitAck(R6NewerMsgId.TF_REQ_DISPLAY_SCREEN_SIZE_R);
		}
		return _native3DofScreenSize;
	}

	public void SetScreenSize(int size)
	{
		if (SupportNative3Dof)
		{
			SendByteToDevice(296, (byte)size);
			Native3DofScreenSize = size;
		}
	}

	public int GetDistance()
	{
		if (SupportNative3Dof)
		{
			GlassesMsgSemaphore.SendMsgAndAwaitAck(R6NewerMsgId.TF_REQ_DISPLAY_DISTANCE);
		}
		return distanceLevel;
	}

	public void SetDistance(int value)
	{
		if (SupportNative3Dof)
		{
			value = Math.Max(0, Math.Min(9, value));
			SendByteToDevice(295, (byte)value);
			DistanceLevel = value;
		}
	}

	public int GetBrightness()
	{
		if (R6NewerModel)
		{
			GlassesMsgSemaphore.SendMsgAndAwaitAck(R6NewerMsgId.TF_REQ_DISPLAY_BRIGHTNESS_R);
		}
		else
		{
			GlassesMsgSemaphore.SendMsgAndAwaitAck(DeviceMsgId.MSG_R_BRIGHTNESS_FINE_GRAINED);
		}
		return brightnessLevel;
	}

	public static int ClampBrightness(int brightness)
	{
		return Math.Min(Math.Max(brightness, 0), 8);
	}

	public static int ClampVolume(int volume, bool r6Series)
	{
		return Math.Min(Math.Max(volume, 0), r6Series ? 15 : 8);
	}

	public void SetBrightness(int brightness)
	{
		brightness = ClampBrightness(brightness);
		if (R6NewerModel)
		{
			SendByteToDevice(290, (byte)brightness);
			BrightnessLevel = brightness;
		}
		else
		{
			SendByteToDevice(6, (byte)brightness);
		}
	}

	public void SetDuty(int duty)
	{
		if (!SupportNative3Dof)
		{
			if (R6NewerModel)
			{
				int num = Math.Min(Math.Max(duty, 0), 98);
				SendByteToDevice(293, (byte)num);
			}
			else
			{
				SendUintToDevice(10, (uint)duty);
				Thread.Sleep(100);
				SendUintToDevice(9, 0u);
			}
		}
	}

	public int GetVolume()
	{
		if (R6NewerModel)
		{
			GlassesMsgSemaphore.SendMsgAndAwaitAck(R6NewerMsgId.TF_REQ_AUDIO_VOLUME_R);
		}
		else
		{
			GlassesMsgSemaphore.SendMsgAndAwaitAck(DeviceMsgId.MSG_R_AUDIO_LEVEL);
		}
		return volumeLevel;
	}

	public void SetVolume(int volume)
	{
		volume = ClampVolume(volume, R6Series);
		if (R6NewerModel)
		{
			SendByteToDevice(513, (byte)volume);
			VolumeLevel = volume;
		}
		else
		{
			SendByteToDevice(51, (byte)volume);
		}
	}

	private static R6NewerDisplayMode PickNativeDisplayMode(bool ultraWide, bool use1200)
	{
		if (ultraWide)
		{
			if (!use1200)
			{
				return R6NewerDisplayMode.NATIVE_DISPLAY_MODE_ULTRAWIDE_3840_1080_120HZ;
			}
			return R6NewerDisplayMode.NATIVE_DISPLAY_MODE_ULTRAWIDE_3840_1200_120HZ;
		}
		if (!use1200)
		{
			return R6NewerDisplayMode.NATIVE_DISPLAY_MODE_1920_1080_120HZ;
		}
		return R6NewerDisplayMode.NATIVE_DISPLAY_MODE_1920_1200_120HZ;
	}

	public void SendNativeDisplayModeHid(bool ultraWide)
	{
		if (SupportSplitProtocol)
		{
			R6NewerDisplayMode r6NewerDisplayMode = PickNativeDisplayMode(ultraWide, Support1200P);
			Logger.Info($"Set NativeDisplayMode: {r6NewerDisplayMode}");
			GlassesMsgSemaphore.SendMsgAndAwaitAck(R6NewerMsgId.TF_CMD_NATIVE_TRACKING_MODE_W, typeof(R6NewerHidMessage), new byte[1] { 1 });
			GlassesMsgSemaphore.SendMsgAndAwaitAck(R6NewerMsgId.TF_CMD_NATIVE_DISPLAY_MODE_W, typeof(R6NewerHidMessage), new byte[1] { (byte)r6NewerDisplayMode });
		}
		else
		{
			R6OlderDisplayMode r6OlderDisplayMode = (ultraWide ? R6OlderDisplayMode.DISPLAY_MODE_ULTRAWIDE_60HZ : R6OlderDisplayMode.DISPLAY_MODE_1920_1080_IN60_OUT120);
			Logger.Info($"Set R6DisplayMode: {r6OlderDisplayMode}");
			byte[] bytes = new byte[2]
			{
				(byte)r6OlderDisplayMode,
				1
			};
			GlassesMsgSemaphore.SendMsgAndAwaitAck(R6NewerMsgId.TF_CMD_DISPLAY_MODE_W, typeof(R6NewerHidMessage), bytes);
		}
	}

	public async Task SendNativeDisplayModeHidAsync(bool ultraWide)
	{
		if (SupportSplitProtocol)
		{
			R6NewerDisplayMode mode = PickNativeDisplayMode(ultraWide, Support1200P);
			Logger.Info($"Set NativeDisplayMode: {mode}");
			await GlassesMsgSemaphore.SendMsgAndAwaitAckAsync(R6NewerMsgId.TF_CMD_NATIVE_TRACKING_MODE_W, typeof(R6NewerHidMessage), new byte[1] { 1 });
			await GlassesMsgSemaphore.SendMsgAndAwaitAckAsync(R6NewerMsgId.TF_CMD_NATIVE_DISPLAY_MODE_W, typeof(R6NewerHidMessage), new byte[1] { (byte)mode });
		}
		else
		{
			R6OlderDisplayMode r6OlderDisplayMode = (ultraWide ? R6OlderDisplayMode.DISPLAY_MODE_ULTRAWIDE_60HZ : R6OlderDisplayMode.DISPLAY_MODE_1920_1080_IN60_OUT120);
			Logger.Info($"Set R6DisplayMode: {r6OlderDisplayMode}");
			byte[] bytes = new byte[2]
			{
				(byte)r6OlderDisplayMode,
				1
			};
			await GlassesMsgSemaphore.SendMsgAndAwaitAckAsync(R6NewerMsgId.TF_CMD_DISPLAY_MODE_W, typeof(R6NewerHidMessage), bytes);
		}
	}

	public void SendUintToDevice(ushort msgId, uint value)
	{
		if (UseHidDevice)
		{
			if (R6NewerModel)
			{
				R6NewerHidMessage r6NewerHidMessage = new R6NewerHidMessage
				{
					MsgID = msgId,
					DataLen = 4
				};
				BitConverter.GetBytes(value).CopyTo(r6NewerHidMessage.Payload, 0);
				SendMsg(r6NewerHidMessage);
			}
			else
			{
				HidMessage hidMessage = new HidMessage();
				hidMessage.Data.MsgID = msgId;
				hidMessage.Data.PutValue(value);
				SendMsg(hidMessage);
			}
		}
		else
		{
			UsbMessage usbMessage = new UsbMessage();
			usbMessage.Data.MsgID = msgId;
			usbMessage.Data.PutValue(value);
			SendMsg(usbMessage);
		}
	}

	public void SendByteToDevice(ushort msgId, byte value)
	{
		if (UseHidDevice)
		{
			if (R6NewerModel)
			{
				R6NewerHidMessage r6NewerHidMessage = new R6NewerHidMessage
				{
					MsgID = msgId,
					DataLen = 1
				};
				r6NewerHidMessage.Payload[0] = value;
				SendMsg(r6NewerHidMessage);
			}
			else
			{
				HidMessage hidMessage = new HidMessage();
				hidMessage.Data.MsgID = msgId;
				hidMessage.Data.PutValue(value);
				SendMsg(hidMessage);
			}
		}
		else
		{
			UsbMessage usbMessage = new UsbMessage();
			usbMessage.Data.MsgID = msgId;
			usbMessage.Data.PutValue(value);
			SendMsg(usbMessage);
		}
	}

	public void Dispose()
	{
		IsConnected = false;
		if (UseHidDevice)
		{
			VitureHidDevice.Instance.Dispose();
		}
		else if (IsRunInUnity)
		{
			CarinaUsbDevice.Instance.Dispose();
		}
		else
		{
			VitureUsbDevice.Instance.Dispose();
		}
	}

	private void Instance_ReceivedHidData(byte[] data)
	{
		this.ReceivedGlassesData?.Invoke(data);
	}

	private void Instance_S6ImuFrameReceived(R6NewerHidMessage r6Msg)
	{
		this.S6ImuFrameReceived?.Invoke(r6Msg);
	}

	private void Instance_ReceivedUsbData(byte[] data)
	{
		this.ReceivedGlassesData?.Invoke(data);
	}

	public static float GetGlassesFov(string yamlConfig)
	{
		float result = 28f;
		if (!Instance.UseHidDevice)
		{
			result = 28.147f;
		}
		switch (Instance.ProductId)
		{
		case 4385:
			result = 28f;
			break;
		case 4401:
			result = 25.7f;
			break;
		case 4417:
			result = 28.435f;
			break;
		case 4433:
			result = 28.435f;
			break;
		case 4113:
			result = 19.98f;
			break;
		case 4121:
		case 4125:
			result = 22.26f;
			break;
		case 4865:
		case 4881:
			result = 22.26f;
			break;
		}
		return result;
	}

	protected virtual void OnPropertyChanged(string propertyName)
	{
		this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
