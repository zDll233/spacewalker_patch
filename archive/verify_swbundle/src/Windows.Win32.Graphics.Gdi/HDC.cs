using System;
using System.CodeDom.Compiler;
using System.Diagnostics;

namespace Windows.Win32.Graphics.Gdi;

[DebuggerDisplay("{Value}")]
[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.275+02bc0c298e.RR")]
public readonly struct HDC : IEquatable<HDC>
{
	public unsafe readonly void* Value;

	public static HDC Null => default(HDC);

	public unsafe bool IsNull => Value == null;

	public unsafe HDC(void* value)
	{
		Value = value;
	}

	public unsafe HDC(IntPtr value)
		: this((void*)value)
	{
	}

	public unsafe static implicit operator void*(HDC value)
	{
		return value.Value;
	}

	public unsafe static explicit operator HDC(void* value)
	{
		return new HDC(value);
	}

	public unsafe static bool operator ==(HDC left, HDC right)
	{
		return left.Value == right.Value;
	}

	public static bool operator !=(HDC left, HDC right)
	{
		return !(left == right);
	}

	public unsafe bool Equals(HDC other)
	{
		return Value == other.Value;
	}

	public override bool Equals(object obj)
	{
		if (obj is HDC other)
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

	public unsafe static implicit operator IntPtr(HDC value)
	{
		return new IntPtr(value.Value);
	}

	public unsafe static explicit operator HDC(IntPtr value)
	{
		return new HDC(value.ToPointer());
	}

	public unsafe static explicit operator HDC(UIntPtr value)
	{
		return new HDC(value.ToPointer());
	}
}
