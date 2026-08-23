using Newtonsoft.Json;

namespace VitureCommonLibrary;

public class ResponseMsg
{
	[JsonProperty("logid")]
	public string LogId { get; set; } = string.Empty;


	[JsonProperty("errno")]
	public int ErrNum { get; set; }

	[JsonProperty("errmsg")]
	public string ErrMsg { get; set; } = string.Empty;


	[JsonProperty("data")]
	public object? Data { get; set; }
}
