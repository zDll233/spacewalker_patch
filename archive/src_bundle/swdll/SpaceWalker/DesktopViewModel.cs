using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Disposables.Fluent;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Reflection;
using System.Threading.Tasks;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using ReactiveUI;
using ReactiveUI.Avalonia;
using SpaceWalker.Assets.Languages;
using SpaceWalker.ViewModels;
using VitureCommonLibrary;

namespace SpaceWalker;

public class DesktopViewModel : ReactiveObject, IActivatableViewModel
{
	private readonly int[] _refreshRates;

	private string _statusText;

	private IBrush _statusColor;

	private double _brightnessSliderValue;

	private double _volumeSliderValue;

	private double _zoomSliderValue;

	private double _zoomSliderMin;

	private double _zoomSliderMax = 4.0;

	private double _zoomSliderTick = 1.0;

	private IReadOnlyList<LayoutModeOption>? _modeOptions;

	private LayoutModeOption? _selectedModeOption;

	private IReadOnlyList<LayoutTypeOption>? _typeOptions;

	private LayoutTypeOption? _selectedTypeOption;

	private int _selectedScreenSizeIndex;

	private int _selectedRefreshRateIndex;

	private bool _suppressLayoutExecute;

	private IReadOnlyList<LayoutType>? _typeOptionsSource;

	public ViewModelActivator Activator { get; } = new ViewModelActivator();


	public MainViewModel ViewModel { get; }

	public string DeviceName { get; }

	public int VolumeMaximum { get; }

	public IBrush? DeviceBackground { get; }

	public IReadOnlyList<string> RefreshRateOptions { get; }

	public IReadOnlyList<string> ScreenSizeOptions { get; } = new string[5]
	{
		Resources.ScreenSizeSmall,
		Resources.ScreenSizeMedium,
		Resources.ScreenSizeLarge,
		Resources.ScreenSizeExtraLarge,
		Resources.ScreenSizeUltraLarge
	};


	public string StatusText
	{
		get
		{
			return _statusText;
		}
		private set
		{
			this.RaiseAndSetIfChanged(ref _statusText, value, "StatusText");
		}
	}

	public IBrush StatusColor
	{
		get
		{
			return _statusColor;
		}
		private set
		{
			this.RaiseAndSetIfChanged(ref _statusColor, value, "StatusColor");
		}
	}

	public double BrightnessSliderValue
	{
		get
		{
			return _brightnessSliderValue;
		}
		set
		{
			this.RaiseAndSetIfChanged(ref _brightnessSliderValue, value, "BrightnessSliderValue");
		}
	}

	public double VolumeSliderValue
	{
		get
		{
			return _volumeSliderValue;
		}
		set
		{
			this.RaiseAndSetIfChanged(ref _volumeSliderValue, value, "VolumeSliderValue");
		}
	}

	public double ZoomSliderValue
	{
		get
		{
			return _zoomSliderValue;
		}
		set
		{
			this.RaiseAndSetIfChanged(ref _zoomSliderValue, value, "ZoomSliderValue");
		}
	}

	public double ZoomSliderMin
	{
		get
		{
			return _zoomSliderMin;
		}
		private set
		{
			this.RaiseAndSetIfChanged(ref _zoomSliderMin, value, "ZoomSliderMin");
		}
	}

	public double ZoomSliderMax
	{
		get
		{
			return _zoomSliderMax;
		}
		private set
		{
			this.RaiseAndSetIfChanged(ref _zoomSliderMax, value, "ZoomSliderMax");
		}
	}

	public double ZoomSliderTick
	{
		get
		{
			return _zoomSliderTick;
		}
		private set
		{
			this.RaiseAndSetIfChanged(ref _zoomSliderTick, value, "ZoomSliderTick");
		}
	}

	public IReadOnlyList<LayoutModeOption>? ModeOptions
	{
		get
		{
			return _modeOptions;
		}
		set
		{
			this.RaiseAndSetIfChanged(ref _modeOptions, value, "ModeOptions");
		}
	}

