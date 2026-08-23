using System;
using System.Buffers.Binary;
using System.Runtime.InteropServices;

namespace VitureCommonLibrary;

public static class S6ImuFrameParser
{
	public readonly struct Parsed
	{
		public ulong SampleTimestampNs { get; }

		public float TemperatureC { get; }

		public float GyroX { get; }

		public float GyroY { get; }

		public float GyroZ { get; }

		public float AccX { get; }

		public float AccY { get; }

		public float AccZ { get; }

		public float MagX { get; }

		public float MagY { get; }

		public float MagZ { get; }

		public uint ImuTsOffsetUs { get; }

		public uint MagTsOffsetUs { get; }

		public uint VsyncTsOffsetUs { get; }

		public Parsed(ulong sampleTimestampNs, float temperatureC, float gyroX, float gyroY, float gyroZ, float accX, float accY, float accZ, float magX, float magY, float magZ, uint imuTsOffsetUs, uint magTsOffsetUs, uint vsyncTsOffsetUs)
		{
			SampleTimestampNs = sampleTimestampNs;
			TemperatureC = temperatureC;
			GyroX = gyroX;
			GyroY = gyroY;
			GyroZ = gyroZ;
			AccX = accX;
			AccY = accY;
			AccZ = accZ;
			MagX = magX;
			MagY = magY;
			MagZ = magZ;
			ImuTsOffsetUs = imuTsOffsetUs;
			MagTsOffsetUs = magTsOffsetUs;
			VsyncTsOffsetUs = vsyncTsOffsetUs;
		}
	}

	public const int MinPayloadLength = 55;

	public const float G = 9.80665f;

	public const float TemperatureStep = 0.2f;

	public static bool TryParse(R6NewerHidMessage r6Msg, out Parsed parsed)
	{
		parsed = default(Parsed);
		if (r6Msg == null || r6Msg.Payload == null || r6Msg.Payload.Length < 55)
		{
			return false;
		}
		return TryParse(r6Msg.Payload.AsSpan(0, Math.Min(r6Msg.Payload.Length, 56)), out parsed);
	}

	public static bool TryParse(ReadOnlySpan<byte> payload, out Parsed parsed)
	{
		parsed = default(Parsed);
		if (payload.Length < 55)
		{
			return false;
		}
		try
		{
			uint num = BinaryPrimitives.ReadUInt32LittleEndian(payload.Slice(0, 4));
			uint num2 = BinaryPrimitives.ReadUInt32LittleEndian(payload.Slice(4, 4));
			ushort num3 = BinaryPrimitives.ReadUInt16LittleEndian(payload.Slice(8, 2));
			float gyroX = ReadFloatLE(payload.Slice(10, 4));
			float gyroY = ReadFloatLE(payload.Slice(14, 4));
			float gyroZ = ReadFloatLE(payload.Slice(18, 4));
			float accX = ReadFloatLE(payload.Slice(22, 4));
			float accY = ReadFloatLE(payload.Slice(26, 4));
			float accZ = ReadFloatLE(payload.Slice(30, 4));
			float magX = ReadFloatLE(payload.Slice(34, 4));
			float magY = ReadFloatLE(payload.Slice(38, 4));
			float magZ = ReadFloatLE(payload.Slice(42, 4));
			uint num4 = ReadU24LE(payload.Slice(46, 3));
			uint magTsOffsetUs = ReadU24LE(payload.Slice(49, 3));
			uint vsyncTsOffsetUs = ReadU24LE(payload.Slice(52, 3));
			long num5 = (long)num2 * 1000L + num - num4;
			if (num5 < 0)
			{
				num5 = 0L;
			}
			ulong sampleTimestampNs = checked((ulong)num5 * 1000);
			float temperatureC = (float)(int)num3 * 0.2f;
			parsed = new Parsed(sampleTimestampNs, temperatureC, gyroX, gyroY, gyroZ, accX, accY, accZ, magX, magY, magZ, num4, magTsOffsetUs, vsyncTsOffsetUs);
			return true;
		}
		catch (Exception ex)
		{
			Logger.Warning("S6ImuFrameParser.TryParse exception: " + ex.Message);
			return false;
		}
	}

	public static ImuData ToImuData(in Parsed p, bool includeMag = false)
	{
		ImuData result = default(ImuData);
		result.timestamp = p.SampleTimestampNs;
		result.temperature = p.TemperatureC;
		result.acc_x = p.AccX * 9.80665f;
		result.acc_y = p.AccY * 9.80665f;
		result.acc_z = p.AccZ * 9.80665f;
		result.gyr_x = p.GyroX;
		result.gyr_y = p.GyroY;
		result.gyr_z = p.GyroZ;
		result.mag_x = (includeMag ? ((double)p.MagX) : 0.0);
		result.mag_y = (includeMag ? ((double)p.MagY) : 0.0);
		result.mag_z = (includeMag ? ((double)p.MagZ) : 0.0);
		return result;
	}

	private static uint ReadU24LE(ReadOnlySpan<byte> bytes)
	{
		return (uint)(bytes[0] | (bytes[1] << 8) | (bytes[2] << 16));
	}

	private static float ReadFloatLE(ReadOnlySpan<byte> src)
	{
		return MemoryMarshal.Read<float>(src);
	}
}
