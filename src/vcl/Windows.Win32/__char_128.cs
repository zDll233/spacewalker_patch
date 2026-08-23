using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Windows.Win32;

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
public struct __char_128
{
	private const int SpanLength = 128;

	public unsafe fixed char Value[128];

	public readonly int Length => 128;

	[UnscopedRef]
	public unsafe ref char this[int index] => ref Value[index];

	public unsafe readonly void CopyTo(Span<char> target, int length = 128)
	{
		fixed (char* pointer = Value)
		{
			ReadOnlySpan<char> readOnlySpan = new ReadOnlySpan<char>(pointer, 128);
			readOnlySpan = readOnlySpan.Slice(0, length);
			readOnlySpan.CopyTo(target);
		}
	}

	public unsafe readonly char[] ToArray(int length = 128)
	{
		fixed (char* pointer = Value)
		{
			ReadOnlySpan<char> readOnlySpan = new ReadOnlySpan<char>(pointer, 128);
			readOnlySpan = readOnlySpan.Slice(0, length);
			return readOnlySpan.ToArray();
		}
	}

	public unsafe readonly bool Equals(ReadOnlySpan<char> value)
	{
		fixed (char* pointer = Value)
		{
			ReadOnlySpan<char> readOnlySpan = new ReadOnlySpan<char>(pointer, 128);
			if (value.Length != 128)
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
			ReadOnlySpan<char> readOnlySpan = new ReadOnlySpan<char>(pointer, 128);
			readOnlySpan = readOnlySpan.Slice(0, length);
			return readOnlySpan.ToString();
		}
	}

	public unsafe override readonly string ToString()
	{
		fixed (char* pointer = Value)
		{
			return new ReadOnlySpan<char>(pointer, 128).SliceAtNull().ToString();
		}
	}

	public static implicit operator __char_128(string value)
	{
		return value.AsSpan();
	}

	public unsafe static implicit operator __char_128(ReadOnlySpan<char> value)
	{
		__char_128 result = default(__char_128);
		Span<char> destination = new Span<char>(result.Value, 128);
		value.CopyTo(destination);
		int length = value.Length;
		destination.Slice(length, 128 - length).Clear();
		return result;
	}
}
