using System;
using System.Runtime.InteropServices;

namespace VitureCommonLibrary;

public class CamPoseEstimator : IDisposable
{
	private IntPtr _handle;

	private bool _hasInit;

	private bool _hasStart;

	private bool _disposed;

	private CpeCameraParams cpeCameraParams;

	private readonly CpePoseCallback _poseCallback;

	private readonly CpeCameraParamsRequest _cameraParamsCallback;

	private static readonly Lazy<CamPoseEstimator> instance = new Lazy<CamPoseEstimator>(() => new CamPoseEstimator());

	public bool HasInit
	{
		get
		{
			if (_hasInit)
			{
				return _handle != IntPtr.Zero;
			}
			return false;
		}
	}

	public bool HasStart
	{
		get
		{
			if (_hasStart)
			{
				return _handle != IntPtr.Zero;
			}
			return false;
		}
	}

	public bool IsConnected => CpeNative.Cpe_IsConnected(_handle) != 0;

	public static CamPoseEstimator Instance => instance.Value;

	public event Action<float[]>? PoseUpdated;

	private CamPoseEstimator()
	{
		_handle = CpeNative.Cpe_Create();
		if (_handle == IntPtr.Zero)
		{
			Logger.Error("Cpe_Create return null!");
		}
		_poseCallback = OnPoseUpdated;
		_cameraParamsCallback = OnCameraParamsRequested;
		CpeNative.Cpe_SetPoseCallback(_handle, _poseCallback);
		CpeNative.Cpe_SetCameraParamsRequestCallback(_handle, _cameraParamsCallback);
	}

	~CamPoseEstimator()
	{
		Dispose(disposing: false);
	}

	public bool Init(R6CameraParam param, float screenInch = 16f, float aspectRatio = 0.625f)
	{
		_hasInit = false;
		if (_handle == IntPtr.Zero)
		{
			return false;
		}
		cpeCameraParams = ConvertCameraParams(param);
		int num = CpeNative.Cpe_Init(_handle, "", screenInch, aspectRatio);
		if (num != 0)
		{
			Logger.Warning($"CamPoseEstimator Init failed with code {num}");
			return false;
		}
		_hasInit = true;
		return true;
	}

	public bool Start()
	{
		if (!_hasInit || _handle == IntPtr.Zero)
		{
			return false;
		}
		int num = CpeNative.Cpe_Start(_handle);
		if (num != 0)
		{
			Logger.Warning($"CamPoseEstimator Cpe_Start failed with code {num}");
			return false;
		}
		_hasStart = true;
		return true;
	}

	public void Stop()
	{
		_hasStart = false;
		if (_hasInit && !(_handle == IntPtr.Zero))
		{
			SetDebugMode(debug: false);
			CpeNative.Cpe_Stop(_handle);
		}
	}

	public void ResetAnchor()
	{
		if (_hasInit && !(_handle == IntPtr.Zero))
		{
			CpeNative.Cpe_ResetAnchor(_handle);
		}
	}

	public void SetDebugMode(bool debug)
	{
		if (_hasInit && !(_handle == IntPtr.Zero))
		{
			CpeNative.Cpe_SetDebugMode(_handle, debug);
		}
	}

	private static int GetValidLen(float[] array)
	{
		if (array == null || array.Length == 0)
		{
			return 0;
		}
		for (int num = array.Length - 1; num >= 0; num--)
		{
			if (array[num] != 0f)
			{
				return num + 1;
			}
		}
		return 0;
	}

	private static CpeCameraParams ConvertCameraParams(R6CameraParam param)
	{
		float[] array = param.DistortionCoeffs.ToArray();
		int validLen = GetValidLen(array);
		CpeCameraParams result = default(CpeCameraParams);
		result.fx = param.Intrinsics[0];
		result.fy = param.Intrinsics[1];
		result.cx = param.Intrinsics[2];
		result.cy = param.Intrinsics[3];
		result.width = (int)param.Width;
		result.height = (int)param.Height;
		result.distCoeffsCount = validLen;
		result.distCoeffs = array;
		return result;
	}

	private void OnPoseUpdated(IntPtr dataPtr)
	{
		float[] array = new float[14];
		Marshal.Copy(dataPtr, array, 0, 14);
		this.PoseUpdated?.Invoke(array);
	}

	private void OnCameraParamsRequested(ref CpeCameraParams param)
	{
		param = cpeCameraParams;
	}

	public void Dispose()
	{
		Dispose(disposing: true);
		GC.SuppressFinalize(this);
	}

	protected virtual void Dispose(bool disposing)
	{
		if (!_disposed)
		{
			Stop();
			if (_handle != IntPtr.Zero)
			{
				CpeNative.Cpe_Destroy(_handle);
				_handle = IntPtr.Zero;
			}
			_disposed = true;
		}
	}
}
