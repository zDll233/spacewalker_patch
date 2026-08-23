using System;
using System.IO;
using System.Runtime.InteropServices;
using VitureCommonLibrary;

public static class CarinaNative
{
	public struct CarinaOrbPoint
	{
		public int octave;

		public float angle;

		public float response;

		public float x;

		public float y;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
		public byte[] desc;

		public uint id;
	}

	public struct CarinaLkPoint
	{
		public float x;

		public float y;

		public uint id;
	}

	public struct CarinaPoints
	{
		public IntPtr points_lk;

		public IntPtr points_orb;

		public int points_lk_rows;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
		public int[] points_lk_cols;

		public int points_orb_rows;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
		public int[] points_orb_cols;
	}

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	private delegate void CarinaA1088VsyncCallBack(double timestamp);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	private delegate void CarinaA1088PoseCallBack(IntPtr data, double timestamp);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	private delegate void CarinaA1088ImuCallBack(IntPtr data, double timestamp);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	private delegate void CarinaA1088CameraCallBack(IntPtr left, IntPtr right, IntPtr left1, IntPtr right1, double timestamp, int width, int height);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	private delegate void CarinaA1088PointsCallBack(ref CarinaPoints points, double timestamp);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	private delegate void CarinaA1088EventCallBack(byte eventCode);

	private const string dllName = "carina_vio";

	private static CarinaA1088PoseCallBack _carinaA1088PoseCallBack = OnCarinaA1088PoseCallBack;

	private static CarinaA1088VsyncCallBack _carinaA1088VsyncCallBack = OnCarinaA1088VsyncCallBack;

	private static CarinaA1088ImuCallBack _carinaA1088ImuCallBack = OnCarinaA1088ImuCallBack;

	private static CarinaA1088CameraCallBack _carinaA1088CameraCallBack = OnCarinaA1088CameraCallBack;

	private static CarinaA1088PointsCallBack _carinaA1088PointsCallBack = OnCarinaA1088PointsCallBack;

	private static CarinaA1088EventCallBack _carinaA1088EventCallBack = OnCarinaA1088EventCallBack;

	private static readonly float[] poseData = new float[16];

	private static readonly float[] imuData = new float[6];

	private static bool hasInit = false;

	private static bool hasStart = false;

	public static event Action<float[], double>? PoseUpdate;

	public static event Action<double>? VsyncUpdate;

	public static event Action<float[], double>? ImuUpdate;

	public static event Action<IntPtr, IntPtr, IntPtr, IntPtr, double, int, int>? CameraImageUpdate;

	public static event Action<CarinaPoints, double>? PointsUpdate;

	public static event Action<byte>? EventUpdate;

	[DllImport("carina_vio", CallingConvention = CallingConvention.Cdecl)]
	private static extern int carina_a1088_viture_init([MarshalAs(UnmanagedType.LPStr)] string custom_config, [MarshalAs(UnmanagedType.LPStr)] string vocab_file_path, int file_descriptor = -1, int bus_num = -1, int dev_addr = -1);

	[DllImport("carina_vio", CallingConvention = CallingConvention.Cdecl)]
	private static extern int carina_a1088_viture_start([MarshalAs(UnmanagedType.FunctionPtr)] CarinaA1088PoseCallBack pose_callback, [MarshalAs(UnmanagedType.FunctionPtr)] CarinaA1088VsyncCallBack vsync_callback, [MarshalAs(UnmanagedType.FunctionPtr)] CarinaA1088ImuCallBack imu_callback, [MarshalAs(UnmanagedType.FunctionPtr)] CarinaA1088CameraCallBack img_callback, [MarshalAs(UnmanagedType.FunctionPtr)] CarinaA1088PointsCallBack points_callback, [MarshalAs(UnmanagedType.FunctionPtr)] CarinaA1088EventCallBack event_callback);

	[DllImport("carina_vio", CallingConvention = CallingConvention.Cdecl)]
	private static extern int carina_a1088_viture_stop();

