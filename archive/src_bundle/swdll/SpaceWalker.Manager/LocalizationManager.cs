using System.Globalization;
using System.Resources;

namespace SpaceWalker.Manager;

public static class LocalizationManager
{
	private static ResourceManager _resourceManager = new ResourceManager("SpaceWalker.Resources.Resource", typeof(LocalizationManager).Assembly);

	public static string GetString(string key)
	{
		string @string = _resourceManager.GetString(key, CultureInfo.CurrentUICulture);
		if (@string != null)
		{
			return @string;
		}
		return string.Empty;
	}
}
