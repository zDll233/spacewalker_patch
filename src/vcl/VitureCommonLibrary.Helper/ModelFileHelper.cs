using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace VitureCommonLibrary.Helper;

public static class ModelFileHelper
{
	public const string ModelBaseUrl = "https://static.viture.dev/external-file/Windows";

	public const string Md5ManifestName = "immersive3d_models_md5.json";

	public static IReadOnlyList<string> GetRequiredModelFiles(string mlDevice)
	{
		List<string> list = new List<string>();
		if (string.IsNullOrWhiteSpace(mlDevice))
		{
			return list;
		}
		if (mlDevice.StartsWith("qnn_"))
		{
			string text = mlDevice.Substring("qnn_".Length);
			if (!string.IsNullOrWhiteSpace(text))
			{
				list.Add("depth_anything_v2_vits_518_" + text + ".bin");
			}
		}
		else
		{
			switch (mlDevice)
			{
			case "vino_ptl":
			case "vino_lnl":
				list.Add("depth_anything_v2_vits_518_vino.xml");
				list.Add("depth_anything_v2_vits_518_vino.bin");
				break;
			case "vino_cnn":
				list.Add("depth_distill_896_vino.xml");
				list.Add("depth_distill_896_vino.bin");
				break;
			default:
				if (mlDevice.StartsWith("cuda_"))
				{
					string text2 = mlDevice.Substring("cuda_".Length);
					if (!string.IsNullOrWhiteSpace(text2))
					{
						list.Add("depth_anything_v2_vits_fp16_518_" + text2 + ".trt");
					}
				}
				break;
			}
		}
		return list;
	}

	public static IReadOnlyList<string> GetOptionalCnnModelFiles(string mlDevice)
	{
		List<string> list = new List<string>();
		if (mlDevice == "dml")
		{
			list.Add("fused_model_q4f16_518.onnx");
		}
		return list;
	}

	public static string GetLocalModelPath(string fileName)
	{
		string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
		return Path.Combine(folderPath, "VITURE", "Immersive3D", "models", fileName);
	}

	private static (string zip, string subdir, string keyDll)? GetPluginInfo(string mlDevice)
	{
		if (string.IsNullOrWhiteSpace(mlDevice))
		{
			return null;
		}
		if (mlDevice.StartsWith("vino"))
		{
			return ("vino_plugin.zip", "vino", "DepthAnythingVino.dll");
		}
		if (mlDevice.StartsWith("cuda"))
		{
			return ("cuda_plugin.zip", "cuda", "DepthAnythingCuda.dll");
		}
		if (mlDevice.StartsWith("qnn"))
		{
			return ("qnn_plugin.zip", "qnn", "DepthAnythingQnn.dll");
		}
		return null;
	}

	public static string GetPluginDir(string subdir)
	{
		return AppDomain.CurrentDomain.BaseDirectory;
	}

	public static bool IsPluginInstalled(string mlDevice)
	{
		(string, string, string)? pluginInfo = GetPluginInfo(mlDevice);
		if (!pluginInfo.HasValue)
		{
			return true;
		}
		return File.Exists(Path.Combine(GetPluginDir(pluginInfo.Value.Item2), pluginInfo.Value.Item3));
	}

	public static string ResolveRuntimeDevice(string mlDevice)
	{
		if (string.IsNullOrWhiteSpace(mlDevice) || mlDevice == "dml")
		{
			return "dml";
		}
		foreach (string requiredModelFile in GetRequiredModelFiles(mlDevice))
		{
			if (!File.Exists(GetLocalModelPath(requiredModelFile)))
			{
				Logger.Warning("ModelFileHelper: model '" + requiredModelFile + "' for device '" + mlDevice + "' not present yet, falling back to dml");
				return "dml";
			}
		}
		if (!IsPluginInstalled(mlDevice))
		{
			Logger.Warning("ModelFileHelper: plugin for device '" + mlDevice + "' not installed yet, falling back to dml");
			return "dml";
		}
		return mlDevice;
	}

	public static async Task<bool> EnsureModelFilesAsync(string mlDevice, int maxRetries = 3)
	{
		IReadOnlyList<string> files = GetRequiredModelFiles(mlDevice);
		IReadOnlyList<string> optionalFiles = GetOptionalCnnModelFiles(mlDevice);
		(string, string, string)? pluginInfo = GetPluginInfo(mlDevice);
		if (files.Count == 0 && optionalFiles.Count == 0 && !pluginInfo.HasValue)
		{
			Logger.Info("ModelFileHelper: nothing required for mlDevice '" + mlDevice + "'");
			return true;
		}
		Dictionary<string, string> md5Dict = await FetchMd5ManifestAsync();
		if (md5Dict == null)
		{
			Logger.Warning("ModelFileHelper: failed to fetch md5 manifest, skip model file check");
			return false;
		}
		bool allOk = true;
		foreach (string file2 in files)
		{
			if (!md5Dict.TryGetValue(file2, out string value) || string.IsNullOrWhiteSpace(value))
			{
				Logger.Warning("ModelFileHelper: md5 missing for " + file2 + ", skip");
				continue;
			}
			if (IsLocalFileValid(GetLocalModelPath(file2), value))
			{
				Logger.Info("ModelFileHelper: " + file2 + " already valid");
				continue;
			}
			Logger.Info("ModelFileHelper: downloading " + file2);
			bool flag = await DownloadWithRetryAsync(file2, value, maxRetries);
			allOk = allOk && flag;
			if (!flag)
			{
				Logger.Warning($"ModelFileHelper: failed to download {file2} after {maxRetries} retries");
			}
		}
		foreach (string file2 in optionalFiles)
		{
			if (!md5Dict.TryGetValue(file2, out string value2) || string.IsNullOrWhiteSpace(value2))
			{
				Logger.Warning("ModelFileHelper: md5 missing for optional " + file2 + ", skip");
				continue;
			}
			if (IsLocalFileValid(GetLocalModelPath(file2), value2))
			{
				Logger.Info("ModelFileHelper: optional " + file2 + " already valid");
				continue;
			}
			Logger.Info("ModelFileHelper: downloading optional " + file2);
			if (!(await DownloadWithRetryAsync(file2, value2, maxRetries)))
			{
				Logger.Warning($"ModelFileHelper: failed to download optional {file2} after {maxRetries} retries (non-fatal)");
			}
		}
		bool flag2 = allOk;
		return flag2 & await EnsurePluginPackageAsync(mlDevice, md5Dict, maxRetries);
	}