	[DllImport("carina_vio", CallingConvention = CallingConvention.Cdecl)]
	private static extern int carina_a1088_viture_release();

	[DllImport("carina_vio", CallingConvention = CallingConvention.Cdecl)]
	private static extern int carina_a1088_viture_pause();

	[DllImport("carina_vio", CallingConvention = CallingConvention.Cdecl)]
	private static extern int carina_a1088_viture_resume();

	[DllImport("carina_vio", CallingConvention = CallingConvention.Cdecl)]
	private static extern IntPtr carina_a1088_viture_get_config();

	[DllImport("carina_vio", CallingConvention = CallingConvention.Cdecl)]
	private static extern int carina_a1088_viture_reset_pose();

	[DllImport("carina_vio", CallingConvention = CallingConvention.Cdecl)]
	private static extern IntPtr carina_a1088_viture_get_sn();

	[DllImport("carina_vio", CallingConvention = CallingConvention.Cdecl)]
	private static extern IntPtr carina_a1088_viture_get_cam_param();

	[DllImport("carina_vio", CallingConvention = CallingConvention.Cdecl)]
	private static extern int carina_a1088_viture_get_imu_pose([MarshalAs(UnmanagedType.LPArray, SizeConst = 32)] float[] pose, double predicttime);

	[DllImport("carina_vio", CallingConvention = CallingConvention.Cdecl)]
	private static extern int carina_a1088_viture_get_gl_pose([MarshalAs(UnmanagedType.LPArray, SizeConst = 32)] float[] pose, double predicttime);

	[DllImport("carina_vio", CallingConvention = CallingConvention.Cdecl)]
	private static extern int carina_a1088_viture_send_custom_data([MarshalAs(UnmanagedType.LPArray, SizeConst = 512)] byte[] data, int len = 512);

	[DllImport("carina_vio", CallingConvention = CallingConvention.Cdecl)]
	private static extern IntPtr carina_a1088_viture_read_custom_data(int len);

	[DllImport("msvcrt.dll", CallingConvention = CallingConvention.Cdecl)]
	private static extern void free(IntPtr ptr);

	private static byte[] GetBufferData(IntPtr ptr)
	{
		byte[] array = new byte[512];
		try
		{
			if (ptr != IntPtr.Zero)
			{
				Marshal.Copy(ptr, array, 0, 512);
			}
		}
		catch (Exception ex)
		{
			Logger.Error(ex.Message, ex.StackTrace);
		}
		return array;
	}

	private static void OnCarinaA1088PoseCallBack(IntPtr data, double timestamp)
	{
		Marshal.Copy(data, poseData, 0, poseData.Length);
		CarinaNative.PoseUpdate?.Invoke(poseData, timestamp);
	}

	private static void OnCarinaA1088VsyncCallBack(double timestamp)
	{
		CarinaNative.VsyncUpdate?.Invoke(timestamp);
	}

	private static void OnCarinaA1088ImuCallBack(IntPtr data, double timestamp)
	{
		Marshal.Copy(data, imuData, 0, imuData.Length);
		CarinaNative.ImuUpdate?.Invoke(imuData, timestamp);
	}

	private static void OnCarinaA1088CameraCallBack(IntPtr left, IntPtr right, IntPtr left1, IntPtr right1, double timestamp, int width, int height)
	{
		CarinaNative.CameraImageUpdate?.Invoke(left, right, left1, right1, timestamp, width, height);
	}

	private static void OnCarinaA1088PointsCallBack(ref CarinaPoints points, double timestamp)
	{
		CarinaNative.PointsUpdate?.Invoke(points, timestamp);
	}

	private static void OnCarinaA1088EventCallBack(byte eventCode)
	{
		CarinaNative.EventUpdate?.Invoke(eventCode);
	}

