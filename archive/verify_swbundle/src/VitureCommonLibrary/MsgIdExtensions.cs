namespace VitureCommonLibrary;

public static class MsgIdExtensions
{
	public static bool Equal(this R6NewerMsgId msgId, ushort value)
	{
		return msgId.SubId() == (value & 0xFFF);
	}

	public static bool Equal(this ushort value, R6NewerMsgId msgId)
	{
		return msgId.SubId() == (value & 0xFFF);
	}

	public static bool Equal(this DeviceMsgId msgId, ushort value)
	{
		return (uint)msgId == value;
	}

	public static bool Equal(this ushort value, DeviceMsgId msgId)
	{
		return (uint)msgId == value;
	}

	public static bool Equal(this ushort value, DeviceEventId enventId)
	{
		return (uint)enventId == value;
	}

	public static bool Equal(this DeviceMsgId msgId, R6NewerMsgId r6MsgId)
	{
		if (DeviceMsgIdConverter.TryConvertToR6MsgId(msgId, out var r6MsgId2))
		{
			return r6MsgId2.SubId() == r6MsgId.SubId();
		}
		return false;
	}

	public static bool Equal(this R6NewerMsgId r6MsgId, DeviceMsgId msgId)
	{
		if (DeviceMsgIdConverter.TryConvertToR6MsgId(msgId, out var r6MsgId2))
		{
			return r6MsgId2.SubId() == r6MsgId.SubId();
		}
		return false;
	}

	public static bool EqualR6MsgId(this DeviceMsgId msgId, ushort r6MsgId)
	{
		if (DeviceMsgIdConverter.TryConvertToR6MsgId(msgId, out var r6MsgId2))
		{
			return (r6MsgId & 0xFFF) == r6MsgId2.SubId();
		}
		return false;
	}

	public static bool R6MsgIdEqual(this ushort r6MsgId, DeviceMsgId msgId)
	{
		if (DeviceMsgIdConverter.TryConvertToR6MsgId(msgId, out var r6MsgId2))
		{
			return (r6MsgId & 0xFFF) == r6MsgId2.SubId();
		}
		return false;
	}

	public static bool R6MsgIdEqual(this ushort r6MsgId, DeviceEventId eventId)
	{
		if (DeviceMsgIdConverter.TryConvertToR6MsgId(eventId, out var r6MsgId2))
		{
			return (r6MsgId & 0xFFF) == r6MsgId2.SubId();
		}
		return false;
	}

	public static ushort SubId(this R6NewerMsgId r6MsgId)
	{
		return (ushort)(r6MsgId & (R6NewerMsgId)4095);
	}
}
