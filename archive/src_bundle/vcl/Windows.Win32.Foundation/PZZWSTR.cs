using System;
using System.CodeDom.Compiler;
using System.Diagnostics;

namespace Windows.Win32.Foundation;

[DebuggerDisplay("{DebuggerDisplay}")]
[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.275+02bc0c298e.RR")]
public readonly struct PZZWSTR : IEquatable<PZZWSTR>
{
	public unsafe readonly char* Value;

	public unsafe int Length => new PCZZWSTR(Value).Length;

	private string DebuggerDisplay => ToString();

	public unsafe PZZWSTR(char* value)
	{
		Value = value;
	}

	public unsafe static explicit operator char*(PZZWSTR value)
	{
		return value.Value;
	}

	public unsafe static implicit operator PZZWSTR(char* value)
	{
		return new PZZWSTR(value);
	}

	public unsafe static implicit operator PCZZWSTR(PZZWSTR value)
	{
		return new PCZZWSTR(value.Value);
	}

	public unsafe bool Equals(PZZWSTR other)
	{
		return Value == other.Value;
	}

	public override bool Equals(object obj)
	{
		if (obj is PZZWSTR other)
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
		return new PCZZWSTR(Value).ToString();
	}

	public unsafe Span<char> AsSpan()
	{
		if (Value != null)
		{
			return new Span<char>(Value, Length);
		}
		return default(Span<char>);
	}
}
