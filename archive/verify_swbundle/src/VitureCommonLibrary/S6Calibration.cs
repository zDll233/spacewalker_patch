using System;
using System.Collections.Generic;
using System.Text;

namespace VitureCommonLibrary;

public sealed class S6Calibration
{
	public string Sn { get; set; } = string.Empty;


	public string FirmwareVersion { get; set; } = string.Empty;


	public DateTime ReadAtUtc { get; set; } = DateTime.UtcNow;


	public Dictionary<S6CalibSection, byte[]> Buffers { get; } = new Dictionary<S6CalibSection, byte[]>();


	public S6DisplayOpticalParam? DisplayOptical { get; set; }

	public bool IsComplete()
	{
		foreach (S6CalibSection value in Enum.GetValues(typeof(S6CalibSection)))
		{
			if (!Buffers.ContainsKey(value) || Buffers[value].Length == 0)
			{
				return false;
			}
		}
		return DisplayOptical != null;
	}

	public bool IsEssentialComplete()
	{
		if (DisplayOptical == null)
		{
			return false;
		}
		if (HasSection(S6CalibSection.ImuCalibration) && HasSection(S6CalibSection.MagCalibration))
		{
			return HasSection(S6CalibSection.ImuGyroTempDrift);
		}
		return false;
	}

	private bool HasSection(S6CalibSection sec)
	{
		if (Buffers.TryGetValue(sec, out byte[] value) && value != null)
		{
			return value.Length != 0;
		}
		return false;
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append($"S6Calibration sn={Sn}, fw={FirmwareVersion}, readAt={ReadAtUtc:o}, sections=");
		foreach (KeyValuePair<S6CalibSection, byte[]> buffer in Buffers)
		{
			stringBuilder.Append($"{buffer.Key}({buffer.Value.Length}B) ");
		}
		return stringBuilder.ToString().TrimEnd(Array.Empty<char>());
	}
}
