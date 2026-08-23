using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SpaceWalker.Assets.Languages;
using SpaceWalker.Helper;
using VitureCommonLibrary;

namespace SpaceWalker.Services.Ota;

public class FirmwareOtaManager
{
	private FirmwareInfo? _lastestVersion;

	private string versionFromDP = string.Empty;

	private string glassesSN = string.Empty;

	private string _currentVersion = string.Empty;

	public static FirmwareOtaManager Instance { get; } = new FirmwareOtaManager();


	public async Task<FirmwareVersionCheckResult> CheckFirmwareVersionAsync()
	{
		FirmwareVersionCheckResult result = ((GlassesDeviceManager.Instance.UseHidDevice && GlassesDeviceManager.Instance.R6NewerModel) ? (await R6CheckFirmwareVersionAsync()) : ((!GlassesDeviceManager.Instance.UseHidDevice) ? (await UsbCheckFirmwareVersionAsync()) : (await HidCheckFirmwareVersionAsync())));
		FirmwareVersionCheckResult firmwareVersionCheckResult = result;
		firmwareVersionCheckResult.NeedWebOta = await FetchOtaConfig();
		return result;
	}

	public void OnReceivedGlassesData(object msg)
	{
		if (msg is HidMessage hidMsg)
		{
			OnReceivedHidData(hidMsg);
		}
		else if (msg is UsbMessage usbMsg)
		{
			OnReceivedUsbData(usbMsg);
		}
		else if (msg is R6NewerHidMessage r6Msg)
		{
			OnReceivedR6Data(r6Msg);
		}
	}

	private void OnReceivedHidData(HidMessage hidMsg)
	{
		if (hidMsg.Data.MsgID.Equal(DeviceMsgId.MSG_R_MCU_APP_FW_VERSION))
		{
			_currentVersion = hidMsg.GetVersion();
			GlassesMsgSemaphore.ReleaseSemaphore(hidMsg.Data.MsgID);
			Logger.Info("HidOta _currentVersion: " + _currentVersion);
		}
		if (hidMsg.Data.MsgID.Equal(DeviceMsgId.MSG_R_GLASSID))
		{
			glassesSN = hidMsg.GetGlassesSN();
			GlassesMsgSemaphore.ReleaseSemaphore(hidMsg.Data.MsgID);
			Logger.Info("HidOta glassesSN: " + glassesSN);
		}
		if (hidMsg.Data.MsgID.Equal(DeviceMsgId.MSG_R_DP_FW_VERSION))
		{
			GlassesMsgSemaphore.ReleaseSemaphore(hidMsg.Data.MsgID);
			int dataLength = hidMsg.Data.DataLength;
			if (dataLength < hidMsg.Data.Payload.Length)
			{
				versionFromDP = Encoding.UTF8.GetString(hidMsg.Data.Payload, 1, dataLength - 1);
			}
			Logger.Info("HidOta versionFromDP: " + versionFromDP);
		}
		if (hidMsg.Data.MsgID.Equal(DeviceMsgId.MSG_R_DP_FW_VERSION_FROMMCU))
		{
			GlassesMsgSemaphore.ReleaseSemaphore(hidMsg.Data.MsgID);
		}
	}

	private void OnReceivedUsbData(UsbMessage usbMsg)
	{
		if (usbMsg.Data.MsgID.Equal(DeviceMsgId.MSG_R_MCU_APP_FW_VERSION))
		{
			_currentVersion = usbMsg.GetVersion();
			if (_currentVersion.Length == 20)
			{
				_currentVersion = _currentVersion.Substring(3, _currentVersion.Length - 3);
			}
			GlassesMsgSemaphore.ReleaseSemaphore(usbMsg.Data.MsgID);
			Logger.Info("UsbOta _currentVersion: " + _currentVersion);
		}
		if (usbMsg.Data.MsgID.Equal(DeviceMsgId.MSG_R_GLASSID))
		{
			glassesSN = usbMsg.GetGlassesSN();
			GlassesMsgSemaphore.ReleaseSemaphore(usbMsg.Data.MsgID);
			Logger.Info("UsbOta glassesSN: " + glassesSN);
		}
		if (usbMsg.Data.MsgID.Equal(DeviceMsgId.MSG_R_DP_FW_VERSION))
		{
			GlassesMsgSemaphore.ReleaseSemaphore(usbMsg.Data.MsgID);
			int dataLength = usbMsg.Data.DataLength;
			if (dataLength < usbMsg.Data.Payload.Length)
			{
				versionFromDP = Encoding.UTF8.GetString(usbMsg.Data.Payload, 1, dataLength - 1);
			}
			Logger.Info("UsbOta versionFromDP: " + versionFromDP);
		}
	}

