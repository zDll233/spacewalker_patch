using System;
using System.Text;

namespace VitureCommonLibrary;

public class HidMessage
{
	public HidMessageData Data;

	public ulong LocalTimestamp;

	public ulong RenderTimestamp;

	public ushort MsgId
	{
		get
		{
			return Data.MsgID;
		}
		set
		{
			Data.MsgID = value;
		}
	}

	public ulong DeviceTimestamp => Data.DeviceTimestamp;

	public HidAckState AckState => (HidAckState)Data.Payload[0];

	public int DataLength
	{
		get
		{
			return Data.Len - 12;
		}
		set
		{
			Data.Len = (ushort)(value + 12);
		}
	}

	public Vector3D AccRaw => Data.AccRaw;

	public Vector3D GyroRaw => Data.GyroRaw;

	public float Temp => Data.Temp;

	public uint ImuTsOffset => Data.ImuTsOffset;

	public uint MagTsOffset => Data.MagTsOffset;

	public uint VSyncTsOffset => Data.VSyncTsOffset;

	public Vector3D EulerAngle => Data.EulerAngle;

	public Vector3D GyroOffset => Data.GyroOffset;

	public RawImuMagData ImuMagData => Data.ImuMagData;

	public HidMessage()
	{
		Data = new HidMessageData();
		LocalTimestamp = UnixTimestampHelper.GetMillisecondTimestamp();
		RenderTimestamp = 0uL;
	}

	public bool GetAckSuceess()
	{
		return Data.Payload[0] == 0;
	}

	public string GetVersion()
	{
		int count = Array.IndexOf(Data.Payload, (byte)0, 1) - 1;
		string text = Encoding.UTF8.GetString(Data.Payload, 1, count);
		if (text.Length == 20)
		{
			text = text.Substring(3, text.Length - 3);
		}
		return text;
	}

	public string GetGlassesSN()
	{
		int count = Array.IndexOf(Data.Payload, (byte)0, 1) - 1;
		return Encoding.UTF8.GetString(Data.Payload, 1, count);
	}

	public string GetPSN()
	{
		int count = Array.IndexOf(Data.Payload, (byte)0, 1) - 1;
		return Encoding.UTF8.GetString(Data.Payload, 1, count);
	}

	public override string ToString()
	{
		return $"DataLength: {DataLength} PayloadTake: {BitConverter.ToString(Data.Payload, 0, DataLength).Replace('-', ' ')}";
	}

	public static HidMessage FromBytes(byte[] bytes)
	{
		HidMessage hidMessage = new HidMessage();
		HidMessageData data = HidMessageData.FromBytes(bytes);
		hidMessage.Data = data;
		return hidMessage;
	}

	public byte[] ToBytes()
	{
		return Data.ToBytes();
	}
}
