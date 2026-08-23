using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Windows.Win32;

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
public struct __char_64
{
	private const int SpanLength = 64;

	public unsafe fixed char Value[64];

	public readonly int Length => 64;

	[UnscopedRef]
	public unsafe ref char this[int index] => ref Value[index];

	public unsafe readonly void CopyTo(Span<char> target, int length = 64)
	{
		fixed (char* pointer = Value)
		{
			ReadOnlySpan<char> readOnlySpan = new ReadOnlySpan<char>(pointer, 64);
			readOnlySpan = readOnlySpan.Slice(0, length);
			readOnlySpan.CopyTo(target);
		}
	}

	public unsafe readonly char[] ToArray(int length = 64)
	{
		fixed (char* pointer = Value)
		{
			ReadOnlySpan<char> readOnlySpan = new ReadOnlySpan<char>(pointer, 64);
			readOnlySpan = readOnlySpan.Slice(0, length);
			return readOnlySpan.ToArray();
		}
	}

	public unsafe readonly bool Equals(ReadOnlySpan<char> value)
	{
		fixed (char* pointer = Value)
		{
			ReadOnlySpan<char> readOnlySpan = new ReadOnlySpan<char>(pointer, 64);
			if (value.Length != 64)
			{
				return readOnlySpan.SliceAtNull().SequenceEqual(value);
			}
			return readOnlySpan.SequenceEqual(value);
		}
	}

	public readonly bool Equals(string value)
	{
		return Equals(value.AsSpan());
	}

	public unsafe readonly string ToString(int length)
	{
		fixed (char* pointer = Value)
		{
			ReadOnlySpan<char> readOnlySpan = new ReadOnlySpan<char>(pointer, 64);
			readOnlySpan = readOnlySpan.Slice(0, length);
			return readOnlySpan.ToString();
		}
	}

	public unsafe override readonly string ToString()
	{
		fixed (char* pointer = Value)
		{
			return new ReadOnlySpan<char>(pointer, 64).SliceAtNull().ToString();
		}
	}

	public static implicit operator __char_64(string value)
	{
		return value.AsSpan();
	}

	public unsafe static implicit operator __char_64(ReadOnlySpan<char> value)
	{
		__char_64 result = default(__char_64);
		Span<char> destination = new Span<char>(result.Value, 64);
		value.CopyTo(destination);
		int length = value.Length;
		destination.Slice(length, 64 - length).Clear();
		return result;
	}
}