	private void OnReceivedR6Data(R6NewerHidMessage r6Msg)
	{
		if (r6Msg.MsgID.Equal(R6NewerMsgId.TF_REQ_ID_APP_FW_VERSION_R) && r6Msg.GetAckSuceess())
		{
			_currentVersion = r6Msg.GetVersion();
			GlassesMsgSemaphore.ReleaseSemaphore(r6Msg.MsgID);
			Logger.Info("R6Ota _currentVersion: " + _currentVersion);
		}
		if (r6Msg.MsgID.Equal(R6NewerMsgId.TF_REQ_ID_BOARD_SN_R) && r6Msg.GetAckSuceess())
		{
			glassesSN = r6Msg.GetGlassesSN();
			GlassesMsgSemaphore.ReleaseSemaphore(r6Msg.MsgID);
			Logger.Info("R6Ota glassesSN: " + glassesSN);
		}
	}

	public async Task<bool> FetchOtaConfig()
	{
		bool needUseWebOta = false;
		Assembly entryAssembly = Assembly.GetEntryAssembly();
		string name = entryAssembly.GetName().Name;
		string exeVersion = entryAssembly.GetCustomAttribute<AssemblyFileVersionAttribute>()?.Version;
		string url = string.Empty;
		if (name == "SpaceWalker")
		{
			url = "https://static.viture.dev/external-file/Windows/ota_config_sww.json";
		}
		else if (name == "Immersive3D")
		{
			url = "https://static.viture.dev/external-file/Windows/ota_config_i3w.json";
		}
		OtaControlConfig otaControlConfig = await OtaConfigRule.RequestJsonContent(url);
		string item = "0x" + GlassesDeviceManager.Instance.ProductId.ToString("X");
		if (otaControlConfig == null)
		{
			return needUseWebOta;
		}
		foreach (OtaRule rule in otaControlConfig.ota_control_config.rules)
		{
			if (rule.pids.Contains(item))
			{
				VersionNum versionNum = new VersionNum
				{
					Version = rule.max_disabled_app_version
				};
				VersionNum obj = new VersionNum
				{
					Version = rule.min_disabled_app_version
				};
				VersionNum versionNum2 = new VersionNum
				{
					Version = exeVersion
				};
				if (obj <= versionNum2 && versionNum2 <= versionNum)
				{
					needUseWebOta = true;
				}
			}
		}
		return needUseWebOta;
	}

	public async Task<FirmwareVersionCheckResult> HidCheckFirmwareVersionAsync()
	{
		FirmwareVersionCheckResult result = new FirmwareVersionCheckResult();
		await Task.Run(async delegate
		{
			glassesSN = string.Empty;
			versionFromDP = string.Empty;
			_currentVersion = string.Empty;
			if (GlassesDeviceManager.Instance.UseHidDevice && !GlassesDeviceManager.Instance.R6NewerModel)
			{
				Thread.Sleep(300);
				GlassesMsgSemaphore.SendMsgAndAwaitAck(DeviceMsgId.MSG_R_GLASSID);
				Thread.Sleep(300);
				GlassesMsgSemaphore.SendMsgAndAwaitAck(DeviceMsgId.MSG_R_DP_FW_VERSION);
				Thread.Sleep(300);
				GlassesMsgSemaphore.SendMsgAndAwaitAck(DeviceMsgId.MSG_R_DP_FW_VERSION_FROMMCU);
				Thread.Sleep(300);
				GlassesMsgSemaphore.SendMsgAndAwaitAck(DeviceMsgId.MSG_R_MCU_APP_FW_VERSION);
				Thread.Sleep(300);
				result.CurrentVersion = _currentVersion;
				if (!string.IsNullOrWhiteSpace(_currentVersion))
				{
					int productId = GlassesDeviceManager.Instance.ProductId;
					SupporDeviceMsg? obj = FirmwareWebRequestHelper.SupportList?.Where((SupporDeviceMsg x) => x.ProductId == productId).FirstOrDefault();
					bool flag = obj != null && obj.SubType == 102;
					if (flag)
					{
						FirmwareInfo lastestVersion = await FirmwareWebRequestHelper.GetFirmware(Resources.Culture.Name, productId, glassesSN, _currentVersion, versionFromDP, flag);
						_lastestVersion = lastestVersion;
						if (_lastestVersion != null && !string.IsNullOrWhiteSpace(_lastestVersion.DisplayVersion))
						{
							result.LatestVersion = _lastestVersion.DisplayVersion;
							result.ReleaseNote = _lastestVersion.ReleaseNote ?? string.Empty;
							string strA = _currentVersion.Substring(_currentVersion.Length - 8, 8);
							string strB = _lastestVersion.DisplayVersion.Substring(_lastestVersion.DisplayVersion.Length - 8, 8);
							result.HasNewVersion = string.Compare(strA, strB) < 0;
						}
					}
				}
			}
		});
		return result;
	}

