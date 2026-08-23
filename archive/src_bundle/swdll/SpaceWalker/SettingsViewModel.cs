using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Disposables.Fluent;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia.Input;
using Avalonia.Media.Imaging;
using ReactiveUI;
using ReactiveUI.Avalonia;
using SpaceWalker.Assets.Languages;
using SpaceWalker.Database;
using SpaceWalker.Helper;
using SpaceWalker.ViewModels;

namespace SpaceWalker;

public class SettingsViewModel : ReactiveObject, IActivatableViewModel
{
	private sealed class TransientMessage
	{
		private static readonly TimeSpan AutoClearDelay = TimeSpan.FromSeconds(2.5);

		private readonly Action<string> _set;

		private CancellationTokenSource? _cts;

		public TransientMessage(Action<string> set)
		{
			_set = set;
		}

		public void Show(string message, bool persistent = false)
		{
			Clear();
			_set(message);
			if (!persistent)
			{
				_cts = new CancellationTokenSource();
				AutoClearAsync(_cts.Token);
			}
		}

		public void Clear()
		{
			_cts?.Cancel();
			_cts?.Dispose();
			_cts = null;
			_set(string.Empty);
		}

		private async Task AutoClearAsync(CancellationToken token)
		{
			try
			{
				await Task.Delay(AutoClearDelay, token);
				if (!token.IsCancellationRequested)
				{
					_set(string.Empty);
				}
			}
			catch (TaskCanceledException)
			{
			}
		}
	}

	private const int ShortcutsTabIndex = 2;

	private static readonly string[] PresetSkyboxes = new string[4] { "Everest.jpg", "BlueWater.jpg", "NorthernLights.jpg", "EltonLake.jpg" };

	private readonly TransientMessage _submitHint;

	private readonly TransientMessage _shortcutsHint;

	private bool _hotkeyUnregistered;

	private int _lastPositiveFilmAngle = 30;

	private ObservableAsPropertyHelper<bool>? _isFilmAngleVisible;

	private ObservableAsPropertyHelper<bool>? _isFilmSeparatorVisible;

	private ObservableAsPropertyHelper<bool>? _isHandGestureHelpVisible;

	private int _selectedTabIndex;

	private bool _isFilmControlEnabled;

	private int _filmAngle = 30;

	private bool _isReduceMotionBlur;

	private bool _isMouseShake;

	private bool _isHandTrack;

	private bool _isSvsEnabled;

	private bool _isSvsDebug;

	private bool _isBuildInScreenOn;

	private bool _isSmoothFollow;

	private bool _isHighDpiScale;

	private static readonly string[] RatioOptions = new string[2] { "24:9", "32:9" };

	private bool _isUltraWideMode;

	private bool _isFilmControlVisible = true;

	private bool _isReduceMotionBlurVisible = true;

	private bool _isMouseShakeVisible = true;

	private bool _isSvsEnabledVisible = true;

	private bool _isSvsDebugVisible;

	private bool _isTurnOffBuildInScreenVisible = true;

	private bool _isSmoothFollowVisible;

	private bool _isHighDpiScaleVisible = true;

	private bool _isUltraWideModeVisible = true;

	private bool _isHandTrackVisible = true;

	private string _skybox = string.Empty;

	private Bitmap? _customSkyboxBitmap;

	private bool _hasCustomSkyboxBitmap;

	private bool canChangeSkybox;

	private CustomHotkeyViewModel _hotkeyVM = new CustomHotkeyViewModel();

	private bool _showHandGestureHelp;

	private bool _hotkeyConflict;

	private string _email = string.Empty;

	private string _description = string.Empty;

	private bool _isSubmitting;

	private string _submitMessage = string.Empty;

	private string _shortcutsMessage = string.Empty;

	public ViewModelActivator Activator { get; } = new ViewModelActivator();


	public MainViewModel ViewModel { get; }

