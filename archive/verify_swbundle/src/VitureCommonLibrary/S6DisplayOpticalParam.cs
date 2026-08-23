using System;
using System.Buffers.Binary;
using System.Runtime.InteropServices;

namespace VitureCommonLibrary;

public sealed class S6DisplayOpticalParam
{
	public const int ExpectedContentLength = 322;

	public const int MinUsableBufferLength = 200;

	public uint Length { get; set; }

	public ushort Crc { get; set; }

	public ushort Version { get; set; }

	public uint Width { get; set; }

	public uint Height { get; set; }

	public float[] QLeftImu { get; set; } = new float[4];


	public float[] TLeftImu { get; set; } = new float[3];


	public float[] QRightImu { get; set; } = new float[4];


	public float[] TRightImu { get; set; } = new float[3];


	public float[] InnerLeftIntrinsics { get; set; } = new float[8];


	public float[] OuterLeftIntrinsics { get; set; } = new float[8];


	public float[] InnerRightIntrinsics { get; set; } = new float[8];


	public float[] OuterRightIntrinsics { get; set; } = new float[8];


	public float[] CenterLeft { get; set; } = new float[2];


	public float[] InnerBoundaryLeft { get; set; } = new float[4];


	public float[] OuterBoundaryLeft { get; set; } = new float[4];


	public float[] CenterRight { get; set; } = new float[2];


	public float[] InnerBoundaryRight { get; set; } = new float[4];


	public float[] OuterBoundaryRight { get; set; } = new float[4];


	public float[] Reserved { get; set; } = new float[12];


	public static bool TryParse(byte[] buffer, out S6DisplayOpticalParam? parsed)
	{
		parsed = null;
		if (buffer == null || buffer.Length < 200)
		{
			return false;
		}
		try
		{
			Span<byte> span = buffer.AsSpan();
			uint length = BinaryPrimitives.ReadUInt32LittleEndian(span.Slice(0, 4));
			ushort crc = BinaryPrimitives.ReadUInt16LittleEndian(span.Slice(4, 2));
			ushort version = BinaryPrimitives.ReadUInt16LittleEndian(span.Slice(6, 2));
			S6DisplayOpticalParam s6DisplayOpticalParam = new S6DisplayOpticalParam
			{
				Length = length,
				Crc = crc,
				Version = version,
				Width = BinaryPrimitives.ReadUInt32LittleEndian(span.Slice(8, 4)),
				Height = BinaryPrimitives.ReadUInt32LittleEndian(span.Slice(12, 4))
			};
			ReadFloatArray(span.Slice(16, 16), s6DisplayOpticalParam.QLeftImu);
			ReadFloatArray(span.Slice(32, 12), s6DisplayOpticalParam.TLeftImu);
			ReadFloatArray(span.Slice(44, 16), s6DisplayOpticalParam.QRightImu);
			ReadFloatArray(span.Slice(60, 12), s6DisplayOpticalParam.TRightImu);
			ReadFloatArray(span.Slice(72, 32), s6DisplayOpticalParam.InnerLeftIntrinsics);
			ReadFloatArray(span.Slice(104, 32), s6DisplayOpticalParam.OuterLeftIntrinsics);
			ReadFloatArray(span.Slice(136, 32), s6DisplayOpticalParam.InnerRightIntrinsics);
			ReadFloatArray(span.Slice(168, 32), s6DisplayOpticalParam.OuterRightIntrinsics);
			if (buffer.Length >= 208)
			{
				ReadFloatArray(span.Slice(200, 8), s6DisplayOpticalParam.CenterLeft);
			}
			if (buffer.Length >= 224)
			{
				ReadFloatArray(span.Slice(208, 16), s6DisplayOpticalParam.InnerBoundaryLeft);
			}
			if (buffer.Length >= 240)
			{
				ReadFloatArray(span.Slice(224, 16), s6DisplayOpticalParam.OuterBoundaryLeft);
			}
			if (buffer.Length >= 248)
			{
				ReadFloatArray(span.Slice(240, 8), s6DisplayOpticalParam.CenterRight);
			}
			if (buffer.Length >= 264)
			{
				ReadFloatArray(span.Slice(248, 16), s6DisplayOpticalParam.InnerBoundaryRight);
			}
			if (buffer.Length >= 280)
			{
				ReadFloatArray(span.Slice(264, 16), s6DisplayOpticalParam.OuterBoundaryRight);
			}
			if (buffer.Length >= 328)
			{
				ReadFloatArray(span.Slice(280, 48), s6DisplayOpticalParam.Reserved);
			}
			parsed = s6DisplayOpticalParam;
			return true;
		}
		catch (Exception ex)
		{
			Logger.Warning("S6DisplayOpticalParam.TryParse exception: " + ex.Message);
			return false;
		}
	}

	private static void ReadFloatArray(ReadOnlySpan<byte> src, float[] dst)
	{
		for (int i = 0; i < dst.Length; i++)
		{
			dst[i] = MemoryMarshal.Read<float>(src.Slice(i * 4, 4));
		}
	}

	public float[] ToImuOriginMatrix(bool useRightEye)
	{
		float[] obj = (useRightEye ? QRightImu : QLeftImu);
		float[] array = (useRightEye ? TRightImu : TLeftImu);
		float num = obj[0];
		float num2 = obj[1];
		float num3 = obj[2];
		float num4 = obj[3];
		float num5 = num2 * num2;
		float num6 = num3 * num3;
		float num7 = num4 * num4;
		float num8 = num2 * num3;
		float num9 = num2 * num4;
		float num10 = num3 * num4;
		float num11 = num * num2;
		float num12 = num * num3;
		float num13 = num * num4;
		return new float[16]
		{
			1f - 2f * (num6 + num7),
			2f * (num8 + num13),
			2f * (num9 - num12),
			0f,
			2f * (num8 - num13),
			1f - 2f * (num5 + num7),
			2f * (num10 + num11),
			0f,
			2f * (num9 + num12),
			2f * (num10 - num11),
			1f - 2f * (num5 + num6),
			0f,
			array[0],
			array[1],
			array[2],
			1f
		};
	}
}
