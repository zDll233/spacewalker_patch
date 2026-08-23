using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Labs.Controls;
using Avalonia.Markup.Xaml.XamlIl.Runtime;
using Avalonia.Media;
using Avalonia.Styling;
using Avalonia.Themes.Fluent;
using Avalonia.Threading;
using CompiledAvaloniaXaml;
using NLog;
using SpaceWalker.Assets.Languages;
using SpaceWalker.Helper;
using SpaceWalker.ViewModels;
using VitureCommonLibrary;

namespace SpaceWalker;

public class App : Application
{
	[CompilerGenerated]
	private static Action<object> _0021XamlIlPopulateOverride;

	public static ILogger Logger => Program.Logger;

	public override void Initialize()
	{
		RegisterLooseFonts();
		CultureInfo currentUICulture = CultureInfo.CurrentUICulture;
		Logger.Info($"currentUICulture: {currentUICulture}");
		SpaceWalker.Assets.Languages.Resources.Culture = currentUICulture;
		_0021XamlIlPopulateTrampoline(this);
	}

	private static void RegisterLooseFonts()
	{
		try
		{
			string text = Path.Combine(AppContext.BaseDirectory, "Assets", "Fonts");
			if (!Directory.Exists(text))
			{
				Logger.Warn("RegisterLooseFonts: dir not found: " + text);
				return;
			}
			string[] files = Directory.GetFiles(text, "*.ttf");
			if (files.Length == 0)
			{
				Logger.Warn("RegisterLooseFonts: no .ttf under " + text);
				return;
			}
			FileSystemFontCollection fontCollection = new FileSystemFontCollection(new Uri("fonts:swsc", UriKind.Absolute), files);
			FontManager.Current.AddFontCollection(fontCollection);
			Logger.Info($"RegisterLooseFonts: registered {files.Length} CJK font file(s) as fonts:swsc");
		}
		catch (Exception exception)
		{
			Logger.Error(exception, "RegisterLooseFonts failed");
		}
	}

	public override void OnFrameworkInitializationCompleted()
	{
		Avalonia.Threading.Dispatcher.UIThread.UnhandledException += delegate(object _, DispatcherUnhandledExceptionEventArgs e)
		{
			Logger.Error(e.Exception, "[UIThread] " + e.Exception.GetType().Name + ": " + e.Exception.Message);
			e.Handled = true;
		};
		if (base.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime classicDesktopStyleApplicationLifetime)
		{
			ThemeManager.Instance.ApplySavedTheme();
			MainViewModel mainViewModel2 = (MainViewModel)(base.DataContext = new MainViewModel());
			classicDesktopStyleApplicationLifetime.MainWindow = new MainWindow2
			{
				DataContext = base.DataContext
			};
			classicDesktopStyleApplicationLifetime.Exit += App_Exit;
			string appVersion = mainViewModel2.AppVersion;
			TelemetryHelper.SetAppVersion(appVersion ?? string.Empty);
			TelemetryHelper.Capture("SpaceWalker " + appVersion + " Startup");
			try
			{
				Task.Run(delegate
				{
					SudoVirtualDisplay.Instance.Init();
				});
			}
			catch (Exception ex)
			{
				Logger.Error(ex, ex.Message);
			}
		}
		base.OnFrameworkInitializationCompleted();
	}

