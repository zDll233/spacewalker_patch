using System;
using System.CodeDom.Compiler;
using System.Diagnostics;

namespace Windows.Win32.UI.WindowsAndMessaging;

[DebuggerDisplay("{Value}")]
[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.275+02bc0c298e.RR")]
public readonly struct HMENU : IEquatable<HMENU>
{
	public unsafe readonly void* Value;

	public static HMENU Null => default(HMENU);

	public unsafe bool IsNull => Value == null;

	public unsafe HMENU(void* value)
	{
		Value = value;
	}

	public unsafe HMENU(IntPtr value)
		: this((void*)value)
	{
	}

	public unsafe static implicit operator void*(HMENU value)
	{
		return value.Value;
	}

	public unsafe static explicit operator HMENU(void* value)
	{
		return new HMENU(value);
	}

	public unsafe static bool operator ==(HMENU left, HMENU right)
	{
		return left.Value == right.Value;
	}

	public static bool operator !=(HMENU left, HMENU right)
	{
		return !(left == right);
	}

	public unsafe bool Equals(HMENU other)
	{
		return Value == other.Value;
	}

	public override bool Equals(object obj)
	{
		if (obj is HMENU other)
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

	public unsafe static implicit operator IntPtr(HMENU value)
	{
		return new IntPtr(value.Value);
	}

	public unsafe static explicit operator HMENU(IntPtr value)
	{
		return new HMENU(value.ToPointer());
	}

	public unsafe static explicit operator HMENU(UIntPtr value)
	{
		return new HMENU(value.ToPointer());
	}
}
