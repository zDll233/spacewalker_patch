using System;
using System.Runtime.InteropServices;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Platform;

namespace SpaceWalker.Helper;

public static class PopupCornerHelper
{
	public static readonly AttachedProperty<bool> RoundCornersProperty;

	static PopupCornerHelper()
	{
		RoundCornersProperty = AvaloniaProperty.RegisterAttached<TopLevel, bool>("RoundCorners", typeof(PopupCornerHelper), defaultValue: false);
		RoundCornersProperty.Changed.AddClassHandler(delegate(TopLevel top, AvaloniaPropertyChangedEventArgs e)
		{
			object newValue = e.NewValue;
			if (newValue is bool && (bool)newValue)
			{
				Apply(top);
			}
		});
	}

	public static void SetRoundCorners(TopLevel element, bool value)
	{
		element.SetValue(RoundCornersProperty, value);
	}

	public static bool GetRoundCorners(TopLevel element)
	{
		return element.GetValue(RoundCornersProperty);
	}

	private static void Apply(TopLevel top)
	{
		if (OperatingSystem.IsWindows())
		{
			IPlatformHandle platformHandle = top.TryGetPlatformHandle();
			if (platformHandle != null)
			{
				int value = 2;
				DwmSetWindowAttribute(platformHandle.Handle, 33, ref value, 4);
			}
		}
	}

	[DllImport("dwmapi.dll")]
	private static extern int DwmSetWindowAttribute(nint hwnd, int attr, ref int value, int size);
}
