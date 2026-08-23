using System;

namespace VitureCommonLibrary;

public struct CameraData
{
	public byte camera_id;

	public ulong timestamp;

	public IntPtr data;

	public uint width;

	public uint height;

	public uint stride;

	public uint exposure;

	public uint gain;
}
