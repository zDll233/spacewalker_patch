using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Markup.Xaml.XamlIl.Runtime;
using Avalonia.Media.Immutable;
using CompiledAvaloniaXaml;
using SpaceWalker.Assets.Languages;

namespace SpaceWalker.Views;

public class LoadingWindow : Window
{
	[CompilerGenerated]
	private static Action<object> _0021XamlIlPopulateOverride;

	public LoadingWindow()
	{
		InitializeComponent();
	}

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "12.0.3.0")]
	[ExcludeFromCodeCoverage]
	public void InitializeComponent(bool loadXaml = true)
	{
		if (loadXaml)
		{
			_0021XamlIlPopulateTrampoline(this);
		}
	}

	[CompilerGenerated]
	private static void _0021XamlIlPopulate(IServiceProvider P_0, LoadingWindow P_1)
	{
		CompiledAvaloniaXaml.XamlIlContext.Context<LoadingWindow> context = new CompiledAvaloniaXaml.XamlIlContext.Context<LoadingWindow>(P_0, new object[1] { _0021AvaloniaResources.NamespaceInfo_003A_002FViews_002FLoadingWindow_002Eaxaml.Singleton }, "avares://SpaceWalker/Views/LoadingWindow.axaml")
		{
			RootObject = P_1,
			IntermediateRoot = P_1
		};
		((ISupportInitialize)P_1).BeginInit();
		P_1.Title = "SpaceWalker.UnityLoading";
		P_1.WindowDecorations = WindowDecorations.BorderOnly;
		P_1.ExtendClientAreaToDecorationsHint = true;
		P_1.ShowInTaskbar = false;
		P_1.Topmost = true;
		P_1.WindowState = WindowState.Maximized;
		Grid grid;
		Grid grid2 = (grid = new Grid());
		((ISupportInitialize)grid2).BeginInit();
		P_1.Content = grid2;
		grid.Background = new ImmutableSolidColorBrush(4281022539u);
		Avalonia.Controls.Controls children = grid.Children;
		StackPanel stackPanel;
		StackPanel stackPanel2 = (stackPanel = new StackPanel());
		((ISupportInitialize)stackPanel2).BeginInit();
		children.Add(stackPanel2);
		stackPanel.HorizontalAlignment = HorizontalAlignment.Center;
		stackPanel.VerticalAlignment = VerticalAlignment.Center;
		Avalonia.Controls.Controls children2 = stackPanel.Children;
		ProgressBar progressBar;
		ProgressBar progressBar2 = (progressBar = new ProgressBar());
		((ISupportInitialize)progressBar2).BeginInit();
		children2.Add(progressBar2);
		progressBar.Width = 100.0;
		progressBar.Height = 100.0;
		progressBar.IsIndeterminate = true;
		((ISupportInitialize)progressBar).EndInit();
		Avalonia.Controls.Controls children3 = stackPanel.Children;
		TextBlock textBlock;
		TextBlock textBlock2 = (textBlock = new TextBlock());
		((ISupportInitialize)textBlock2).BeginInit();
		children3.Add(textBlock2);
		textBlock.Classes.Add("textdisplay");
		textBlock.Margin = new Thickness(10.0, 20.0, 0.0, 0.0);
		textBlock.HorizontalAlignment = HorizontalAlignment.Center;
		textBlock.Foreground = new ImmutableSolidColorBrush(uint.MaxValue);
		textBlock.Text = SpaceWalker.Assets.Languages.Resources.WaitTips;
		((ISupportInitialize)textBlock).EndInit();
		((ISupportInitialize)stackPanel).EndInit();
		((ISupportInitialize)grid).EndInit();
		((ISupportInitialize)P_1).EndInit();
		if (P_1 is StyledElement styled)
		{
			NameScope.SetNameScope(styled, context.AvaloniaNameScope);
		}
		context.AvaloniaNameScope.Complete();
	}

	[CompilerGenerated]
	private static void _0021XamlIlPopulateTrampoline(LoadingWindow P_0)
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
