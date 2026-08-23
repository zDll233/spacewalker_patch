using System;
using System.Diagnostics;
using System.Globalization;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Logging;
using NLog;
using NLog.Config;
using ReactiveUI.Avalonia;
using ReactiveUI.Builder;
using SpaceWalker.Helper;
using VitureCommonLibrary;

namespace SpaceWalker;

internal class Program
{
	public static ILogger Logger;

	[STAThread]
	public static void Main(string[] args)
	{
		string environmentVariable = Environment.GetEnvironmentVariable("VITURE_UI_CULTURE");
		if (!string.IsNullOrWhiteSpace(environmentVariable))
		{
			CultureInfo.CurrentUICulture = (CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo(environmentVariable));
		}
		try
		{
			LogManager.Configuration = new XmlLoggingConfiguration("Assets/Configs/Nlog.config");
			LogManager.ReconfigExistingLoggers();
			Logger = LogManager.GetCurrentClassLogger();
			VitureCommonLibrary.Logger.Initialize(Logger);
			AppDomain.CurrentDomain.UnhandledException += delegate(object _, UnhandledExceptionEventArgs e)
			{
				Exception ex4 = e.ExceptionObject as Exception;
				Logger.Error(ex4, "[AppDomain] " + ex4?.GetType().Name + ": " + ex4?.Message);
			};
			TaskScheduler.UnobservedTaskException += delegate(object? _, UnobservedTaskExceptionEventArgs e)
			{
				Logger.Error(e.Exception, "[UnobservedTask] " + e.Exception.GetType().Name + ": " + e.Exception.Message);
				e.SetObserved();
			};
			Task.Run(delegate
			{
				try
				{
					TelemetryHelper.InitPosthog("phc_alzW8qeHdBNM2opYCW8v8akQlVq9fqFYywXVYqJ95vo");
				}
				catch (Exception ex2)
				{
					Logger.Error(ex2, "[TelemetryInit.Posthog] " + ex2.Message);
				}
				try
				{
					TelemetryHelper.InitSentry("https://94f6a1db3fb49f5580d71399ee60a99d@sentry.viture.dev/10");
				}
				catch (Exception ex3)
				{
					Logger.Error(ex3, "[TelemetryInit.Sentry] " + ex3.Message);
				}
			});
			KillAllProcess();
			BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
		}
		catch (Exception ex)
		{
			Logger.Error(ex, ex.Message);
		}
	}

	private static void KillAllProcess()
	{
		try
		{
			int processId = Environment.ProcessId;
			Process[] processesByName = Process.GetProcessesByName("SpaceWalker");
			foreach (Process process in processesByName)
			{
				if (process.Id != processId && !process.HasExited)
				{
					process.Kill();
				}
			}
		}
		catch (Exception ex)
		{
			Logger.Error(ex, ex.Message);
		}
	}

	public static AppBuilder BuildAvaloniaApp()
	{
		return AppBuilder.Configure<App>().UsePlatformDetect().With(new Win32PlatformOptions
		{
			DpiAwareness = Win32DpiAwareness.PerMonitorDpiAware
		})
			.WithInterFont()
			.LogToTrace(LogEventLevel.Warning)
			.UseReactiveUI(delegate(ReactiveUIBuilder x)
			{
				x.WithExceptionHandler(Observer.Create(delegate(Exception ex)
				{
					Logger.Error(ex, "[ReactiveUI] " + ex.GetType().Name + ": " + ex.Message);
				}));
			});
	}
}
