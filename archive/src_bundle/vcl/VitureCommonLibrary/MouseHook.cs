using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Timers;
using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.UI.WindowsAndMessaging;

namespace VitureCommonLibrary;

public static class MouseHook
{
	private const int WM_MOUSEMOVE = 512;

	private static HOOKPROC _proc = HookCallback;

	private static HHOOK _hookID;

	private static Rectangle? _exclude;

	private static Point _lastValidPoint;

	private static DateTime _lastSnapTime = DateTime.MinValue;

	private const int SnapDebounceMs = 50;

	private const int IdleTimeoutMs = 5000;

	private static Timer? _idleTimer;

	private static bool _isMoving;

	public static Rectangle? CurrentExclude => _exclude;

	public static bool IsMoving
	{
		get
		{
			return _isMoving;
		}
		private set
		{
			if (_isMoving != value)
			{
				_isMoving = value;
				MouseHook.MoveStateChanged?.Invoke(_isMoving);
			}
		}
	}

	public static event Action<bool>? MoveStateChanged;

	public static void SetMouseHook(Rectangle? exclude)
	{
		_exclude = exclude;
		if (_idleTimer == null)
		{
			_idleTimer = new Timer(5000.0)
			{
				AutoReset = false
			};
			_idleTimer.Elapsed += delegate
			{
				IsMoving = false;
			};
		}
		if (_hookID == default(HHOOK))
		{
			_hookID = SetHook(_proc);
		}
		Logger.Info("SetMouseHook: exclude=" + (exclude?.ToString() ?? "null"));
	}

	private static HHOOK SetHook(HOOKPROC proc)
	{
		using Process process = Process.GetCurrentProcess();
		ProcessModule mainModule = process.MainModule;
		if (mainModule == null)
		{
			return default(HHOOK);
		}
		FreeLibrarySafeHandle moduleHandle = PInvoke.GetModuleHandle(mainModule.ModuleName);
		return PInvoke.SetWindowsHookEx(WINDOWS_HOOK_ID.WH_MOUSE_LL, proc, new HINSTANCE(moduleHandle.DangerousGetHandle()), 0u);
	}

	public static void UnSetHook()
	{
		_exclude = null;
		if (_hookID != default(HHOOK))
		{
			PInvoke.UnhookWindowsHookEx(_hookID);
			_hookID = default(HHOOK);
		}
		_idleTimer?.Stop();
		_idleTimer?.Dispose();
		_idleTimer = null;
		_isMoving = false;
		Logger.Info("UnSetHook");
	}

	public static Point GetCursorPosition()
	{
		PInvoke.GetCursorPos(out var lpPoint);
		return new Point(lpPoint.X, lpPoint.Y);
	}

	private static LRESULT HookCallback(int nCode, WPARAM wParam, LPARAM lParam)
	{
		if (nCode >= 0 && wParam.Value == 512)
		{
			Point pt = Marshal.PtrToStructure<MSLLHOOKSTRUCT>((nint)lParam).pt;
			IsMoving = true;
			try
			{
				_idleTimer?.Stop();
				_idleTimer?.Start();
			}
			catch (ObjectDisposedException)
			{
			}
			catch (Exception ex2)
			{
				Logger.Warning("MouseHook idle timer: " + ex2.Message);
			}
			Rectangle? exclude = _exclude;
			if (exclude.HasValue && exclude.GetValueOrDefault().Contains(pt.X, pt.Y))
			{
				if ((DateTime.Now - _lastSnapTime).TotalMilliseconds >= 50.0)
				{
					PInvoke.SetCursorPos(_lastValidPoint.X, _lastValidPoint.Y);
					_lastSnapTime = DateTime.Now;
				}
				return new LRESULT(1);
			}
			_lastValidPoint = new Point(pt.X, pt.Y);
			if (MouseShakeDetector.EnableMouseShake)
			{
				MouseShakeDetector.MouseMove(new Point(pt.X, pt.Y));
			}
			if (MouseShakeDetector.IsShaking)
			{
				return new LRESULT(1);
			}
		}
		return PInvoke.CallNextHookEx(_hookID, nCode, wParam, lParam);
	}

	public static bool SetCursorPos(int X, int Y)
	{
		return PInvoke.SetCursorPos(X, Y);
	}
}
