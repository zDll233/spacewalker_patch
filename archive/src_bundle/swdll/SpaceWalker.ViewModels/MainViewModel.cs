using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Disposables.Fluent;
using System.Reactive.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Input;
using Avalonia.Labs.Controls;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia.Platform.Storage;
using Avalonia.Svg.Skia;
using Avalonia.Threading;
using GlobalHotKeys;
using GlobalHotKeys.Native.Types;
using NLog;
using ReactiveUI;
using ReactiveUI.Avalonia;
using SpaceWalker.Assets.Languages;
using SpaceWalker.Database;
using SpaceWalker.Helper;
using SpaceWalker.Ipc;
using SpaceWalker.Services.Ota;
using Viture.Ipc.Pubsub;
using Viture.Ipc.Transport;
using VitureCommonLibrary;

namespace SpaceWalker.ViewModels;

public class MainViewModel : ReactiveObject, IActivatableViewModel
{
	private readonly TaskCompletionSource<bool> _clientUpdateGate = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);

	private int _clientUpdateCheckStarted;

	private int filmControlAngle = 30;

	private bool enableHighDpiScale;

	private bool enableReduceMotionBlur;

	private bool handTrack = true;

	private bool smoothFollow = true;

	private bool svsEnable = true;

	private bool svsDebug;

	private bool lockXAxis;

	private bool lockYAxis;

	private bool lockZAxis;

	private LockAxisState lockAxis;

	private VitureDeviceType vitureDeviceType;

	private VitureLayoutMode vitureLayoutMode;

	private LayoutType layoutType;

	private string glassesSN = string.Empty;

	private string firmwareVersion = string.Empty;

	private bool vitureCommConnect;

	private bool vitureDisplayConnect;

	private bool supportHighRefreshRate;

	private int monitorCount;

	private bool turnOffBuildInScreen;

	private bool useUltraWideSize = true;

	private int frameRate = 120;

	private int duty = 100;

	private string skybox = string.Empty;

	private string? customSkyboxFile;

	private double zoomLevel = 1.0;

	private int screenSize;

	private int volumeLevel;

	private int brightnessLevel;

	private bool vitureCommInited;

	private bool vitureAppMode;

	private bool viewAppRunning;

	private bool isBusy;

	public static ILogger Logger = Program.Logger;

	private bool isInit;

	private CompositeDisposable disposables = new CompositeDisposable();

	private bool _topologyTeardownInFlight;

	private int _fenceVddCount;

	private int _fenceInternalCount;

	private bool _vddRestartPrompted;

	private static GlobalHotKeys.HotKeyManager hotKeyManager = new GlobalHotKeys.HotKeyManager();

	private static List<IRegistration> hotKeySubscription = new List<IRegistration>();

	private ContentDialog? _firmwareDialog;

	private ContentDialog? _connectionDialog;

	private static readonly SvgSource WarningIconSvg = SvgSource.Load("avares://SpaceWalker/Assets/Images/ic_warning.svg");

	private const int FeedbackTabIndex = 3;

	private bool _otaFinished;

	private bool _isOnLayoutOrDesktop;

	private int _connectionFailureCount;

	private const int ConnectionFailureFeedbackThreshold = 3;

	private bool _feedbackDialogShowing;

	private ushort curCamParamSegNum;

	private ushort totalCamParamSegNumm = 1;

	private readonly List<byte> camParamBytes = new List<byte>();

	private readonly object stickyVirtualScreenLock = new object();

	private static string CustomSkyboxDir => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "VITURE", "SpaceWalker");

	public ViewModelActivator Activator { get; } = new ViewModelActivator();


	public INavigation NavigationRouter { get; set; }

	public ReactiveCommand<Unit, Unit> ExitAppCmd { get; }

	public ReactiveCommand<Unit, Unit> ShowWindowCmd { get; }

	public ReactiveCommand<Unit, Unit> ResetCmd { get; }

	public ReactiveCommand<Unit, Unit> SvsEnableCmd { get; }

	public ReactiveCommand<Unit, Unit> SvsDebugCmd { get; }

	public ReactiveCommand<double?, Unit> ZoomCmd { get; }

	public ReactiveCommand<int, Unit> ScreenSizeCmd { get; }

	public ReactiveCommand<bool?, Unit> CheckSpacewalkerUpdateCmd { get; }

	public ReactiveCommand<bool?, Unit> CheckFirmwareUpdateCmd { get; }

	public ReactiveCommand<Unit, Unit> ResetAllSettingsCmd { get; }

	public ReactiveCommand<string, Unit> OpenWebUrlCmd { get; }

	public ReactiveCommand<string?, Unit> ChangeSkyboxCmd { get; }

	public ReactiveCommand<Unit, Unit> SelectCustomSkyboxCmd { get; }

	public ReactiveCommand<Unit, Unit> MouseShakeCmd { get; }

	public ReactiveCommand<LockAxisState, Unit> LockAxisCmd { get; }

	public ReactiveCommand<Unit, Unit> UltraWideCmd { get; }

	public ReactiveCommand<Unit, Unit> LaunchCmd { get; }

	public ReactiveCommand<Unit, Unit> ReduceMotionBlurCmd { get; }

	public ReactiveCommand<Unit, Unit> HighDpiScaleCmd { get; }

	public ReactiveCommand<Unit, Unit> TurnOffBuildInScreenCmd { get; }

	public ReactiveCommand<Unit, Unit> SmoothFollowCmd { get; }

	public ReactiveCommand<Unit, Unit> HandTrackCmd { get; }

	public ReactiveCommand<int, Unit> BrightnessCmd { get; }

	public ReactiveCommand<int, Unit> VolumeCmd { get; }

	public ReactiveCommand<Unit, Unit> DeviceFirstInitCmd { get; }

	public string? AppVersion { get; } = UpdateHelper.GetCurrentAppVersion();


	public bool EnableMouseShake
	{
		get
		{
			return MouseShakeDetector.EnableMouseShake;
		}
		private set
		{
			if (MouseShakeDetector.EnableMouseShake != value)
			{
				MouseShakeDetector.EnableMouseShake = value;
				this.RaisePropertyChanged("EnableMouseShake");
			}
		}
	}

	public int FilmControlAngle
	{
		get
		{
			return filmControlAngle;
		}
		private set
		{
			this.RaiseAndSetIfChanged(ref filmControlAngle, value, "FilmControlAngle");
		}
	}

	public bool EnableHighDpiScale
	{
		get
		{
			return enableHighDpiScale;
		}
		private set
		{
			this.RaiseAndSetIfChanged(ref enableHighDpiScale, value, "EnableHighDpiScale");
		}
	}

	public bool EnableReduceMotionBlur
	{
		get
		{
			return enableReduceMotionBlur;
		}
		private set
		{
			this.RaiseAndSetIfChanged(ref enableReduceMotionBlur, value, "EnableReduceMotionBlur");
		}
	}

	public bool HandTrack
	{
		get
		{
			return handTrack;
		}
		private set
		{
			this.RaiseAndSetIfChanged(ref handTrack, value, "HandTrack");
		}
	}

	public bool SmoothFollow
	{
		get
		{
			return smoothFollow;
		}
		private set
		{
			this.RaiseAndSetIfChanged(ref smoothFollow, value, "SmoothFollow");
		}
	}

	public bool SvsEnable
	{
		get
		{
			return svsEnable;
		}
		private set
		{
			this.RaiseAndSetIfChanged(ref svsEnable, value, "SvsEnable");
		}
	}

	public bool SvsDebug
	{
		get
		{
			return svsDebug;
		}
		private set
		{
			this.RaiseAndSetIfChanged(ref svsDebug, value, "SvsDebug");
		}
	}

	public bool LockXAxis
	{
		get
		{
			return lockXAxis;
		}
		private set
		{
			this.RaiseAndSetIfChanged(ref lockXAxis, value, "LockXAxis");
		}
	}

	public bool LockYAxis
	{
		get
		{
			return lockYAxis;
		}
		private set
		{
			this.RaiseAndSetIfChanged(ref lockYAxis, value, "LockYAxis");
		}
	}

	public bool LockZAxis
	{
		get
		{
			return lockZAxis;
		}
		private set
		{
			this.RaiseAndSetIfChanged(ref lockZAxis, value, "LockZAxis");
		}
	}

	public VitureDeviceType VitureDeviceType
	{
		get
		{
			return vitureDeviceType;
		}
		set
		{
			this.RaiseAndSetIfChanged(ref vitureDeviceType, value, "VitureDeviceType");
		}
	}

	public VitureLayoutMode VitureLayoutMode
	{
		get
		{
			return vitureLayoutMode;
		}
		set
		{
			this.RaiseAndSetIfChanged(ref vitureLayoutMode, value, "VitureLayoutMode");
		}
	}

	public LayoutType LayoutType
	{
		get
		{
			return layoutType;
		}
		set
		{
			this.RaiseAndSetIfChanged(ref layoutType, value, "LayoutType");
		}
	}

	public string GlassesSN
	{
		get
		{
			return glassesSN;
		}
		set
		{
			this.RaiseAndSetIfChanged(ref glassesSN, value, "GlassesSN");
		}
	}

	public string FirmwareVersion
	{
		get
		{
			return firmwareVersion;
		}
		set
		{
			this.RaiseAndSetIfChanged(ref firmwareVersion, value, "FirmwareVersion");
		}
	}

	public bool VitureCommConnect
	{
		get
		{
			return vitureCommConnect;
		}
		set
		{
			this.RaiseAndSetIfChanged(ref vitureCommConnect, value, "VitureCommConnect");
			Logger.Info($"VitureCommConnect: {vitureCommConnect}");
			if (vitureCommConnect)
			{
				TelemetryHelper.Capture($"VITURE HID Connect: {vitureCommConnect}");
			}
		}
	}

	public bool VitureDisplayConnect
	{
		get
		{
			return vitureDisplayConnect;
		}
		set
		{
			this.RaiseAndSetIfChanged(ref vitureDisplayConnect, value, "VitureDisplayConnect");
			Logger.Info($"VitureDisplayConnect: {vitureDisplayConnect}");
			if (vitureDisplayConnect)
			{
				TelemetryHelper.Capture($"VITURE Display Connect: {vitureDisplayConnect}");
			}
		}
	}

	public bool SupportHighRefreshRate
	{
		get
		{
			return supportHighRefreshRate;
		}
		set
		{
			this.RaiseAndSetIfChanged(ref supportHighRefreshRate, value, "SupportHighRefreshRate");
		}
	}

	public bool AtLeastOneMonitor
	{
		get
		{
			if (!turnOffBuildInScreen)
			{
				return monitorCount >= 1;
			}
			return false;
		}
	}

	public int MonitorCount
	{
		get
		{
			return monitorCount;
		}
		private set
		{
			this.RaiseAndSetIfChanged(ref monitorCount, value, "MonitorCount");
			this.RaisePropertyChanged("AtLeastOneMonitor");
		}
	}

	public bool TurnOffBuildInScreen
	{
		get
		{
			return turnOffBuildInScreen;
		}
		private set
		{
			this.RaiseAndSetIfChanged(ref turnOffBuildInScreen, value, "TurnOffBuildInScreen");
			this.RaisePropertyChanged("AtLeastOneMonitor");
		}
	}

	public bool UseUltraWideSize
	{
		get
		{
			return useUltraWideSize;
		}
		set
		{
			this.RaiseAndSetIfChanged(ref useUltraWideSize, value, "UseUltraWideSize");
		}
	}

	public int FrameRate
	{
		get
		{
			return frameRate;
		}
		set
		{
			this.RaiseAndSetIfChanged(ref frameRate, value, "FrameRate");
		}
	}

	public int Duty
	{
		get
		{
			return duty;
		}
		set
		{
			this.RaiseAndSetIfChanged(ref duty, value, "Duty");
			SetDutyCmd(duty);
		}
	}

	public string Skybox
	{
		get
		{
			return skybox;
		}
		set
		{
			this.RaiseAndSetIfChanged(ref skybox, value, "Skybox");
		}
	}

	public string? CustomSkyboxFile
	{
		get
		{
			return customSkyboxFile;
		}
		private set
		{
			this.RaiseAndSetIfChanged(ref customSkyboxFile, value, "CustomSkyboxFile");
		}
	}

	public double ZoomLevel
	{
		get
		{
			return zoomLevel;
		}
		set
		{
			this.RaiseAndSetIfChanged(ref zoomLevel, value, "ZoomLevel");
		}
	}

	public int ScreenSize
	{
		get
		{
			return screenSize;
		}
		set
		{
			this.RaiseAndSetIfChanged(ref screenSize, value, "ScreenSize");
		}
	}

	private static bool IsZhCulture => Resources.Culture?.Name.StartsWith("zh", StringComparison.OrdinalIgnoreCase) ?? false;

	public string PrivacyPolicyUrl
	{
		get
		{
			if (!IsZhCulture)
			{
				return "https://www.viture.com/privacy-policy";
			}
			return "https://static.viture.dev/privacy-policy/v1/zh-cn/index.html";
		}
	}

	public string TermsOfServiceUrl
	{
		get
		{
			if (!IsZhCulture)
			{
				return "https://www.viture.com/terms-of-service";
			}
			return "https://static.viture.dev/terms-of-service/v1/zh-cn/index.html";
		}
	}

	public int VolumeLevel
	{
		get
		{
			return volumeLevel;
		}
		set
		{
			this.RaiseAndSetIfChanged(ref volumeLevel, value, "VolumeLevel");
		}
	}

	public int BrightnessLevel
	{
		get
		{
			return brightnessLevel;
		}
		set
		{
			this.RaiseAndSetIfChanged(ref brightnessLevel, value, "BrightnessLevel");
		}
	}

	public bool VitureCommInited
	{
		get
		{
			return vitureCommInited;
		}
		set
		{
			this.RaiseAndSetIfChanged(ref vitureCommInited, value, "VitureCommInited");
		}
	}

	public bool VitureAppMode
	{
		get
		{
			return vitureAppMode;
		}
		set
		{
			this.RaiseAndSetIfChanged(ref vitureAppMode, value, "VitureAppMode");
		}
	}

	public bool ViewAppRunning
	{
		get
		{
			return viewAppRunning;
		}
		private set
		{
			this.RaiseAndSetIfChanged(ref viewAppRunning, value, "ViewAppRunning");
		}
	}

	public bool IsBusy
	{
		get
		{
			return isBusy;
		}
		set
		{
			this.RaiseAndSetIfChanged(ref isBusy, value, "IsBusy");
		}
	}

	public string FaqUrl
	{
		get
		{
			if (!IsZhCulture)
			{
				return "https://www.viture.com/faq";
			}
			return "https://www.viture.com/zh-CN/faq";
		}
	}

	public string ConnectUrl
	{
		get
		{
			if (!IsZhCulture)
			{
				return "https://www.viture.com/academy/spacewalker/desktop#connect-your-xr-glasses";
			}
			return "https://www.viture.com/zh-CN/academy/spacewalker/pc%E7%AB%AF#%E8%BF%9E%E6%8E%A5%E6%82%A8%E7%9A%84xr%E7%9C%BC%E9%95%9C";
		}
	}

	public string CheckInterfaceUrl
	{
		get
		{
			if (!IsZhCulture)
			{
				return "https://www.viture.com/academy/spacewalker/desktop#check-interface-support";
			}
			return "https://www.viture.com/zh-CN/academy/spacewalker/pc%E7%AB%AF#%E6%A3%80%E6%9F%A5-windows-%E4%B8%8A%E7%9A%84%E6%8E%A5%E5%8F%A3%E6%94%AF%E6%8C%81";
		}
	}

	private async Task ExitApp()
	{
		bool flag = ViewAppRunning;
		if (flag)
		{
			flag = !(await ShowExitConfirmDialogAsync());
		}
		if (!flag)
		{
			ShutdownApp();
		}
	}

	internal static void ShutdownApp()
	{
		(Application.Current?.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime)?.Shutdown();
	}

	private static void ShowMainWindow()
	{
		if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime { MainWindow: not null } classicDesktopStyleApplicationLifetime)
		{
			classicDesktopStyleApplicationLifetime.MainWindow.Show();
			classicDesktopStyleApplicationLifetime.MainWindow.WindowState = WindowState.Normal;
			classicDesktopStyleApplicationLifetime.MainWindow.Activate();
		}
	}

	public async Task SendResetCmd(RecenterSource source)
	{
		TelemetryHelper.Capture("SendResetCmd");
		if (ProcessManager.Running && !GlassesDeviceManager.Instance.SupportNative3Dof)
		{
			if (DisplayManager2.Instance.CurrentViewDisplays.Length != 0)
			{
				if (source == RecenterSource.Hotkey)
				{
					HomeMainWindowToPrimary();
				}
				await ProcessManager.RecenterAsync(source);
			}
		}
		else if (GlassesDeviceManager.Instance.SupportNative3Dof)
		{
			GlassesMsgSemaphore.SendMsgAndAwaitAck(R6NewerMsgId.TF_CMD_DISPLAY_RECENTER_W, typeof(R6NewerHidMessage));
		}
	}

	private static void HomeMainWindowToPrimary()
	{
		MainWindow2.CenterMainWindowOnPrimary();
	}

	private void ToggleSvsEnable()
	{
		SvsEnable = !SvsEnable;
		DbManager.Instance.Settings.SvsEnable = SvsEnable;
		DbManager.Instance.SaveSettings();
		if (VitureCommConnect && IsNative3DofMode())
		{
			if (SvsEnable)
			{
				Task.Run((Action)StickyVirtualScreenStart);
			}
			else
			{
				StickyVirtualScreenStop();
			}
		}
	}

	private void ToggleSvsDebug()
	{
		SvsDebug = !SvsDebug;
		DbManager.Instance.Settings.SvsDebug = SvsDebug;
		DbManager.Instance.SaveSettings();
		if (GlassesDeviceManager.Instance.SupportNative3Dof)
		{
			CamPoseEstimator.Instance.SetDebugMode(SvsDebug);
		}
	}

	public void StartClientUpdateCheckInBackground()
	{
		if (Interlocked.Exchange(ref _clientUpdateCheckStarted, 1) == 0)
		{
			RunClientUpdateCheckAsync();
		}
	}

	private async Task RunClientUpdateCheckAsync()
	{
		bool canContinueLaunch = true;
		try
		{
			if (await Task.Run((Func<Task<bool>?>)UpdateHelper.CheckUpdates))
			{
				bool choseUpdateNow = false;
				await Dispatcher.UIThread.InvokeAsync(async delegate
				{
					choseUpdateNow = await ShowSpacewalkerUpdateDialogAsync();
				});
				canContinueLaunch = !choseUpdateNow;
			}
		}
		catch (Exception ex)
		{
			Logger.Warn("[ClientUpdateCheck] " + ex.Message);
		}
		finally
		{
			_clientUpdateGate.TrySetResult(canContinueLaunch);
		}
	}

	private async Task CheckSpacewalkerUpdate(bool? skipIfLastest)
	{
		_ = 2;
		try
		{
			if (await UpdateHelper.CheckUpdates())
			{
				await ShowSpacewalkerUpdateDialogAsync();
			}
			else if (!skipIfLastest.GetValueOrDefault())
			{
				await ShowSpacewalkerUpdateDialogAsync(update: false);
			}
		}
		catch (Exception)
		{
		}
	}

	private async Task<bool> CheckFirmwareUpdate(bool? skipIfLastest)
	{
		_ = 2;
		try
		{
			if (FirmwareOtaManager.Instance == null)
			{
				return false;
			}
			FirmwareVersionCheckResult firmwareVersionCheckResult = await FirmwareOtaManager.Instance.CheckFirmwareVersionAsync();
			if (!VitureCommConnect)
			{
				return false;
			}
			if (firmwareVersionCheckResult.HasNewVersion)
			{
				return await ShowFirmwareUpdateDialogAsync();
			}
			if (skipIfLastest.GetValueOrDefault())
			{
				return false;
			}
			return await ShowFirmwareUpdateDialogAsync(update: false);
		}
		catch (Exception)
		{
		}
		return false;
	}

	public async Task ResetAllSettings()
	{
		if (await ShowResetAllSettingsConfirmDialogAsync())
		{
			DbManager.Instance.ResetSettings();
			LoadSettings();
			ThemeManager.Instance.ApplySavedTheme();
			UnRegisterHotkey();
			RegisterHotKey();
			if (ViewAppRunning)
			{
				await LaunchCmd.Execute();
			}
		}
	}

	private static void OpenWebUrl(string url)
	{
		Process.Start(new ProcessStartInfo(url)
		{
			UseShellExecute = true
		});
	}

	private async Task SelectCustomSkybox()
	{
		string text = FindExistingCustomSkybox();
		if (!string.IsNullOrEmpty(text))
		{
			if (Skybox != text)
			{
				Skybox = text;
				PersistAndPublishSkybox();
			}
		}
		else
		{
			await ChangeSkybox(string.Empty);
		}
	}

	private static string? FindExistingCustomSkybox()
	{
		try
		{
			string result = null;
			DateTime dateTime = DateTime.MinValue;
			foreach (string item in Directory.EnumerateFiles(CustomSkyboxDir, "Custom_Skybox*.*"))
			{
				DateTime lastWriteTimeUtc = File.GetLastWriteTimeUtc(item);
				if (lastWriteTimeUtc >= dateTime)
				{
					dateTime = lastWriteTimeUtc;
					result = item;
				}
			}
			return result;
		}
		catch
		{
			return null;
		}
	}

	private async Task ChangeSkybox(string? imageFile)
	{
		if (imageFile == string.Empty)
		{
			imageFile = await OpenFilePickerAsync();
			if (string.IsNullOrWhiteSpace(imageFile))
			{
				return;
			}
			foreach (string item in Directory.EnumerateFiles(CustomSkyboxDir, "Custom_Skybox*.*"))
			{
				File.Delete(item);
			}
			string destFileName = Path.Combine(CustomSkyboxDir, $"Custom_Skybox_{DateTime.Now:yyyyMMddHHmmssfff}{Path.GetExtension(imageFile)}");
			File.Copy(imageFile, destFileName, overwrite: true);
			Skybox = destFileName;
			CustomSkyboxFile = destFileName;
		}
		else
		{
			Skybox = imageFile ?? string.Empty;
		}
		PersistAndPublishSkybox();
	}

	private void PersistAndPublishSkybox()
	{
		DbManager.Instance.Settings.Skybox = Skybox;
		DbManager.Instance.SaveSettings();
		if (ViewAppRunning && !GlassesDeviceManager.Instance.SupportNative3Dof)
		{
			Channel channel = ProcessManager.Channel;
			if (channel != null)
			{
				TypedTopic<SkyboxPath> typedTopic = Topics.Skybox;
				SkyboxPath value = new SkyboxPath(skybox);
				typedTopic.Publish(channel, in value);
			}
		}
	}

	private void ToggleMouseShake()
	{
		EnableMouseShake = !EnableMouseShake;
		DbManager.Instance.Settings.EnableMouseShake = EnableMouseShake;
		DbManager.Instance.SaveSettings();
	}

	private void UpdateLockAxisState(LockAxisState lockAxis)
	{
		switch (lockAxis)
		{
		case LockAxisState.LockX:
			LockXAxis = !LockXAxis;
			break;
		case LockAxisState.LockY:
			LockYAxis = !LockYAxis;
			break;
		case LockAxisState.LockZ:
			LockZAxis = !LockZAxis;
			break;
		}
		lockAxis = (LockAxisState)((((LockZAxis ? 1u : 0u) << 2) + ((LockYAxis ? 1u : 0u) << 1) + (LockXAxis ? 1u : 0u)) & 7u);
		this.lockAxis = lockAxis;
		DbManager.Instance.Settings.LockAxis = (int)lockAxis;
		DbManager.Instance.SaveSettings();
		if (ViewAppRunning && !GlassesDeviceManager.Instance.SupportNative3Dof)
		{
			Channel channel = ProcessManager.Channel;
			if (channel != null)
			{
				TypedTopic<LockAxisValue> typedTopic = Topics.LockAxis;
				LockAxisValue value = new LockAxisValue(lockAxis);
				typedTopic.Publish(channel, in value);
				TelemetryHelper.Capture("send LockAxisState");
			}
		}
	}

	private void ToggleReduceMotionBlur()
	{
		EnableReduceMotionBlur = !EnableReduceMotionBlur;
		if (_otaFinished)
		{
			Duty = (EnableReduceMotionBlur ? 50 : 99);
		}
		DbManager.Instance.Settings.EnableReduceMotionBlur = EnableReduceMotionBlur;
		DbManager.Instance.SaveSettings();
	}

	private void ToggleHighDpiScale()
	{
		EnableHighDpiScale = !EnableHighDpiScale;
		DbManager.Instance.Settings.EnableHighDPIScale = EnableHighDpiScale;
		DbManager.Instance.SaveSettings();
	}

	private void ToggleTurnOffBuildInScreen()
	{
		bool flag = !DbManager.Instance.Settings.TurnOffBuildInScreen;
		DbManager.Instance.Settings.TurnOffBuildInScreen = flag;
		DbManager.Instance.SaveSettings();
		if (flag && LayoutType == LayoutType.Mirror)
		{
			LayoutType = LayoutType.Extend;
		}
		TurnOffBuildInScreen = flag;
		if (ProcessManager.Running || ViewAppRunning)
		{
			LaunchCmd.Execute().Subscribe();
		}
	}

	private void ToggleSmoothFollow()
	{
		bool value = !DbManager.Instance.Settings.SmoothFollow;
		DbManager.Instance.Settings.SmoothFollow = value;
		DbManager.Instance.SaveSettings();
		SmoothFollow = value;
		if ((!ProcessManager.Running && !ViewAppRunning) || !ProcessManager.Running)
		{
			return;
		}
		Channel channel = ProcessManager.Channel;
		if (channel != null)
		{
			try
			{
				TypedTopic<SmoothFollowValue> typedTopic = Topics.SmoothFollow;
				SmoothFollowValue value2 = new SmoothFollowValue(value);
				typedTopic.Publish(channel, in value2);
			}
			catch (Exception ex)
			{
				Logger.Error(ex.Message, ex.StackTrace);
			}
		}
	}

	private void ToggleHandTrack()
	{
		HandTrack = !HandTrack;
		DbManager.Instance.Settings.HandTrack = HandTrack;
		DbManager.Instance.SaveSettings();
		if (!ProcessManager.Running || GlassesDeviceManager.Instance.UseHidDevice)
		{
			return;
		}
		Channel channel = ProcessManager.Channel;
		if (channel != null)
		{
			try
			{
				TypedTopic<HandTrackValue> typedTopic = Topics.HandTrack;
				HandTrackValue value = new HandTrackValue(HandTrack);
				typedTopic.Publish(channel, in value);
			}
			catch (Exception ex)
			{
				Logger.Error(ex.Message, ex.StackTrace);
			}
		}
	}

	private void ToggleUltraWideMode()
	{
		UseUltraWideSize = !UseUltraWideSize;
		DbManager.Instance.Settings.UseUltraWideSize = UseUltraWideSize;
		DbManager.Instance.SaveSettings();
		if (VitureLayoutMode == VitureLayoutMode.UltraWide && ProcessManager.Running)
		{
			LaunchCmd.Execute().Subscribe();
		}
	}

	private async Task Launch()
	{
		StartClientUpdateCheckInBackground();
		if (await _clientUpdateGate.Task)
		{
			IsBusy = true;
			await DeviceFirstInitCmd.IsExecuting.Where((bool x) => !x).FirstAsync();
			await ChangeScreenLayout2(VitureLayoutMode, LayoutType, FrameRate);
			IsBusy = false;
			if (VitureCommConnect && IsNative3DofMode() && SvsEnable)
			{
				Task.Run((Action)StickyVirtualScreenStart);
			}
		}
	}

	public async Task StepScreenDistance(ZoomDirection direction)
	{
		double step;
		double min;
		double max;
		double num;
		if (GlassesDeviceManager.Instance.SupportNative3Dof)
		{
			step = 1.0;
			min = 0.0;
			max = 9.0;
			num = await Task.Run(() => GlassesDeviceManager.Instance.GetDistance());
		}
		else
		{
			if (!ProcessManager.Running)
			{
				return;
			}
			step = 0.1;
			min = 0.5;
			max = 2.0;
			num = ZoomLevel;
		}
		double value = ((direction == ZoomDirection.ZoomIn) ? (num - step) : (num + step));
		value = Math.Round(Math.Clamp(value, min, max), 1);
		TelemetryHelper.Capture($"StepScreenDistance {direction} -> {value}");
		SetScreenDistance(value);
	}

	private void SetScreenDistance(double? zoom)
	{
		if (!zoom.HasValue)
		{
			return;
		}
		if (ProcessManager.Running && !GlassesDeviceManager.Instance.SupportNative3Dof)
		{
			Channel channel = ProcessManager.Channel;
			if (channel != null)
			{
				TelemetryHelper.Capture($"SendSetZoomCmd {zoom}");
				TypedTopic<ZoomValue> setZoom = Topics.SetZoom;
				ZoomValue value = new ZoomValue(zoom.Value);
				setZoom.Publish(channel, in value);
				ZoomLevel = zoom.Value;
				return;
			}
		}
		GlassesDeviceManager.Instance.SetDistance((int)Math.Round(zoom.Value));
	}

	private void SetScreenSize(int size)
	{
		if (GlassesDeviceManager.Instance.SupportNative3Dof)
		{
			TelemetryHelper.Capture($"SendSetScreenSizeCmd {size}");
			ScreenSize = size;
			GlassesDeviceManager.Instance.SetScreenSize(size);
		}
	}

	public async Task StepBrightness(int delta)
	{
		int num = ((!GlassesDeviceManager.Instance.SupportNative3Dof) ? BrightnessLevel : (await Task.Run(() => GlassesDeviceManager.Instance.GetBrightness())));
		int num2 = GlassesDeviceManager.ClampBrightness(num + delta);
		TelemetryHelper.Capture($"StepBrightness {delta} -> {num2}");
		await SetBrightness(num2);
	}

	public async Task StepVolume(int delta)
	{
		int num;
		int max;
		if (GlassesDeviceManager.Instance.SupportNative3Dof)
		{
			num = await Task.Run(() => GlassesDeviceManager.Instance.GetVolume());
			max = 15;
		}
		else
		{
			num = VolumeLevel;
			max = 8;
		}
		int num2 = Math.Clamp(num + delta, 0, max);
		TelemetryHelper.Capture($"StepVolume {delta} -> {num2}");
		await SetVolume(num2);
	}

	private Task SetBrightness(int val)
	{
		return Task.Run(delegate
		{
			try
			{
				if (ProcessManager.Running && !GlassesDeviceManager.Instance.SupportNative3Dof)
				{
					Channel channel = ProcessManager.Channel;
					if (channel != null)
					{
						TypedTopic<BrightnessValue> setBrightness = Topics.SetBrightness;
						BrightnessValue value = new BrightnessValue(val);
						setBrightness.Publish(channel, in value);
						Dispatcher.UIThread.Post(delegate
						{
							BrightnessLevel = val;
						});
						return;
					}
				}
				GlassesDeviceManager.Instance.SetBrightness(val);
			}
			catch (Exception ex)
			{
				Logger.Error(ex.Message, ex.StackTrace);
			}
		});
	}

	private async Task SetVolume(int val)
	{
		await Task.Run(delegate
		{
			try
			{
				if (ProcessManager.Running && !GlassesDeviceManager.Instance.SupportNative3Dof)
				{
					Channel channel = ProcessManager.Channel;
					if (channel != null)
					{
						TypedTopic<VolumeValue> setVolume = Topics.SetVolume;
						VolumeValue value = new VolumeValue(val);
						setVolume.Publish(channel, in value);
						Dispatcher.UIThread.Post(delegate
						{
							VolumeLevel = val;
						});
						return;
					}
				}
				GlassesDeviceManager.Instance.SetVolume(val);
			}
			catch (Exception ex)
			{
				Logger.Error(ex.Message, ex.StackTrace);
			}
		});
	}

	private async Task DeviceFirstInit()
	{
		if (VitureCommConnect && !GlassesDeviceManager.Instance.SupportNative3Dof)
		{
			await Task.Delay(200);
			Duty = (EnableReduceMotionBlur ? 50 : 99);
			await Task.Delay(200);
			if (GlassesDeviceManager.Instance.P6Series && GlassesDeviceManager.Instance.UseHidDevice)
			{
				await Task.Run((Action)InitVitureSlam);
			}
		}
	}

	public MainViewModel()
	{
		LoadSettings();
		ExitAppCmd = ReactiveCommand.CreateFromTask(ExitApp);
		ShowWindowCmd = ReactiveCommand.Create(ShowMainWindow);
		ResetCmd = ReactiveCommand.CreateFromTask(() => SendResetCmd(RecenterSource.Ui));
		SvsEnableCmd = ReactiveCommand.Create(ToggleSvsEnable);
		SvsDebugCmd = ReactiveCommand.Create(ToggleSvsDebug);
		CheckSpacewalkerUpdateCmd = ReactiveCommand.CreateFromTask<bool?>(CheckSpacewalkerUpdate);
		CheckFirmwareUpdateCmd = ReactiveCommand.CreateFromTask<bool?>(CheckFirmwareUpdate);
		ResetAllSettingsCmd = ReactiveCommand.CreateFromTask(ResetAllSettings);
		OpenWebUrlCmd = ReactiveCommand.Create<string>(OpenWebUrl);
		ChangeSkyboxCmd = ReactiveCommand.CreateFromTask<string>(ChangeSkybox);
		SelectCustomSkyboxCmd = ReactiveCommand.CreateFromTask(SelectCustomSkybox);
		MouseShakeCmd = ReactiveCommand.Create(ToggleMouseShake);
		LockAxisCmd = ReactiveCommand.Create<LockAxisState>(UpdateLockAxisState);
		ReduceMotionBlurCmd = ReactiveCommand.Create(ToggleReduceMotionBlur);
		HighDpiScaleCmd = ReactiveCommand.Create(ToggleHighDpiScale);
		TurnOffBuildInScreenCmd = ReactiveCommand.Create(ToggleTurnOffBuildInScreen);
		SmoothFollowCmd = ReactiveCommand.Create(ToggleSmoothFollow);
		HandTrackCmd = ReactiveCommand.Create(ToggleHandTrack);
		UltraWideCmd = ReactiveCommand.Create(ToggleUltraWideMode);
		ZoomCmd = ReactiveCommand.Create<double?>(SetScreenDistance);
		ScreenSizeCmd = ReactiveCommand.Create<int>(SetScreenSize);
		BrightnessCmd = ReactiveCommand.CreateFromTask<int>(SetBrightness);
		VolumeCmd = ReactiveCommand.CreateFromTask<int>(SetVolume);
		DeviceFirstInitCmd = ReactiveCommand.CreateFromTask(DeviceFirstInit);
		LaunchCmd = ReactiveCommand.CreateFromTask(Launch);
		ResetCmd.ThrownExceptions.Subscribe(delegate(Exception ex)
		{
			Logger.Warn("[ResetCmd] " + ex.Message);
		});
		CheckSpacewalkerUpdateCmd.ThrownExceptions.Subscribe(delegate(Exception ex)
		{
			Logger.Warn("[CheckSpacewalkerUpdateCmd] " + ex.Message);
		});
		CheckFirmwareUpdateCmd.ThrownExceptions.Subscribe(delegate(Exception ex)
		{
			Logger.Warn("[CheckFirmwareUpdateCmd] " + ex.Message);
		});
		ResetAllSettingsCmd.ThrownExceptions.Subscribe(delegate(Exception ex)
		{
			Logger.Warn("[ResetAllSettingsCmd] " + ex.Message);
		});
		ChangeSkyboxCmd.ThrownExceptions.Subscribe(delegate(Exception ex)
		{
			Logger.Warn("[ChangeSkyboxCmd] " + ex.Message);
		});
		SelectCustomSkyboxCmd.ThrownExceptions.Subscribe(delegate(Exception ex)
		{
			Logger.Warn("[SelectCustomSkyboxCmd] " + ex.Message);
		});
		BrightnessCmd.ThrownExceptions.Subscribe(delegate(Exception ex)
		{
			Logger.Warn("[BrightnessCmd] " + ex.Message);
		});
		VolumeCmd.ThrownExceptions.Subscribe(delegate(Exception ex)
		{
			Logger.Warn("[VolumeCmd] " + ex.Message);
		});
		DeviceFirstInitCmd.ThrownExceptions.Subscribe(delegate(Exception ex)
		{
			Logger.Warn("[DeviceFirstInitCmd] " + ex.Message);
		});
		LaunchCmd.ThrownExceptions.Subscribe(delegate(Exception ex)
		{
			Logger.Warn("[LaunchCmd] " + ex.Message);
		});
		ScreenSizeCmd.ThrownExceptions.Subscribe(delegate(Exception ex)
		{
			Logger.Warn("[ScreenSizeCmd] " + ex.Message);
		});
		this.WhenActivated(delegate(CompositeDisposable d)
		{
			Disposable.Create(DoCleanup).DisposeWith(d);
		});
	}

	public async Task Initialize()
	{
		GlassesDeviceManager instance = GlassesDeviceManager.Instance;
		instance.DeviceEnterBootMode = (Action<bool>)Delegate.Combine(instance.DeviceEnterBootMode, new Action<bool>(OnDeviceEnterBootMode));
		await DisplayManager2.Instance.InitAsync();
		IDisposable item = GlassesDeviceManager.Instance.ObservableForProperty((GlassesDeviceManager x) => x.BrightnessLevel).Value().ObserveOn(AvaloniaScheduler.Instance)
			.Subscribe(delegate(int x)
			{
				BrightnessLevel = x;
			}, delegate(Exception ex)
			{
				Logger.Warn("[BrightnessObserver] " + ex.Message);
			});
		disposables.Add(item);
		IDisposable item2 = GlassesDeviceManager.Instance.ObservableForProperty((GlassesDeviceManager x) => x.VolumeLevel).Value().ObserveOn(AvaloniaScheduler.Instance)
			.Subscribe(delegate(int x)
			{
				VolumeLevel = x;
			}, delegate(Exception ex)
			{
				Logger.Warn("[VolumeObserver] " + ex.Message);
			});
		disposables.Add(item2);
		IDisposable item3 = GlassesDeviceManager.Instance.ObservableForProperty((GlassesDeviceManager x) => x.DistanceLevel).Value().ObserveOn(AvaloniaScheduler.Instance)
			.Subscribe(delegate(int x)
			{
				if (GlassesDeviceManager.Instance.SupportNative3Dof)
				{
					ZoomLevel = x;
				}
			}, delegate(Exception ex)
			{
				Logger.Warn("[DistanceObserver] " + ex.Message);
			});
		disposables.Add(item3);
		IDisposable item4 = GlassesDeviceManager.Instance.ObservableForProperty((GlassesDeviceManager x) => x.Native3DofScreenSize).Value().ObserveOn(AvaloniaScheduler.Instance)
			.Subscribe(delegate(int x)
			{
				if (GlassesDeviceManager.Instance.SupportNative3Dof)
				{
					ScreenSize = x;
				}
			}, delegate(Exception ex)
			{
				Logger.Warn("[ScreenSizeObserver] " + ex.Message);
			});
		disposables.Add(item4);
		IDisposable item5 = Observable.Create(delegate(IObserver<bool> observer)
		{
			IDisposable result2 = Observable.FromEvent(delegate(Action<bool> h)
			{
				GlassesDeviceManager instance6 = GlassesDeviceManager.Instance;
				instance6.DeviceConnectChanged = (Action<bool>)Delegate.Combine(instance6.DeviceConnectChanged, h);
			}, delegate(Action<bool> h)
			{
				GlassesDeviceManager instance5 = GlassesDeviceManager.Instance;
				instance5.DeviceConnectChanged = (Action<bool>)Delegate.Remove(instance5.DeviceConnectChanged, h);
			}).Subscribe(observer);
			observer.OnNext(GlassesDeviceManager.Instance.IsConnected);
			return result2;
		}).Synchronize().DistinctUntilChanged()
			.ObserveOn(AvaloniaScheduler.Instance)
			.SelectConcat(OnDeviceConnectChanged, AvaloniaScheduler.Instance)
			.Subscribe(delegate
			{
			}, delegate(Exception ex)
			{
				Logger.Error(ex.Message, ex.StackTrace);
			});
		disposables.Add(item5);
		IDisposable item6 = Observable.Create(delegate(IObserver<(bool active, bool connect)> observer)
		{
			IDisposable result = Observable.FromEvent<Action<bool, bool>, (bool, bool)>(delegate(Action<(bool active, bool connect)> h)
			{
				Action<(bool active, bool connect)> h2 = h;
				return delegate(bool a, bool c)
				{
					h2((a, c));
				};
			}, delegate(Action<bool, bool> h)
			{
				DisplayManager2 instance4 = DisplayManager2.Instance;
				instance4.VitureDisplayConnectChanged = (Action<bool, bool>)Delegate.Combine(instance4.VitureDisplayConnectChanged, h);
			}, delegate(Action<bool, bool> h)
			{
				DisplayManager2 instance3 = DisplayManager2.Instance;
				instance3.VitureDisplayConnectChanged = (Action<bool, bool>)Delegate.Remove(instance3.VitureDisplayConnectChanged, h);
			}).Subscribe(observer);
			observer.OnNext((DisplayManager2.Instance.VitureDisplayActive, DisplayManager2.Instance.VitureDisplayConnected));
			return result;
		}).Synchronize().DistinctUntilChanged()
			.ObserveOn(AvaloniaScheduler.Instance)
			.Subscribe(delegate((bool active, bool connect) x)
			{
				OnVitureDisplayConnectChanged(x.active, x.connect);
			}, delegate(Exception ex)
			{
				Logger.Error("[DisplayConnectObserver] " + ex.Message, ex.StackTrace);
			});
		disposables.Add(item6);
		GlassesDeviceManager.Instance.ReceivedGlassesData += OnReceivedGlassesData;
		DisplayManager2 instance2 = DisplayManager2.Instance;
		instance2.PhysicalMonitorChanged = (Action<int>)Delegate.Combine(instance2.PhysicalMonitorChanged, new Action<int>(OnMonitorChanged));
		IDisposable item7 = Observable.FromEvent(delegate(Action h)
		{
			DisplayManager2.Instance.DisplayChanged += h;
		}, delegate(Action h)
		{
			DisplayManager2.Instance.DisplayChanged -= h;
		}).Throttle(TimeSpan.FromMilliseconds(500.0)).ObserveOn(AvaloniaScheduler.Instance)
			.SelectConcat((Unit _) => OnDisplayTopologySettled(), AvaloniaScheduler.Instance)
			.Subscribe(delegate
			{
			}, delegate(Exception ex)
			{
				Logger.Error("[TopologyObserver] " + ex.Message, ex.StackTrace);
			});
		disposables.Add(item7);
		try
		{
			OnMonitorChanged(DisplayManager2.Instance.GetVitures().Count() + DisplayManager2.Instance.GetInternals().Count());
		}
		catch (Exception ex2)
		{
			Logger.Warn("[mvm] eager monitor count init failed: " + ex2.Message);
		}
		MouseShakeDetector.OnMouseShake += MouseShakeDetector_OnMouseShake;
		ProcessManager.Kill();
		ProcessManager.UnityStatusChanged = (Action<int>)Delegate.Combine(ProcessManager.UnityStatusChanged, new Action<int>(SetUnityViewRunningState));
		IDisposable item8 = hotKeyManager.HotKeyPressed.ObserveOn(AvaloniaScheduler.Instance).SelectConcat(ProcessHotkeyCallback, AvaloniaScheduler.Instance).Subscribe(delegate
		{
		}, delegate(Exception ex)
		{
			Logger.Error(ex.Message, ex.StackTrace);
		});
		disposables.Add(item8);
		RegisterHotKey();
		SetupConnectionStateHandler();
		isInit = true;
	}

	private void LoadSettings()
	{
		SettingsData settings = DbManager.Instance.Settings;
		FilmControlAngle = settings.FilmControlAngle;
		EnableMouseShake = settings.EnableMouseShake;
		EnableReduceMotionBlur = settings.EnableReduceMotionBlur;
		EnableHighDpiScale = settings.EnableHighDPIScale;
		TurnOffBuildInScreen = settings.TurnOffBuildInScreen;
		SmoothFollow = settings.SmoothFollow;
		SvsEnable = settings.SvsEnable;
		SvsDebug = false;
		HandTrack = settings.HandTrack;
		UseUltraWideSize = settings.UseUltraWideSize;
		FrameRate = settings.RefreshRate;
		Skybox = settings.Skybox;
		CustomSkyboxFile = FindExistingCustomSkybox();
		lockAxis = (LockAxisState)settings.LockAxis;
		LockXAxis = (lockAxis & LockAxisState.LockX) == LockAxisState.LockX;
		LockYAxis = (int)(lockAxis & LockAxisState.LockY) >> 1 == 1;
		LockZAxis = (int)(lockAxis & LockAxisState.LockZ) >> 2 == 1;
	}

	private void DoCleanup()
	{
		if (isInit)
		{
			GlassesDeviceManager instance = GlassesDeviceManager.Instance;
			instance.DeviceEnterBootMode = (Action<bool>)Delegate.Remove(instance.DeviceEnterBootMode, new Action<bool>(OnDeviceEnterBootMode));
			GlassesDeviceManager.Instance.ReceivedGlassesData -= OnReceivedGlassesData;
			DisplayManager2 instance2 = DisplayManager2.Instance;
			instance2.PhysicalMonitorChanged = (Action<int>)Delegate.Remove(instance2.PhysicalMonitorChanged, new Action<int>(OnMonitorChanged));
			ProcessManager.UnityStatusChanged = (Action<int>)Delegate.Remove(ProcessManager.UnityStatusChanged, new Action<int>(SetUnityViewRunningState));
			UnRegisterHotkey();
			isInit = false;
		}
		disposables.Dispose();
		hotKeyManager.Dispose();
		StickyVirtualScreenStop();
		WindowMover.Instance.Stop();
	}

	private async Task<Unit> OnDisplayTopologySettled()
	{
		if (IsBusy)
		{
			return Unit.Default;
		}
		if (!ProcessManager.Running && !ViewAppRunning)
		{
			return Unit.Default;
		}
		if (_topologyTeardownInFlight)
		{
			return Unit.Default;
		}
		string text = string.Empty;
		try
		{
			if (_fenceVddCount > 0)
			{
				int num = DisplayManager2.Instance.GetVirtuals(onlyActive: true).Count();
				if (num < _fenceVddCount)
				{
					text = $"VDD {num}/{_fenceVddCount} (Modern Standby watchdog?)";
				}
			}
			if (text.Length == 0 && _fenceInternalCount > 0 && !DisplayManager2.Instance.TurnOffBuildInScreen)
			{
				int num2 = DisplayManager2.Instance.GetInternals(onlyActive: true).Count();
				if (num2 < _fenceInternalCount)
				{
					text = $"internal {num2}/{_fenceInternalCount} (lid closed?)";
				}
			}
		}
		catch (Exception ex)
		{
			Logger.Warn("[Topology] count query failed: " + ex.Message);
			return Unit.Default;
		}
		if (text.Length == 0)
		{
			TryRefreshMouseFence();
			return Unit.Default;
		}
		_topologyTeardownInFlight = true;
		try
		{
			Logger.Info("[Topology] layout dependency lost: " + text + " — teardown + back to layout");
			try
			{
				MouseHook.UnSetHook();
			}
			catch (Exception ex2)
			{
				Logger.Warn("[Topology] UnSetHook: " + ex2.Message);
			}
			try
			{
				await Task.Run((Action)ProcessManager.Kill);
			}
			catch (Exception ex3)
			{
				Logger.Warn("[Topology] Kill: " + ex3.Message);
			}
			try
			{
				KeepAwake.Disable();
			}
			catch (Exception ex4)
			{
				Logger.Warn("[Topology] KeepAwake.Disable: " + ex4.Message);
			}
			_fenceVddCount = 0;
			_fenceInternalCount = 0;
			await NavigateToLayoutAsync();
		}
		finally
		{
			_topologyTeardownInFlight = false;
		}
		return Unit.Default;
	}

	private void TryRefreshMouseFence()
	{
		try
		{
			Rectangle? currentExclude = MouseHook.CurrentExclude;
			if (!currentExclude.HasValue)
			{
				return;
			}
			Rectangle valueOrDefault = currentExclude.GetValueOrDefault();
			currentExclude = DisplayManager2.Instance.GetVitureRect();
			if (currentExclude.HasValue)
			{
				Rectangle valueOrDefault2 = currentExclude.GetValueOrDefault();
				if (valueOrDefault2 != valueOrDefault)
				{
					Logger.Info($"[Topology] VITURE rect moved {valueOrDefault} → {valueOrDefault2}, refresh mouse fence");
					MouseHook.SetMouseHook(valueOrDefault2);
				}
			}
		}
		catch (Exception ex)
		{
			Logger.Warn("[Topology] refresh mouse fence failed: " + ex.Message);
		}
	}

	public bool IsNative3DofMode()
	{
		if (!GlassesDeviceManager.Instance.SupportNative3Dof)
		{
			return DisplayManager2.Instance.GetVitures().FirstOrDefault()?.GetDeviceName().ToUpper().Contains("BEAST") ?? false;
		}
		return true;
	}

	public async Task<string?> OpenFilePickerAsync()
	{
		if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime { MainWindow: not null } classicDesktopStyleApplicationLifetime)
		{
			IStorageProvider storageProvider = classicDesktopStyleApplicationLifetime.MainWindow.StorageProvider;
			FilePickerOpenOptions filePickerOpenOptions = new FilePickerOpenOptions();
			filePickerOpenOptions.Title = "Select Image File";
			filePickerOpenOptions.AllowMultiple = false;
			filePickerOpenOptions.FileTypeFilter = new FilePickerFileType[2]
			{
				new FilePickerFileType("Image file")
				{
					Patterns = new string[4] { "*.jpg", "*.jpeg", "*.png", "*.bmp" }
				},
				new FilePickerFileType("Any type file")
				{
					Patterns = new string[1] { "*.*" }
				}
			};
			FilePickerOpenOptions options = filePickerOpenOptions;
			IReadOnlyList<IStorageFile> readOnlyList = await storageProvider.OpenFilePickerAsync(options);
			if (readOnlyList.Count > 0 && readOnlyList.FirstOrDefault()?.TryGetLocalPath() != null)
			{
				return readOnlyList.FirstOrDefault()?.TryGetLocalPath();
			}
		}
		return null;
	}

	public static void UnRegisterHotkey()
	{
		foreach (IRegistration item in hotKeySubscription)
		{
			item?.Dispose();
		}
		hotKeySubscription.Clear();
	}

	public static void DisposeHotKeyManager()
	{
		UnRegisterHotkey();
		hotKeyManager.Dispose();
	}

	public static void RegisterHotKey()
	{
		foreach (KeyValuePair<string, string> globalHotkey in DbManager.Instance.Settings.GlobalHotkeys)
		{
			KeyGesture keyGesture = KeyGesture.Parse(globalHotkey.Value);
			Modifiers modifiers = (Modifiers)(keyGesture.KeyModifiers & (KeyModifiers.Alt | KeyModifiers.Control | KeyModifiers.Shift | KeyModifiers.Meta));
			VirtualKeyCode? virtualKeyCode = KeyCodeHelper.ConvertKeyToVirtualKeyCode(keyGesture.Key);
			if (virtualKeyCode.HasValue)
			{
				hotKeySubscription.Add(hotKeyManager.Register(virtualKeyCode.Value, modifiers));
			}
		}
	}

	private void MouseShakeDetector_OnMouseShake()
	{
		if (!EnableMouseShake)
		{
			return;
		}
		Logger.Info("OnMouseShake Trigger");
		TelemetryHelper.Capture("OnMouseShake Trigger");
		if (ProcessManager.Running && !GlassesDeviceManager.Instance.SupportNative3Dof)
		{
			ProcessManager.RecenterMouseAsync();
			return;
		}
		DisplayInfo primaryDisplay = DisplayManager2.Instance.PrimaryDisplay;
		if (primaryDisplay != null)
		{
			System.Drawing.Point position = primaryDisplay.CurrentSetting.Position;
			System.Drawing.Size resolution = primaryDisplay.CurrentSetting.Resolution;
			int x = position.X + resolution.Width / 2;
			int y = position.Y + resolution.Height / 2;
			MouseHook.SetCursorPos(x, y);
		}
	}

	private void OnMonitorChanged(int count)
	{
		if (count != 0 || !turnOffBuildInScreen)
		{
			MonitorCount = count;
			TelemetryHelper.Capture($"TurnOffBuildInScreen: {TurnOffBuildInScreen} MonitorCount: {count}");
		}
	}

	private async void OnVitureDisplayConnectChanged(bool active, bool connect)
	{
		VitureDisplayConnect = connect;
		if (!connect)
		{
			HideConnectionDialog();
			HideFirmwareUpdateDialog();
		}
	}

	private void OnDeviceEnterBootMode(bool boot)
	{
		VitureAppMode = !boot;
		GlassesDeviceManager.Instance.Initialize();
		VitureCommInited = true;
	}

	private async Task<Unit> OnDeviceConnectChanged(bool connect)
	{
		if (!connect)
		{
			VitureCommInited = false;
		}
		VitureAppMode = GlassesDeviceManager.Instance.AppMode;
		VitureCommConnect = connect;
		if (connect)
		{
			GlassesDeviceManager.Instance.Initialize();
			VitureDeviceType = GetVitureDeviceType(GlassesDeviceManager.Instance.ProductId);
			if (GlassesDeviceManager.Instance.UsbCommunicationFailed)
			{
				Logger.Warn("[HID] enumerated but no usable channel (open/claim failed), treat as USB comm failure");
				VitureCommConnect = false;
				VitureCommInited = false;
				return Unit.Default;
			}
			bool native3Dof = GlassesDeviceManager.Instance.SupportNative3Dof;
			ZoomLevel = 1.0;
			VitureLayoutMode = (native3Dof ? VitureLayoutMode.Horizontal3A : VitureLayoutMode.Horizontal3);
			LayoutType = LayoutType.Mirror;
			SupportHighRefreshRate = VitureDeviceType != VitureDeviceType.N6 && VitureDeviceType != VitureDeviceType.N6C && VitureDeviceType != VitureDeviceType.R6;
			if (!SupportHighRefreshRate)
			{
				FrameRate = 60;
			}
			VitureCommInited = true;
			Task.Run(delegate
			{
				try
				{
					int volume = GlassesDeviceManager.Instance.GetVolume();
					int brightness = GlassesDeviceManager.Instance.GetBrightness();
					int? distance = null;
					int? screenSize = null;
					if (native3Dof)
					{
						distance = GlassesDeviceManager.Instance.GetDistance();
						screenSize = GlassesDeviceManager.Instance.GetScreenSize();
					}
					Dispatcher.UIThread.Post(delegate
					{
						VolumeLevel = volume;
						BrightnessLevel = brightness;
						if (distance.HasValue)
						{
							ZoomLevel = distance.Value;
						}
						if (screenSize.HasValue)
						{
							ScreenSize = screenSize.Value;
						}
					});
				}
				catch (Exception ex)
				{
					Logger.Warn("[OnDeviceConnectChanged] device param read failed: " + ex.Message);
				}
			});
		}
		else
		{
			VitureCommInited = false;
			_otaFinished = false;
			HideConnectionDialog();
			HideFirmwareUpdateDialog();
			MouseHook.UnSetHook();
			await Task.Run((Action)ProcessManager.Kill);
			WindowMover.Instance.Stop();
			SudoVirtualDisplay.Instance.RemoveVirtualDisplay();
			KeepAwake.Disable();
			IsBusy = false;
			if (GlassesDeviceManager.Instance.UseHidDevice && GlassesDeviceManager.Instance.P6Series)
			{
				P6BiasReader.Clear();
				VitureSlam.SaveOnlineBaisData(GlassesDeviceManager.Instance.GlassesSN);
				VitureSlam.Stop();
			}
			if (GlassesDeviceManager.Instance.UseHidDevice && GlassesDeviceManager.Instance.SupportNative3Dof)
			{
				StickyVirtualScreenStop();
			}
			GlassesDeviceManager.Instance.Dispose();
		}
		return Unit.Default;
	}

	private void OnReceivedGlassesData(byte[] bytes)
	{
		if (GlassesDeviceManager.Instance.UseHidDevice)
		{
			if (GlassesDeviceManager.Instance.R6NewerModel)
			{
				ProcessR6NewerMessage(bytes);
			}
			else
			{
				ProcessHidMessage(bytes);
			}
		}
		else
		{
			ProcessUsbMessage(bytes);
		}
	}

	private Window? GetMainWindow()
	{
		if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime classicDesktopStyleApplicationLifetime)
		{
			return classicDesktopStyleApplicationLifetime.MainWindow;
		}
		return null;
	}

	private async Task PromptVddRestartAsync()
	{
		await Dispatcher.UIThread.InvokeAsync(async delegate
		{
			await new ContentDialog
			{
				Classes = { "message" },
				Content = Resources.VddDriverHungRestart,
				PrimaryButtonText = Resources.Confirm
			}.ShowAsync();
		});
	}

	private async Task<bool> ShowSpacewalkerUpdateDialogAsync(bool update = true)
	{
		string name = Assembly.GetExecutingAssembly().GetName().Name;
		if (update)
		{
			if (await new ContentDialog
			{
				Classes = { "message" },
				Tag = new Image
				{
					Width = 68.0,
					Height = 68.0,
					Source = new Bitmap(AssetLoader.Open(new Uri("avares://" + name + "/Assets/Images/app_icon.png")))
				},
				Title = Resources.Update,
				Content = Resources.UpdateDialogText,
				PrimaryButtonText = Resources.UpdateNow,
				SecondaryButtonText = Resources.NotNow
			}.ShowAsync() == ContentDialogResult.Primary)
			{
				await UpdateHelper.RunInstaller();
				return true;
			}
		}
		else if (await new ContentDialog
		{
			Classes = { "message" },
			Tag = new Image
			{
				Width = 68.0,
				Height = 68.0,
				Source = new Bitmap(AssetLoader.Open(new Uri("avares://" + name + "/Assets/Images/app_icon.png")))
			},
			Title = Resources.Update,
			Content = Resources.AlreadyLastestSW,
			PrimaryButtonText = Resources.Confirm
		}.ShowAsync() == ContentDialogResult.Primary)
		{
			return true;
		}
		return false;
	}

	private async Task<bool> ShowFirmwareUpdateDialogAsync(bool update = true, bool isBootMode = false)
	{
		string newValue = Assembly.GetEntryAssembly()?.GetName().Name;
		if (update)
		{
			ContentDialog dialog = (_firmwareDialog = new ContentDialog
			{
				Classes = { "message" },
				Title = Resources.FirmwareUpdate,
				Content = Resources.JumpToWebOta.Replace("{APP_NAME}", newValue),
				PrimaryButtonText = Resources.OK,
				SecondaryButtonText = (isBootMode ? null : Resources.NotNow)
			});
			try
			{
				if (await dialog.ShowAsync() == ContentDialogResult.Primary)
				{
					Process.Start(new ProcessStartInfo("https://www.viture.com/firmware/update")
					{
						UseShellExecute = true
					});
					GetMainWindow()?.Close();
					return true;
				}
			}
			finally
			{
				if (_firmwareDialog == dialog)
				{
					_firmwareDialog = null;
				}
			}
		}
		else
		{
			ContentDialog dialog = (_firmwareDialog = new ContentDialog
			{
				Classes = { "message" },
				Title = Resources.FirmwareUpdate,
				Content = Resources.AlreadyLastestFM,
				PrimaryButtonText = Resources.Back
			});
			try
			{
				if (await dialog.ShowAsync() == ContentDialogResult.Primary)
				{
					return true;
				}
			}
			finally
			{
				if (_firmwareDialog == dialog)
				{
					_firmwareDialog = null;
				}
			}
		}
		return false;
	}

	private void HideFirmwareUpdateDialog()
	{
		_firmwareDialog?.Hide();
		_firmwareDialog = null;
	}

	private void HideConnectionDialog()
	{
		_connectionDialog?.Hide();
		_connectionDialog = null;
	}

	private static Image CreateWarningIcon()
	{
		return new Image
		{
			Width = 48.0,
			Height = 48.0,
			Source = new SvgImage
			{
				Source = WarningIconSvg
			}
		};
	}

	private async Task ShowConnectionDialogAsync(string content)
	{
		_connectionDialog?.Hide();
		ContentDialog dialog = (_connectionDialog = new ContentDialog
		{
			Classes = { "message" },
			Tag = CreateWarningIcon(),
			Title = Resources.ConnectionFailed,
			Content = content,
			CloseButtonText = Resources.Close,
			PrimaryButtonText = Resources.ViewTutorial,
			PrimaryButtonCommand = ReactiveCommand.Create(delegate
			{
				Process.Start(new ProcessStartInfo(ConnectUrl)
				{
					UseShellExecute = true
				});
			})
		});
		try
		{
			await dialog.ShowAsync();
		}
		finally
		{
			if (_connectionDialog == dialog)
			{
				_connectionDialog = null;
			}
		}
	}

	internal async Task<bool> ShowOsdSwitchToUltraWideAsync()
	{
		return await new ContentDialog
		{
			Classes = { "message" },
			Tag = CreateWarningIcon(),
			Title = Resources.SwitchToUltraWide,
			Content = Resources.SwitchToUltraWideTips,
			PrimaryButtonText = Resources.IHaveSwitched,
			SecondaryButtonText = Resources.Cancel
		}.ShowAsync() == ContentDialogResult.Primary;
	}

	internal async Task<bool> ShowExitConfirmDialogAsync()
	{
		return await new ContentDialog
		{
			Classes = { "message" },
			Tag = CreateWarningIcon(),
			Title = Resources.ExitConfirmTitle,
			Content = Resources.ExitConfirmContent,
			PrimaryButtonText = Resources.ExitConfirmOk,
			SecondaryButtonText = Resources.Cancel
		}.ShowAsync() == ContentDialogResult.Primary;
	}

	internal async Task<bool> ShowResetAllSettingsConfirmDialogAsync()
	{
		return await new ContentDialog
		{
			Classes = { "message" },
			Title = Resources.RestoreDefaultSettings,
			Content = Resources.ResetAllSettingsConfirmContent,
			PrimaryButtonText = Resources.ResetDefaults,
			SecondaryButtonText = Resources.Cancel
		}.ShowAsync() == ContentDialogResult.Primary;
	}

	internal async Task ShowFeedbackAsync()
	{
		await new ContentDialog
		{
			Classes = { "dialog" },
			Content = new SettingsView
			{
				DataContext = new SettingsViewModel(this)
				{
					SelectedTabIndex = 3
				}
			}
		}.ShowWithScrimDismissAsync(GetMainWindow());
	}

	public async Task ShowUpdateGlassesDialogAsync()
	{
		await new ContentDialog
		{
			Classes = { "message" },
			Title = Resources.GetProXRGlasses + Environment.NewLine + Resources.HighRefresh,
			Content = Resources.UpdateProGlassesTips,
			PrimaryButtonText = Resources.NotNow,
			SecondaryButtonText = Resources.Upgrade,
			SecondaryButtonCommand = ReactiveCommand.Create(delegate
			{
				string text = "https://www.viture.com/beast";
				TelemetryHelper.Capture("Buy Button Click: " + text);
				Process.Start(new ProcessStartInfo(text)
				{
					UseShellExecute = true
				});
			})
		}.ShowAsync();
	}

	public static VitureDeviceType GetVitureDeviceType(int productId)
	{
		SortedDictionary<int, VitureDeviceType> sortedDictionary = new SortedDictionary<int, VitureDeviceType>
		{
			{
				4112,
				VitureDeviceType.N6
			},
			{
				4113,
				VitureDeviceType.N6
			},
			{
				4114,
				VitureDeviceType.N6
			},
			{
				4115,
				VitureDeviceType.N6
			},
			{
				4116,
				VitureDeviceType.N6C
			},
			{
				4117,
				VitureDeviceType.N6C
			},
			{
				4118,
				VitureDeviceType.N6
			},
			{
				4119,
				VitureDeviceType.N6
			},
			{
				4120,
				VitureDeviceType.N6P
			},
			{
				4121,
				VitureDeviceType.N6P
			},
			{
				4122,
				VitureDeviceType.N6C
			},
			{
				4123,
				VitureDeviceType.N6C
			},
			{
				4124,
				VitureDeviceType.N6P
			},
			{
				4125,
				VitureDeviceType.N6P
			},
			{
				4352,
				VitureDeviceType.P6S
			},
			{
				4353,
				VitureDeviceType.P6S
			},
			{
				4354,
				VitureDeviceType.P6S
			},
			{
				4355,
				VitureDeviceType.P6S
			},
			{
				4356,
				VitureDeviceType.P6S
			},
			{
				4384,
				VitureDeviceType.P6
			},
			{
				4385,
				VitureDeviceType.P6
			},
			{
				4400,
				VitureDeviceType.P6C
			},
			{
				4401,
				VitureDeviceType.P6C
			},
			{
				4416,
				VitureDeviceType.P6
			},
			{
				4417,
				VitureDeviceType.P6
			},
			{
				4432,
				VitureDeviceType.P6X
			},
			{
				4433,
				VitureDeviceType.P6X
			},
			{
				4608,
				VitureDeviceType.R6
			},
			{
				4609,
				VitureDeviceType.R6
			},
			{
				4624,
				VitureDeviceType.R6
			},
			{
				4625,
				VitureDeviceType.R6
			},
			{
				4864,
				VitureDeviceType.S6
			},
			{
				4865,
				VitureDeviceType.S6
			},
			{
				4880,
				VitureDeviceType.S6P
			},
			{
				4881,
				VitureDeviceType.S6P
			}
		};
		if (sortedDictionary.TryGetValue(productId, out var value))
		{
			return value;
		}
		int key = 0;
		int num = int.MaxValue;
		foreach (int key2 in sortedDictionary.Keys)
		{
			int num2 = Math.Abs(key2 - productId);
			if (num2 < num)
			{
				num = num2;
				key = key2;
			}
		}
		return sortedDictionary[key];
	}

	public string GetVitureDeviceName(VitureDeviceType deviceType)
	{
		switch (deviceType)
		{
		case VitureDeviceType.N6:
			return "VITURE One XR Glasses";
		case VitureDeviceType.N6C:
			return "VITURE One Lite XR Glasses";
		case VitureDeviceType.N6P:
			return "VITURE Pro XR Glasses";
		case VitureDeviceType.P6:
			return "VITURE Luma Pro XR Glasses";
		case VitureDeviceType.P6C:
			return "VITURE Luma XR Glasses";
		case VitureDeviceType.P6S:
			return "VITURE Luma Ultra XR Glasses";
		case VitureDeviceType.P6X:
			return "VITURE Luma Cyber XR Glasses";
		case VitureDeviceType.R6:
			return "VITURE Beast XR Glasses";
		case VitureDeviceType.S6:
		case VitureDeviceType.S6P:
			return "VITURE Pro 2 XR Glasses";
		default:
			return "Viture " + Enum.GetName(deviceType) + " XR Glasses";
		}
	}

	public static IReadOnlyDictionary<VitureLayoutMode, IReadOnlyList<LayoutType>> GetVitureLayoutModes(VitureDeviceType deviceType, int monitorCount = 1, bool turnOffBuildInScreen = false)
	{
		if (turnOffBuildInScreen || monitorCount == 0)
		{
			List<LayoutType> value = new List<LayoutType> { LayoutType.Extend };
			if (deviceType == VitureDeviceType.R6)
			{
				return new Dictionary<VitureLayoutMode, IReadOnlyList<LayoutType>>
				{
					[VitureLayoutMode.Horizontal1A] = value,
					[VitureLayoutMode.UltraWideA] = value,
					[VitureLayoutMode.Horizontal3A] = value,
					[VitureLayoutMode.Horizontal2A] = value
				};
			}
			return new Dictionary<VitureLayoutMode, IReadOnlyList<LayoutType>>
			{
				[VitureLayoutMode.Horizontal1] = value,
				[VitureLayoutMode.Horizontal2] = value,
				[VitureLayoutMode.Horizontal3] = value,
				[VitureLayoutMode.Vertical3] = value,
				[VitureLayoutMode.UltraWide] = value,
				[VitureLayoutMode.HorizontalPortrait] = value
			};
		}
		if (deviceType == VitureDeviceType.R6)
		{
			return new Dictionary<VitureLayoutMode, IReadOnlyList<LayoutType>>
			{
				[VitureLayoutMode.Horizontal1A] = new List<LayoutType>
				{
					LayoutType.Mirror,
					LayoutType.Extend
				},
				[VitureLayoutMode.UltraWideA] = new List<LayoutType> { LayoutType.Extend },
				[VitureLayoutMode.Horizontal3A] = new List<LayoutType>
				{
					LayoutType.Mirror,
					LayoutType.Extend
				},
				[VitureLayoutMode.Horizontal2A] = new List<LayoutType>
				{
					LayoutType.Mirror,
					LayoutType.Extend
				}
			};
		}
		return new Dictionary<VitureLayoutMode, IReadOnlyList<LayoutType>>
		{
			[VitureLayoutMode.Horizontal1] = new List<LayoutType>
			{
				LayoutType.Mirror,
				LayoutType.Extend
			},
			[VitureLayoutMode.Horizontal2] = new List<LayoutType>
			{
				LayoutType.Mirror,
				LayoutType.Extend
			},
			[VitureLayoutMode.Horizontal3] = new List<LayoutType>
			{
				LayoutType.Mirror,
				LayoutType.Extend
			},
			[VitureLayoutMode.Vertical3] = new List<LayoutType> { LayoutType.Mirror },
			[VitureLayoutMode.UltraWide] = new List<LayoutType> { LayoutType.Extend },
			[VitureLayoutMode.HorizontalPortrait] = new List<LayoutType>
			{
				LayoutType.Mirror,
				LayoutType.Extend
			}
		};
	}

	public static (LayoutMode, LayoutType) GetLegacyLayout(VitureLayoutMode vmode, LayoutType vtype)
	{
		LayoutMode item = LayoutMode.HorizonExtend1;
		if (vmode == VitureLayoutMode.Horizontal1)
		{
			item = ((vtype == LayoutType.Mirror) ? LayoutMode.HorizonMirror1 : LayoutMode.HorizonExtend1);
		}
		if (vmode == VitureLayoutMode.Horizontal2)
		{
			item = ((vtype == LayoutType.Mirror) ? LayoutMode.HorizonMirror2 : LayoutMode.HorizonExtend2);
		}
		if (vmode == VitureLayoutMode.Horizontal3)
		{
			item = ((vtype == LayoutType.Mirror) ? LayoutMode.HorizonMirror3 : LayoutMode.HorizonExtend3);
		}
		if (vmode == VitureLayoutMode.Vertical3)
		{
			item = LayoutMode.VerticalMirror3;
			vtype = LayoutType.Mirror;
		}
		if (vmode == VitureLayoutMode.UltraWide)
		{
			item = LayoutMode.UltraWide;
			vtype = LayoutType.Extend;
		}
		if (vmode == VitureLayoutMode.HorizontalPortrait)
		{
			item = ((vtype == LayoutType.Mirror) ? LayoutMode.HorizonPortraitMirror : LayoutMode.HorizonPortraitExtend);
		}
		if (vmode == VitureLayoutMode.Horizontal1A)
		{
			item = ((vtype == LayoutType.Mirror) ? LayoutMode.HorizonMirror1 : LayoutMode.HorizonExtend1);
		}
		if (vmode == VitureLayoutMode.UltraWideA)
		{
			item = LayoutMode.UltraWide;
			vtype = LayoutType.Extend;
		}
		if (vmode == VitureLayoutMode.Horizontal3A)
		{
			item = ((vtype == LayoutType.Mirror) ? LayoutMode.HorizonMirror3 : LayoutMode.HorizonExtend3);
		}
		if (vmode == VitureLayoutMode.Horizontal2A)
		{
			item = ((vtype == LayoutType.Mirror) ? LayoutMode.HorizonMirror2 : LayoutMode.HorizonExtend2);
		}
		return (item, vtype);
	}

	private void SetupConnectionStateHandler()
	{
		IObservable<Unit> first = (from x in this.WhenAnyValue((MainViewModel x) => x.VitureCommInited, (MainViewModel x) => x.VitureAppMode)
			where x.Item1 && !x.Item2 && GlassesDeviceManager.Instance.IsConnected
			select x).ObserveOn(AvaloniaScheduler.Instance).SelectConcat(async delegate
		{
			IsBusy = false;
			await ShowFirmwareUpdateDialogAsync(update: true, isBootMode: true);
			return Unit.Default;
		}, AvaloniaScheduler.Instance);
		IObservable<Unit> second = (from _ in this.WhenAnyValue((MainViewModel x) => x.VitureCommConnect, (MainViewModel x) => x.VitureDisplayConnect).Select<(bool, bool), IObservable<long>>(delegate((bool hid, bool dp) t)
			{
				if (_isOnLayoutOrDesktop || (!t.hid && !t.dp))
				{
					if (!t.hid && !t.dp)
					{
						_connectionFailureCount = 0;
					}
					return Observable.Empty<long>();
				}
				IsBusy = true;
				return Observable.Timer(TimeSpan.FromSeconds((t.hid && t.dp) ? 1 : 15));
			}).Switch()
			where !_isOnLayoutOrDesktop && (!GlassesDeviceManager.Instance.IsConnected || GlassesDeviceManager.Instance.AppMode)
			select _).ObserveOn(AvaloniaScheduler.Instance).SelectConcat(async delegate
		{
			bool flag = VitureCommConnect;
			bool flag2 = VitureDisplayConnect;
			if (flag2 && flag)
			{
				await (from t in this.WhenAnyValue((MainViewModel x) => x.VitureCommInited, (MainViewModel x) => x.VitureCommConnect)
					where t.Item1 || !t.Item2
					select t).Take(1);
				if (Disconnected())
				{
					return Unit.Default;
				}
				await CheckFirmwareUpdateCmd.Execute(true);
				if (Disconnected())
				{
					return Unit.Default;
				}
				_otaFinished = true;
				await DeviceFirstInitCmd.Execute();
				if (Disconnected())
				{
					return Unit.Default;
				}
				IsBusy = false;
				await NavigateToLayoutAsync();
				return Unit.Default;
			}
			if (flag2)
			{
				IsBusy = false;
				if (IsNative3DofMode())
				{
					await NavigateToLayoutAsync();
					return Unit.Default;
				}
				await ShowConnectionFailureAsync(Resources.CommunicationFailed);
				return Unit.Default;
			}
			IsBusy = false;
			if (DisplayManager2.Instance.VitureDisplayConnected)
			{
				Logger.Info("[connectionBranch] stale VitureDisplayConnect=false; truth source connected, resync");
				OnVitureDisplayConnectChanged(DisplayManager2.Instance.VitureDisplayActive, connect: true);
				return Unit.Default;
			}
			await ShowConnectionFailureAsync(Resources.NoDPConnect);
			return Unit.Default;
		}, AvaloniaScheduler.Instance);
		IObservable<Unit> second2 = (from dp in this.WhenAnyValue((MainViewModel x) => x.VitureDisplayConnect)
			where !dp && _isOnLayoutOrDesktop
			select dp).ObserveOn(AvaloniaScheduler.Instance).SelectConcat(async delegate
		{
			if (DisplayManager2.Instance.VitureDisplayConnected)
			{
				Logger.Info("[disconnectBranch] stale VitureDisplayConnect=false ignored; display already reconnected");
				return Unit.Default;
			}
			MouseHook.UnSetHook();
			if (ViewAppRunning)
			{
				ProcessManager.Kill();
			}
			KeepAwake.Disable();
			await NavigateToConnectAsync();
			return Unit.Default;
		}, AvaloniaScheduler.Instance);
		disposables.Add(first.Merge(second).Merge(second2).Subscribe(delegate
		{
		}, delegate(Exception ex)
		{
			Logger.Error(ex.Message, ex.StackTrace);
		}));
		disposables.Add(this.WhenAnyValue((MainViewModel x) => x.VitureCommConnect).Subscribe(delegate(bool hid)
		{
			GlassesDeviceManager.Instance.Muted = !hid;
		}));
		bool Disconnected()
		{
			if (VitureCommConnect && VitureDisplayConnect)
			{
				return false;
			}
			Logger.Info("[connectionBranch] device gone mid-flow, abort");
			IsBusy = false;
			return true;
		}
	}

	private async Task ShowConnectionFailureAsync(string reason)
	{
		_connectionFailureCount++;
		if (_connectionFailureCount < 3)
		{
			await ShowConnectionDialogAsync(reason);
		}
		else
		{
			if (_feedbackDialogShowing)
			{
				return;
			}
			_feedbackDialogShowing = true;
			try
			{
				await ShowFeedbackAsync();
			}
			catch (Exception ex)
			{
				Logger.Error(ex.Message, ex.StackTrace);
			}
			finally
			{
				_feedbackDialogShowing = false;
			}
		}
	}

	private async Task NavigateToLayoutAsync()
	{
		_isOnLayoutOrDesktop = true;
		_connectionFailureCount = 0;
		IsBusy = false;
		if (NavigationRouter != null)
		{
			await NavigationRouter.ReplaceAsync(new LayoutView
			{
				DataContext = new LayoutViewModel(this)
			}, null);
		}
	}

	private async Task NavigateToConnectAsync()
	{
		_isOnLayoutOrDesktop = false;
		_otaFinished = false;
		IsBusy = false;
		if (NavigationRouter != null)
		{
			await NavigationRouter.ReplaceAsync(new ConnectView
			{
				DataContext = new ConnectViewModel(this)
			}, null);
		}
	}

	private async Task<Unit> ProcessHotkeyCallback(HotKey hotKey)
	{
		if (hotKey == null || hotKey.Modifiers == (Modifiers)0)
		{
			return Unit.Default;
		}
		Logger.Info($"HotKey: Id={hotKey.Id}, Key={hotKey.Key}, Modifiers={hotKey.Modifiers}");
		KeyModifiers modifiers = (KeyModifiers)(hotKey.Modifiers & (Modifiers.Alt | Modifiers.Control | Modifiers.Shift | Modifiers.Win));
		Key? key = KeyCodeHelper.ConvertVirtualKeyCodeToKey(hotKey.Key);
		Dictionary<string, string> globalHotkeys = DbManager.Instance.Settings.GlobalHotkeys;
		if (!key.HasValue || globalHotkeys == null)
		{
			return Unit.Default;
		}
		KeyGesture gesture = new KeyGesture(key.Value, modifiers);
		string key2 = globalHotkeys.FirstOrDefault((KeyValuePair<string, string> pair) => pair.Value.Equals(gesture.ToString())).Key;
		if (GestureHeaderHelper.Equal(key2, Resources.QuitHeader))
		{
			ShutdownApp();
		}
		else if (GestureHeaderHelper.Equal(key2, Resources.LockXHeader))
		{
			await LockAxisCmd.Execute(LockAxisState.LockY);
		}
		else if (GestureHeaderHelper.Equal(key2, Resources.LockYHeader))
		{
			await LockAxisCmd.Execute(LockAxisState.LockX);
		}
		else if (GestureHeaderHelper.Equal(key2, Resources.LockZHeader))
		{
			await LockAxisCmd.Execute(LockAxisState.LockZ);
		}
		else if (GestureHeaderHelper.Equal(key2, Resources.ShowMainWindow))
		{
			await ShowWindowCmd.Execute();
		}
		else if (GestureHeaderHelper.Equal(key2, Resources.RecenterHeader))
		{
			await SendResetCmd(RecenterSource.Hotkey);
		}
		else if (GestureHeaderHelper.Equal(key2, Resources.ZoomInHeader))
		{
			await StepScreenDistance(ZoomDirection.ZoomIn);
		}
		else if (GestureHeaderHelper.Equal(key2, Resources.ZoomOutHeader))
		{
			await StepScreenDistance(ZoomDirection.ZoomOut);
		}
		else if (GestureHeaderHelper.Equal(key2, Resources.IncreaseBrightness))
		{
			await StepBrightness(1);
		}
		else if (GestureHeaderHelper.Equal(key2, Resources.DecreaseBrightness))
		{
			await StepBrightness(-1);
		}
		else if (GestureHeaderHelper.Equal(key2, Resources.IncreaseVolume))
		{
			await StepVolume(1);
		}
		else if (GestureHeaderHelper.Equal(key2, Resources.DecreaseVolume))
		{
			await StepVolume(-1);
		}
		else if (VitureDisplayConnect)
		{
			(VitureLayoutMode, LayoutType)? tuple = GestureHeaderHelper.ToLayout(IsNative3DofMode(), key2);
			if (!tuple.HasValue)
			{
				return Unit.Default;
			}
			if (IsBusy)
			{
				Logger.Info("HotKey: layout launch ignored while busy");
				return Unit.Default;
			}
			var (vitureLayoutMode, layoutType) = tuple.Value;
			TelemetryHelper.Capture("Launch SpaceWalker from ShortCutKey");
			VitureLayoutMode = vitureLayoutMode;
			LayoutType = layoutType;
			await LaunchCmd.Execute();
		}
		return Unit.Default;
	}

	public void SetFilmControlAngle(bool enabled, int angle)
	{
		FilmControlAngle = (enabled ? angle : (-1));
		DbManager.Instance.Settings.FilmControlAngle = FilmControlAngle;
		DbManager.Instance.SaveSettings();
		if (ViewAppRunning)
		{
			Channel channel = ProcessManager.Channel;
			if (channel != null)
			{
				TypedTopic<FilmAngleValue> film = Topics.Film;
				FilmAngleValue value = new FilmAngleValue(filmControlAngle);
				film.Publish(channel, in value);
				TelemetryHelper.Capture("send FilmControlAngle");
			}
		}
	}

	private void SetUnityViewRunningState(int status)
	{
		if (status == 2)
		{
			ViewAppRunning = true;
		}
		if (status == 3)
		{
			ViewAppRunning = false;
		}
	}

	private void InitVitureSlam()
	{
		Thread.Sleep(1000);
		string yamlConfig = P6BiasReader.GetYamlConfig(GlassesDeviceManager.Instance.GlassesSN);
		List<VitureCommonLibrary.Vector3D> biasData = YamlParseHelper.GetBiasData(yamlConfig);
		if (string.IsNullOrWhiteSpace(yamlConfig) || !yamlConfig.Trim().StartsWith("%YAML") || !yamlConfig.Trim().EndsWith("]") || biasData.Count != 3)
		{
			P6BiasReader.Clear();
			P6BiasReader.ClearFile(GlassesDeviceManager.Instance.GlassesSN);
			Thread.Sleep(1000);
			yamlConfig = P6BiasReader.GetYamlConfig(GlassesDeviceManager.Instance.GlassesSN);
			biasData = YamlParseHelper.GetBiasData(yamlConfig);
		}
		if (biasData.Count == 3)
		{
			Logger.Info("AccBias: " + biasData[0].ToString());
			Logger.Info("AccScale: " + biasData[1].ToString());
			VitureSlam.CarinaYamlContent = yamlConfig;
		}
		else
		{
			Logger.Warn($"parse biasData from yamlConfig is failed. biasData.Count = {biasData.Count}");
		}
		if (!string.IsNullOrWhiteSpace(yamlConfig) && biasData.Count == 3)
		{
			Thread.Sleep(200);
			GetBiasCmd();
			Thread.Sleep(200);
			OpenImuCmd();
			Thread.Sleep(200);
			SetImuFrameCmd();
			Logger.Info("carinaConfigYAML: " + yamlConfig);
			VitureSlam.LoadOnlineBaisData(GlassesDeviceManager.Instance.GlassesSN);
			VitureSlam.Start("Assets/Configs/slam_config_P6.yaml");
		}
		else
		{
			Logger.Warn("carinaConfigYAML is WhiteSpace: " + yamlConfig);
			P6BiasReader.Clear();
			P6BiasReader.ClearFile(GlassesDeviceManager.Instance.GlassesSN);
		}
	}

	private void OpenImuCmd()
	{
		if (GlassesDeviceManager.Instance.UseHidDevice)
		{
			HidMessage hidMessage = new HidMessage();
			hidMessage.Data.MsgID = 21;
			hidMessage.Data.PutValue((byte)((!GlassesDeviceManager.Instance.P6Series) ? 1 : 3));
			GlassesDeviceManager.Instance.SendMsg(hidMessage);
		}
	}

	public void SetImuFrameCmd(int rate = 500)
	{
		if (GlassesDeviceManager.Instance.UseHidDevice)
		{
			byte b = rate switch
			{
				90 => 1, 
				120 => 2, 
				240 => 3, 
				500 => 4, 
				_ => 0, 
			};
			Logger.Info($"SetImuFrameCmd: {rate} cmd: {b}");
			GlassesMsgSemaphore.SendMsgAndAwaitAck(DeviceMsgId.MSG_W_CONTROL_IMU_REPORT_FQ, b);
		}
	}

	public async Task<bool> SetNativeDisplayMode(bool ultraWide)
	{
		TelemetryHelper.Capture($"SetNativeDisplayMode begin {ultraWide}");
		Logger.Info($"[NDM] begin layoutMode={ultraWide} arm64Translated={PlatformHelper.IsArm64Translated}");
		DisplayConfig displayConfig = DisplayManager2.Instance.GetVitures().FirstOrDefault();
		if (displayConfig == null)
		{
			Logger.Warn("VITURE display not found");
			return false;
		}
		bool flag = (displayConfig.DeviceInfo.GetTargetPreferredMode()?.width ?? 0) >= 3840;
		if (ultraWide && !flag)
		{
			if (!VitureCommConnect)
			{
				await ShowOsdSwitchToUltraWideAsync();
			}
			else
			{
				await GlassesDeviceManager.Instance.SendNativeDisplayModeHidAsync(ultraWide: true);
			}
			if (!(await WaitNativeModeSwitchedAsync(ultraWide: true)))
			{
				Logger.Warn("[NDM] Ultra Wide mode switch timed out after 9s");
				return false;
			}
		}
		else if (!ultraWide && flag && PlatformHelper.IsArm64Translated)
		{
			if (VitureCommConnect)
			{
				await GlassesDeviceManager.Instance.SendNativeDisplayModeHidAsync(ultraWide: false);
				if (!(await WaitNativeModeSwitchedAsync(ultraWide: false)))
				{
					Logger.Warn("[NDM] Standard mode switch timed out after 9s (arm64)");
					return false;
				}
			}
			else
			{
				Logger.Warn("[NDM] arm64 translated but HID offline; cannot switch EDID back to standard");
			}
		}
		return true;
	}

	private async Task<bool> WaitNativeModeSwitchedAsync(bool ultraWide)
	{
		for (int i = 0; i < 30; i++)
		{
			try
			{
				DisplayConfig displayConfig = DisplayManager2.Instance.GetVitures().FirstOrDefault();
				string path = displayConfig?.GetDevicePath();
				uint valueOrDefault = (displayConfig?.DeviceInfo.GetTargetPreferredMode()?.width).GetValueOrDefault();
				bool flag = (ultraWide ? (valueOrDefault >= 3840) : (valueOrDefault != 0 && valueOrDefault < 3840));
				if (displayConfig != null && flag && !string.IsNullOrEmpty(path) && DisplayManager2.Instance.GetVitures(onlyActive: true).Any((DisplayConfig v) => v.GetDevicePath() == path))
				{
					return true;
				}
			}
			catch when (i + 1 < 30)
			{
			}
			await Task.Delay(300);
		}
		return false;
	}

	public void SetDutyCmd(int duty)
	{
		if (GlassesDeviceManager.Instance.SupportNative3Dof)
		{
			return;
		}
		if (ViewAppRunning)
		{
			Channel channel = ProcessManager.Channel;
			if (channel != null)
			{
				TypedTopic<DutyValue> typedTopic = Topics.Duty;
				DutyValue value = new DutyValue(duty);
				typedTopic.Publish(channel, in value);
				return;
			}
		}
		GlassesDeviceManager.Instance.SetDuty(duty);
	}

	private void GetBiasCmd()
	{
		if (!GlassesDeviceManager.Instance.SupportNative3Dof && GlassesDeviceManager.Instance.UseHidDevice && GlassesDeviceManager.Instance.P6Series)
		{
			GlassesMsgSemaphore.SendMsgAndAwaitAck(DeviceMsgId.MSG_ID_IMU_CALI_PARAM_R);
		}
	}

	private async Task ChangeScreenLayout2(VitureLayoutMode vmode, LayoutType vtype, int fps = 120)
	{
		turnOffBuildInScreen = DbManager.Instance.Settings.TurnOffBuildInScreen;
		(LayoutMode, LayoutType) legacyLayout = GetLegacyLayout(vmode, vtype);
		LayoutMode layoutMode = legacyLayout.Item1;
		vtype = ((!AtLeastOneMonitor || turnOffBuildInScreen) ? LayoutType.External : legacyLayout.Item2);
		TelemetryHelper.Capture($"ChangeScreenLayout {layoutMode} {vtype} {fps}");
		Logger.Info($"ChangeScreenLayout2: {layoutMode} {vtype} {fps} {EnableHighDpiScale} {TurnOffBuildInScreen}");
		if (DisplayManager2.Instance.LayoutMode == layoutMode && DisplayManager2.Instance.UseUltraWideSize == useUltraWideSize && DisplayManager2.Instance.EnableHighDpiScale == enableHighDpiScale && DisplayManager2.Instance.TurnOffBuildInScreen == turnOffBuildInScreen && ProcessManager.Running)
		{
			return;
		}
		ProcessManager.Kill();
		WindowMover.Instance.Stop();
		DisplayManager2.Instance.LayoutMode = layoutMode;
		DisplayManager2.Instance.UseUltraWideSize = useUltraWideSize;
		DisplayManager2.Instance.EnableHighDpiScale = enableHighDpiScale;
		DisplayManager2.Instance.TurnOffBuildInScreen = turnOffBuildInScreen;
		if (GlassesDeviceManager.Instance.UseHidDevice && GlassesDeviceManager.Instance.P6Series)
		{
			P6BiasReader.Clear();
			VitureSlam.SaveOnlineBaisData(GlassesDeviceManager.Instance.GlassesSN);
			VitureSlam.Stop();
		}
		if (!GlassesDeviceManager.Instance.SupportNative3Dof)
		{
			GlassesDeviceManager.Instance.Dispose();
		}
		try
		{
			if (IsNative3DofMode())
			{
				await SetNativeDisplayMode(vmode > VitureLayoutMode.Horizontal1A);
			}
			try
			{
				System.Drawing.Size wdSize = DisplayManager2.Instance.WdSize;
				System.Drawing.Size vdSize = DisplayManager2.Instance.VdSize;
				System.Drawing.Size native3DofVdSize = DisplayManager2.Instance.Native3DofVdSize;
				DisplayManager2.Instance.UltraWide = (Width: (uint)wdSize.Width, Height: (uint)wdSize.Height);
				DisplayManager2.Instance.Standard = (Width: (uint)vdSize.Width, Height: (uint)vdSize.Height);
				DisplayManager2.Instance.SidePanel = (Width: (uint)native3DofVdSize.Width, Height: (uint)native3DofVdSize.Height);
				DisplayManager2.Instance.EnableHighDpiScale = EnableHighDpiScale;
				TelemetryHelper.Capture($"DisplayAutoLayout mode: {layoutMode} {fps}");
				Rectangle value = await DisplayManager2.Instance.SetLayoutAsync(vmode, (VitureLayoutType)vtype, fps);
				_vddRestartPrompted = false;
				try
				{
					_fenceInternalCount = DisplayManager2.Instance.GetInternals(onlyActive: true).Count();
				}
				catch (Exception ex)
				{
					_fenceInternalCount = 0;
					Logger.Warn("[Topology] internal baseline snapshot failed: " + ex.Message);
				}
				if (vmode == VitureLayoutMode.Horizontal1A || vmode == VitureLayoutMode.UltraWideA)
				{
					MouseHook.SetMouseHook(null);
					WindowMover.Instance.Stop();
					ViewAppRunning = true;
					_fenceVddCount = 0;
				}
				else
				{
					MouseHook.SetMouseHook(value);
					try
					{
						_fenceVddCount = DisplayManager2.Instance.GetVirtuals(onlyActive: true).Count();
					}
					catch (Exception ex2)
					{
						_fenceVddCount = 0;
						Logger.Warn("[Topology] baseline snapshot failed: " + ex2.Message);
					}
					WindowMover.Instance.ShouldProcess = () => ProcessManager.Running;
					WindowMover.Instance.ViewWindowTitles = (IReadOnlyCollection<string>)(object)new string[2] { "SpaceWalker.Unity", "SpaceWalker.UnityLoading" };
					WindowMover.Instance.Start();
					string glassesModel = (GlassesDeviceManager.Instance.P6Series ? "P6Series" : (GlassesDeviceManager.Instance.SupportNative3Dof ? "Native3Dof" : (GlassesDeviceManager.Instance.S6NeedsHostSlam ? "S6Series" : "N6Series")));
					ProcessManager.Start(new ProcessParam
					{
						LayoutMode = layoutMode,
						FilmAngle = filmControlAngle,
						TurnOffScreen = turnOffBuildInScreen,
						GlassesModel = glassesModel,
						LockAxis = lockAxis,
						Duty = duty,
						Skybox = skybox,
						HandTrack = handTrack,
						SmoothFollow = smoothFollow
					});
				}
				HomeMainWindowToPrimary();
				KeepAwake.Enable();
			}
			catch (Exception ex3)
			{
				Logger.Error($"ChangeScreenLayout2 layout/start failed: {ex3}", ex3.StackTrace);
				ProcessManager.Kill();
				MouseHook.SetMouseHook(null);
				WindowMover.Instance.Stop();
				_fenceVddCount = 0;
				_fenceInternalCount = 0;
				if (vmode != VitureLayoutMode.Horizontal1A && vmode != VitureLayoutMode.UltraWideA && vtype != 0 && !_vddRestartPrompted)
				{
					int num = 0;
					try
					{
						num = DisplayManager2.Instance.GetVirtuals(onlyActive: true).Count((DisplayConfig x) => !string.IsNullOrEmpty(x.GetDevicePath()));
					}
					catch (Exception ex4)
					{
						Logger.Warn("[VDD] query ready count failed: " + ex4.Message);
					}
					if (num == 0)
					{
						_vddRestartPrompted = true;
						await PromptVddRestartAsync();
					}
				}
			}
			await DisplayManager2.Instance.ChangeNotifyAsync();
		}
		catch (Exception ex5)
		{
			Logger.Error($"ChangeScreenLayout2 outer failed: {ex5}", ex5.StackTrace);
		}
	}

	public static void RestoreIcons()
	{
		try
		{
			string text = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "VITURE", "SpaceWalker");
			if (!Directory.Exists(text))
			{
				Directory.CreateDirectory(text);
			}
			string text2 = Path.Combine(text, "icons.dok");
			if (File.Exists(text2))
			{
				Process.Start(new ProcessStartInfo
				{
					FileName = "Assets/Tools/DesktopOK_x64.exe",
					Arguments = "/load /silent " + text2 + " ",
					UseShellExecute = false,
					WindowStyle = ProcessWindowStyle.Hidden
				});
			}
		}
		catch (Exception ex)
		{
			Logger.Warn(ex.Message);
		}
	}

	private void ProcessUsbMessage(byte[] bytes)
	{
		UsbMessage usbMessage = UsbMessage.FromBytes(bytes);
		if (usbMessage.Data.Header == UsbMessageData.HEADER_TYPE.DOWN || usbMessage.Data.Header == UsbMessageData.HEADER_TYPE.UP)
		{
			if (FirmwareOtaManager.Instance != null)
			{
				FirmwareOtaManager.Instance.OnReceivedGlassesData(usbMessage);
			}
			if (usbMessage.Data.MsgID.Equal(DeviceMsgId.MSG_R_MCU_APP_FW_VERSION))
			{
				GlassesMsgSemaphore.ReleaseSemaphore(usbMessage.Data.MsgID);
				string version = usbMessage.GetVersion();
				Logger.Info("fwVersion: " + version);
			}
			if (usbMessage.Data.MsgID.Equal(DeviceMsgId.MSG_R_GLASSID))
			{
				GlassesSN = GlassesDeviceManager.Instance.GlassesSN;
				TelemetryHelper.SetGlassesSN(GlassesSN);
				TelemetryHelper.Capture("Glasses Connected: " + GlassesSN);
			}
			if (usbMessage.Data.MsgID.Equal(DeviceMsgId.MSG_W_DISPLAY_2D_3D))
			{
				DbManager.Instance.Settings.RefreshRate = GlassesDeviceManager.Instance.RefreshRate;
				DbManager.Instance.SaveSettings();
				GlassesMsgSemaphore.ReleaseSemaphore(usbMessage.Data.MsgID);
			}
			if (usbMessage.Data.MsgID.Equal(DeviceMsgId.MSG_W_DISPLAY_DUTY))
			{
				HidAckState ackState = usbMessage.Data.AckState;
				Logger.Info($"set Duty ret: {ackState}");
			}
			if (usbMessage.Data.MsgID.Equal(DeviceMsgId.MSG_R_DISPLAY_DUTY))
			{
				byte value = usbMessage.Data.Payload[1];
				Logger.Info($"duty: {value}");
			}
		}
	}

	private void ProcessHidMessage(byte[] bytes)
	{
		HidMessage hidMessage = HidMessage.FromBytes(bytes);
		if (hidMessage.Data.Header1 == byte.MaxValue)
		{
			if (FirmwareOtaManager.Instance != null)
			{
				FirmwareOtaManager.Instance.OnReceivedGlassesData(hidMessage);
			}
			if (hidMessage.Data.MsgID.Equal(DeviceMsgId.MSG_R_MCU_APP_FW_VERSION))
			{
				FirmwareVersion = GlassesDeviceManager.Instance.FirmwareVersion;
				TelemetryHelper.Capture("FirmwareVersion: " + FirmwareVersion);
			}
			if (hidMessage.Data.MsgID.Equal(DeviceMsgId.MSG_R_GLASSID))
			{
				GlassesSN = GlassesDeviceManager.Instance.GlassesSN;
				TelemetryHelper.SetGlassesSN(GlassesSN);
				TelemetryHelper.Capture("Glasses Connected: " + GlassesSN);
			}
			if (hidMessage.Data.MsgID.Equal(DeviceMsgId.MSG_W_DISPLAY_2D_3D))
			{
				DbManager.Instance.Settings.RefreshRate = GlassesDeviceManager.Instance.RefreshRate;
				DbManager.Instance.SaveSettings();
				GlassesMsgSemaphore.ReleaseSemaphore(hidMessage.Data.MsgID);
			}
			if (hidMessage.Data.MsgID.Equal(DeviceMsgId.MSG_W_CONTROL_IMU_REPORT_FQ))
			{
				GlassesMsgSemaphore.ReleaseSemaphore(hidMessage.Data.MsgID);
			}
			if (hidMessage.Data.MsgID.Equal(DeviceMsgId.MSG_W_DISPLAY_DUTY))
			{
				HidAckState ackState = hidMessage.Data.AckState;
				Logger.Info($"set Duty ret: {ackState}");
			}
			if (hidMessage.Data.MsgID.Equal(DeviceMsgId.MSG_R_DISPLAY_DUTY))
			{
				byte value = hidMessage.Data.Payload[1];
				Logger.Info($"duty: {value}");
			}
			if (hidMessage.Data.MsgID.Equal(DeviceMsgId.MSG_R_DOF6_PARA_INFO) || hidMessage.Data.MsgID.Equal(DeviceMsgId.MSG_R_READ_DOF6_PARA))
			{
				P6BiasReader.ProcessMsg(hidMessage);
			}
			if (hidMessage.Data.MsgID.Equal(DeviceMsgId.MSG_ID_IMU_CALI_PARAM_R))
			{
				VitureSlam.GyroBias = hidMessage.GyroOffset.ToBytes();
				GlassesMsgSemaphore.ReleaseSemaphore(hidMessage.Data.MsgID);
			}
			if (hidMessage.Data.MsgID.Equal(DeviceEventId.RawIMUEventReport) || hidMessage.Data.MsgID.Equal(DeviceEventId.VsyncHIDEventReport))
			{
				VitureSlam.Track(hidMessage, hidMessage.Data.MsgID.Equal(DeviceEventId.RawIMUEventReport));
			}
		}
	}

	private void ProcessR6NewerMessage(byte[] bytes)
	{
		R6NewerHidMessage r6NewerHidMessage = R6NewerHidMessage.FromBytes(bytes);
		if (r6NewerHidMessage.CRC != r6NewerHidMessage.GetCrc())
		{
			return;
		}
		if (FirmwareOtaManager.Instance != null)
		{
			FirmwareOtaManager.Instance.OnReceivedGlassesData(r6NewerHidMessage);
		}
		if (r6NewerHidMessage.MsgID.Equal(R6NewerMsgId.TF_REQ_ID_APP_FW_VERSION_R))
		{
			FirmwareVersion = GlassesDeviceManager.Instance.FirmwareVersion;
			TelemetryHelper.Capture("FirmwareVersion: " + FirmwareVersion);
		}
		if (r6NewerHidMessage.MsgID.Equal(R6NewerMsgId.TF_REQ_ID_BOARD_SN_R))
		{
			GlassesSN = GlassesDeviceManager.Instance.GlassesSN;
			TelemetryHelper.SetGlassesSN(GlassesSN);
			TelemetryHelper.Capture("Glasses Connected: " + GlassesSN);
		}
		if (r6NewerHidMessage.MsgID.Equal(R6NewerMsgId.TF_CMD_DISPLAY_MODE_W))
		{
			GlassesMsgSemaphore.ReleaseSemaphore(r6NewerHidMessage.MsgID);
		}
		if (r6NewerHidMessage.MsgID.Equal(R6NewerMsgId.TF_CMD_NATIVE_DISPLAY_MODE_W))
		{
			GlassesMsgSemaphore.ReleaseSemaphore(r6NewerHidMessage.MsgID);
		}
		if (r6NewerHidMessage.MsgID.Equal(R6NewerMsgId.TF_CMD_DISPLAY_RECENTER_W))
		{
			GlassesMsgSemaphore.ReleaseSemaphore(r6NewerHidMessage.MsgID);
			if (GlassesDeviceManager.Instance.SupportNative3Dof && SvsEnable)
			{
				Task.Run((Action)StickyVirtualScreenStart);
			}
		}
		if (r6NewerHidMessage.MsgID.Equal(R6NewerMsgId.TF_REQ_DISPLAY_CAMERA_CALIBRATION_R) && r6NewerHidMessage.GetAckSuceess() && r6NewerHidMessage.DataLen >= 5)
		{
			R6LongResponse r6LongResponse = new R6LongResponse(r6NewerHidMessage.Payload.AsSpan());
			totalCamParamSegNumm = r6LongResponse.TOTAL_SEG_NUM;
			Logger.Info($"r6LongRsp: {r6LongResponse.APP_SEQ} {r6LongResponse.CURRENT_SEG_NUM} {r6LongResponse.TOTAL_SEG_NUM}");
			if (r6LongResponse.CURRENT_SEG_NUM == 0)
			{
				camParamBytes.Clear();
			}
			camParamBytes.AddRange(r6LongResponse.PayloadTake(r6NewerHidMessage.DataLen - 5));
			GlassesMsgSemaphore.ReleaseSemaphore(r6NewerHidMessage.MsgID);
		}
	}

	private void StickyVirtualScreenStart()
	{
		Thread.Sleep(200);
		lock (stickyVirtualScreenLock)
		{
			if (!CamPoseEstimator.Instance.HasInit)
			{
				while (curCamParamSegNum < totalCamParamSegNumm)
				{
					GlassesMsgSemaphore.SendMsgAndAwaitAck(R6NewerMsgId.TF_REQ_DISPLAY_CAMERA_CALIBRATION_R, BitConverter.GetBytes(curCamParamSegNum));
					curCamParamSegNum++;
				}
				if (camParamBytes.Count == 136)
				{
					CamPoseEstimator.Instance.Init(new R6CameraParam(camParamBytes.ToArray().AsSpan()));
				}
			}
			if (!CamPoseEstimator.Instance.HasStart)
			{
				CamPoseEstimator.Instance.PoseUpdated += OnR6SvsPoseUpdate;
				CamPoseEstimator.Instance.Start();
			}
			if (CamPoseEstimator.Instance.HasStart)
			{
				CamPoseEstimator.Instance.ResetAnchor();
				CamPoseEstimator.Instance.SetDebugMode(SvsDebug);
			}
		}
	}

	private void StickyVirtualScreenStop()
	{
		lock (stickyVirtualScreenLock)
		{
			CamPoseEstimator.Instance.PoseUpdated -= OnR6SvsPoseUpdate;
			CamPoseEstimator.Instance.Stop();
		}
	}

	private void OnR6SvsPoseUpdate(float[] data)
	{
		Logger.Info("R6 SVS Pose Updated: " + string.Join(",", data));
		if (GlassesDeviceManager.Instance.SupportNative3Dof && GlassesDeviceManager.Instance.IsConnected)
		{
			R6NewerHidMessage r6NewerHidMessage = new R6NewerHidMessage
			{
				MsgID = 4358,
				DataLen = (ushort)(4 * data.Length)
			};
			r6NewerHidMessage.Payload = new byte[data.Length * 4];
			MemoryMarshal.AsBytes(data.AsSpan()).CopyTo(r6NewerHidMessage.Payload);
			GlassesDeviceManager.Instance.SendMsg(r6NewerHidMessage);
		}
	}
}
