using System;
using System.CodeDom.Compiler;
using System.Diagnostics;

namespace Windows.Win32.Foundation;

[DebuggerDisplay("{Value}")]
[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.275+02bc0c298e.RR")]
public readonly struct HINSTANCE : IEquatable<HINSTANCE>
{
	public unsafe readonly void* Value;

	public static HINSTANCE Null => default(HINSTANCE);

	public unsafe bool IsNull => Value == null;

	public unsafe HINSTANCE(void* value)
	{
		Value = value;
	}

	public unsafe HINSTANCE(IntPtr value)
		: this((void*)value)
	{
	}

	public unsafe static implicit operator void*(HINSTANCE value)
	{
		return value.Value;
	}

	public unsafe static explicit operator HINSTANCE(void* value)
	{
		return new HINSTANCE(value);
	}

	public unsafe static bool operator ==(HINSTANCE left, HINSTANCE right)
	{
		return left.Value == right.Value;
	}

	public static bool operator !=(HINSTANCE left, HINSTANCE right)
	{
		return !(left == right);
	}

	public unsafe bool Equals(HINSTANCE other)
	{
		return Value == other.Value;
	}

	public override bool Equals(object obj)
	{
		if (obj is HINSTANCE other)
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

	public unsafe static implicit operator IntPtr(HINSTANCE value)
	{
		return new IntPtr(value.Value);
	}

	public unsafe static explicit operator HINSTANCE(IntPtr value)
	{
		return new HINSTANCE(value.ToPointer());
	}

	public unsafe static explicit operator HINSTANCE(UIntPtr value)
	{
		return new HINSTANCE(value.ToPointer());
	}

	public unsafe static implicit operator HMODULE(HINSTANCE value)
	{
		return new HMODULE(value.Value);
	}
}
