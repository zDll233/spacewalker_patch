using System;
using System.Runtime.InteropServices;

namespace VitureCommonLibrary;

public static class CpeNative
{
	private const string DllName = "CamPoseEstimationApi.dll";

	[DllImport("CamPoseEstimationApi.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern IntPtr Cpe_Create();

	[DllImport("CamPoseEstimationApi.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern void Cpe_Destroy(IntPtr handle);

	[DllImport("CamPoseEstimationApi.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern int Cpe_Init(IntPtr handle, string modelPath, float screenInch, float aspectRatio);

	[DllImport("CamPoseEstimationApi.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern void Cpe_SetPoseCallback(IntPtr handle, CpePoseCallback callback);

	[DllImport("CamPoseEstimationApi.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern void Cpe_SetCameraParamsRequestCallback(IntPtr handle, CpeCameraParamsRequest callback);

	[DllImport("CamPoseEstimationApi.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern int Cpe_Start(IntPtr handle);

	[DllImport("CamPoseEstimationApi.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern void Cpe_Stop(IntPtr handle);

	[DllImport("CamPoseEstimationApi.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern int Cpe_IsConnected(IntPtr handle);

	[DllImport("CamPoseEstimationApi.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern void Cpe_ResetAnchor(IntPtr handle);

	[DllImport("CamPoseEstimationApi.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern void Cpe_SetDebugMode(IntPtr handle, bool debug);
}
