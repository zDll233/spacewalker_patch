namespace SpaceWalker.Services.Ota;

public class FirmwareVersionCheckResult
{
	public bool HasNewVersion { get; set; }

	public string CurrentVersion { get; set; } = string.Empty;


	public string LatestVersion { get; set; } = string.Empty;


	public string ReleaseNote { get; set; } = string.Empty;


	public bool NeedWebOta { get; set; }
}
