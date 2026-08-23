using System;
using Avalonia;
using Avalonia.Labs.Notifications;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia.Threading;

namespace SpaceWalker;

public class Toast
{
	private static INativeNotification? _currentNotification;

	public static void ShowNotification(string notifyMsg = "", int showTimeMs = 5000, Rect? screenBound = null)
	{
		string notifyMsg2 = notifyMsg;
		Dispatcher.UIThread.InvokeAsync(delegate
		{
			INativeNotification nativeNotification = GenerateNotification();
			if (nativeNotification != null)
			{
				nativeNotification.Title = "SpaceWalker";
				nativeNotification.Message = notifyMsg2;
				nativeNotification.Expiration = TimeSpan.FromMilliseconds(showTimeMs);
				nativeNotification.Show();
			}
		});
	}

	private static INativeNotification? GenerateNotification(string? category = null)
	{
		_currentNotification = NativeNotificationManager.Current?.CreateNotification(category);
		if (_currentNotification != null)
		{
			_currentNotification.Icon = new Bitmap(AssetLoader.Open(new Uri("avares://SpaceWalker/Assets/Images/app_icon.png")));
		}
		return _currentNotification;
	}
}
