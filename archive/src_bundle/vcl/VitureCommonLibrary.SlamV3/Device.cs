using System;
using System.Runtime.InteropServices;

namespace VitureCommonLibrary.SlamV3;

public struct Device
{
	[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
	public string sn;

	public Imu imu;

	public Display display_left;

	public Display display_right;

	public int num_cameras;

	public IntPtr cameras;
}
