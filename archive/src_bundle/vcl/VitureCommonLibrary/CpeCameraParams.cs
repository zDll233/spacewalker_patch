using System.Runtime.InteropServices;

namespace VitureCommonLibrary;

public struct CpeCameraParams
{
	public float fx;

	public float fy;

	public float cx;

	public float cy;

	public int width;

	public int height;

	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 14)]
	public float[] distCoeffs;

	public int distCoeffsCount;
}
