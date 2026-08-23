using System;
using System.Buffers.Binary;

namespace VitureCommonLibrary;

internal static class R6LongPacketParser
{
	public enum FirstSegResult
	{
		Ok,
		PayloadTooShort,
		Unsupported,
		NotFlashed,
		BogusTotalSegNum,
		WrongCurSegNum
	}

	public const int LongResponseHeaderSize = 5;

	public const int LengthCrcHeaderSize = 6;

	public const int FullPayloadSize = 56;

	public const int MaxPayloadPerSegment = 51;

	public static FirstSegResult TryParseFirstSegment(ReadOnlySpan<byte> payload56, out ushort totalSegNum, out ushort curSegNum, out uint length, out ushort crc)
	{
		totalSegNum = 0;
		curSegNum = 0;
		length = 0u;
		crc = 0;
		if (payload56.Length < 11)
		{
			return FirstSegResult.PayloadTooShort;
		}
		totalSegNum = BinaryPrimitives.ReadUInt16LittleEndian(payload56.Slice(1, 2));
		curSegNum = BinaryPrimitives.ReadUInt16LittleEndian(payload56.Slice(3, 2));
		if (totalSegNum == 0)
		{
			return FirstSegResult.Unsupported;
		}
		if (totalSegNum == ushort.MaxValue)
		{
			return FirstSegResult.NotFlashed;
		}
		if (totalSegNum > 4096)
		{
			return FirstSegResult.BogusTotalSegNum;
		}
		if (curSegNum != 0)
		{
			return FirstSegResult.WrongCurSegNum;
		}
		length = BinaryPrimitives.ReadUInt32LittleEndian(payload56.Slice(5, 4));
		crc = BinaryPrimitives.ReadUInt16LittleEndian(payload56.Slice(9, 2));
		return FirstSegResult.Ok;
	}

	public static bool VerifyChecksum(ReadOnlySpan<byte> buffer, uint length, ushort crcExpected, out ushort actual)
	{
		actual = 0;
		if (buffer.Length < 6 + length)
		{
			return false;
		}
		uint num = 0u;
		for (int i = 0; i < length; i++)
		{
			num += buffer[6 + i];
		}
		actual = (ushort)num;
		return actual == crcExpected;
	}
}
