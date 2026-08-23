using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class HideTaskbarIcon : MonoBehaviour
{
	private const int GWL_EXSTYLE = -20;

	private const int WS_EX_TOOLWINDOW = 128;

	private const int SW_HIDE = 0;

	[DllImport("user32.dll")]
	private static extern IntPtr GetActiveWindow();

	[DllImport("user32.dll")]
	private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

	[DllImport("user32.dll")]
	private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

	[DllImport("user32.dll")]
	private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

	private void Start()
	{
		if (!Application.isEditor)
		{
			IntPtr activeWindow = GetActiveWindow();
			int windowLong = GetWindowLong(activeWindow, -20);
			SetWindowLong(activeWindow, -20, windowLong | 0x80);
		}
	}
}