	public int SelectedTabIndex
	{
		get
		{
			return _selectedTabIndex;
		}
		set
		{
			bool flag = value == 2;
			this.RaiseAndSetIfChanged(ref _selectedTabIndex, value, "SelectedTabIndex");
			if (flag && !_hotkeyUnregistered)
			{
				_hotkeyUnregistered = true;
				MainViewModel.UnRegisterHotkey();
			}
			else if (!flag && _hotkeyUnregistered)
			{
				_hotkeyUnregistered = false;
				MainViewModel.RegisterHotKey();
				HotkeyVM = new CustomHotkeyViewModel();
				HotkeyConflict = false;
				_shortcutsHint.Clear();
			}
		}
	}

	public bool IsFilmControlEnabled
	{
		get
		{
			return _isFilmControlEnabled;
		}
		set
		{
			if (_isFilmControlEnabled != value)
			{
				_isFilmControlEnabled = value;
				this.RaisePropertyChanged("IsFilmControlEnabled");
				ViewModel.SetFilmControlAngle(value, _lastPositiveFilmAngle);
			}
		}
	}

	public int FilmAngle
	{
		get
		{
			return _filmAngle;
		}
		set
		{
			if (_filmAngle != value)
			{
				_filmAngle = value;
				this.RaisePropertyChanged("FilmAngle");
				if (_isFilmControlEnabled)
				{
					_lastPositiveFilmAngle = value;
					ViewModel.SetFilmControlAngle(enabled: true, value);
				}
			}
		}
	}

	public bool IsReduceMotionBlur
	{
		get
		{
			return _isReduceMotionBlur;
		}
		set
		{
			if (_isReduceMotionBlur != value)
			{
				_isReduceMotionBlur = value;
				this.RaisePropertyChanged("IsReduceMotionBlur");
				if (value != ViewModel.EnableReduceMotionBlur)
				{
					ViewModel.ReduceMotionBlurCmd.Execute().Subscribe();
				}
			}
		}
	}

	public bool IsMouseShake
	{
		get
		{
			return _isMouseShake;
		}
		set
		{
			if (_isMouseShake != value)
			{
				_isMouseShake = value;
				this.RaisePropertyChanged("IsMouseShake");
				if (value != ViewModel.EnableMouseShake)
				{
					ViewModel.MouseShakeCmd.Execute().Subscribe();
				}
			}
		}
	}

	public bool IsHandTrack
	{
		get
		{
			return _isHandTrack;
		}
		set
		{
			if (_isHandTrack != value)
			{
				_isHandTrack = value;
				this.RaisePropertyChanged("IsHandTrack");
				if (value != ViewModel.HandTrack)
				{
					ViewModel.HandTrackCmd.Execute().Subscribe();
				}
			}
		}
	}

	public bool IsSvsEnabled
	{
		get
		{
			return _isSvsEnabled;
		}
		set
		{
			if (_isSvsEnabled != value)
			{
				_isSvsEnabled = value;
				this.RaisePropertyChanged("IsSvsEnabled");
				if (value != ViewModel.SvsEnable)
				{
					ViewModel.SvsEnableCmd.Execute().Subscribe();
				}
			}
		}
	}

	public bool IsSvsDebug
	{
		get
		{
			return _isSvsDebug;
		}
		set
		{
			if (_isSvsDebug != value)
			{
				_isSvsDebug = value;
				this.RaisePropertyChanged("IsSvsDebug");
				if (value != ViewModel.SvsDebug)
				{
					ViewModel.SvsDebugCmd.Execute().Subscribe();
				}
			}
		}
	}

	public bool IsBuildInScreenOn
	{
		get
		{
			return _isBuildInScreenOn;
		}
		set
		{
			if (_isBuildInScreenOn != value)
			{
				_isBuildInScreenOn = value;
				this.RaisePropertyChanged("IsBuildInScreenOn");
				if (value == DbManager.Instance.Settings.TurnOffBuildInScreen)
				{
					ViewModel.TurnOffBuildInScreenCmd.Execute().Subscribe();
				}
			}
		}
	}

