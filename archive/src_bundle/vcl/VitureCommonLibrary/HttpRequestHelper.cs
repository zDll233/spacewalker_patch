using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace VitureCommonLibrary;

public static class HttpRequestHelper
{
	private static bool downloading;

	private static bool IsPlaceApiRequest(string urlPath, bool getMethod)
	{
		if (getMethod)
		{
			return string.Equals(urlPath, "/place", StringComparison.OrdinalIgnoreCase);
		}
		return false;
	}

	private static void ApplyPlaceApiConfig(ref string baseUrl, Signature signature)
	{
		baseUrl = "https://static.viture.dev";
		signature.TAG = "VITURE";
		signature.AK = "ak_YeiQdfKefzOke0Zdf1gp";
		signature.SK = "sk_81ddk1dkfllnslOwqdkf";
	}

	private static string BuildAuthorizationHeader(Signature signature, string urlPath, string queryParam, string dt, bool getMethod, string jsonContent)
	{
		if (!getMethod && !string.IsNullOrWhiteSpace(jsonContent))
		{
			byte[] bytes = Encoding.UTF8.GetBytes(jsonContent);
			return signature.CreatePostSign(urlPath, dt, bytes);
		}
		return signature.CreateGetSign(urlPath + queryParam, dt);
	}

	public static async Task<string?> Request(string urlPath, string queryParam = "", string lang = "en-US", bool getMethod = true, string jsonContent = "")
	{
		string baseUrl = "https://cloud.viture.dev";
		if (string.IsNullOrWhiteSpace(lang))
		{
			lang = "en-US";
		}
		Signature signature = new Signature
		{
			TAG = "VITURE",
			AK = "ak_pk4qutY0wlxmvboFPefg",
			SK = "sk_7kembweYzxHqmpzhOukr"
		};
		if (IsPlaceApiRequest(urlPath, getMethod))
		{
			ApplyPlaceApiConfig(ref baseUrl, signature);
		}
		string text = DateTime.UtcNow.ToString("R");
		string value = BuildAuthorizationHeader(signature, urlPath, queryParam, text, getMethod, jsonContent);
		try
		{
			using HttpClient client = new HttpClient();
			client.Timeout = TimeSpan.FromSeconds(5.0);
			client.DefaultRequestHeaders.Add("Date", text);
			client.DefaultRequestHeaders.Add("Authorization", value);
			client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.3");
			if (!getMethod && !string.IsNullOrWhiteSpace(lang))
			{
				client.DefaultRequestHeaders.Add("Language", lang);
			}
			HttpResponseMessage httpResponseMessage = null;
			if (getMethod)
			{
				httpResponseMessage = await client.GetAsync(baseUrl + urlPath + queryParam);
			}
			else if (!string.IsNullOrWhiteSpace(jsonContent))
			{
				StringContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
				httpResponseMessage = await client.PostAsync(baseUrl + urlPath + queryParam, content);
			}
			if (httpResponseMessage != null && httpResponseMessage.IsSuccessStatusCode)
			{
				string text2 = await httpResponseMessage.Content.ReadAsStringAsync();
				Logger.Info("responseJson:" + text2);
				return text2;
			}
			Logger.Info($"Failed statusCode: {httpResponseMessage?.StatusCode}");
		}
		catch (Exception ex)
		{
			Logger.Error(ex.Message, ex.StackTrace);
		}
		return null;
	}

	public static async Task<string?> RequestJsonContent(string url)
	{
		using HttpClient client = new HttpClient();
		client.Timeout = TimeSpan.FromSeconds(5.0);
		client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.3");
		Stopwatch sw = Stopwatch.StartNew();
		try
		{
			Logger.Info("RequestJsonContent: GET " + url);
			HttpResponseMessage httpResponseMessage = await client.GetAsync(url);
			Logger.Info($"RequestJsonContent: response {(int)httpResponseMessage.StatusCode} ({httpResponseMessage.StatusCode}) in {sw.ElapsedMilliseconds} ms, url={url}");
			httpResponseMessage.EnsureSuccessStatusCode();
			string text = await httpResponseMessage.Content.ReadAsStringAsync();
			Logger.Info($"RequestJsonContent: ok {text.Length} chars, url={url}");
			return text;
		}
		catch (TaskCanceledException ex)
		{
			Logger.Error($"RequestJsonContent: timeout/canceled after {sw.ElapsedMilliseconds} ms, url={url}, msg={ex.Message}", ex.StackTrace);
		}
		catch (HttpRequestException ex2)
		{
			string text2 = ((ex2.InnerException != null) ? (" inner=[" + ex2.InnerException.GetType().Name + "] " + ex2.InnerException.Message) : "");
			Logger.Error($"RequestJsonContent: HTTP error after {sw.ElapsedMilliseconds} ms, url={url}, msg={ex2.Message}{text2}", ex2.StackTrace);
		}
		catch (JsonException ex3)
		{
			Logger.Error("RequestJsonContent: JSON parse error, url=" + url + ", msg=" + ex3.Message, ex3.StackTrace);
		}
		catch (Exception ex4)
		{
			Logger.Error($"RequestJsonContent: unexpected error [{ex4.GetType().Name}] after {sw.ElapsedMilliseconds} ms, url={url}, msg={ex4.Message}", ex4.StackTrace);
		}
		return string.Empty;
	}

