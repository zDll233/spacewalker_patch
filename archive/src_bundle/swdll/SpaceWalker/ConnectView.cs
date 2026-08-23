using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reactive.Disposables;
using System.Reactive.Disposables.Fluent;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Documents;
using Avalonia.Data;
using Avalonia.Layout;
using Avalonia.Markup.Xaml.Converters;
using Avalonia.Markup.Xaml.MarkupExtensions;
using Avalonia.Markup.Xaml.XamlIl.Runtime;
using Avalonia.Media;
using Avalonia.Media.Immutable;
using Avalonia.Svg.Skia;
using CompiledAvaloniaXaml;
using ReactiveUI;
using SpaceWalker.Controls;

namespace SpaceWalker;

public class ConnectView : ContentPage, IActivatableView
{
	[CompilerGenerated]
	private static Action<object> _0021XamlIlPopulateOverride;

	public ConnectView()
	{
		InitializeComponent();
		this.WhenActivated(delegate(CompositeDisposable d)
		{
			if (base.DataContext is IActivatableViewModel activatableViewModel)
			{
				activatableViewModel.Activator.Activate().DisposeWith(d);
			}
		});
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
	private static void _0021XamlIlPopulate(IServiceProvider P_0, ConnectView P_1)
	{
		CompiledAvaloniaXaml.XamlIlContext.Context<ConnectView> context = new CompiledAvaloniaXaml.XamlIlContext.Context<ConnectView>(P_0, new object[1] { _0021AvaloniaResources.NamespaceInfo_003A_002FViews_002FConnectView_002Eaxaml.Singleton }, "avares://SpaceWalker/Views/ConnectView.axaml")
		{
			RootObject = P_1,
			IntermediateRoot = P_1
		};
		((ISupportInitialize)P_1).BeginInit();
		context.PushParent(P_1);
		NavigationPage.SetHasNavigationBar(P_1, value: false);
		P_1.Background = new ImmutableSolidColorBrush(16777215u);
		Grid grid;
		Grid grid2 = (grid = new Grid());
		((ISupportInitialize)grid2).BeginInit();
		P_1.Content = grid2;
		Grid grid3;
		Grid grid4 = (grid3 = grid);
		context.PushParent(grid3);
		Grid grid5 = grid3;
		RowDefinitions rowDefinitions = new RowDefinitions();
		rowDefinitions.Capacity = 3;
		rowDefinitions.Add(new RowDefinition(new GridLength(1.0, GridUnitType.Star)));
		rowDefinitions.Add(new RowDefinition(new GridLength(0.0, GridUnitType.Auto)));
		rowDefinitions.Add(new RowDefinition(new GridLength(0.0, GridUnitType.Auto)));
		grid5.RowDefinitions = rowDefinitions;
		grid5.Margin = new Thickness(12.0, 12.0, 12.0, 12.0);
		Avalonia.Controls.Controls children = grid5.Children;
		Border border;
		Border border2 = (border = new Border());
		((ISupportInitialize)border2).BeginInit();
		children.Add(border2);
		Border border3;
		Border border4 = (border3 = border);
		context.PushParent(border3);
		border3.CornerRadius = new CornerRadius(6.0, 6.0, 6.0, 6.0);
		border3.ClipToBounds = true;
		Grid grid6;
		Grid grid7 = (grid6 = new Grid());
		((ISupportInitialize)grid7).BeginInit();
		border3.Child = grid7;
		Grid grid8 = (grid3 = grid6);
		context.PushParent(grid3);
		Grid grid9 = grid3;
		Avalonia.Controls.Controls children2 = grid9.Children;
		Image image;
		Image image2 = (image = new Image());
		((ISupportInitialize)image2).BeginInit();
		children2.Add(image2);
		Image image3;
		Image image4 = (image3 = image);
		context.PushParent(image3);
		Image image5 = image3;
		image5.Source = (IImage)new BitmapTypeConverter().ConvertFrom(context, CultureInfo.InvariantCulture, "/Assets/Images/welcome.png");
		image5.Stretch = Stretch.UniformToFill;
		context.PopParent();
		((ISupportInitialize)image4).EndInit();
		Avalonia.Controls.Controls children3 = grid9.Children;
		IconLabel iconLabel;
		IconLabel iconLabel2 = (iconLabel = new IconLabel());
		((ISupportInitialize)iconLabel2).BeginInit();
		children3.Add(iconLabel2);
		IconLabel iconLabel3;
		IconLabel iconLabel4 = (iconLabel3 = iconLabel);
		context.PushParent(iconLabel3);
		iconLabel3.Margin = new Thickness(0.0, 15.75, 0.0, 0.0);
		iconLabel3.Spacing = 12.0;
		iconLabel3.IsVisible = false;
		iconLabel3.HorizontalAlignment = HorizontalAlignment.Center;
		iconLabel3.VerticalAlignment = VerticalAlignment.Top;
		Image image6;
		Image image7 = (image6 = new Image());
		((ISupportInitialize)image7).BeginInit();
		iconLabel3.Icon = image7;
		Image image8 = (image3 = image6);
		context.PushParent(image3);
		Image image9 = image3;
		image9.Width = 44.0;
		image9.Height = 44.0;
		image9.Source = (IImage)new BitmapTypeConverter().ConvertFrom(context, CultureInfo.InvariantCulture, "/Assets/Images/app_icon.png");
		image9.Stretch = Stretch.Fill;
		context.PopParent();
		((ISupportInitialize)image8).EndInit();
		TextBlock textBlock;
		TextBlock textBlock2 = (textBlock = new TextBlock());
		((ISupportInitialize)textBlock2).BeginInit();
		iconLabel3.Content = textBlock2;
		textBlock.Foreground = new ImmutableSolidColorBrush(4278979596u);
		textBlock.Opacity = 0.64;
		textBlock.FontSize = 16.0;
		textBlock.FontWeight = FontWeight.Bold;
		textBlock.Text = "VITURE SpaceWalker";
		((ISupportInitialize)textBlock).EndInit();
		context.PopParent();
		((ISupportInitialize)iconLabel4).EndInit();
		context.PopParent();
		((ISupportInitialize)grid8).EndInit();
		context.PopParent();
		((ISupportInitialize)border4).EndInit();
		Avalonia.Controls.Controls children4 = grid5.Children;
		TextBlock textBlock3;
		TextBlock textBlock4 = (textBlock3 = new TextBlock());
		((ISupportInitialize)textBlock4).BeginInit();
		children4.Add(textBlock4);
		TextBlock textBlock5;
		TextBlock textBlock6 = (textBlock5 = textBlock3);
		context.PushParent(textBlock5);
		TextBlock textBlock7 = textBlock5;
		Grid.SetRow(textBlock7, 1);
		textBlock7.Margin = new Thickness(0.0, 16.0, 0.0, 0.0);
		textBlock7.Classes.Add("base");
		textBlock7.Classes.Add("textdisplay");
		textBlock7.HorizontalAlignment = HorizontalAlignment.Center;
		StyledProperty<string?> textProperty = TextBlock.TextProperty;
		ReflectionBindingExtension reflectionBindingExtension = new ReflectionBindingExtension("TitleText");
		context.ProvideTargetProperty = TextBlock.TextProperty;
		ReflectionBinding binding = reflectionBindingExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		textBlock7.Bind(textProperty, binding);
		context.PopParent();
		((ISupportInitialize)textBlock6).EndInit();
		Avalonia.Controls.Controls children5 = grid5.Children;
		Grid grid10;
		Grid grid11 = (grid10 = new Grid());
		((ISupportInitialize)grid11).BeginInit();
		children5.Add(grid11);
		Grid grid12 = (grid3 = grid10);
		context.PushParent(grid3);
		Grid grid13 = grid3;
		Grid.SetRow(grid13, 2);
		grid13.Margin = new Thickness(80.0, 12.0, 80.0, 12.0);
		grid13.Height = 50.0;
		Avalonia.Controls.Controls children6 = grid13.Children;
		TextBlock textBlock8;
		TextBlock textBlock9 = (textBlock8 = new TextBlock());
		((ISupportInitialize)textBlock9).BeginInit();
		children6.Add(textBlock9);
		TextBlock textBlock10 = (textBlock5 = textBlock8);
		context.PushParent(textBlock5);
		TextBlock textBlock11 = textBlock5;
		textBlock11.Classes.Add("base");
		textBlock11.Opacity = 0.72;
		textBlock11.TextWrapping = TextWrapping.WrapWithOverflow;
		textBlock11.TextAlignment = TextAlignment.Center;
		textBlock11.VerticalAlignment = VerticalAlignment.Center;
		textBlock11.FontSize = 16.0;
		textBlock11.FontWeight = FontWeight.Normal;
		textBlock11.HorizontalAlignment = HorizontalAlignment.Center;
		InlineCollection? inlines = textBlock11.Inlines;
		Run run = new Run();
		((ISupportInitialize)run).BeginInit();
		Run run2 = run;
		context.PushParent(run2);
		StyledProperty<string?> textProperty2 = Run.TextProperty;
		ReflectionBindingExtension reflectionBindingExtension2 = new ReflectionBindingExtension("TipsText");
		context.ProvideTargetProperty = Run.TextProperty;
		ReflectionBinding binding2 = reflectionBindingExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		run2.Bind(textProperty2, binding2);
		context.PopParent();
		((ISupportInitialize)run).EndInit();
		inlines.Add(run);
		textBlock11.Inlines.Add(" ");
		InlineCollection? inlines2 = textBlock11.Inlines;
		InlineUIContainer inlineUIContainer = new InlineUIContainer();
		((ISupportInitialize)inlineUIContainer).BeginInit();
		InlineUIContainer inlineUIContainer2 = inlineUIContainer;
		context.PushParent(inlineUIContainer2);
		inlineUIContainer2.BaselineAlignment = BaselineAlignment.TextBottom;
		Button button;
		Button button2 = (button = new Button());
		((ISupportInitialize)button2).BeginInit();
		inlineUIContainer2.Child = button2;
		Button button3;
		Button button4 = (button3 = button);
		context.PushParent(button3);
		button3.Classes.Add("flat");
		button3.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		button3.Background = new ImmutableSolidColorBrush(16777215u);
		StyledProperty<ICommand?> commandProperty = Button.CommandProperty;
		ReflectionBindingExtension reflectionBindingExtension3 = new ReflectionBindingExtension("ViewModel.OpenWebUrlCmd");
		context.ProvideTargetProperty = Button.CommandProperty;
		ReflectionBinding binding3 = reflectionBindingExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		button3.Bind(commandProperty, binding3);
		ReflectionBindingExtension reflectionBindingExtension4 = new ReflectionBindingExtension("ViewModel.CheckInterfaceUrl");
		context.ProvideTargetProperty = Button.CommandParameterProperty;
		ReflectionBinding reflectionBinding = reflectionBindingExtension4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_10(button3, reflectionBinding);
		Image image10;
		Image image11 = (image10 = new Image());
		((ISupportInitialize)image11).BeginInit();
		button3.Content = image11;
		Image image12 = (image3 = image10);
		context.PushParent(image3);
		Image image13 = image3;
		image13.Height = 16.0;
		image13.Margin = new Thickness(2.0, 2.0, 2.0, 2.0);
		SvgImageExtension svgImageExtension = new SvgImageExtension("/Assets/Images/ic_help.svg");
		context.ProvideTargetProperty = Image.SourceProperty;
		object obj = svgImageExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_11(image13, obj);
		context.PopParent();
		((ISupportInitialize)image12).EndInit();
		context.PopParent();
		((ISupportInitialize)button4).EndInit();
		context.PopParent();
		((ISupportInitialize)inlineUIContainer).EndInit();
		inlines2.Add(inlineUIContainer);
		context.PopParent();
		((ISupportInitialize)textBlock10).EndInit();
		context.PopParent();
		((ISupportInitialize)grid12).EndInit();
		context.PopParent();
		((ISupportInitialize)grid4).EndInit();
		context.PopParent();
		((ISupportInitialize)P_1).EndInit();
		if (P_1 is StyledElement styled)
		{
			NameScope.SetNameScope(styled, context.AvaloniaNameScope);
		}
		context.AvaloniaNameScope.Complete();
	}

	[CompilerGenerated]
	private static void _0021XamlIlPopulateTrampoline(ConnectView P_0)
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
