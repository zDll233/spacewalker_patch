using System;

namespace VitureCommonLibrary;

[AttributeUsage(AttributeTargets.Field)]
public class DeviceEventIdAttribute : Attribute
{
	public DeviceEventId EventId { get; }

	public DeviceEventIdAttribute(DeviceEventId eventId)
	{
		EventId = eventId;
	}
}
