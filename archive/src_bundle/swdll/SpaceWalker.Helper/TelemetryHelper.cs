using System;
using System.Globalization;
using System.Text.Json;
using Microsoft.Win32;
using PostHog;
using Sentry;
using VitureCommonLibrary;

namespace SpaceWalker.Helper;

public static class TelemetryHelper
{
	private static PostHogClient? _client;

	private static readonly string _windowsInstallSN;

	private static string _glassesSN;

	private static string _appVersion;

	private static bool? _isDisabledRegionCached;

	private const string PlacePath = "/place";

	private const string CryptographyRegistryPath = "SOFTWARE\\Microsoft\\Cryptography";

	private const string MachineGuidValueName = "MachineGuid";

	static TelemetryHelper()
	{
		_client = null;
		_glassesSN = "null";
		_appVersion = string.Empty;
		_isDisabledRegionCached = null;
		_windowsInstallSN = GetWindowsInstallSn();
	}

	public static void InitPosthog(string apiKey)
	{
		if (!IsDisabledRegion() && !string.IsNullOrWhiteSpace(apiKey))
		{
			_client = new PostHogClient(new PostHogOptions
			{
				ProjectToken = apiKey,
				HostUrl = new Uri("https://posthog.viture.dev")
			});
		}
	}

	public static void InitSentry(string dsn)
	{
		string dsn2 = dsn;
		if (!IsDisabledRegion() && !string.IsNullOrWhiteSpace(dsn2))
		{
			SentrySdk.Init(delegate(SentryOptions o)
			{
				o.Dsn = dsn2;
			});
		}
	}

	public static void Capture(string content = "")
	{
		_client?.Capture(_windowsInstallSN ?? "", content);
	}

	public static void SetGlassesSN(string sn)
	{
		_glassesSN = sn;
	}

	public static void SetAppVersion(string version)
	{
		_appVersion = version;
	}

	private static bool IsDisabledRegion()
	{
		if (_isDisabledRegionCached.HasValue)
		{
			return _isDisabledRegionCached.Value;
		}
		_isDisabledRegionCached = ComputeIsDisabledRegion();
		return _isDisabledRegionCached.Value;
	}

	private static bool ComputeIsDisabledRegion()
	{
		CultureInfo currentUICulture = CultureInfo.CurrentUICulture;
		if (currentUICulture.Name.Equals("zh-CN", StringComparison.OrdinalIgnoreCase) || currentUICulture.Name.StartsWith("zh-Hans", StringComparison.OrdinalIgnoreCase))
		{
			Logger.Info("Telemetry disabled: system UI culture is " + currentUICulture.Name);
			return true;
		}
		try
		{
			string countryFromPlaceApi = GetCountryFromPlaceApi();
			if (countryFromPlaceApi == null)
			{
				Logger.Info("Telemetry disabled: /place API returned null country");
				return true;
			}
			if (string.Equals(countryFromPlaceApi, "CN", StringComparison.OrdinalIgnoreCase) || string.Equals(countryFromPlaceApi, "China", StringComparison.OrdinalIgnoreCase))
			{
				Logger.Info("Telemetry disabled: /place API reported country=" + countryFromPlaceApi);
				return true;
			}
		}
		catch (Exception ex)
		{
			Logger.Warning("Telemetry disabled: /place API failed (" + ex.Message + ")");
			return true;
		}
		Logger.Info("Telemetry enabled: region check passed");
		return false;
	}

	private static string? GetCountryFromPlaceApi()
	{
		string result = HttpRequestHelper.Request("/place").GetAwaiter().GetResult();
		if (string.IsNullOrWhiteSpace(result))
		{
			return null;
		}
		return JsonSerializer.Deserialize<_003CTelemetryHelper_003EF735A824493BCA37BF56E031EE042154DFF89FABD89AA5566F13A902A4DC4FC04__PlaceResponse>(result)?.Country?.Name;
	}

	private static string GetWindowsInstallSn()
	{
		try
		{
			string text = ReadMachineGuid(RegistryView.Registry64) ?? ReadMachineGuid(RegistryView.Registry32);
			if (!string.IsNullOrWhiteSpace(text))
			{
				return text;
			}
		}
		catch (Exception ex)
		{
			Logger.Warning("Failed to read MachineGuid from registry: " + ex.Message);
		}
		return Environment.MachineName;
	}

	private static string ReadMachineGuid(RegistryView registryView)
	{
		using RegistryKey registryKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, registryView);
		using RegistryKey registryKey2 = registryKey.OpenSubKey("SOFTWARE\\Microsoft\\Cryptography");
		return registryKey2?.GetValue("MachineGuid") as string;
	}
}