	public async Task<FirmwareVersionCheckResult> UsbCheckFirmwareVersionAsync()
	{
		FirmwareVersionCheckResult result = new FirmwareVersionCheckResult();
		await Task.Run(async delegate
		{
			glassesSN = string.Empty;
			versionFromDP = string.Empty;
			_currentVersion = string.Empty;
			if (!GlassesDeviceManager.Instance.UseHidDevice)
			{
				Thread.Sleep(500);
				GlassesMsgSemaphore.SendMsgAndAwaitAck(DeviceMsgId.MSG_R_GLASSID, typeof(UsbMessage));
				Thread.Sleep(500);
				GlassesMsgSemaphore.SendMsgAndAwaitAck(DeviceMsgId.MSG_R_DP_FW_VERSION, typeof(UsbMessage));
				Thread.Sleep(500);
				GlassesMsgSemaphore.SendMsgAndAwaitAck(DeviceMsgId.MSG_R_MCU_APP_FW_VERSION, typeof(UsbMessage));
				Thread.Sleep(500);
				result.CurrentVersion = _currentVersion;
				if (!string.IsNullOrWhiteSpace(_currentVersion) && !string.IsNullOrWhiteSpace(versionFromDP))
				{
					int productId = GlassesDeviceManager.Instance.ProductId;
					SupporDeviceMsg? obj = FirmwareWebRequestHelper.SupportList?.Where((SupporDeviceMsg x) => x.ProductId == productId).FirstOrDefault();
					bool flag = obj != null && obj.SubType == 102;
					if (flag)
					{
						FirmwareInfo lastestVersion = await FirmwareWebRequestHelper.GetFirmware(Resources.Culture.Name, productId, glassesSN, _currentVersion, versionFromDP, flag);
						_lastestVersion = lastestVersion;
						if (_lastestVersion != null && !string.IsNullOrWhiteSpace(_lastestVersion.DisplayVersion))
						{
							result.LatestVersion = _lastestVersion.DisplayVersion;
							result.ReleaseNote = _lastestVersion.ReleaseNote ?? string.Empty;
							string strA = _currentVersion.Substring(_currentVersion.Length - 8, 8);
							string strB = _lastestVersion.DisplayVersion.Substring(_lastestVersion.DisplayVersion.Length - 8, 8);
							result.HasNewVersion = string.Compare(strA, strB) < 0;
						}
					}
				}
			}
		});
		return result;
	}

	public async Task<FirmwareVersionCheckResult> R6CheckFirmwareVersionAsync()
	{
		FirmwareVersionCheckResult result = new FirmwareVersionCheckResult();
		await Task.Run(async delegate
		{
			glassesSN = string.Empty;
			_currentVersion = string.Empty;
			if (GlassesDeviceManager.Instance.UseHidDevice)
			{
				Thread.Sleep(200);
				GlassesMsgSemaphore.SendMsgAndAwaitAck(R6NewerMsgId.TF_REQ_ID_BOARD_SN_R);
				Thread.Sleep(200);
				GlassesMsgSemaphore.SendMsgAndAwaitAck(R6NewerMsgId.TF_REQ_ID_APP_FW_VERSION_R);
				Thread.Sleep(200);
				result.CurrentVersion = _currentVersion;
				if (!string.IsNullOrWhiteSpace(_currentVersion))
				{
					int productId = GlassesDeviceManager.Instance.ProductId;
					SupporDeviceMsg? obj = FirmwareWebRequestHelper.SupportList?.Where((SupporDeviceMsg x) => x.ProductId == productId).FirstOrDefault();
					bool flag = obj != null && obj.SubType == 102;
					if (flag)
					{
						FirmwareInfo lastestVersion = await FirmwareWebRequestHelper.GetFirmware(Resources.Culture.Name, productId, glassesSN, _currentVersion, string.Empty, flag);
						_lastestVersion = lastestVersion;
						if (_lastestVersion != null && !string.IsNullOrWhiteSpace(_lastestVersion.DisplayVersion))
						{
							result.LatestVersion = _lastestVersion.DisplayVersion;
							result.ReleaseNote = _lastestVersion.ReleaseNote ?? string.Empty;
							string strA = _currentVersion.Substring(_currentVersion.Length - 8, 8);
							string strB = _lastestVersion.DisplayVersion.Substring(_lastestVersion.DisplayVersion.Length - 8, 8);
							result.HasNewVersion = string.Compare(strA, strB) < 0;
						}
					}
				}
			}
		});
		return result;
	}
}
