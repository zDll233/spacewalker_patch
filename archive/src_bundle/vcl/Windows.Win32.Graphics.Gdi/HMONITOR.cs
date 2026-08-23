using System;
using System.CodeDom.Compiler;
using System.Diagnostics;

namespace Windows.Win32.Graphics.Gdi;

[DebuggerDisplay("{Value}")]
[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.275+02bc0c298e.RR")]
public readonly struct HMONITOR : IEquatable<HMONITOR>
{
	public unsafe readonly void* Value;

	public static HMONITOR Null => default(HMONITOR);

	public unsafe bool IsNull => Value == null;

	public unsafe HMONITOR(void* value)
	{
		Value = value;
	}

	public unsafe HMONITOR(IntPtr value)
		: this((void*)value)
	{
	}

	public unsafe static implicit operator void*(HMONITOR value)
	{
		return value.Value;
	}

	public unsafe static explicit operator HMONITOR(void* value)
	{
		return new HMONITOR(value);
	}

	public unsafe static bool operator ==(HMONITOR left, HMONITOR right)
	{
		return left.Value == right.Value;
	}

	public static bool operator !=(HMONITOR left, HMONITOR right)
	{
		return !(left == right);
	}

	public unsafe bool Equals(HMONITOR other)
	{
		return Value == other.Value;
	}

	public override bool Equals(object obj)
	{
		if (obj is HMONITOR other)
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

	public unsafe static implicit operator IntPtr(HMONITOR value)
	{
		return new IntPtr(value.Value);
	}

	public unsafe static explicit operator HMONITOR(IntPtr value)
	{
		return new HMONITOR(value.ToPointer());
	}

	public unsafe static explicit operator HMONITOR(UIntPtr value)
	{
		return new HMONITOR(value.ToPointer());
	}
}
