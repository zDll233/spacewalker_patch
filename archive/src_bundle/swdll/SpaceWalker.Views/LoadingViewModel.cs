using ReactiveUI;
using SpaceWalker.ViewModels;

namespace SpaceWalker.Views;

public class LoadingViewModel : ReactiveObject
{
	public MainViewModel ViewModel { get; }

	public LoadingViewModel(MainViewModel vm)
	{
		ViewModel = vm;
	}
}
