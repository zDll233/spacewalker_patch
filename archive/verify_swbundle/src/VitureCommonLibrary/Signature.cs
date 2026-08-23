using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace VitureCommonLibrary;

public class Signature
{
	public string TAG { get; set; } = string.Empty;


	public string AK { get; set; } = string.Empty;


	public string SK { get; set; } = string.Empty;


	public string Do(string key, string method, string path, byte[] body, string date, Dictionary<string, string> _params)
	{
		string text = "";
		if (body != null && body.Length != 0)
		{
			using MD5 mD = MD5.Create();
			text = ToHexString(mD.ComputeHash(body));
		}
		string text2 = "";
		if (_params != null)
		{
			List<string> values = (from p in _params.OrderBy<KeyValuePair<string, string>, string>((KeyValuePair<string, string> p) => p.Key).ToList()
				select p.Key + "=" + p.Value).ToList();
			text2 = string.Join("&", values);
		}
		string value = string.Join("\n", method, path, text, date, text2);
		return HmacSha1(key, value);
	}

	private string HmacSha1(string key, string value)
	{
		using HMACSHA1 hMACSHA = new HMACSHA1(Encoding.UTF8.GetBytes(key));
		return ToHexString(hMACSHA.ComputeHash(Encoding.UTF8.GetBytes(value)));
	}

	private string ToHexString(byte[] bytes)
	{
		return BitConverter.ToString(bytes).Replace("-", "").ToLower();
	}

	public string CreateGetSign(string url, string dt, Dictionary<string, string> _params)
	{
		string text = Do(SK, "GET", url, null, dt, _params);
		return TAG + " " + AK + " " + text;
	}

	public string CreatePostSign(string url, string dt, byte[] body)
	{
		string text = Do(SK, "POST", url, body, dt, null);
		return TAG + " " + AK + " " + text;
	}

	public string CreateGetSign(string url, string dt)
	{
		string text = Do(SK, "GET", url, null, dt, null);
		return TAG + " " + AK + " " + text;
	}

	public string CreateAuth()
	{
		return TAG + " " + AK;
	}
}
