using System.Collections.Generic;
using Newtonsoft.Json;

namespace VitureCommonLibrary;

public class VersionMsg
{
	[JsonProperty("version_name")]
	public string Name { get; set; } = string.Empty;


	[JsonProperty("version_code")]
	public long Code { get; set; }

	[JsonProperty("display_version")]
	public string DisplayVersion { get; set; } = string.Empty;


	[JsonProperty("update_time")]
	public string UpdateTime { get; set; } = string.Empty;


	[JsonProperty("release_note")]
	public Dictionary<string, string>? ReleaseNote { get; set; }
}
