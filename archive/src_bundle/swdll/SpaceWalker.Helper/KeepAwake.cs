using System;
using System.Runtime.InteropServices;
using Avalonia.Threading;
using VitureCommonLibrary;

namespace SpaceWalker.Helper;

internal static class KeepAwake
{
	[Flags]
	private enum EXECUTION_STATE : uint
	{
		ES_CONTINUOUS = 0x80000000u,
		ES_SYSTEM_REQUIRED = 1u,
		ES_DISPLAY_REQUIRED = 2u
	}

	private static bool _enabled;

	[DllImport("kernel32.dll", SetLastError = true)]
	private static extern EXECUTION_STATE SetThreadExecutionState(EXECUTION_STATE esFlags);

	public static void Enable()
	{
		_enabled = true;
		Apply(EXECUTION_STATE.ES_CONTINUOUS | EXECUTION_STATE.ES_SYSTEM_REQUIRED | EXECUTION_STATE.ES_DISPLAY_REQUIRED);
	}

	public static void Disable()
	{
		if (_enabled)
		{
			_enabled = false;
			Apply(EXECUTION_STATE.ES_CONTINUOUS);
		}
	}

	private static void Apply(EXECUTION_STATE state)
	{
		if (Dispatcher.UIThread.CheckAccess())
		{
			DoSet(state);
			return;
		}
		Dispatcher.UIThread.Post(delegate
		{
			DoSet(state);
		});
	}

	private static void DoSet(EXECUTION_STATE state)
	{
		if (SetThreadExecutionState(state) == (EXECUTION_STATE)0u)
		{
			Logger.Warning("[KeepAwake] SetThreadExecutionState failed");
		}
	}
}
