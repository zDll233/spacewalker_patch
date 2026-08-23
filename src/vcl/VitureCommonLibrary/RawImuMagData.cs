using System;
using System.Runtime.InteropServices;

namespace VitureCommonLibrary;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct RawImuMagData
{
	public float GyroX;

	public float GyroY;

	public float GyroZ;

	public float AccX;

	public float AccY;

	public float AccZ;

	public float MagX;

	public float MagY;

	public float MagZ;

	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
	public byte[] ImuTsOffset;

	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
	public byte[] MagTsOffset;

	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
	public byte[] VSyncTsOffset;

	public byte RSV;

	public RawImuMagData()
	{
		GyroX = 0f;
		GyroY = 0f;
		GyroZ = 0f;
		AccX = 0f;
		AccY = 0f;
		AccZ = 0f;
		MagX = 0f;
		MagY = 0f;
		MagZ = 0f;
		RSV = 0;
		ImuTsOffset = new byte[3];
		MagTsOffset = new byte[3];
		VSyncTsOffset = new byte[3];
	}

	public override string ToString()
	{
		return $"Gyro: {GyroX}, {GyroY}, {GyroZ} Acc: {AccX} {AccY} {AccZ}\r\n" + $"Mag: {MagX} {MagY} {MagZ}";
	}

	public static RawImuMagData FromBytesBigEndian(byte[] bytes, int startIndex = 0)
	{
		if (bytes == null)
		{
			throw new ArgumentNullException("bytes");
		}
		if (startIndex + 46 > bytes.Length)
		{
			throw new ArgumentException("Not enough bytes in the array");
		}
		RawImuMagData result = new RawImuMagData();
		int num = startIndex;
		for (int i = 0; i < 9; i++)
		{
			byte[] array = new byte[4];
			Array.Copy(bytes, num, array, 0, 4);
			Array.Reverse((Array)array);
			float num2 = BitConverter.ToSingle(array, 0);
			switch (i)
			{
			case 0:
				result.GyroX = num2;
				break;
			case 1:
				result.GyroY = num2;
				break;
			case 2:
				result.GyroZ = num2;
				break;
			case 3:
				result.AccX = num2;
				break;
			case 4:
				result.AccY = num2;
				break;
			case 5:
				result.AccZ = num2;
				break;
			case 6:
				result.MagX = num2;
				break;
			case 7:
				result.MagY = num2;
				break;
			case 8:
				result.MagZ = num2;
				break;
			}
			num += 4;
		}
		Array.Copy(bytes, num, result.ImuTsOffset, 0, 3);
		num += 3;
		Array.Copy(bytes, num, result.MagTsOffset, 0, 3);
		num += 3;
		Array.Copy(bytes, num, result.VSyncTsOffset, 0, 3);
		num += 3;
		result.RSV = bytes[num];
		return result;
	}

	public byte[] ToBytesBigEndian()
	{
		byte[] result = new byte[46];
		int offset = 0;
		Action<float> obj = delegate(float value)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			Array.Reverse((Array)bytes);
			Array.Copy(bytes, 0, result, offset, 4);
			offset += 4;
		};
		obj(GyroX);
		obj(GyroY);
		obj(GyroZ);
		obj(AccX);
		obj(AccY);
		obj(AccZ);
		obj(MagX);
		obj(MagY);
		obj(MagZ);
		Array.Copy(ImuTsOffset, 0, result, offset, 3);
		offset += 3;
		Array.Copy(MagTsOffset, 0, result, offset, 3);
		offset += 3;
		Array.Copy(VSyncTsOffset, 0, result, offset, 3);
		offset += 3;
		result[offset] = RSV;
		return result;
	}
}