	public bool IsSmoothFollow
	{
		get
		{
			return _isSmoothFollow;
		}
		set
		{
			if (_isSmoothFollow != value)
			{
				_isSmoothFollow = value;
				this.RaisePropertyChanged("IsSmoothFollow");
				if (value != DbManager.Instance.Settings.SmoothFollow)
				{
					ViewModel.SmoothFollowCmd.Execute().Subscribe();
				}
			}
		}
	}

	public bool IsHighDpiScale
	{
		get
		{
			return _isHighDpiScale;
		}
		set
		{
			if (_isHighDpiScale != value)
			{
				_isHighDpiScale = value;
				this.RaisePropertyChanged("IsHighDpiScale");
				if (value != ViewModel.EnableHighDpiScale)
				{
					ViewModel.HighDpiScaleCmd.Execute().Subscribe();
				}
			}
		}
	}

	public string[] UltraWideRatioOptions => RatioOptions;

	public int SelectedUltraWideRatioIndex
	{
		get
		{
			return _isUltraWideMode ? 1 : 0;
		}
		set
		{
			if (value < 0)
			{
				return;
			}
			bool flag = value == 1;
			if (_isUltraWideMode != flag)
			{
				_isUltraWideMode = flag;
				this.RaisePropertyChanged("SelectedUltraWideRatioIndex");
				if (flag != ViewModel.UseUltraWideSize)
				{
					ViewModel.UltraWideCmd.Execute().Subscribe();
				}
			}
		}
	}

	public bool IsFilmControlVisible
	{
		get
		{
			return _isFilmControlVisible;
		}
		set
		{
			this.RaiseAndSetIfChanged(ref _isFilmControlVisible, value, "IsFilmControlVisible");
		}
	}

	public bool IsFilmAngleVisible => _isFilmAngleVisible?.Value ?? false;

	public bool IsFilmSeparatorVisible => _isFilmSeparatorVisible?.Value ?? false;

	public bool IsHandGestureHelpVisible => _isHandGestureHelpVisible?.Value ?? false;

	public bool IsReduceMotionBlurVisible
	{
		get
		{
			return _isReduceMotionBlurVisible;
		}
		set
		{
			this.RaiseAndSetIfChanged(ref _isReduceMotionBlurVisible, value, "IsReduceMotionBlurVisible");
		}
	}

	public bool IsMouseShakeVisible
	{
		get
		{
			return _isMouseShakeVisible;
		}
		set
		{
			this.RaiseAndSetIfChanged(ref _isMouseShakeVisible, value, "IsMouseShakeVisible");
		}
	}

	public bool IsSvsEnabledVisible
	{
		get
		{
			return _isSvsEnabledVisible;
		}
		set
		{
			this.RaiseAndSetIfChanged(ref _isSvsEnabledVisible, value, "IsSvsEnabledVisible");
		}
	}

	public bool IsSvsDebugVisible
	{
		get
		{
			return _isSvsDebugVisible;
		}
		set
		{
			this.RaiseAndSetIfChanged(ref _isSvsDebugVisible, value, "IsSvsDebugVisible");
		}
	}

	public bool IsTurnOffBuildInScreenVisible
	{
		get
		{
			return _isTurnOffBuildInScreenVisible;
		}
		set
		{
			this.RaiseAndSetIfChanged(ref _isTurnOffBuildInScreenVisible, value, "IsTurnOffBuildInScreenVisible");
		}
	}

	public bool IsSmoothFollowVisible
	{
		get
		{
			return _isSmoothFollowVisible;
		}
		set
		{
			this.RaiseAndSetIfChanged(ref _isSmoothFollowVisible, value, "IsSmoothFollowVisible");
		}
	}

	public bool IsHighDpiScaleVisible
	{
		get
		{
			return _isHighDpiScaleVisible;
		}
		set
		{
			this.RaiseAndSetIfChanged(ref _isHighDpiScaleVisible, value, "IsHighDpiScaleVisible");
		}
	}

	public bool IsUltraWideModeVisible
	{
		get
		{
			return _isUltraWideModeVisible;
		}
		set
		{
			this.RaiseAndSetIfChanged(ref _isUltraWideModeVisible, value, "IsUltraWideModeVisible");
		}
	}

