using System;
using UnityEngine;
using VitureCommonLibrary;

public static class GestureManager
{
	private static GestureSdk.SWRCallback _swrCallback;

	private static bool _hasInit;

	public static bool HasInit => _hasInit;

	public static event Action<float[]> GesturePoseUpdate;

	public static bool Init(string carinaYamlContent, string modelPath = "")
	{
		_hasInit = false;
		if (string.IsNullOrWhiteSpace(modelPath))
		{
			modelPath = Application.streamingAssetsPath + "/model/";
		}
		VitureCommonLibrary.Logger.Info("gesture_init begin");
		int num = GestureSdk.gesture_init(modelPath);
		if (num != 0)
		{
			VitureCommonLibrary.Logger.Error($"gesture_init failed ret = {num}");
			return false;
		}
		VitureCommonLibrary.Logger.Info("gesture_init success");
		if (string.IsNullOrWhiteSpace(carinaYamlContent))
		{
			VitureCommonLibrary.Logger.Error("carinaYamlContent invalid!");
			return false;
		}
		if (!SetParam(YamlParseHelper.GetParamFromYaml(carinaYamlContent)))
		{
			VitureCommonLibrary.Logger.Error("SetParam failed!");
			return false;
		}
		_swrCallback = OnSwrCallback;
		num = GestureSdk.Register_Gesture_Callbacks(IntPtr.Zero, _swrCallback, IntPtr.Zero, IntPtr.Zero);
		if (num != 0)
		{
			VitureCommonLibrary.Logger.Error($"gesture_init failed ret = {num}");
			return false;
		}
		_hasInit = true;
		return true;
	}

	public static bool Start()
	{
		int num = GestureSdk.gesture_start();
		if (num != 0)
		{
			VitureCommonLibrary.Logger.Error($"gesture_start failed ret = {num}");
			return false;
		}
		return true;
	}

	public static void Uninit()
	{
		if (!_hasInit)
		{
			return;
		}
		_hasInit = false;
		try
		{
			GestureSdk.Register_Gesture_Callbacks(IntPtr.Zero, null, IntPtr.Zero, IntPtr.Zero);
		}
		catch (Exception ex)
		{
			VitureCommonLibrary.Logger.Warning("GestureManager.Uninit unregister callback failed: " + ex.Message);
		}
		try
		{
			int num = GestureSdk.gesture_stop();
			if (num != 0)
			{
				VitureCommonLibrary.Logger.Warning($"gesture_stop ret = {num}");
			}
		}
		catch (Exception ex2)
		{
			VitureCommonLibrary.Logger.Warning("GestureManager.Uninit gesture_stop failed: " + ex2.Message);
		}
		try
		{
			int num2 = GestureSdk.gesture_uninit();
			if (num2 != 0)
			{
				VitureCommonLibrary.Logger.Warning($"gesture_uninit ret = {num2}");
			}
		}
		catch (Exception ex3)
		{
			VitureCommonLibrary.Logger.Warning("GestureManager.Uninit gesture_uninit failed: " + ex3.Message);
		}
		GestureManager.GesturePoseUpdate = null;
		_swrCallback = null;
	}

	public static bool UpdateImage(IntPtr left, IntPtr right, uint size, double timestamp)
	{
		if (!_hasInit)
		{
			return false;
		}
		int num = GestureSdk.OnCameraData(left, right, size, timestamp);
		if (num != 0)
		{
			VitureCommonLibrary.Logger.Error($"OnCameraData failed ret = {num}");
			return false;
		}
		return true;
	}

	public static bool UpdatePose(float[] pose, double timestamp)
	{
		if (!_hasInit)
		{
			return false;
		}
		if (pose.Length != 16)
		{
			VitureCommonLibrary.Logger.Error($"param error: pose.Length = {pose.Length}");
			return false;
		}
		int num = GestureSdk.OnSlamPose(pose, timestamp);
		if (num != 0)
		{
			VitureCommonLibrary.Logger.Error($"OnSlamPose failed ret = {num}");
			return false;
		}
		return true;
	}

	private static bool SetParam(CameraParam param)
	{
		if (_hasInit)
		{
			return false;
		}
		VitureCommonLibrary.Logger.Info("param.ExtrinsicsL: " + string.Join(" ", param.ExtrinsicsL));
		VitureCommonLibrary.Logger.Info("param.ExtrinsicsR: " + string.Join(" ", param.ExtrinsicsR));
		VitureCommonLibrary.Logger.Info("param.ExtrinsicsLR: " + string.Join(" ", param.ExtrinsicsLR));
		int num = GestureSdk.update_mode(1, param.ExtrinsicsL, param.DistL, param.IntrinsicsL, param.ExtrinsicsR, param.DistR, param.IntrinsicsR, param.ExtrinsicsLR);
		if (num != 0)
		{
			VitureCommonLibrary.Logger.Error($"update_mode failed ret = {num}");
			return false;
		}
		VitureCommonLibrary.Logger.Info("update_mode success!");
		return true;
	}

	private static void OnSwrCallback(float[] data)
	{
		if (data.Length == 374 && (data[0] != 0f || data[187] != 0f))
		{
			VitureCommonLibrary.Logger.Debug("OnSwrCallback: " + string.Join(",", data));
		}
		GestureManager.GesturePoseUpdate?.Invoke(data);
	}
}