	private static async Task<bool> EnsurePluginPackageAsync(string mlDevice, Dictionary<string, string> md5Dict, int maxRetries)
	{
		(string, string, string)? pluginInfo = GetPluginInfo(mlDevice);
		if (!pluginInfo.HasValue)
		{
			return true;
		}
		var (zip, text, keyDll) = pluginInfo.Value;
		if (!md5Dict.TryGetValue(zip, out string md5) || string.IsNullOrWhiteSpace(md5))
		{
			Logger.Warning("ModelFileHelper: md5 missing for plugin " + zip + ", skip");
			return false;
		}
		string pluginDir = GetPluginDir(text);
		string marker = Path.Combine(pluginDir, ".pkg_md5_" + text);
		if (File.Exists(Path.Combine(pluginDir, keyDll)) && File.Exists(marker) && string.Equals(File.ReadAllText(marker).Trim(), md5, StringComparison.OrdinalIgnoreCase))
		{
			Logger.Info("ModelFileHelper: plugin " + zip + " already up to date");
			return true;
		}
		string relZip = "Immersive3D/plugins/" + zip;
		string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
		string localZip = Path.Combine(folderPath, "VITURE", "Immersive3D", "plugins", zip);
		int attempt = 0;
		while (attempt < maxRetries)
		{
			attempt++;
			try
			{
				if (await HttpRequestHelper.DownloadFile("https://static.viture.dev/external-file/Windows/" + zip, relZip, md5) && IsLocalFileValid(localZip, md5))
				{
					Directory.CreateDirectory(pluginDir);
					using (ZipArchive zipArchive = ZipFile.OpenRead(localZip))
					{
						foreach (ZipArchiveEntry entry in zipArchive.Entries)
						{
							if (!string.IsNullOrEmpty(entry.Name))
							{
								string text2 = Path.Combine(pluginDir, entry.FullName);
								string directoryName = Path.GetDirectoryName(text2);
								if (!string.IsNullOrEmpty(directoryName))
								{
									Directory.CreateDirectory(directoryName);
								}
								entry.ExtractToFile(text2, overwrite: true);
							}
						}
					}
					File.WriteAllText(marker, md5);
					if (File.Exists(Path.Combine(pluginDir, keyDll)))
					{
						Logger.Info("ModelFileHelper: plugin " + zip + " installed to " + pluginDir);
						return true;
					}
					Logger.Warning("ModelFileHelper: plugin " + zip + " extracted but " + keyDll + " missing");
				}
				Logger.Warning($"ModelFileHelper: plugin download attempt {attempt}/{maxRetries} failed for {zip}");
			}
			catch (Exception ex)
			{
				Logger.Warning($"ModelFileHelper: plugin attempt {attempt}/{maxRetries} error for {zip}: {ex.Message}");
			}
			if (attempt < maxRetries)
			{
				await Task.Delay(1000);
			}
		}
		return false;
	}

	private static async Task<Dictionary<string, string>?> FetchMd5ManifestAsync()
	{
		try
		{
			string value = await HttpRequestHelper.RequestJsonContent("https://static.viture.dev/external-file/Windows/immersive3d_models_md5.json");
			if (string.IsNullOrWhiteSpace(value))
			{
				return null;
			}
			return JsonConvert.DeserializeObject<Dictionary<string, string>>(value);
		}
		catch (Exception ex)
		{
			Logger.Warning("ModelFileHelper: parse md5 manifest failed: " + ex.Message);
			return null;
		}
	}

	private static bool IsLocalFileValid(string filePath, string expectedMd5)
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
			Logger.Warning("ModelFileHelper: md5 check failed for " + filePath + ": " + ex.Message);
			return false;
		}
	}

	private static async Task<bool> DownloadWithRetryAsync(string modelFile, string md5, int maxRetries)
	{
		int attempt = 0;
		while (attempt < maxRetries)
		{
			attempt++;
			try
			{
				if (await HttpRequestHelper.DownloadFile("https://static.viture.dev/external-file/Windows/" + modelFile, "Immersive3D/models/" + modelFile, md5) && IsLocalFileValid(GetLocalModelPath(modelFile), md5))
				{
					return true;
				}
				Logger.Warning($"ModelFileHelper: download attempt {attempt}/{maxRetries} verification failed for {modelFile}");
			}
			catch (Exception ex)
			{
				Logger.Warning($"ModelFileHelper: download attempt {attempt}/{maxRetries} failed for {modelFile}: {ex.Message}");
			}
			if (attempt < maxRetries)
			{
				await Task.Delay(1000);
			}
		}
		return false;
	}
}
