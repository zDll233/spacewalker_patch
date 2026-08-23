using System;
using System.IO;

namespace VitureCommonLibrary;

[Serializable]
public class UsbOtaMessageAck
{
	public const ushort CONST_HEADER = 22010;

	public ushort Header = 22010;

	public byte Cmd;

	public byte Status;

	public ushort Len;

	public byte[] Data;

	public UsbOtaMessageAck()
	{
		Cmd = 0;
		Status = 0;
		Len = 0;
		Data = new byte[0];
	}

	public static UsbOtaMessageAck FromBytes(byte[] bytes)
	{
		if (bytes == null || bytes.Length < 6)
		{
			throw new ArgumentException("Invalid byte array");
		}
		UsbOtaMessageAck usbOtaMessageAck = new UsbOtaMessageAck();
		using (MemoryStream input = new MemoryStream(bytes))
		{
			using BinaryReader binaryReader = new BinaryReader(input);
			usbOtaMessageAck.Header = binaryReader.ReadUInt16();
			usbOtaMessageAck.Cmd = binaryReader.ReadByte();
			usbOtaMessageAck.Status = binaryReader.ReadByte();
			byte[] array = binaryReader.ReadBytes(2);
			if (BitConverter.IsLittleEndian)
			{
				Array.Reverse((Array)array);
			}
			usbOtaMessageAck.Len = BitConverter.ToUInt16(array, 0);
			int num = bytes.Length - 6;
			if (num > 0)
			{
				usbOtaMessageAck.Data = binaryReader.ReadBytes(num);
			}
			else
			{
				usbOtaMessageAck.Data = new byte[0];
			}
		}
		return usbOtaMessageAck;
	}
}
