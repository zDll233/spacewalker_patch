using System;
using System.Runtime.InteropServices;
using System.Text;

namespace VitureCommonLibrary;

[Serializable]
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public class UsbMessageData
{
	public enum HEADER_TYPE : ushort
	{
		UP = 65023,
		DOWN = 65279
	}

	public const ushort MIN_LEN = 12;

	public const ushort REPORT_LEN = 512;

	public HEADER_TYPE Header;

	public ushort CRC;

	public ushort Len;

	public uint TSaux;

	public uint TS;

	public ushort MsgID;

	public ushort Reserved;

	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 494)]
	public byte[] Payload;

	public HidAckState AckState => (HidAckState)Payload[0];

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

	public Vector3D EulerAngle
	{
		get
		{
			return Vector3D.FromBytesBigEndian(Payload);
		}
		set
		{
			byte[] array = value.ToBytesBigEndian();
			Array.Copy(array, 0, Payload, 0, array.Length);
		}
	}

	public UsbMessageData()
	{
		Header = HEADER_TYPE.DOWN;
		CRC = 0;
		Len = 12;
		TSaux = 0u;
		TS = 0u;
		MsgID = 0;
		Reserved = 0;
		Payload = new byte[494];
	}

	public byte[] ToBytes()
	{
		int num = Marshal.SizeOf(this);
		byte[] array = new byte[num];
		IntPtr intPtr = Marshal.AllocHGlobal(num);
		Marshal.StructureToPtr(this, intPtr, fDeleteOld: true);
		Marshal.Copy(intPtr, array, 0, num);
		Marshal.FreeHGlobal(intPtr);
		CRC = CrcHelper.GetCrc(array, 4, Len + 2);
		array[2] = (byte)(CRC & 0xFFu);
		array[3] = (byte)((uint)(CRC >> 8) & 0xFFu);
		return array;
	}

	public static UsbMessageData FromBytes(byte[] arr)
	{
		UsbMessageData usbMessageData = new UsbMessageData();
		int num = Marshal.SizeOf(usbMessageData);
		IntPtr intPtr = Marshal.AllocHGlobal(num);
		Marshal.Copy(arr, 0, intPtr, num);
		usbMessageData = (UsbMessageData)Marshal.PtrToStructure(intPtr, usbMessageData.GetType());
		Marshal.FreeHGlobal(intPtr);
		return usbMessageData;
	}

	private static byte[] ReverseBytes(byte[] arr, int startIndex, int length)
	{
		byte[] array = new byte[length];
		Array.Copy(arr, startIndex, array, 0, length);
		Array.Reverse((Array)array);
		return array;
	}

	public bool GetAckSuceess()
	{
		return Payload[0] == 0;
	}

	public uint DeviceTimestamp()
	{
		return (uint)((ulong)(TS & -4294967296L) >> 32);
	}

	public string GetVersion()
	{
		return Encoding.UTF8.GetString(Payload, 1, DataLength);
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
