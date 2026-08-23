using System.Text.Json.Serialization;

namespace SpaceWalker.Helper;

public class VersionInfo
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

	[JsonPropertyName("commitId")]
	public string CommitId { get; set; } = string.Empty;

}
