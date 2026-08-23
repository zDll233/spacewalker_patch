using Newtonsoft.Json;

namespace VitureCommonLibrary;

public class GetFirmwareParam
{
	[JsonProperty("curVersion")]
	public string CurVersion { get; set; } = string.Empty;


	[JsonProperty("curSubVersion")]
	public string CurSubVersion { get; set; } = string.Empty;


	[JsonProperty("versionName")]
	public string VersionName { get; set; } = string.Empty;


	[JsonProperty("SN")]
	public string SN { get; set; } = string.Empty;


	[JsonProperty("osType")]
	public string OsType { get; set; } = string.Empty;


	[JsonProperty("osVersion")]
	public string OsVersion { get; set; } = string.Empty;


	[JsonProperty("type")]
	public string Type { get; set; } = string.Empty;


	[JsonProperty("model")]
	public string Model { get; set; } = string.Empty;


	[JsonProperty("pid")]
	public int Pid { get; set; }

	[JsonProperty("bootVersion")]
	public string BootVersion { get; set; } = string.Empty;


	[JsonProperty("updateLevel")]
	public int UpdateLevel { get; set; }
}
