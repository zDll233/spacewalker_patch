using System.Collections.Generic;
using System.Linq;

namespace VitureCommonLibrary;

public static class DisplayTargetTokensExtensions
{
	public static IEnumerable<DisplayConfig> GetDisplays(this IEnumerable<string> paths, IEnumerable<DisplayConfig> dcs, bool distinct = true)
	{
		IEnumerable<DisplayConfig> dcs2 = dcs;
		if (distinct)
		{
			paths = paths.Distinct();
		}
		return paths.Select((string x) => dcs2.First((DisplayConfig y) => y.GetDevicePath() == x)).ToArray();
	}
}
