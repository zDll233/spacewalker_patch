using System.Collections.Generic;
using Newtonsoft.Json;

namespace VitureCommonLibrary;

public class FirmwareResponseMsg
{
	[JsonProperty("logid")]
	public string LogId { get; set; } = string.Empty;


	[JsonProperty("errno")]
	public int ErrNum { get; set; }

	[JsonProperty("errmsg")]
	public string ErrMsg { get; set; } = string.Empty;


	[JsonProperty("data")]
	[JsonConverter(typeof(FirmwareInfoConverter))]
	public List<FirmwareInfo>? Data { get; set; }
}
