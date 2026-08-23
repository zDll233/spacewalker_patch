using Avalonia;
using Avalonia.Controls;

namespace SpaceWalker.Controls;

public class IconLabel : ContentControl
{
	public static readonly StyledProperty<object?> IconProperty = AvaloniaProperty.Register<IconLabel, object>("Icon");

	public static readonly StyledProperty<double> SpacingProperty = AvaloniaProperty.Register<IconLabel, double>("Spacing", 8.0);

	public object? Icon
	{
		get
		{
			return GetValue(IconProperty);
		}
		set
		{
			SetValue(IconProperty, value);
		}
	}

	public double Spacing
	{
		get
		{
			return GetValue(SpacingProperty);
		}
		set
		{
			SetValue(SpacingProperty, value);
		}
	}
}
