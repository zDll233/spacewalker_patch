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
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Invalid comparison between Unknown and I4
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Invalid comparison between Unknown and I4
		try
		{
			JToken val = JToken.Load(reader);
			if ((int)val.Type == 2)
			{
				return val.ToObject<List<FirmwareInfo>>(serializer) ?? new List<FirmwareInfo>();
			}
			if ((int)val.Type == 1)
			{
				FirmwareInfo firmwareInfo = val.ToObject<FirmwareInfo>(serializer);
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
