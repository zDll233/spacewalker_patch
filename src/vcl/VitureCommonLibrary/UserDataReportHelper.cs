using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace VitureCommonLibrary;

public static class UserDataReportHelper
{
	public static ulong LastReportTime = 0uL;

	public static List<int> UseTime = new List<int>();

	public static List<int> ClickData = new List<int>();

	public static async Task Report()
	{
		if (UseTime.Count != 0 && ClickData.Count != 0)
		{
			int productId = GlassesDeviceManager.Instance.ProductId;
			string glassesSN = GlassesDeviceManager.Instance.GlassesSN;
			string firmwareVersion = GlassesDeviceManager.Instance.FirmwareVersion;
			ulong secondTimestamp = UnixTimestampHelper.GetSecondTimestamp();
			Dictionary<string, object> value = new Dictionary<string, object>
			{
				["dpVersion"] = string.Empty,
				["fwVersion"] = firmwareVersion,
				["mode"] = "app",
				["productId"] = productId,
				["productName"] = "XR Glasses",
				["sn"] = glassesSN
			};
			Dictionary<string, object> value2 = new Dictionary<string, object>
			{
				["last_report_ts"] = LastReportTime,
				["use_time"] = UseTime,
				["click_data"] = ClickData,
				["cur_report_ts"] = secondTimestamp
			};
			string jsonContent = JsonConvert.SerializeObject(new Dictionary<string, object> { ["data"] = new Dictionary<string, object>
			{
				["info"] = value,
				["report"] = value2,
				["type"] = "greport",
				["from"] = "Windows_SpaceWalker"
			} });
			if (await HttpRequestHelper.Request("/api/v1/alog", string.Empty, string.Empty, getMethod: false, jsonContent) != null)
			{
				LastReportTime = UnixTimestampHelper.GetSecondTimestamp();
			}
		}
	}
}