	public LayoutModeOption? SelectedModeOption
	{
		get
		{
			return _selectedModeOption;
		}
		set
		{
			this.RaiseAndSetIfChanged(ref _selectedModeOption, value, "SelectedModeOption");
		}
	}

	public IReadOnlyList<LayoutTypeOption>? TypeOptions
	{
		get
		{
			return _typeOptions;
		}
		set
		{
			this.RaiseAndSetIfChanged(ref _typeOptions, value, "TypeOptions");
		}
	}

	public LayoutTypeOption? SelectedTypeOption
	{
		get
		{
			return _selectedTypeOption;
		}
		set
		{
			this.RaiseAndSetIfChanged(ref _selectedTypeOption, value, "SelectedTypeOption");
		}
	}

	public int SelectedScreenSizeIndex
	{
		get
		{
			return _selectedScreenSizeIndex;
		}
		set
		{
			if (_selectedScreenSizeIndex != value)
			{
				_selectedScreenSizeIndex = value;
				this.RaisePropertyChanged("SelectedScreenSizeIndex");
				if (value >= 0 && value != ViewModel.ScreenSize)
				{
					ViewModel.ScreenSizeCmd.Execute(value).Subscribe();
				}
			}
		}
	}

	public int SelectedRefreshRateIndex
	{
		get
		{
			return _selectedRefreshRateIndex;
		}
		set
		{
			if (_selectedRefreshRateIndex == value)
			{
				return;
			}
			_selectedRefreshRateIndex = value;
			this.RaisePropertyChanged("SelectedRefreshRateIndex");
			if (value < 0 || value >= _refreshRates.Length)
			{
				return;
			}
			int rate = _refreshRates[value];
			if (rate == ViewModel.FrameRate)
			{
				return;
			}
			ViewModel.FrameRate = rate;
			Task.Run(delegate
			{
				try
				{
					DisplayManager2.Instance.SetRefreshRate(rate);
				}
				catch (Exception ex)
				{
					Logger.Error(ex.Message, ex.StackTrace);
				}
			});
		}
	}

