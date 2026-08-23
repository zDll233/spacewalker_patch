using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace VitureCommonLibrary;

public class FirmwareInfoConverter : JsonConverter
{
	public override bool CanConvert(Type objectType)
	{
		return objectType == typeof(List<FirmwareInfo>);
	}

	public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
	{
		try
		{
			JToken jToken = JToken.Load(reader);
			if (jToken.Type == JTokenType.Array)
			{
				return jToken.ToObject<List<FirmwareInfo>>(serializer) ?? new List<FirmwareInfo>();
			}
			if (jToken.Type == JTokenType.Object)
			{
				FirmwareInfo firmwareInfo = jToken.ToObject<FirmwareInfo>(serializer);
				if (firmwareInfo != null && !IsEmptyFirmwareInfo(firmwareInfo))
				{
					return new List<FirmwareInfo> { firmwareInfo };
				}
			}
			return new List<FirmwareInfo>();
		}
		catch (Exception)
		{
			return new List<FirmwareInfo>();
		}
	}

	public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
	{
		serializer.Serialize(writer, value);
	}

	private bool IsEmptyFirmwareInfo(FirmwareInfo info)
	{
		if (string.IsNullOrEmpty(info.Checksum) && string.IsNullOrEmpty(info.PkgName) && string.IsNullOrEmpty(info.Url))
		{
			return string.IsNullOrEmpty(info.VerName);
		}
		return false;
	}
}
