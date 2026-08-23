using System;
using System.CodeDom.Compiler;
using System.Diagnostics;

namespace Windows.Win32.Foundation;

[DebuggerDisplay("{Value}")]
[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.275+02bc0c298e.RR")]
public readonly struct HWND : IEquatable<HWND>
{
	public unsafe readonly void* Value;

	public static readonly HWND HWND_BROADCAST = (HWND)(IntPtr)65535;

	public static HWND Null => default(HWND);

	public unsafe bool IsNull => Value == null;

	public unsafe HWND(void* value)
	{
		Value = value;
	}

	public unsafe HWND(IntPtr value)
		: this((void*)value)
	{
	}

	public unsafe static implicit operator void*(HWND value)
	{
		return value.Value;
	}

	public unsafe static explicit operator HWND(void* value)
	{
		return new HWND(value);
	}

	public unsafe static bool operator ==(HWND left, HWND right)
	{
		return left.Value == right.Value;
	}

	public static bool operator !=(HWND left, HWND right)
	{
		return !(left == right);
	}

	public unsafe bool Equals(HWND other)
	{
		return Value == other.Value;
	}

	public override bool Equals(object obj)
	{
		if (obj is HWND other)
		{
			return Equals(other);
		}
		return false;
	}

	public unsafe override int GetHashCode()
	{
		return (int)Value;
	}

	public unsafe override string ToString()
	{
		return $"0x{(nuint)Value:x}";
	}

	public unsafe static implicit operator IntPtr(HWND value)
	{
		return new IntPtr(value.Value);
	}

	public unsafe static explicit operator HWND(IntPtr value)
	{
		return new HWND(value.ToPointer());
	}

	public unsafe static explicit operator HWND(UIntPtr value)
	{
		return new HWND(value.ToPointer());
	}

	public unsafe static implicit operator HANDLE(HWND value)
	{
		return new HANDLE(value.Value);
	}
}
