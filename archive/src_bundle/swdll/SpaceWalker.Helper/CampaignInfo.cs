using System.Text.Json.Serialization;

namespace SpaceWalker.Helper;

public class CampaignInfo
{
	[JsonPropertyName("id")]
	public int Id { get; set; }

	[JsonPropertyName("action_url")]
	public string ActionUrl { get; set; } = string.Empty;


	[JsonPropertyName("regions")]
	public string Regions { get; set; } = string.Empty;


	[JsonPropertyName("status")]
	public int Status { get; set; }

	[JsonPropertyName("image_url")]
	public string ImageUrl { get; set; } = string.Empty;

}
