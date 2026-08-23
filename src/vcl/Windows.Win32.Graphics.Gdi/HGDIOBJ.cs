using System;
using System.CodeDom.Compiler;
using System.Diagnostics;

namespace Windows.Win32.Graphics.Gdi;

[DebuggerDisplay("{Value}")]
[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.275+02bc0c298e.RR")]
public readonly struct HGDIOBJ : IEquatable<HGDIOBJ>
{
	public unsafe readonly void* Value;

	public static HGDIOBJ Null => default(HGDIOBJ);

	public unsafe bool IsNull => Value == null;

	public unsafe HGDIOBJ(void* value)
	{
		Value = value;
	}

	public unsafe HGDIOBJ(IntPtr value)
		: this((void*)value)
	{
	}

	public unsafe static implicit operator void*(HGDIOBJ value)
	{
		return value.Value;
	}

	public unsafe static explicit operator HGDIOBJ(void* value)
	{
		return new HGDIOBJ(value);
	}

	public unsafe static bool operator ==(HGDIOBJ left, HGDIOBJ right)
	{
		return left.Value == right.Value;
	}

	public static bool operator !=(HGDIOBJ left, HGDIOBJ right)
	{
		return !(left == right);
	}

	public unsafe bool Equals(HGDIOBJ other)
	{
		return Value == other.Value;
	}

	public override bool Equals(object obj)
	{
		if (obj is HGDIOBJ other)
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

	public unsafe static implicit operator IntPtr(HGDIOBJ value)
	{
		return new IntPtr(value.Value);
	}

	public unsafe static explicit operator HGDIOBJ(IntPtr value)
	{
		return new HGDIOBJ(value.ToPointer());
	}

	public unsafe static explicit operator HGDIOBJ(UIntPtr value)
	{
		return new HGDIOBJ(value.ToPointer());
	}
}
