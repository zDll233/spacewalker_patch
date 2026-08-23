using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace VitureCommonLibrary;

[Serializable]
public class UsbOtaMessage
{
	public const ushort CONST_HEADER = 22010;

	public ushort Header = 22010;

	public byte Cmd;

	public ushort Len;

	public byte[] Data;

	public UsbOtaMessage()
	{
		Cmd = 0;
		Len = 0;
		Data = new byte[0];
	}

	public byte[] ToBytes()
	{
		int num = Marshal.SizeOf<ushort>() + Marshal.SizeOf<byte>() + Marshal.SizeOf<ushort>();
		byte[] data = Data;
		byte[] array = new byte[num + ((data != null) ? data.Length : 0)];
		using (MemoryStream output = new MemoryStream(array))
		{
			using BinaryWriter binaryWriter = new BinaryWriter(output);
			binaryWriter.Write(Header);
			binaryWriter.Write(Cmd);
			binaryWriter.Write(BitConverter.IsLittleEndian ? BitConverter.GetBytes(Len).Reverse().ToArray() : BitConverter.GetBytes(Len));
			if (Data != null && Data.Length != 0)
			{
				binaryWriter.Write(Data);
			}
		}
		return array;
	}
}