	public DesktopViewModel(MainViewModel vm)
	{
		MainViewModel vm2 = vm;
		base._002Ector();
		DesktopViewModel desktopViewModel = this;
		ViewModel = vm2;
		VitureDeviceType vitureDeviceType = vm2.VitureDeviceType;
		DeviceName = vm2.GetVitureDeviceName(vitureDeviceType);
		VolumeMaximum = ((vitureDeviceType == VitureDeviceType.R6) ? 15 : 8);
		DeviceBackground = BuildDeviceBackground(vitureDeviceType);
		_refreshRates = ((!vm2.SupportHighRefreshRate) ? new int[1] { 60 } : new int[3] { 60, 90, 120 });
		RefreshRateOptions = _refreshRates.Select(GetRefreshRateName).ToArray();
		_selectedRefreshRateIndex = Math.Max(0, Array.IndexOf(_refreshRates, vm2.FrameRate));
		bool vitureCommConnect = vm2.VitureCommConnect;
		_statusText = (vitureCommConnect ? Resources.Connected : Resources.Disconnected);
		_statusColor = new SolidColorBrush(vitureCommConnect ? Color.Parse("#50E255") : Colors.Yellow);
		_zoomSliderValue = vm2.ZoomLevel;
		_brightnessSliderValue = vm2.BrightnessLevel;
		_volumeSliderValue = ((vm2.VolumeLevel >= 0) ? vm2.VolumeLevel : 0);
		this.WhenActivated(delegate(CompositeDisposable disposables)
		{
			vm2.WhenAnyValue((MainViewModel x) => x.VitureCommConnect).Subscribe(delegate(bool c)
			{
				desktopViewModel.StatusText = (c ? Resources.Connected : Resources.Disconnected);
				desktopViewModel.StatusColor = new SolidColorBrush(c ? Color.Parse("#50E255") : Colors.Yellow);
			}).DisposeWith(disposables);
			vm2.WhenAnyValue((MainViewModel x) => x.ZoomLevel).DistinctUntilChanged().Subscribe(delegate(double v)
			{
				desktopViewModel.ZoomSliderValue = v;
			})
				.DisposeWith(disposables);
			vm2.WhenAnyValue((MainViewModel x) => x.BrightnessLevel).DistinctUntilChanged().Subscribe(delegate(int v)
			{
				desktopViewModel.BrightnessSliderValue = v;
			})
				.DisposeWith(disposables);
			(from v in vm2.WhenAnyValue((MainViewModel x) => x.VolumeLevel)
				where v >= 0
				select v).DistinctUntilChanged().Subscribe(delegate(int v)
			{
				desktopViewModel.VolumeSliderValue = v;
			}).DisposeWith(disposables);
			vm2.WhenAnyValue((MainViewModel x) => x.MonitorCount, (MainViewModel x) => x.TurnOffBuildInScreen).ObserveOn(AvaloniaScheduler.Instance).Subscribe(delegate
			{
				desktopViewModel.ModeOptions = BuildModeOptions(vm2);
				desktopViewModel.SyncSelectionFromVm(vm2);
			})
				.DisposeWith(disposables);
			vm2.WhenAnyValue((MainViewModel x) => x.VitureLayoutMode, (MainViewModel x) => x.LayoutType).ObserveOn(AvaloniaScheduler.Instance).Subscribe(delegate
			{
				desktopViewModel.SyncSelectionFromVm(vm2);
			})
				.DisposeWith(disposables);
			vm2.WhenAnyValue((MainViewModel x) => x.ScreenSize).ObserveOn(AvaloniaScheduler.Instance).Subscribe(delegate(int v)
			{
				desktopViewModel.SelectedScreenSizeIndex = v;
			})
				.DisposeWith(disposables);
			vm2.WhenAnyValue((MainViewModel x) => x.FrameRate).ObserveOn(AvaloniaScheduler.Instance).Subscribe(delegate(int rate)
			{
				int num = Array.IndexOf(desktopViewModel._refreshRates, rate);
				if (num >= 0)
				{
					desktopViewModel.SelectedRefreshRateIndex = num;
				}
			})
				.DisposeWith(disposables);
			(from _ in Observable.FromEvent(delegate(Action h)
				{
					DisplayManager2.Instance.DisplayChanged += h;
				}, delegate(Action h)
				{
					DisplayManager2.Instance.DisplayChanged -= h;
				}).Throttle(TimeSpan.FromMilliseconds(500.0))
				select DisplayManager2.Instance.GetVitureRefreshRate() into rate
				where rate.HasValue && Array.IndexOf(desktopViewModel._refreshRates, rate.Value) >= 0
				select rate).ObserveOn(AvaloniaScheduler.Instance).Subscribe(delegate(int? rate)
			{
				if (vm2.FrameRate != rate.Value)
				{
					vm2.FrameRate = rate.Value;
				}
			}, delegate(Exception ex)
			{
				Logger.Error(ex.Message, ex.StackTrace);
			}).DisposeWith(disposables);
			(from mode in desktopViewModel.WhenAnyValue((DesktopViewModel x) => x.SelectedModeOption).Skip(1)
				where mode != null && !desktopViewModel._suppressLayoutExecute
				select mode).Select((Func<LayoutModeOption, Task>)async delegate(LayoutModeOption mode)
			{
				LayoutType type = desktopViewModel.AlignTypeOptionsToMode(mode, vm2.LayoutType);
				await desktopViewModel.ApplyLayoutAsync(mode.Mode, type);
			}).Subscribe(delegate
			{
			}, delegate(Exception ex)
			{
				Logger.Error(ex.Message, ex.StackTrace);
			}).DisposeWith(disposables);
			(from t in desktopViewModel.WhenAnyValue((DesktopViewModel x) => x.SelectedTypeOption).Skip(1)
				where t != null && !desktopViewModel._suppressLayoutExecute && desktopViewModel.SelectedModeOption != null
				select t).Select((Func<LayoutTypeOption, Task>)async delegate(LayoutTypeOption t)
			{
				await desktopViewModel.ApplyLayoutAsync(desktopViewModel.SelectedModeOption.Mode, t.Type);
			}).Subscribe(delegate
			{
			}, delegate(Exception ex)
			{
				Logger.Error(ex.Message, ex.StackTrace);
			}).DisposeWith(disposables);
			vm2.WhenAnyValue((MainViewModel x) => x.ViewAppRunning).ObserveOn(AvaloniaScheduler.Instance).Subscribe(delegate(bool running)
			{
				if (GlassesDeviceManager.Instance.SupportNative3Dof)
				{
					desktopViewModel.ZoomSliderMin = 0.0;
					desktopViewModel.ZoomSliderMax = 9.0;
					desktopViewModel.ZoomSliderTick = 1.0;
				}
				else if (running)
				{
					desktopViewModel.ZoomSliderMin = 0.5;
					desktopViewModel.ZoomSliderMax = 2.0;
					desktopViewModel.ZoomSliderTick = 0.1;
				}
				else
				{
					desktopViewModel.ZoomSliderMin = 0.0;
					desktopViewModel.ZoomSliderMax = 4.0;
					desktopViewModel.ZoomSliderTick = 1.0;
				}
				desktopViewModel.ZoomSliderValue = vm2.ZoomLevel;
			})
				.DisposeWith(disposables);
		});
	}

