using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Avalonia.Markup.Xaml.XamlIl.Runtime;
using SpaceWalker;
using SpaceWalker.Views;

namespace CompiledAvaloniaXaml;

[EditorBrowsable(EditorBrowsableState.Never)]
[CompilerGenerated]
public class _0021XamlLoader
{
	public static object TryLoad(IServiceProvider P_0, string P_1)
	{
		if (string.Equals(P_1, "avares://SpaceWalker/App.axaml", StringComparison.OrdinalIgnoreCase))
		{
			return new App();
		}
		if (string.Equals(P_1, "avares://SpaceWalker/Controls/Button.axaml", StringComparison.OrdinalIgnoreCase))
		{
			return _0021AvaloniaResources.Build_003A_002FControls_002FButton_002Eaxaml(XamlIlRuntimeHelpers.CreateRootServiceProviderV3(P_0));
		}
		if (string.Equals(P_1, "avares://SpaceWalker/Controls/CheckBox.axaml", StringComparison.OrdinalIgnoreCase))
		{
			return _0021AvaloniaResources.Build_003A_002FControls_002FCheckBox_002Eaxaml(XamlIlRuntimeHelpers.CreateRootServiceProviderV3(P_0));
		}
		if (string.Equals(P_1, "avares://SpaceWalker/Controls/ComboBox.axaml", StringComparison.OrdinalIgnoreCase))
		{
			return _0021AvaloniaResources.Build_003A_002FControls_002FComboBox_002Eaxaml(XamlIlRuntimeHelpers.CreateRootServiceProviderV3(P_0));
		}
		if (string.Equals(P_1, "avares://SpaceWalker/Controls/ContentDialog.axaml", StringComparison.OrdinalIgnoreCase))
		{
			return _0021AvaloniaResources.Build_003A_002FControls_002FContentDialog_002Eaxaml(XamlIlRuntimeHelpers.CreateRootServiceProviderV3(P_0));
		}
		if (string.Equals(P_1, "avares://SpaceWalker/Controls/IconLabel.axaml", StringComparison.OrdinalIgnoreCase))
		{
			return _0021AvaloniaResources.Build_003A_002FControls_002FIconLabel_002Eaxaml(XamlIlRuntimeHelpers.CreateRootServiceProviderV3(P_0));
		}
		if (string.Equals(P_1, "avares://SpaceWalker/Controls/ListBox.axaml", StringComparison.OrdinalIgnoreCase))
		{
			return _0021AvaloniaResources.Build_003A_002FControls_002FListBox_002Eaxaml(XamlIlRuntimeHelpers.CreateRootServiceProviderV3(P_0));
		}
		if (string.Equals(P_1, "avares://SpaceWalker/Controls/RadioButton.axaml", StringComparison.OrdinalIgnoreCase))
		{
			return _0021AvaloniaResources.Build_003A_002FControls_002FRadioButton_002Eaxaml(XamlIlRuntimeHelpers.CreateRootServiceProviderV3(P_0));
		}
		if (string.Equals(P_1, "avares://SpaceWalker/Controls/ScrollViewer.axaml", StringComparison.OrdinalIgnoreCase))
		{
			return _0021AvaloniaResources.Build_003A_002FControls_002FScrollViewer_002Eaxaml(XamlIlRuntimeHelpers.CreateRootServiceProviderV3(P_0));
		}
		if (string.Equals(P_1, "avares://SpaceWalker/Controls/Slider.axaml", StringComparison.OrdinalIgnoreCase))
		{
			return _0021AvaloniaResources.Build_003A_002FControls_002FSlider_002Eaxaml(XamlIlRuntimeHelpers.CreateRootServiceProviderV3(P_0));
		}
		if (string.Equals(P_1, "avares://SpaceWalker/Controls/Styles.axaml", StringComparison.OrdinalIgnoreCase))
		{
			return _0021AvaloniaResources.Build_003A_002FControls_002FStyles_002Eaxaml(XamlIlRuntimeHelpers.CreateRootServiceProviderV3(P_0));
		}
		if (string.Equals(P_1, "avares://SpaceWalker/Controls/TabControl.axaml", StringComparison.OrdinalIgnoreCase))
		{
			return _0021AvaloniaResources.Build_003A_002FControls_002FTabControl_002Eaxaml(XamlIlRuntimeHelpers.CreateRootServiceProviderV3(P_0));
		}
		if (string.Equals(P_1, "avares://SpaceWalker/Controls/TextStyles.axaml", StringComparison.OrdinalIgnoreCase))
		{
			return _0021AvaloniaResources.Build_003A_002FControls_002FTextStyles_002Eaxaml(XamlIlRuntimeHelpers.CreateRootServiceProviderV3(P_0));
		}
		if (string.Equals(P_1, "avares://SpaceWalker/Controls/ToggleSwitch.axaml", StringComparison.OrdinalIgnoreCase))
		{
			return _0021AvaloniaResources.Build_003A_002FControls_002FToggleSwitch_002Eaxaml(XamlIlRuntimeHelpers.CreateRootServiceProviderV3(P_0));
		}
		if (string.Equals(P_1, "avares://SpaceWalker/Themes/Colors.axaml", StringComparison.OrdinalIgnoreCase))
		{
			return _0021AvaloniaResources.Build_003A_002FThemes_002FColors_002Eaxaml(XamlIlRuntimeHelpers.CreateRootServiceProviderV3(P_0));
		}
		if (string.Equals(P_1, "avares://SpaceWalker/Views/ConnectView.axaml", StringComparison.OrdinalIgnoreCase))
		{
			return new ConnectView();
		}
		if (string.Equals(P_1, "avares://SpaceWalker/Views/DesktopView.axaml", StringComparison.OrdinalIgnoreCase))
		{
			return new DesktopView();
		}
		if (string.Equals(P_1, "avares://SpaceWalker/Views/LayoutView.axaml", StringComparison.OrdinalIgnoreCase))
		{
			return new LayoutView();
		}
		if (string.Equals(P_1, "avares://SpaceWalker/Views/LoadingView.axaml", StringComparison.OrdinalIgnoreCase))
		{
			return new LoadingView();
		}
		if (string.Equals(P_1, "avares://SpaceWalker/Views/LoadingWindow.axaml", StringComparison.OrdinalIgnoreCase))
		{
			return new LoadingWindow();
		}
		if (string.Equals(P_1, "avares://SpaceWalker/Views/MainWindow2.axaml", StringComparison.OrdinalIgnoreCase))
		{
			return new MainWindow2();
		}
		if (string.Equals(P_1, "avares://SpaceWalker/Views/SettingsView.axaml", StringComparison.OrdinalIgnoreCase))
		{
			return new SettingsView();
		}
		return null;
	}

	public static object TryLoad(string P_0)
	{
		return TryLoad(null, P_0);
	}
}
