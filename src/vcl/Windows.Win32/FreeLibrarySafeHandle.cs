using System;
using System.CodeDom.Compiler;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;

namespace Windows.Win32;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.275+02bc0c298e.RR")]
public class FreeLibrarySafeHandle : SafeHandle
{
	private static readonly IntPtr INVALID_HANDLE_VALUE = new IntPtr(0L);

	public override bool IsInvalid => handle.ToInt64() == 0;

	public FreeLibrarySafeHandle()
		: base(INVALID_HANDLE_VALUE, ownsHandle: true)
	{
	}

	public FreeLibrarySafeHandle(IntPtr preexistingHandle, bool ownsHandle = true)
		: base(INVALID_HANDLE_VALUE, ownsHandle)
	{
		SetHandle(preexistingHandle);
	}

	protected override bool ReleaseHandle()
	{
		return PInvoke.FreeLibrary((HMODULE)handle);
	}
}
