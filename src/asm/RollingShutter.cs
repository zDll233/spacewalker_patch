using System;
using UnityEngine;
using VitureCommonLibrary;

public static class RollingShutter
{
	public static bool Enabled = true;

	public static float ScanSign = -1f;

	public static float Strength = 1f;

	public static float ScanTimeMsOverride = 0f;

	public static float OmegaSmooth = 0.3f;

	public static float MaxRateRad = 20f;

	private static readonly int ID_YawRate = Shader.PropertyToID("_RS_YawRate");

	private static readonly int ID_PitchRate = Shader.PropertyToID("_RS_PitchRate");

	private static readonly int ID_ScanTime = Shader.PropertyToID("_RS_ScanTime");

	private static readonly int ID_ScanSign = Shader.PropertyToID("_RS_ScanSign");

	private static readonly int ID_FovH = Shader.PropertyToID("_RS_FovH");

	private static readonly int ID_FovV = Shader.PropertyToID("_RS_FovV");

	private static readonly int ID_Strength = Shader.PropertyToID("_RS_Strength");

	private static bool _argsParsed;

	private static bool _fed;

	private static Quaternion _prevQ = Quaternion.identity;

	private static double _prevTime = -1.0;

	private static Vector2 _omega = Vector2.zero;

	private static int _lastLoggedRefresh = int.MinValue;

	public static bool Active
	{
		get
		{
			if (Enabled && Strength > 0f)
			{
				return _fed;
			}
			return false;
		}
	}

	public static void Feed(Quaternion appliedRotation, Camera cam)
	{
		EnsureArgs();
		if (!Enabled)
		{
			_fed = false;
			return;
		}
		double realtimeSinceStartupAsDouble = Time.realtimeSinceStartupAsDouble;
		if (_prevTime > 0.0)
		{
			float num = (float)(realtimeSinceStartupAsDouble - _prevTime);
			if (num > 0.0001f)
			{
				Quaternion quaternion = Quaternion.Inverse(_prevQ) * appliedRotation;
				if (quaternion.w < 0f)
				{
					quaternion.x = 0f - quaternion.x;
					quaternion.y = 0f - quaternion.y;
					quaternion.z = 0f - quaternion.z;
					quaternion.w = 0f - quaternion.w;
				}
				Vector2 b = new Vector2(quaternion.x, quaternion.y) * (2f / num);
				b.x = Mathf.Clamp(b.x, 0f - MaxRateRad, MaxRateRad);
				b.y = Mathf.Clamp(b.y, 0f - MaxRateRad, MaxRateRad);
				_omega = Vector2.Lerp(_omega, b, OmegaSmooth);
			}
		}
		_prevQ = appliedRotation;
		_prevTime = realtimeSinceStartupAsDouble;
		float num2 = MathF.PI / 180f * ((cam != null && cam.fieldOfView > 0f) ? cam.fieldOfView : 28.147f);
		float num3 = ((cam != null && cam.aspect > 0f) ? cam.aspect : 1.7777778f);
		float value = 2f * Mathf.Atan(Mathf.Tan(num2 * 0.5f) * num3);
		float value2 = ((ScanTimeMsOverride > 0f) ? (ScanTimeMsOverride * 0.001f) : (1f / RefreshHz()));
		Shader.SetGlobalFloat(ID_YawRate, _omega.y);
		Shader.SetGlobalFloat(ID_PitchRate, _omega.x);
		Shader.SetGlobalFloat(ID_ScanTime, value2);
		Shader.SetGlobalFloat(ID_ScanSign, ScanSign);
		Shader.SetGlobalFloat(ID_FovH, value);
		Shader.SetGlobalFloat(ID_FovV, num2);
		Shader.SetGlobalFloat(ID_Strength, Strength);
		_fed = true;
	}

	public static void ResetMotion()
	{
		_prevTime = -1.0;
		_omega = Vector2.zero;
	}

	private static float RefreshHz()
	{
		Resolution currentResolution = Screen.currentResolution;
		int refreshRate = currentResolution.refreshRate;
		float num;
		string arg;
		if (refreshRate > 1)
		{
			num = refreshRate;
			arg = "Screen.currentResolution";
		}
		else if (Application.targetFrameRate > 1)
		{
			num = Application.targetFrameRate;
			arg = "Application.targetFrameRate";
		}
		else
		{
			num = 120f;
			arg = "fallback-120";
		}
		if (refreshRate != _lastLoggedRefresh)
		{
			_lastLoggedRefresh = refreshRate;
			float num2 = ((ScanTimeMsOverride > 0f) ? ScanTimeMsOverride : (1000f / num));
			VitureCommonLibrary.Logger.Info($"RollingShutter refresh: Screen.currentResolution={currentResolution.width}x{currentResolution.height}@{refreshRate}Hz " + $"-> using {num}Hz ({arg}), scanTime={num2:F2}ms" + ((ScanTimeMsOverride > 0f) ? " (-rsScanMs override)" : ""));
		}
		return num;
	}

	private static void EnsureArgs()
	{
		if (_argsParsed)
		{
			return;
		}
		_argsParsed = true;
		try
		{
			string[] commandLineArgs = Environment.GetCommandLineArgs();
			for (int i = 0; i < commandLineArgs.Length - 1; i++)
			{
				string text = commandLineArgs[i];
				string text2 = commandLineArgs[i + 1];
				switch (text)
				{
				case "-rollingShutter":
				{
					if (bool.TryParse(text2, out var result2))
					{
						Enabled = result2;
					}
					break;
				}
				case "-rsStrength":
				{
					if (float.TryParse(text2, out var result4))
					{
						Strength = result4;
					}
					break;
				}
				case "-rsScanSign":
				{
					if (float.TryParse(text2, out var result5))
					{
						ScanSign = result5;
					}
					break;
				}
				case "-rsScanMs":
				{
					if (float.TryParse(text2, out var result3))
					{
						ScanTimeMsOverride = result3;
					}
					break;
				}
				case "-rsSmooth":
				{
					if (float.TryParse(text2, out var result))
					{
						OmegaSmooth = result;
					}
					break;
				}
				}
			}
			VitureCommonLibrary.Logger.Info($"RollingShutter args: enabled={Enabled} strength={Strength} " + $"scanSign={ScanSign} scanMs={ScanTimeMsOverride} smooth={OmegaSmooth}");
		}
		catch (Exception ex)
		{
			VitureCommonLibrary.Logger.Warning("RollingShutter.EnsureArgs failed: " + ex.Message);
		}
	}
}