	public bool IsHandTrackVisible
	{
		get
		{
			return _isHandTrackVisible;
		}
		set
		{
			this.RaiseAndSetIfChanged(ref _isHandTrackVisible, value, "IsHandTrackVisible");
		}
	}

	public bool IsDefaultTheme
	{
		get
		{
			return ThemeManager.Instance.CurrentTheme == AppTheme.Default;
		}
		set
		{
			if (value && !IsDefaultTheme)
			{
				ThemeManager.Instance.Apply(AppTheme.Default);
				RaiseThemeChanged();
			}
		}
	}

	public bool IsPbzTheme
	{
		get
		{
			return ThemeManager.Instance.CurrentTheme == AppTheme.PhantomBladeZero;
		}
		set
		{
			if (value && !IsPbzTheme)
			{
				ThemeManager.Instance.Apply(AppTheme.PhantomBladeZero);
				RaiseThemeChanged();
			}
		}
	}

	public bool IsSkyboxNone => string.IsNullOrEmpty(_skybox);

	public bool IsSkyboxEverest => _skybox == "Everest.jpg";

	public bool IsSkyboxBlueWater => _skybox == "BlueWater.jpg";

	public bool IsSkyboxNorthernLights => _skybox == "NorthernLights.jpg";

	public bool IsSkyboxEltonLake => _skybox == "EltonLake.jpg";

	public bool IsSkyboxCustom
	{
		get
		{
			if (!string.IsNullOrEmpty(_skybox))
			{
				return !PresetSkyboxes.Contains(_skybox);
			}
			return false;
		}
	}

	public Bitmap? CustomSkyboxBitmap
	{
		get
		{
			return _customSkyboxBitmap;
		}
		private set
		{
			this.RaiseAndSetIfChanged(ref _customSkyboxBitmap, value, "CustomSkyboxBitmap");
		}
	}

	public bool HasCustomSkyboxBitmap
	{
		get
		{
			return _hasCustomSkyboxBitmap;
		}
		private set
		{
			this.RaiseAndSetIfChanged(ref _hasCustomSkyboxBitmap, value, "HasCustomSkyboxBitmap");
		}
	}

	public bool CanChangeSkybox
	{
		get
		{
			return canChangeSkybox;
		}
		private set
		{
			this.RaiseAndSetIfChanged(ref canChangeSkybox, value, "CanChangeSkybox");
		}
	}

	public CustomHotkeyViewModel HotkeyVM
	{
		get
		{
			return _hotkeyVM;
		}
		set
		{
			this.RaiseAndSetIfChanged(ref _hotkeyVM, value, "HotkeyVM");
		}
	}

	public bool ShowHandGestureHelp
	{
		get
		{
			return _showHandGestureHelp;
		}
		set
		{
			this.RaiseAndSetIfChanged(ref _showHandGestureHelp, value, "ShowHandGestureHelp");
		}
	}

	public bool HotkeyConflict
	{
		get
		{
			return _hotkeyConflict;
		}
		set
		{
			this.RaiseAndSetIfChanged(ref _hotkeyConflict, value, "HotkeyConflict");
		}
	}

	public string Email
	{
		get
		{
			return _email;
		}
		set
		{
			this.RaiseAndSetIfChanged(ref _email, value, "Email");
		}
	}

	public string Description
	{
		get
		{
			return _description;
		}
		set
		{
			this.RaiseAndSetIfChanged(ref _description, value, "Description");
		}
	}

	public bool IsSubmitting
	{
		get
		{
			return _isSubmitting;
		}
		set
		{
			this.RaiseAndSetIfChanged(ref _isSubmitting, value, "IsSubmitting");
		}
	}

	public string SubmitMessage
	{
		get
		{
			return _submitMessage;
		}
		set
		{
			this.RaiseAndSetIfChanged(ref _submitMessage, value, "SubmitMessage");
			this.RaisePropertyChanged("HasSubmitMessage");
		}
	}

	public bool HasSubmitMessage => !string.IsNullOrWhiteSpace(SubmitMessage);

