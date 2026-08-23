using Newtonsoft.Json;

namespace VitureCommonLibrary;

public class FirmwareInfo
{
	[JsonProperty("checksum")]
	public string Checksum { get; set; } = string.Empty;


	[JsonProperty("pkgName")]
	public string PkgName { get; set; } = string.Empty;


	[JsonProperty("url")]
	public string Url { get; set; } = string.Empty;


	[JsonProperty("subUrl")]
	public string SubUrl { get; set; } = string.Empty;


	[JsonProperty("osdUrl")]
	public string OsdUrl { get; set; } = string.Empty;


	[JsonProperty("verCode")]
	public string VerCode { get; set; } = string.Empty;


	[JsonProperty("verName")]
	public string VerName { get; set; } = string.Empty;


	[JsonProperty("subVerCode")]
	public string SubVerCode { get; set; } = string.Empty;


	[JsonProperty("subCRC")]
	public string SubCRC { get; set; } = string.Empty;


	[JsonProperty("osdVerCode")]
	public string OsdVerCode { get; set; } = string.Empty;


	[JsonProperty("release_note")]
	public string ReleaseNote { get; set; } = string.Empty;


	[JsonProperty("updateTime")]
	public string UpdateTime { get; set; } = string.Empty;


	[JsonProperty("isLatest")]
	public int IsLatest { get; set; }

	[JsonProperty("isHistoryVer")]
	public int IsHistoryVer { get; set; }

	[JsonProperty("displayVersion")]
	public string DisplayVersion { get; set; } = string.Empty;


	[JsonProperty("metadata")]
	public R6FirmwareMetaData MetaData { get; set; }
}
