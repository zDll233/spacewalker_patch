using System;
using System.Runtime.InteropServices;

namespace VitureCommonLibrary;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct GetYAMLConfigCmd
{
	public ushort Version;

	public uint Offset;

	public ushort Len;

	public uint TotalLen;

	public byte[] ToBytes()
	{
		byte[] array = new byte[Marshal.SizeOf(this)];
		byte[] bytes = BitConverter.GetBytes(Version);
		byte[] bytes2 = BitConverter.GetBytes(Offset);
		byte[] bytes3 = BitConverter.GetBytes(Len);
		byte[] bytes4 = BitConverter.GetBytes(TotalLen);
		Array.Reverse((Array)bytes);
		Array.Reverse((Array)bytes2);
		Array.Reverse((Array)bytes3);
		Array.Reverse((Array)bytes4);
		int num = 0;
		Array.Copy(bytes, 0, array, num, bytes.Length);
		num += bytes.Length;
		Array.Copy(bytes2, 0, array, num, bytes2.Length);
		num += bytes2.Length;
		Array.Copy(bytes3, 0, array, num, bytes3.Length);
		num += bytes3.Length;
		Array.Copy(bytes4, 0, array, num, bytes4.Length);
		num += bytes4.Length;
		return array;
	}
}
