using System;
using System.CodeDom.Compiler;
using System.Diagnostics;

namespace Windows.Win32.UI.WindowsAndMessaging;

[DebuggerDisplay("{Value}")]
[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.275+02bc0c298e.RR")]
public readonly struct HHOOK : IEquatable<HHOOK>
{
	public unsafe readonly void* Value;

	public static HHOOK Null => default(HHOOK);

	public unsafe bool IsNull => Value == null;

	public unsafe HHOOK(void* value)
	{
		Value = value;
	}

	public unsafe HHOOK(IntPtr value)
		: this((void*)value)
	{
	}

	public unsafe static implicit operator void*(HHOOK value)
	{
		return value.Value;
	}

	public unsafe static explicit operator HHOOK(void* value)
	{
		return new HHOOK(value);
	}

	public unsafe static bool operator ==(HHOOK left, HHOOK right)
	{
		return left.Value == right.Value;
	}

	public static bool operator !=(HHOOK left, HHOOK right)
	{
		return !(left == right);
	}

	public unsafe bool Equals(HHOOK other)
	{
		return Value == other.Value;
	}

	public override bool Equals(object obj)
	{
		if (obj is HHOOK other)
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

	public unsafe static implicit operator IntPtr(HHOOK value)
	{
		return new IntPtr(value.Value);
	}

	public unsafe static explicit operator HHOOK(IntPtr value)
	{
		return new HHOOK(value.ToPointer());
	}

	public unsafe static explicit operator HHOOK(UIntPtr value)
	{
		return new HHOOK(value.ToPointer());
	}
}
