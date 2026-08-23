using System;
using System.Collections.Generic;
using System.Management;
using System.Text.RegularExpressions;

namespace VitureCommonLibrary.Helper;

public static class MlDeviceHelper
{
	private static Dictionary<string, string> computeCapabilityTable = new Dictionary<string, string>
	{
		{ "5090", "cuda_12.0" },
		{ "5080", "cuda_12.0" },
		{ "5070", "cuda_12.0" },
		{ "5060", "cuda_12.0" },
		{ "4090", "cuda_8.9" },
		{ "4080", "cuda_8.9" },
		{ "4070", "cuda_8.9" },
		{ "4060", "cuda_8.9" },
		{ "4050", "cuda_8.9" },
		{ "3090", "cuda_8.6" },
		{ "3080", "cuda_8.6" },
		{ "3070", "cuda_8.6" },
		{ "3060", "cuda_8.6" },
		{ "2050", "cuda_8.6" },
		{ "2090", "cuda_7.5" },
		{ "2080", "cuda_7.5" },
		{ "2070", "cuda_7.5" },
		{ "2060", "cuda_7.5" }
	};

	private static readonly HashSet<string> highEnd90HzGpus = new HashSet<string> { "3080", "3090", "4060", "4070", "4080", "4090", "5070", "5080", "5090" };

	private static bool? _allow90Hz3D;

	private static string CheckNvGpu()
	{
		try
		{
			foreach (ManagementObject item in new ManagementObjectSearcher("SELECT * FROM Win32_VideoController").Get())
			{
				string text = item["Name"]?.ToString();
				string text2 = item["Description"]?.ToString();
				if (text == null || text2 == null || (!text.Contains("NVIDIA") && !text2.Contains("NVIDIA")))
				{
					continue;
				}
				Match match = Regex.Match(text, "\\d+");
				Match match2 = Regex.Match(text2, "\\d+");
				if (match.Success || match2.Success)
				{
					string key = (match.Success ? match.Value : match2.Value);
					if (computeCapabilityTable.ContainsKey(key))
					{
						return computeCapabilityTable[key];
					}
				}
			}
		}
		catch (Exception ex)
		{
			Logger.Warning("Get GPU Info Failed: " + ex.Message);
		}
		return string.Empty;
	}

	public static bool SupportsCnnToggle(string mlDevice)
	{
		if (string.IsNullOrWhiteSpace(mlDevice))
		{
			return false;
		}
		if (!(mlDevice == "dml"))
		{
			return mlDevice.StartsWith("cuda", StringComparison.OrdinalIgnoreCase);
		}
		return true;
	}

	public static bool Allow90Hz3D()
	{
		if (_allow90Hz3D.HasValue)
		{
			return _allow90Hz3D.Value;
		}
		bool flag = GetMlDevice().StartsWith("cuda", StringComparison.OrdinalIgnoreCase) && IsHighEndNvidiaGpu();
		_allow90Hz3D = flag;
		Logger.Info($"Allow90Hz3D = {flag}");
		return flag;
	}

	private static bool IsHighEndNvidiaGpu()
	{
		try
		{
			foreach (ManagementObject item in new ManagementObjectSearcher("SELECT * FROM Win32_VideoController").Get())
			{
				string text = item["Name"]?.ToString();
				string text2 = item["Description"]?.ToString();
				if ((text != null && text.Contains("NVIDIA")) || (text2 != null && text2.Contains("NVIDIA")))
				{
					Match match = Regex.Match(text ?? string.Empty, "\\d{4}");
					if (!match.Success)
					{
						match = Regex.Match(text2 ?? string.Empty, "\\d{4}");
					}
					if (match.Success && highEnd90HzGpus.Contains(match.Value))
					{
						return true;
					}
				}
			}
		}
		catch (Exception ex)
		{
			Logger.Warning("IsHighEndNvidiaGpu failed: " + ex.Message);
		}
		return false;
	}

	public static string GetMlDevice()
	{
		string text = CheckCPUName();
		if (!string.IsNullOrWhiteSpace(text))
		{
			return text;
		}
		string text2 = CheckNvGpu();
		if (!string.IsNullOrWhiteSpace(text2))
		{
			return text2;
		}
		return "dml";
	}

	public static string CheckCPUName()
	{
		string result = string.Empty;
		try
		{
			foreach (ManagementObject item in new ManagementObjectSearcher("SELECT * FROM Win32_Processor").Get())
			{
				if (!(item["Name"] is string text) || string.IsNullOrWhiteSpace(text))
				{
					continue;
				}
				if (text.Contains("Snapdragon"))
				{
					result = "qnn_v73";
					if (text.ToUpper().Contains("X2") || text.ToUpper().Contains("NEXT"))
					{
						result = "qnn_v81";
					}
					break;
				}
				if (!text.Contains("Intel"))
				{
					continue;
				}
				if (text.Contains("Ultra"))
				{
					if (new Regex("Intel.*Ultra.*\\s[12][0-9]{2}", RegexOptions.Compiled).IsMatch(text))
					{
						result = "vino_cnn";
					}
					if (new Regex("Intel.*Ultra.*\\s2[0-9]{2}V", RegexOptions.Compiled).IsMatch(text))
					{
						result = "vino_lnl";
					}
					if (new Regex("Intel.*Ultra.*\\s3[0-9]{2}", RegexOptions.Compiled).IsMatch(text))
					{
						result = "vino_ptl";
					}
				}
				if (new Regex("Intel.*G3.*", RegexOptions.Compiled).IsMatch(text))
				{
					result = "vino_ptl";
				}
				break;
			}
		}
		catch (Exception ex)
		{
			Logger.Warning("Get CPU Info Failed: " + ex.Message);
		}
		return result;
	}
}
