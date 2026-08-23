using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Reactive.Disposables;
using System.Reactive.Disposables.Fluent;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Avalonia;
using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Input;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.MarkupExtensions;
using Avalonia.Markup.Xaml.Templates;
using Avalonia.Markup.Xaml.XamlIl.Runtime;
using Avalonia.Media;
using Avalonia.Media.Immutable;
using Avalonia.Styling;
using Avalonia.Svg.Skia;
using CompiledAvaloniaXaml;
using ReactiveUI;
using SpaceWalker.Assets.Languages;
using SpaceWalker.Converters;
using SpaceWalker.Helper;
using SpaceWalker.ViewModels;
using VitureCommonLibrary;

namespace SpaceWalker;

public class LayoutView : ContentPage, IActivatableView
{
	[CompilerGenerated]
	private class XamlClosure_14
	{
		public static object Build_1(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<LayoutView> context = CreateContext(P_0);
			return new VitureDeviceTypeVisibilityConverter();
		}

		public static CompiledAvaloniaXaml.XamlIlContext.Context<LayoutView> CreateContext(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<LayoutView> context = new CompiledAvaloniaXaml.XamlIlContext.Context<LayoutView>(P_0, new object[1] { _0021AvaloniaResources.NamespaceInfo_003A_002FViews_002FLayoutView_002Eaxaml.Singleton }, "avares://SpaceWalker/Views/LayoutView.axaml");
			if (P_0 != null)
			{
				object service = P_0.GetService(typeof(IRootObjectProvider));
				if (service != null)
				{
					service = ((IRootObjectProvider)service).RootObject;
					context.RootObject = (LayoutView)service;
				}
			}
			return context;
		}

		public static object Build_2(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<LayoutView> context = CreateContext(P_0);
			context.IntermediateRoot = new Border();
			object intermediateRoot = context.IntermediateRoot;
			((ISupportInitialize)intermediateRoot).BeginInit();
			((StyledElement)intermediateRoot).Name = "PART_Frame";
			object element = intermediateRoot;
			context.AvaloniaNameScope.Register("PART_Frame", element);
			((AvaloniaObject)intermediateRoot).Bind((AvaloniaProperty)Border.BackgroundProperty, new TemplateBinding(TemplatedControl.BackgroundProperty).ProvideValue());
			((AvaloniaObject)intermediateRoot).Bind((AvaloniaProperty)Border.BorderBrushProperty, new TemplateBinding(TemplatedControl.BorderBrushProperty).ProvideValue());
			((AvaloniaObject)intermediateRoot).Bind((AvaloniaProperty)Border.BorderThicknessProperty, new TemplateBinding(TemplatedControl.BorderThicknessProperty).ProvideValue());
			((AvaloniaObject)intermediateRoot).Bind((AvaloniaProperty)Border.CornerRadiusProperty, new TemplateBinding(TemplatedControl.CornerRadiusProperty).ProvideValue());
			((AvaloniaObject)intermediateRoot).Bind((AvaloniaProperty)Decorator.PaddingProperty, new TemplateBinding(TemplatedControl.PaddingProperty).ProvideValue());
			StyledProperty<Transitions?> transitionsProperty = Animatable.TransitionsProperty;
			Transitions transitions = new Transitions();
			BrushTransition brushTransition = new BrushTransition();
			brushTransition.Property = Border.BackgroundProperty;
			brushTransition.Duration = TimeSpan.FromTicks(1000000L);
			transitions.Add(brushTransition);
			((AvaloniaObject)intermediateRoot).SetValue(transitionsProperty, transitions, BindingPriority.Template);
			ContentPresenter contentPresenter;
			ContentPresenter contentPresenter2 = (contentPresenter = new ContentPresenter());
			((ISupportInitialize)contentPresenter2).BeginInit();
			((Decorator)intermediateRoot).Child = contentPresenter2;
			CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(contentPresenter, BindingPriority.Template, new TemplateBinding(ContentControl.ContentProperty).ProvideValue());
			contentPresenter.Bind(ContentPresenter.ContentTemplateProperty, new TemplateBinding(ContentControl.ContentTemplateProperty).ProvideValue());
			((ISupportInitialize)contentPresenter).EndInit();
			((ISupportInitialize)intermediateRoot).EndInit();
			return intermediateRoot;
		}

		public static object Build_3(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<LayoutView> context = CreateContext(P_0);
			context.IntermediateRoot = new UniformGrid();
			object intermediateRoot = context.IntermediateRoot;
			((ISupportInitialize)intermediateRoot).BeginInit();
			((AvaloniaObject)intermediateRoot).SetValue(UniformGrid.ColumnsProperty, 3, BindingPriority.Template);
			((AvaloniaObject)intermediateRoot).SetValue(UniformGrid.RowsProperty, 2, BindingPriority.Template);
			((AvaloniaObject)intermediateRoot).SetValue(Layoutable.HorizontalAlignmentProperty, HorizontalAlignment.Stretch, BindingPriority.Template);
			((AvaloniaObject)intermediateRoot).SetValue(Layoutable.VerticalAlignmentProperty, VerticalAlignment.Stretch, BindingPriority.Template);
			((ISupportInitialize)intermediateRoot).EndInit();
			return intermediateRoot;
		}

