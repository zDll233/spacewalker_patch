using System;
using System.CodeDom.Compiler;
using System.Diagnostics;

namespace Windows.Win32.UI.WindowsAndMessaging;

[DebuggerDisplay("{Value}")]
[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.275+02bc0c298e.RR")]
public readonly struct HCURSOR : IEquatable<HCURSOR>
{
	public unsafe readonly void* Value;

	public static HCURSOR Null => default(HCURSOR);

	public unsafe bool IsNull => Value == null;

	public unsafe HCURSOR(void* value)
	{
		Value = value;
	}

	public unsafe HCURSOR(IntPtr value)
		: this((void*)value)
	{
	}

	public unsafe static implicit operator void*(HCURSOR value)
	{
		return value.Value;
	}

	public unsafe static explicit operator HCURSOR(void* value)
	{
		return new HCURSOR(value);
	}

	public unsafe static bool operator ==(HCURSOR left, HCURSOR right)
	{
		return left.Value == right.Value;
	}

	public static bool operator !=(HCURSOR left, HCURSOR right)
	{
		return !(left == right);
	}

	public unsafe bool Equals(HCURSOR other)
	{
		return Value == other.Value;
	}

	public override bool Equals(object obj)
	{
		if (obj is HCURSOR other)
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

	public unsafe static implicit operator IntPtr(HCURSOR value)
	{
		return new IntPtr(value.Value);
	}

	public unsafe static explicit operator HCURSOR(IntPtr value)
	{
		return new HCURSOR(value.ToPointer());
	}

	public unsafe static explicit operator HCURSOR(UIntPtr value)
	{
		return new HCURSOR(value.ToPointer());
	}

	public unsafe static implicit operator HICON(HCURSOR value)
	{
		return new HICON(value.Value);
	}
}
