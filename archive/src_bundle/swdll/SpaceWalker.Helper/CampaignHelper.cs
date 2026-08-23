using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using VitureCommonLibrary;

namespace SpaceWalker.Helper;

public static class CampaignHelper
{
	public static async Task<CampaignInfo?> RequestCampaign(int pid, int vid, string psn, string sn)
	{
		string text = await HttpRequestHelper.Request("/api/v1/desktop/channelcampaign", $"?pid={pid}&vid={vid}&psn={psn}&sn={sn}");
		if (!string.IsNullOrWhiteSpace(text))
		{
			try
			{
				CampaignResponseMsg campaignResponseMsg = JsonSerializer.Deserialize<CampaignResponseMsg>(text);
				if (campaignResponseMsg != null && campaignResponseMsg.ErrNum == 10000)
				{
					Logger.Info("RequestCampaign Success");
					List<CampaignInfo> list = campaignResponseMsg.Data?.CampaignList;
					if (list != null && list.Count > 0)
					{
						return list.Where((CampaignInfo x) => x.Status == 1).FirstOrDefault();
					}
				}
				else
				{
					Logger.Warning("Response Error: " + campaignResponseMsg?.ErrMsg);
				}
			}
			catch (Exception ex)
			{
				Logger.Error(ex.Message, ex.StackTrace);
			}
		}
		return null;
	}
}
