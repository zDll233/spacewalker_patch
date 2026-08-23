using System;
using System.Runtime.InteropServices;

namespace VitureCommonLibrary;

public static class PlatformHelper
{
	private const ushort NativeMachineArm64 = 43620;

	private static readonly Lazy<bool> _isArm64Translated = new Lazy<bool>(DetectArm64Translated);

	public static bool IsArm64Translated => _isArm64Translated.Value;

	[DllImport("kernel32.dll", SetLastError = true)]
	[return: MarshalAs(UnmanagedType.Bool)]
	private static extern bool IsWow64Process2(IntPtr hProcess, out ushort pProcessMachine, out ushort pNativeMachine);

	[DllImport("kernel32.dll")]
	private static extern IntPtr GetCurrentProcess();

	private static bool DetectArm64Translated()
	{
		try
		{
			if (IsWow64Process2(GetCurrentProcess(), out var _, out var pNativeMachine))
			{
				return pNativeMachine == 43620;
			}
		}
		catch
		{
		}
		return false;
	}
}
