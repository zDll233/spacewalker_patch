using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SpaceWalker.Helper;

public class CampaignData
{
	[JsonPropertyName("channel")]
	public int Channel { get; set; }

	[JsonPropertyName("campaign_list")]
	public List<CampaignInfo> CampaignList { get; set; } = new List<CampaignInfo>();

}