	public string ShortcutsMessage
	{
		get
		{
			return _shortcutsMessage;
		}
		set
		{
			this.RaiseAndSetIfChanged(ref _shortcutsMessage, value, "ShortcutsMessage");
			this.RaisePropertyChanged("HasShortcutsMessage");
		}
	}

	public bool HasShortcutsMessage => !string.IsNullOrWhiteSpace(ShortcutsMessage);

	public ICommand SwitchHandGestureHelpCmd { get; }

	public ICommand FeedbackCmd { get; }

	public ICommand ApplyShortcutsCmd { get; }

	public ICommand RestoreShortcutsCmd { get; }

	public SettingsViewModel(MainViewModel vm)
	{
		MainViewModel vm2 = vm;
		base._002Ector();
		SettingsViewModel settingsViewModel = this;
		ViewModel = vm2;
		_submitHint = new TransientMessage(delegate(string v)
		{
			settingsViewModel.SubmitMessage = v;
		});
		_shortcutsHint = new TransientMessage(delegate(string v)
		{
			settingsViewModel.ShortcutsMessage = v;
		});
		SwitchHandGestureHelpCmd = ReactiveCommand.Create(() => settingsViewModel.ShowHandGestureHelp = !settingsViewModel.ShowHandGestureHelp);
		ApplyShortcutsCmd = ReactiveCommand.Create(ApplyShortcuts);
		RestoreShortcutsCmd = ReactiveCommand.Create(RestoreShortcuts);
		IObservable<bool> canExecute = this.WhenAnyValue((SettingsViewModel x) => x.Description, (SettingsViewModel x) => x.IsSubmitting, (string desc, bool submitting) => !submitting && !string.IsNullOrWhiteSpace(desc));
		FeedbackCmd = ReactiveCommand.CreateFromTask(SubmitFeedbackAsync, canExecute);
		_isFilmAngleVisible = this.WhenAnyValue((SettingsViewModel x) => x.IsFilmControlVisible, (SettingsViewModel x) => x.IsFilmControlEnabled, (bool visible, bool enabled) => visible && enabled).ToProperty(this, (SettingsViewModel x) => x.IsFilmAngleVisible);
		_isFilmSeparatorVisible = this.WhenAnyValue((SettingsViewModel x) => x.IsFilmControlVisible, (SettingsViewModel x) => x.IsFilmControlEnabled, (bool visible, bool enabled) => visible && !enabled).ToProperty(this, (SettingsViewModel x) => x.IsFilmSeparatorVisible);
		_isHandGestureHelpVisible = this.WhenAnyValue((SettingsViewModel x) => x.ShowHandGestureHelp, (SettingsViewModel x) => x.IsHandTrackVisible, (bool show, bool trackVisible) => show && trackVisible).ToProperty(this, (SettingsViewModel x) => x.IsHandGestureHelpVisible);
		this.WhenActivated(delegate(CompositeDisposable disposables)
		{
			settingsViewModel.HotkeyVM = new CustomHotkeyViewModel();
			AvaloniaScheduler instance = AvaloniaScheduler.Instance;
			ThemeManager.Instance.WhenAnyValue((ThemeManager x) => x.CurrentTheme).ObserveOn(instance).Subscribe(delegate
			{
				settingsViewModel.RaiseThemeChanged();
			})
				.DisposeWith(disposables);
			vm2.WhenAnyValue((MainViewModel x) => x.VitureCommConnect, (MainViewModel x) => x.VitureDisplayConnect).ObserveOn(instance).Subscribe(delegate
			{
				settingsViewModel.CanChangeSkybox = !vm2.IsNative3DofMode();
			})
				.DisposeWith(disposables);
			vm2.WhenAnyValue((MainViewModel x) => x.FilmControlAngle).ObserveOn(instance).Subscribe(delegate(int angle)
			{
				settingsViewModel._isFilmControlEnabled = angle >= 0;
				settingsViewModel.RaisePropertyChanged("IsFilmControlEnabled");
				if (angle >= 0)
				{
					settingsViewModel._lastPositiveFilmAngle = angle;
					settingsViewModel._filmAngle = angle;
					settingsViewModel.RaisePropertyChanged("FilmAngle");
				}
			})
				.DisposeWith(disposables);
			vm2.WhenAnyValue((MainViewModel x) => x.EnableReduceMotionBlur).ObserveOn(instance).Subscribe(delegate(bool v)
			{
				settingsViewModel.IsReduceMotionBlur = v;
			})
				.DisposeWith(disposables);
			vm2.WhenAnyValue((MainViewModel x) => x.EnableMouseShake).ObserveOn(instance).Subscribe(delegate(bool v)
			{
				settingsViewModel.IsMouseShake = v;
			})
				.DisposeWith(disposables);
			vm2.WhenAnyValue((MainViewModel x) => x.HandTrack).ObserveOn(instance).Subscribe(delegate(bool v)
			{
				settingsViewModel.IsHandTrack = v;
			})
				.DisposeWith(disposables);
			vm2.WhenAnyValue((MainViewModel x) => x.SvsEnable).ObserveOn(instance).Subscribe(delegate(bool v)
			{
				settingsViewModel.IsSvsEnabled = v;
			})
				.DisposeWith(disposables);
			vm2.WhenAnyValue((MainViewModel x) => x.SvsDebug).ObserveOn(instance).Subscribe(delegate(bool v)
			{
				settingsViewModel.IsSvsDebug = v;
			})
				.DisposeWith(disposables);
			vm2.WhenAnyValue((MainViewModel x) => x.TurnOffBuildInScreen).ObserveOn(instance).Subscribe(delegate
			{
				settingsViewModel.IsBuildInScreenOn = !DbManager.Instance.Settings.TurnOffBuildInScreen;
			})
				.DisposeWith(disposables);
			vm2.WhenAnyValue((MainViewModel x) => x.SmoothFollow).ObserveOn(instance).Subscribe(delegate
			{
				settingsViewModel.IsSmoothFollow = DbManager.Instance.Settings.SmoothFollow;
			})
				.DisposeWith(disposables);
			vm2.WhenAnyValue((MainViewModel x) => x.EnableHighDpiScale).ObserveOn(instance).Subscribe(delegate(bool v)
			{
				settingsViewModel.IsHighDpiScale = v;
			})
				.DisposeWith(disposables);
			vm2.WhenAnyValue((MainViewModel x) => x.UseUltraWideSize).ObserveOn(instance).Subscribe(delegate(bool v)
			{
				settingsViewModel.SelectedUltraWideRatioIndex = (v ? 1 : 0);
			})
				.DisposeWith(disposables);
			vm2.WhenAnyValue((MainViewModel x) => x.Skybox).ObserveOn(instance).Subscribe(settingsViewModel.SyncSkybox)
				.DisposeWith(disposables);
			vm2.WhenAnyValue((MainViewModel x) => x.CustomSkyboxFile).ObserveOn(instance).Subscribe(settingsViewModel.SyncCustomSkyboxThumb)
				.DisposeWith(disposables);
			vm2.WhenAnyValue((MainViewModel x) => x.VitureDeviceType).ObserveOn(instance).Subscribe(settingsViewModel.ApplyDeviceVisibility)
				.DisposeWith(disposables);
			Disposable.Create(delegate
			{
				if (settingsViewModel._hotkeyUnregistered)
				{
					settingsViewModel._hotkeyUnregistered = false;
					MainViewModel.RegisterHotKey();
				}
			}).DisposeWith(disposables);
		});
	}