		public static object Build_4(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<LayoutView> context = CreateContext(P_0);
			context.IntermediateRoot = new UniformGrid();
			object intermediateRoot = context.IntermediateRoot;
			((ISupportInitialize)intermediateRoot).BeginInit();
			((AvaloniaObject)intermediateRoot).SetValue(UniformGrid.ColumnsProperty, 3, BindingPriority.Template);
			((AvaloniaObject)intermediateRoot).SetValue(UniformGrid.RowsProperty, 2, BindingPriority.Template);
			((AvaloniaObject)intermediateRoot).SetValue(Layoutable.HorizontalAlignmentProperty, HorizontalAlignment.Stretch, BindingPriority.Template);
			((AvaloniaObject)intermediateRoot).SetValue(Layoutable.VerticalAlignmentProperty, VerticalAlignment.Stretch, BindingPriority.Template);
			((ISupportInitialize)intermediateRoot).EndInit();
			return intermediateRoot;
		}
	}

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "12.0.3.0")]
	internal ContentPage LayoutRoot;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "12.0.3.0")]
	internal Grid PartGridRoot;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "12.0.3.0")]
	internal ListBox LayoutListBox;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "12.0.3.0")]
	internal TextBlock PART_UltraWideRatio;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "12.0.3.0")]
	internal ListBox LayoutListBoxR6;

	[CompilerGenerated]
	private static Action<object> _0021XamlIlPopulateOverride;

	public LayoutView()
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
		INameScope nameScope = this.FindNameScope();
		LayoutRoot = nameScope?.Find<ContentPage>("LayoutRoot");
		PartGridRoot = nameScope?.Find<Grid>("PartGridRoot");
		LayoutListBox = nameScope?.Find<ListBox>("LayoutListBox");
		PART_UltraWideRatio = nameScope?.Find<TextBlock>("PART_UltraWideRatio");
		LayoutListBoxR6 = nameScope?.Find<ListBox>("LayoutListBoxR6");
	}

	[CompilerGenerated]
	private unsafe static void _0021XamlIlPopulate(IServiceProvider P_0, LayoutView P_1)
	{
		CompiledAvaloniaXaml.XamlIlContext.Context<LayoutView> context = new CompiledAvaloniaXaml.XamlIlContext.Context<LayoutView>(P_0, new object[1] { _0021AvaloniaResources.NamespaceInfo_003A_002FViews_002FLayoutView_002Eaxaml.Singleton }, "avares://SpaceWalker/Views/LayoutView.axaml")
		{
			RootObject = P_1,
			IntermediateRoot = P_1
		};
		((ISupportInitialize)P_1).BeginInit();
		context.PushParent(P_1);
		P_1.HorizontalAlignment = HorizontalAlignment.Stretch;
		P_1.VerticalAlignment = VerticalAlignment.Stretch;
		P_1.Name = "LayoutRoot";
		object element = P_1;
		context.AvaloniaNameScope.Register("LayoutRoot", element);
		NavigationPage.SetHasNavigationBar(P_1, value: false);
		P_1.Background = new ImmutableSolidColorBrush(16777215u);
		((ResourceDictionary)P_1.Resources).AddDeferred((object)"VitureDeviceTypeVisibilityConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_14.Build_1), context));
		Styles styles = P_1.Styles;
		Style style = new Style();
		style.Selector = ((Selector?)null).OfType(typeof(ListBox)).Class("layoutcards").Descendant()
			.OfType(typeof(ListBoxItem));
		Setter setter = new Setter();
		setter.Property = Layoutable.MarginProperty;
		setter.Value = new Thickness(0.0, 0.0, 0.0, 4.0);
		style.Add(setter);
		Setter setter2 = new Setter();
		setter2.Property = TemplatedControl.PaddingProperty;
		setter2.Value = new Thickness(6.3, 6.3, 6.3, 8.4);
		style.Add(setter2);
		Setter setter3 = new Setter();
		setter3.Property = Layoutable.VerticalAlignmentProperty;
		setter3.Value = VerticalAlignment.Center;
		style.Add(setter3);
		Setter setter4 = new Setter();
		setter4.Property = TemplatedControl.CornerRadiusProperty;
		setter4.Value = new CornerRadius(4.2, 4.2, 4.2, 4.2);
		style.Add(setter4);
		Setter setter5 = new Setter();
		setter5.Property = TemplatedControl.BackgroundProperty;
		setter5.Value = new ImmutableSolidColorBrush(16777215u);
		style.Add(setter5);
		Setter setter6 = new Setter();
		setter6.Property = TemplatedControl.BorderBrushProperty;
		setter6.Value = new ImmutableSolidColorBrush(16777215u);
		style.Add(setter6);
		Setter setter7 = new Setter();
		setter7.Property = TemplatedControl.BorderThicknessProperty;
		setter7.Value = new Thickness(1.5, 1.5, 1.5, 1.5);
		style.Add(setter7);
		Setter setter8 = new Setter();
		setter8.Property = InputElement.CursorProperty;
		setter8.Value = new Cursor(StandardCursorType.Hand);
		style.Add(setter8);
		Setter setter9 = new Setter();
		setter9.Property = TemplatedControl.TemplateProperty;
		setter9.Value = new ControlTemplate
		{
			Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_14.Build_2), context)
		};
		style.Add(setter9);
		styles.Add(style);
		Styles styles2 = P_1.Styles;
		Style style2;
		Style item = (style2 = new Style());
		context.PushParent(style2);
		Style style3 = style2;
		style3.Selector = ((Selector?)null).OfType(typeof(ListBox)).Class("layoutcards").Descendant()
			.OfType(typeof(ListBoxItem))
			.Class(":pointerover")
			.Template()
			.OfType(typeof(Border))
			.Name("PART_Frame");
		Setter setter10;
		Setter setter11 = (setter10 = new Setter());
		context.PushParent(setter10);
		Setter setter12 = setter10;
		setter12.Property = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension = new DynamicResourceExtension("LayoutItemHoverBrush");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
		BindingBase value = dynamicResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		setter12.Value = value;
		context.PopParent();
		style3.Add(setter11);
		context.PopParent();
		styles2.Add(item);
		Styles styles3 = P_1.Styles;
		Style item2 = (style2 = new Style());
		context.PushParent(style2);
		Style style4 = style2;
		style4.Selector = ((Selector?)null).OfType(typeof(ListBox)).Class("layoutcards").Descendant()
			.OfType(typeof(ListBoxItem))
			.Class(":selected")
			.Template()
			.OfType(typeof(Border))
			.Name("PART_Frame");
		Setter setter13 = (setter10 = new Setter());
		context.PushParent(setter10);
		Setter setter14 = setter10;
		setter14.Property = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension2 = new DynamicResourceExtension("LayoutItemSelectedBrush");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
		BindingBase value2 = dynamicResourceExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		setter14.Value = value2;
		context.PopParent();
		style4.Add(setter13);
		Setter setter15 = (setter10 = new Setter());
		context.PushParent(setter10);
		Setter setter16 = setter10;
		setter16.Property = Border.BorderBrushProperty;
		DynamicResourceExtension dynamicResourceExtension3 = new DynamicResourceExtension("LayoutItemSelectedBorderBrush");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
		BindingBase value3 = dynamicResourceExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		setter16.Value = value3;
		context.PopParent();
		style4.Add(setter15);
		context.PopParent();
		styles3.Add(item2);
		Styles styles4 = P_1.Styles;
		Style item3 = (style2 = new Style());
		context.PushParent(style2);
		Style style5 = style2;
		style5.Selector = ((Selector?)null).OfType(typeof(ListBox)).Class("layoutcards").Descendant()
			.OfType(typeof(ListBoxItem))
			.Class(":pressed")
			.Template()
			.OfType(typeof(Border))
			.Name("PART_Frame");
		Setter setter17 = (setter10 = new Setter());
		context.PushParent(setter10);
		Setter setter18 = setter10;
		setter18.Property = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension4 = new DynamicResourceExtension("LayoutItemPressedBrush");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
		BindingBase value4 = dynamicResourceExtension4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		setter18.Value = value4;
		context.PopParent();
		style5.Add(setter17);
		Setter setter19 = new Setter();
		setter19.Property = Border.BorderBrushProperty;
		setter19.Value = new ImmutableSolidColorBrush(16777215u);
		style5.Add(setter19);
		context.PopParent();
		styles4.Add(item3);
		Styles styles5 = P_1.Styles;
		Style style6 = new Style();
		style6.Selector = ((Selector?)null).OfType(typeof(ListBox)).Class("layoutcards").Descendant()
			.OfType(typeof(ListBoxItem))
			.Descendant()
			.OfType(typeof(TextBlock));
		Setter setter20 = new Setter();
		setter20.Property = TextBlock.TextAlignmentProperty;
		setter20.Value = TextAlignment.Center;
		style6.Add(setter20);
		Setter setter21 = new Setter();
		setter21.Property = TextBlock.FontSizeProperty;
		setter21.Value = 14.0;
		style6.Add(setter21);
		Setter setter22 = new Setter();
		setter22.Property = TextBlock.FontWeightProperty;
		setter22.Value = FontWeight.DemiBold;
		style6.Add(setter22);
		Setter setter23 = new Setter();
		setter23.Property = TextBlock.ForegroundProperty;
		setter23.Value = new ImmutableSolidColorBrush(uint.MaxValue);
		style6.Add(setter23);
		styles5.Add(style6);
		Styles styles6 = P_1.Styles;
		Style style7 = new Style();
		style7.Selector = ((Selector?)null).OfType(typeof(ListBox)).Class("layoutcards").Descendant()
			.OfType(typeof(ListBoxItem))
			.Descendant()
			.OfType(typeof(TextBlock))
			.Class("cardname");
		Setter setter24 = new Setter();
		setter24.Property = TextBlock.FontWeightProperty;
		setter24.Value = FontWeight.Medium;
		style7.Add(setter24);
		Setter setter25 = new Setter();
		setter25.Property = Visual.OpacityProperty;
		setter25.Value = 0.72;
		style7.Add(setter25);
		styles6.Add(style7);
		Styles styles7 = P_1.Styles;
		Style style8 = new Style();
		style8.Selector = Selectors.Or(new List<Selector>
		{
			((Selector?)null).OfType(typeof(ListBox)).Class("layoutcards").Descendant()
				.OfType(typeof(ListBoxItem))
				.Class(":pointerover")
				.Descendant()
				.OfType(typeof(TextBlock))
				.Class("cardname"),
			((Selector?)null).OfType(typeof(ListBox)).Class("layoutcards").Descendant()
				.OfType(typeof(ListBoxItem))
				.Class(":selected")
				.Descendant()
				.OfType(typeof(TextBlock))
				.Class("cardname")
		});
		Setter setter26 = new Setter();
		setter26.Property = TextBlock.FontWeightProperty;
		setter26.Value = FontWeight.DemiBold;
		style8.Add(setter26);
		Setter setter27 = new Setter();
		setter27.Property = Visual.OpacityProperty;
		setter27.Value = 1.0;
		style8.Add(setter27);
		styles7.Add(style8);
		Styles styles8 = P_1.Styles;
		Style style9 = new Style();
		style9.Selector = ((Selector?)null).OfType(typeof(ListBox)).Class("layoutcards").Descendant()
			.OfType(typeof(ListBoxItem))
			.Class(":pressed")
			.Descendant()
			.OfType(typeof(TextBlock))
			.Class("cardname");
		Setter setter28 = new Setter();
		setter28.Property = TextBlock.FontWeightProperty;
		setter28.Value = FontWeight.DemiBold;
		style9.Add(setter28);
		Setter setter29 = new Setter();
		setter29.Property = Visual.OpacityProperty;
		setter29.Value = 0.8;
		style9.Add(setter29);
		styles8.Add(style9);
		Styles styles9 = P_1.Styles;
		Style style10 = new Style();
		style10.Selector = ((Selector?)null).OfType(typeof(ListBox)).Class("layoutcards").Descendant()
			.OfType(typeof(ListBoxItem))
			.Descendant()
			.OfType(typeof(Image));
		Setter setter30 = new Setter();
		setter30.Property = Image.StretchProperty;
		setter30.Value = Stretch.UniformToFill;
		style10.Add(setter30);
		styles9.Add(style10);
		Styles styles10 = P_1.Styles;
		Style style11 = new Style();
		style11.Selector = ((Selector?)null).OfType(typeof(ListBox)).Class("layoutcards").Descendant()
			.OfType(typeof(ListBoxItem))
			.Descendant()
			.OfType(typeof(Border))
			.Class("thumbclip");
		Setter setter31 = new Setter();
		setter31.Property = Border.CornerRadiusProperty;
		setter31.Value = new CornerRadius(4.0, 4.0, 4.0, 4.0);
		style11.Add(setter31);
		Setter setter32 = new Setter();
		setter32.Property = Visual.ClipToBoundsProperty;
		setter32.Value = true;
		style11.Add(setter32);
		Setter setter33 = new Setter();
		setter33.Property = Layoutable.HorizontalAlignmentProperty;
		setter33.Value = HorizontalAlignment.Center;
		style11.Add(setter33);
		Setter setter34 = new Setter();
		setter34.Property = Layoutable.VerticalAlignmentProperty;
		setter34.Value = VerticalAlignment.Center;
		style11.Add(setter34);
		styles10.Add(style11);
		Styles styles11 = P_1.Styles;
		Style style12 = new Style();
		style12.Selector = ((Selector?)null).OfType(typeof(ListBox)).Class("layoutcards").Descendant()
			.OfType(typeof(ListBoxItem))
			.Descendant()
			.OfType(typeof(Image))
			.Class("imgactive");
		Setter setter35 = new Setter();
		setter35.Property = Visual.OpacityProperty;
		setter35.Value = 0.0;
		style12.Add(setter35);
		styles11.Add(style12);
		Styles styles12 = P_1.Styles;
		Style style13 = new Style();
		style13.Selector = Selectors.Or(new List<Selector>
		{
			((Selector?)null).OfType(typeof(ListBox)).Class("layoutcards").Descendant()
				.OfType(typeof(ListBoxItem))
				.Class(":pointerover")
				.Descendant()
				.OfType(typeof(Image))
				.Class("imgactive"),
			((Selector?)null).OfType(typeof(ListBox)).Class("layoutcards").Descendant()
				.OfType(typeof(ListBoxItem))
				.Class(":pressed")
				.Descendant()
				.OfType(typeof(Image))
				.Class("imgactive"),
			((Selector?)null).OfType(typeof(ListBox)).Class("layoutcards").Descendant()
				.OfType(typeof(ListBoxItem))
				.Class(":selected")
				.Descendant()
				.OfType(typeof(Image))
				.Class("imgactive")
		});
		Setter setter36 = new Setter();
		setter36.Property = Visual.OpacityProperty;
		setter36.Value = 1.0;
		style13.Add(setter36);
		styles12.Add(style13);
		Styles styles13 = P_1.Styles;
		Style style14 = new Style();
		style14.Selector = Selectors.Or(new List<Selector>
		{
			((Selector?)null).OfType(typeof(ListBox)).Class("layoutcards").Descendant()
				.OfType(typeof(ListBoxItem))
				.Class(":pointerover")
				.Descendant()
				.OfType(typeof(Image))
				.Class("imgnormal"),
			((Selector?)null).OfType(typeof(ListBox)).Class("layoutcards").Descendant()
				.OfType(typeof(ListBoxItem))
				.Class(":pressed")
				.Descendant()
				.OfType(typeof(Image))
				.Class("imgnormal"),
			((Selector?)null).OfType(typeof(ListBox)).Class("layoutcards").Descendant()
				.OfType(typeof(ListBoxItem))
				.Class(":selected")
				.Descendant()
				.OfType(typeof(Image))
				.Class("imgnormal")
		});
		Setter setter37 = new Setter();
		setter37.Property = Visual.OpacityProperty;
		setter37.Value = 0.0;
		style14.Add(setter37);
		styles13.Add(style14);
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
		grid5.Margin = new Thickness(24.0, 16.0, 24.0, 16.0);
		grid5.Name = "PartGridRoot";
		element = grid5;
		context.AvaloniaNameScope.Register("PartGridRoot", element);
		Avalonia.Controls.Controls children = grid5.Children;
		Grid grid6;
		Grid grid7 = (grid6 = new Grid());
		((ISupportInitialize)grid7).BeginInit();
		children.Add(grid7);
		Grid grid8 = (grid3 = grid6);
		context.PushParent(grid3);
		Grid grid9 = grid3;
		RowDefinitions rowDefinitions2 = new RowDefinitions();
		rowDefinitions2.Capacity = 2;
		rowDefinitions2.Add(new RowDefinition(new GridLength(0.0, GridUnitType.Auto)));
		rowDefinitions2.Add(new RowDefinition(new GridLength(1.0, GridUnitType.Star)));
		grid9.RowDefinitions = rowDefinitions2;
		Avalonia.Controls.Controls children2 = grid9.Children;
		TextBlock textBlock;
		TextBlock textBlock2 = (textBlock = new TextBlock());
		((ISupportInitialize)textBlock2).BeginInit();
		children2.Add(textBlock2);
		textBlock.Classes.Add("base");
		textBlock.Classes.Add("texttitle");
		textBlock.Margin = new Thickness(8.0, 0.0, 0.0, 8.0);
		textBlock.Text = SpaceWalker.Assets.Languages.Resources.SelectALayout;
		((ISupportInitialize)textBlock).EndInit();
		Avalonia.Controls.Controls children3 = grid9.Children;
		Grid grid10;
		Grid grid11 = (grid10 = new Grid());
		((ISupportInitialize)grid11).BeginInit();
		children3.Add(grid11);
		Grid grid12 = (grid3 = grid10);
		context.PushParent(grid3);
		Grid grid13 = grid3;
		Grid.SetRow(grid13, 1);
		Avalonia.Controls.Controls children4 = grid13.Children;
		Grid grid14;
		Grid grid15 = (grid14 = new Grid());
		((ISupportInitialize)grid15).BeginInit();
		children4.Add(grid15);
		Grid grid16 = (grid3 = grid14);
		context.PushParent(grid3);
		Grid grid17 = grid3;
		Avalonia.Controls.Controls children5 = grid17.Children;
		ListBox listBox;
		ListBox listBox2 = (listBox = new ListBox());
		((ISupportInitialize)listBox2).BeginInit();
		children5.Add(listBox2);
		ListBox listBox3;
		ListBox listBox4 = (listBox3 = listBox);
		context.PushParent(listBox3);
		ListBox listBox5 = listBox3;
		listBox5.Name = "LayoutListBox";
		element = listBox5;
		context.AvaloniaNameScope.Register("LayoutListBox", element);
		listBox5.SelectionMode = SelectionMode.Single;
		listBox5.Background = new ImmutableSolidColorBrush(16777215u);
		listBox5.Classes.Add("void");
		listBox5.Classes.Add("layoutcards");
		ScrollViewer.SetHorizontalScrollBarVisibility(listBox5, ScrollBarVisibility.Disabled);
		ScrollViewer.SetVerticalScrollBarVisibility(listBox5, ScrollBarVisibility.Disabled);
		StyledProperty<bool> isVisibleProperty = Visual.IsVisibleProperty;
		ReflectionBindingExtension reflectionBindingExtension = new ReflectionBindingExtension("Show6Layout");
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		ReflectionBinding binding = reflectionBindingExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		listBox5.Bind(isVisibleProperty, binding);
		listBox5.ItemsPanel = new ItemsPanelTemplate
		{
			Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_14.Build_3), context)
		};
		ItemCollection items = listBox5.Items;
		ListBoxItem listBoxItem;
		ListBoxItem listBoxItem2 = (listBoxItem = new ListBoxItem());
		((ISupportInitialize)listBoxItem2).BeginInit();
		items.Add(listBoxItem2);
		ListBoxItem listBoxItem3;
		ListBoxItem listBoxItem4 = (listBoxItem3 = listBoxItem);
		context.PushParent(listBoxItem3);
		ListBoxItem listBoxItem5 = listBoxItem3;
		StyledProperty<bool> isSelectedProperty = ListBoxItem.IsSelectedProperty;
		ReflectionBindingExtension reflectionBindingExtension2 = new ReflectionBindingExtension("ViewModel.VitureLayoutMode");
		reflectionBindingExtension2.Mode = BindingMode.TwoWay;
		reflectionBindingExtension2.Converter = EqualsToBooleanConverter.Instance;
		reflectionBindingExtension2.ConverterParameter = VitureLayoutMode.Horizontal1;
		context.ProvideTargetProperty = ListBoxItem.IsSelectedProperty;
		ReflectionBinding binding2 = reflectionBindingExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		listBoxItem5.Bind(isSelectedProperty, binding2);
		Grid grid18;
		Grid grid19 = (grid18 = new Grid());
		((ISupportInitialize)grid19).BeginInit();
		listBoxItem5.Content = grid19;
		Grid grid20 = (grid3 = grid18);
		context.PushParent(grid3);
		Grid grid21 = grid3;
		RowDefinitions rowDefinitions3 = new RowDefinitions();
		rowDefinitions3.Capacity = 2;
		rowDefinitions3.Add(new RowDefinition(new GridLength(1.0, GridUnitType.Star)));
		rowDefinitions3.Add(new RowDefinition(new GridLength(0.0, GridUnitType.Auto)));
		grid21.RowDefinitions = rowDefinitions3;
		Avalonia.Controls.Controls children6 = grid21.Children;
		Border border;
		Border border2 = (border = new Border());
		((ISupportInitialize)border2).BeginInit();
		children6.Add(border2);
		Border border3;
		Border border4 = (border3 = border);
		context.PushParent(border3);
		Border border5 = border3;
		border5.Classes.Add("thumbclip");
		Panel panel;
		Panel panel2 = (panel = new Panel());
		((ISupportInitialize)panel2).BeginInit();
		border5.Child = panel2;
		Panel panel3;
		Panel panel4 = (panel3 = panel);
		context.PushParent(panel3);
		Panel panel5 = panel3;
		Avalonia.Controls.Controls children7 = panel5.Children;
		Image image;
		Image image2 = (image = new Image());
		((ISupportInitialize)image2).BeginInit();
		children7.Add(image2);
		Image image3;
		Image image4 = (image3 = image);
		context.PushParent(image3);
		Image image5 = image3;
		image5.Classes.Add("imgnormal");
		SvgImageExtension svgImageExtension = new SvgImageExtension("/Assets/Images/ic_layout1n.svg");
		context.ProvideTargetProperty = Image.SourceProperty;
		object obj = svgImageExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_11(image5, obj);
		StyledProperty<bool> isVisibleProperty2 = Visual.IsVisibleProperty;
		ReflectionBindingExtension obj2 = new ReflectionBindingExtension("!IsPbz")
		{
			Source = ThemeManager.Instance
		};
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		ReflectionBinding binding3 = obj2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		image5.Bind(isVisibleProperty2, binding3);
		context.PopParent();
		((ISupportInitialize)image4).EndInit();
		Avalonia.Controls.Controls children8 = panel5.Children;
		Image image6;
		Image image7 = (image6 = new Image());
		((ISupportInitialize)image7).BeginInit();
		children8.Add(image7);
		Image image8 = (image3 = image6);
		context.PushParent(image3);
		Image image9 = image3;
		image9.Classes.Add("imgactive");
		SvgImageExtension svgImageExtension2 = new SvgImageExtension("/Assets/Images/ic_layout1s.svg");
		context.ProvideTargetProperty = Image.SourceProperty;
		object obj3 = svgImageExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_11(image9, obj3);
		StyledProperty<bool> isVisibleProperty3 = Visual.IsVisibleProperty;
		ReflectionBindingExtension obj4 = new ReflectionBindingExtension("!IsPbz")
		{
			Source = ThemeManager.Instance
		};
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		ReflectionBinding binding4 = obj4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		image9.Bind(isVisibleProperty3, binding4);
		context.PopParent();
		((ISupportInitialize)image8).EndInit();
		Avalonia.Controls.Controls children9 = panel5.Children;
		Image image10;
		Image image11 = (image10 = new Image());
		((ISupportInitialize)image11).BeginInit();
		children9.Add(image11);
		Image image12 = (image3 = image10);
		context.PushParent(image3);
		Image image13 = image3;
		SvgImageExtension svgImageExtension3 = new SvgImageExtension("/Assets/Images/pbz/pbz_layout1.svg");
		context.ProvideTargetProperty = Image.SourceProperty;
		object obj5 = svgImageExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_11(image13, obj5);
		StyledProperty<bool> isVisibleProperty4 = Visual.IsVisibleProperty;
		ReflectionBindingExtension obj6 = new ReflectionBindingExtension("IsPbz")
		{
			Source = ThemeManager.Instance
		};
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		ReflectionBinding binding5 = obj6.ProvideValue(context);
		context.ProvideTargetProperty = null;
		image13.Bind(isVisibleProperty4, binding5);
		context.PopParent();
		((ISupportInitialize)image12).EndInit();
		context.PopParent();
		((ISupportInitialize)panel4).EndInit();
		context.PopParent();
		((ISupportInitialize)border4).EndInit();
		Avalonia.Controls.Controls children10 = grid21.Children;
		TextBlock textBlock3;
		TextBlock textBlock4 = (textBlock3 = new TextBlock());
		((ISupportInitialize)textBlock4).BeginInit();
		children10.Add(textBlock4);
		Grid.SetRow(textBlock3, 1);
		textBlock3.Margin = new Thickness(0.0, 8.0, 0.0, 0.0);
		textBlock3.Classes.Add("base");
		textBlock3.Classes.Add("cardname");
		textBlock3.Text = SpaceWalker.Assets.Languages.Resources.SingleDisplay;
		((ISupportInitialize)textBlock3).EndInit();
		context.PopParent();
		((ISupportInitialize)grid20).EndInit();
		context.PopParent();
		((ISupportInitialize)listBoxItem4).EndInit();
		ItemCollection items2 = listBox5.Items;
		ListBoxItem listBoxItem6;
		ListBoxItem listBoxItem7 = (listBoxItem6 = new ListBoxItem());
		((ISupportInitialize)listBoxItem7).BeginInit();
		items2.Add(listBoxItem7);
		ListBoxItem listBoxItem8 = (listBoxItem3 = listBoxItem6);
		context.PushParent(listBoxItem3);
		ListBoxItem listBoxItem9 = listBoxItem3;
		StyledProperty<bool> isSelectedProperty2 = ListBoxItem.IsSelectedProperty;
		ReflectionBindingExtension reflectionBindingExtension3 = new ReflectionBindingExtension("ViewModel.VitureLayoutMode");
		reflectionBindingExtension3.Mode = BindingMode.TwoWay;
		reflectionBindingExtension3.Converter = EqualsToBooleanConverter.Instance;
		reflectionBindingExtension3.ConverterParameter = VitureLayoutMode.Horizontal2;
		context.ProvideTargetProperty = ListBoxItem.IsSelectedProperty;
		ReflectionBinding binding6 = reflectionBindingExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		listBoxItem9.Bind(isSelectedProperty2, binding6);
		Grid grid22;
		Grid grid23 = (grid22 = new Grid());
		((ISupportInitialize)grid23).BeginInit();
		listBoxItem9.Content = grid23;
		Grid grid24 = (grid3 = grid22);
		context.PushParent(grid3);
		Grid grid25 = grid3;
		RowDefinitions rowDefinitions4 = new RowDefinitions();
		rowDefinitions4.Capacity = 2;
		rowDefinitions4.Add(new RowDefinition(new GridLength(1.0, GridUnitType.Star)));
		rowDefinitions4.Add(new RowDefinition(new GridLength(0.0, GridUnitType.Auto)));
		grid25.RowDefinitions = rowDefinitions4;
		Avalonia.Controls.Controls children11 = grid25.Children;
		Border border6;
		Border border7 = (border6 = new Border());
		((ISupportInitialize)border7).BeginInit();
		children11.Add(border7);
		Border border8 = (border3 = border6);
		context.PushParent(border3);
		Border border9 = border3;
		border9.Classes.Add("thumbclip");
		Panel panel6;
		Panel panel7 = (panel6 = new Panel());
		((ISupportInitialize)panel7).BeginInit();
		border9.Child = panel7;
		Panel panel8 = (panel3 = panel6);
		context.PushParent(panel3);
		Panel panel9 = panel3;
		Avalonia.Controls.Controls children12 = panel9.Children;
		Image image14;
		Image image15 = (image14 = new Image());
		((ISupportInitialize)image15).BeginInit();
		children12.Add(image15);
		Image image16 = (image3 = image14);
		context.PushParent(image3);
		Image image17 = image3;
		image17.Classes.Add("imgnormal");
		SvgImageExtension svgImageExtension4 = new SvgImageExtension("/Assets/Images/ic_layout2n.svg");
		context.ProvideTargetProperty = Image.SourceProperty;
		object obj7 = svgImageExtension4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_11(image17, obj7);
		StyledProperty<bool> isVisibleProperty5 = Visual.IsVisibleProperty;
		ReflectionBindingExtension obj8 = new ReflectionBindingExtension("!IsPbz")
		{
			Source = ThemeManager.Instance
		};
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		ReflectionBinding binding7 = obj8.ProvideValue(context);
		context.ProvideTargetProperty = null;
		image17.Bind(isVisibleProperty5, binding7);
		context.PopParent();
		((ISupportInitialize)image16).EndInit();
		Avalonia.Controls.Controls children13 = panel9.Children;
		Image image18;
		Image image19 = (image18 = new Image());
		((ISupportInitialize)image19).BeginInit();
		children13.Add(image19);
		Image image20 = (image3 = image18);
		context.PushParent(image3);
		Image image21 = image3;
		image21.Classes.Add("imgactive");
		SvgImageExtension svgImageExtension5 = new SvgImageExtension("/Assets/Images/ic_layout2s.svg");
		context.ProvideTargetProperty = Image.SourceProperty;
		object obj9 = svgImageExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_11(image21, obj9);
		StyledProperty<bool> isVisibleProperty6 = Visual.IsVisibleProperty;
		ReflectionBindingExtension obj10 = new ReflectionBindingExtension("!IsPbz")
		{
			Source = ThemeManager.Instance
		};
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		ReflectionBinding binding8 = obj10.ProvideValue(context);
		context.ProvideTargetProperty = null;
		image21.Bind(isVisibleProperty6, binding8);
		context.PopParent();
		((ISupportInitialize)image20).EndInit();
		Avalonia.Controls.Controls children14 = panel9.Children;
		Image image22;
		Image image23 = (image22 = new Image());
		((ISupportInitialize)image23).BeginInit();
		children14.Add(image23);
		Image image24 = (image3 = image22);
		context.PushParent(image3);
		Image image25 = image3;
		SvgImageExtension svgImageExtension6 = new SvgImageExtension("/Assets/Images/pbz/pbz_layout2.svg");
		context.ProvideTargetProperty = Image.SourceProperty;
		object obj11 = svgImageExtension6.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_11(image25, obj11);
		StyledProperty<bool> isVisibleProperty7 = Visual.IsVisibleProperty;
		ReflectionBindingExtension obj12 = new ReflectionBindingExtension("IsPbz")
		{
			Source = ThemeManager.Instance
		};
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		ReflectionBinding binding9 = obj12.ProvideValue(context);
		context.ProvideTargetProperty = null;
		image25.Bind(isVisibleProperty7, binding9);
		context.PopParent();
		((ISupportInitialize)image24).EndInit();
		context.PopParent();
		((ISupportInitialize)panel8).EndInit();
		context.PopParent();
		((ISupportInitialize)border8).EndInit();
		Avalonia.Controls.Controls children15 = grid25.Children;
		TextBlock textBlock5;
		TextBlock textBlock6 = (textBlock5 = new TextBlock());
		((ISupportInitialize)textBlock6).BeginInit();
		children15.Add(textBlock6);
		Grid.SetRow(textBlock5, 1);
		textBlock5.Margin = new Thickness(0.0, 8.0, 0.0, 0.0);
		textBlock5.Classes.Add("base");
		textBlock5.Classes.Add("cardname");
		textBlock5.Text = SpaceWalker.Assets.Languages.Resources.TwoDisplaysSBS;
		((ISupportInitialize)textBlock5).EndInit();
		context.PopParent();
		((ISupportInitialize)grid24).EndInit();
		context.PopParent();
		((ISupportInitialize)listBoxItem8).EndInit();
		ItemCollection items3 = listBox5.Items;
		ListBoxItem listBoxItem10;
		ListBoxItem listBoxItem11 = (listBoxItem10 = new ListBoxItem());
		((ISupportInitialize)listBoxItem11).BeginInit();
		items3.Add(listBoxItem11);
		ListBoxItem listBoxItem12 = (listBoxItem3 = listBoxItem10);
		context.PushParent(listBoxItem3);
		ListBoxItem listBoxItem13 = listBoxItem3;
		StyledProperty<bool> isSelectedProperty3 = ListBoxItem.IsSelectedProperty;
		ReflectionBindingExtension reflectionBindingExtension4 = new ReflectionBindingExtension("ViewModel.VitureLayoutMode");
		reflectionBindingExtension4.Mode = BindingMode.TwoWay;
		reflectionBindingExtension4.Converter = EqualsToBooleanConverter.Instance;
		reflectionBindingExtension4.ConverterParameter = VitureLayoutMode.Horizontal3;
		context.ProvideTargetProperty = ListBoxItem.IsSelectedProperty;
		ReflectionBinding binding10 = reflectionBindingExtension4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		listBoxItem13.Bind(isSelectedProperty3, binding10);
		Grid grid26;
		Grid grid27 = (grid26 = new Grid());
		((ISupportInitialize)grid27).BeginInit();
		listBoxItem13.Content = grid27;
		Grid grid28 = (grid3 = grid26);
		context.PushParent(grid3);
		Grid grid29 = grid3;
		RowDefinitions rowDefinitions5 = new RowDefinitions();
		rowDefinitions5.Capacity = 2;
		rowDefinitions5.Add(new RowDefinition(new GridLength(1.0, GridUnitType.Star)));
		rowDefinitions5.Add(new RowDefinition(new GridLength(0.0, GridUnitType.Auto)));
		grid29.RowDefinitions = rowDefinitions5;
		Avalonia.Controls.Controls children16 = grid29.Children;
		Border border10;
		Border border11 = (border10 = new Border());
		((ISupportInitialize)border11).BeginInit();
		children16.Add(border11);
		Border border12 = (border3 = border10);
		context.PushParent(border3);
		Border border13 = border3;
		border13.Classes.Add("thumbclip");
		Panel panel10;
		Panel panel11 = (panel10 = new Panel());
		((ISupportInitialize)panel11).BeginInit();
		border13.Child = panel11;
		Panel panel12 = (panel3 = panel10);
		context.PushParent(panel3);
		Panel panel13 = panel3;
		Avalonia.Controls.Controls children17 = panel13.Children;
		Image image26;
		Image image27 = (image26 = new Image());
		((ISupportInitialize)image27).BeginInit();
		children17.Add(image27);
		Image image28 = (image3 = image26);
		context.PushParent(image3);
		Image image29 = image3;
		image29.Classes.Add("imgnormal");
		SvgImageExtension svgImageExtension7 = new SvgImageExtension("/Assets/Images/ic_layout3n.svg");
		context.ProvideTargetProperty = Image.SourceProperty;
		object obj13 = svgImageExtension7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_11(image29, obj13);
		StyledProperty<bool> isVisibleProperty8 = Visual.IsVisibleProperty;
		ReflectionBindingExtension obj14 = new ReflectionBindingExtension("!IsPbz")
		{
			Source = ThemeManager.Instance
		};
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		ReflectionBinding binding11 = obj14.ProvideValue(context);
		context.ProvideTargetProperty = null;
		image29.Bind(isVisibleProperty8, binding11);
		context.PopParent();
		((ISupportInitialize)image28).EndInit();
		Avalonia.Controls.Controls children18 = panel13.Children;
		Image image30;
		Image image31 = (image30 = new Image());
		((ISupportInitialize)image31).BeginInit();
		children18.Add(image31);
		Image image32 = (image3 = image30);
		context.PushParent(image3);
		Image image33 = image3;
		image33.Classes.Add("imgactive");
		SvgImageExtension svgImageExtension8 = new SvgImageExtension("/Assets/Images/ic_layout3s.svg");
		context.ProvideTargetProperty = Image.SourceProperty;
		object obj15 = svgImageExtension8.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_11(image33, obj15);
		StyledProperty<bool> isVisibleProperty9 = Visual.IsVisibleProperty;
		ReflectionBindingExtension obj16 = new ReflectionBindingExtension("!IsPbz")
		{
			Source = ThemeManager.Instance
		};
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		ReflectionBinding binding12 = obj16.ProvideValue(context);
		context.ProvideTargetProperty = null;
		image33.Bind(isVisibleProperty9, binding12);
		context.PopParent();
		((ISupportInitialize)image32).EndInit();
		Avalonia.Controls.Controls children19 = panel13.Children;
		Image image34;
		Image image35 = (image34 = new Image());
		((ISupportInitialize)image35).BeginInit();
		children19.Add(image35);
		Image image36 = (image3 = image34);
		context.PushParent(image3);
		Image image37 = image3;
		SvgImageExtension svgImageExtension9 = new SvgImageExtension("/Assets/Images/pbz/pbz_layout3.svg");
		context.ProvideTargetProperty = Image.SourceProperty;
		object obj17 = svgImageExtension9.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_11(image37, obj17);
		StyledProperty<bool> isVisibleProperty10 = Visual.IsVisibleProperty;
		ReflectionBindingExtension obj18 = new ReflectionBindingExtension("IsPbz")
		{
			Source = ThemeManager.Instance
		};
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		ReflectionBinding binding13 = obj18.ProvideValue(context);
		context.ProvideTargetProperty = null;
		image37.Bind(isVisibleProperty10, binding13);
		context.PopParent();
		((ISupportInitialize)image36).EndInit();
		context.PopParent();
		((ISupportInitialize)panel12).EndInit();
		context.PopParent();
		((ISupportInitialize)border12).EndInit();
		Avalonia.Controls.Controls children20 = grid29.Children;
		TextBlock textBlock7;
		TextBlock textBlock8 = (textBlock7 = new TextBlock());
		((ISupportInitialize)textBlock8).BeginInit();
		children20.Add(textBlock8);
		Grid.SetRow(textBlock7, 1);
		textBlock7.Margin = new Thickness(0.0, 8.0, 0.0, 0.0);
		textBlock7.Classes.Add("base");
		textBlock7.Classes.Add("cardname");
		textBlock7.Text = SpaceWalker.Assets.Languages.Resources.ThreeDisplaysSBS;
		((ISupportInitialize)textBlock7).EndInit();
		context.PopParent();
		((ISupportInitialize)grid28).EndInit();
		context.PopParent();
		((ISupportInitialize)listBoxItem12).EndInit();
		ItemCollection items4 = listBox5.Items;
		ListBoxItem listBoxItem14;
		ListBoxItem listBoxItem15 = (listBoxItem14 = new ListBoxItem());
		((ISupportInitialize)listBoxItem15).BeginInit();
		items4.Add(listBoxItem15);
		ListBoxItem listBoxItem16 = (listBoxItem3 = listBoxItem14);
		context.PushParent(listBoxItem3);
		ListBoxItem listBoxItem17 = listBoxItem3;
		StyledProperty<bool> isSelectedProperty4 = ListBoxItem.IsSelectedProperty;
		ReflectionBindingExtension reflectionBindingExtension5 = new ReflectionBindingExtension("ViewModel.VitureLayoutMode");
		reflectionBindingExtension5.Mode = BindingMode.TwoWay;
		reflectionBindingExtension5.Converter = EqualsToBooleanConverter.Instance;
		reflectionBindingExtension5.ConverterParameter = VitureLayoutMode.Vertical3;
		context.ProvideTargetProperty = ListBoxItem.IsSelectedProperty;
		ReflectionBinding binding14 = reflectionBindingExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		listBoxItem17.Bind(isSelectedProperty4, binding14);
		Grid grid30;
		Grid grid31 = (grid30 = new Grid());
		((ISupportInitialize)grid31).BeginInit();
		listBoxItem17.Content = grid31;
		Grid grid32 = (grid3 = grid30);
		context.PushParent(grid3);
		Grid grid33 = grid3;
		RowDefinitions rowDefinitions6 = new RowDefinitions();
		rowDefinitions6.Capacity = 2;
		rowDefinitions6.Add(new RowDefinition(new GridLength(1.0, GridUnitType.Star)));
		rowDefinitions6.Add(new RowDefinition(new GridLength(0.0, GridUnitType.Auto)));
		grid33.RowDefinitions = rowDefinitions6;
		Avalonia.Controls.Controls children21 = grid33.Children;
		Border border14;
		Border border15 = (border14 = new Border());
		((ISupportInitialize)border15).BeginInit();
		children21.Add(border15);
		Border border16 = (border3 = border14);
		context.PushParent(border3);
		Border border17 = border3;
		border17.Classes.Add("thumbclip");
		Panel panel14;
		Panel panel15 = (panel14 = new Panel());
		((ISupportInitialize)panel15).BeginInit();
		border17.Child = panel15;
		Panel panel16 = (panel3 = panel14);
		context.PushParent(panel3);
		Panel panel17 = panel3;
		Avalonia.Controls.Controls children22 = panel17.Children;
		Image image38;
		Image image39 = (image38 = new Image());
		((ISupportInitialize)image39).BeginInit();
		children22.Add(image39);
		Image image40 = (image3 = image38);
		context.PushParent(image3);
		Image image41 = image3;
		image41.Classes.Add("imgnormal");
		SvgImageExtension svgImageExtension10 = new SvgImageExtension("/Assets/Images/ic_layout4n.svg");
		context.ProvideTargetProperty = Image.SourceProperty;
		object obj19 = svgImageExtension10.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_11(image41, obj19);
		StyledProperty<bool> isVisibleProperty11 = Visual.IsVisibleProperty;
		ReflectionBindingExtension obj20 = new ReflectionBindingExtension("!IsPbz")
		{
			Source = ThemeManager.Instance
		};
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		ReflectionBinding binding15 = obj20.ProvideValue(context);
		context.ProvideTargetProperty = null;
		image41.Bind(isVisibleProperty11, binding15);
		context.PopParent();
		((ISupportInitialize)image40).EndInit();
		Avalonia.Controls.Controls children23 = panel17.Children;
		Image image42;
		Image image43 = (image42 = new Image());
		((ISupportInitialize)image43).BeginInit();
		children23.Add(image43);
		Image image44 = (image3 = image42);
		context.PushParent(image3);
		Image image45 = image3;
		image45.Classes.Add("imgactive");
		SvgImageExtension svgImageExtension11 = new SvgImageExtension("/Assets/Images/ic_layout4s.svg");
		context.ProvideTargetProperty = Image.SourceProperty;
		object obj21 = svgImageExtension11.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_11(image45, obj21);
		StyledProperty<bool> isVisibleProperty12 = Visual.IsVisibleProperty;
		ReflectionBindingExtension obj22 = new ReflectionBindingExtension("!IsPbz")
		{
			Source = ThemeManager.Instance
		};
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		ReflectionBinding binding16 = obj22.ProvideValue(context);
		context.ProvideTargetProperty = null;
		image45.Bind(isVisibleProperty12, binding16);
		context.PopParent();
		((ISupportInitialize)image44).EndInit();
		Avalonia.Controls.Controls children24 = panel17.Children;
		Image image46;
		Image image47 = (image46 = new Image());
		((ISupportInitialize)image47).BeginInit();
		children24.Add(image47);
		Image image48 = (image3 = image46);
		context.PushParent(image3);
		Image image49 = image3;
		SvgImageExtension svgImageExtension12 = new SvgImageExtension("/Assets/Images/pbz/pbz_layout4.svg");
		context.ProvideTargetProperty = Image.SourceProperty;
		object obj23 = svgImageExtension12.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_11(image49, obj23);
		StyledProperty<bool> isVisibleProperty13 = Visual.IsVisibleProperty;
		ReflectionBindingExtension obj24 = new ReflectionBindingExtension("IsPbz")
		{
			Source = ThemeManager.Instance
		};
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		ReflectionBinding binding17 = obj24.ProvideValue(context);
		context.ProvideTargetProperty = null;
		image49.Bind(isVisibleProperty13, binding17);
		context.PopParent();
		((ISupportInitialize)image48).EndInit();
		context.PopParent();
		((ISupportInitialize)panel16).EndInit();
		context.PopParent();
		((ISupportInitialize)border16).EndInit();
		Avalonia.Controls.Controls children25 = grid33.Children;
		TextBlock textBlock9;
		TextBlock textBlock10 = (textBlock9 = new TextBlock());
		((ISupportInitialize)textBlock10).BeginInit();
		children25.Add(textBlock10);
		Grid.SetRow(textBlock9, 1);
		textBlock9.Margin = new Thickness(0.0, 8.0, 0.0, 0.0);
		textBlock9.Classes.Add("base");
		textBlock9.Classes.Add("cardname");
		textBlock9.Text = SpaceWalker.Assets.Languages.Resources.ThreeStackedDisplays;
		((ISupportInitialize)textBlock9).EndInit();
		context.PopParent();
		((ISupportInitialize)grid32).EndInit();
		context.PopParent();
		((ISupportInitialize)listBoxItem16).EndInit();
		ItemCollection items5 = listBox5.Items;
		ListBoxItem listBoxItem18;
		ListBoxItem listBoxItem19 = (listBoxItem18 = new ListBoxItem());
		((ISupportInitialize)listBoxItem19).BeginInit();
		items5.Add(listBoxItem19);
		ListBoxItem listBoxItem20 = (listBoxItem3 = listBoxItem18);
		context.PushParent(listBoxItem3);
		ListBoxItem listBoxItem21 = listBoxItem3;
		StyledProperty<bool> isSelectedProperty5 = ListBoxItem.IsSelectedProperty;
		ReflectionBindingExtension reflectionBindingExtension6 = new ReflectionBindingExtension("ViewModel.VitureLayoutMode");
		reflectionBindingExtension6.Mode = BindingMode.TwoWay;
		reflectionBindingExtension6.Converter = EqualsToBooleanConverter.Instance;
		reflectionBindingExtension6.ConverterParameter = VitureLayoutMode.UltraWide;
		context.ProvideTargetProperty = ListBoxItem.IsSelectedProperty;
		ReflectionBinding binding18 = reflectionBindingExtension6.ProvideValue(context);
		context.ProvideTargetProperty = null;
		listBoxItem21.Bind(isSelectedProperty5, binding18);
		Grid grid34;
		Grid grid35 = (grid34 = new Grid());
		((ISupportInitialize)grid35).BeginInit();
		listBoxItem21.Content = grid35;
		Grid grid36 = (grid3 = grid34);
		context.PushParent(grid3);
		Grid grid37 = grid3;
		RowDefinitions rowDefinitions7 = new RowDefinitions();
		rowDefinitions7.Capacity = 2;
		rowDefinitions7.Add(new RowDefinition(new GridLength(1.0, GridUnitType.Star)));
		rowDefinitions7.Add(new RowDefinition(new GridLength(0.0, GridUnitType.Auto)));
		grid37.RowDefinitions = rowDefinitions7;
		Avalonia.Controls.Controls children26 = grid37.Children;
		Border border18;
		Border border19 = (border18 = new Border());
		((ISupportInitialize)border19).BeginInit();
		children26.Add(border19);
		Border border20 = (border3 = border18);
		context.PushParent(border3);
		Border border21 = border3;
		border21.Classes.Add("thumbclip");
		Panel panel18;
		Panel panel19 = (panel18 = new Panel());
		((ISupportInitialize)panel19).BeginInit();
		border21.Child = panel19;
		Panel panel20 = (panel3 = panel18);
		context.PushParent(panel3);
		Panel panel21 = panel3;
		Avalonia.Controls.Controls children27 = panel21.Children;
		Image image50;
		Image image51 = (image50 = new Image());
		((ISupportInitialize)image51).BeginInit();
		children27.Add(image51);
		Image image52 = (image3 = image50);
		context.PushParent(image3);
		Image image53 = image3;
		image53.Classes.Add("imgnormal");
		SvgImageExtension svgImageExtension13 = new SvgImageExtension("/Assets/Images/ic_layout5n.svg");
		context.ProvideTargetProperty = Image.SourceProperty;
		object obj25 = svgImageExtension13.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_11(image53, obj25);
		StyledProperty<bool> isVisibleProperty14 = Visual.IsVisibleProperty;
		ReflectionBindingExtension obj26 = new ReflectionBindingExtension("!IsPbz")
		{
			Source = ThemeManager.Instance
		};
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		ReflectionBinding binding19 = obj26.ProvideValue(context);
		context.ProvideTargetProperty = null;
		image53.Bind(isVisibleProperty14, binding19);
		context.PopParent();
		((ISupportInitialize)image52).EndInit();
		Avalonia.Controls.Controls children28 = panel21.Children;
		Image image54;
		Image image55 = (image54 = new Image());
		((ISupportInitialize)image55).BeginInit();
		children28.Add(image55);
		Image image56 = (image3 = image54);
		context.PushParent(image3);
		Image image57 = image3;
		image57.Classes.Add("imgactive");
		SvgImageExtension svgImageExtension14 = new SvgImageExtension("/Assets/Images/ic_layout5s.svg");
		context.ProvideTargetProperty = Image.SourceProperty;
		object obj27 = svgImageExtension14.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_11(image57, obj27);
		StyledProperty<bool> isVisibleProperty15 = Visual.IsVisibleProperty;
		ReflectionBindingExtension obj28 = new ReflectionBindingExtension("!IsPbz")
		{
			Source = ThemeManager.Instance
		};
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		ReflectionBinding binding20 = obj28.ProvideValue(context);
		context.ProvideTargetProperty = null;
		image57.Bind(isVisibleProperty15, binding20);
		context.PopParent();
		((ISupportInitialize)image56).EndInit();
		Avalonia.Controls.Controls children29 = panel21.Children;
		Image image58;
		Image image59 = (image58 = new Image());
		((ISupportInitialize)image59).BeginInit();
		children29.Add(image59);
		Image image60 = (image3 = image58);
		context.PushParent(image3);
		Image image61 = image3;
		SvgImageExtension svgImageExtension15 = new SvgImageExtension("/Assets/Images/pbz/pbz_layout5.svg");
		context.ProvideTargetProperty = Image.SourceProperty;
		object obj29 = svgImageExtension15.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_11(image61, obj29);
		StyledProperty<bool> isVisibleProperty16 = Visual.IsVisibleProperty;
		ReflectionBindingExtension obj30 = new ReflectionBindingExtension("IsPbz")
		{
			Source = ThemeManager.Instance
		};
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		ReflectionBinding binding21 = obj30.ProvideValue(context);
		context.ProvideTargetProperty = null;
		image61.Bind(isVisibleProperty16, binding21);
		context.PopParent();
		((ISupportInitialize)image60).EndInit();
		Avalonia.Controls.Controls children30 = panel21.Children;
		TextBlock textBlock11;
		TextBlock textBlock12 = (textBlock11 = new TextBlock());
		((ISupportInitialize)textBlock12).BeginInit();
		children30.Add(textBlock12);
		TextBlock textBlock13;
		TextBlock textBlock14 = (textBlock13 = textBlock11);
		context.PushParent(textBlock13);
		textBlock13.Name = "PART_UltraWideRatio";
		element = textBlock13;
		context.AvaloniaNameScope.Register("PART_UltraWideRatio", element);
		textBlock13.Classes.Add("base");
		textBlock13.FontSize = 18.0;
		textBlock13.HorizontalAlignment = HorizontalAlignment.Center;
		textBlock13.VerticalAlignment = VerticalAlignment.Center;
		StyledProperty<string?> textProperty = TextBlock.TextProperty;
		ReflectionBindingExtension reflectionBindingExtension7 = new ReflectionBindingExtension("UltraWideRatio");
		context.ProvideTargetProperty = TextBlock.TextProperty;
		ReflectionBinding binding22 = reflectionBindingExtension7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		textBlock13.Bind(textProperty, binding22);
		context.PopParent();
		((ISupportInitialize)textBlock14).EndInit();
		context.PopParent();
		((ISupportInitialize)panel20).EndInit();
		context.PopParent();
		((ISupportInitialize)border20).EndInit();
		Avalonia.Controls.Controls children31 = grid37.Children;
		TextBlock textBlock15;
		TextBlock textBlock16 = (textBlock15 = new TextBlock());
		((ISupportInitialize)textBlock16).BeginInit();
		children31.Add(textBlock16);
		Grid.SetRow(textBlock15, 1);
		textBlock15.Margin = new Thickness(0.0, 8.0, 0.0, 0.0);
		textBlock15.Classes.Add("base");
		textBlock15.Classes.Add("cardname");
		textBlock15.Text = SpaceWalker.Assets.Languages.Resources.UltraWide;
		((ISupportInitialize)textBlock15).EndInit();
		context.PopParent();
		((ISupportInitialize)grid36).EndInit();
		context.PopParent();
		((ISupportInitialize)listBoxItem20).EndInit();
		ItemCollection items6 = listBox5.Items;
		ListBoxItem listBoxItem22;
		ListBoxItem listBoxItem23 = (listBoxItem22 = new ListBoxItem());
		((ISupportInitialize)listBoxItem23).BeginInit();
		items6.Add(listBoxItem23);
		ListBoxItem listBoxItem24 = (listBoxItem3 = listBoxItem22);
		context.PushParent(listBoxItem3);
		ListBoxItem listBoxItem25 = listBoxItem3;
		StyledProperty<bool> isSelectedProperty6 = ListBoxItem.IsSelectedProperty;
		ReflectionBindingExtension reflectionBindingExtension8 = new ReflectionBindingExtension("ViewModel.VitureLayoutMode");
		reflectionBindingExtension8.Mode = BindingMode.TwoWay;
		reflectionBindingExtension8.Converter = EqualsToBooleanConverter.Instance;
		reflectionBindingExtension8.ConverterParameter = VitureLayoutMode.HorizontalPortrait;
		context.ProvideTargetProperty = ListBoxItem.IsSelectedProperty;
		ReflectionBinding binding23 = reflectionBindingExtension8.ProvideValue(context);
		context.ProvideTargetProperty = null;
		listBoxItem25.Bind(isSelectedProperty6, binding23);
		Grid grid38;
		Grid grid39 = (grid38 = new Grid());
		((ISupportInitialize)grid39).BeginInit();
		listBoxItem25.Content = grid39;
		Grid grid40 = (grid3 = grid38);
		context.PushParent(grid3);
		Grid grid41 = grid3;
		RowDefinitions rowDefinitions8 = new RowDefinitions();
		rowDefinitions8.Capacity = 2;
		rowDefinitions8.Add(new RowDefinition(new GridLength(1.0, GridUnitType.Star)));
		rowDefinitions8.Add(new RowDefinition(new GridLength(0.0, GridUnitType.Auto)));
		grid41.RowDefinitions = rowDefinitions8;
		Avalonia.Controls.Controls children32 = grid41.Children;
		Border border22;
		Border border23 = (border22 = new Border());
		((ISupportInitialize)border23).BeginInit();
		children32.Add(border23);
		Border border24 = (border3 = border22);
		context.PushParent(border3);
		Border border25 = border3;
		border25.Classes.Add("thumbclip");
		Panel panel22;
		Panel panel23 = (panel22 = new Panel());
		((ISupportInitialize)panel23).BeginInit();
		border25.Child = panel23;
		Panel panel24 = (panel3 = panel22);
		context.PushParent(panel3);
		Panel panel25 = panel3;
		Avalonia.Controls.Controls children33 = panel25.Children;
		Image image62;
		Image image63 = (image62 = new Image());
		((ISupportInitialize)image63).BeginInit();
		children33.Add(image63);
		Image image64 = (image3 = image62);
		context.PushParent(image3);
		Image image65 = image3;
		image65.Classes.Add("imgnormal");
		SvgImageExtension svgImageExtension16 = new SvgImageExtension("/Assets/Images/ic_layout6n.svg");
		context.ProvideTargetProperty = Image.SourceProperty;
		object obj31 = svgImageExtension16.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_11(image65, obj31);
		StyledProperty<bool> isVisibleProperty17 = Visual.IsVisibleProperty;
		ReflectionBindingExtension obj32 = new ReflectionBindingExtension("!IsPbz")
		{
			Source = ThemeManager.Instance
		};
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		ReflectionBinding binding24 = obj32.ProvideValue(context);
		context.ProvideTargetProperty = null;
		image65.Bind(isVisibleProperty17, binding24);
		context.PopParent();
		((ISupportInitialize)image64).EndInit();
		Avalonia.Controls.Controls children34 = panel25.Children;
		Image image66;
		Image image67 = (image66 = new Image());
		((ISupportInitialize)image67).BeginInit();
		children34.Add(image67);
		Image image68 = (image3 = image66);
		context.PushParent(image3);
		Image image69 = image3;
		image69.Classes.Add("imgactive");
		SvgImageExtension svgImageExtension17 = new SvgImageExtension("/Assets/Images/ic_layout6s.svg");
		context.ProvideTargetProperty = Image.SourceProperty;
		object obj33 = svgImageExtension17.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_11(image69, obj33);
		StyledProperty<bool> isVisibleProperty18 = Visual.IsVisibleProperty;
		ReflectionBindingExtension obj34 = new ReflectionBindingExtension("!IsPbz")
		{
			Source = ThemeManager.Instance
		};
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		ReflectionBinding binding25 = obj34.ProvideValue(context);
		context.ProvideTargetProperty = null;
		image69.Bind(isVisibleProperty18, binding25);
		context.PopParent();
		((ISupportInitialize)image68).EndInit();
		Avalonia.Controls.Controls children35 = panel25.Children;
		Image image70;
		Image image71 = (image70 = new Image());
		((ISupportInitialize)image71).BeginInit();
		children35.Add(image71);
		Image image72 = (image3 = image70);
		context.PushParent(image3);
		Image image73 = image3;
		SvgImageExtension svgImageExtension18 = new SvgImageExtension("/Assets/Images/pbz/pbz_layout6.svg");
		context.ProvideTargetProperty = Image.SourceProperty;
		object obj35 = svgImageExtension18.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_11(image73, obj35);
		StyledProperty<bool> isVisibleProperty19 = Visual.IsVisibleProperty;
		ReflectionBindingExtension obj36 = new ReflectionBindingExtension("IsPbz")
		{
			Source = ThemeManager.Instance
		};
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		ReflectionBinding binding26 = obj36.ProvideValue(context);
		context.ProvideTargetProperty = null;
		image73.Bind(isVisibleProperty19, binding26);
		context.PopParent();
		((ISupportInitialize)image72).EndInit();
		context.PopParent();
		((ISupportInitialize)panel24).EndInit();
		context.PopParent();
		((ISupportInitialize)border24).EndInit();
		Avalonia.Controls.Controls children36 = grid41.Children;
		TextBlock textBlock17;
		TextBlock textBlock18 = (textBlock17 = new TextBlock());
		((ISupportInitialize)textBlock18).BeginInit();
		children36.Add(textBlock18);
		Grid.SetRow(textBlock17, 1);
		textBlock17.Margin = new Thickness(0.0, 8.0, 0.0, 0.0);
		textBlock17.Classes.Add("base");
		textBlock17.Classes.Add("cardname");
		textBlock17.Text = SpaceWalker.Assets.Languages.Resources.PortraitLandscapePortrait;
		((ISupportInitialize)textBlock17).EndInit();
		context.PopParent();
		((ISupportInitialize)grid40).EndInit();
		context.PopParent();
		((ISupportInitialize)listBoxItem24).EndInit();
		context.PopParent();
		((ISupportInitialize)listBox4).EndInit();
		Avalonia.Controls.Controls children37 = grid17.Children;
		ListBox listBox6;
		ListBox listBox7 = (listBox6 = new ListBox());
		((ISupportInitialize)listBox7).BeginInit();
		children37.Add(listBox7);
		ListBox listBox8 = (listBox3 = listBox6);
		context.PushParent(listBox3);
		ListBox listBox9 = listBox3;
		listBox9.Name = "LayoutListBoxR6";
		element = listBox9;
		context.AvaloniaNameScope.Register("LayoutListBoxR6", element);
		listBox9.SelectionMode = SelectionMode.Single;
		listBox9.Background = new ImmutableSolidColorBrush(16777215u);
		listBox9.Classes.Add("void");
		listBox9.Classes.Add("layoutcards");
		ScrollViewer.SetHorizontalScrollBarVisibility(listBox9, ScrollBarVisibility.Disabled);
		ScrollViewer.SetVerticalScrollBarVisibility(listBox9, ScrollBarVisibility.Disabled);
		StyledProperty<bool> isVisibleProperty20 = Visual.IsVisibleProperty;
		ReflectionBindingExtension reflectionBindingExtension9 = new ReflectionBindingExtension("!Show6Layout");
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		ReflectionBinding binding27 = reflectionBindingExtension9.ProvideValue(context);
		context.ProvideTargetProperty = null;
		listBox9.Bind(isVisibleProperty20, binding27);
		listBox9.ItemsPanel = new ItemsPanelTemplate
		{
			Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_14.Build_4), context)
		};
		ItemCollection items7 = listBox9.Items;
		ListBoxItem listBoxItem26;
		ListBoxItem listBoxItem27 = (listBoxItem26 = new ListBoxItem());
		((ISupportInitialize)listBoxItem27).BeginInit();
		items7.Add(listBoxItem27);
		ListBoxItem listBoxItem28 = (listBoxItem3 = listBoxItem26);
		context.PushParent(listBoxItem3);
		ListBoxItem listBoxItem29 = listBoxItem3;
		StyledProperty<bool> isSelectedProperty7 = ListBoxItem.IsSelectedProperty;
		ReflectionBindingExtension reflectionBindingExtension10 = new ReflectionBindingExtension("ViewModel.VitureLayoutMode");
		reflectionBindingExtension10.Mode = BindingMode.TwoWay;
		reflectionBindingExtension10.Converter = EqualsToBooleanConverter.Instance;
		reflectionBindingExtension10.ConverterParameter = VitureLayoutMode.Horizontal1A;
		context.ProvideTargetProperty = ListBoxItem.IsSelectedProperty;
		ReflectionBinding binding28 = reflectionBindingExtension10.ProvideValue(context);
		context.ProvideTargetProperty = null;
		listBoxItem29.Bind(isSelectedProperty7, binding28);
		Grid grid42;
		Grid grid43 = (grid42 = new Grid());
		((ISupportInitialize)grid43).BeginInit();
		listBoxItem29.Content = grid43;
		Grid grid44 = (grid3 = grid42);
		context.PushParent(grid3);
		Grid grid45 = grid3;
		RowDefinitions rowDefinitions9 = new RowDefinitions();
		rowDefinitions9.Capacity = 2;
		rowDefinitions9.Add(new RowDefinition(new GridLength(1.0, GridUnitType.Star)));
		rowDefinitions9.Add(new RowDefinition(new GridLength(0.0, GridUnitType.Auto)));
		grid45.RowDefinitions = rowDefinitions9;
		Avalonia.Controls.Controls children38 = grid45.Children;
		Border border26;
		Border border27 = (border26 = new Border());
		((ISupportInitialize)border27).BeginInit();
		children38.Add(border27);
		Border border28 = (border3 = border26);
		context.PushParent(border3);
		Border border29 = border3;
		border29.Classes.Add("thumbclip");
		Panel panel26;
		Panel panel27 = (panel26 = new Panel());
		((ISupportInitialize)panel27).BeginInit();
		border29.Child = panel27;
		Panel panel28 = (panel3 = panel26);
		context.PushParent(panel3);
		Panel panel29 = panel3;
		Avalonia.Controls.Controls children39 = panel29.Children;
		Image image74;
		Image image75 = (image74 = new Image());
		((ISupportInitialize)image75).BeginInit();
		children39.Add(image75);
		Image image76 = (image3 = image74);
		context.PushParent(image3);
		Image image77 = image3;
		image77.Classes.Add("imgnormal");
		SvgImageExtension svgImageExtension19 = new SvgImageExtension("/Assets/Images/ic_layout1n.svg");
		context.ProvideTargetProperty = Image.SourceProperty;
		object obj37 = svgImageExtension19.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_11(image77, obj37);
		StyledProperty<bool> isVisibleProperty21 = Visual.IsVisibleProperty;
		ReflectionBindingExtension obj38 = new ReflectionBindingExtension("!IsPbz")
		{
			Source = ThemeManager.Instance
		};
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		ReflectionBinding binding29 = obj38.ProvideValue(context);
		context.ProvideTargetProperty = null;
		image77.Bind(isVisibleProperty21, binding29);
		context.PopParent();
		((ISupportInitialize)image76).EndInit();
		Avalonia.Controls.Controls children40 = panel29.Children;
		Image image78;
		Image image79 = (image78 = new Image());
		((ISupportInitialize)image79).BeginInit();
		children40.Add(image79);
		Image image80 = (image3 = image78);
		context.PushParent(image3);
		Image image81 = image3;
		image81.Classes.Add("imgactive");
		SvgImageExtension svgImageExtension20 = new SvgImageExtension("/Assets/Images/ic_layout1s.svg");
		context.ProvideTargetProperty = Image.SourceProperty;
		object obj39 = svgImageExtension20.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_11(image81, obj39);
		StyledProperty<bool> isVisibleProperty22 = Visual.IsVisibleProperty;
		ReflectionBindingExtension obj40 = new ReflectionBindingExtension("!IsPbz")
		{
			Source = ThemeManager.Instance
		};
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		ReflectionBinding binding30 = obj40.ProvideValue(context);
		context.ProvideTargetProperty = null;
		image81.Bind(isVisibleProperty22, binding30);
		context.PopParent();
		((ISupportInitialize)image80).EndInit();
		Avalonia.Controls.Controls children41 = panel29.Children;
		Image image82;
		Image image83 = (image82 = new Image());
		((ISupportInitialize)image83).BeginInit();
		children41.Add(image83);
		Image image84 = (image3 = image82);
		context.PushParent(image3);
		Image image85 = image3;
		SvgImageExtension svgImageExtension21 = new SvgImageExtension("/Assets/Images/pbz/pbz_layout1.svg");
		context.ProvideTargetProperty = Image.SourceProperty;
		object obj41 = svgImageExtension21.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_11(image85, obj41);
		StyledProperty<bool> isVisibleProperty23 = Visual.IsVisibleProperty;
		ReflectionBindingExtension obj42 = new ReflectionBindingExtension("IsPbz")
		{
			Source = ThemeManager.Instance
		};
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		ReflectionBinding binding31 = obj42.ProvideValue(context);
		context.ProvideTargetProperty = null;
		image85.Bind(isVisibleProperty23, binding31);
		context.PopParent();
		((ISupportInitialize)image84).EndInit();
		context.PopParent();
		((ISupportInitialize)panel28).EndInit();
		context.PopParent();
		((ISupportInitialize)border28).EndInit();
		Avalonia.Controls.Controls children42 = grid45.Children;
		TextBlock textBlock19;
		TextBlock textBlock20 = (textBlock19 = new TextBlock());
		((ISupportInitialize)textBlock20).BeginInit();
		children42.Add(textBlock20);
		Grid.SetRow(textBlock19, 1);
		textBlock19.Margin = new Thickness(0.0, 8.0, 0.0, 0.0);
		textBlock19.Classes.Add("base");
		textBlock19.Classes.Add("cardname");
		textBlock19.Text = SpaceWalker.Assets.Languages.Resources.SingleDisplay;
		((ISupportInitialize)textBlock19).EndInit();
		context.PopParent();
		((ISupportInitialize)grid44).EndInit();
		context.PopParent();
		((ISupportInitialize)listBoxItem28).EndInit();
		ItemCollection items8 = listBox9.Items;
		ListBoxItem listBoxItem30;
		ListBoxItem listBoxItem31 = (listBoxItem30 = new ListBoxItem());
		((ISupportInitialize)listBoxItem31).BeginInit();
		items8.Add(listBoxItem31);
		ListBoxItem listBoxItem32 = (listBoxItem3 = listBoxItem30);
		context.PushParent(listBoxItem3);
		ListBoxItem listBoxItem33 = listBoxItem3;
		StyledProperty<bool> isSelectedProperty8 = ListBoxItem.IsSelectedProperty;
		ReflectionBindingExtension reflectionBindingExtension11 = new ReflectionBindingExtension("ViewModel.VitureLayoutMode");
		reflectionBindingExtension11.Mode = BindingMode.TwoWay;
		reflectionBindingExtension11.Converter = EqualsToBooleanConverter.Instance;
		reflectionBindingExtension11.ConverterParameter = VitureLayoutMode.UltraWideA;
		context.ProvideTargetProperty = ListBoxItem.IsSelectedProperty;
		ReflectionBinding binding32 = reflectionBindingExtension11.ProvideValue(context);
		context.ProvideTargetProperty = null;
		listBoxItem33.Bind(isSelectedProperty8, binding32);
		Grid grid46;
		Grid grid47 = (grid46 = new Grid());
		((ISupportInitialize)grid47).BeginInit();
		listBoxItem33.Content = grid47;
		Grid grid48 = (grid3 = grid46);
		context.PushParent(grid3);
		Grid grid49 = grid3;
		RowDefinitions rowDefinitions10 = new RowDefinitions();
		rowDefinitions10.Capacity = 2;
		rowDefinitions10.Add(new RowDefinition(new GridLength(1.0, GridUnitType.Star)));
		rowDefinitions10.Add(new RowDefinition(new GridLength(0.0, GridUnitType.Auto)));
		grid49.RowDefinitions = rowDefinitions10;
		Avalonia.Controls.Controls children43 = grid49.Children;
		Border border30;
		Border border31 = (border30 = new Border());
		((ISupportInitialize)border31).BeginInit();
		children43.Add(border31);
		Border border32 = (border3 = border30);
		context.PushParent(border3);
		Border border33 = border3;
		border33.Classes.Add("thumbclip");
		Panel panel30;
		Panel panel31 = (panel30 = new Panel());
		((ISupportInitialize)panel31).BeginInit();
		border33.Child = panel31;
		Panel panel32 = (panel3 = panel30);
		context.PushParent(panel3);
		Panel panel33 = panel3;
		Avalonia.Controls.Controls children44 = panel33.Children;
		Image image86;
		Image image87 = (image86 = new Image());
		((ISupportInitialize)image87).BeginInit();
		children44.Add(image87);
		Image image88 = (image3 = image86);
		context.PushParent(image3);
		Image image89 = image3;
		image89.Classes.Add("imgnormal");
		SvgImageExtension svgImageExtension22 = new SvgImageExtension("/Assets/Images/ic_layout5n.svg");
		context.ProvideTargetProperty = Image.SourceProperty;
		object obj43 = svgImageExtension22.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_11(image89, obj43);
		StyledProperty<bool> isVisibleProperty24 = Visual.IsVisibleProperty;
		ReflectionBindingExtension obj44 = new ReflectionBindingExtension("!IsPbz")
		{
			Source = ThemeManager.Instance
		};
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		ReflectionBinding binding33 = obj44.ProvideValue(context);
		context.ProvideTargetProperty = null;
		image89.Bind(isVisibleProperty24, binding33);
		context.PopParent();
		((ISupportInitialize)image88).EndInit();
		Avalonia.Controls.Controls children45 = panel33.Children;
		Image image90;
		Image image91 = (image90 = new Image());
		((ISupportInitialize)image91).BeginInit();
		children45.Add(image91);
		Image image92 = (image3 = image90);
		context.PushParent(image3);
		Image image93 = image3;
		image93.Classes.Add("imgactive");
		SvgImageExtension svgImageExtension23 = new SvgImageExtension("/Assets/Images/ic_layout5s.svg");
		context.ProvideTargetProperty = Image.SourceProperty;
		object obj45 = svgImageExtension23.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_11(image93, obj45);
		StyledProperty<bool> isVisibleProperty25 = Visual.IsVisibleProperty;
		ReflectionBindingExtension obj46 = new ReflectionBindingExtension("!IsPbz")
		{
			Source = ThemeManager.Instance
		};
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		ReflectionBinding binding34 = obj46.ProvideValue(context);
		context.ProvideTargetProperty = null;
		image93.Bind(isVisibleProperty25, binding34);
		context.PopParent();
		((ISupportInitialize)image92).EndInit();
		Avalonia.Controls.Controls children46 = panel33.Children;
		Image image94;
		Image image95 = (image94 = new Image());
		((ISupportInitialize)image95).BeginInit();
		children46.Add(image95);
		Image image96 = (image3 = image94);
		context.PushParent(image3);
		Image image97 = image3;
		SvgImageExtension svgImageExtension24 = new SvgImageExtension("/Assets/Images/pbz/pbz_layout5.svg");
		context.ProvideTargetProperty = Image.SourceProperty;
		object obj47 = svgImageExtension24.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_11(image97, obj47);
		StyledProperty<bool> isVisibleProperty26 = Visual.IsVisibleProperty;
		ReflectionBindingExtension obj48 = new ReflectionBindingExtension("IsPbz")
		{
			Source = ThemeManager.Instance
		};
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		ReflectionBinding binding35 = obj48.ProvideValue(context);
		context.ProvideTargetProperty = null;
		image97.Bind(isVisibleProperty26, binding35);
		context.PopParent();
		((ISupportInitialize)image96).EndInit();
		context.PopParent();
		((ISupportInitialize)panel32).EndInit();
		context.PopParent();
		((ISupportInitialize)border32).EndInit();
		Avalonia.Controls.Controls children47 = grid49.Children;
		TextBlock textBlock21;
		TextBlock textBlock22 = (textBlock21 = new TextBlock());
		((ISupportInitialize)textBlock22).BeginInit();
		children47.Add(textBlock22);
		Grid.SetRow(textBlock21, 1);
		textBlock21.Margin = new Thickness(0.0, 8.0, 0.0, 0.0);
		textBlock21.Classes.Add("base");
		textBlock21.Classes.Add("cardname");
		textBlock21.Text = SpaceWalker.Assets.Languages.Resources.UltraWide;
		((ISupportInitialize)textBlock21).EndInit();
		context.PopParent();
		((ISupportInitialize)grid48).EndInit();
		context.PopParent();
		((ISupportInitialize)listBoxItem32).EndInit();
		ItemCollection items9 = listBox9.Items;
		ListBoxItem listBoxItem34;
		ListBoxItem listBoxItem35 = (listBoxItem34 = new ListBoxItem());
		((ISupportInitialize)listBoxItem35).BeginInit();
		items9.Add(listBoxItem35);
		ListBoxItem listBoxItem36 = (listBoxItem3 = listBoxItem34);
		context.PushParent(listBoxItem3);
		ListBoxItem listBoxItem37 = listBoxItem3;
		StyledProperty<bool> isSelectedProperty9 = ListBoxItem.IsSelectedProperty;
		ReflectionBindingExtension reflectionBindingExtension12 = new ReflectionBindingExtension("ViewModel.VitureLayoutMode");
		reflectionBindingExtension12.Mode = BindingMode.TwoWay;
		reflectionBindingExtension12.Converter = EqualsToBooleanConverter.Instance;
		reflectionBindingExtension12.ConverterParameter = VitureLayoutMode.Horizontal3A;
		context.ProvideTargetProperty = ListBoxItem.IsSelectedProperty;
		ReflectionBinding binding36 = reflectionBindingExtension12.ProvideValue(context);
		context.ProvideTargetProperty = null;
		listBoxItem37.Bind(isSelectedProperty9, binding36);
		Grid grid50;
		Grid grid51 = (grid50 = new Grid());
		((ISupportInitialize)grid51).BeginInit();
		listBoxItem37.Content = grid51;
		Grid grid52 = (grid3 = grid50);
		context.PushParent(grid3);
		Grid grid53 = grid3;
		RowDefinitions rowDefinitions11 = new RowDefinitions();
		rowDefinitions11.Capacity = 2;
		rowDefinitions11.Add(new RowDefinition(new GridLength(1.0, GridUnitType.Star)));
		rowDefinitions11.Add(new RowDefinition(new GridLength(0.0, GridUnitType.Auto)));
		grid53.RowDefinitions = rowDefinitions11;
		Avalonia.Controls.Controls children48 = grid53.Children;
		Border border34;
		Border border35 = (border34 = new Border());
		((ISupportInitialize)border35).BeginInit();
		children48.Add(border35);
		Border border36 = (border3 = border34);
		context.PushParent(border3);
		Border border37 = border3;
		border37.Classes.Add("thumbclip");
		Panel panel34;
		Panel panel35 = (panel34 = new Panel());
		((ISupportInitialize)panel35).BeginInit();
		border37.Child = panel35;
		Panel panel36 = (panel3 = panel34);
		context.PushParent(panel3);
		Panel panel37 = panel3;
		Avalonia.Controls.Controls children49 = panel37.Children;
		Image image98;
		Image image99 = (image98 = new Image());
		((ISupportInitialize)image99).BeginInit();
		children49.Add(image99);
		Image image100 = (image3 = image98);
		context.PushParent(image3);
		Image image101 = image3;
		image101.Classes.Add("imgnormal");
		SvgImageExtension svgImageExtension25 = new SvgImageExtension("/Assets/Images/ic_layout3n.svg");
		context.ProvideTargetProperty = Image.SourceProperty;
		object obj49 = svgImageExtension25.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_11(image101, obj49);
		StyledProperty<bool> isVisibleProperty27 = Visual.IsVisibleProperty;
		ReflectionBindingExtension obj50 = new ReflectionBindingExtension("!IsPbz")
		{
			Source = ThemeManager.Instance
		};
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		ReflectionBinding binding37 = obj50.ProvideValue(context);
		context.ProvideTargetProperty = null;
		image101.Bind(isVisibleProperty27, binding37);
		context.PopParent();
		((ISupportInitialize)image100).EndInit();
		Avalonia.Controls.Controls children50 = panel37.Children;
		Image image102;
		Image image103 = (image102 = new Image());
		((ISupportInitialize)image103).BeginInit();
		children50.Add(image103);
		Image image104 = (image3 = image102);
		context.PushParent(image3);
		Image image105 = image3;
		image105.Classes.Add("imgactive");
		SvgImageExtension svgImageExtension26 = new SvgImageExtension("/Assets/Images/ic_layout3s.svg");
		context.ProvideTargetProperty = Image.SourceProperty;
		object obj51 = svgImageExtension26.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_11(image105, obj51);
		StyledProperty<bool> isVisibleProperty28 = Visual.IsVisibleProperty;
		ReflectionBindingExtension obj52 = new ReflectionBindingExtension("!IsPbz")
		{
			Source = ThemeManager.Instance
		};
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		ReflectionBinding binding38 = obj52.ProvideValue(context);
		context.ProvideTargetProperty = null;
		image105.Bind(isVisibleProperty28, binding38);
		context.PopParent();
		((ISupportInitialize)image104).EndInit();
		Avalonia.Controls.Controls children51 = panel37.Children;
		Image image106;
		Image image107 = (image106 = new Image());
		((ISupportInitialize)image107).BeginInit();
		children51.Add(image107);
		Image image108 = (image3 = image106);
		context.PushParent(image3);
		Image image109 = image3;
		SvgImageExtension svgImageExtension27 = new SvgImageExtension("/Assets/Images/pbz/pbz_layout3.svg");
		context.ProvideTargetProperty = Image.SourceProperty;
		object obj53 = svgImageExtension27.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_11(image109, obj53);
		StyledProperty<bool> isVisibleProperty29 = Visual.IsVisibleProperty;
		ReflectionBindingExtension obj54 = new ReflectionBindingExtension("IsPbz")
		{
			Source = ThemeManager.Instance
		};
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		ReflectionBinding binding39 = obj54.ProvideValue(context);
		context.ProvideTargetProperty = null;
		image109.Bind(isVisibleProperty29, binding39);
		context.PopParent();
		((ISupportInitialize)image108).EndInit();
		context.PopParent();
		((ISupportInitialize)panel36).EndInit();
		context.PopParent();
		((ISupportInitialize)border36).EndInit();
		Avalonia.Controls.Controls children52 = grid53.Children;
		TextBlock textBlock23;
		TextBlock textBlock24 = (textBlock23 = new TextBlock());
		((ISupportInitialize)textBlock24).BeginInit();
		children52.Add(textBlock24);
		Grid.SetRow(textBlock23, 1);
		textBlock23.Margin = new Thickness(0.0, 8.0, 0.0, 0.0);
		textBlock23.Classes.Add("base");
		textBlock23.Classes.Add("cardname");
		textBlock23.Text = SpaceWalker.Assets.Languages.Resources.ThreeDisplaysSBS;
		((ISupportInitialize)textBlock23).EndInit();
		context.PopParent();
		((ISupportInitialize)grid52).EndInit();
		context.PopParent();
		((ISupportInitialize)listBoxItem36).EndInit();
		ItemCollection items10 = listBox9.Items;
		ListBoxItem listBoxItem38;
		ListBoxItem listBoxItem39 = (listBoxItem38 = new ListBoxItem());
		((ISupportInitialize)listBoxItem39).BeginInit();
		items10.Add(listBoxItem39);
		ListBoxItem listBoxItem40 = (listBoxItem3 = listBoxItem38);
		context.PushParent(listBoxItem3);
		ListBoxItem listBoxItem41 = listBoxItem3;
		StyledProperty<bool> isSelectedProperty10 = ListBoxItem.IsSelectedProperty;
		ReflectionBindingExtension reflectionBindingExtension13 = new ReflectionBindingExtension("ViewModel.VitureLayoutMode");
		reflectionBindingExtension13.Mode = BindingMode.TwoWay;
		reflectionBindingExtension13.Converter = EqualsToBooleanConverter.Instance;
		reflectionBindingExtension13.ConverterParameter = VitureLayoutMode.Horizontal2A;
		context.ProvideTargetProperty = ListBoxItem.IsSelectedProperty;
		ReflectionBinding binding40 = reflectionBindingExtension13.ProvideValue(context);
		context.ProvideTargetProperty = null;
		listBoxItem41.Bind(isSelectedProperty10, binding40);
		Grid grid54;
		Grid grid55 = (grid54 = new Grid());
		((ISupportInitialize)grid55).BeginInit();
		listBoxItem41.Content = grid55;
		Grid grid56 = (grid3 = grid54);
		context.PushParent(grid3);
		Grid grid57 = grid3;
		RowDefinitions rowDefinitions12 = new RowDefinitions();
		rowDefinitions12.Capacity = 2;
		rowDefinitions12.Add(new RowDefinition(new GridLength(1.0, GridUnitType.Star)));
		rowDefinitions12.Add(new RowDefinition(new GridLength(0.0, GridUnitType.Auto)));
		grid57.RowDefinitions = rowDefinitions12;
		Avalonia.Controls.Controls children53 = grid57.Children;
		Border border38;
		Border border39 = (border38 = new Border());
		((ISupportInitialize)border39).BeginInit();
		children53.Add(border39);
		Border border40 = (border3 = border38);
		context.PushParent(border3);
		Border border41 = border3;
		border41.Classes.Add("thumbclip");
		Panel panel38;
		Panel panel39 = (panel38 = new Panel());
		((ISupportInitialize)panel39).BeginInit();
		border41.Child = panel39;
		Panel panel40 = (panel3 = panel38);
		context.PushParent(panel3);
		Panel panel41 = panel3;
		Avalonia.Controls.Controls children54 = panel41.Children;
		Image image110;
		Image image111 = (image110 = new Image());
		((ISupportInitialize)image111).BeginInit();
		children54.Add(image111);
		Image image112 = (image3 = image110);
		context.PushParent(image3);
		Image image113 = image3;
		image113.Classes.Add("imgnormal");
		SvgImageExtension svgImageExtension28 = new SvgImageExtension("/Assets/Images/ic_layout2n.svg");
		context.ProvideTargetProperty = Image.SourceProperty;
		object obj55 = svgImageExtension28.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_11(image113, obj55);
		StyledProperty<bool> isVisibleProperty30 = Visual.IsVisibleProperty;
		ReflectionBindingExtension obj56 = new ReflectionBindingExtension("!IsPbz")
		{
			Source = ThemeManager.Instance
		};
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		ReflectionBinding binding41 = obj56.ProvideValue(context);
		context.ProvideTargetProperty = null;
		image113.Bind(isVisibleProperty30, binding41);
		context.PopParent();
		((ISupportInitialize)image112).EndInit();
		Avalonia.Controls.Controls children55 = panel41.Children;
		Image image114;
		Image image115 = (image114 = new Image());
		((ISupportInitialize)image115).BeginInit();
		children55.Add(image115);
		Image image116 = (image3 = image114);
		context.PushParent(image3);
		Image image117 = image3;
		image117.Classes.Add("imgactive");
		SvgImageExtension svgImageExtension29 = new SvgImageExtension("/Assets/Images/ic_layout2s.svg");
		context.ProvideTargetProperty = Image.SourceProperty;
		object obj57 = svgImageExtension29.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_11(image117, obj57);
		StyledProperty<bool> isVisibleProperty31 = Visual.IsVisibleProperty;
		ReflectionBindingExtension obj58 = new ReflectionBindingExtension("!IsPbz")
		{
			Source = ThemeManager.Instance
		};
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		ReflectionBinding binding42 = obj58.ProvideValue(context);
		context.ProvideTargetProperty = null;
		image117.Bind(isVisibleProperty31, binding42);
		context.PopParent();
		((ISupportInitialize)image116).EndInit();
		Avalonia.Controls.Controls children56 = panel41.Children;
		Image image118;
		Image image119 = (image118 = new Image());
		((ISupportInitialize)image119).BeginInit();
		children56.Add(image119);
		Image image120 = (image3 = image118);
		context.PushParent(image3);
		Image image121 = image3;
		SvgImageExtension svgImageExtension30 = new SvgImageExtension("/Assets/Images/pbz/pbz_layout2.svg");
		context.ProvideTargetProperty = Image.SourceProperty;
		object obj59 = svgImageExtension30.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_11(image121, obj59);
		StyledProperty<bool> isVisibleProperty32 = Visual.IsVisibleProperty;
		ReflectionBindingExtension obj60 = new ReflectionBindingExtension("IsPbz")
		{
			Source = ThemeManager.Instance
		};
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		ReflectionBinding binding43 = obj60.ProvideValue(context);
		context.ProvideTargetProperty = null;
		image121.Bind(isVisibleProperty32, binding43);
		context.PopParent();
		((ISupportInitialize)image120).EndInit();
		context.PopParent();
		((ISupportInitialize)panel40).EndInit();
		context.PopParent();
		((ISupportInitialize)border40).EndInit();
		Avalonia.Controls.Controls children57 = grid57.Children;
		TextBlock textBlock25;
		TextBlock textBlock26 = (textBlock25 = new TextBlock());
		((ISupportInitialize)textBlock26).BeginInit();
		children57.Add(textBlock26);
		Grid.SetRow(textBlock25, 1);
		textBlock25.Margin = new Thickness(0.0, 8.0, 0.0, 0.0);
		textBlock25.Classes.Add("base");
		textBlock25.Classes.Add("cardname");
		textBlock25.Text = SpaceWalker.Assets.Languages.Resources.TwoDisplaysSBS;
		((ISupportInitialize)textBlock25).EndInit();
		context.PopParent();
		((ISupportInitialize)grid56).EndInit();
		context.PopParent();
		((ISupportInitialize)listBoxItem40).EndInit();
		context.PopParent();
		((ISupportInitialize)listBox8).EndInit();
		context.PopParent();
		((ISupportInitialize)grid16).EndInit();
		context.PopParent();
		((ISupportInitialize)grid12).EndInit();
		context.PopParent();
		((ISupportInitialize)grid8).EndInit();
		Avalonia.Controls.Controls children58 = grid5.Children;
		Grid grid58;
		Grid grid59 = (grid58 = new Grid());
		((ISupportInitialize)grid59).BeginInit();
		children58.Add(grid59);
		Grid grid60 = (grid3 = grid58);
		context.PushParent(grid3);
		Grid grid61 = grid3;
		Grid.SetRow(grid61, 1);
		ColumnDefinitions columnDefinitions = new ColumnDefinitions();
		columnDefinitions.Capacity = 2;
		columnDefinitions.Add(new ColumnDefinition(new GridLength(1.0, GridUnitType.Star)));
		columnDefinitions.Add(new ColumnDefinition(new GridLength(1.0, GridUnitType.Star)));
		grid61.ColumnDefinitions = columnDefinitions;
		grid61.Margin = new Thickness(0.0, 16.0, 0.0, 0.0);
		Avalonia.Controls.Controls children59 = grid61.Children;
		Grid grid62;
		Grid grid63 = (grid62 = new Grid());
		((ISupportInitialize)grid63).BeginInit();
		children59.Add(grid63);
		Grid grid64 = (grid3 = grid62);
		context.PushParent(grid3);
		Grid grid65 = grid3;
		Grid.SetColumn(grid65, 0);
		RowDefinitions rowDefinitions13 = new RowDefinitions();
		rowDefinitions13.Capacity = 2;
		rowDefinitions13.Add(new RowDefinition(new GridLength(0.0, GridUnitType.Auto)));
		rowDefinitions13.Add(new RowDefinition(new GridLength(0.0, GridUnitType.Auto)));
		grid65.RowDefinitions = rowDefinitions13;
		grid65.Margin = new Thickness(8.0, 0.0, 8.0, 8.0);
		Avalonia.Controls.Controls children60 = grid65.Children;
		TextBlock textBlock27;
		TextBlock textBlock28 = (textBlock27 = new TextBlock());
		((ISupportInitialize)textBlock28).BeginInit();
		children60.Add(textBlock28);
		textBlock27.Classes.Add("base");
		textBlock27.Classes.Add("texttitle");
		textBlock27.Opacity = 0.9;
		textBlock27.Margin = new Thickness(0.0, 0.0, 0.0, 12.0);
		textBlock27.Text = SpaceWalker.Assets.Languages.Resources.DisplayLayoutsHeader;
		((ISupportInitialize)textBlock27).EndInit();
		Avalonia.Controls.Controls children61 = grid65.Children;
		StackPanel stackPanel;
		StackPanel stackPanel2 = (stackPanel = new StackPanel());
		((ISupportInitialize)stackPanel2).BeginInit();
		children61.Add(stackPanel2);
		StackPanel stackPanel3;
		StackPanel stackPanel4 = (stackPanel3 = stackPanel);
		context.PushParent(stackPanel3);
		StackPanel stackPanel5 = stackPanel3;
		Grid.SetRow(stackPanel5, 1);
		stackPanel5.Orientation = Orientation.Horizontal;
		stackPanel5.Spacing = 48.0;
		stackPanel5.Margin = new Thickness(4.0, 0.0, 4.0, 0.0);
		StyledProperty<bool> isEnabledProperty = InputElement.IsEnabledProperty;
		ReflectionBindingExtension reflectionBindingExtension14 = new ReflectionBindingExtension("EnableLayoutType");
		context.ProvideTargetProperty = InputElement.IsEnabledProperty;
		ReflectionBinding binding44 = reflectionBindingExtension14.ProvideValue(context);
		context.ProvideTargetProperty = null;
		stackPanel5.Bind(isEnabledProperty, binding44);
		ReflectionBindingExtension reflectionBindingExtension15 = new ReflectionBindingExtension("LayoutTypeDisabledReason");
		context.ProvideTargetProperty = ToolTip.TipProperty;
		ReflectionBinding reflectionBinding = reflectionBindingExtension15.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_13((ToolTip)(object)stackPanel5, reflectionBinding);
		ToolTip.SetShowDelay(stackPanel5, 0);
		ToolTip.SetShowOnDisabled(stackPanel5, value: true);
		Avalonia.Controls.Controls children62 = stackPanel5.Children;
		RadioButton radioButton;
		RadioButton radioButton2 = (radioButton = new RadioButton());
		((ISupportInitialize)radioButton2).BeginInit();
		children62.Add(radioButton2);
		RadioButton radioButton3;
		RadioButton radioButton4 = (radioButton3 = radioButton);
		context.PushParent(radioButton3);
		RadioButton radioButton5 = radioButton3;
		radioButton5.Classes.Add("base");
		radioButton5.Foreground = new ImmutableSolidColorBrush(uint.MaxValue);
		radioButton5.FontWeight = FontWeight.DemiBold;
		radioButton5.FontSize = 14.0;
		radioButton5.Content = SpaceWalker.Assets.Languages.Resources.ExtendDesktop;
		StyledProperty<bool?> isCheckedProperty = ToggleButton.IsCheckedProperty;
		ReflectionBindingExtension reflectionBindingExtension16 = new ReflectionBindingExtension("ViewModel.LayoutType");
		reflectionBindingExtension16.Mode = BindingMode.TwoWay;
		reflectionBindingExtension16.Converter = EqualsToBooleanConverter.Instance;
		reflectionBindingExtension16.ConverterParameter = LayoutType.Extend;
		context.ProvideTargetProperty = ToggleButton.IsCheckedProperty;
		ReflectionBinding binding45 = reflectionBindingExtension16.ProvideValue(context);
		context.ProvideTargetProperty = null;
		radioButton5.Bind(isCheckedProperty, binding45);
		context.PopParent();
		((ISupportInitialize)radioButton4).EndInit();
		Avalonia.Controls.Controls children63 = stackPanel5.Children;
		RadioButton radioButton6;
		RadioButton radioButton7 = (radioButton6 = new RadioButton());
		((ISupportInitialize)radioButton7).BeginInit();
		children63.Add(radioButton7);
		RadioButton radioButton8 = (radioButton3 = radioButton6);
		context.PushParent(radioButton3);
		RadioButton radioButton9 = radioButton3;
		radioButton9.Classes.Add("base");
		radioButton9.Foreground = new ImmutableSolidColorBrush(uint.MaxValue);
		radioButton9.FontWeight = FontWeight.DemiBold;
		radioButton9.FontSize = 14.0;
		radioButton9.Content = SpaceWalker.Assets.Languages.Resources.MirrorDisplays;
		StyledProperty<bool?> isCheckedProperty2 = ToggleButton.IsCheckedProperty;
		ReflectionBindingExtension reflectionBindingExtension17 = new ReflectionBindingExtension("ViewModel.LayoutType");
		reflectionBindingExtension17.Mode = BindingMode.TwoWay;
		reflectionBindingExtension17.Converter = EqualsToBooleanConverter.Instance;
		reflectionBindingExtension17.ConverterParameter = LayoutType.Mirror;
		context.ProvideTargetProperty = ToggleButton.IsCheckedProperty;
		ReflectionBinding binding46 = reflectionBindingExtension17.ProvideValue(context);
		context.ProvideTargetProperty = null;
		radioButton9.Bind(isCheckedProperty2, binding46);
		context.PopParent();
		((ISupportInitialize)radioButton8).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel4).EndInit();
		context.PopParent();
		((ISupportInitialize)grid64).EndInit();
		Avalonia.Controls.Controls children64 = grid61.Children;
		Grid grid66;
		Grid grid67 = (grid66 = new Grid());
		((ISupportInitialize)grid67).BeginInit();
		children64.Add(grid67);
		Grid grid68 = (grid3 = grid66);
		context.PushParent(grid3);
		Grid grid69 = grid3;
		Grid.SetColumn(grid69, 1);
		RowDefinitions rowDefinitions14 = new RowDefinitions();
		rowDefinitions14.Capacity = 2;
		rowDefinitions14.Add(new RowDefinition(new GridLength(0.0, GridUnitType.Auto)));
		rowDefinitions14.Add(new RowDefinition(new GridLength(0.0, GridUnitType.Auto)));
		grid69.RowDefinitions = rowDefinitions14;
		grid69.Margin = new Thickness(8.0, 0.0, 8.0, 8.0);
		StyledProperty<bool> isVisibleProperty33 = Visual.IsVisibleProperty;
		ReflectionBindingExtension reflectionBindingExtension18 = new ReflectionBindingExtension("ShowRefreshRate");
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		ReflectionBinding binding47 = reflectionBindingExtension18.ProvideValue(context);
		context.ProvideTargetProperty = null;
		grid69.Bind(isVisibleProperty33, binding47);
		Avalonia.Controls.Controls children65 = grid69.Children;
		TextBlock textBlock29;
		TextBlock textBlock30 = (textBlock29 = new TextBlock());
		((ISupportInitialize)textBlock30).BeginInit();
		children65.Add(textBlock30);
		textBlock29.Classes.Add("base");
		textBlock29.Classes.Add("texttitle");
		textBlock29.Opacity = 0.9;
		textBlock29.Margin = new Thickness(0.0, 0.0, 0.0, 12.0);
		textBlock29.Text = SpaceWalker.Assets.Languages.Resources.SelectRefreshRate;
		((ISupportInitialize)textBlock29).EndInit();
		Avalonia.Controls.Controls children66 = grid69.Children;
		StackPanel stackPanel6;
		StackPanel stackPanel7 = (stackPanel6 = new StackPanel());
		((ISupportInitialize)stackPanel7).BeginInit();
		children66.Add(stackPanel7);
		StackPanel stackPanel8 = (stackPanel3 = stackPanel6);
		context.PushParent(stackPanel3);
		StackPanel stackPanel9 = stackPanel3;
		stackPanel9.Orientation = Orientation.Horizontal;
		stackPanel9.Spacing = 48.0;
		stackPanel9.Margin = new Thickness(4.0, 0.0, 4.0, 0.0);
		Grid.SetRow(stackPanel9, 1);
		Avalonia.Controls.Controls children67 = stackPanel9.Children;
		RadioButton radioButton10;
		RadioButton radioButton11 = (radioButton10 = new RadioButton());
		((ISupportInitialize)radioButton11).BeginInit();
		children67.Add(radioButton11);
		RadioButton radioButton12 = (radioButton3 = radioButton10);
		context.PushParent(radioButton3);
		RadioButton radioButton13 = radioButton3;
		radioButton13.Classes.Add("base");
		radioButton13.FontWeight = FontWeight.DemiBold;
		radioButton13.FontSize = 14.0;
		radioButton13.Content = SpaceWalker.Assets.Languages.Resources.RefreshRate120Hz;
		StyledProperty<bool?> isCheckedProperty3 = ToggleButton.IsCheckedProperty;
		ReflectionBindingExtension reflectionBindingExtension19 = new ReflectionBindingExtension("ViewModel.FrameRate");
		reflectionBindingExtension19.Mode = BindingMode.TwoWay;
		reflectionBindingExtension19.Converter = EqualsToBooleanConverter.Instance;
		reflectionBindingExtension19.ConverterParameter = "120";
		context.ProvideTargetProperty = ToggleButton.IsCheckedProperty;
		ReflectionBinding binding48 = reflectionBindingExtension19.ProvideValue(context);
		context.ProvideTargetProperty = null;
		radioButton13.Bind(isCheckedProperty3, binding48);
		context.PopParent();
		((ISupportInitialize)radioButton12).EndInit();
		Avalonia.Controls.Controls children68 = stackPanel9.Children;
		RadioButton radioButton14;
		RadioButton radioButton15 = (radioButton14 = new RadioButton());
		((ISupportInitialize)radioButton15).BeginInit();
		children68.Add(radioButton15);
		RadioButton radioButton16 = (radioButton3 = radioButton14);
		context.PushParent(radioButton3);
		RadioButton radioButton17 = radioButton3;
		radioButton17.Classes.Add("base");
		radioButton17.FontWeight = FontWeight.DemiBold;
		radioButton17.FontSize = 14.0;
		radioButton17.Content = SpaceWalker.Assets.Languages.Resources.RefreshRate90Hz;
		StyledProperty<bool?> isCheckedProperty4 = ToggleButton.IsCheckedProperty;
		ReflectionBindingExtension reflectionBindingExtension20 = new ReflectionBindingExtension("ViewModel.FrameRate");
		reflectionBindingExtension20.Mode = BindingMode.TwoWay;
		reflectionBindingExtension20.Converter = EqualsToBooleanConverter.Instance;
		reflectionBindingExtension20.ConverterParameter = "90";
		context.ProvideTargetProperty = ToggleButton.IsCheckedProperty;
		ReflectionBinding binding49 = reflectionBindingExtension20.ProvideValue(context);
		context.ProvideTargetProperty = null;
		radioButton17.Bind(isCheckedProperty4, binding49);
		context.PopParent();
		((ISupportInitialize)radioButton16).EndInit();
		Avalonia.Controls.Controls children69 = stackPanel9.Children;
		RadioButton radioButton18;
		RadioButton radioButton19 = (radioButton18 = new RadioButton());
		((ISupportInitialize)radioButton19).BeginInit();
		children69.Add(radioButton19);
		RadioButton radioButton20 = (radioButton3 = radioButton18);
		context.PushParent(radioButton3);
		RadioButton radioButton21 = radioButton3;
		radioButton21.Classes.Add("base");
		radioButton21.FontWeight = FontWeight.DemiBold;
		radioButton21.FontSize = 14.0;
		radioButton21.Content = SpaceWalker.Assets.Languages.Resources.RefreshRate60Hz;
		StyledProperty<bool?> isCheckedProperty5 = ToggleButton.IsCheckedProperty;
		ReflectionBindingExtension reflectionBindingExtension21 = new ReflectionBindingExtension("ViewModel.FrameRate");
		reflectionBindingExtension21.Mode = BindingMode.TwoWay;
		reflectionBindingExtension21.Converter = EqualsToBooleanConverter.Instance;
		reflectionBindingExtension21.ConverterParameter = "60";
		context.ProvideTargetProperty = ToggleButton.IsCheckedProperty;
		ReflectionBinding binding50 = reflectionBindingExtension21.ProvideValue(context);
		context.ProvideTargetProperty = null;
		radioButton21.Bind(isCheckedProperty5, binding50);
		context.PopParent();
		((ISupportInitialize)radioButton20).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel8).EndInit();
		context.PopParent();
		((ISupportInitialize)grid68).EndInit();
		context.PopParent();
		((ISupportInitialize)grid60).EndInit();
		Avalonia.Controls.Controls children70 = grid5.Children;
		Grid grid70;
		Grid grid71 = (grid70 = new Grid());
		((ISupportInitialize)grid71).BeginInit();
		children70.Add(grid71);
		Grid grid72 = (grid3 = grid70);
		context.PushParent(grid3);
		Grid grid73 = grid3;
		Grid.SetRow(grid73, 2);
		grid73.Margin = new Thickness(0.0, 16.0, 0.0, 0.0);
		Avalonia.Controls.Controls children71 = grid73.Children;
		Button button;
		Button button2 = (button = new Button());
		((ISupportInitialize)button2).BeginInit();
		children71.Add(button2);
		Button button3;
		Button button4 = (button3 = button);
		context.PushParent(button3);
		button3.HorizontalAlignment = HorizontalAlignment.Center;
		button3.MinWidth = 130.0;
		button3.FontSize = 14.0;
		button3.Classes.Add("base");
		button3.Classes.Add("accent");
		ReflectionBindingExtension reflectionBindingExtension22 = new ReflectionBindingExtension("LaunchButtonText");
		context.ProvideTargetProperty = ContentControl.ContentProperty;
		ReflectionBinding reflectionBinding2 = reflectionBindingExtension22.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_14(button3, reflectionBinding2);
		StyledProperty<ICommand?> commandProperty = Button.CommandProperty;
		ReflectionBindingExtension reflectionBindingExtension23 = new ReflectionBindingExtension("ViewModel.LaunchCmd");
		context.ProvideTargetProperty = Button.CommandProperty;
		ReflectionBinding binding51 = reflectionBindingExtension23.ProvideValue(context);
		context.ProvideTargetProperty = null;
		button3.Bind(commandProperty, binding51);
		context.PopParent();
		((ISupportInitialize)button4).EndInit();
		context.PopParent();
		((ISupportInitialize)grid72).EndInit();
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
	private static void _0021XamlIlPopulateTrampoline(LayoutView P_0)
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
