using System;
using System.Runtime.InteropServices;
using System.Text;

namespace VitureCommonLibrary;

[Serializable]
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public class HidMessageData
{
	public enum HEADER2_TYPE : byte
	{
		IMU_UP = 252,
		UP,
		DOWN
	}

	public const int HEADER1_DEFAULT = 255;

	public const ushort MIN_LEN = 12;

	public const ushort REPORT_LEN = 64;

	public byte Header1;

	public HEADER2_TYPE Header2;

	public ushort CRC;

	public ushort Len;

	public uint TS_AUX_US;

	public uint TS_MS;

	public ushort MsgID;

	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
	public byte[] Reserved;

	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 46)]
	public byte[] Payload;

	public HidAckState AckState => (HidAckState)Payload[0];

	public ulong DeviceTimestamp => TS_MS * 1000 + TS_AUX_US;

	public int DataLength
	{
		get
		{
			return Len - 12;
		}
		set
		{
			Len = (ushort)(value + 12);
		}
	}

	public Vector3D EulerAngle => Vector3D.FromBytesBigEndian(Payload);

	public Vector3D GyroOffset => Vector3D.FromBytes(Payload, 1);

	public RawImuMagData ImuMagData => RawImuMagData.FromBytesBigEndian(Payload);

	public Vector3D GyroRaw => Vector3D.FromBytesBigEndian(Payload);

	public Vector3D AccRaw => Vector3D.FromBytesBigEndian(Payload, 12);

	public float Temp => FromBytesBigEndianGetFloat(Payload, 24);

	public uint ImuTsOffset => FromBytesBigEndianGetUint(Payload, 36);

	public uint MagTsOffset => FromBytesBigEndianGetUint(Payload, 39);

	public uint VSyncTsOffset => FromBytesBigEndianGetUint(Payload, 42);

	public HidMessageData()
	{
		Header1 = byte.MaxValue;
		Header2 = HEADER2_TYPE.DOWN;
		CRC = 0;
		Len = 12;
		TS_AUX_US = 0u;
		TS_MS = 0u;
		MsgID = 0;
		Reserved = new byte[2];
		Payload = new byte[46];
	}

	public byte[] ToBytes()
	{
		int num = Marshal.SizeOf(this);
		byte[] array = new byte[num];
		IntPtr intPtr = Marshal.AllocHGlobal(num);
		Marshal.StructureToPtr(this, intPtr, fDeleteOld: true);
		Marshal.Copy(intPtr, array, 0, num);
		Marshal.FreeHGlobal(intPtr);
		return array;
	}

	public static HidMessageData FromBytes(byte[] arr)
	{
		HidMessageData hidMessageData = new HidMessageData();
		int num = Marshal.SizeOf(hidMessageData);
		IntPtr intPtr = Marshal.AllocHGlobal(num);
		Marshal.Copy(arr, 0, intPtr, num);
		hidMessageData = (HidMessageData)Marshal.PtrToStructure(intPtr, hidMessageData.GetType());
		Marshal.FreeHGlobal(intPtr);
		return hidMessageData;
	}

	public bool GetAckSuceess()
	{
		return Payload[0] == 0;
	}

	public string GetVersion()
	{
		return Encoding.UTF8.GetString(Payload, 1, DataLength);
	}

	private float FromBytesBigEndianGetFloat(byte[] bytes, int startIndex = 0)
	{
		byte[] array = new byte[4];
		Array.Copy(bytes, startIndex, array, 0, 4);
		Array.Reverse((Array)array);
		return BitConverter.ToSingle(array, 0);
	}

	private uint FromBytesBigEndianGetUint(byte[] bytes, int startIndex = 0)
	{
		return (uint)((((bytes[startIndex] << 8) | bytes[startIndex + 1]) << 8) | bytes[startIndex + 2]);
	}

	public override string ToString()
	{
		return $"DataLength: {DataLength} PayloadTake: {BitConverter.ToString(Payload, 0, DataLength).Replace('-', ' ')}";
	}

	public void PutValue(byte value)
	{
		DataLength = 1;
		Payload[0] = value;
	}

	public void PutValue(int value)
	{
		DataLength = 4;
		BitConverter.GetBytes(value).CopyTo(Payload, 0);
	}

	public void PutValue(uint value)
	{
		DataLength = 4;
		BitConverter.GetBytes(value).CopyTo(Payload, 0);
	}

	public void PutValue(long value)
	{
		DataLength = 8;
		BitConverter.GetBytes(value).CopyTo(Payload, 0);
	}

	public void PutValue(ulong value)
	{
		DataLength = 8;
		BitConverter.GetBytes(value).CopyTo(Payload, 0);
	}

	public void PutValue(short value)
	{
		DataLength = 2;
		BitConverter.GetBytes(value).CopyTo(Payload, 0);
	}

	public void PutValue(ushort value)
	{
		DataLength = 2;
		BitConverter.GetBytes(value).CopyTo(Payload, 0);
	}

	public void PutValue(float value)
	{
		BitConverter.GetBytes(value).CopyTo(Payload, 0);
	}

	public void PutValue(double value)
	{
		BitConverter.GetBytes(value).CopyTo(Payload, 0);
	}
}
