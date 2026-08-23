using System;
using System.CodeDom.Compiler;
using System.Runtime.InteropServices;
using Windows.Win32.UI.WindowsAndMessaging;

namespace Windows.Win32;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.275+02bc0c298e.RR")]
public class UnhookWindowsHookExSafeHandle : SafeHandle
{
	private static readonly IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1L);

	public override bool IsInvalid
	{
		get
		{
			if (handle.ToInt64() != -1)
			{
				return handle.ToInt64() == 0;
			}
			return true;
		}
	}

	public UnhookWindowsHookExSafeHandle()
		: base(INVALID_HANDLE_VALUE, ownsHandle: true)
	{
	}

	public UnhookWindowsHookExSafeHandle(IntPtr preexistingHandle, bool ownsHandle = true)
		: base(INVALID_HANDLE_VALUE, ownsHandle)
	{
		SetHandle(preexistingHandle);
	}

	protected override bool ReleaseHandle()
	{
		return PInvoke.UnhookWindowsHookEx((HHOOK)handle);
	}
}
