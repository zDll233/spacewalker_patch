using System;
using System.Buffers.Binary;
using System.Runtime.InteropServices;

namespace VitureCommonLibrary;

public readonly ref struct R6CameraParam
{
	private readonly ReadOnlySpan<byte> _data;

	public const int TotalSize = 136;

	public uint Length => BinaryPrimitives.ReadUInt32LittleEndian(_data.Slice(0, 4));

	public ushort Crc => BinaryPrimitives.ReadUInt16LittleEndian(_data.Slice(4, 2));

	public ushort Version => BinaryPrimitives.ReadUInt16LittleEndian(_data.Slice(6, 2));

	public uint Width => BinaryPrimitives.ReadUInt32LittleEndian(_data.Slice(8, 4));

	public uint Height => BinaryPrimitives.ReadUInt32LittleEndian(_data.Slice(12, 4));

	public ReadOnlySpan<float> QCamImu => MemoryMarshal.Cast<byte, float>(_data.Slice(16, 16));

	public ReadOnlySpan<float> TCamImu => MemoryMarshal.Cast<byte, float>(_data.Slice(32, 12));

	public uint CameraModel => BinaryPrimitives.ReadUInt32LittleEndian(_data.Slice(44, 4));

	public ReadOnlySpan<float> Intrinsics => MemoryMarshal.Cast<byte, float>(_data.Slice(48, 24));

	public uint DistortionModel => BinaryPrimitives.ReadUInt32LittleEndian(_data.Slice(72, 4));

	public ReadOnlySpan<float> DistortionCoeffs => MemoryMarshal.Cast<byte, float>(_data.Slice(76, 56));

	public float Timeshift => MemoryMarshal.Read<float>(_data.Slice(132, 4));

	public R6CameraParam(ReadOnlySpan<byte> data)
	{
		if (data.Length < 136)
		{
			throw new ArgumentException("Need 136 bytes");
		}
		_data = data;
	}
}
