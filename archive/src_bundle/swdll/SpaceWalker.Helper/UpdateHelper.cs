using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using VitureCommonLibrary;

namespace SpaceWalker.Helper;

internal static class UpdateHelper
{
	private static string newVersionUrl = string.Empty;

	private static string remoteFileMd5 = string.Empty;

	private static bool downloading = false;

	private static async Task<UpdateInfo?> RequestJsonContent(string url)
	{
		using HttpClient client = new HttpClient();
		client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.3");
		try
		{
			using CancellationTokenSource cts = new CancellationTokenSource(TimeSpan.FromSeconds(10.0));
			HttpResponseMessage obj = await client.GetAsync(url, cts.Token);
			obj.EnsureSuccessStatusCode();
			UpdateInfo updateInfo = JsonSerializer.Deserialize<UpdateInfo>(await obj.Content.ReadAsStringAsync());
			if (updateInfo != null)
			{
				return updateInfo;
			}
			Logger.Error("JSON Deserialize get null");
		}
		catch (OperationCanceledException)
		{
			Logger.Error("HTTP Request Timeout (10s)");
		}
		catch (HttpRequestException ex2)
		{
			Logger.Error("HTTP Request Error: " + ex2.Message);
		}
		catch (JsonException ex3)
		{
			Logger.Error("JSON Parse Error: " + ex3.Message);
		}
		return null;
	}

	private static VersionInfo? LoadVersionFile()
	{
		string path = Path.Combine(Directory.GetCurrentDirectory(), "version.json");
		if (File.Exists(path))
		{
			try
			{
				return JsonSerializer.Deserialize<VersionInfo>(File.ReadAllText(path, Encoding.UTF8));
			}
			catch (Exception ex)
			{
				Logger.Error(ex.Message, ex.StackTrace);
				return null;
			}
		}
		return null;
	}

	public static async Task<bool> RunInstaller()
	{
		_ = 1;
		try
		{
			bool flag = false;
			if (!downloading)
			{
				flag = await HttpRequestHelper.DownloadFile(newVersionUrl, "SpaceWalker_installer.exe", remoteFileMd5);
			}
			if (flag)
			{
				string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
				string filePath = Path.Combine(folderPath, "VITURE", "SpaceWalker_installer.exe");
				if (File.Exists(filePath))
				{
					Process process = new Process();
					process.StartInfo.UseShellExecute = true;
					process.StartInfo.RedirectStandardOutput = false;
					process.StartInfo.CreateNoWindow = false;
					process.StartInfo.WorkingDirectory = Directory.GetCurrentDirectory();
					process.StartInfo.FileName = filePath;
					process.StartInfo.Arguments = string.Empty;
					process.Start();
					await process.WaitForExitAsync();
					File.Delete(filePath);
				}
				else
				{
					Logger.Error("Installer file not exists");
				}
			}
		}
		catch (Exception ex)
		{
			Logger.Error("Run installer error: " + ex.Message);
		}
		return false;
	}

	public static async Task<bool> CheckUpdates()
	{
		UpdateInfo updateInfo = await RequestJsonContent("https://static.viture.dev/external-file/Windows/latest_version.json");
		VersionInfo versionInfo = LoadVersionFile();
		if (versionInfo == null && updateInfo != null)
		{
			string currentAppVersion = GetCurrentAppVersion();
			if (new VersionNum
			{
				Version = (currentAppVersion ?? "0.0.0.0")
			} < updateInfo.VersionNum)
			{
				newVersionUrl = updateInfo.Url;
				remoteFileMd5 = updateInfo.MD5;
				Logger.Info("Has new Version: " + newVersionUrl);
				return !downloading;
			}
		}
		Logger.Info("Current App Version: " + JsonSerializer.Serialize(versionInfo));
		if (versionInfo != null && updateInfo != null && versionInfo.VersionNum < updateInfo.VersionNum)
		{
			newVersionUrl = updateInfo.Url;
			remoteFileMd5 = updateInfo.MD5;
			Logger.Info("Has new Version: " + newVersionUrl);
			return !downloading;
		}
		return false;
	}

	public static string? GetCurrentAppVersion()
	{
		return Assembly.GetEntryAssembly()?.GetCustomAttribute<AssemblyFileVersionAttribute>()?.Version;
	}
}
