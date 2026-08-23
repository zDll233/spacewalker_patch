using System;
using System.CodeDom.Compiler;
using System.Diagnostics;

namespace Windows.Win32.Foundation;

[DebuggerDisplay("{Value}")]
[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.275+02bc0c298e.RR")]
public readonly struct HANDLE : IEquatable<HANDLE>
{
	public unsafe readonly void* Value;

	public static HANDLE Null => default(HANDLE);

	public unsafe bool IsNull => Value == null;

	public unsafe HANDLE(void* value)
	{
		Value = value;
	}

	public unsafe HANDLE(IntPtr value)
		: this((void*)value)
	{
	}

	public unsafe static implicit operator void*(HANDLE value)
	{
		return value.Value;
	}

	public unsafe static explicit operator HANDLE(void* value)
	{
		return new HANDLE(value);
	}

	public unsafe static bool operator ==(HANDLE left, HANDLE right)
	{
		return left.Value == right.Value;
	}

	public static bool operator !=(HANDLE left, HANDLE right)
	{
		return !(left == right);
	}

	public unsafe bool Equals(HANDLE other)
	{
		return Value == other.Value;
	}

	public override bool Equals(object obj)
	{
		if (obj is HANDLE other)
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

	public unsafe static implicit operator IntPtr(HANDLE value)
	{
		return new IntPtr(value.Value);
	}

	public unsafe static explicit operator HANDLE(IntPtr value)
	{
		return new HANDLE(value.ToPointer());
	}

	public unsafe static explicit operator HANDLE(UIntPtr value)
	{
		return new HANDLE(value.ToPointer());
	}
}
