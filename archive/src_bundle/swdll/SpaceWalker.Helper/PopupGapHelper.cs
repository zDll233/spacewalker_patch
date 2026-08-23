using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Primitives.PopupPositioning;

namespace SpaceWalker.Helper;

public static class PopupGapHelper
{
	public static readonly AttachedProperty<double> SymmetricVerticalGapProperty;

	static PopupGapHelper()
	{
		SymmetricVerticalGapProperty = AvaloniaProperty.RegisterAttached<Popup, double>("SymmetricVerticalGap", typeof(PopupGapHelper), double.NaN);
		SymmetricVerticalGapProperty.Changed.AddClassHandler(delegate(Popup popup, AvaloniaPropertyChangedEventArgs _)
		{
			Popup popup2 = popup;
			popup2.CustomPopupPlacementCallback = delegate(CustomPopupPlacement placement)
			{
				Place(popup2, placement);
			};
			popup2.Placement = PlacementMode.Custom;
		});
	}

	public static void SetSymmetricVerticalGap(Popup element, double value)
	{
		element.SetValue(SymmetricVerticalGapProperty, value);
	}

	public static double GetSymmetricVerticalGap(Popup element)
	{
		return element.GetValue(SymmetricVerticalGapProperty);
	}

	private static void Place(Popup popup, CustomPopupPlacement placement)
	{
		double num = GetSymmetricVerticalGap(popup);
		if (double.IsNaN(num))
		{
			num = 0.0;
		}
		Rect anchorRectangle = placement.AnchorRectangle;
		placement.AnchorRectangle = new Rect(anchorRectangle.X, anchorRectangle.Y - num, anchorRectangle.Width, anchorRectangle.Height + 2.0 * num);
		placement.Anchor = PopupAnchor.Bottom;
		placement.Gravity = PopupGravity.Bottom;
		placement.Offset = placement.Offset.WithY(0.0);
	}
}
