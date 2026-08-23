using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Threading;
using NAudio.CoreAudioApi;
using NAudio.Wave;
using SpaceWalker.Assets.Languages;
using SpaceWalker.Helper;
using SpaceWalker.Ipc;
using SpaceWalker.ViewModels;
using Viture.Ipc.Transport;
using VitureCommonLibrary;

namespace SpaceWalker;

public static class ProcessManager
{
	private enum RecenterKind
	{
		Recenter,
		Calibrate,
		Mouse
	}

	private static ProcessParam? _viewAppParam;

	private static Process? viewAppProcess;

	private static bool _running = false;

	private static Channel? _channel = null;

	private static readonly object _ipcLock = new object();

	private static RecenterClient? _recenterClient = null;

	private static readonly List<Subscription> _typedSubs = new List<Subscription>();

	private static CancellationToken? _cancellationToken;

	private static CancellationTokenSource? _cancellationTokenSource;

	private static Task? _monitorTask;

	private static int _restartCount = 0;

	private const int MaxStartCount = 5;

	private static DateTime beginUseTime = DateTime.MinValue;

	public static Action<bool>? RunningStateChanged;

	public static Action<int>? UnityStatusChanged;

	private static readonly string _ipcNodeId = $"spacewalker.launcher.{Process.GetCurrentProcess().Id}";

	private static string ExeName => "SpaceWalker.Unity";

	public static Channel? Channel
	{
		get
		{
			if (GlassesDeviceManager.Instance.SupportNative3Dof)
			{
				return null;
			}
			lock (_ipcLock)
			{
				return _channel;
			}
		}
	}

	public static bool Running => getRunningState();

	private static bool getRunningState()
	{
		return Process.GetProcessesByName(ExeName).Length != 0;
	}

	private static bool StartIpc()
	{
		if (GlassesDeviceManager.Instance.SupportNative3Dof)
		{
			return true;
		}
		lock (_ipcLock)
		{
			DeInitIpc();
			Channel channel2 = null;
			try
			{
				channel2 = new Channel(_ipcNodeId);
				channel2.FrameError += delegate(string channel, string reason)
				{
					Logger.Warning("IPC framer: channel=" + channel + " reason=" + reason);
				};
				_typedSubs.Add(Topics.StaticCalib.Subscribe(channel2, OnStaticCalib));
				_typedSubs.Add(Topics.BrightnessEvent.Subscribe(channel2, delegate(BrightnessValue v)
				{
					OnVmUpdate(delegate(MainViewModel vm)
					{
						vm.BrightnessLevel = v.Value;
					});
				}));
				_typedSubs.Add(Topics.VolumeEvent.Subscribe(channel2, delegate(VolumeValue v)
				{
					OnVmUpdate(delegate(MainViewModel vm)
					{
						vm.VolumeLevel = v.Value;
					});
				}));
				_typedSubs.Add(Topics.ZoomEvent.Subscribe(channel2, delegate(ZoomValue v)
				{
					OnVmUpdate(delegate(MainViewModel vm)
					{
						vm.ZoomLevel = v.Zoom;
					});
				}));
				_recenterClient = new RecenterClient(channel2);
				_channel = channel2;
				return true;
			}
			catch (Exception ex)
			{
				Logger.Error("StartIpc failed, IPC disabled this round: " + ex.Message, ex.StackTrace);
				DeInitIpc();
				if (channel2 != null && channel2 != _channel)
				{
					try
					{
						channel2.Dispose();
					}
					catch (Exception ex2)
					{
						Logger.Warning("StartIpc rollback dispose failed: " + ex2.Message);
					}
				}
				_recenterClient = null;
				_channel = null;
				return false;
			}
		}
	}

	private static void DeInitIpc()
	{
		if (GlassesDeviceManager.Instance.SupportNative3Dof)
		{
			return;
		}
		lock (_ipcLock)
		{
			foreach (Subscription typedSub in _typedSubs)
			{
				try
				{
					typedSub.Dispose();
				}
				catch (Exception ex)
				{
					Logger.Warning("DeInitIpc sub dispose failed: " + ex.Message);
				}
			}
			_typedSubs.Clear();
			if (_recenterClient != null)
			{
				try
				{
					_recenterClient.Dispose();
				}
				catch (Exception ex2)
				{
					Logger.Warning("DeInitIpc recenterClient dispose failed: " + ex2.Message);
				}
				_recenterClient = null;
			}
			if (_channel != null)
			{
				try
				{
					_channel.Dispose();
				}
				catch (Exception ex3)
				{
					Logger.Warning("DeInitIpc channel dispose failed: " + ex3.Message);
				}
				_channel = null;
			}
		}
	}

