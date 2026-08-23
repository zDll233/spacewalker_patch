using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace VitureCommonLibrary.Helper;

public static class CdnPackageInstallerHelper
{
	public static string GetLocalPath(string fileName)
	{
		return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "VITURE", fileName);
	}

	public static async Task<CdnPackageInstallerResult> EnsureAsync(CdnPackageDescriptor pkg, int maxRetries = 3)
	{
		string tag = pkg.LogTag;
		string text = pkg.BaseUrl + "/" + pkg.ManifestFileName;
		string fileUrl = pkg.BaseUrl + "/" + pkg.FileName;
		string localPath = GetLocalPath(pkg.FileName);
		Logger.Info(tag + ": ===== EnsureAsync begin =====");
		Logger.Info(tag + ": file=" + pkg.FileName + " os=" + RuntimeInformation.OSDescription);
		Logger.Info(tag + ": manifestUrl=" + text);
		Logger.Info(tag + ": fileUrl=" + fileUrl);
		Logger.Info(tag + ": localPath=" + localPath);
		LogLocalFileState(tag, localPath);
		HttpRequestHelper.LogEndpointDiagnostics(text);
		Dictionary<string, string> dictionary = await FetchMd5ManifestAsync(tag, text);
		if (dictionary == null)
		{
			Logger.Warning(tag + ": failed to fetch md5 manifest (见上方 RequestJsonContent 的 HTTP 详情)");
			return CdnPackageInstallerResult.Unavailable;
		}
		Logger.Info(tag + ": manifest entries=[" + string.Join(", ", dictionary.Keys) + "]");
		if (!dictionary.TryGetValue(pkg.FileName, out var value) || string.IsNullOrWhiteSpace(value))
		{
			Logger.Info(tag + ": manifest has no entry for " + pkg.FileName);
			return CdnPackageInstallerResult.Unavailable;
		}
		Logger.Info(tag + ": expected md5 for " + pkg.FileName + " = " + value);
		if (IsLocalFileValid(tag, localPath, value))
		{
			Logger.Info(tag + ": " + pkg.FileName + " already valid at " + localPath + " — no download needed");
			return CdnPackageInstallerResult.AlreadyValid;
		}
		Logger.Info($"{tag}: downloading {pkg.FileName} (maxRetries={maxRetries})");
		bool flag = await DownloadWithRetryAsync(tag, fileUrl, pkg.FileName, localPath, value, maxRetries);
		Logger.Info(tag + ": ===== EnsureAsync end — " + (flag ? "Downloaded" : "Failed") + " =====");
		return flag ? CdnPackageInstallerResult.Downloaded : CdnPackageInstallerResult.Failed;
	}

	private static void LogLocalFileState(string tag, string filePath)
	{
		try
		{
			if (File.Exists(filePath))
			{
				FileInfo fileInfo = new FileInfo(filePath);
				Logger.Info($"{tag}: existing local file size={fileInfo.Length} bytes lastWrite={fileInfo.LastWriteTimeUtc:o} at {filePath}");
			}
			else
			{
				Logger.Info(tag + ": no existing local file at " + filePath);
			}
		}
		catch (Exception ex)
		{
			Logger.Warning(tag + ": stat local file failed for " + filePath + ": " + ex.Message);
		}
	}

	private static async Task<Dictionary<string, string>?> FetchMd5ManifestAsync(string tag, string manifestUrl)
	{
		try
		{
			string value = await HttpRequestHelper.RequestJsonContent(manifestUrl);
			if (string.IsNullOrWhiteSpace(value))
			{
				return null;
			}
			return JsonConvert.DeserializeObject<Dictionary<string, string>>(value);
		}
		catch (Exception ex)
		{
			Logger.Warning(tag + ": parse md5 manifest failed: " + ex.Message);
			return null;
		}
	}

	private static bool IsLocalFileValid(string tag, string filePath, string expectedMd5)
	{
		if (!File.Exists(filePath))
		{
			return false;
		}
		try
		{
			using MD5 mD = MD5.Create();
			using FileStream inputStream = File.OpenRead(filePath);
			return string.Equals(BitConverter.ToString(mD.ComputeHash(inputStream)).Replace("-", "").ToLowerInvariant(), expectedMd5, StringComparison.OrdinalIgnoreCase);
		}
		catch (Exception ex)
		{
			Logger.Warning(tag + ": md5 check failed for " + filePath + ": " + ex.Message);
			return false;
		}
	}

	private static async Task<bool> DownloadWithRetryAsync(string tag, string url, string fileName, string localPath, string expectedMd5, int maxRetries)
	{
		List<string> attemptLog = new List<string>();
		for (int attempt = 1; attempt <= maxRetries; attempt++)
		{
			try
			{
				bool flag = await HttpRequestHelper.DownloadFile(url, fileName, expectedMd5);
				bool flag2 = flag && IsLocalFileValid(tag, localPath, expectedMd5);
				long num = (File.Exists(localPath) ? new FileInfo(localPath).Length : (-1));
				attemptLog.Add($"#{attempt}: downloadFile={flag} md5Valid={flag2} size={num}");
				if (flag2)
				{
					Logger.Info($"{tag}: download attempt {attempt}/{maxRetries} OK for {fileName} (size={num})");
					return true;
				}
				Logger.Warning($"{tag}: download attempt {attempt}/{maxRetries} verification failed for {fileName} (downloadFile={flag}, size={num}, expectedMd5={expectedMd5})");
			}
			catch (Exception ex)
			{
				attemptLog.Add($"#{attempt}: exception=[{ex.GetType().Name}] {ex.Message}");
				Logger.Warning($"{tag}: download attempt {attempt}/{maxRetries} threw for {fileName}: [{ex.GetType().Name}] {ex.Message}");
			}
			if (attempt < maxRetries)
			{
				await Task.Delay(1000);
			}
		}
		Logger.Error(string.Format("{0}: ALL {1} attempts failed for {2}. Summary: {3}", tag, maxRetries, fileName, string.Join(" | ", attemptLog)));
		LogLocalFileState(tag, localPath);
		HttpRequestHelper.LogEndpointDiagnostics(url);
		return false;
	}
}
