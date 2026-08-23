using System;
using System.Runtime.InteropServices;

namespace VitureCommonLibrary.SlamV3;

public static class VitureSlamV3Native
{
	public const string DllName = "SlamV3";

	[DllImport("SlamV3", CallingConvention = CallingConvention.Cdecl)]
	public static extern IntPtr VitureSlamCreateHandler([MarshalAs(UnmanagedType.LPStr)] string slam_config, Platform platform);

	[DllImport("SlamV3", CallingConvention = CallingConvention.Cdecl)]
	public static extern int VitureSlamDestroyHandler(IntPtr slam_handler);

	[DllImport("SlamV3", CallingConvention = CallingConvention.Cdecl)]
	public static extern int VitureSlamInitialize(IntPtr slam_handler, DeviceModel device_model);

	[DllImport("SlamV3", CallingConvention = CallingConvention.Cdecl)]
	public static extern int VitureSlamTerminate(IntPtr slam_handler);

	[DllImport("SlamV3", CallingConvention = CallingConvention.Cdecl)]
	public static extern int VitureSlamStart(IntPtr slam_handler);

	[DllImport("SlamV3", CallingConvention = CallingConvention.Cdecl)]
	public static extern int VitureSlamStop(IntPtr slam_handler);

	[DllImport("SlamV3", CallingConvention = CallingConvention.Cdecl)]
	public static extern int VitureSlamSetMode(IntPtr slam_handler, SlamMode mode);

	[DllImport("SlamV3", CallingConvention = CallingConvention.Cdecl)]
	public static extern int VitureSlamProcessImuData(IntPtr slam_handler, ref ImuData imu_data);

	[DllImport("SlamV3", CallingConvention = CallingConvention.Cdecl)]
	public static extern int VitureSlamProcessImagesData(IntPtr slam_handler, ulong timestamp, IntPtr images_data, int num_images);

	[DllImport("SlamV3", CallingConvention = CallingConvention.Cdecl)]
	public static extern int VitureSlamProcessRecenterSignal(IntPtr slam_handler);

	[DllImport("SlamV3", CallingConvention = CallingConvention.Cdecl)]
	public static extern int VitureSlamGetPose(IntPtr slam_handler, ulong timestamp, ref SlamPose pose_world_imu);

	[DllImport("SlamV3", CallingConvention = CallingConvention.Cdecl)]
	public static extern int VitureSlamGetHeadPose(IntPtr slam_handler, ulong timestamp, ref SlamPose pose_world_head);

	[DllImport("SlamV3", CallingConvention = CallingConvention.Cdecl)]
	public static extern int VitureSlamRecenter(IntPtr slam_handler);

	[DllImport("SlamV3", CallingConvention = CallingConvention.Cdecl)]
	public static extern int VitureSlamRegisterPoseCallback(IntPtr slam_handler, VitureSlamPoseCallback callback);

	[DllImport("SlamV3", CallingConvention = CallingConvention.Cdecl)]
	public static extern int VitureSlamRegisterSetPriorityCallback(IntPtr slam_handler, VitureSlamSetPriorityCallback callback);

	[DllImport("SlamV3", CallingConvention = CallingConvention.Cdecl)]
	public static extern int VitureSlamRegisterLoadCustomFileCallback(IntPtr slam_handler, VitureSlamLoadCustomFileCallback callback);

	[DllImport("SlamV3", CallingConvention = CallingConvention.Cdecl)]
	public static extern int VitureSlamRegisterSaveCustomFileCallback(IntPtr slam_handler, VitureSlamSaveCustomFileCallback callback);

	[DllImport("SlamV3", CallingConvention = CallingConvention.Cdecl)]
	public static extern int VitureSlamRegisterLoadDeviceInfoCallback(IntPtr slam_handler, VitureSlamLoadDeviceInfoCallback callback);

	[DllImport("SlamV3", CallingConvention = CallingConvention.Cdecl)]
	public static extern int VitureSlamRegisterReadStaticGyrBiasCallback(IntPtr slam_handler, VitureSlamReadStaticGyrBiasCallback callback);

	[DllImport("SlamV3", CallingConvention = CallingConvention.Cdecl)]
	public static extern int VitureSlamRegisterReadCarinaConfigCallback(IntPtr slam_handler, VitureSlamReadCarinaConfigCallback callback);
}