	private void App_Exit(object? sender, ControlledApplicationLifetimeExitEventArgs e)
	{
		Logger.Info($"Application exiting {e.ApplicationExitCode}...");
		try
		{
			TelemetryHelper.Capture("App_Exit");
		}
		catch (Exception ex)
		{
			Logger.Error(ex, ex.Message);
		}
		Task task = Task.Run(delegate
		{
			try
			{
				MouseHook.UnSetHook();
			}
			catch (Exception ex4)
			{
				Logger.Error(ex4, ex4.Message);
			}
			try
			{
				ProcessManager.Kill();
			}
			catch (Exception ex5)
			{
				Logger.Error(ex5, ex5.Message);
			}
			try
			{
				SudoVirtualDisplay.Instance.Dispose();
			}
			catch (Exception ex6)
			{
				Logger.Error(ex6, ex6.Message);
			}
			try
			{
				WindowMover.Instance.Dispose();
			}
			catch (Exception ex7)
			{
				Logger.Error(ex7, ex7.Message);
			}
			try
			{
				if (GlassesDeviceManager.Instance.UseHidDevice && GlassesDeviceManager.Instance.P6Series)
				{
					P6BiasReader.Clear();
					VitureSlam.SaveOnlineBaisData(GlassesDeviceManager.Instance.GlassesSN);
					VitureSlam.Stop();
				}
			}
			catch (Exception ex8)
			{
				Logger.Error(ex8, ex8.Message);
			}
			try
			{
				GlassesDeviceManager.Instance.Dispose();
			}
			catch (Exception ex9)
			{
				Logger.Error(ex9, ex9.Message);
			}
			try
			{
				DisplayManager2.Instance.ShutdownAsync().Wait(2000);
			}
			catch (Exception ex10)
			{
				Logger.Error(ex10, ex10.Message);
			}
			try
			{
				DisplayManager2.Instance.ChangeNotifyAsync().Wait(1000);
			}
			catch (Exception ex11)
			{
				Logger.Error(ex11, ex11.Message);
			}
		});
		try
		{
			task.Wait(TimeSpan.FromSeconds(6.0));
		}
		catch (Exception ex2)
		{
			Logger.Error(ex2, ex2.Message);
		}
		Logger.Info("App_Exit: force-killing own process to avoid a hung graceful shutdown");
		try
		{
			LogManager.Flush(TimeSpan.FromMilliseconds(500.0));
		}
		catch
		{
		}
		try
		{
			Process.GetCurrentProcess().Kill();
		}
		catch (Exception ex3)
		{
			Logger.Error(ex3, ex3.Message);
			Environment.Exit(e.ApplicationExitCode);
		}
	}

	[CompilerGenerated]
	private static void _0021XamlIlPopulate(IServiceProvider P_0, App P_1)
	{
		CompiledAvaloniaXaml.XamlIlContext.Context<App> context = new CompiledAvaloniaXaml.XamlIlContext.Context<App>(P_0, new object[1] { _0021AvaloniaResources.NamespaceInfo_003A_002FApp_002Eaxaml.Singleton }, "avares://SpaceWalker/App.axaml")
		{
			RootObject = P_1,
			IntermediateRoot = P_1
		};
		App app;
		App app2 = (app = P_1);
		context.PushParent(app);
		app.RequestedThemeVariant = ThemeVariant.Light;
		ResourceDictionary resourceDictionary;
		ResourceDictionary resources = (resourceDictionary = new ResourceDictionary());
		context.PushParent(resourceDictionary);
		resourceDictionary.MergedDictionaries.Add(_0021AvaloniaResources.Build_003A_002FThemes_002FColors_002Eaxaml(XamlIlRuntimeHelpers.CreateRootServiceProviderV3(context)));
		context.PopParent();
		app.Resources = resources;
		app.Styles.Add(new FluentTheme(context));
		app.Styles.Add(new ControlThemes(context));
		app.Styles.Add(_0021AvaloniaResources.Build_003A_002FControls_002FStyles_002Eaxaml(XamlIlRuntimeHelpers.CreateRootServiceProviderV3(context)));
		context.PopParent();
		if (app2 is StyledElement styled)
		{
			NameScope.SetNameScope(styled, context.AvaloniaNameScope);
		}
		context.AvaloniaNameScope.Complete();
	}

	[CompilerGenerated]
	private static void _0021XamlIlPopulateTrampoline(App P_0)
	{
		if (_0021XamlIlPopulateOverride != null)
		{
			_0021XamlIlPopulateOverride(P_0);
		}
		else
		{
			_0021XamlIlPopulate(XamlIlRuntimeHelpers.CreateRootServiceProviderV3(null), P_0);
		}
	}
}
