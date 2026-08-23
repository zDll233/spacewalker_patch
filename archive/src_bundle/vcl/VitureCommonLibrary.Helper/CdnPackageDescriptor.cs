namespace VitureCommonLibrary.Helper;

public sealed class CdnPackageDescriptor
{
	public const string DefaultBaseUrl = "https://static.viture.dev/external-file/Windows";

	public string BaseUrl { get; set; } = "https://static.viture.dev/external-file/Windows";


	public string ManifestFileName { get; set; } = "";


	public string FileName { get; set; } = "";


	public string LogTag { get; set; } = "CDN";

}