	public static void LogEndpointDiagnostics(string url)
	{
		try
		{
			Uri uri = new Uri(url);
			Logger.Info($"NetDiag: host={uri.Host} scheme={uri.Scheme} port={uri.Port}");
			try
			{
				Stopwatch stopwatch = Stopwatch.StartNew();
				IPAddress[] hostAddresses = Dns.GetHostAddresses(uri.Host);
				Logger.Info(string.Format("NetDiag: DNS {0} -> [{1}] in {2} ms", uri.Host, string.Join(", ", hostAddresses.Select((IPAddress a) => a.ToString())), stopwatch.ElapsedMilliseconds));
			}
			catch (Exception ex)
			{
				Logger.Warning("NetDiag: DNS resolve FAILED for " + uri.Host + ": [" + ex.GetType().Name + "] " + ex.Message);
			}
			try
			{
				IWebProxy defaultWebProxy = WebRequest.DefaultWebProxy;
				if (defaultWebProxy == null)
				{
					Logger.Info("NetDiag: no system proxy (DefaultWebProxy=null)");
					return;
				}
				if (defaultWebProxy.IsBypassed(uri))
				{
					Logger.Info("NetDiag: proxy bypassed (direct) for " + uri.Host);
					return;
				}
				Uri proxy = defaultWebProxy.GetProxy(uri);
				Logger.Info("NetDiag: proxy for " + uri.Host + " -> " + ((proxy == null || proxy == uri) ? "(direct)" : proxy.ToString()));
			}
			catch (Exception ex2)
			{
				Logger.Warning("NetDiag: proxy probe failed: [" + ex2.GetType().Name + "] " + ex2.Message);
			}
		}
		catch (Exception ex3)
		{
			Logger.Warning("NetDiag: failed for url=" + url + ": [" + ex3.GetType().Name + "] " + ex3.Message);
		}
	}

