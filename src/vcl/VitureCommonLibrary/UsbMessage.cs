using System;
using System.Text;

namespace VitureCommonLibrary;

public class UsbMessage
{
	public UsbMessageData Data;

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

	public ResponseStatus ResponseStatus => (ResponseStatus)Data.Payload[0];

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

	public UsbMessage()
	{
		Data = new UsbMessageData();
		LocalTimestamp = UnixTimestampHelper.GetMillisecondTimestamp();
		RenderTimestamp = 0uL;
	}

	public uint DeviceTimestamp()
	{
		return Data.DeviceTimestamp();
	}

	public bool GetAckSuceess()
	{
		return Data.Payload[0] == 0;
	}

	public string GetVersion()
	{
		int count = Array.IndexOf(Data.Payload, (byte)0, 1) - 1;
		return Encoding.UTF8.GetString(Data.Payload, 1, count);
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

	public static UsbMessage FromBytes(byte[] bytes)
	{
		UsbMessage usbMessage = new UsbMessage();
		UsbMessageData data = UsbMessageData.FromBytes(bytes);
		usbMessage.Data = data;
		return usbMessage;
	}

	public byte[] ToBytes()
	{
		return Data.ToBytes();
	}
}
