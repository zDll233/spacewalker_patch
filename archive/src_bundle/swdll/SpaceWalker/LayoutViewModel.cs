using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Disposables.Fluent;
using System.Reactive.Linq;
using System.Threading.Tasks;
using ReactiveUI;
using ReactiveUI.Avalonia;
using SpaceWalker.Assets.Languages;
using SpaceWalker.ViewModels;
using VitureCommonLibrary;

namespace SpaceWalker;

public class LayoutViewModel : ReactiveObject, IActivatableViewModel
{
	private bool enableLayoutType = true;

	private string? layoutTypeDisabledReason;

	private string launchButtonText;

	private bool _show6Layout;

	private bool showRefreshRate = true;

	private string _ultraWideRatio = "24:9";

	public ViewModelActivator Activator { get; } = new ViewModelActivator();


	public MainViewModel ViewModel { get; }

	public bool EnableLayoutType
	{
		get
		{
			return enableLayoutType;
		}
		set
		{
			this.RaiseAndSetIfChanged(ref enableLayoutType, value, "EnableLayoutType");
		}
	}

	public string? LayoutTypeDisabledReason
	{
		get
		{
			return layoutTypeDisabledReason;
		}
		set
		{
			this.RaiseAndSetIfChanged(ref layoutTypeDisabledReason, value, "LayoutTypeDisabledReason");
		}
	}

	public string LaunchButtonText
	{
		get
		{
			return launchButtonText;
		}
		set
		{
			this.RaiseAndSetIfChanged(ref launchButtonText, value, "LaunchButtonText");
		}
	}

	public bool Show6Layout
	{
		get
		{
			return _show6Layout;
		}
		set
		{
			this.RaiseAndSetIfChanged(ref _show6Layout, value, "Show6Layout");
		}
	}

	public bool ShowRefreshRate
	{
		get
		{
			return showRefreshRate;
		}
		set
		{
			this.RaiseAndSetIfChanged(ref showRefreshRate, value, "ShowRefreshRate");
		}
	}

	public string UltraWideRatio
	{
		get
		{
			return _ultraWideRatio;
		}
		set
		{
			this.RaiseAndSetIfChanged(ref _ultraWideRatio, value, "UltraWideRatio");
		}
	}

	public LayoutViewModel(MainViewModel vm)
	{
		MainViewModel vm2 = vm;
		base._002Ector();
		LayoutViewModel layoutViewModel = this;
		ViewModel = vm2;
		AvaloniaScheduler sync = AvaloniaScheduler.Instance;
		this.WhenActivated(delegate(CompositeDisposable disposables)
		{
			layoutViewModel.Show6Layout = !vm2.IsNative3DofMode();
			layoutViewModel.ShowRefreshRate = !vm2.IsNative3DofMode() && vm2.SupportHighRefreshRate;
			vm2.WhenAnyValue((MainViewModel x) => x.VitureLayoutMode, (MainViewModel x) => x.MonitorCount, (MainViewModel x) => x.TurnOffBuildInScreen).ObserveOn(sync).Subscribe(delegate((VitureLayoutMode, int, bool) x)
			{
				var (key, num, flag) = x;
				if (MainViewModel.GetVitureLayoutModes(vm2.VitureDeviceType, num, flag).TryGetValue(key, out IReadOnlyList<LayoutType> value) && value.Count == 1)
				{
					vm2.LayoutType = value.First();
					layoutViewModel.EnableLayoutType = false;
					layoutViewModel.LayoutTypeDisabledReason = (flag ? Resources.LayoutTypeDisabledTurnOffBuiltIn : ((num == 0) ? Resources.LayoutTypeDisabledNoMonitor : ((value[0] == LayoutType.Extend) ? Resources.LayoutTypeDisabledExtendOnly : Resources.LayoutTypeDisabledMirrorOnly)));
				}
				else
				{
					layoutViewModel.EnableLayoutType = true;
					layoutViewModel.LayoutTypeDisabledReason = null;
				}
			})
				.DisposeWith(disposables);
			(from t in vm2.WhenAnyValue((MainViewModel x) => x.VitureDeviceType)
				where t == VitureDeviceType.R6
				select t).ObserveOn(sync).Subscribe(delegate
			{
				vm2.FrameRate = 60;
			}).DisposeWith(disposables);
			(from x in vm2.WhenAnyValue((MainViewModel x) => x.FrameRate, (MainViewModel x) => x.SupportHighRefreshRate).Skip(1)
				where vm2.VitureDeviceType < VitureDeviceType.P6 && !x.Item2 && (x.Item1 == 120 || x.Item1 == 90)
				select x).ObserveOn(sync).Select((Func<(int, bool), Task>)async delegate
			{
				vm2.FrameRate = 60;
				await vm2.ShowUpdateGlassesDialogAsync();
			}).Subscribe()
				.DisposeWith(disposables);
			vm2.WhenAnyValue((MainViewModel x) => x.ViewAppRunning).ObserveOn(sync).Subscribe(delegate(bool running)
			{
				if (running)
				{
					layoutViewModel.LaunchButtonText = Resources.ChangeSpaceWalkerLayout;
				}
				else
				{
					layoutViewModel.LaunchButtonText = Resources.LaunchSpaceWalker;
				}
			})
				.DisposeWith(disposables);
			vm2.WhenAnyValue((MainViewModel x) => x.UseUltraWideSize).ObserveOn(sync).Subscribe(delegate(bool u)
			{
				layoutViewModel.UltraWideRatio = (u ? "32:9" : "24:9");
			})
				.DisposeWith(disposables);
			(from x in vm2.WhenAnyValue((MainViewModel x) => x.ViewAppRunning, (MainViewModel x) => x.VitureCommConnect)
				where x.Item1 && x.Item2
				select x).ObserveOn(sync).Select((Func<(bool, bool), Task>)async delegate
			{
				DesktopView page = new DesktopView
				{
					DataContext = new DesktopViewModel(vm2)
				};
				await vm2.NavigationRouter.ReplaceAsync(page, null);
			}).Subscribe()
				.DisposeWith(disposables);
		});
	}
}
