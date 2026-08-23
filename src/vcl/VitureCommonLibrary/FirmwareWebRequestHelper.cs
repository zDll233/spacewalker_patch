using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Win32;
using Newtonsoft.Json;

namespace VitureCommonLibrary;

public static class FirmwareWebRequestHelper
{
	private static List<SupporDeviceMsg>? supportList;

	public static List<SupporDeviceMsg>? SupportList
	{
		get
		{
			if (supportList == null)
			{
				supportList = RequestSupportList().Result;
			}
			return supportList;
		}
	}

	private static async Task<List<SupporDeviceMsg>?> RequestSupportList()
	{
		string text = await HttpRequestHelper.Request("/api/v1/system/supportlist");
		if (!string.IsNullOrWhiteSpace(text))
		{
			try
			{
				SupportListResponseMsg supportListResponseMsg = JsonConvert.DeserializeObject<SupportListResponseMsg>(text);
				if (supportListResponseMsg != null && supportListResponseMsg.ErrNum == 0)
				{
					Logger.Info("RequestSupportList Success");
					supportList = supportListResponseMsg?.Data;
					return supportList;
				}
				Logger.Warning("Response Error: " + supportListResponseMsg?.ErrMsg);
			}
			catch (Exception ex)
			{
				Logger.Warning(ex.Message + ": " + text);
			}
		}
		return null;
	}

	public static async Task<FirmwareInfo?> GetFirmware(string culture, int productId, string glassesSN, string currentVersion, string versionFrom7911, bool appMode = true, bool isLatest = true)
	{
		string currentVersion2 = currentVersion;
		try
		{
			if (supportList == null)
			{
				supportList = await RequestSupportList();
			}
			SupporDeviceMsg supporDeviceMsg = supportList?.Where((SupporDeviceMsg x) => x.ProductId == productId).FirstOrDefault();
			string jsonContent = JsonConvert.SerializeObject(new GetFirmwareParam
			{
				CurVersion = "0",
				CurSubVersion = versionFrom7911,
				VersionName = (appMode ? currentVersion2 : string.Empty),
				SN = glassesSN,
				OsType = GetOSName(),
				OsVersion = Environment.OSVersion.ToString(),
				Type = "1",
				Model = (supporDeviceMsg?.Name ?? string.Empty),
				Pid = productId,
				BootVersion = ((!appMode) ? currentVersion2 : string.Empty),
				UpdateLevel = 1
			});
			string value = await HttpRequestHelper.Request("/api/v1/system/firmwarelist", string.Empty, culture, getMethod: false, jsonContent);
			if (!string.IsNullOrWhiteSpace(value))
			{
				FirmwareResponseMsg firmwareResponseMsg = JsonConvert.DeserializeObject<FirmwareResponseMsg>(value);
				if (firmwareResponseMsg != null && firmwareResponseMsg.ErrNum == 0)
				{
					Logger.Info("RequestFirmwareList Success");
					if (firmwareResponseMsg != null && firmwareResponseMsg.Data?.Count > 0)
					{
						return (!isLatest) ? firmwareResponseMsg?.Data.Where((FirmwareInfo x) => x.DisplayVersion == currentVersion2).FirstOrDefault() : firmwareResponseMsg?.Data.Where((FirmwareInfo x) => x.IsLatest == 1).FirstOrDefault();
					}
				}
				else
				{
					Logger.Info("Response Error: " + firmwareResponseMsg?.ErrMsg);
				}
			}
		}
		catch (Exception ex)
		{
			Logger.Error(ex.Message, ex.StackTrace);
		}
		return null;
	}

	private static string GetOSName()
	{
		string result = "UnKnown";
		try
		{
			using RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion");
			if (registryKey != null)
			{
				result = registryKey.GetValue("ProductName")?.ToString() ?? "UnKnown";
			}
		}
		catch (Exception ex)
		{
			Logger.Error(ex.Message, ex.StackTrace);
		}
		return result;
	}
}
