using System;
using System.CodeDom.Compiler;
using System.Diagnostics;

namespace Windows.Win32.UI.WindowsAndMessaging;

[DebuggerDisplay("{Value}")]
[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.275+02bc0c298e.RR")]
public readonly struct HICON : IEquatable<HICON>
{
	public unsafe readonly void* Value;

	public static HICON Null => default(HICON);

	public unsafe bool IsNull => Value == null;

	public unsafe HICON(void* value)
	{
		Value = value;
	}

	public unsafe HICON(IntPtr value)
		: this((void*)value)
	{
	}

	public unsafe static implicit operator void*(HICON value)
	{
		return value.Value;
	}

	public unsafe static explicit operator HICON(void* value)
	{
		return new HICON(value);
	}

	public unsafe static bool operator ==(HICON left, HICON right)
	{
		return left.Value == right.Value;
	}

	public static bool operator !=(HICON left, HICON right)
	{
		return !(left == right);
	}

	public unsafe bool Equals(HICON other)
	{
		return Value == other.Value;
	}

	public override bool Equals(object obj)
	{
		if (obj is HICON other)
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

	public unsafe static implicit operator IntPtr(HICON value)
	{
		return new IntPtr(value.Value);
	}

	public unsafe static explicit operator HICON(IntPtr value)
	{
		return new HICON(value.ToPointer());
	}

	public unsafe static explicit operator HICON(UIntPtr value)
	{
		return new HICON(value.ToPointer());
	}
}
