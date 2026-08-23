using System.Runtime.InteropServices;

namespace VitureCommonLibrary;

public struct CameraParam
{
	[MarshalAs(UnmanagedType.LPArray, SizeConst = 16)]
	public float[] ExtrinsicsL;

	[MarshalAs(UnmanagedType.LPArray, SizeConst = 4)]
	public float[] DistL;

	[MarshalAs(UnmanagedType.LPArray, SizeConst = 4)]
	public float[] IntrinsicsL;

	[MarshalAs(UnmanagedType.LPArray, SizeConst = 16)]
	public float[] ExtrinsicsR;

	[MarshalAs(UnmanagedType.LPArray, SizeConst = 4)]
	public float[] DistR;

	[MarshalAs(UnmanagedType.LPArray, SizeConst = 4)]
	public float[] IntrinsicsR;

	[MarshalAs(UnmanagedType.LPArray, SizeConst = 16)]
	public float[] ExtrinsicsLR;
}
