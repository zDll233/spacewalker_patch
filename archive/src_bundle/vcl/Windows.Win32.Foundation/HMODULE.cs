using System;
using System.CodeDom.Compiler;
using System.Diagnostics;

namespace Windows.Win32.Foundation;

[DebuggerDisplay("{Value}")]
[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.275+02bc0c298e.RR")]
public readonly struct HMODULE : IEquatable<HMODULE>
{
	public unsafe readonly void* Value;

	public static HMODULE Null => default(HMODULE);

	public unsafe bool IsNull => Value == null;

	public unsafe HMODULE(void* value)
	{
		Value = value;
	}

	public unsafe HMODULE(IntPtr value)
		: this((void*)value)
	{
	}

	public unsafe static implicit operator void*(HMODULE value)
	{
		return value.Value;
	}

	public unsafe static explicit operator HMODULE(void* value)
	{
		return new HMODULE(value);
	}

	public unsafe static bool operator ==(HMODULE left, HMODULE right)
	{
		return left.Value == right.Value;
	}

	public static bool operator !=(HMODULE left, HMODULE right)
	{
		return !(left == right);
	}

	public unsafe bool Equals(HMODULE other)
	{
		return Value == other.Value;
	}

	public override bool Equals(object obj)
	{
		if (obj is HMODULE other)
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

	public unsafe static implicit operator IntPtr(HMODULE value)
	{
		return new IntPtr(value.Value);
	}

	public unsafe static explicit operator HMODULE(IntPtr value)
	{
		return new HMODULE(value.ToPointer());
	}

	public unsafe static explicit operator HMODULE(UIntPtr value)
	{
		return new HMODULE(value.ToPointer());
	}

	public unsafe static implicit operator HINSTANCE(HMODULE value)
	{
		return new HINSTANCE(value.Value);
	}
}
