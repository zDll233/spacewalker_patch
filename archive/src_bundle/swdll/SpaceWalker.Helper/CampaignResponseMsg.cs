using System.Text.Json.Serialization;

namespace SpaceWalker.Helper;

public class CampaignResponseMsg
{
	[JsonPropertyName("logid")]
	public string LogId { get; set; } = string.Empty;


	[JsonPropertyName("errno")]
	public int ErrNum { get; set; }

	[JsonPropertyName("errmsg")]
	public string ErrMsg { get; set; } = string.Empty;


	[JsonPropertyName("data")]
	public CampaignData? Data { get; set; }
}
