using System;
using Newtonsoft.Json;

namespace VitureCommonLibrary;

public class R6FirmwareMetaData
{
	[JsonProperty("version")]
	public string Version { get; set; } = string.Empty;


	[JsonProperty("raw")]
	public byte[] RawData { get; set; } = Array.Empty<byte>();

}