	private static string GetRefreshRateName(int rate)
	{
		return rate switch
		{
			120 => Resources.RefreshRate120Hz, 
			90 => Resources.RefreshRate90Hz, 
			_ => Resources.RefreshRate60Hz, 
		};
	}

	private void SyncSelectionFromVm(MainViewModel vm)
	{
		MainViewModel vm2 = vm;
		IReadOnlyList<LayoutModeOption> modeOptions = ModeOptions;
		if (modeOptions == null || modeOptions.Count == 0)
		{
			return;
		}
		LayoutModeOption layoutModeOption = modeOptions.FirstOrDefault((LayoutModeOption m) => m.Mode == vm2.VitureLayoutMode) ?? modeOptions[0];
		_suppressLayoutExecute = true;
		try
		{
			if (layoutModeOption != SelectedModeOption)
			{
				SelectedModeOption = layoutModeOption;
			}
		}
		finally
		{
			_suppressLayoutExecute = false;
		}
		AlignTypeOptionsToMode(layoutModeOption, vm2.LayoutType);
	}

	private LayoutType AlignTypeOptionsToMode(LayoutModeOption mode, LayoutType preferred)
	{
		LayoutType resolved = (mode.SupportedTypes.Contains(preferred) ? preferred : mode.SupportedTypes[0]);
		_suppressLayoutExecute = true;
		try
		{
			if (_typeOptionsSource != mode.SupportedTypes)
			{
				TypeOptions = mode.SupportedTypes.Select((LayoutType t) => new LayoutTypeOption(GetLayoutTypeName(t), t)).ToList();
				_typeOptionsSource = mode.SupportedTypes;
			}
			LayoutTypeOption layoutTypeOption = TypeOptions.First((LayoutTypeOption t) => t.Type == resolved);
			if (layoutTypeOption != SelectedTypeOption)
			{
				SelectedTypeOption = layoutTypeOption;
			}
		}
		finally
		{
			_suppressLayoutExecute = false;
		}
		return resolved;
	}

	private Task ApplyLayoutAsync(VitureLayoutMode mode, LayoutType type)
	{
		ViewModel.VitureLayoutMode = mode;
		ViewModel.LayoutType = type;
		return ViewModel.LaunchCmd.Execute().ToTask();
	}

	public void CommitZoom()
	{
		ViewModel.ZoomCmd.Execute(ZoomSliderValue).Subscribe();
	}

	public void CommitBrightness()
	{
		int num = (int)Math.Round(BrightnessSliderValue);
		if (num != ViewModel.BrightnessLevel)
		{
			ViewModel.BrightnessCmd.Execute(num).Subscribe();
		}
	}