	private void ApplyDeviceVisibility(VitureDeviceType deviceType)
	{
		bool flag = deviceType == VitureDeviceType.R6;
		bool flag2 = deviceType == VitureDeviceType.S6;
		bool flag3 = deviceType == VitureDeviceType.S6P;
		bool flag4 = deviceType == VitureDeviceType.P6S;
		bool flag5 = deviceType == VitureDeviceType.None;
		bool flag6 = flag || flag3;
		IsFilmControlVisible = !flag5 && !flag6 && !flag2;
		IsReduceMotionBlurVisible = !flag5 && !flag6;
		IsSvsEnabledVisible = !flag5 && flag;
		IsUltraWideModeVisible = !flag5 && !flag;
		IsHandTrackVisible = !flag5 && flag4;
		IsSmoothFollowVisible = !flag5 && !flag;
	}

	private void RaiseThemeChanged()
	{
		this.RaisePropertyChanged("IsDefaultTheme");
		this.RaisePropertyChanged("IsPbzTheme");
	}

	private void SyncSkybox(string skybox)
	{
		_skybox = skybox;
		this.RaisePropertyChanged("IsSkyboxNone");
		this.RaisePropertyChanged("IsSkyboxEverest");
		this.RaisePropertyChanged("IsSkyboxBlueWater");
		this.RaisePropertyChanged("IsSkyboxNorthernLights");
		this.RaisePropertyChanged("IsSkyboxEltonLake");
		this.RaisePropertyChanged("IsSkyboxCustom");
	}

