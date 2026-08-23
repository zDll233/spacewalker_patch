using Avalonia;
using Avalonia.Styling;
using ReactiveUI;
using SpaceWalker.Database;

namespace SpaceWalker.Helper;

public class ThemeManager : ReactiveObject
{
	public static readonly ThemeVariant PhantomBladeZeroVariant = new ThemeVariant("PhantomBladeZero", ThemeVariant.Light);

	private AppTheme _currentTheme;

	public static ThemeManager Instance { get; } = new ThemeManager();


	public AppTheme CurrentTheme
	{
		get
		{
			return _currentTheme;
		}
		private set
		{
			this.RaiseAndSetIfChanged(ref _currentTheme, value, "CurrentTheme");
			this.RaisePropertyChanged("IsPbz");
		}
	}

	public bool IsPbz => _currentTheme == AppTheme.PhantomBladeZero;

	private ThemeManager()
	{
	}

	public void ApplySavedTheme()
	{
		AppTheme theme = ((DbManager.Instance.Settings.Theme == "PhantomBladeZero") ? AppTheme.PhantomBladeZero : AppTheme.Default);
		Apply(theme, persist: false);
	}

	public void Apply(AppTheme theme, bool persist = true)
	{
		Application current = Application.Current;
		if (current != null)
		{
			current.RequestedThemeVariant = ((theme == AppTheme.PhantomBladeZero) ? PhantomBladeZeroVariant : ThemeVariant.Light);
		}
		CurrentTheme = theme;
		if (persist)
		{
			DbManager.Instance.Settings.Theme = theme.ToString();
			DbManager.Instance.SaveSettings();
		}
	}
}
