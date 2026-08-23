using System.Text.Json.Serialization;

namespace SpaceWalker.Helper;

public class UpdateInfo
{
	private readonly VersionNum _versionNum = new VersionNum();

	[JsonIgnore]
	public VersionNum VersionNum => _versionNum;

	[JsonPropertyName("version")]
	public string Version
	{
		get
		{
			return _versionNum.Version;
		}
		set
		{
			_versionNum.Version = value;
		}
	}

	[JsonPropertyName("url")]
	public string Url { get; set; } = string.Empty;


	[JsonPropertyName("md5")]
	public string MD5 { get; set; } = string.Empty;

}