	public void CommitVolume()
	{
		if (ViewModel.VolumeLevel >= 0)
		{
			int num = (int)Math.Round(VolumeSliderValue);
			if (num != ViewModel.VolumeLevel)
			{
				ViewModel.VolumeCmd.Execute(num).Subscribe();
			}
		}
	}

	private static IBrush? BuildDeviceBackground(VitureDeviceType deviceType)
	{
		IImageBrushSource vitureDeviceImage = GetVitureDeviceImage(deviceType);
		if (vitureDeviceImage == null)
		{
			return null;
		}
		return new ImageBrush
		{
			Source = vitureDeviceImage,
			Stretch = Stretch.UniformToFill,
			AlignmentX = AlignmentX.Center,
			AlignmentY = AlignmentY.Center
		};
	}

	private static IImageBrushSource? GetVitureDeviceImage(VitureDeviceType deviceType)
	{
		try
		{
			string name = Assembly.GetExecutingAssembly().GetName().Name;
			return deviceType switch
			{
				VitureDeviceType.N6 => new Bitmap(AssetLoader.Open(new Uri("avares://" + name + "/Assets/Images/bg_one.png"))), 
				VitureDeviceType.N6C => new Bitmap(AssetLoader.Open(new Uri("avares://" + name + "/Assets/Images/bg_n6c.png"))), 
				VitureDeviceType.N6P => new Bitmap(AssetLoader.Open(new Uri("avares://" + name + "/Assets/Images/bg_n6p.png"))), 
				VitureDeviceType.P6 => new Bitmap(AssetLoader.Open(new Uri("avares://" + name + "/Assets/Images/bg_p6.png"))), 
				VitureDeviceType.P6C => new Bitmap(AssetLoader.Open(new Uri("avares://" + name + "/Assets/Images/bg_p6c.png"))), 
				VitureDeviceType.P6S => new Bitmap(AssetLoader.Open(new Uri("avares://" + name + "/Assets/Images/bg_p6s.png"))), 
				VitureDeviceType.P6X => new Bitmap(AssetLoader.Open(new Uri("avares://" + name + "/Assets/Images/bg_p6.png"))), 
				_ => new Bitmap(AssetLoader.Open(new Uri("avares://" + name + "/Assets/Images/bg_r6.png"))), 
			};
		}
		catch (Exception)
		{
			return null;
		}
	}

	private static List<LayoutModeOption> BuildModeOptions(MainViewModel vm)
	{
		return (from kvp in MainViewModel.GetVitureLayoutModes(vm.VitureDeviceType, vm.MonitorCount, vm.TurnOffBuildInScreen)
			select new LayoutModeOption(GetVitureLayoutModeName(kvp.Key), kvp.Key, kvp.Value)).ToList();
	}

	private static string GetVitureLayoutModeName(VitureLayoutMode mode)
	{
		switch (mode)
		{
		case VitureLayoutMode.Horizontal1:
		case VitureLayoutMode.Horizontal1A:
			return Resources.SingleDisplay;
		case VitureLayoutMode.Horizontal2:
		case VitureLayoutMode.Horizontal2A:
			return Resources.TwoDisplaysSBS;
		case VitureLayoutMode.Horizontal3:
		case VitureLayoutMode.Horizontal3A:
			return Resources.ThreeDisplaysSBS;
		case VitureLayoutMode.Vertical3:
			return Resources.ThreeStackedDisplays;
		case VitureLayoutMode.UltraWide:
		case VitureLayoutMode.UltraWideA:
			return Resources.UltraWide;
		case VitureLayoutMode.HorizontalPortrait:
			return Resources.PortraitLandscapePortrait;
		default:
			return mode.ToString();
		}
	}

	private static string GetLayoutTypeName(LayoutType layoutType)
	{
		return layoutType switch
		{
			LayoutType.Mirror => Resources.MirrorDisplays, 
			LayoutType.Extend => Resources.ExtendDesktop, 
			_ => layoutType.ToString(), 
		};
	}
}
