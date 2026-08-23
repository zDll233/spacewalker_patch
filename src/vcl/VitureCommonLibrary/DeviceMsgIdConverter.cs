using System;
using System.Collections.Generic;
using System.Reflection;

namespace VitureCommonLibrary;

public static class DeviceMsgIdConverter
{
	private static readonly Dictionary<DeviceMsgId, R6NewerMsgId> _deviceToR6Map;

	private static readonly Dictionary<DeviceEventId, R6NewerMsgId> _eventToR6Map;

	static DeviceMsgIdConverter()
	{
		_deviceToR6Map = new Dictionary<DeviceMsgId, R6NewerMsgId>();
		_eventToR6Map = new Dictionary<DeviceEventId, R6NewerMsgId>();
		FieldInfo[] fields = typeof(R6NewerMsgId).GetFields(BindingFlags.Static | BindingFlags.Public);
		foreach (FieldInfo fieldInfo in fields)
		{
			DeviceMsgIdAttribute customAttribute = fieldInfo.GetCustomAttribute<DeviceMsgIdAttribute>();
			if (customAttribute != null)
			{
				R6NewerMsgId value = (R6NewerMsgId)fieldInfo.GetValue(null);
				_deviceToR6Map[customAttribute.DeviceMsgId] = value;
			}
		}
		fields = typeof(R6NewerMsgId).GetFields(BindingFlags.Static | BindingFlags.Public);
		foreach (FieldInfo fieldInfo2 in fields)
		{
			DeviceEventIdAttribute customAttribute2 = fieldInfo2.GetCustomAttribute<DeviceEventIdAttribute>();
			if (customAttribute2 != null)
			{
				R6NewerMsgId value2 = (R6NewerMsgId)fieldInfo2.GetValue(null);
				_eventToR6Map[customAttribute2.EventId] = value2;
			}
		}
	}

	public static bool TryConvertToR6MsgId(DeviceMsgId deviceMsgId, out R6NewerMsgId r6MsgId)
	{
		return _deviceToR6Map.TryGetValue(deviceMsgId, out r6MsgId);
	}

	public static bool TryConvertToR6MsgId(DeviceEventId eventId, out R6NewerMsgId r6MsgId)
	{
		return _eventToR6Map.TryGetValue(eventId, out r6MsgId);
	}

	public static R6NewerMsgId ToR6MsgId(DeviceMsgId deviceMsgId)
	{
		if (_deviceToR6Map.TryGetValue(deviceMsgId, out var value))
		{
			return value;
		}
		throw new ArgumentException($"DeviceMsgId.{deviceMsgId} Not have R6MsgId Mapping", "deviceMsgId");
	}

	public static R6NewerMsgId? ToR6MsgIdOrDefault(DeviceMsgId deviceMsgId)
	{
		_deviceToR6Map.TryGetValue(deviceMsgId, out var value);
		if (!_deviceToR6Map.ContainsKey(deviceMsgId))
		{
			return null;
		}
		return value;
	}
}