	public static bool Init(string config_file = "Assets/Configs/custom_config.yaml", string databaseFile = "Assets/Configs/database.bin")
	{
		if (!GlassesDeviceManager.IsRunInUnityEditor)
		{
			string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
			if (!Path.IsPathRooted(config_file))
			{
				config_file = Path.Combine(baseDirectory, config_file);
			}
			if (!Path.IsPathRooted(databaseFile))
			{
				databaseFile = Path.Combine(baseDirectory, databaseFile);
			}
		}
		config_file = ((!File.Exists(config_file)) ? string.Empty : File.ReadAllText(config_file));
		int num = carina_a1088_viture_init(config_file, databaseFile);
		Logger.Info($"carina_a1088_viture_init ret: {num}");
		hasInit = num == 0;
		return hasInit;
	}

	public static bool Start()
	{
		_carinaA1088PoseCallBack = OnCarinaA1088PoseCallBack;
		_carinaA1088VsyncCallBack = OnCarinaA1088VsyncCallBack;
		_carinaA1088ImuCallBack = OnCarinaA1088ImuCallBack;
		_carinaA1088CameraCallBack = OnCarinaA1088CameraCallBack;
		_carinaA1088PointsCallBack = OnCarinaA1088PointsCallBack;
		_carinaA1088EventCallBack = OnCarinaA1088EventCallBack;
		int num = -1;
		num = carina_a1088_viture_start(_carinaA1088PoseCallBack, _carinaA1088VsyncCallBack, _carinaA1088ImuCallBack, _carinaA1088CameraCallBack, _carinaA1088PointsCallBack, _carinaA1088EventCallBack);
		Logger.Info($"carina_a1088_viture_start ret: {num}");
		if (num == 0)
		{
			num = carina_a1088_viture_resume();
			Logger.Info($"carina_a1088_viture_resume ret: {num}");
		}
		hasStart = num == 0;
		return hasStart;
	}

	public static bool ResetPose()
	{
		return carina_a1088_viture_reset_pose() == 0;
	}

	public static void Stop()
	{
		if (hasStart)
		{
			carina_a1088_viture_pause();
			carina_a1088_viture_stop();
		}
	}

	public static void Release()
	{
		try
		{
			if (hasInit)
			{
				hasInit = false;
				try
				{
					carina_a1088_viture_release();
					return;
				}
				catch (Exception ex)
				{
					Logger.Error(ex.Message, ex.StackTrace);
					return;
				}
			}
		}
		catch (Exception ex2)
		{
			Logger.Warning(ex2.Message);
		}
	}

	public static string GetConfig()
	{
		if (!hasInit)
		{
			return string.Empty;
		}
		IntPtr intPtr = carina_a1088_viture_get_config();
		if (intPtr == IntPtr.Zero)
		{
			return string.Empty;
		}
		return Marshal.PtrToStringAnsi(intPtr) ?? string.Empty;
	}

	public static int WriteData(byte[] data)
	{
		return carina_a1088_viture_send_custom_data(data, data.Length);
	}

	public static byte[]? ReadData()
	{
		byte[] result = null;
		try
		{
			IntPtr intPtr = carina_a1088_viture_read_custom_data(512);
			if (intPtr != IntPtr.Zero)
			{
				result = GetBufferData(intPtr);
			}
			return result;
		}
		catch (Exception ex)
		{
			Logger.Warning(ex.Message);
			return result;
		}
	}

	public static string GetSN()
	{
		string empty = string.Empty;
		IntPtr intPtr = carina_a1088_viture_get_sn();
		if (intPtr == IntPtr.Zero)
		{
			return empty;
		}
		try
		{
			return Marshal.PtrToStringAnsi(intPtr);
		}
		finally
		{
			free(intPtr);
		}
	}

	public static string GetCameraParam()
	{
		string empty = string.Empty;
		IntPtr intPtr = carina_a1088_viture_get_cam_param();
		if (intPtr == IntPtr.Zero)
		{
			return empty;
		}
		try
		{
			return Marshal.PtrToStringAnsi(intPtr);
		}
		finally
		{
			free(intPtr);
		}
	}

	public static bool GetPose(double predicttime, float[] pose)
	{
		if (carina_a1088_viture_get_imu_pose(pose, predicttime) != 0)
		{
			return false;
		}
		return true;
	}
}
