using System;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using VitureCommonLibrary;

namespace SpaceWalker.Helper;

public static class FeedbackHelper
{
	public static async Task Request(string email = "UnKnown", string issues = "Please view log files")
	{
		string baseUrl = "https://cloud.viture.dev";
		Signature obj = new Signature
		{
			TAG = "VITURE",
			AK = "ak_pk4qutY0wlxmvboFPefg",
			SK = "sk_7kembweYzxHqmpzhOukr"
		};
		string path = "/api/v1/system/feedback?source=2";
		string value = DateTime.UtcNow.ToString("R");
		string value2 = obj.CreateAuth();
		using HttpClient client = new HttpClient();
		string glassesSN = GlassesDeviceManager.Instance.GlassesSN;
		client.DefaultRequestHeaders.Add("Date", value);
		client.DefaultRequestHeaders.Add("SN", string.IsNullOrWhiteSpace(glassesSN) ? "unknown" : glassesSN);
		client.DefaultRequestHeaders.Add("Authorization", value2);
		client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.3");
		MultipartFormDataContent content = new MultipartFormDataContent
		{
			{
				new StringContent(email),
				"email"
			},
			{
				new StringContent(issues),
				"issues"
			}
		};
		string text = await PackageLogFile();
		long length = new FileInfo(text).Length;
		Logger.Info($"Feedback upload zip: {text} size={length} bytes");
		StreamContent streamContent = new StreamContent(File.OpenRead(text));
		streamContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/octet-stream");
		content.Add(streamContent, "file", Path.GetFileName(text));
		try
		{
			HttpResponseMessage response = await client.PostAsync(baseUrl + path, content);
			if (response.IsSuccessStatusCode)
			{
				ResponseMsg responseMsg = JsonSerializer.Deserialize<ResponseMsg>(await response.Content.ReadAsStringAsync());
				if (responseMsg != null && responseMsg.ErrNum == 0)
				{
					Logger.Info($"Success statusCode: {response.StatusCode}");
				}
				else
				{
					Logger.Info("Response Error: " + responseMsg?.ErrMsg);
				}
			}
			else
			{
				Logger.Info($"Failed statusCode: {response.StatusCode}");
			}
		}
		catch (Exception ex)
		{
			string text2 = FlattenException(ex);
			Logger.Error("Feedback submit failed: " + text2, ex.StackTrace ?? string.Empty);
			throw new Exception(text2, ex);
		}
		finally
		{
			content.Dispose();
		}
	}

	private static string FlattenException(Exception ex)
	{
		StringBuilder stringBuilder = new StringBuilder();
		for (Exception ex2 = ex; ex2 != null; ex2 = ex2.InnerException)
		{
			if (stringBuilder.Length > 0)
			{
				stringBuilder.Append(" -> ");
			}
			stringBuilder.Append(ex2.GetType().Name).Append(": ").Append(ex2.Message);
		}
		return stringBuilder.ToString();
	}

	private static Task<string> PackageLogFile()
	{
		return Task.Run(delegate
		{
			string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "VITURE", "SpaceWalker");
			string text = Path.Combine(path, "upload.zip");
			if (File.Exists(text))
			{
				File.Delete(text);
			}
			string path2 = Path.Combine(path, "logs");
			string[] array = new string[2] { "SpaceWalker.log", "SpaceWalker.Unity.log" };
			using ZipArchive archive = ZipFile.Open(text, ZipArchiveMode.Create);
			string[] array2 = array;
			foreach (string text2 in array2)
			{
				string text3 = Path.Combine(path2, text2);
				if (File.Exists(text3))
				{
					AddFileToZip(archive, text3, text2);
				}
			}
			return text;
		});
	}

	private static void AddFileToZip(ZipArchive archive, string filePath, string entryName)
	{
		try
		{
			ZipArchiveEntry zipArchiveEntry = archive.CreateEntry(entryName, CompressionLevel.Optimal);
			zipArchiveEntry.LastWriteTime = File.GetLastWriteTime(filePath);
			using FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
			using Stream destination = zipArchiveEntry.Open();
			fileStream.CopyTo(destination);
		}
		catch (Exception ex)
		{
			Logger.Warning("FeedbackHelper: skipped file '" + filePath + "' during zip: " + ex.Message);
		}
	}
}