	private static void OnVmUpdate(Action<MainViewModel> apply)
	{
		Action<MainViewModel> apply2 = apply;
		Dispatcher.UIThread.InvokeAsync(delegate
		{
			if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime classicDesktopStyleApplicationLifetime && classicDesktopStyleApplicationLifetime.MainWindow?.DataContext is MainViewModel obj)
			{
				apply2(obj);
			}
		});
	}

	private static void OnStaticCalib()
	{
		Toast.ShowNotification(Resources.StaticCalibrationToast);
	}

	public static void Start(ProcessParam param)
	{
		UnityStatusChanged?.Invoke(1);
		_viewAppParam = param;
		if (!_running)
		{
			_running = true;
			RunningStateChanged?.Invoke(_running);
		}
		string text = Path.Combine(Environment.CurrentDirectory, ExeName + ".exe");
		if (!File.Exists(text))
		{
			Logger.Error("Unity exe file not exists:", text);
			return;
		}
		if (GlassesDeviceManager.Instance.UseHidDevice && !GlassesDeviceManager.Instance.R6NewerModel)
		{
			playAudio();
		}
		viewAppProcess?.Dispose();
		viewAppProcess = new Process();
		viewAppProcess.StartInfo.UseShellExecute = false;
		viewAppProcess.StartInfo.RedirectStandardOutput = false;
		viewAppProcess.StartInfo.WorkingDirectory = Environment.CurrentDirectory;
		viewAppProcess.StartInfo.FileName = ExeName + ".exe";
		viewAppProcess.StartInfo.Arguments = _viewAppParam.ToString();
		viewAppProcess.Start();
		JobObject.AddProcess(viewAppProcess.Handle);
		Logger.Info("Start Process " + _viewAppParam.ToString());
		TelemetryHelper.Capture("Start Process {_viewAppParam.ToString()}");
		_restartCount = 0;
		_cancellationTokenSource = new CancellationTokenSource();
		_cancellationToken = _cancellationTokenSource.Token;
		_monitorTask = new Task(delegate
		{
			while ((!_cancellationToken.HasValue || !_cancellationToken.GetValueOrDefault().IsCancellationRequested) && GlassesDeviceManager.Instance.IsConnected)
			{
				_cancellationToken?.WaitHandle.WaitOne(3000);
				try
				{
					if (Process.GetProcessesByName(ExeName).Length != 0)
					{
						_restartCount = 0;
					}
					else
					{
						if (_restartCount >= 5)
						{
							MouseHook.UnSetHook();
							break;
						}
						_restartCount++;
						StartIpc();
						viewAppProcess?.Start();
						if (viewAppProcess != null)
						{
							JobObject.AddProcess(viewAppProcess.Handle);
						}
						Logger.Info($"Process Restart {_restartCount}");
						TelemetryHelper.Capture($"Process Restart {_restartCount}");
					}
				}
				catch (Exception ex)
				{
					Logger.Error("Process monitor restart failed: " + ex.Message, ex.StackTrace);
				}
			}
		}, _cancellationToken.Value);
		_monitorTask.Start();
		beginUseTime = DateTime.Now;
		StartIpc();
		UnityStatusChanged?.Invoke(2);
	}

	private static void playAudio()
	{
		List<string> list;
		try
		{
			using MMDeviceEnumerator mMDeviceEnumerator = new MMDeviceEnumerator();
			list = (from d in mMDeviceEnumerator.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active)
				select d.ID).ToList();
		}
		catch (Exception ex)
		{
			Logger.Warning("Failed to enumerate audio endpoints: " + ex.Message);
			return;
		}
		foreach (string deviceId in list)
		{
			Task.Run(delegate
			{
				PlayAudioOnDevice(deviceId);
			});
		}
	}

	private static void PlayAudioOnDevice(string deviceId)
	{
		MMDevice mMDevice = null;
		try
		{
			using MMDeviceEnumerator mMDeviceEnumerator = new MMDeviceEnumerator();
			mMDevice = mMDeviceEnumerator.GetDevice(deviceId);
			if (mMDevice == null || mMDevice.State != DeviceState.Active)
			{
				return;
			}
			bool flag = false;
			try
			{
				flag = mMDevice.AudioEndpointVolume?.Mute ?? false;
			}
			catch (Exception ex)
			{
				Logger.Warning("Cannot read endpoint volume on " + mMDevice.FriendlyName + ": " + ex.Message);
			}
			Logger.Info("Playing on device: " + mMDevice.FriendlyName);
			using AudioFileReader waveProvider = new AudioFileReader(Path.Combine(Directory.GetCurrentDirectory(), "Assets/Audio/audio.wav"));
			using WasapiOut wasapiOut = new WasapiOut(mMDevice, AudioClientShareMode.Shared, useEventSync: true, 0);
			wasapiOut.Init(waveProvider);
			wasapiOut.Play();
			while (wasapiOut.PlaybackState == PlaybackState.Playing)
			{
				Thread.Sleep(100);
			}
			if (!flag)
			{
				return;
			}
			try
			{
				if (mMDevice.AudioEndpointVolume != null)
				{
					mMDevice.AudioEndpointVolume.Mute = true;
				}
			}
			catch (Exception ex2)
			{
				Logger.Warning("Cannot restore mute on " + mMDevice.FriendlyName + ": " + ex2.Message);
			}
		}
		catch (Exception ex3)
		{
			Logger.Warning("Error playing audio on device " + deviceId + ": " + ex3.Message);
		}
		finally
		{
			mMDevice?.Dispose();
		}
	}

	public static void RestartCurrentProcess()
	{
		string currentDirectory = Directory.GetCurrentDirectory();
		Process process = new Process();
		process.StartInfo.WorkingDirectory = currentDirectory;
		process.StartInfo.FileName = Path.Combine(currentDirectory, "SpaceWalker.exe");
		process.Start();
	}

	public static Task RecenterAsync(RecenterSource source)
	{
		return CallRecenterAsync((RecenterClient c) => c.RecenterAsync(source), RecenterKind.Recenter, source);
	}

	public static Task RecenterMouseAsync()
	{
		return CallRecenterAsync((RecenterClient c) => c.RecenterMouseAsync(), RecenterKind.Mouse, RecenterSource.Hotkey);
	}

	private static async Task CallRecenterAsync(Func<RecenterClient, Task<RecenterResult>> call, RecenterKind kind, RecenterSource source)
	{
		RecenterClient recenterClient;
		lock (_ipcLock)
		{
			recenterClient = _recenterClient;
		}
		if (recenterClient == null)
		{
			return;
		}
		try
		{
			ApplyRecenterResult(await call(recenterClient).ConfigureAwait(continueOnCapturedContext: false), kind, source);
		}
		catch (Exception ex)
		{
			Logger.Warning($"Recenter RPC ({kind}) failed: {ex.Message}");
		}
	}

	private static void ApplyRecenterResult(RecenterResult result, RecenterKind kind, RecenterSource source)
	{
		string displayName = result.DisplayName;
		if (string.IsNullOrWhiteSpace(displayName))
		{
			return;
		}
		Logger.Info("Recenter result displayName: " + displayName);
		DisplayInfo displayInfo = DisplayManager2.Instance.CurrentViewDisplays.FirstOrDefault((DisplayInfo x) => x.DisplayName == displayName);
		if (displayInfo == null)
		{
			return;
		}
		System.Drawing.Point position = displayInfo.CurrentSetting.Position;
		System.Drawing.Size resolution = displayInfo.CurrentSetting.Resolution;
		Rect screenBound = new Rect(new Avalonia.Point(position.X, position.Y), new Avalonia.Size(resolution.Width, resolution.Height));
		if (kind == RecenterKind.Mouse || source == RecenterSource.Hotkey)
		{
			int num = (int)((float)position.X + result.CursorX * (float)resolution.Width);
			int num2 = (int)((float)position.Y + result.CursorY * (float)resolution.Height);
			Logger.Info($"SetCursorPos: {num} {num2}");
			MouseHook.SetCursorPos(num, num2);
		}
		if (!GlassesDeviceManager.Instance.P6Series && kind == RecenterKind.Recenter)
		{
			Dispatcher.UIThread.InvokeAsync(delegate
			{
				Toast.ShowNotification(Resources.RecenterToast, 5000, screenBound);
			});
		}
	}

	public static void Kill()
	{
		try
		{
			UnityStatusChanged?.Invoke(3);
			try
			{
				if (beginUseTime > DateTime.MinValue)
				{
					int value = (int)(DateTime.Now - beginUseTime).TotalMinutes;
					beginUseTime = DateTime.MinValue;
					Logger.Info($"Running Duration: {value}");
					TelemetryHelper.Capture($"Running Duration: {value}");
				}
				_cancellationTokenSource?.Cancel();
				try
				{
					_monitorTask?.Wait(3000);
				}
				catch (Exception)
				{
				}
				_monitorTask = null;
				DeInitIpc();
				Process[] processesByName = Process.GetProcessesByName(ExeName);
				foreach (Process process in processesByName)
				{
					try
					{
						process.Kill();
					}
					catch (Exception)
					{
					}
				}
				if (_running)
				{
					_running = false;
					RunningStateChanged?.Invoke(_running);
				}
			}
			catch (Exception ex3)
			{
				Logger.Error(ex3.Message, ex3.StackTrace);
			}
			UnityStatusChanged?.Invoke(4);
		}
		catch
		{
		}
	}
}
