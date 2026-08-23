using System;
using System.CodeDom.Compiler;
using System.Diagnostics;

namespace Windows.Win32.Foundation;

[DebuggerDisplay("{DebuggerDisplay}")]
[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.275+02bc0c298e.RR")]
public readonly struct PCZZWSTR : IEquatable<PCZZWSTR>
{
	public unsafe readonly char* Value;

	public unsafe int Length
	{
		get
		{
			PCWSTR pCWSTR = new PCWSTR(Value);
			while (true)
			{
				int length = pCWSTR.Length;
				if (length <= 0)
				{
					break;
				}
				pCWSTR = new PCWSTR(pCWSTR.Value + length + 1);
			}
			return checked((int)(pCWSTR.Value - Value));
		}
	}

	private string DebuggerDisplay => ToString();

	public unsafe PCZZWSTR(char* value)
	{
		Value = value;
	}

	public unsafe static explicit operator char*(PCZZWSTR value)
	{
		return value.Value;
	}

	public unsafe static implicit operator PCZZWSTR(char* value)
	{
		return new PCZZWSTR(value);
	}

	public unsafe bool Equals(PCZZWSTR other)
	{
		return Value == other.Value;
	}

	public override bool Equals(object obj)
	{
		if (obj is PCZZWSTR other)
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
		if (Value != null)
		{
			return new string(Value, 0, Length);
		}
		return null;
	}

	public unsafe ReadOnlySpan<char> AsSpan()
	{
		if (Value != null)
		{
			return new ReadOnlySpan<char>(Value, Length);
		}
		return default(ReadOnlySpan<char>);
	}
}
