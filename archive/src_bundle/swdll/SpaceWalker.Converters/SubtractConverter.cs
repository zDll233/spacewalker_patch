using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace SpaceWalker.Converters;

public class SubtractConverter : IValueConverter
{
	public static readonly SubtractConverter Instance = new SubtractConverter();

	public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
	{
		if (value is double num && parameter is string s && double.TryParse(s, out var result))
		{
			return num - result;
		}
		return value;
	}

	public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
	{
		throw new NotImplementedException();
	}
}
