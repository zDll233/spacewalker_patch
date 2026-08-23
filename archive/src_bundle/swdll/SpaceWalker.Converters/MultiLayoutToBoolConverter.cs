using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Avalonia.Data;
using Avalonia.Data.Converters;

namespace SpaceWalker.Converters;

public class MultiLayoutToBoolConverter : IMultiValueConverter
{
	public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
	{
		if (values.Count < 2)
		{
			return false;
		}
		object value = values.LastOrDefault();
		if (values.Take(values.Count - 1).Contains<object>(value))
		{
			return true;
		}
		return false;
	}

	public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
	{
		return BindingOperations.DoNothing;
	}
}
