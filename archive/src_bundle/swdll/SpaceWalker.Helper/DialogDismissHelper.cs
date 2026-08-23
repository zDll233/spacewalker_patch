using System.Linq;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Labs.Controls;
using Avalonia.VisualTree;

namespace SpaceWalker.Helper;

internal static class DialogDismissHelper
{
	public static async Task<ContentDialogResult> ShowWithScrimDismissAsync(this ContentDialog dialog, Window? owner = null)
	{
		ContentDialog dialog2 = dialog;
		dialog2.AddHandler(InputElement.PointerPressed, OnPointerPressed, RoutingStrategies.Tunnel);
		try
		{
			return (owner == null) ? (await dialog2.ShowAsync()) : (await dialog2.ShowAsync(owner));
		}
		finally
		{
			dialog2.RemoveHandler(InputElement.PointerPressed, OnPointerPressed);
		}
		void OnPointerPressed(object? s, PointerPressedEventArgs args)
		{
			if (!(args.Source is Visual visual) || !visual.GetSelfAndVisualAncestors().Any(delegate(Visual a)
			{
				if (a is Border)
				{
					if (a.Name == "BackgroundElement")
					{
						goto IL_0024;
					}
				}
				else if (a is PopupRoot)
				{
					goto IL_0024;
				}
				return false;
				IL_0024:
				return true;
			}))
			{
				dialog2.Hide();
			}
		}
	}
}
