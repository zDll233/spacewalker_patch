using System;
using System.Collections;
using System.Drawing;
using System.Runtime.InteropServices;
using UnityEngine;
using VitureCommonLibrary;

public class WindowManager : MonoBehaviour
{
	private const int GWL_EXSTYLE = -20;

	private const int WS_EX_TOOLWINDOW = 128;

	private const int WS_EX_NOACTIVATE = 134217728;

	private const int WS_EX_TOPMOST = 8;

	private const int TOP_MOST = -1;

	private const uint SWP_NOSIZE = 1u;

	private const uint SWP_NOMOVE = 2u;

	private const uint SWP_FRAMECHANGED = 32u;

	private const uint SWP_SHOWWINDOW = 64u;

	private Point viturePos;

	private Size vitureSize;

	private int checkCount;

	private const int MaxCheckCount = 15;

	private static bool hasSetDisplay;

	private string _glassesModel = string.Empty;

	[DllImport("user32.dll", SetLastError = true)]
	private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

	[DllImport("user32.dll", SetLastError = true)]
	private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

	private void Start()
	{
		if (!Application.isEditor)
		{
			InitParam();
			Win32PInvoke.SetNoFrameWindow();
			Win32PInvoke.SetWindowState(2);
			ShowWindow();
			StartCoroutine(checkAndChangeWindowsPos());
		}
	}

	private IEnumerator checkAndChangeWindowsPos()
	{
		while (checkCount < 15)
		{
			yield return new WaitForSeconds(1f);
			checkCount++;
			if (DisplayManager2.Instance.VitureDisplay != null)
			{
				ShowWindow();
				if (hasSetDisplay)
				{
					yield break;
				}
			}
		}
		VitureCommonLibrary.Logger.Warning($"checkAndChangeWindowsPos: VitureDisplay {15}s 内未就绪，放弃重试");
	}

	private void InitParam()
	{
		string[] commandLineArgs = Environment.GetCommandLineArgs();
		for (int i = 0; i < commandLineArgs.Length; i++)
		{
			if (commandLineArgs[i] == "-glassesModel" && i + 1 < commandLineArgs.Length)
			{
				string glassesModel = commandLineArgs[i + 1];
				_glassesModel = glassesModel;
				VitureCommonLibrary.Logger.Info("_glassesModel: " + _glassesModel);
			}
		}
	}

	private void ShowWindow()
	{
		VitureCommonLibrary.DisplayInfo vitureDisplay = DisplayManager2.Instance.VitureDisplay;
		if (vitureDisplay == null)
		{
			VitureCommonLibrary.Logger.Warning("ShowWindow: VitureDisplay not ready yet, skipping (will retry via checkAndChangeWindowsPos)");
			return;
		}
		viturePos = vitureDisplay.CurrentSetting.Position;
		vitureSize = vitureDisplay.CurrentSetting.Resolution;
		VitureCommonLibrary.Logger.Info($"SetWindowState: {viturePos.X} {viturePos.Y} {vitureSize.Width} {vitureSize.Height}");
		if (!hasSetDisplay && vitureSize.Width > 0 && vitureSize.Height > 0)
		{
			hasSetDisplay = true;
			Screen.SetResolution(vitureSize.Width, vitureSize.Height, FullScreenMode.FullScreenWindow);
			QualitySettings.vSyncCount = 1;
			QualitySettings.maxQueuedFrames = 1;
			VitureCommonLibrary.Logger.Info($"[Display] {_glassesModel} -> FullScreenWindow {vitureSize.Width}x{vitureSize.Height} vSync=1 maxQueuedFrames=1 (was {Screen.width}x{Screen.height})");
		}
		Win32PInvoke.SetWindowState(1);
		int windowLong = GetWindowLong(Win32PInvoke.UnityHWnd, -20);
		windowLong |= 0x8000088;
		SetWindowLong(Win32PInvoke.UnityHWnd, -20, windowLong);
		Win32PInvoke.SetWindowPos(Win32PInvoke.UnityHWnd, -1, viturePos.X, viturePos.Y, vitureSize.Width, vitureSize.Height, 96u);
		StartCoroutine(LogViewportDiag());
	}

	private IEnumerator LogViewportDiag()
	{
		for (int i = 0; i < 8; i++)
		{
			Win32PInvoke.RECT lpRect = default(Win32PInvoke.RECT);
			Win32PInvoke.RECT lpRect2 = default(Win32PInvoke.RECT);
			Win32PInvoke.GetWindowRect(Win32PInvoke.UnityHWnd, ref lpRect);
			Win32PInvoke.GetClientRect(Win32PInvoke.UnityHWnd, ref lpRect2);
			int num = lpRect.Right - lpRect.Left;
			int num2 = lpRect.Bottom - lpRect.Top;
			int num3 = lpRect2.Right - lpRect2.Left;
			int num4 = lpRect2.Bottom - lpRect2.Top;
			Camera main = Camera.main;
			string arg = ((main != null) ? ($"cam.pixel={main.pixelWidth}x{main.pixelHeight} cam.rect={main.rect} aspect={main.aspect:F3} " + $"clear={main.clearFlags} bg={main.backgroundColor} " + "targetTex=" + ((main.targetTexture != null) ? (main.targetTexture.width + "x" + main.targetTexture.height) : "null")) : "cam=null");
			VitureCommonLibrary.Logger.Info($"[ViewportDiag #{i}] expectVITURE={vitureSize.Width}x{vitureSize.Height}@({viturePos.X},{viturePos.Y}) | " + $"winRect={num}x{num2}@({lpRect.Left},{lpRect.Top}) clientRect={num3}x{num4} | " + $"Screen={Screen.width}x{Screen.height} fullScreen={Screen.fullScreen} mode={Screen.fullScreenMode} " + $"curRes={Screen.currentResolution.width}x{Screen.currentResolution.height}@{Screen.currentResolution.refreshRate} | " + $"camCount={Camera.allCamerasCount} {arg}");
			yield return new WaitForSeconds(1f);
		}
	}
}
