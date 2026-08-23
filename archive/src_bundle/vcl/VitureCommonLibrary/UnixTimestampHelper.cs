using System;

namespace VitureCommonLibrary;

public class UnixTimestampHelper
{
	public static ulong GetMicrosecondTimestamp()
	{
		return (ulong)(DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() * 1000);
	}

	public static ulong GetMillisecondTimestamp()
	{
		return (ulong)DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
	}

	public static ulong GetSecondTimestamp()
	{
		return (ulong)DateTimeOffset.UtcNow.ToUnixTimeSeconds();
	}
}
