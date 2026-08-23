using System;
using System.CodeDom.Compiler;
using System.Diagnostics;

namespace Windows.Win32.Graphics.Gdi;

[DebuggerDisplay("{Value}")]
[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.275+02bc0c298e.RR")]
public readonly struct HBRUSH : IEquatable<HBRUSH>
{
	public unsafe readonly void* Value;

	public static HBRUSH Null => default(HBRUSH);

	public unsafe bool IsNull => Value == null;

	public unsafe HBRUSH(void* value)
	{
		Value = value;
	}

	public unsafe HBRUSH(IntPtr value)
		: this((void*)value)
	{
	}

	public unsafe static implicit operator void*(HBRUSH value)
	{
		return value.Value;
	}

	public unsafe static explicit operator HBRUSH(void* value)
	{
		return new HBRUSH(value);
	}

	public unsafe static bool operator ==(HBRUSH left, HBRUSH right)
	{
		return left.Value == right.Value;
	}

	public static bool operator !=(HBRUSH left, HBRUSH right)
	{
		return !(left == right);
	}

	public unsafe bool Equals(HBRUSH other)
	{
		return Value == other.Value;
	}

	public override bool Equals(object obj)
	{
		if (obj is HBRUSH other)
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

	public unsafe static implicit operator IntPtr(HBRUSH value)
	{
		return new IntPtr(value.Value);
	}

	public unsafe static explicit operator HBRUSH(IntPtr value)
	{
		return new HBRUSH(value.ToPointer());
	}

	public unsafe static explicit operator HBRUSH(UIntPtr value)
	{
		return new HBRUSH(value.ToPointer());
	}

	public unsafe static implicit operator HGDIOBJ(HBRUSH value)
	{
		return new HGDIOBJ(value.Value);
	}
}
