using System;
using System.CodeDom.Compiler;

namespace Windows.Win32;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.275+02bc0c298e.RR")]
public static class InlineArrayIndexerExtensions
{
	public static ReadOnlySpan<char> SliceAtNull(this ReadOnlySpan<char> value)
	{
		int num = value.IndexOf('\0');
		if (num >= 0)
		{
			return value.Slice(0, num);
		}
		return value;
	}

	public unsafe static ref readonly char ReadOnlyItemRef(this in __char_32 @this, int index)
	{
		return ref @this.Value[index];
	}

	public unsafe static ref readonly char ReadOnlyItemRef(this in __char_64 @this, int index)
	{
		return ref @this.Value[index];
	}

	public unsafe static ref readonly char ReadOnlyItemRef(this in __char_128 @this, int index)
	{
		return ref @this.Value[index];
	}
}
