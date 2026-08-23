using System;
using System.Buffers.Binary;

namespace VitureCommonLibrary;

public readonly ref struct R6LongResponse
{
	private readonly ReadOnlySpan<byte> _data;

	public byte APP_SEQ => _data[0];

	public ushort TOTAL_SEG_NUM => BinaryPrimitives.ReadUInt16LittleEndian(_data.Slice(1, 2));

	public ushort CURRENT_SEG_NUM => BinaryPrimitives.ReadUInt16LittleEndian(_data.Slice(3, 2));

	public ReadOnlySpan<byte> PayloadTake(int len)
	{
		return _data.Slice(5, len);
	}

	public R6LongResponse(ReadOnlySpan<byte> data)
	{
		_data = default(ReadOnlySpan<byte>);
		if (data.Length >= 56)
		{
			_data = data;
		}
	}
}
