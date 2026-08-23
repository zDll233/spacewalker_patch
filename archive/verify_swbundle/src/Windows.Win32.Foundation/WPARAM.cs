using System;
using System.CodeDom.Compiler;
using System.Diagnostics;

namespace Windows.Win32.Foundation;

[DebuggerDisplay("{Value}")]
[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.275+02bc0c298e.RR")]
public readonly struct WPARAM : IEquatable<WPARAM>
{
	public readonly nuint Value;

	public WPARAM(nuint value)
	{
		Value = value;
	}

	public static implicit operator nuint(WPARAM value)
	{
		return value.Value;
	}

	public static implicit operator WPARAM(nuint value)
	{
		return new WPARAM(value);
	}

	public static bool operator ==(WPARAM left, WPARAM right)
	{
		return left.Value == right.Value;
	}

	public static bool operator !=(WPARAM left, WPARAM right)
	{
		return !(left == right);
	}

	public bool Equals(WPARAM other)
	{
		return Value == other.Value;
	}

	public override bool Equals(object obj)
	{
		if (obj is WPARAM other)
		{
			return Equals(other);
		}
		return false;
	}

	public override int GetHashCode()
	{
		nuint value = Value;
		return ((UIntPtr)value).GetHashCode();
	}

	public override string ToString()
	{
		return $"0x{Value:x}";
	}
}