	public static async Task<bool> DownloadFile(string url, string fileName, string checkSum = "")
	{
		string checkSum2 = checkSum;
		string url2 = url;
		downloading = true;
		string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
		string filePath = Path.Combine(folderPath, "VITURE", fileName);
		Stopwatch sw = Stopwatch.StartNew();
		Logger.Info("DownloadFile: begin url=" + url2 + " -> " + filePath + " expectedMd5=" + (string.IsNullOrWhiteSpace(checkSum2) ? "(none)" : checkSum2));
		try
		{
			await Task.Run(async delegate
			{
				string directoryName = Path.GetDirectoryName(filePath);
				if (!Directory.Exists(directoryName) && !string.IsNullOrWhiteSpace(directoryName))
				{
					Directory.CreateDirectory(directoryName);
				}
				bool flag = true;
				if (!string.IsNullOrWhiteSpace(checkSum2) && File.Exists(filePath))
				{
					string text3 = BitConverter.ToString(MD5.Create().ComputeHash(File.ReadAllBytes(filePath))).Replace("-", "").ToLowerInvariant();
					if (text3 != checkSum2)
					{
						Logger.Info("DownloadFile: existing file md5 mismatch (local=" + text3 + " expected=" + checkSum2 + "); deleting and re-downloading " + filePath);
						File.Delete(filePath);
					}
					else
					{
						flag = false;
						Logger.Info("DownloadFile: existing file md5 matches expected; skip download " + filePath);
					}
				}
				if (flag)
				{
					using HttpClient httpClient = new HttpClient();
					httpClient.Timeout = TimeSpan.FromMinutes(30.0);
					httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.3");
					Logger.Info("DownloadFile: GET " + url2);
					using (HttpResponseMessage response = await httpClient.GetAsync(url2))
					{
						long? contentLength = response.Content.Headers.ContentLength;
						string text4 = response.Content.Headers.ContentType?.ToString() ?? "(none)";
						Logger.Info(string.Format("DownloadFile: response {0} ({1}) Content-Length={2} Content-Type={3} after {4} ms, url={5}", (int)response.StatusCode, response.StatusCode, contentLength.HasValue ? contentLength.Value.ToString() : "unknown", text4, sw.ElapsedMilliseconds, url2));
						response.EnsureSuccessStatusCode();
						using Stream streamToReadFrom = await response.Content.ReadAsStreamAsync();
						using FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None, 81920, useAsync: true);
						byte[] buffer = new byte[81920];
						long totalRead = 0L;
						long lastLoggedBytes = 0L;
						Stopwatch lastLog = Stopwatch.StartNew();
						while (true)
						{
							int num;
							int read = (num = await streamToReadFrom.ReadAsync(buffer, 0, buffer.Length));
							if (num <= 0)
							{
								break;
							}
							await fileStream.WriteAsync(buffer, 0, read);
							totalRead += read;
							if (lastLog.ElapsedMilliseconds >= 1000 || totalRead - lastLoggedBytes >= 5242880)
							{
								Logger.Info("DownloadFile: progress " + FormatProgress(totalRead, contentLength, sw.Elapsed) + " url=" + url2);
								lastLoggedBytes = totalRead;
								lastLog.Restart();
							}
						}
						await fileStream.FlushAsync();
						Logger.Info("DownloadFile: progress " + FormatProgress(totalRead, contentLength, sw.Elapsed) + " (done) url=" + url2);
					}
					long num2 = (File.Exists(filePath) ? new FileInfo(filePath).Length : 0);
					Logger.Info($"DownloadFile: wrote {num2} bytes to {filePath} in {sw.ElapsedMilliseconds} ms");
					if (!string.IsNullOrWhiteSpace(checkSum2) && File.Exists(filePath))
					{
						using MD5 mD = MD5.Create();
						using FileStream inputStream = File.OpenRead(filePath);
						string text5 = BitConverter.ToString(mD.ComputeHash(inputStream)).Replace("-", "").ToLowerInvariant();
						if (string.Equals(text5, checkSum2, StringComparison.OrdinalIgnoreCase))
						{
							Logger.Info("DownloadFile: md5 verified OK for " + filePath);
						}
						else
						{
							Logger.Warning($"DownloadFile: md5 verify FAILED for {filePath} (local={text5} expected={checkSum2}, size={num2})");
						}
					}
				}
			});
			downloading = false;
			Logger.Info($"DownloadFile: success url={url2} ({sw.ElapsedMilliseconds} ms)");
			return true;
		}
		catch (TaskCanceledException ex)
		{
			Logger.Error($"DownloadFile: timeout/canceled after {sw.ElapsedMilliseconds} ms, url={url2}, target={filePath}, msg={ex.Message}", ex.StackTrace);
		}
		catch (HttpRequestException ex2)
		{
			string text = ((ex2.InnerException != null) ? (" inner=[" + ex2.InnerException.GetType().Name + "] " + ex2.InnerException.Message) : "");
			Logger.Error($"DownloadFile: HTTP error after {sw.ElapsedMilliseconds} ms, url={url2}, target={filePath}, msg={ex2.Message}{text}", ex2.StackTrace);
		}
		catch (IOException ex3)
		{
			Logger.Error($"DownloadFile: file IO error after {sw.ElapsedMilliseconds} ms, url={url2}, target={filePath}, msg={ex3.Message}", ex3.StackTrace);
		}
		catch (Exception ex4)
		{
			string text2 = ((ex4.InnerException != null) ? (" inner=[" + ex4.InnerException.GetType().Name + "] " + ex4.InnerException.Message) : "");
			Logger.Error($"DownloadFile: unexpected error [{ex4.GetType().Name}] after {sw.ElapsedMilliseconds} ms, url={url2}, target={filePath}, msg={ex4.Message}{text2}", ex4.StackTrace);
		}
		downloading = false;
		Logger.Info($"DownloadFile: failed url={url2} ({sw.ElapsedMilliseconds} ms)");
		return false;
	}

	private static string FormatProgress(long downloaded, long? total, TimeSpan elapsed)
	{
		double num = Math.Max(elapsed.TotalSeconds, 0.001);
		double num2 = (double)downloaded / num / 1048576.0;
		if (total.HasValue && total.Value > 0)
		{
			double num3 = (double)downloaded * 100.0 / (double)total.Value;
			return $"{FormatBytes(downloaded)}/{FormatBytes(total.Value)} ({num3:F1}%) @ {num2:F2} MB/s";
		}
		return $"{FormatBytes(downloaded)}/unknown @ {num2:F2} MB/s";
	}

	private static string FormatBytes(long bytes)
	{
		if (bytes >= 1073741824)
		{
			return $"{(double)bytes / 1073741824.0:F2} GB";
		}
		if (bytes >= 1048576)
		{
			return $"{(double)bytes / 1048576.0:F2} MB";
		}
		if (bytes >= 1024)
		{
			return $"{(double)bytes / 1024.0:F1} KB";
		}
		return $"{bytes} B";
	}
}
