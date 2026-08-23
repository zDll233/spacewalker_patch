using System;
using System.Globalization;
using Avalonia.Data.Converters;
using SpaceWalker.Assets.Languages;

namespace SpaceWalker.Converters;

public class EnumToLocalizedStringConverter : IValueConverter
{
	public static readonly EnumToLocalizedStringConverter Instance = new EnumToLocalizedStringConverter();

	public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
	{
		if (!(value is Enum @enum))
		{
			return string.Empty;
		}
		string text = $"{@enum.GetType().Name}_{@enum}";
		return Resources.ResourceManager.GetString(text, culture) ?? text;
	}

	public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
	{
		throw new NotImplementedException();
	}
}
