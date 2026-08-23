using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Animation;
using Avalonia.Animation.Easings;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Markup.Xaml.XamlIl.Runtime;
using Avalonia.Media;
using Avalonia.Media.Immutable;
using Avalonia.Styling;
using CompiledAvaloniaXaml;
using SpaceWalker.Assets.Languages;
using SpaceWalker.ViewModels;

namespace SpaceWalker.Views;

public class LoadingView : ContentPage
{
	[CompilerGenerated]
	private static Action<object> _0021XamlIlPopulateOverride;

	public LoadingView()
	{
		InitializeComponent();
	}

	protected override async void OnLoaded(RoutedEventArgs e)
	{
		base.OnLoaded(e);
		if (!(base.DataContext is LoadingViewModel loadingViewModel))
		{
			return;
		}
		MainViewModel mainViewModel = loadingViewModel.ViewModel;
		mainViewModel.StartClientUpdateCheckInBackground();
		if (mainViewModel.NavigationRouter != null)
		{
			await Task.Run(() => mainViewModel.Initialize());
			await mainViewModel.NavigationRouter.PushAsync(new ConnectView
			{
				DataContext = new ConnectViewModel(mainViewModel)
			}, null);
		}
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
	private static void _0021XamlIlPopulate(IServiceProvider P_0, LoadingView P_1)
	{
		CompiledAvaloniaXaml.XamlIlContext.Context<LoadingView> context = new CompiledAvaloniaXaml.XamlIlContext.Context<LoadingView>(P_0, new object[1] { _0021AvaloniaResources.NamespaceInfo_003A_002FViews_002FLoadingView_002Eaxaml.Singleton }, "avares://SpaceWalker/Views/LoadingView.axaml")
		{
			RootObject = P_1,
			IntermediateRoot = P_1
		};
		((ISupportInitialize)P_1).BeginInit();
		NavigationPage.SetHasNavigationBar(P_1, value: false);
		P_1.Background = new ImmutableSolidColorBrush(16777215u);
		StackPanel stackPanel;
		StackPanel stackPanel2 = (stackPanel = new StackPanel());
		((ISupportInitialize)stackPanel2).BeginInit();
		P_1.Content = stackPanel2;
		stackPanel.HorizontalAlignment = HorizontalAlignment.Center;
		stackPanel.VerticalAlignment = VerticalAlignment.Center;
		Avalonia.Controls.Controls children = stackPanel.Children;
		Grid grid;
		Grid grid2 = (grid = new Grid());
		((ISupportInitialize)grid2).BeginInit();
		children.Add(grid2);
		grid.Width = 100.0;
		grid.Height = 100.0;
		Avalonia.Controls.Controls children2 = grid.Children;
		Ellipse ellipse;
		Ellipse ellipse2 = (ellipse = new Ellipse());
		((ISupportInitialize)ellipse2).BeginInit();
		children2.Add(ellipse2);
		ellipse.Stroke = new ImmutableSolidColorBrush(536870911u);
		ellipse.StrokeThickness = 6.0;
		ellipse.Margin = new Thickness(6.0, 6.0, 6.0, 6.0);
		((ISupportInitialize)ellipse).EndInit();
		Avalonia.Controls.Controls children3 = grid.Children;
		Arc arc;
		Arc arc2 = (arc = new Arc());
		((ISupportInitialize)arc2).BeginInit();
		children3.Add(arc2);
		arc.StartAngle = 0.0;
		arc.SweepAngle = 270.0;
		arc.StrokeThickness = 6.0;
		arc.StrokeLineCap = PenLineCap.Round;
		arc.Margin = new Thickness(6.0, 6.0, 6.0, 6.0);
		Styles styles = arc.Styles;
		Style style = new Style();
		style.Selector = ((Selector?)null).OfType(typeof(Arc));
		IList<IAnimation> animations = style.Animations;
		Animation animation = new Animation();
		animation.Duration = TimeSpan.FromTicks(12000000L);
		animation.IterationCount = IterationCount.Parse("INFINITE");
		animation.Easing = Easing.Parse("LinearEasing");
		KeyFrames children4 = animation.Children;
		KeyFrame keyFrame = new KeyFrame();
		keyFrame.Cue = Cue.Parse("0%", CultureInfo.InvariantCulture);
		AvaloniaList<IAnimationSetter> setters = keyFrame.Setters;
		Setter setter = new Setter();
		setter.Property = Arc.StartAngleProperty;
		setter.Value = 0.0;
		setters.Add(setter);
		children4.Add(keyFrame);
		KeyFrames children5 = animation.Children;
		KeyFrame keyFrame2 = new KeyFrame();
		keyFrame2.Cue = Cue.Parse("100%", CultureInfo.InvariantCulture);
		AvaloniaList<IAnimationSetter> setters2 = keyFrame2.Setters;
		Setter setter2 = new Setter();
		setter2.Property = Arc.StartAngleProperty;
		setter2.Value = 360.0;
		setters2.Add(setter2);
		children5.Add(keyFrame2);
		animations.Add(animation);
		styles.Add(style);
		LinearGradientBrush linearGradientBrush = new LinearGradientBrush();
		linearGradientBrush.StartPoint = new RelativePoint(0.0, 0.5, RelativeUnit.Relative);
		linearGradientBrush.EndPoint = new RelativePoint(1.0, 0.5, RelativeUnit.Relative);
		GradientStops gradientStops = linearGradientBrush.GradientStops;
		GradientStop gradientStop = new GradientStop();
		gradientStop.Color = Color.FromUInt32(16777215u);
		gradientStop.Offset = 0.0;
		gradientStops.Add(gradientStop);
		GradientStops gradientStops2 = linearGradientBrush.GradientStops;
		GradientStop gradientStop2 = new GradientStop();
		gradientStop2.Color = Color.FromUInt32(uint.MaxValue);
		gradientStop2.Offset = 1.0;
		gradientStops2.Add(gradientStop2);
		arc.Stroke = linearGradientBrush;
		arc.Effect = new BlurEffect
		{
			Radius = 0.5
		};
		((ISupportInitialize)arc).EndInit();
		((ISupportInitialize)grid).EndInit();
		Avalonia.Controls.Controls children6 = stackPanel.Children;
		TextBlock textBlock;
		TextBlock textBlock2 = (textBlock = new TextBlock());
		((ISupportInitialize)textBlock2).BeginInit();
		children6.Add(textBlock2);
		textBlock.Classes.Add("base");
		textBlock.Classes.Add("textdisplay");
		textBlock.Margin = new Thickness(10.0, 20.0, 0.0, 0.0);
		textBlock.HorizontalAlignment = HorizontalAlignment.Center;
		textBlock.Text = SpaceWalker.Assets.Languages.Resources.WaitTips;
		((ISupportInitialize)textBlock).EndInit();
		((ISupportInitialize)stackPanel).EndInit();
		((ISupportInitialize)P_1).EndInit();
		if (P_1 is StyledElement styled)
		{
			NameScope.SetNameScope(styled, context.AvaloniaNameScope);
		}
		context.AvaloniaNameScope.Complete();
	}

	[CompilerGenerated]
	private static void _0021XamlIlPopulateTrampoline(LoadingView P_0)
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
