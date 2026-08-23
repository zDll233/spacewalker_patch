using System;
using System.Globalization;
using Avalonia.Data;
using Avalonia.Data.Converters;

namespace SpaceWalker.Converters;

public class EqualsToBooleanConverter : IValueConverter
{
	public static EqualsToBooleanConverter Instance { get; } = new EqualsToBooleanConverter();


	public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
	{
		if (value == null || parameter == null)
		{
			return false;
		}
		try
		{
			object obj = System.Convert.ChangeType(parameter, value.GetType());
			return value.Equals(obj);
		}
		catch
		{
			return value.Equals(parameter);
		}
	}

	public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
	{
		bool flag = default(bool);
		int num;
		if (value is bool)
		{
			flag = (bool)value;
			num = 1;
		}
		else
		{
			num = 0;
		}
		if (((uint)num & (flag ? 1u : 0u)) != 0)
		{
			try
			{
				return System.Convert.ChangeType(parameter, targetType);
			}
			catch
			{
				return parameter;
			}
		}
		return BindingOperations.DoNothing;
	}
}
