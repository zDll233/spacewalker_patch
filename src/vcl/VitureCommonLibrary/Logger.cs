#define TRACE
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace VitureCommonLibrary;

public static class Logger
{
	public enum LogLevel
	{
		Debug,
		Info,
		Warning,
		Error,
		Fatal
	}

	private static ILogger? _logger;

	private static readonly bool s_isUnity;

	private static readonly bool s_isUnityEditor;

	private static readonly MethodInfo? s_unityLog;

	private static readonly MethodInfo? s_unityLogWarning;

	private static readonly MethodInfo? s_unityLogError;

	[ThreadStatic]
	private static bool t_inLog;

	public static bool IsUnity => s_isUnity;

	public static bool IsUnityEditor => s_isUnityEditor;

	static Logger()
	{
		try
		{
			Assembly assembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(delegate(Assembly a)
			{
				string name = a.GetName().Name;
				return name == "UnityEngine" || name == "UnityEngine.CoreModule";
			});
			if (assembly == null)
			{
				return;
			}
			s_isUnity = true;
			Type type = assembly.GetType("UnityEngine.Debug");
			Type type2 = assembly.GetType("UnityEngine.Application");
			if (type == null)
			{
				return;
			}
			if (type2 != null)
			{
				PropertyInfo property = type2.GetProperty("isEditor", BindingFlags.Static | BindingFlags.Public);
				if (property != null)
				{
					s_isUnityEditor = (bool)property.GetValue(null);
				}
			}
			s_unityLog = type.GetMethod("Log", new Type[1] { typeof(object) });
			s_unityLogWarning = type.GetMethod("LogWarning", new Type[1] { typeof(object) });
			s_unityLogError = type.GetMethod("LogError", new Type[1] { typeof(object) });
		}
		catch
		{
		}
	}

	public static void Initialize(string config_file = "Assets/Configs/Nlog.config")
	{
		if (!s_isUnityEditor && !Path.IsPathRooted(config_file))
		{
			config_file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, config_file);
		}
		LogManager.Configuration = new XmlLoggingConfiguration(config_file);
		LogManager.ReconfigExistingLoggers();
		_logger = new NLogger();
	}

	public static void Initialize(NLog.ILogger logger)
	{
		_logger = new NLogger(logger);
	}

	public static void InitializeConsoleLogger()
	{
		try
		{
			LoggingConfiguration loggingConfiguration = new LoggingConfiguration();
			loggingConfiguration.AddRule(target: new ConsoleTarget("console")
			{
				Layout = "${longdate} ${uppercase:${level}} - ${message}"
			}, minLevel: NLog.LogLevel.Trace, maxLevel: NLog.LogLevel.Fatal);
			LogManager.Configuration = loggingConfiguration;
			LogManager.ReconfigExistingLoggers();
			_logger = new NLogger();
		}
		catch
		{
		}
	}

	private static string GetStackTraceInformation()
	{
		StackFrame frame = new StackTrace(2, fNeedFileInfo: true).GetFrame(0);
		return $"{Path.GetFileName(frame?.GetFileName())}:L{frame?.GetFileLineNumber()}";
	}

	public static void Debug(string message)
	{
		if (t_inLog)
		{
			return;
		}
		t_inLog = true;
		try
		{
			string text = GetStackTraceInformation() + " " + message;
			if (_logger != null)
			{
				_logger.Debug(text);
			}
			else
			{
				Trace.TraceInformation(text);
			}
			UnityInvoke(s_unityLog, text);
		}
		finally
		{
			t_inLog = false;
		}
	}

	public static void Info(string message)
	{
		if (t_inLog)
		{
			return;
		}
		t_inLog = true;
		try
		{
			string text = GetStackTraceInformation() + " " + message;
			if (_logger != null)
			{
				_logger.Info(text);
			}
			else
			{
				Trace.TraceInformation(text);
			}
			UnityInvoke(s_unityLog, text);
		}
		finally
		{
			t_inLog = false;
		}
	}

	public static void Warning(string message)
	{
		if (t_inLog)
		{
			return;
		}
		t_inLog = true;
		try
		{
			string text = GetStackTraceInformation() + " " + message;
			if (_logger != null)
			{
				_logger.Warning(text);
			}
			else
			{
				Trace.TraceWarning(text);
			}
			UnityInvoke(s_unityLogWarning, text);
		}
		finally
		{
			t_inLog = false;
		}
	}

	public static void Error(string message, string? stackTrace = "")
	{
		if (t_inLog)
		{
			return;
		}
		t_inLog = true;
		try
		{
			string text = (string.IsNullOrWhiteSpace(stackTrace) ? (GetStackTraceInformation() + " " + message) : (GetStackTraceInformation() + " " + message + "\n" + stackTrace));
			if (_logger != null)
			{
				_logger.Error(text);
			}
			else
			{
				Trace.TraceError(text);
			}
			UnityInvoke(s_unityLogError, text);
		}
		finally
		{
			t_inLog = false;
		}
	}

	public static void Fatal(string message, string stackTrace = "")
	{
		if (t_inLog)
		{
			return;
		}
		t_inLog = true;
		try
		{
			string text = (string.IsNullOrWhiteSpace(stackTrace) ? (GetStackTraceInformation() + " " + message) : (GetStackTraceInformation() + " " + message + "\n" + stackTrace));
			if (_logger != null)
			{
				_logger.Fatal(text);
			}
			else
			{
				Trace.TraceError(text);
			}
			UnityInvoke(s_unityLogError, text);
		}
		finally
		{
			t_inLog = false;
		}
	}

	private static void UnityInvoke(MethodInfo? m, string line)
	{
		if (m == null)
		{
			return;
		}
		try
		{
			m.Invoke(null, new object[1] { line });
		}
		catch
		{
		}
	}
}
