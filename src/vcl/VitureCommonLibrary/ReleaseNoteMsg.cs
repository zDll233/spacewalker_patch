using System.Collections.Generic;
using Newtonsoft.Json;

namespace VitureCommonLibrary;

public class ReleaseNoteMsg
{
	[JsonProperty("name")]
	public string Name { get; set; } = string.Empty;


	[JsonProperty("currentVersion")]
	public List<VersionMsg>? Version { get; set; }
}
