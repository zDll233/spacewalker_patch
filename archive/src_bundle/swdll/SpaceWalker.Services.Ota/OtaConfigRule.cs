using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using VitureCommonLibrary;

namespace SpaceWalker.Services.Ota;

public static class OtaConfigRule
{
	public static async Task<OtaControlConfig?> RequestJsonContent(string url)
	{
		using HttpClient client = new HttpClient();
		client.Timeout = TimeSpan.FromSeconds(5.0);
		client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.3");
		try
		{
			HttpResponseMessage obj = await client.GetAsync(url);
			obj.EnsureSuccessStatusCode();
			OtaControlConfig otaControlConfig = JsonSerializer.Deserialize<OtaControlConfig>(await obj.Content.ReadAsStringAsync());
			if (otaControlConfig != null)
			{
				return otaControlConfig;
			}
			Logger.Error("JSON Deserialize get null");
		}
		catch (TaskCanceledException ex)
		{
			Logger.Error("HTTP Request Timeout: " + ex.Message);
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
}
