using System;
using System.Globalization;
using Avalonia.Data;
using Avalonia.Data.Converters;
using SpaceWalker.ViewModels;

namespace SpaceWalker.Converters;

public class VitureDeviceTypeVisibilityConverter : IValueConverter
{
	public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
	{
		if (!(value is VitureDeviceType value2))
		{
			return false;
		}
		bool flag = Enum.GetName(value2)?.StartsWith("R6") ?? false;
		bool flag2 = Enum.GetName(value2)?.StartsWith("N6") ?? false;
		string text = parameter?.ToString() ?? "NonR6";
		if (text.Equals("R6", StringComparison.OrdinalIgnoreCase))
		{
			return flag;
		}
		if (text.Equals("NonR6", StringComparison.OrdinalIgnoreCase))
		{
			return !flag;
		}
		if (text.Equals("PreP6", StringComparison.OrdinalIgnoreCase))
		{
			return flag2;
		}
		if (text.Equals("P6Series", StringComparison.OrdinalIgnoreCase))
		{
			return !flag && !flag2;
		}
		return !flag;
	}

	public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
	{
		return BindingOperations.DoNothing;
	}
}
