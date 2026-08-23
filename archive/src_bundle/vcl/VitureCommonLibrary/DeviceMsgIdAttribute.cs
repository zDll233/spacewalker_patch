using System;

namespace VitureCommonLibrary;

[AttributeUsage(AttributeTargets.Field)]
public class DeviceMsgIdAttribute : Attribute
{
	public DeviceMsgId DeviceMsgId { get; }

	public DeviceMsgIdAttribute(DeviceMsgId deviceMsgId)
	{
		DeviceMsgId = deviceMsgId;
	}
}