	private void SyncCustomSkyboxThumb(string? file)
	{
		Bitmap customSkyboxBitmap = CustomSkyboxBitmap;
		if (!string.IsNullOrEmpty(file) && File.Exists(file))
		{
			try
			{
				CustomSkyboxBitmap = new Bitmap(file);
				HasCustomSkyboxBitmap = true;
				customSkyboxBitmap?.Dispose();
				return;
			}
			catch
			{
			}
		}
		CustomSkyboxBitmap = null;
		HasCustomSkyboxBitmap = false;
		customSkyboxBitmap?.Dispose();
	}

	private void ApplyShortcuts()
	{
		if (!ValidateShortcuts())
		{
			return;
		}
		foreach (GlobalHotkeyItem globalHotkey in HotkeyVM.GlobalHotkeys)
		{
			DbManager.Instance.Settings.GlobalHotkeys[globalHotkey.Header] = globalHotkey.HotKey;
		}
		DbManager.Instance.SaveSettings();
		MainViewModel.UnRegisterHotkey();
		_hotkeyUnregistered = true;
		_shortcutsHint.Show(Resources.ShortcutsApplied);
	}

	internal bool ValidateShortcuts()
	{
		HashSet<string> hashSet = new HashSet<string>();
		bool flag = true;
		foreach (GlobalHotkeyItem globalHotkey in HotkeyVM.GlobalHotkeys)
		{
			if (string.IsNullOrWhiteSpace(globalHotkey.HotKey))
			{
				flag = false;
				break;
			}
			KeyGesture keyGesture = KeyGesture.Parse(globalHotkey.HotKey);
			if (!hashSet.Add(globalHotkey.HotKey) || keyGesture.KeyModifiers == KeyModifiers.None || KeyCodeIsModifierOnly(keyGesture.Key))
			{
				flag = false;
				break;
			}
		}
		if (!flag)
		{
			HotkeyConflict = true;
			_shortcutsHint.Show(Resources.HotkeyConflict, persistent: true);
		}
		else if (HotkeyConflict)
		{
			HotkeyConflict = false;
			_shortcutsHint.Clear();
		}
		return flag;
	}

	private void RestoreShortcuts()
	{
		HotkeyConflict = false;
		HotkeyVM = new CustomHotkeyViewModel();
		_shortcutsHint.Show(Resources.ShortcutsRestored);
	}

	internal static bool KeyCodeIsModifierOnly(Key key)
	{
		string text = key.ToString();
		if (!text.Contains("Alt") && !text.Contains("Ctrl") && !text.Contains("Shift") && !text.Contains("Meta"))
		{
			return text.Contains("Win");
		}
		return true;
	}

	private async Task SubmitFeedbackAsync()
	{
		IsSubmitting = true;
		_submitHint.Clear();
		try
		{
			await FeedbackHelper.Request(Email, Description);
			_submitHint.Show(Resources.FeedbackSubmitSuccess);
			Email = string.Empty;
			Description = string.Empty;
		}
		catch (Exception)
		{
			_submitHint.Show(Resources.FeedbackSubmitFailed, persistent: true);
		}
		finally
		{
			IsSubmitting = false;
		}
	}
}
