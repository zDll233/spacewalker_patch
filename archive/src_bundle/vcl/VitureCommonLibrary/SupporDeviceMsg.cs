using System.Collections.Generic;
using Newtonsoft.Json;

namespace VitureCommonLibrary;

public class SupporDeviceMsg
{
	public const int SUB_TYPE_GLASSES_BOOT = 101;

	public const int SUB_TYPE_GLASSES_APP = 102;

	[JsonProperty("name")]
	public string Name { get; set; } = string.Empty;


	[JsonProperty("sub_type")]
	public int SubType { get; set; } = 102;


	[JsonProperty("vid")]
	public int VendorId { get; set; }

	[JsonProperty("pid")]
	public int ProductId { get; set; }

	public Dictionary<string, string>? Extend { get; set; }
}
