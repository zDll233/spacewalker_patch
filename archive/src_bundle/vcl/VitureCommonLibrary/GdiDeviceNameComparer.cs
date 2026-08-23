using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace VitureCommonLibrary;

public class GdiDeviceNameComparer : IComparer<string?>
{
	private static readonly Regex Re = new Regex("DISPLAY(\\d+)", RegexOptions.IgnoreCase);

	public static GdiDeviceNameComparer Instance { get; } = new GdiDeviceNameComparer();


	public int Compare(string? x, string? y)
	{
		if (x == null && y == null)
		{
			return 0;
		}
		if (x == null)
		{
			return -1;
		}
		if (y == null)
		{
			return 1;
		}
		return ExtractNum(x).CompareTo(ExtractNum(y));
	}

	private int ExtractNum(string s)
	{
		Match match = Re.Match(s);
		if (!match.Success || !int.TryParse(match.Groups[1].Value, out var result))
		{
			return int.MaxValue;
		}
		return result;
	}
}
