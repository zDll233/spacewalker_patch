using System.Threading.Tasks;

namespace VitureCommonLibrary.Helper;

public static class DotnetRuntimeInstallerHelper
{
	public const string ManifestFileName = "dotnet_runtime_md5.json";

	public const string LogTag = "DotnetRuntime";

	public const string FileName = "windowsdesktop-runtime-8.0.22-win-x64.exe";

	public static string GetLocalInstallerPath()
	{
		return CdnPackageInstallerHelper.GetLocalPath("windowsdesktop-runtime-8.0.22-win-x64.exe");
	}

	public static CdnPackageDescriptor BuildDescriptor()
	{
		return new CdnPackageDescriptor
		{
			ManifestFileName = "dotnet_runtime_md5.json",
			FileName = "windowsdesktop-runtime-8.0.22-win-x64.exe",
			LogTag = "DotnetRuntime"
		};
	}

	public static Task<CdnPackageInstallerResult> EnsureInstallerAsync(int maxRetries = 3)
	{
		return CdnPackageInstallerHelper.EnsureAsync(BuildDescriptor(), maxRetries);
	}
}
