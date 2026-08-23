using System;
using System.Reactive.Disposables;
using System.Reactive.Disposables.Fluent;
using System.Reactive.Linq;
using ReactiveUI;
using ReactiveUI.Avalonia;
using SpaceWalker.Assets.Languages;
using SpaceWalker.ViewModels;

namespace SpaceWalker;

public class ConnectViewModel : ReactiveObject, IActivatableViewModel
{
	private string _titleText = Resources.ConnectXRGlasses;

	private string _tipsText = Resources.ConnectTips;

	public ViewModelActivator Activator { get; } = new ViewModelActivator();


	public MainViewModel ViewModel { get; }

	public string TitleText
	{
		get
		{
			return _titleText;
		}
		private set
		{
			this.RaiseAndSetIfChanged(ref _titleText, value, "TitleText");
		}
	}

	public string TipsText
	{
		get
		{
			return _tipsText;
		}
		private set
		{
			this.RaiseAndSetIfChanged(ref _tipsText, value, "TipsText");
		}
	}

	public ConnectViewModel(MainViewModel vm)
	{
		MainViewModel vm2 = vm;
		base._002Ector();
		ConnectViewModel connectViewModel = this;
		ViewModel = vm2;
		this.WhenActivated(delegate(CompositeDisposable disposables)
		{
			vm2.WhenAnyValue((MainViewModel x) => x.IsBusy).ObserveOn(AvaloniaScheduler.Instance).Subscribe(delegate(bool busy)
			{
				connectViewModel.TitleText = (busy ? Resources.ConnectXRGlasses2 : Resources.ConnectXRGlasses);
				connectViewModel.TipsText = (busy ? Resources.ConnectTips2 : Resources.ConnectTips);
			})
				.DisposeWith(disposables);
		});
	}
}
