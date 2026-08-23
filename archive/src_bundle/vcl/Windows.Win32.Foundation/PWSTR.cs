using System;
using System.CodeDom.Compiler;
using System.Diagnostics;

namespace Windows.Win32.Foundation;

[DebuggerDisplay("{DebuggerDisplay,nq}")]
[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.275+02bc0c298e.RR")]
public readonly struct PWSTR : IEquatable<PWSTR>
{
	public unsafe readonly char* Value;

	public unsafe int Length => new PCWSTR(Value).Length;

	private string DebuggerDisplay => ToString();

	public unsafe PWSTR(char* value)
	{
		Value = value;
	}

	public unsafe PWSTR(IntPtr value)
		: this((char*)(void*)value)
	{
	}

	public unsafe static implicit operator char*(PWSTR value)
	{
		return value.Value;
	}

	public unsafe static implicit operator PWSTR(char* value)
	{
		return new PWSTR(value);
	}

	public unsafe static bool operator ==(PWSTR left, PWSTR right)
	{
		return left.Value == right.Value;
	}

	public static bool operator !=(PWSTR left, PWSTR right)
	{
		return !(left == right);
	}

	public unsafe bool Equals(PWSTR other)
	{
		return Value == other.Value;
	}

	public override bool Equals(object obj)
	{
		if (obj is PWSTR other)
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
		return new PCWSTR(Value).ToString();
	}

	public unsafe static implicit operator PCWSTR(PWSTR value)
	{
		return new PCWSTR(value.Value);
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
