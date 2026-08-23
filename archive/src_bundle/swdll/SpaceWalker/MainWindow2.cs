using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reactive.Disposables;
using System.Reactive.Disposables.Fluent;
using System.Runtime.CompilerServices;
using Avalonia;
using Avalonia.Animation;
using Avalonia.Automation;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls.Chrome;
using Avalonia.Controls.Presenters;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Labs.Controls;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.Converters;
using Avalonia.Markup.Xaml.MarkupExtensions;
using Avalonia.Markup.Xaml.Templates;
using Avalonia.Markup.Xaml.XamlIl.Runtime;
using Avalonia.Media;
using Avalonia.Media.Immutable;
using Avalonia.Platform;
using Avalonia.Styling;
using Avalonia.Svg.Skia;
using Avalonia.Threading;
using CompiledAvaloniaXaml;
using ReactiveUI;
using SpaceWalker.Helper;
using SpaceWalker.ViewModels;
using SpaceWalker.Views;

namespace SpaceWalker;

public class MainWindow2 : Window, IActivatableView
{
	[CompilerGenerated]
	private class XamlClosure_15
	{
		public unsafe static object Build_1(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<MainWindow2> provider = CreateContext(P_0);
			ControlTheme controlTheme = new ControlTheme();
			controlTheme.TargetType = typeof(Button);
			Setter setter = new Setter();
			setter.Property = Layoutable.WidthProperty;
			setter.Value = 40.0;
			controlTheme.Add(setter);
			Setter setter2 = new Setter();
			setter2.Property = Layoutable.HeightProperty;
			setter2.Value = 40.0;
			controlTheme.Add(setter2);
			Setter setter3 = new Setter();
			setter3.Property = TemplatedControl.BackgroundProperty;
			setter3.Value = new ImmutableSolidColorBrush(16777215u);
			controlTheme.Add(setter3);
			Setter setter4 = new Setter();
			setter4.Property = TemplatedControl.CornerRadiusProperty;
			setter4.Value = new CornerRadius(4.0, 4.0, 4.0, 4.0);
			controlTheme.Add(setter4);
			Setter setter5 = new Setter();
			setter5.Property = TemplatedControl.PaddingProperty;
			setter5.Value = new Thickness(0.0, 0.0, 0.0, 0.0);
			controlTheme.Add(setter5);
			Setter setter6 = new Setter();
			setter6.Property = ContentControl.HorizontalContentAlignmentProperty;
			setter6.Value = HorizontalAlignment.Center;
			controlTheme.Add(setter6);
			Setter setter7 = new Setter();
			setter7.Property = ContentControl.VerticalContentAlignmentProperty;
			setter7.Value = VerticalAlignment.Center;
			controlTheme.Add(setter7);
			Setter setter8 = new Setter();
			setter8.Property = InputElement.CursorProperty;
			setter8.Value = new Cursor(StandardCursorType.Hand);
			controlTheme.Add(setter8);
			Setter setter9 = new Setter();
			setter9.Property = TemplatedControl.TemplateProperty;
			setter9.Value = new ControlTemplate
			{
				Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&Build_2), provider)
			};
			controlTheme.Add(setter9);
			Style style = new Style();
			style.Selector = ((Selector?)null).Nesting().Class(":pointerover").Template()
				.OfType(typeof(Border))
				.Name("PART_Border");
			Setter setter10 = new Setter();
			setter10.Property = Border.BackgroundProperty;
			setter10.Value = new ImmutableSolidColorBrush(335544320u);
			style.Add(setter10);
			controlTheme.Add(style);
			Style style2 = new Style();
			style2.Selector = ((Selector?)null).Nesting().Class(":pressed").Template()
				.OfType(typeof(Border))
				.Name("PART_Border");
			Setter setter11 = new Setter();
			setter11.Property = Border.BackgroundProperty;
			setter11.Value = new ImmutableSolidColorBrush(520093696u);
			style2.Add(setter11);
			controlTheme.Add(style2);
			return controlTheme;
		}

		public static CompiledAvaloniaXaml.XamlIlContext.Context<MainWindow2> CreateContext(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<MainWindow2> context = new CompiledAvaloniaXaml.XamlIlContext.Context<MainWindow2>(P_0, new object[1] { _0021AvaloniaResources.NamespaceInfo_003A_002FViews_002FMainWindow2_002Eaxaml.Singleton }, "avares://SpaceWalker/Views/MainWindow2.axaml");
			if (P_0 != null)
			{
				object service = P_0.GetService(typeof(IRootObjectProvider));
				if (service != null)
				{
					service = ((IRootObjectProvider)service).RootObject;
					context.RootObject = (MainWindow2)service;
				}
			}
			return context;
		}

		public static object Build_2(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<MainWindow2> context = CreateContext(P_0);
			context.IntermediateRoot = new Border();
			object intermediateRoot = context.IntermediateRoot;
			((ISupportInitialize)intermediateRoot).BeginInit();
			((StyledElement)intermediateRoot).Name = "PART_Border";
			object element = intermediateRoot;
			context.AvaloniaNameScope.Register("PART_Border", element);
			((AvaloniaObject)intermediateRoot).Bind((AvaloniaProperty)Border.BackgroundProperty, new TemplateBinding(TemplatedControl.BackgroundProperty).ProvideValue());
			((AvaloniaObject)intermediateRoot).Bind((AvaloniaProperty)Border.CornerRadiusProperty, new TemplateBinding(TemplatedControl.CornerRadiusProperty).ProvideValue());
			StyledProperty<Transitions?> transitionsProperty = Animatable.TransitionsProperty;
			Transitions transitions = new Transitions();
			BrushTransition brushTransition = new BrushTransition();
			brushTransition.Property = Border.BackgroundProperty;
			brushTransition.Duration = TimeSpan.FromTicks(1500000L);
			transitions.Add(brushTransition);
			((AvaloniaObject)intermediateRoot).SetValue(transitionsProperty, transitions, BindingPriority.Template);
			ContentPresenter contentPresenter;
			ContentPresenter contentPresenter2 = (contentPresenter = new ContentPresenter());
			((ISupportInitialize)contentPresenter2).BeginInit();
			((Decorator)intermediateRoot).Child = contentPresenter2;
			CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(contentPresenter, BindingPriority.Template, new TemplateBinding(ContentControl.ContentProperty).ProvideValue());
			contentPresenter.Bind(ContentPresenter.ContentTemplateProperty, new TemplateBinding(ContentControl.ContentTemplateProperty).ProvideValue());
			contentPresenter.Bind(Layoutable.HorizontalAlignmentProperty, new TemplateBinding(ContentControl.HorizontalContentAlignmentProperty).ProvideValue());
			contentPresenter.Bind(Layoutable.VerticalAlignmentProperty, new TemplateBinding(ContentControl.VerticalContentAlignmentProperty).ProvideValue());
			((ISupportInitialize)contentPresenter).EndInit();
			((ISupportInitialize)intermediateRoot).EndInit();
			return intermediateRoot;
		}
	}

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "12.0.3.0")]
	internal Button SettingsButton;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "12.0.3.0")]
	internal Button MinimizeButton;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "12.0.3.0")]
	internal Button CloseButton;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "12.0.3.0")]
	internal NavigationPage PartNavigation;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "12.0.3.0")]
	internal ProgressBar PartProgressBar;

	[CompilerGenerated]
	private static Action<object> _0021XamlIlPopulateOverride;

	public MainWindow2()
	{
		InitializeComponent();
		MinimizeButton.Click += delegate
		{
			base.WindowState = WindowState.Minimized;
		};
		CloseButton.Click += delegate
		{
			if (base.DataContext is MainViewModel mainViewModel)
			{
				mainViewModel.ExitAppCmd.Execute().Subscribe();
			}
			else
			{
				Close();
			}
		};
		SettingsButton.Click += OnSettingsClick;
		this.WhenActivated(delegate(CompositeDisposable d)
		{
			if (base.DataContext is IActivatableViewModel activatableViewModel)
			{
				activatableViewModel.Activator.Activate().DisposeWith(d);
			}
		});
	}

	protected override void OnOpened(EventArgs e)
	{
		base.OnOpened(e);
		CenterOnPrimaryDisplay();
	}

	protected override void OnKeyDown(KeyEventArgs e)
	{
		base.OnKeyDown(e);
		if (!e.Handled && e.Key == Key.Escape)
		{
			OverlayLayer? overlayLayer = OverlayLayer.GetOverlayLayer(this);
			if ((overlayLayer == null || overlayLayer.Children.Count <= 0) && base.DataContext is MainViewModel mainViewModel)
			{
				e.Handled = true;
				mainViewModel.ExitAppCmd.Execute().Subscribe();
			}
		}
	}

	protected override void OnLoaded(RoutedEventArgs e)
	{
		base.OnLoaded(e);
		if (base.DataContext is MainViewModel mainViewModel)
		{
			mainViewModel.NavigationRouter = PartNavigation;
			LoadingView page = new LoadingView
			{
				DataContext = new LoadingViewModel(mainViewModel)
			};
			mainViewModel.NavigationRouter.ReplaceAsync(page, null);
		}
	}

	protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
	{
		base.OnPropertyChanged(change);
		if (change.Property == Window.WindowStateProperty && change.GetOldValue<WindowState>() == WindowState.Minimized && change.GetNewValue<WindowState>() != WindowState.Minimized)
		{
			CenterOnPrimaryDisplay();
		}
	}

	public void CenterOnPrimaryDisplay()
	{
		Screen primary = base.Screens.Primary;
		if (!(primary == null))
		{
			PixelSize pixelSize = PixelSize.FromSize(base.FrameSize ?? new Size(base.Width, base.Height), primary.Scaling);
			PixelRect workingArea = primary.WorkingArea;
			base.Position = new PixelPoint(workingArea.X + (workingArea.Width - pixelSize.Width) / 2, workingArea.Y + (workingArea.Height - pixelSize.Height) / 2);
		}
	}

	public static void CenterMainWindowOnPrimary()
	{
		Avalonia.Threading.Dispatcher.UIThread.InvokeAsync(delegate
		{
			if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime classicDesktopStyleApplicationLifetime)
			{
				(classicDesktopStyleApplicationLifetime.MainWindow as MainWindow2)?.CenterOnPrimaryDisplay();
			}
		});
	}

	private async void OnSettingsClick(object? sender, RoutedEventArgs e)
	{
		try
		{
			if (base.DataContext is MainViewModel vm)
			{
				await new ContentDialog
				{
					Classes = { "dialog" },
					Content = new SettingsView
					{
						DataContext = new SettingsViewModel(vm)
					}
				}.ShowWithScrimDismissAsync(this);
			}
		}
		catch (Exception exception)
		{
			MainViewModel.Logger.Error(exception, "OnSettingsClick: show settings dialog failed");
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
		INameScope nameScope = this.FindNameScope();
		SettingsButton = nameScope?.Find<Button>("SettingsButton");
		MinimizeButton = nameScope?.Find<Button>("MinimizeButton");
		CloseButton = nameScope?.Find<Button>("CloseButton");
		PartNavigation = nameScope?.Find<NavigationPage>("PartNavigation");
		PartProgressBar = nameScope?.Find<ProgressBar>("PartProgressBar");
	}

	[CompilerGenerated]
	private unsafe static void _0021XamlIlPopulate(IServiceProvider P_0, MainWindow2 P_1)
	{
		CompiledAvaloniaXaml.XamlIlContext.Context<MainWindow2> context = new CompiledAvaloniaXaml.XamlIlContext.Context<MainWindow2>(P_0, new object[1] { _0021AvaloniaResources.NamespaceInfo_003A_002FViews_002FMainWindow2_002Eaxaml.Singleton }, "avares://SpaceWalker/Views/MainWindow2.axaml")
		{
			RootObject = P_1,
			IntermediateRoot = P_1
		};
		((ISupportInitialize)P_1).BeginInit();
		context.PushParent(P_1);
		P_1.Icon = (WindowIcon)new IconTypeConverter().ConvertFrom(context, CultureInfo.InvariantCulture, "avares://SpaceWalker/Assets/Images/logo.ico");
		P_1.Title = "SpaceWalker";
		P_1.CanResize = false;
		P_1.CanMaximize = false;
		P_1.MinHeight = 568.0;
		P_1.MinWidth = 786.0;
		P_1.Height = 568.0;
		P_1.Width = 786.0;
		P_1.Background = new ImmutableSolidColorBrush(16777215u);
		P_1.TransparencyLevelHint = new WindowTransparencyLevel[1] { WindowTransparencyLevel.AcrylicBlur };
		P_1.ExtendClientAreaToDecorationsHint = true;
		P_1.WindowDecorations = WindowDecorations.BorderOnly;
		((ResourceDictionary)P_1.Resources).AddDeferred((object)"TitleButtonTheme", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_15.Build_1), context));
		Panel panel;
		Panel panel2 = (panel = new Panel());
		((ISupportInitialize)panel2).BeginInit();
		P_1.Content = panel2;
		Panel panel3;
		Panel panel4 = (panel3 = panel);
		context.PushParent(panel3);
		Panel panel5 = panel3;
		Avalonia.Controls.Controls children = panel5.Children;
		ExperimentalAcrylicBorder experimentalAcrylicBorder;
		ExperimentalAcrylicBorder experimentalAcrylicBorder2 = (experimentalAcrylicBorder = new ExperimentalAcrylicBorder());
		((ISupportInitialize)experimentalAcrylicBorder2).BeginInit();
		children.Add(experimentalAcrylicBorder2);
		experimentalAcrylicBorder.IsHitTestVisible = false;
		ExperimentalAcrylicMaterial experimentalAcrylicMaterial = new ExperimentalAcrylicMaterial();
		experimentalAcrylicMaterial.BackgroundSource = AcrylicBackgroundSource.Digger;
		experimentalAcrylicMaterial.TintColor = Color.FromUInt32(4283588208u);
		experimentalAcrylicMaterial.TintOpacity = 0.4;
		experimentalAcrylicMaterial.MaterialOpacity = 0.4;
		experimentalAcrylicBorder.Material = experimentalAcrylicMaterial;
		((ISupportInitialize)experimentalAcrylicBorder).EndInit();
		Avalonia.Controls.Controls children2 = panel5.Children;
		Grid grid;
		Grid grid2 = (grid = new Grid());
		((ISupportInitialize)grid2).BeginInit();
		children2.Add(grid2);
		Grid grid3;
		Grid grid4 = (grid3 = grid);
		context.PushParent(grid3);
		Grid grid5 = grid3;
		RowDefinitions rowDefinitions = new RowDefinitions();
		rowDefinitions.Capacity = 2;
		rowDefinitions.Add(new RowDefinition(new GridLength(48.0, GridUnitType.Pixel)));
		rowDefinitions.Add(new RowDefinition(new GridLength(1.0, GridUnitType.Star)));
		grid5.RowDefinitions = rowDefinitions;
		Avalonia.Controls.Controls children3 = grid5.Children;
		Panel panel6;
		Panel panel7 = (panel6 = new Panel());
		((ISupportInitialize)panel7).BeginInit();
		children3.Add(panel7);
		Panel panel8 = (panel3 = panel6);
		context.PushParent(panel3);
		Panel panel9 = panel3;
		Grid.SetRow(panel9, 0);
		Avalonia.Controls.Controls children4 = panel9.Children;
		Border border;
		Border border2 = (border = new Border());
		((ISupportInitialize)border2).BeginInit();
		children4.Add(border2);
		border.Background = new ImmutableSolidColorBrush(1728053247u);
		border.IsHitTestVisible = false;
		((ISupportInitialize)border).EndInit();
		Avalonia.Controls.Controls children5 = panel9.Children;
		Grid grid6;
		Grid grid7 = (grid6 = new Grid());
		((ISupportInitialize)grid7).BeginInit();
		children5.Add(grid7);
		Grid grid8 = (grid3 = grid6);
		context.PushParent(grid3);
		Grid grid9 = grid3;
		ColumnDefinitions columnDefinitions = new ColumnDefinitions();
		columnDefinitions.Capacity = 3;
		columnDefinitions.Add(new ColumnDefinition(new GridLength(0.0, GridUnitType.Auto)));
		columnDefinitions.Add(new ColumnDefinition(new GridLength(1.0, GridUnitType.Star)));
		columnDefinitions.Add(new ColumnDefinition(new GridLength(0.0, GridUnitType.Auto)));
		grid9.ColumnDefinitions = columnDefinitions;
		Avalonia.Controls.Controls children6 = grid9.Children;
		Border border3;
		Border border4 = (border3 = new Border());
		((ISupportInitialize)border4).BeginInit();
		children6.Add(border4);
		Grid.SetColumn(border3, 0);
		Grid.SetColumnSpan(border3, 2);
		border3.Background = new ImmutableSolidColorBrush(16777215u);
		WindowDecorationProperties.SetElementRole(border3, WindowDecorationsElementRole.TitleBar);
		((ISupportInitialize)border3).EndInit();
		Avalonia.Controls.Controls children7 = grid9.Children;
		StackPanel stackPanel;
		StackPanel stackPanel2 = (stackPanel = new StackPanel());
		((ISupportInitialize)stackPanel2).BeginInit();
		children7.Add(stackPanel2);
		StackPanel stackPanel3;
		StackPanel stackPanel4 = (stackPanel3 = stackPanel);
		context.PushParent(stackPanel3);
		StackPanel stackPanel5 = stackPanel3;
		Grid.SetColumn(stackPanel5, 0);
		stackPanel5.Orientation = Orientation.Horizontal;
		stackPanel5.Spacing = 8.0;
		stackPanel5.VerticalAlignment = VerticalAlignment.Center;
		stackPanel5.Margin = new Thickness(16.0, 0.0, 0.0, 0.0);
		stackPanel5.IsHitTestVisible = false;
		Avalonia.Controls.Controls children8 = stackPanel5.Children;
		Image image;
		Image image2 = (image = new Image());
		((ISupportInitialize)image2).BeginInit();
		children8.Add(image2);
		Image image3;
		Image image4 = (image3 = image);
		context.PushParent(image3);
		Image image5 = image3;
		image5.Height = 30.0;
		image5.Width = 30.0;
		image5.Source = (IImage)new BitmapTypeConverter().ConvertFrom(context, CultureInfo.InvariantCulture, "avares://SpaceWalker/Assets/Images/app_icon.png");
		context.PopParent();
		((ISupportInitialize)image4).EndInit();
		Avalonia.Controls.Controls children9 = stackPanel5.Children;
		TextBlock textBlock;
		TextBlock textBlock2 = (textBlock = new TextBlock());
		((ISupportInitialize)textBlock2).BeginInit();
		children9.Add(textBlock2);
		textBlock.VerticalAlignment = VerticalAlignment.Center;
		textBlock.FontSize = 13.0;
		textBlock.FontWeight = FontWeight.DemiBold;
		textBlock.Foreground = new ImmutableSolidColorBrush(4278979596u);
		textBlock.Opacity = 0.72;
		textBlock.Text = "VITURE SpaceWalker";
		((ISupportInitialize)textBlock).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel4).EndInit();
		Avalonia.Controls.Controls children10 = grid9.Children;
		StackPanel stackPanel6;
		StackPanel stackPanel7 = (stackPanel6 = new StackPanel());
		((ISupportInitialize)stackPanel7).BeginInit();
		children10.Add(stackPanel7);
		StackPanel stackPanel8 = (stackPanel3 = stackPanel6);
		context.PushParent(stackPanel3);
		StackPanel stackPanel9 = stackPanel3;
		Grid.SetColumn(stackPanel9, 2);
		stackPanel9.Orientation = Orientation.Horizontal;
		stackPanel9.Margin = new Thickness(0.0, 4.0, 8.0, 4.0);
		Avalonia.Controls.Controls children11 = stackPanel9.Children;
		Button button;
		Button button2 = (button = new Button());
		((ISupportInitialize)button2).BeginInit();
		children11.Add(button2);
		Button button3;
		Button button4 = (button3 = button);
		context.PushParent(button3);
		Button button5 = button3;
		button5.Name = "SettingsButton";
		object element = button5;
		context.AvaloniaNameScope.Register("SettingsButton", element);
		AutomationProperties.SetName(button5, "Settings");
		StaticResourceExtension staticResourceExtension = new StaticResourceExtension("TitleButtonTheme");
		context.ProvideTargetProperty = StyledElement.ThemeProperty;
		object? obj = staticResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_15(button5, obj);
		Image image6;
		Image image7 = (image6 = new Image());
		((ISupportInitialize)image7).BeginInit();
		button5.Content = image7;
		Image image8 = (image3 = image6);
		context.PushParent(image3);
		Image image9 = image3;
		image9.Width = 24.0;
		image9.Height = 24.0;
		SvgImageExtension svgImageExtension = new SvgImageExtension("/Assets/Images/ic_settings.svg");
		context.ProvideTargetProperty = Image.SourceProperty;
		object obj2 = svgImageExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_11(image9, obj2);
		context.PopParent();
		((ISupportInitialize)image8).EndInit();
		context.PopParent();
		((ISupportInitialize)button4).EndInit();
		Avalonia.Controls.Controls children12 = stackPanel9.Children;
		Button button6;
		Button button7 = (button6 = new Button());
		((ISupportInitialize)button7).BeginInit();
		children12.Add(button7);
		Button button8 = (button3 = button6);
		context.PushParent(button3);
		Button button9 = button3;
		button9.Name = "MinimizeButton";
		element = button9;
		context.AvaloniaNameScope.Register("MinimizeButton", element);
		AutomationProperties.SetName(button9, "Minimize");
		StaticResourceExtension staticResourceExtension2 = new StaticResourceExtension("TitleButtonTheme");
		context.ProvideTargetProperty = StyledElement.ThemeProperty;
		object? obj3 = staticResourceExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_15(button9, obj3);
		Image image10;
		Image image11 = (image10 = new Image());
		((ISupportInitialize)image11).BeginInit();
		button9.Content = image11;
		Image image12 = (image3 = image10);
		context.PushParent(image3);
		Image image13 = image3;
		image13.Width = 30.0;
		image13.Height = 30.0;
		SvgImageExtension svgImageExtension2 = new SvgImageExtension("/Assets/Images/ic_scale.svg");
		context.ProvideTargetProperty = Image.SourceProperty;
		object obj4 = svgImageExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_11(image13, obj4);
		context.PopParent();
		((ISupportInitialize)image12).EndInit();
		context.PopParent();
		((ISupportInitialize)button8).EndInit();
		Avalonia.Controls.Controls children13 = stackPanel9.Children;
		Button button10;
		Button button11 = (button10 = new Button());
		((ISupportInitialize)button11).BeginInit();
		children13.Add(button11);
		Button button12 = (button3 = button10);
		context.PushParent(button3);
		Button button13 = button3;
		button13.Name = "CloseButton";
		element = button13;
		context.AvaloniaNameScope.Register("CloseButton", element);
		AutomationProperties.SetName(button13, "Close");
		StaticResourceExtension staticResourceExtension3 = new StaticResourceExtension("TitleButtonTheme");
		context.ProvideTargetProperty = StyledElement.ThemeProperty;
		object? obj5 = staticResourceExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_15(button13, obj5);
		Image image14;
		Image image15 = (image14 = new Image());
		((ISupportInitialize)image15).BeginInit();
		button13.Content = image15;
		Image image16 = (image3 = image14);
		context.PushParent(image3);
		Image image17 = image3;
		image17.Width = 30.0;
		image17.Height = 30.0;
		SvgImageExtension svgImageExtension3 = new SvgImageExtension("/Assets/Images/ic_close.svg");
		context.ProvideTargetProperty = Image.SourceProperty;
		object obj6 = svgImageExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_11(image17, obj6);
		context.PopParent();
		((ISupportInitialize)image16).EndInit();
		context.PopParent();
		((ISupportInitialize)button12).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel8).EndInit();
		context.PopParent();
		((ISupportInitialize)grid8).EndInit();
		context.PopParent();
		((ISupportInitialize)panel8).EndInit();
		Avalonia.Controls.Controls children14 = grid5.Children;
		Grid grid10;
		Grid grid11 = (grid10 = new Grid());
		((ISupportInitialize)grid11).BeginInit();
		children14.Add(grid11);
		Grid grid12 = (grid3 = grid10);
		context.PushParent(grid3);
		Grid grid13 = grid3;
		Grid.SetRow(grid13, 1);
		Avalonia.Controls.Controls children15 = grid13.Children;
		NavigationPage navigationPage;
		NavigationPage navigationPage2 = (navigationPage = new NavigationPage());
		((ISupportInitialize)navigationPage2).BeginInit();
		children15.Add(navigationPage2);
		navigationPage.Name = "PartNavigation";
		element = navigationPage;
		context.AvaloniaNameScope.Register("PartNavigation", element);
		navigationPage.IsBackButtonVisible = false;
		((ISupportInitialize)navigationPage).EndInit();
		Avalonia.Controls.Controls children16 = grid13.Children;
		ProgressBar progressBar;
		ProgressBar progressBar2 = (progressBar = new ProgressBar());
		((ISupportInitialize)progressBar2).BeginInit();
		children16.Add(progressBar2);
		ProgressBar progressBar3;
		ProgressBar progressBar4 = (progressBar3 = progressBar);
		context.PushParent(progressBar3);
		progressBar3.Name = "PartProgressBar";
		element = progressBar3;
		context.AvaloniaNameScope.Register("PartProgressBar", element);
		progressBar3.VerticalAlignment = VerticalAlignment.Bottom;
		progressBar3.Background = new ImmutableSolidColorBrush(16777215u);
		progressBar3.Height = 3.0;
		progressBar3.MinHeight = 3.0;
		progressBar3.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		progressBar3.BorderThickness = new Thickness(0.0, 0.0, 0.0, 0.0);
		progressBar3.Margin = new Thickness(0.0, 0.0, 0.0, 0.0);
		progressBar3.IsIndeterminate = true;
		StyledProperty<bool> isVisibleProperty = Visual.IsVisibleProperty;
		ReflectionBindingExtension reflectionBindingExtension = new ReflectionBindingExtension("IsBusy");
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		ReflectionBinding binding = reflectionBindingExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		progressBar3.Bind(isVisibleProperty, binding);
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
		progressBar3.Foreground = linearGradientBrush;
		progressBar3.Effect = new BlurEffect
		{
			Radius = 0.5
		};
		context.PopParent();
		((ISupportInitialize)progressBar4).EndInit();
		context.PopParent();
		((ISupportInitialize)grid12).EndInit();
		context.PopParent();
		((ISupportInitialize)grid4).EndInit();
		context.PopParent();
		((ISupportInitialize)panel4).EndInit();
		context.PopParent();
		((ISupportInitialize)P_1).EndInit();
		if (P_1 is StyledElement styled)
		{
			NameScope.SetNameScope(styled, context.AvaloniaNameScope);
		}
		context.AvaloniaNameScope.Complete();
	}

	[CompilerGenerated]
	private static void _0021XamlIlPopulateTrampoline(MainWindow2 P_0)
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
