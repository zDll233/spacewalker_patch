using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.UI.WindowsAndMessaging;

namespace VitureCommonLibrary;

internal sealed class DisplayChangeMonitor : IDisposable
{
	private const uint WM_DISPLAYCHANGE = 126u;

	private const uint WM_DEVICECHANGE = 537u;

	private const int DBT_DEVNODES_CHANGED = 7;

	private const uint WM_QUIT = 18u;

	private const string WindowName = "DisplayMonitor";

	public Action? OnDisplayChanged;

	private Thread? _thread;

	private HWND _hwnd;

	private WNDPROC? _wndProcDelegate;

	private readonly string _className = "SpaceWalker_DisplayMonitor_" + Guid.NewGuid().ToString("N");

	private volatile bool _disposed;

	private readonly ConcurrentExclusiveSchedulerPair _schedulerPair = new ConcurrentExclusiveSchedulerPair();

	public void Start()
	{
		_thread = new Thread(MessageLoopProc)
		{
			IsBackground = true,
			Name = "DisplayMonitorMsgLoop"
		};
		_thread.SetApartmentState(ApartmentState.STA);
		_thread.Start();
	}

	private unsafe void MessageLoopProc()
	{
		HINSTANCE hInstance = new HINSTANCE(PInvoke.GetModuleHandle(default(PCWSTR)).Value);
		_wndProcDelegate = WndProc;
		fixed (char* value = _className)
		{
			fixed (char* value2 = "DisplayMonitor")
			{
				WNDCLASSEXW wNDCLASSEXW = default(WNDCLASSEXW);
				wNDCLASSEXW.cbSize = (uint)Marshal.SizeOf<WNDCLASSEXW>();
				wNDCLASSEXW.style = WNDCLASS_STYLES.CS_VREDRAW | WNDCLASS_STYLES.CS_HREDRAW;
				wNDCLASSEXW.lpfnWndProc = _wndProcDelegate;
				wNDCLASSEXW.hInstance = hInstance;
				wNDCLASSEXW.lpszClassName = new PCWSTR(value);
				WNDCLASSEXW param = wNDCLASSEXW;
				if (PInvoke.RegisterClassEx(in param) == 0)
				{
					Logger.Error($"[DisplayMonitor] RegisterClassEx failed: {Marshal.GetLastWin32Error()}");
					return;
				}
				_hwnd = PInvoke.CreateWindowEx(WINDOW_EX_STYLE.WS_EX_LEFT, new PCWSTR(value), new PCWSTR(value2), WINDOW_STYLE.WS_POPUP, 0, 0, 0, 0, HWND.Null, HMENU.Null, hInstance, null);
				if (_hwnd.IsNull)
				{
					Logger.Error($"[DisplayMonitor] CreateWindowEx failed: {Marshal.GetLastWin32Error()}");
					PInvoke.UnregisterClass(new PCWSTR(value), hInstance);
					return;
				}
			}
		}
		Logger.Info("[DisplayMonitor] Message-only window created, listening for display events.");
		MSG lpMsg;
		while (PInvoke.GetMessage(out lpMsg, HWND.Null, 0u, 0u).Value > 0)
		{
			PInvoke.TranslateMessage(in lpMsg);
			PInvoke.DispatchMessage(in lpMsg);
		}
		PInvoke.DestroyWindow(_hwnd);
		fixed (char* value3 = _className)
		{
			PInvoke.UnregisterClass(new PCWSTR(value3), hInstance);
		}
		_hwnd = HWND.Null;
	}

	private LRESULT WndProc(HWND hWnd, uint msg, WPARAM wParam, LPARAM lParam)
	{
		switch (msg)
		{
		case 126u:
			Logger.Info("[DisplayMonitor] WM_DISPLAYCHANGE received.");
			DispatchDisplayChanged();
			break;
		case 537u:
			if ((int)wParam.Value == 7)
			{
				Logger.Info("[DisplayMonitor] WM_DEVICECHANGE/DBT_DEVNODES_CHANGED received.");
				DispatchDisplayChanged();
			}
			break;
		}
		return PInvoke.DefWindowProc(hWnd, msg, wParam, lParam);
	}

	private void DispatchDisplayChanged()
	{
		Task.Factory.StartNew(delegate
		{
			OnDisplayChanged?.Invoke();
		}, CancellationToken.None, TaskCreationOptions.DenyChildAttach, _schedulerPair.ExclusiveScheduler);
	}

	public void Stop()
	{
		if (!_disposed)
		{
			_disposed = true;
			if (!_hwnd.IsNull)
			{
				PInvoke.PostMessage(_hwnd, 18u, default(WPARAM), default(LPARAM));
			}
			_thread?.Join(3000);
			_thread = null;
		}
	}

	public void Dispose()
	{
		Stop();
	}
}
