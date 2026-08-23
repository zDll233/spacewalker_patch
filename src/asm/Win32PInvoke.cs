using System;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;

public static class Win32PInvoke
{
	private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

	private delegate bool EnumMonitorsDelegate(IntPtr hMonitor, IntPtr hdcMonitor, ref RECT lprcMonitor, IntPtr dwData);

	public struct RECT
	{
		public int Left;

		public int Top;

		public int Right;

		public int Bottom;

		public override string ToString()
		{
			return $"left = {Left}  right = {Right}  top = {Top}  bottom = {Bottom}";
		}
	}

	private struct POINT
	{
		public int X;

		public int Y;
	}

	public struct MonitorInfo
	{
		public int Left;

		public int Top;

		public int Right;

		public int Bottom;

		public bool IsPrimary;

		public override string ToString()
		{
			return $"(left = {Left}, top = {Top}, right = {Right}, bottom =  {Bottom})";
		}
	}

	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
	private struct MONITORINFOEX
	{
		public int Size;

		public RECT Monitor;

		public RECT WorkArea;

		public uint Flags;

		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
		public string DeviceName;
	}

	private static IntPtr ptr;

	public const ulong WS_MAXIMIZEBOX = 65536uL;

	public const ulong WS_DLGFRAME = 4194304uL;

	public const ulong WS_SIZEBOX = 262144uL;

	public const ulong WS_BORDER = 8388608uL;

	public const ulong WS_CAPTION = 12582912uL;

	public const int GWLP_WNDPROC = -4;

	public const int WM_SIZING = 532;

	public const int WS_POPUP = 8388608;

	public const int WS_EX_TOOLWINDOW = 128;

	public const int GWL_STYLE = -16;

	public const uint SWP_SHOWWINDOW = 64u;

	public const uint SWP_NOMOVE = 2u;

	public const int SW_SHOWNORMAL = 1;

	public const int SW_SHOWMINIMIZED = 2;

	public const int SW_SHOWMAXIMIZED = 3;

	public const string UNITY_WND_CLASSNAME = "UnityWndClass";

	public static IntPtr UnityHWnd
	{
		get
		{
			_ = ptr;
			if (ptr == IntPtr.Zero)
			{
				ptr = GetUnityWindow();
			}
			return ptr;
		}
	}

	[DllImport("user32.dll")]
	public static extern IntPtr CallWindowProc(IntPtr lpPrevWndFunc, IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

	[DllImport("user32.dll")]
	public static extern IntPtr GetWindowLongPtr(IntPtr hwnd, int nIndex);

	[DllImport("user32.dll", SetLastError = true)]
	public static extern bool GetWindowRect(IntPtr hwnd, ref RECT lpRect);

	[DllImport("user32.dll")]
	public static extern bool GetClientRect(IntPtr hWnd, ref RECT lpRect);

	[DllImport("user32.dll", CharSet = CharSet.Auto)]
	public static extern IntPtr SetWindowLongPtr(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

	[DllImport("user32.dll")]
	public static extern bool ShowWindow(IntPtr hwnd, int nCmdShow);

	[DllImport("user32.dll")]
	public static extern bool SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

	[DllImport("user32.dll")]
	private static extern bool EnumThreadWindows(uint dwThreadId, EnumWindowsProc lpEnumFunc, IntPtr lParam);

	[DllImport("kernel32.dll")]
	private static extern uint GetCurrentThreadId();

	[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
	private static extern int GetClassName(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

	[DllImport("user32.dll")]
	public static extern bool ReleaseCapture();

	[DllImport("user32.dll")]
	public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

	[DllImport("user32.dll")]
	private static extern bool GetCursorPos(out POINT lpPoint);

	[DllImport("user32.dll")]
	private static extern bool EnumDisplayMonitors(IntPtr hdc, IntPtr lprcClip, EnumMonitorsDelegate lpfnEnum, IntPtr dwData);

	[DllImport("user32.dll", CharSet = CharSet.Auto)]
	private static extern bool GetMonitorInfo(IntPtr hMonitor, ref MONITORINFOEX lpmi);

	public static void SetWindowState(int state)
	{
		if (!Application.isEditor)
		{
			ShowWindow(UnityHWnd, state);
		}
		else
		{
			Debug.LogWarning("Win32PInvoke:为避免编辑器行为异常， 请打包 exe 后测试！");
		}
	}

	public static void SetNoFrameWindow()
	{
		if (!Application.isEditor)
		{
			ulong num = (ulong)(long)GetWindowLongPtr(UnityHWnd, -16);
			num &= 0;
			SetWindowLongPtr(UnityHWnd, -16, (IntPtr)(long)num);
		}
		else
		{
			Debug.LogWarning("Win32PInvoke:为避免编辑器行为异常， 请打包 exe 后测试！");
		}
	}

	public static void DragWindow()
	{
		ReleaseCapture();
		SendMessage(UnityHWnd, 161, 2, 0);
		SendMessage(UnityHWnd, 514, 0, 0);
	}

	public static void MouseButtonUp()
	{
		ReleaseCapture();
		SendMessage(UnityHWnd, 514, 0, 0);
	}

	public static IntPtr GetUnityWindow()
	{
		IntPtr unityHWnd = IntPtr.Zero;
		EnumThreadWindows(GetCurrentThreadId(), delegate(IntPtr hWnd, IntPtr lParam)
		{
			StringBuilder stringBuilder = new StringBuilder("UnityWndClass".Length + 1);
			GetClassName(hWnd, stringBuilder, stringBuilder.Capacity);
			if (stringBuilder.ToString() == "UnityWndClass")
			{
				unityHWnd = hWnd;
				return false;
			}
			return true;
		}, IntPtr.Zero);
		return unityHWnd;
	}
}
