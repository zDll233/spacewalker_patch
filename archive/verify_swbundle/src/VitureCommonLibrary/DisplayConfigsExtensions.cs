using System;
using Windows.Win32.Devices.Display;
using Windows.Win32.Foundation;

namespace VitureCommonLibrary;

public static class DisplayConfigsExtensions
{
	public static DisplayTargetToken GetTargetToken(this DisplayConfig path)
	{
		DisplayTargetToken result = default(DisplayTargetToken);
		result.AdapterLuid = path.TargetInfo.AdapterId;
		result.TargetId = path.TargetInfo.Id;
		return result;
	}

	public static string GetDevicePath(this DisplayConfig dc)
	{
		return dc.DeviceInfo.GetTargetDeviceName()?.monitorDevicePath.ToString() ?? string.Empty;
	}

	public static string GetDeviceName(this DisplayConfig dc)
	{
		return dc.DeviceInfo.GetTargetDeviceName()?.monitorFriendlyDeviceName.ToString() ?? string.Empty;
	}

	public static ulong ToUInt64(this LUID luid)
	{
		return (ulong)(((long)luid.HighPart << 32) | luid.LowPart);
	}

	public static LUID ToLuid(this ulong value)
	{
		LUID result = default(LUID);
		result.HighPart = (int)(value >> 32);
		result.LowPart = (uint)(value & 0xFFFFFFFFu);
		return result;
	}

	public static double ToDouble(this DISPLAYCONFIG_RATIONAL value)
	{
		if (value.Denominator == 0)
		{
			return double.PositiveInfinity;
		}
		return (double)value.Numerator / (double)value.Denominator;
	}

	public static DISPLAYCONFIG_RATIONAL ToDisplayConfigRational(this double value)
	{
		DISPLAYCONFIG_RATIONAL result;
		if (double.IsNaN(value) || double.IsInfinity(value))
		{
			result = default(DISPLAYCONFIG_RATIONAL);
			result.Numerator = 0u;
			result.Denominator = 0u;
			return result;
		}
		if (value <= 0.0)
		{
			result = default(DISPLAYCONFIG_RATIONAL);
			result.Numerator = 0u;
			result.Denominator = 1u;
			return result;
		}
		if (value <= 4294967295.0 && value.Equals(Math.Floor(value)))
		{
			result = default(DISPLAYCONFIG_RATIONAL);
			result.Numerator = (uint)value;
			result.Denominator = 1u;
			return result;
		}
		double num = value;
		ulong num2 = 0uL;
		ulong num3 = 1uL;
		ulong num4 = 1uL;
		ulong num5 = 0uL;
		for (int i = 0; i < 64; i++)
		{
			ulong num6 = (ulong)num;
			double num7 = num - (double)num6;
			if ((num4 != 0L && num6 > (uint.MaxValue - num2) / num4) || (num5 != 0L && num6 > (uint.MaxValue - num3) / num5))
			{
				break;
			}
			ulong num8 = num6 * num4 + num2;
			ulong num9 = num6 * num5 + num3;
			num2 = num4;
			num3 = num5;
			num4 = num8;
			num5 = num9;
			if (num7 < 1E-09)
			{
				break;
			}
			num = 1.0 / num7;
		}
		if (num5 == 0L)
		{
			num4 = Math.Min((ulong)Math.Round(value), 4294967295uL);
			num5 = 1uL;
		}
		num2 = num4;
		num3 = num5;
		while (num3 != 0L)
		{
			ulong num10 = num3;
			num3 = num2 % num3;
			num2 = num10;
		}
		result = default(DISPLAYCONFIG_RATIONAL);
		result.Numerator = (uint)(num4 / num2);
		result.Denominator = (uint)(num5 / num2);
		return result;
	}
}
