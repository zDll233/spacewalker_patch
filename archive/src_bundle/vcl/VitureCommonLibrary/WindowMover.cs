using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace VitureCommonLibrary;

public class WindowMover : IDisposable
{
	public struct POINT
	{
		public int X;

		public int Y;

		public POINT(int x, int y)
		{
			X = x;
			Y = y;
		}
	}

	public struct RECT
	{
		public int Left;

		public int Top;

		public int Right;

		public int Bottom;
	}

	private struct MSG
	{
		public IntPtr hwnd;

		public uint message;

		public IntPtr wParam;

		public IntPtr lParam;

		public uint time;

		public POINT pt;
	}

	private enum MONITOR_DPI_TYPE
	{
		MDT_EFFECTIVE_DPI = 0,
		MDT_ANGULAR_DPI = 1,
		MDT_RAW_DPI = 2,
		MDT_DEFAULT = 0
	}

	[UnmanagedFunctionPointer(CallingConvention.StdCall)]
	private delegate void WinEventProc(IntPtr hHook, uint eventType, IntPtr hWnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime);

	private const string SYS_TITLE = "Program Manager";

	private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);

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

	private const int GWL_EXSTYLE = -20;

	private const uint WS_EX_TOPMOST = 8u;

	public const uint SWP_SHOWWINDOW = 64u;

	public const uint SWP_NOMOVE = 2u;

	public const int SW_SHOWNORMAL = 1;

	public const int SW_SHOWMINIMIZED = 2;

	public const int SW_SHOWMAXIMIZED = 3;

	private const uint WINEVENT_OUTOFCONTEXT = 0u;

	private const uint EVENT_SYSTEM_FOREGROUND = 3u;

	private const uint EVENT_SYSTEM_MOVESIZEEND = 11u;

	private const uint EVENT_OBJECT_SHOW = 32770u;

	private const uint WM_QUIT = 18u;

	private const int SWP_NOSIZE = 1;

	private const int SWP_NOZORDER = 4;

	private const int GW_HWNDNEXT = 2;

	private const uint MONITOR_DEFAULTTONEAREST = 2u;

	private volatile bool _displayDirty = true;

	private Rectangle _vitureRect;

	private Rectangle _primaryRect;

	private Rectangle[] _extRects = Array.Empty<Rectangle>();

	private readonly object _cacheLock = new object();

	private Thread? _hookThread;

	private uint _hookThreadId;

	private WinEventProc? _winEventDelegate;

	private readonly List<IntPtr> _hookHandles = new List<IntPtr>();

	private readonly ConcurrentQueue<IntPtr> _pendingWindows = new ConcurrentQueue<IntPtr>();

	private Timer? _processTimer;

	private volatile bool _running;

	private static readonly Lazy<WindowMover> instance = new Lazy<WindowMover>(() => new WindowMover());

	public Func<bool> ShouldProcess { get; set; } = () => true;


	public IReadOnlyCollection<string> ViewWindowTitles { get; set; } = (IReadOnlyCollection<string>)(object)Array.Empty<string>();


	public static WindowMover Instance => instance.Value;

	[DllImport("user32.dll")]
	private static extern IntPtr SetWinEventHook(uint eventMin, uint eventMax, IntPtr hmodWinEventProc, WinEventProc lpfnWinEventProc, uint idProcess, uint idThread, uint dwFlags);

	[DllImport("user32.dll")]
	private static extern bool UnhookWinEvent(IntPtr hWinEventHook);

	[DllImport("user32.dll")]
	private static extern int GetMessage(out MSG lpMsg, IntPtr hWnd, uint min, uint max);

	[DllImport("user32.dll")]
	private static extern bool TranslateMessage(ref MSG lpMsg);

	[DllImport("user32.dll")]
	private static extern IntPtr DispatchMessage(ref MSG lpMsg);

	[DllImport("user32.dll")]
	private static extern bool PostThreadMessage(uint idThread, uint msg, IntPtr wParam, IntPtr lParam);

	[DllImport("kernel32.dll")]
	private static extern uint GetCurrentThreadId();

	[DllImport("user32.dll")]
	private static extern IntPtr MonitorFromWindow(IntPtr hwnd, uint dwFlags);

	[DllImport("shcore.dll")]
	private static extern uint GetDpiForMonitor(IntPtr hMonitor, MONITOR_DPI_TYPE dpiType, out uint dpiX, out uint dpiY);

	[DllImport("user32.dll")]
	public static extern IntPtr MonitorFromPoint(POINT pt, uint dwFlags);

	[DllImport("user32.dll")]
	public static extern IntPtr GetForegroundWindow();

	[DllImport("user32.dll")]
	private static extern uint GetWindowLong(IntPtr hWnd, int nIndex);

	[DllImport("user32.dll")]
	private static extern bool IsWindowEnabled(IntPtr hWnd);

	[DllImport("user32.dll")]
	public static extern bool GetWindowRect(IntPtr hWnd, ref RECT lpRect);

	[DllImport("user32.dll", SetLastError = true)]
	public static extern uint GetDpiForWindow(IntPtr hWnd);

	[DllImport("user32.dll")]
	public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

	[DllImport("user32.dll")]
	public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

	[DllImport("user32.dll", SetLastError = true)]
	public static extern IntPtr GetNextWindow(IntPtr hWnd, int uCmd);

	[DllImport("user32.dll", CharSet = CharSet.Auto)]
	public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

	[DllImport("user32.dll", CharSet = CharSet.Auto)]
	public static extern int GetWindowTextLength(IntPtr hWnd);

	[DllImport("user32.dll")]
	public static extern IntPtr GetWindowLongPtr(IntPtr hwnd, int nIndex);

	[DllImport("user32.dll", CharSet = CharSet.Auto)]
	public static extern IntPtr SetWindowLongPtr(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

	[DllImport("user32.dll")]
	public static extern bool SetForegroundWindow(IntPtr hWnd);

	[DllImport("user32.dll")]
	private static extern bool IsIconic(IntPtr hWnd);

	[DllImport("user32.dll")]
	private static extern bool IsWindowVisible(IntPtr hWnd);

	public static string GetWindowTitle(IntPtr hWnd)
	{
		int num = GetWindowTextLength(hWnd) + 1;
		StringBuilder stringBuilder = new StringBuilder(num);
		GetWindowText(hWnd, stringBuilder, num);
		return stringBuilder.ToString();
	}

	private void RefreshDisplayCacheIfNeeded()
	{
		if (!_displayDirty)
		{
			return;
		}
		lock (_cacheLock)
		{
			if (!_displayDirty)
			{
				return;
			}
			try
			{
				IEnumerable<DisplayConfig> vitures = DisplayManager2.Instance.GetVitures(onlyActive: true);
				IEnumerable<string> viturePaths = vitures.Select((DisplayConfig x) => x.GetDevicePath());
				DisplayConfig displayConfig = vitures.FirstOrDefault();
				DisplayConfig primary = DisplayManager2.Instance.GetPrimary();
				IEnumerable<DisplayConfig> source = from x in DisplayManager2.Instance.GetAll(onlyActive: true)
					where !viturePaths.Contains(x.GetDevicePath())
					select x;
				Rectangle vitureRect;
				if (displayConfig != null)
				{
					DisplaySourceMode sourceMode = displayConfig.GetSourceMode();
					if (sourceMode != null)
					{
						vitureRect = new Rectangle(sourceMode.Left, sourceMode.Top, (int)sourceMode.Width, (int)sourceMode.Height);
						goto IL_00d5;
					}
				}
				vitureRect = Rectangle.Empty;
				goto IL_00d5;
				IL_0114:
				Rectangle primaryRect;
				_primaryRect = (Rectangle)primaryRect;
				_extRects = (from x in source
					select x.GetSourceMode() into sm
					where sm != null
					select new Rectangle(sm.Left, sm.Top, (int)sm.Width, (int)sm.Height)).ToArray();
				return;
				IL_00d5:
				_vitureRect = vitureRect;
				if (primary != null)
				{
					DisplaySourceMode sourceMode2 = primary.GetSourceMode();
					if (sourceMode2 != null)
					{
						primaryRect = new Rectangle(sourceMode2.Left, sourceMode2.Top, (int)sourceMode2.Width, (int)sourceMode2.Height);
						goto IL_0114;
					}
				}
				primaryRect = Rectangle.Empty;
				goto IL_0114;
			}
			catch (Exception ex)
			{
				Logger.Warning("[WindowMover] RefreshDisplayCache failed: " + ex.Message);
			}
			finally
			{
				_displayDirty = false;
			}
		}
	}

	private WindowMover()
	{
	}

	public void Start()
	{
		if (!_running)
		{
			_running = true;
			DisplayManager2.Instance.DisplayChanged += OnDisplayTopologyChanged;
			_displayDirty = true;
			_hookThread = new Thread(HookThreadProc)
			{
				IsBackground = true,
				Name = "WindowMover.Hook"
			};
			_hookThread.SetApartmentState(ApartmentState.STA);
			_hookThread.Start();
			_processTimer = new Timer(delegate
			{
				ProcessPendingWindows();
			}, null, 500, 500);
		}
	}

	private void OnDisplayTopologyChanged()
	{
		_displayDirty = true;
	}

	public void Stop()
	{
		if (_running)
		{
			_running = false;
			_processTimer?.Dispose();
			_processTimer = null;
			DisplayManager2.Instance.DisplayChanged -= OnDisplayTopologyChanged;
			_displayDirty = true;
			if (_hookThreadId != 0)
			{
				PostThreadMessage(_hookThreadId, 18u, IntPtr.Zero, IntPtr.Zero);
			}
			_hookThread?.Join(2000);
			_hookThread = null;
			_hookThreadId = 0u;
		}
	}

	private void HookThreadProc()
	{
		_hookThreadId = GetCurrentThreadId();
		_winEventDelegate = OnWinEvent;
		_hookHandles.AddRange(new IntPtr[3]
		{
			SetWinEventHook(3u, 3u, IntPtr.Zero, _winEventDelegate, 0u, 0u, 0u),
			SetWinEventHook(11u, 11u, IntPtr.Zero, _winEventDelegate, 0u, 0u, 0u),
			SetWinEventHook(32770u, 32770u, IntPtr.Zero, _winEventDelegate, 0u, 0u, 0u)
		});
		while (_running)
		{
			MSG lpMsg;
			int message = GetMessage(out lpMsg, IntPtr.Zero, 0u, 0u);
			if (message == 0 || message == -1)
			{
				break;
			}
			TranslateMessage(ref lpMsg);
			DispatchMessage(ref lpMsg);
		}
		foreach (IntPtr hookHandle in _hookHandles)
		{
			if (hookHandle != IntPtr.Zero)
			{
				UnhookWinEvent(hookHandle);
			}
		}
		_hookHandles.Clear();
	}

	private void OnWinEvent(IntPtr hHook, uint eventType, IntPtr hWnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime)
	{
		if (!(hWnd == IntPtr.Zero) && idObject == 0)
		{
			_pendingWindows.Enqueue(hWnd);
		}
	}

	private void ProcessPendingWindows()
	{
		if (_pendingWindows.IsEmpty)
		{
			return;
		}
		if (!ShouldProcess())
		{
			IntPtr result;
			while (_pendingWindows.TryDequeue(out result))
			{
			}
			return;
		}
		HashSet<IntPtr> hashSet = new HashSet<IntPtr>();
		IntPtr result2;
		while (_pendingWindows.TryDequeue(out result2))
		{
			hashSet.Add(result2);
		}
		RefreshDisplayCacheIfNeeded();
		foreach (IntPtr item in hashSet)
		{
			TryMoveWindow(item);
		}
	}

	private void TryMoveWindow(IntPtr hwnd)
	{
		if (!IsValidMovableWindow(hwnd))
		{
			return;
		}
		string windowTitle = GetWindowTitle(hwnd);
		if (string.IsNullOrWhiteSpace(windowTitle) || windowTitle == "Program Manager")
		{
			return;
		}
		RECT lpRect = default(RECT);
		if (!GetWindowRect(hwnd, ref lpRect))
		{
			return;
		}
		if (ViewWindowTitles.Contains(windowTitle))
		{
			if (!_vitureRect.IsEmpty)
			{
				Logger.Info($"[WindowMover] Set View Window -> {_vitureRect.X} {_vitureRect.Y} {_vitureRect.Width} {_vitureRect.Height}");
				SetWindowPos(hwnd, IntPtr.Zero, _vitureRect.X, _vitureRect.Y, _vitureRect.Width, _vitureRect.Height, 68u);
			}
			return;
		}
		RECT rECT = default(RECT);
		rECT.Left = lpRect.Left;
		rECT.Top = lpRect.Top;
		rECT.Right = lpRect.Right;
		rECT.Bottom = lpRect.Top + 40;
		RECT rect = rECT;
		rECT = default(RECT);
		rECT.Left = _vitureRect.Left;
		rECT.Top = _vitureRect.Top;
		rECT.Right = _vitureRect.Right;
		rECT.Bottom = _vitureRect.Bottom;
		RECT rect2 = rECT;
		bool flag = true;
		Rectangle[] extRects = _extRects;
		for (int i = 0; i < extRects.Length; i++)
		{
			Rectangle rectangle = extRects[i];
			rECT = default(RECT);
			rECT.Left = rectangle.Left;
			rECT.Top = rectangle.Top;
			rECT.Right = rectangle.Right;
			rECT.Bottom = rectangle.Bottom;
			RECT rect3 = rECT;
			if (IsContainRect(rect, rect3, 0.25f))
			{
				flag = false;
				break;
			}
		}
		if ((!flag && !IsContainRect(rect, rect2)) || _primaryRect.IsEmpty)
		{
			return;
		}
		if (lpRect.Right - lpRect.Left <= _primaryRect.Width && lpRect.Bottom - lpRect.Top <= _primaryRect.Height)
		{
			Logger.Info("[WindowMover] title:" + windowTitle + " fits primary display, no resize needed.");
			SetWindowPos(hwnd, IntPtr.Zero, _primaryRect.X + (_primaryRect.Width - (lpRect.Right - lpRect.Left)) / 2, _primaryRect.Y + (_primaryRect.Height - (lpRect.Bottom - lpRect.Top)) / 2, lpRect.Right - lpRect.Left, lpRect.Bottom - lpRect.Top, 68u);
			SetForegroundWindow(hwnd);
			return;
		}
		IntPtr intPtr = MonitorFromPoint(new POINT(_primaryRect.X + _primaryRect.Width / 2, _primaryRect.Y + _primaryRect.Height / 2), 2u);
		uint dpiX = 96u;
		uint dpiY = 96u;
		if (intPtr != IntPtr.Zero)
		{
			GetDpiForMonitor(intPtr, MONITOR_DPI_TYPE.MDT_EFFECTIVE_DPI, out dpiX, out dpiY);
		}
		int num = _primaryRect.Width / 2 * (int)dpiX / 96;
		int num2 = _primaryRect.Height / 2 * (int)dpiY / 96;
		int num3 = _primaryRect.X + _primaryRect.Width / 2 - num / 2;
		int num4 = _primaryRect.Y + _primaryRect.Height / 2 - num2 / 2;
		Logger.Info($"[WindowMover] title:{windowTitle} -> primary center {num3} {num4} {num} {num2}");
		ShowWindow(hwnd, 1);
		SetWindowPos(hwnd, IntPtr.Zero, num3, num4, num, num2, 68u);
		SetForegroundWindow(hwnd);
	}

	private static bool IsValidMovableWindow(IntPtr hWnd)
	{
		try
		{
			if (hWnd == IntPtr.Zero || !IsWindowVisible(hWnd) || IsIconic(hWnd) || !IsWindowEnabled(hWnd))
			{
				return false;
			}
			string windowTitle = GetWindowTitle(hWnd);
			if (string.IsNullOrWhiteSpace(windowTitle) || windowTitle == "Program Manager")
			{
				return false;
			}
			if ((GetWindowLong(hWnd, -16) & 0x40000000u) != 0)
			{
				return false;
			}
			RECT lpRect = default(RECT);
			if (GetWindowRect(hWnd, ref lpRect))
			{
				int num = lpRect.Right - lpRect.Left;
				int num2 = lpRect.Bottom - lpRect.Top;
				if (num < 50 || num2 < 50 || num > 5000 || num2 > 5000)
				{
					return false;
				}
			}
			return true;
		}
		catch (Exception ex)
		{
			Logger.Warning("[WindowMover] IsValidMovableWindow: " + ex.Message);
			return false;
		}
	}

	private bool IsContainRect(RECT rect1, RECT rect2, float areaTh = 0.75f)
	{
		int num = rect1.Right - rect1.Left;
		if (num <= 0)
		{
			return false;
		}
		int num2 = Math.Max(0, Math.Min(rect1.Right, rect2.Right) - Math.Max(rect1.Left, rect2.Left));
		int num3 = Math.Max(0, Math.Min(rect1.Bottom, rect2.Bottom) - Math.Max(rect1.Top, rect2.Top));
		if (num2 <= 0 || num3 <= 0)
		{
			return false;
		}
		float num4 = (float)num2 * 1f / (float)num;
		if (num4 < areaTh)
		{
			Logger.Info($"[WindowMover] overlayRatio: {num4:F2} overlayWidth:{num2} width1:{num}");
		}
		return num4 >= areaTh;
	}

	public void Dispose()
	{
		Stop();
	}
}
