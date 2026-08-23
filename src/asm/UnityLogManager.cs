using System.IO;
using UnityEngine;
using VitureCommonLibrary;

public class UnityLogManager : MonoBehaviour
{
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
	private static void InitTraceListener()
	{
		VitureCommonLibrary.Logger.Initialize(Path.Combine(Application.streamingAssetsPath, "Configs/Nlog.config"));
	}

	private void Awake()
	{
		Application.logMessageReceivedThreaded += HandleLog;
	}

	private void OnDestroy()
	{
		Application.logMessageReceivedThreaded -= HandleLog;
	}

	private void HandleLog(string logString, string stackTrace, LogType type)
	{
		switch (type)
		{
		case LogType.Log:
			VitureCommonLibrary.Logger.Info(logString);
			break;
		case LogType.Warning:
			VitureCommonLibrary.Logger.Warning(logString);
			break;
		case LogType.Error:
		case LogType.Exception:
			VitureCommonLibrary.Logger.Error(logString, stackTrace);
			break;
		case LogType.Assert:
			break;
		}
	}
}
