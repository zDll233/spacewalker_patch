using System;
using System.Runtime.InteropServices;

namespace VitureCommonLibrary;

public static class GestureSdk
{
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate void SWRCallback([MarshalAs(UnmanagedType.LPArray, SizeConst = 374)] float[] data);

	private const string DllName = "gesture_recognition.dll";

	[DllImport("gesture_recognition.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern int gesture_init(string modelPath);

	[DllImport("gesture_recognition.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern int gesture_uninit();

	[DllImport("gesture_recognition.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern int gesture_start();

	[DllImport("gesture_recognition.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern int gesture_stop();

	[DllImport("gesture_recognition.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern int OnSlamPose([MarshalAs(UnmanagedType.LPArray, SizeConst = 16)] float[] pose, double timestamp);

	[DllImport("gesture_recognition.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern int OnCameraData(IntPtr leftBuffer, IntPtr rightBuffer, uint bufferSize, double timestamp);

	[DllImport("gesture_recognition.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern int update_mode(int mode, [MarshalAs(UnmanagedType.LPArray, SizeConst = 16)] float[] extrinsicsL, [MarshalAs(UnmanagedType.LPArray, SizeConst = 4)] float[] distL, [MarshalAs(UnmanagedType.LPArray, SizeConst = 4)] float[] intrinsicsL, [MarshalAs(UnmanagedType.LPArray, SizeConst = 16)] float[] extrinsicsR, [MarshalAs(UnmanagedType.LPArray, SizeConst = 4)] float[] distR, [MarshalAs(UnmanagedType.LPArray, SizeConst = 4)] float[] intrinsicsR, [MarshalAs(UnmanagedType.LPArray, SizeConst = 16)] float[] extrinsicsLR);

	[DllImport("gesture_recognition.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern int Register_Gesture_Callbacks(IntPtr adrCallback, SWRCallback swrCallback, IntPtr irCallback, IntPtr handrayCallback);
}
