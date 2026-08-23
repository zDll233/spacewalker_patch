using System;
using System.Runtime.InteropServices;
using System.Text;

namespace VitureCommonLibrary;

[Serializable]
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public class R6NewerHidMessage
{
	public const int PROTO_VER = 16;

	public const ushort REPORT_LEN = 64;

	public byte ProtoVer = 16;

	public byte SeqNum;

	public ushort MsgID;

	public ushort DataLen;

	public ushort CRC;

	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 56)]
	public byte[] Payload = new byte[56];

	public byte[] ToBytes()
	{
		CRC = GetCrc();
		int num = Marshal.SizeOf(this);
		byte[] array = new byte[num];
		IntPtr intPtr = Marshal.AllocHGlobal(num);
		Marshal.StructureToPtr(this, intPtr, fDeleteOld: true);
		Marshal.Copy(intPtr, array, 0, num);
		Marshal.FreeHGlobal(intPtr);
		return array;
	}

	public static R6NewerHidMessage FromBytes(byte[] bytes)
	{
		return Marshal.PtrToStructure<R6NewerHidMessage>(Marshal.UnsafeAddrOfPinnedArrayElement(bytes, 0));
	}

	public ushort GetCrc()
	{
		uint num = 0u;
		if (DataLen > Payload.Length)
		{
			return (ushort)num;
		}
		for (int i = 0; i < DataLen; i++)
		{
			num += Payload[i];
		}
		return (ushort)num;
	}

	public string GetVersion()
	{
		if (GetAckSuceess() && DataLen >= 0)
		{
			int num = 0;
			int count = DataLen;
			if (DataLen > 18)
			{
				num = DataLen - 18;
				count = 18;
			}
			string @string = Encoding.UTF8.GetString(Payload, num + 1, count);
			Logger.Info("firmwareVersion: " + @string);
			return @string.TrimEnd('\0', '\r', '\n', ' ');
		}
		return string.Empty;
	}

	public string GetGlassesSN()
	{
		if (GetAckSuceess())
		{
			return Encoding.UTF8.GetString(Payload, 1, DataLen).TrimEnd('\0', '\r', '\n', ' ');
		}
		return string.Empty;
	}

	public string GetPSN()
	{
		if (GetAckSuceess())
		{
			return Encoding.UTF8.GetString(Payload, 1, DataLen).TrimEnd('\0', '\r', '\n', ' ');
		}
		return string.Empty;
	}

	public R6AckStatus GetAckStatus()
	{
		return (R6AckStatus)Payload[0];
	}

	public bool GetAckSuceess()
	{
		return GetAckStatus() == R6AckStatus.TF_RSP_OK;
	}
}
