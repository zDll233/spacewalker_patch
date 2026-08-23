using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using Avalonia;
using Avalonia.Animation;
using Avalonia.Animation.Easings;
using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Shapes;
using Avalonia.Controls.Templates;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Input;
using Avalonia.Labs.Controls;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.Converters;
using Avalonia.Markup.Xaml.MarkupExtensions;
using Avalonia.Markup.Xaml.Templates;
using Avalonia.Markup.Xaml.XamlIl.Runtime;
using Avalonia.Media;
using Avalonia.Media.Immutable;
using Avalonia.Styling;
using Avalonia.Svg.Skia;
using SpaceWalker.Assets.Languages;
using SpaceWalker.Controls;
using SpaceWalker.Helper;

namespace CompiledAvaloniaXaml;

[EditorBrowsable(EditorBrowsableState.Never)]
[CompilerGenerated]
public class _0021AvaloniaResources
{
	[CompilerGenerated]
	internal class NamespaceInfo_003A_002FApp_002Eaxaml : IAvaloniaXamlIlXmlNamespaceInfoProvider
	{
		private IReadOnlyDictionary<string, IReadOnlyList<AvaloniaXamlIlXmlNamespaceInfo>> _xmlNamespaces;

		public static IAvaloniaXamlIlXmlNamespaceInfoProvider Singleton;

		public virtual IReadOnlyDictionary<string, IReadOnlyList<AvaloniaXamlIlXmlNamespaceInfo>> XmlNamespaces
		{
			get
			{
				if (_xmlNamespaces == null)
				{
					_xmlNamespaces = CreateNamespaces();
				}
				return _xmlNamespaces;
			}
		}

		private static AvaloniaXamlIlXmlNamespaceInfo CreateNamespaceInfo(string P_0, string P_1)
		{
			return new AvaloniaXamlIlXmlNamespaceInfo
			{
				ClrNamespace = P_0,
				ClrAssemblyName = P_1
			};
		}

		private static IReadOnlyDictionary<string, IReadOnlyList<AvaloniaXamlIlXmlNamespaceInfo>> CreateNamespaces()
		{
			return new Dictionary<string, IReadOnlyList<AvaloniaXamlIlXmlNamespaceInfo>>(5)
			{
				{
					"",
					new AvaloniaXamlIlXmlNamespaceInfo[37]
					{
						CreateNamespaceInfo("Avalonia", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Animation", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Animation.Easings", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Controls", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Data", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Data.Converters", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Input", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Input.GestureRecognizers", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Input.TextInput", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Layout", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.LogicalTree", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Media", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Media.Imaging", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Media.Transformation", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Styling", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Controls", "Avalonia.Controls.DataGrid"),
						CreateNamespaceInfo("Avalonia.Controls.Collections", "Avalonia.Controls.DataGrid"),
						CreateNamespaceInfo("Avalonia.Controls.Primitives", "Avalonia.Controls.DataGrid"),
						CreateNamespaceInfo("Avalonia", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Automation", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Embedding", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Presenters", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Primitives", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Shapes", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Templates", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Notifications", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Chrome", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Documents", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Fonts.Inter", "Avalonia.Fonts.Inter"),
						CreateNamespaceInfo("Avalonia.Data", "Avalonia.Markup"),
						CreateNamespaceInfo("Avalonia.Markup.Data", "Avalonia.Markup"),
						CreateNamespaceInfo("Avalonia.Markup.Xaml.MarkupExtensions", "Avalonia.Markup.Xaml"),
						CreateNamespaceInfo("Avalonia.Markup.Xaml.Styling", "Avalonia.Markup.Xaml"),
						CreateNamespaceInfo("Avalonia.Markup.Xaml.Templates", "Avalonia.Markup.Xaml"),
						CreateNamespaceInfo("Avalonia.Themes.Fluent", "Avalonia.Themes.Fluent"),
						CreateNamespaceInfo("Avalonia.Svg.Skia", "Svg.Controls.Skia.Avalonia")
					}
				},
				{
					"x",
					new AvaloniaXamlIlXmlNamespaceInfo[0]
				},
				{
					"lang",
					new AvaloniaXamlIlXmlNamespaceInfo[1] { CreateNamespaceInfo("SpaceWalker.Assets.Languages", "SpaceWalker") }
				},
				{
					"labs",
					new AvaloniaXamlIlXmlNamespaceInfo[1] { CreateNamespaceInfo("Avalonia.Labs.Controls", null) }
				},
				{
					"helper",
					new AvaloniaXamlIlXmlNamespaceInfo[1] { CreateNamespaceInfo("SpaceWalker.Helper", null) }
				}
			};
		}

		static NamespaceInfo_003A_002FApp_002Eaxaml()
		{
			Singleton = new NamespaceInfo_003A_002FApp_002Eaxaml();
		}
	}

	[CompilerGenerated]
	internal class NamespaceInfo_003A_002FControls_002FButton_002Eaxaml : IAvaloniaXamlIlXmlNamespaceInfoProvider
	{
		private IReadOnlyDictionary<string, IReadOnlyList<AvaloniaXamlIlXmlNamespaceInfo>> _xmlNamespaces;

		public static IAvaloniaXamlIlXmlNamespaceInfoProvider Singleton;

		public virtual IReadOnlyDictionary<string, IReadOnlyList<AvaloniaXamlIlXmlNamespaceInfo>> XmlNamespaces
		{
			get
			{
				if (_xmlNamespaces == null)
				{
					_xmlNamespaces = CreateNamespaces();
				}
				return _xmlNamespaces;
			}
		}

		private static AvaloniaXamlIlXmlNamespaceInfo CreateNamespaceInfo(string P_0, string P_1)
		{
			return new AvaloniaXamlIlXmlNamespaceInfo
			{
				ClrNamespace = P_0,
				ClrAssemblyName = P_1
			};
		}

		private static IReadOnlyDictionary<string, IReadOnlyList<AvaloniaXamlIlXmlNamespaceInfo>> CreateNamespaces()
		{
			return new Dictionary<string, IReadOnlyList<AvaloniaXamlIlXmlNamespaceInfo>>(2)
			{
				{
					"",
					new AvaloniaXamlIlXmlNamespaceInfo[37]
					{
						CreateNamespaceInfo("Avalonia", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Animation", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Animation.Easings", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Controls", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Data", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Data.Converters", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Input", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Input.GestureRecognizers", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Input.TextInput", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Layout", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.LogicalTree", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Media", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Media.Imaging", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Media.Transformation", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Styling", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Controls", "Avalonia.Controls.DataGrid"),
						CreateNamespaceInfo("Avalonia.Controls.Collections", "Avalonia.Controls.DataGrid"),
						CreateNamespaceInfo("Avalonia.Controls.Primitives", "Avalonia.Controls.DataGrid"),
						CreateNamespaceInfo("Avalonia", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Automation", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Embedding", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Presenters", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Primitives", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Shapes", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Templates", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Notifications", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Chrome", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Documents", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Fonts.Inter", "Avalonia.Fonts.Inter"),
						CreateNamespaceInfo("Avalonia.Data", "Avalonia.Markup"),
						CreateNamespaceInfo("Avalonia.Markup.Data", "Avalonia.Markup"),
						CreateNamespaceInfo("Avalonia.Markup.Xaml.MarkupExtensions", "Avalonia.Markup.Xaml"),
						CreateNamespaceInfo("Avalonia.Markup.Xaml.Styling", "Avalonia.Markup.Xaml"),
						CreateNamespaceInfo("Avalonia.Markup.Xaml.Templates", "Avalonia.Markup.Xaml"),
						CreateNamespaceInfo("Avalonia.Themes.Fluent", "Avalonia.Themes.Fluent"),
						CreateNamespaceInfo("Avalonia.Svg.Skia", "Svg.Controls.Skia.Avalonia")
					}
				},
				{
					"x",
					new AvaloniaXamlIlXmlNamespaceInfo[0]
				}
			};
		}

		static NamespaceInfo_003A_002FControls_002FButton_002Eaxaml()
		{
			Singleton = new NamespaceInfo_003A_002FControls_002FButton_002Eaxaml();
		}
	}

	[CompilerGenerated]
	private class XamlClosure_1
	{
		public static object Build_1(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<Styles> context = CreateContext(P_0);
			context.IntermediateRoot = new Border();
			object intermediateRoot = context.IntermediateRoot;
			((ISupportInitialize)intermediateRoot).BeginInit();
			((StyledElement)intermediateRoot).Name = "PART_Border";
			object element = intermediateRoot;
			context.AvaloniaNameScope.Register("PART_Border", element);
			((AvaloniaObject)intermediateRoot).SetValue(Border.BackgroundProperty, (IBrush)new ImmutableSolidColorBrush(872415231u), BindingPriority.Template);
			((AvaloniaObject)intermediateRoot).SetValue(Border.BorderBrushProperty, (IBrush)new ImmutableSolidColorBrush(452984831u), BindingPriority.Template);
			((AvaloniaObject)intermediateRoot).SetValue(Border.BorderThicknessProperty, new Thickness(1.0, 1.0, 1.0, 1.0), BindingPriority.Template);
			((AvaloniaObject)intermediateRoot).SetValue(Border.CornerRadiusProperty, new CornerRadius(999.0, 999.0, 999.0, 999.0), BindingPriority.Template);
			((AvaloniaObject)intermediateRoot).Bind((AvaloniaProperty)Decorator.PaddingProperty, new TemplateBinding(TemplatedControl.PaddingProperty).ProvideValue());
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

		public static CompiledAvaloniaXaml.XamlIlContext.Context<Styles> CreateContext(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<Styles> context = new CompiledAvaloniaXaml.XamlIlContext.Context<Styles>(P_0, new object[1] { NamespaceInfo_003A_002FControls_002FButton_002Eaxaml.Singleton }, "avares://SpaceWalker/Controls/Button.axaml");
			if (P_0 != null)
			{
				object service = P_0.GetService(typeof(IRootObjectProvider));
				if (service != null)
				{
					service = ((IRootObjectProvider)service).RootObject;
					context.RootObject = (Styles)service;
				}
			}
			return context;
		}

		public static object Build_2(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<Styles> context = CreateContext(P_0);
			context.IntermediateRoot = new Border();
			object intermediateRoot = context.IntermediateRoot;
			((ISupportInitialize)intermediateRoot).BeginInit();
			((StyledElement)intermediateRoot).Name = "PART_Border";
			object element = intermediateRoot;
			context.AvaloniaNameScope.Register("PART_Border", element);
			((AvaloniaObject)intermediateRoot).SetValue(Border.BackgroundProperty, (IBrush)new ImmutableSolidColorBrush(16777215u), BindingPriority.Template);
			((AvaloniaObject)intermediateRoot).SetValue(Border.BorderThicknessProperty, new Thickness(0.0, 0.0, 0.0, 0.0), BindingPriority.Template);
			((AvaloniaObject)intermediateRoot).SetValue(Border.CornerRadiusProperty, new CornerRadius(999.0, 999.0, 999.0, 999.0), BindingPriority.Template);
			((AvaloniaObject)intermediateRoot).Bind((AvaloniaProperty)Decorator.PaddingProperty, new TemplateBinding(TemplatedControl.PaddingProperty).ProvideValue());
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

		public static object Build_3(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<Styles> context = CreateContext(P_0);
			context.IntermediateRoot = new Border();
			object intermediateRoot = context.IntermediateRoot;
			((ISupportInitialize)intermediateRoot).BeginInit();
			((StyledElement)intermediateRoot).Name = "PART_Border";
			object element = intermediateRoot;
			context.AvaloniaNameScope.Register("PART_Border", element);
			((AvaloniaObject)intermediateRoot).SetValue(Border.BackgroundProperty, (IBrush)new ImmutableSolidColorBrush(452984831u), BindingPriority.Template);
			((AvaloniaObject)intermediateRoot).SetValue(Border.CornerRadiusProperty, new CornerRadius(8.0, 8.0, 8.0, 8.0), BindingPriority.Template);
			((AvaloniaObject)intermediateRoot).SetValue(Layoutable.WidthProperty, 16.0, BindingPriority.Template);
			((AvaloniaObject)intermediateRoot).SetValue(Layoutable.HeightProperty, 16.0, BindingPriority.Template);
			StyledProperty<Transitions?> transitionsProperty = Animatable.TransitionsProperty;
			Transitions transitions = new Transitions();
			BrushTransition brushTransition = new BrushTransition();
			brushTransition.Property = Border.BackgroundProperty;
			brushTransition.Duration = TimeSpan.FromTicks(1500000L);
			transitions.Add(brushTransition);
			((AvaloniaObject)intermediateRoot).SetValue(transitionsProperty, transitions, BindingPriority.Template);
			Path path;
			Path path2 = (path = new Path());
			((ISupportInitialize)path2).BeginInit();
			((Decorator)intermediateRoot).Child = path2;
			path.SetValue(Path.DataProperty, Geometry.Parse("M5.5,5.5L10.5,10.5M10.5,5.5L5.5,10.5"), BindingPriority.Template);
			path.SetValue(Shape.StrokeProperty, new ImmutableSolidColorBrush(uint.MaxValue), BindingPriority.Template);
			path.SetValue(Shape.StrokeThicknessProperty, 1.2, BindingPriority.Template);
			path.SetValue(Shape.StrokeLineCapProperty, PenLineCap.Round, BindingPriority.Template);
			path.SetValue(Visual.OpacityProperty, 0.64, BindingPriority.Template);
			path.SetValue(Layoutable.WidthProperty, 16.0, BindingPriority.Template);
			path.SetValue(Layoutable.HeightProperty, 16.0, BindingPriority.Template);
			path.SetValue(Shape.StretchProperty, Stretch.None, BindingPriority.Template);
			path.SetValue(Layoutable.HorizontalAlignmentProperty, HorizontalAlignment.Center, BindingPriority.Template);
			path.SetValue(Layoutable.VerticalAlignmentProperty, VerticalAlignment.Center, BindingPriority.Template);
			((ISupportInitialize)path).EndInit();
			((ISupportInitialize)intermediateRoot).EndInit();
			return intermediateRoot;
		}
	}

	[CompilerGenerated]
	internal class NamespaceInfo_003A_002FControls_002FCheckBox_002Eaxaml : IAvaloniaXamlIlXmlNamespaceInfoProvider
	{
		private IReadOnlyDictionary<string, IReadOnlyList<AvaloniaXamlIlXmlNamespaceInfo>> _xmlNamespaces;

		public static IAvaloniaXamlIlXmlNamespaceInfoProvider Singleton;

		public virtual IReadOnlyDictionary<string, IReadOnlyList<AvaloniaXamlIlXmlNamespaceInfo>> XmlNamespaces
		{
			get
			{
				if (_xmlNamespaces == null)
				{
					_xmlNamespaces = CreateNamespaces();
				}
				return _xmlNamespaces;
			}
		}

		private static AvaloniaXamlIlXmlNamespaceInfo CreateNamespaceInfo(string P_0, string P_1)
		{
			return new AvaloniaXamlIlXmlNamespaceInfo
			{
				ClrNamespace = P_0,
				ClrAssemblyName = P_1
			};
		}

		private static IReadOnlyDictionary<string, IReadOnlyList<AvaloniaXamlIlXmlNamespaceInfo>> CreateNamespaces()
		{
			return new Dictionary<string, IReadOnlyList<AvaloniaXamlIlXmlNamespaceInfo>>(2)
			{
				{
					"",
					new AvaloniaXamlIlXmlNamespaceInfo[37]
					{
						CreateNamespaceInfo("Avalonia", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Animation", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Animation.Easings", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Controls", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Data", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Data.Converters", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Input", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Input.GestureRecognizers", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Input.TextInput", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Layout", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.LogicalTree", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Media", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Media.Imaging", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Media.Transformation", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Styling", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Controls", "Avalonia.Controls.DataGrid"),
						CreateNamespaceInfo("Avalonia.Controls.Collections", "Avalonia.Controls.DataGrid"),
						CreateNamespaceInfo("Avalonia.Controls.Primitives", "Avalonia.Controls.DataGrid"),
						CreateNamespaceInfo("Avalonia", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Automation", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Embedding", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Presenters", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Primitives", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Shapes", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Templates", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Notifications", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Chrome", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Documents", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Fonts.Inter", "Avalonia.Fonts.Inter"),
						CreateNamespaceInfo("Avalonia.Data", "Avalonia.Markup"),
						CreateNamespaceInfo("Avalonia.Markup.Data", "Avalonia.Markup"),
						CreateNamespaceInfo("Avalonia.Markup.Xaml.MarkupExtensions", "Avalonia.Markup.Xaml"),
						CreateNamespaceInfo("Avalonia.Markup.Xaml.Styling", "Avalonia.Markup.Xaml"),
						CreateNamespaceInfo("Avalonia.Markup.Xaml.Templates", "Avalonia.Markup.Xaml"),
						CreateNamespaceInfo("Avalonia.Themes.Fluent", "Avalonia.Themes.Fluent"),
						CreateNamespaceInfo("Avalonia.Svg.Skia", "Svg.Controls.Skia.Avalonia")
					}
				},
				{
					"x",
					new AvaloniaXamlIlXmlNamespaceInfo[0]
				}
			};
		}

		static NamespaceInfo_003A_002FControls_002FCheckBox_002Eaxaml()
		{
			Singleton = new NamespaceInfo_003A_002FControls_002FCheckBox_002Eaxaml();
		}
	}

	[CompilerGenerated]
	private class XamlClosure_2
	{
		public static object Build_1(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<Styles> context = CreateContext(P_0);
			context.IntermediateRoot = new Grid();
			object intermediateRoot = context.IntermediateRoot;
			((ISupportInitialize)intermediateRoot).BeginInit();
			ColumnDefinitions columnDefinitions = new ColumnDefinitions();
			columnDefinitions.Capacity = 2;
			columnDefinitions.Add(new ColumnDefinition(new GridLength(0.0, GridUnitType.Auto)));
			columnDefinitions.Add(new ColumnDefinition(new GridLength(1.0, GridUnitType.Star)));
			((Grid)intermediateRoot).ColumnDefinitions = columnDefinitions;
			((AvaloniaObject)intermediateRoot).SetValue(Layoutable.VerticalAlignmentProperty, VerticalAlignment.Center, BindingPriority.Template);
			Controls children = ((Panel)intermediateRoot).Children;
			Panel panel;
			Panel panel2 = (panel = new Panel());
			((ISupportInitialize)panel2).BeginInit();
			children.Add(panel2);
			panel.SetValue(Grid.ColumnProperty, 0, BindingPriority.Template);
			panel.SetValue(Layoutable.UseLayoutRoundingProperty, value: false, BindingPriority.Template);
			panel.SetValue(Layoutable.HorizontalAlignmentProperty, HorizontalAlignment.Center, BindingPriority.Template);
			panel.SetValue(Layoutable.VerticalAlignmentProperty, VerticalAlignment.Center, BindingPriority.Template);
			Controls children2 = panel.Children;
			Border border;
			Border border2 = (border = new Border());
			((ISupportInitialize)border2).BeginInit();
			children2.Add(border2);
			border.SetValue(Layoutable.WidthProperty, 16.0, BindingPriority.Template);
			border.SetValue(Layoutable.HeightProperty, 16.0, BindingPriority.Template);
			border.Name = "OuterBox";
			object element = border;
			context.AvaloniaNameScope.Register("OuterBox", element);
			border.SetValue(Border.CornerRadiusProperty, new CornerRadius(4.0, 4.0, 4.0, 4.0), BindingPriority.Template);
			border.SetValue(Border.BackgroundProperty, new ImmutableSolidColorBrush(436207616u), BindingPriority.Template);
			border.SetValue(Border.BorderBrushProperty, new ImmutableSolidColorBrush(2063597567u), BindingPriority.Template);
			border.SetValue(Border.BorderThicknessProperty, new Thickness(1.5, 1.5, 1.5, 1.5), BindingPriority.Template);
			StyledProperty<Transitions?> transitionsProperty = Animatable.TransitionsProperty;
			Transitions transitions = new Transitions();
			BrushTransition brushTransition = new BrushTransition();
			brushTransition.Property = Border.BackgroundProperty;
			brushTransition.Duration = TimeSpan.FromTicks(1500000L);
			transitions.Add(brushTransition);
			BrushTransition brushTransition2 = new BrushTransition();
			brushTransition2.Property = Border.BorderBrushProperty;
			brushTransition2.Duration = TimeSpan.FromTicks(1500000L);
			transitions.Add(brushTransition2);
			border.SetValue(transitionsProperty, transitions, BindingPriority.Template);
			((ISupportInitialize)border).EndInit();
			Controls children3 = panel.Children;
			Path path;
			Path path2 = (path = new Path());
			((ISupportInitialize)path2).BeginInit();
			children3.Add(path2);
			path.Name = "CheckMark";
			element = path;
			context.AvaloniaNameScope.Register("CheckMark", element);
			path.SetValue(Layoutable.WidthProperty, 16.0, BindingPriority.Template);
			path.SetValue(Layoutable.HeightProperty, 16.0, BindingPriority.Template);
			path.SetValue(Shape.StretchProperty, Stretch.None, BindingPriority.Template);
			path.SetValue(Shape.StrokeProperty, new ImmutableSolidColorBrush(4293784831u), BindingPriority.Template);
			path.SetValue(Shape.StrokeThicknessProperty, 1.44, BindingPriority.Template);
			path.SetValue(Shape.StrokeLineCapProperty, PenLineCap.Square, BindingPriority.Template);
			path.SetValue(Shape.StrokeJoinProperty, PenLineJoin.Round, BindingPriority.Template);
			path.SetValue(Path.DataProperty, Geometry.Parse("M 11.37,5.90 L 6.74,10.53 L 5.40,8.20"), BindingPriority.Template);
			path.SetValue(Visual.OpacityProperty, 0.0, BindingPriority.Template);
			path.SetValue(Layoutable.HorizontalAlignmentProperty, HorizontalAlignment.Center, BindingPriority.Template);
			path.SetValue(Layoutable.VerticalAlignmentProperty, VerticalAlignment.Center, BindingPriority.Template);
			StyledProperty<Transitions?> transitionsProperty2 = Animatable.TransitionsProperty;
			Transitions transitions2 = new Transitions();
			DoubleTransition doubleTransition = new DoubleTransition();
			doubleTransition.Property = Visual.OpacityProperty;
			doubleTransition.Duration = TimeSpan.FromTicks(1500000L);
			transitions2.Add(doubleTransition);
			path.SetValue(transitionsProperty2, transitions2, BindingPriority.Template);
			((ISupportInitialize)path).EndInit();
			((ISupportInitialize)panel).EndInit();
			Controls children4 = ((Panel)intermediateRoot).Children;
			ContentPresenter contentPresenter;
			ContentPresenter contentPresenter2 = (contentPresenter = new ContentPresenter());
			((ISupportInitialize)contentPresenter2).BeginInit();
			children4.Add(contentPresenter2);
			contentPresenter.Name = "PART_ContentPresenter";
			element = contentPresenter;
			context.AvaloniaNameScope.Register("PART_ContentPresenter", element);
			contentPresenter.SetValue(Grid.ColumnProperty, 1, BindingPriority.Template);
			contentPresenter.SetValue(Layoutable.MarginProperty, new Thickness(8.0, 0.0, 0.0, 0.0), BindingPriority.Template);
			contentPresenter.SetValue(Layoutable.VerticalAlignmentProperty, VerticalAlignment.Center, BindingPriority.Template);
			CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(contentPresenter, BindingPriority.Template, new TemplateBinding(ContentControl.ContentProperty).ProvideValue());
			contentPresenter.Bind(ContentPresenter.ContentTemplateProperty, new TemplateBinding(ContentControl.ContentTemplateProperty).ProvideValue());
			contentPresenter.Bind(ContentPresenter.FontSizeProperty, new TemplateBinding(TemplatedControl.FontSizeProperty).ProvideValue());
			contentPresenter.SetValue(ContentPresenter.RecognizesAccessKeyProperty, value: true, BindingPriority.Template);
			((ISupportInitialize)contentPresenter).EndInit();
			((ISupportInitialize)intermediateRoot).EndInit();
			return intermediateRoot;
		}

		public static CompiledAvaloniaXaml.XamlIlContext.Context<Styles> CreateContext(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<Styles> context = new CompiledAvaloniaXaml.XamlIlContext.Context<Styles>(P_0, new object[1] { NamespaceInfo_003A_002FControls_002FCheckBox_002Eaxaml.Singleton }, "avares://SpaceWalker/Controls/CheckBox.axaml");
			if (P_0 != null)
			{
				object service = P_0.GetService(typeof(IRootObjectProvider));
				if (service != null)
				{
					service = ((IRootObjectProvider)service).RootObject;
					context.RootObject = (Styles)service;
				}
			}
			return context;
		}
	}

	[CompilerGenerated]
	internal class NamespaceInfo_003A_002FControls_002FComboBox_002Eaxaml : IAvaloniaXamlIlXmlNamespaceInfoProvider
	{
		private IReadOnlyDictionary<string, IReadOnlyList<AvaloniaXamlIlXmlNamespaceInfo>> _xmlNamespaces;

		public static IAvaloniaXamlIlXmlNamespaceInfoProvider Singleton;

		public virtual IReadOnlyDictionary<string, IReadOnlyList<AvaloniaXamlIlXmlNamespaceInfo>> XmlNamespaces
		{
			get
			{
				if (_xmlNamespaces == null)
				{
					_xmlNamespaces = CreateNamespaces();
				}
				return _xmlNamespaces;
			}
		}

		private static AvaloniaXamlIlXmlNamespaceInfo CreateNamespaceInfo(string P_0, string P_1)
		{
			return new AvaloniaXamlIlXmlNamespaceInfo
			{
				ClrNamespace = P_0,
				ClrAssemblyName = P_1
			};
		}

		private static IReadOnlyDictionary<string, IReadOnlyList<AvaloniaXamlIlXmlNamespaceInfo>> CreateNamespaces()
		{
			return new Dictionary<string, IReadOnlyList<AvaloniaXamlIlXmlNamespaceInfo>>(3)
			{
				{
					"",
					new AvaloniaXamlIlXmlNamespaceInfo[37]
					{
						CreateNamespaceInfo("Avalonia", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Animation", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Animation.Easings", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Controls", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Data", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Data.Converters", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Input", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Input.GestureRecognizers", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Input.TextInput", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Layout", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.LogicalTree", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Media", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Media.Imaging", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Media.Transformation", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Styling", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Controls", "Avalonia.Controls.DataGrid"),
						CreateNamespaceInfo("Avalonia.Controls.Collections", "Avalonia.Controls.DataGrid"),
						CreateNamespaceInfo("Avalonia.Controls.Primitives", "Avalonia.Controls.DataGrid"),
						CreateNamespaceInfo("Avalonia", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Automation", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Embedding", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Presenters", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Primitives", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Shapes", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Templates", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Notifications", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Chrome", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Documents", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Fonts.Inter", "Avalonia.Fonts.Inter"),
						CreateNamespaceInfo("Avalonia.Data", "Avalonia.Markup"),
						CreateNamespaceInfo("Avalonia.Markup.Data", "Avalonia.Markup"),
						CreateNamespaceInfo("Avalonia.Markup.Xaml.MarkupExtensions", "Avalonia.Markup.Xaml"),
						CreateNamespaceInfo("Avalonia.Markup.Xaml.Styling", "Avalonia.Markup.Xaml"),
						CreateNamespaceInfo("Avalonia.Markup.Xaml.Templates", "Avalonia.Markup.Xaml"),
						CreateNamespaceInfo("Avalonia.Themes.Fluent", "Avalonia.Themes.Fluent"),
						CreateNamespaceInfo("Avalonia.Svg.Skia", "Svg.Controls.Skia.Avalonia")
					}
				},
				{
					"x",
					new AvaloniaXamlIlXmlNamespaceInfo[0]
				},
				{
					"helper",
					new AvaloniaXamlIlXmlNamespaceInfo[1] { CreateNamespaceInfo("SpaceWalker.Helper", null) }
				}
			};
		}

		static NamespaceInfo_003A_002FControls_002FComboBox_002Eaxaml()
		{
			Singleton = new NamespaceInfo_003A_002FControls_002FComboBox_002Eaxaml();
		}
	}

	[CompilerGenerated]
	private class XamlClosure_3
	{
		public static object Build_1(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<Styles> context = CreateContext(P_0);
			return new SolidColorBrush
			{
				Color = Color.FromUInt32(872415231u)
			};
		}

		public static CompiledAvaloniaXaml.XamlIlContext.Context<Styles> CreateContext(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<Styles> context = new CompiledAvaloniaXaml.XamlIlContext.Context<Styles>(P_0, new object[1] { NamespaceInfo_003A_002FControls_002FComboBox_002Eaxaml.Singleton }, "avares://SpaceWalker/Controls/ComboBox.axaml");
			if (P_0 != null)
			{
				object service = P_0.GetService(typeof(IRootObjectProvider));
				if (service != null)
				{
					service = ((IRootObjectProvider)service).RootObject;
					context.RootObject = (Styles)service;
				}
			}
			return context;
		}

		public static object Build_2(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<Styles> context = CreateContext(P_0);
			return new SolidColorBrush
			{
				Color = Color.FromUInt32(1308622847u)
			};
		}

		public static object Build_3(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<Styles> context = CreateContext(P_0);
			return new SolidColorBrush
			{
				Color = Color.FromUInt32(704643071u)
			};
		}

		public static object Build_4(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<Styles> context = CreateContext(P_0);
			LinearGradientBrush linearGradientBrush = new LinearGradientBrush();
			linearGradientBrush.StartPoint = new RelativePoint(0.0, 0.0, RelativeUnit.Absolute);
			linearGradientBrush.EndPoint = new RelativePoint(0.0, 1.5, RelativeUnit.Absolute);
			GradientStops gradientStops = linearGradientBrush.GradientStops;
			GradientStop gradientStop = new GradientStop();
			gradientStop.Offset = 0.0;
			gradientStop.Color = Color.FromUInt32(2801795071u);
			gradientStops.Add(gradientStop);
			GradientStops gradientStops2 = linearGradientBrush.GradientStops;
			GradientStop gradientStop2 = new GradientStop();
			gradientStop2.Offset = 1.0;
			gradientStop2.Color = Color.FromUInt32(16777215u);
			gradientStops2.Add(gradientStop2);
			return linearGradientBrush;
		}

		public static object Build_5(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<Styles> context = CreateContext(P_0);
			return new SolidColorBrush
			{
				Color = Color.FromUInt32(872415231u)
			};
		}

		public static object Build_6(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<Styles> context = CreateContext(P_0);
			return StreamGeometry.Parse("M0,0L0,0M20,20L20,20M14.1156 8.07031C14.5184 7.69271 15.1509 7.71371 15.5287 8.11621C15.9064 8.51907 15.8865 9.15155 15.4838 9.5293L10.684 14.0293C10.2993 14.3899 9.70045 14.3899 9.3158 14.0293L4.51599 9.5293C4.11351 9.15157 4.09264 8.519 4.47009 8.11621C4.84765 7.7135 5.48024 7.69312 5.88318 8.07031L9.99939 11.9287L14.1156 8.07031Z");
		}

		public static object Build_7(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<Styles> context = CreateContext(P_0);
			return StreamGeometry.Parse("M0.8501,0 L5.916,5.066 A0.6,0.6 0 0 1 5.916,5.9145 L0.8486,10.982 L0,10.1334 L4.6432,5.4903 L0.0016,0.8486 Z");
		}

		public static object Build_8(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<Styles> context = CreateContext(P_0);
			context.IntermediateRoot = new StackPanel();
			object intermediateRoot = context.IntermediateRoot;
			((ISupportInitialize)intermediateRoot).BeginInit();
			StackPanel stackPanel = (StackPanel)intermediateRoot;
			context.PushParent(stackPanel);
			Controls children = stackPanel.Children;
			ContentPresenter contentPresenter;
			ContentPresenter contentPresenter2 = (contentPresenter = new ContentPresenter());
			((ISupportInitialize)contentPresenter2).BeginInit();
			children.Add(contentPresenter2);
			contentPresenter.Name = "PART_ContentPresenter";
			object element = contentPresenter;
			context.AvaloniaNameScope.Register("PART_ContentPresenter", element);
			contentPresenter.SetValue(Layoutable.MinHeightProperty, 24.0, BindingPriority.Template);
			contentPresenter.Bind(ContentPresenter.ForegroundProperty, new TemplateBinding(TemplatedControl.ForegroundProperty).ProvideValue());
			contentPresenter.Bind(ContentPresenter.BackgroundProperty, new TemplateBinding(TemplatedControl.BackgroundProperty).ProvideValue());
			contentPresenter.Bind(ContentPresenter.BorderBrushProperty, new TemplateBinding(TemplatedControl.BorderBrushProperty).ProvideValue());
			contentPresenter.Bind(ContentPresenter.BorderThicknessProperty, new TemplateBinding(TemplatedControl.BorderThicknessProperty).ProvideValue());
			contentPresenter.Bind(ContentPresenter.CornerRadiusProperty, new TemplateBinding(TemplatedControl.CornerRadiusProperty).ProvideValue());
			CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(contentPresenter, BindingPriority.Template, new TemplateBinding(ContentControl.ContentProperty).ProvideValue());
			contentPresenter.Bind(ContentPresenter.ContentTemplateProperty, new TemplateBinding(ContentControl.ContentTemplateProperty).ProvideValue());
			contentPresenter.Bind(ContentPresenter.PaddingProperty, new TemplateBinding(TemplatedControl.PaddingProperty).ProvideValue());
			contentPresenter.Bind(ContentPresenter.HorizontalContentAlignmentProperty, new TemplateBinding(ContentControl.HorizontalContentAlignmentProperty).ProvideValue());
			contentPresenter.Bind(ContentPresenter.VerticalContentAlignmentProperty, new TemplateBinding(ContentControl.VerticalContentAlignmentProperty).ProvideValue());
			((ISupportInitialize)contentPresenter).EndInit();
			Controls children2 = stackPanel.Children;
			Border border;
			Border border2 = (border = new Border());
			((ISupportInitialize)border2).BeginInit();
			children2.Add(border2);
			Border border3;
			Border border4 = (border3 = border);
			context.PushParent(border3);
			border3.Name = "DividerBorder";
			element = border3;
			context.AvaloniaNameScope.Register("DividerBorder", element);
			border3.SetValue(Layoutable.HeightProperty, 1.0, BindingPriority.Template);
			border3.SetValue(Layoutable.MarginProperty, new Thickness(6.0, 4.0, 6.0, 4.0), BindingPriority.Template);
			StaticResourceExtension staticResourceExtension = new StaticResourceExtension("ComboBoxItemDividerBrush");
			context.ProvideTargetProperty = Border.BackgroundProperty;
			object? obj = staticResourceExtension.ProvideValue(context);
			context.ProvideTargetProperty = null;
			CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_2(border3, BindingPriority.Template, obj);
			border3.SetValue(InputElement.IsHitTestVisibleProperty, value: false, BindingPriority.Template);
			context.PopParent();
			((ISupportInitialize)border4).EndInit();
			context.PopParent();
			((ISupportInitialize)intermediateRoot).EndInit();
			return intermediateRoot;
		}
	}

	[CompilerGenerated]
	internal class NamespaceInfo_003A_002FControls_002FContentDialog_002Eaxaml : IAvaloniaXamlIlXmlNamespaceInfoProvider
	{
		private IReadOnlyDictionary<string, IReadOnlyList<AvaloniaXamlIlXmlNamespaceInfo>> _xmlNamespaces;

		public static IAvaloniaXamlIlXmlNamespaceInfoProvider Singleton;

		public virtual IReadOnlyDictionary<string, IReadOnlyList<AvaloniaXamlIlXmlNamespaceInfo>> XmlNamespaces
		{
			get
			{
				if (_xmlNamespaces == null)
				{
					_xmlNamespaces = CreateNamespaces();
				}
				return _xmlNamespaces;
			}
		}

		private static AvaloniaXamlIlXmlNamespaceInfo CreateNamespaceInfo(string P_0, string P_1)
		{
			return new AvaloniaXamlIlXmlNamespaceInfo
			{
				ClrNamespace = P_0,
				ClrAssemblyName = P_1
			};
		}

		private static IReadOnlyDictionary<string, IReadOnlyList<AvaloniaXamlIlXmlNamespaceInfo>> CreateNamespaces()
		{
			return new Dictionary<string, IReadOnlyList<AvaloniaXamlIlXmlNamespaceInfo>>(4)
			{
				{
					"",
					new AvaloniaXamlIlXmlNamespaceInfo[37]
					{
						CreateNamespaceInfo("Avalonia", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Animation", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Animation.Easings", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Controls", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Data", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Data.Converters", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Input", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Input.GestureRecognizers", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Input.TextInput", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Layout", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.LogicalTree", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Media", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Media.Imaging", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Media.Transformation", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Styling", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Controls", "Avalonia.Controls.DataGrid"),
						CreateNamespaceInfo("Avalonia.Controls.Collections", "Avalonia.Controls.DataGrid"),
						CreateNamespaceInfo("Avalonia.Controls.Primitives", "Avalonia.Controls.DataGrid"),
						CreateNamespaceInfo("Avalonia", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Automation", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Embedding", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Presenters", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Primitives", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Shapes", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Templates", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Notifications", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Chrome", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Documents", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Fonts.Inter", "Avalonia.Fonts.Inter"),
						CreateNamespaceInfo("Avalonia.Data", "Avalonia.Markup"),
						CreateNamespaceInfo("Avalonia.Markup.Data", "Avalonia.Markup"),
						CreateNamespaceInfo("Avalonia.Markup.Xaml.MarkupExtensions", "Avalonia.Markup.Xaml"),
						CreateNamespaceInfo("Avalonia.Markup.Xaml.Styling", "Avalonia.Markup.Xaml"),
						CreateNamespaceInfo("Avalonia.Markup.Xaml.Templates", "Avalonia.Markup.Xaml"),
						CreateNamespaceInfo("Avalonia.Themes.Fluent", "Avalonia.Themes.Fluent"),
						CreateNamespaceInfo("Avalonia.Svg.Skia", "Svg.Controls.Skia.Avalonia")
					}
				},
				{
					"x",
					new AvaloniaXamlIlXmlNamespaceInfo[0]
				},
				{
					"sys",
					new AvaloniaXamlIlXmlNamespaceInfo[1] { CreateNamespaceInfo("System", null) }
				},
				{
					"ui",
					new AvaloniaXamlIlXmlNamespaceInfo[1] { CreateNamespaceInfo("Avalonia.Labs.Controls", null) }
				}
			};
		}

		static NamespaceInfo_003A_002FControls_002FContentDialog_002Eaxaml()
		{
			Singleton = new NamespaceInfo_003A_002FControls_002FContentDialog_002Eaxaml();
		}
	}

	[CompilerGenerated]
	private class XamlClosure_4
	{
		public static object Build_1(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<ResourceDictionary> context = CreateContext(P_0);
			return new SolidColorBrush
			{
				Color = Color.FromUInt32(2147483648u)
			};
		}

		public static CompiledAvaloniaXaml.XamlIlContext.Context<ResourceDictionary> CreateContext(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<ResourceDictionary> context = new CompiledAvaloniaXaml.XamlIlContext.Context<ResourceDictionary>(P_0, new object[1] { NamespaceInfo_003A_002FControls_002FContentDialog_002Eaxaml.Singleton }, "avares://SpaceWalker/Controls/ContentDialog.axaml");
			if (P_0 != null)
			{
				object service = P_0.GetService(typeof(IRootObjectProvider));
				if (service != null)
				{
					service = ((IRootObjectProvider)service).RootObject;
					context.RootObject = (ResourceDictionary)service;
				}
			}
			return context;
		}

		public static object Build_2(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<ResourceDictionary> context = CreateContext(P_0);
			return new SolidColorBrush
			{
				Color = Color.FromUInt32(16777215u)
			};
		}

		public static object Build_3(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<ResourceDictionary> context = CreateContext(P_0);
			return new SolidColorBrush
			{
				Color = Color.FromUInt32(16777215u)
			};
		}

		public static object Build_4(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<ResourceDictionary> context = CreateContext(P_0);
			return new SolidColorBrush
			{
				Color = Color.FromUInt32(16777215u)
			};
		}

		public static object Build_5(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<ResourceDictionary> context = CreateContext(P_0);
			return new SolidColorBrush
			{
				Color = Color.FromUInt32(uint.MaxValue)
			};
		}

		public static object Build_6(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<ResourceDictionary> context = CreateContext(P_0);
			return new SolidColorBrush
			{
				Color = Color.FromUInt32(2155905152u)
			};
		}

		public static object Build_7(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<ResourceDictionary> context = CreateContext(P_0);
			DrawingBrush drawingBrush = new DrawingBrush();
			DrawingGroup drawingGroup = new DrawingGroup();
			DrawingCollection children = drawingGroup.Children;
			GeometryDrawing geometryDrawing = new GeometryDrawing();
			geometryDrawing.Brush = new ImmutableSolidColorBrush(1308622847u);
			geometryDrawing.Geometry = new RectangleGeometry
			{
				Rect = Rect.Parse("0,0,1,1")
			};
			children.Add(geometryDrawing);
			DrawingCollection children2 = drawingGroup.Children;
			GeometryDrawing geometryDrawing2 = new GeometryDrawing();
			LinearGradientBrush linearGradientBrush = new LinearGradientBrush();
			linearGradientBrush.StartPoint = new RelativePoint(0.0, 1.0, RelativeUnit.Absolute);
			linearGradientBrush.EndPoint = new RelativePoint(0.0, 0.0, RelativeUnit.Absolute);
			GradientStops gradientStops = linearGradientBrush.GradientStops;
			GradientStop gradientStop = new GradientStop();
			gradientStop.Color = Color.FromUInt32(168980991u);
			gradientStop.Offset = 0.0;
			gradientStops.Add(gradientStop);
			GradientStops gradientStops2 = linearGradientBrush.GradientStops;
			GradientStop gradientStop2 = new GradientStop();
			gradientStop2.Color = Color.FromUInt32(168980991u);
			gradientStop2.Offset = 1.0;
			gradientStops2.Add(gradientStop2);
			geometryDrawing2.Brush = linearGradientBrush;
			geometryDrawing2.Geometry = new RectangleGeometry
			{
				Rect = Rect.Parse("0,0,1,1")
			};
			children2.Add(geometryDrawing2);
			drawingBrush.Drawing = drawingGroup;
			return drawingBrush;
		}

		public unsafe static object Build_8(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<ResourceDictionary> context = CreateContext(P_0);
			ControlTheme controlTheme;
			ControlTheme result = (controlTheme = new ControlTheme());
			context.PushParent(controlTheme);
			controlTheme.TargetType = typeof(ContentDialog);
			Setter setter;
			Setter setter2 = (setter = new Setter());
			context.PushParent(setter);
			Setter setter3 = setter;
			setter3.Property = TemplatedControl.ForegroundProperty;
			DynamicResourceExtension dynamicResourceExtension = new DynamicResourceExtension("ContentDialogForeground");
			context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
			BindingBase value = dynamicResourceExtension.ProvideValue(context);
			context.ProvideTargetProperty = null;
			setter3.Value = value;
			context.PopParent();
			controlTheme.Add(setter2);
			Setter setter4 = (setter = new Setter());
			context.PushParent(setter);
			Setter setter5 = setter;
			setter5.Property = TemplatedControl.BackgroundProperty;
			DynamicResourceExtension dynamicResourceExtension2 = new DynamicResourceExtension("ContentDialogBackground");
			context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
			BindingBase value2 = dynamicResourceExtension2.ProvideValue(context);
			context.ProvideTargetProperty = null;
			setter5.Value = value2;
			context.PopParent();
			controlTheme.Add(setter4);
			Setter setter6 = (setter = new Setter());
			context.PushParent(setter);
			Setter setter7 = setter;
			setter7.Property = TemplatedControl.BorderBrushProperty;
			DynamicResourceExtension dynamicResourceExtension3 = new DynamicResourceExtension("ContentDialogBorderBrush");
			context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
			BindingBase value3 = dynamicResourceExtension3.ProvideValue(context);
			context.ProvideTargetProperty = null;
			setter7.Value = value3;
			context.PopParent();
			controlTheme.Add(setter6);
			Setter setter8 = (setter = new Setter());
			context.PushParent(setter);
			Setter setter9 = setter;
			setter9.Property = TemplatedControl.BorderThicknessProperty;
			DynamicResourceExtension dynamicResourceExtension4 = new DynamicResourceExtension("ContentDialogBorderWidth");
			context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
			BindingBase value4 = dynamicResourceExtension4.ProvideValue(context);
			context.ProvideTargetProperty = null;
			setter9.Value = value4;
			context.PopParent();
			controlTheme.Add(setter8);
			Setter setter10 = (setter = new Setter());
			context.PushParent(setter);
			Setter setter11 = setter;
			setter11.Property = TemplatedControl.CornerRadiusProperty;
			DynamicResourceExtension dynamicResourceExtension5 = new DynamicResourceExtension("OverlayCornerRadius");
			context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
			BindingBase value5 = dynamicResourceExtension5.ProvideValue(context);
			context.ProvideTargetProperty = null;
			setter11.Value = value5;
			context.PopParent();
			controlTheme.Add(setter10);
			Setter setter12 = (setter = new Setter());
			context.PushParent(setter);
			Setter setter13 = setter;
			setter13.Property = TemplatedControl.TemplateProperty;
			ControlTemplate controlTemplate;
			ControlTemplate value6 = (controlTemplate = new ControlTemplate());
			context.PushParent(controlTemplate);
			controlTemplate.Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&Build_9), context);
			context.PopParent();
			setter13.Value = value6;
			context.PopParent();
			controlTheme.Add(setter12);
			context.PopParent();
			return result;
		}

		public unsafe static object Build_9(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<ResourceDictionary> context = CreateContext(P_0);
			context.IntermediateRoot = new Border();
			object intermediateRoot = context.IntermediateRoot;
			((ISupportInitialize)intermediateRoot).BeginInit();
			Border border = (Border)intermediateRoot;
			context.PushParent(border);
			Border border2 = border;
			border2.Name = "Container";
			object element = border2;
			context.AvaloniaNameScope.Register("Container", element);
			Border border3;
			Border border4 = (border3 = new Border());
			((ISupportInitialize)border4).BeginInit();
			border2.Child = border4;
			Border border5 = (border = border3);
			context.PushParent(border);
			Border border6 = border;
			border6.Name = "PART_LayoutRoot";
			element = border6;
			context.AvaloniaNameScope.Register("PART_LayoutRoot", element);
			StyledProperty<IBrush?> backgroundProperty = Border.BackgroundProperty;
			DynamicResourceExtension dynamicResourceExtension = new DynamicResourceExtension("ContentDialogSmokeFill");
			context.ProvideTargetProperty = Border.BackgroundProperty;
			BindingBase binding = dynamicResourceExtension.ProvideValue(context);
			context.ProvideTargetProperty = null;
			border6.Bind(backgroundProperty, binding);
			Border border7;
			Border border8 = (border7 = new Border());
			((ISupportInitialize)border8).BeginInit();
			border6.Child = border8;
			Border border9 = (border = border7);
			context.PushParent(border);
			Border border10 = border;
			border10.Name = "BackgroundElement";
			element = border10;
			context.AvaloniaNameScope.Register("BackgroundElement", element);
			border10.Bind(Border.BackgroundProperty, new TemplateBinding(TemplatedControl.BackgroundProperty).ProvideValue());
			StaticResourceExtension staticResourceExtension = new StaticResourceExtension("ContentDialogBorderWidth");
			context.ProvideTargetProperty = Border.BorderThicknessProperty;
			object? obj = staticResourceExtension.ProvideValue(context);
			context.ProvideTargetProperty = null;
			CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_3(border10, BindingPriority.Template, obj);
			border10.Bind(Border.BorderBrushProperty, new TemplateBinding(TemplatedControl.BorderBrushProperty).ProvideValue());
			border10.Bind(Border.CornerRadiusProperty, new TemplateBinding(TemplatedControl.CornerRadiusProperty).ProvideValue());
			border10.SetValue(Layoutable.HorizontalAlignmentProperty, HorizontalAlignment.Stretch, BindingPriority.Template);
			border10.SetValue(Layoutable.VerticalAlignmentProperty, VerticalAlignment.Stretch, BindingPriority.Template);
			border10.SetValue(Layoutable.MarginProperty, new Thickness(35.0, 85.0, 35.0, 35.0), BindingPriority.Template);
			border10.SetValue(Border.BoxShadowProperty, BoxShadows.Parse("0 8 32 0 #66000000"), BindingPriority.Template);
			Grid grid;
			Grid grid2 = (grid = new Grid());
			((ISupportInitialize)grid2).BeginInit();
			border10.Child = grid2;
			Grid grid3;
			Grid grid4 = (grid3 = grid);
			context.PushParent(grid3);
			Grid grid5 = grid3;
			Controls children = grid5.Children;
			Border border11;
			Border border12 = (border11 = new Border());
			((ISupportInitialize)border12).BeginInit();
			children.Add(border12);
			Border border13 = (border = border11);
			context.PushParent(border);
			Border border14 = border;
			border14.SetValue(Visual.ClipToBoundsProperty, value: true, BindingPriority.Template);
			border14.Bind(Border.CornerRadiusProperty, new TemplateBinding(TemplatedControl.CornerRadiusProperty).ProvideValue());
			Grid grid6;
			Grid grid7 = (grid6 = new Grid());
			((ISupportInitialize)grid7).BeginInit();
			border14.Child = grid7;
			Grid grid8 = (grid3 = grid6);
			context.PushParent(grid3);
			Grid grid9 = grid3;
			grid9.Name = "DialogSpace";
			element = grid9;
			context.AvaloniaNameScope.Register("DialogSpace", element);
			grid9.SetValue(Visual.ClipToBoundsProperty, value: true, BindingPriority.Template);
			RowDefinitions rowDefinitions = new RowDefinitions();
			rowDefinitions.Capacity = 2;
			rowDefinitions.Add(new RowDefinition(new GridLength(1.0, GridUnitType.Star)));
			rowDefinitions.Add(new RowDefinition(new GridLength(0.0, GridUnitType.Auto)));
			grid9.RowDefinitions = rowDefinitions;
			Controls children2 = grid9.Children;
			ScrollViewer scrollViewer;
			ScrollViewer scrollViewer2 = (scrollViewer = new ScrollViewer());
			((ISupportInitialize)scrollViewer2).BeginInit();
			children2.Add(scrollViewer2);
			ScrollViewer scrollViewer3;
			ScrollViewer scrollViewer4 = (scrollViewer3 = scrollViewer);
			context.PushParent(scrollViewer3);
			scrollViewer3.Name = "ContentScrollViewer";
			element = scrollViewer3;
			context.AvaloniaNameScope.Register("ContentScrollViewer", element);
			scrollViewer3.SetValue(ScrollViewer.HorizontalScrollBarVisibilityProperty, ScrollBarVisibility.Disabled, BindingPriority.Template);
			scrollViewer3.SetValue(ScrollViewer.VerticalScrollBarVisibilityProperty, ScrollBarVisibility.Disabled, BindingPriority.Template);
			Border border15;
			Border border16 = (border15 = new Border());
			((ISupportInitialize)border16).BeginInit();
			scrollViewer3.Content = border16;
			Border border17 = (border = border15);
			context.PushParent(border);
			Border border18 = border;
			StyledProperty<IBrush?> backgroundProperty2 = Border.BackgroundProperty;
			DynamicResourceExtension dynamicResourceExtension2 = new DynamicResourceExtension("ContentDialogTopOverlay");
			context.ProvideTargetProperty = Border.BackgroundProperty;
			BindingBase binding2 = dynamicResourceExtension2.ProvideValue(context);
			context.ProvideTargetProperty = null;
			border18.Bind(backgroundProperty2, binding2);
			StyledProperty<Thickness> paddingProperty = Decorator.PaddingProperty;
			DynamicResourceExtension dynamicResourceExtension3 = new DynamicResourceExtension("ContentDialogPadding");
			context.ProvideTargetProperty = Decorator.PaddingProperty;
			BindingBase binding3 = dynamicResourceExtension3.ProvideValue(context);
			context.ProvideTargetProperty = null;
			border18.Bind(paddingProperty, binding3);
			StyledProperty<Thickness> borderThicknessProperty = Border.BorderThicknessProperty;
			DynamicResourceExtension dynamicResourceExtension4 = new DynamicResourceExtension("ContentDialogSeparatorThickness");
			context.ProvideTargetProperty = Border.BorderThicknessProperty;
			BindingBase binding4 = dynamicResourceExtension4.ProvideValue(context);
			context.ProvideTargetProperty = null;
			border18.Bind(borderThicknessProperty, binding4);
			StyledProperty<IBrush?> borderBrushProperty = Border.BorderBrushProperty;
			DynamicResourceExtension dynamicResourceExtension5 = new DynamicResourceExtension("ContentDialogSeparatorBorderBrush");
			context.ProvideTargetProperty = Border.BorderBrushProperty;
			BindingBase binding5 = dynamicResourceExtension5.ProvideValue(context);
			context.ProvideTargetProperty = null;
			border18.Bind(borderBrushProperty, binding5);
			Grid grid10;
			Grid grid11 = (grid10 = new Grid());
			((ISupportInitialize)grid11).BeginInit();
			border18.Child = grid11;
			Grid grid12 = (grid3 = grid10);
			context.PushParent(grid3);
			Grid grid13 = grid3;
			RowDefinitions rowDefinitions2 = new RowDefinitions();
			rowDefinitions2.Capacity = 2;
			rowDefinitions2.Add(new RowDefinition(new GridLength(0.0, GridUnitType.Auto)));
			rowDefinitions2.Add(new RowDefinition(new GridLength(1.0, GridUnitType.Star)));
			grid13.RowDefinitions = rowDefinitions2;
			Styles styles = grid13.Styles;
			Style style = new Style();
			style.Selector = ((Selector?)null).OfType(typeof(TextBlock));
			Setter setter = new Setter();
			setter.Property = TextBlock.TextWrappingProperty;
			setter.Value = TextWrapping.Wrap;
			style.Add(setter);
			styles.Add(style);
			Controls children3 = grid13.Children;
			ContentPresenter contentPresenter;
			ContentPresenter contentPresenter2 = (contentPresenter = new ContentPresenter());
			((ISupportInitialize)contentPresenter2).BeginInit();
			children3.Add(contentPresenter2);
			ContentPresenter contentPresenter3;
			ContentPresenter contentPresenter4 = (contentPresenter3 = contentPresenter);
			context.PushParent(contentPresenter3);
			ContentPresenter contentPresenter5 = contentPresenter3;
			contentPresenter5.Name = "PART_TitlePresenter";
			element = contentPresenter5;
			context.AvaloniaNameScope.Register("PART_TitlePresenter", element);
			StaticResourceExtension staticResourceExtension2 = new StaticResourceExtension("ContentDialogTitleMargin");
			context.ProvideTargetProperty = Layoutable.MarginProperty;
			object? obj2 = staticResourceExtension2.ProvideValue(context);
			context.ProvideTargetProperty = null;
			CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_4(contentPresenter5, BindingPriority.Template, obj2);
			CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(contentPresenter5, BindingPriority.Template, new TemplateBinding(ContentDialog.TitleProperty).ProvideValue());
			contentPresenter5.Bind(ContentPresenter.ContentTemplateProperty, new TemplateBinding(ContentDialog.TitleTemplateProperty).ProvideValue());
			contentPresenter5.SetValue(ContentPresenter.FontSizeProperty, 20.0, BindingPriority.Template);
			StyledProperty<bool> isVisibleProperty = Visual.IsVisibleProperty;
			DynamicResourceExtension dynamicResourceExtension6 = new DynamicResourceExtension("ContentDialogTitleIsVisible");
			context.ProvideTargetProperty = Visual.IsVisibleProperty;
			BindingBase binding6 = dynamicResourceExtension6.ProvideValue(context);
			context.ProvideTargetProperty = null;
			contentPresenter5.Bind(isVisibleProperty, binding6);
			StyledProperty<FontFamily> fontFamilyProperty = ContentPresenter.FontFamilyProperty;
			DynamicResourceExtension dynamicResourceExtension7 = new DynamicResourceExtension("SystemFontFamily");
			context.ProvideTargetProperty = ContentPresenter.FontFamilyProperty;
			BindingBase binding7 = dynamicResourceExtension7.ProvideValue(context);
			context.ProvideTargetProperty = null;
			contentPresenter5.Bind(fontFamilyProperty, binding7);
			contentPresenter5.SetValue(ContentPresenter.FontWeightProperty, FontWeight.DemiBold, BindingPriority.Template);
			contentPresenter5.Bind(ContentPresenter.ForegroundProperty, new TemplateBinding(TemplatedControl.ForegroundProperty).ProvideValue());
			contentPresenter5.SetValue(Layoutable.HorizontalAlignmentProperty, HorizontalAlignment.Left, BindingPriority.Template);
			contentPresenter5.SetValue(Layoutable.VerticalAlignmentProperty, VerticalAlignment.Top, BindingPriority.Template);
			context.PopParent();
			((ISupportInitialize)contentPresenter4).EndInit();
			Controls children4 = grid13.Children;
			ContentPresenter contentPresenter6;
			ContentPresenter contentPresenter7 = (contentPresenter6 = new ContentPresenter());
			((ISupportInitialize)contentPresenter7).BeginInit();
			children4.Add(contentPresenter7);
			ContentPresenter contentPresenter8 = (contentPresenter3 = contentPresenter6);
			context.PushParent(contentPresenter3);
			ContentPresenter contentPresenter9 = contentPresenter3;
			contentPresenter9.Name = "PART_ContentPresenter";
			element = contentPresenter9;
			context.AvaloniaNameScope.Register("PART_ContentPresenter", element);
			contentPresenter9.Bind(ContentPresenter.ContentTemplateProperty, new TemplateBinding(ContentControl.ContentTemplateProperty).ProvideValue());
			CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(contentPresenter9, BindingPriority.Template, new TemplateBinding(ContentControl.ContentProperty).ProvideValue());
			StaticResourceExtension staticResourceExtension3 = new StaticResourceExtension("ControlContentThemeFontSize");
			context.ProvideTargetProperty = ContentPresenter.FontSizeProperty;
			object? obj3 = staticResourceExtension3.ProvideValue(context);
			context.ProvideTargetProperty = null;
			CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_5(contentPresenter9, BindingPriority.Template, obj3);
			StyledProperty<FontFamily> fontFamilyProperty2 = ContentPresenter.FontFamilyProperty;
			DynamicResourceExtension dynamicResourceExtension8 = new DynamicResourceExtension("SystemFontFamily");
			context.ProvideTargetProperty = ContentPresenter.FontFamilyProperty;
			BindingBase binding8 = dynamicResourceExtension8.ProvideValue(context);
			context.ProvideTargetProperty = null;
			contentPresenter9.Bind(fontFamilyProperty2, binding8);
			contentPresenter9.Bind(ContentPresenter.ForegroundProperty, new TemplateBinding(TemplatedControl.ForegroundProperty).ProvideValue());
			contentPresenter9.SetValue(Grid.RowProperty, 1, BindingPriority.Template);
			context.PopParent();
			((ISupportInitialize)contentPresenter8).EndInit();
			context.PopParent();
			((ISupportInitialize)grid12).EndInit();
			context.PopParent();
			((ISupportInitialize)border17).EndInit();
			context.PopParent();
			((ISupportInitialize)scrollViewer4).EndInit();
			Controls children5 = grid9.Children;
			Border border19;
			Border border20 = (border19 = new Border());
			((ISupportInitialize)border20).BeginInit();
			children5.Add(border20);
			Border border21 = (border = border19);
			context.PushParent(border);
			Border border22 = border;
			StyledProperty<Thickness> paddingProperty2 = Decorator.PaddingProperty;
			DynamicResourceExtension dynamicResourceExtension9 = new DynamicResourceExtension("ContentDialogPadding");
			context.ProvideTargetProperty = Decorator.PaddingProperty;
			BindingBase binding9 = dynamicResourceExtension9.ProvideValue(context);
			context.ProvideTargetProperty = null;
			border22.Bind(paddingProperty2, binding9);
			border22.SetValue(Grid.RowProperty, 1, BindingPriority.Template);
			border22.SetValue(Layoutable.HorizontalAlignmentProperty, HorizontalAlignment.Stretch, BindingPriority.Template);
			border22.SetValue(Layoutable.VerticalAlignmentProperty, VerticalAlignment.Bottom, BindingPriority.Template);
			border22.Bind(Border.BackgroundProperty, new TemplateBinding(TemplatedControl.BackgroundProperty).ProvideValue());
			Grid grid14;
			Grid grid15 = (grid14 = new Grid());
			((ISupportInitialize)grid15).BeginInit();
			border22.Child = grid15;
			grid14.Name = "CommandSpace";
			element = grid14;
			context.AvaloniaNameScope.Register("CommandSpace", element);
			ColumnDefinitions columnDefinitions = grid14.ColumnDefinitions;
			ColumnDefinition columnDefinition = new ColumnDefinition();
			columnDefinition.SetValue(ColumnDefinition.WidthProperty, new GridLength(1.0, GridUnitType.Star), BindingPriority.Template);
			columnDefinitions.Add(columnDefinition);
			ColumnDefinitions columnDefinitions2 = grid14.ColumnDefinitions;
			ColumnDefinition columnDefinition2 = new ColumnDefinition();
			columnDefinition2.SetValue(ColumnDefinition.WidthProperty, new GridLength(0.5, GridUnitType.Star), BindingPriority.Template);
			columnDefinitions2.Add(columnDefinition2);
			ColumnDefinitions columnDefinitions3 = grid14.ColumnDefinitions;
			ColumnDefinition columnDefinition3 = new ColumnDefinition();
			columnDefinition3.SetValue(ColumnDefinition.WidthProperty, new GridLength(0.5, GridUnitType.Star), BindingPriority.Template);
			columnDefinitions3.Add(columnDefinition3);
			ColumnDefinitions columnDefinitions4 = grid14.ColumnDefinitions;
			ColumnDefinition columnDefinition4 = new ColumnDefinition();
			columnDefinition4.SetValue(ColumnDefinition.WidthProperty, new GridLength(1.0, GridUnitType.Star), BindingPriority.Template);
			columnDefinitions4.Add(columnDefinition4);
			Controls children6 = grid14.Children;
			Button button;
			Button button2 = (button = new Button());
			((ISupportInitialize)button2).BeginInit();
			children6.Add(button2);
			button.Name = "PART_PrimaryButton";
			element = button;
			context.AvaloniaNameScope.Register("PART_PrimaryButton", element);
			CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_6(button, BindingPriority.Template, new TemplateBinding(ContentDialog.PrimaryButtonTextProperty).ProvideValue());
			button.Bind(InputElement.IsEnabledProperty, new TemplateBinding(ContentDialog.IsPrimaryButtonEnabledProperty).ProvideValue());
			button.Bind(Button.CommandProperty, new TemplateBinding(ContentDialog.PrimaryButtonCommandProperty).ProvideValue());
			CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_7(button, BindingPriority.Template, new TemplateBinding(ContentDialog.PrimaryButtonCommandParameterProperty).ProvideValue());
			button.SetValue(Layoutable.HorizontalAlignmentProperty, HorizontalAlignment.Stretch, BindingPriority.Template);
			button.SetValue(Layoutable.VerticalAlignmentProperty, VerticalAlignment.Stretch, BindingPriority.Template);
			button.SetValue(ContentControl.HorizontalContentAlignmentProperty, HorizontalAlignment.Center, BindingPriority.Template);
			button.SetValue(ContentControl.VerticalContentAlignmentProperty, VerticalAlignment.Center, BindingPriority.Template);
			button.SetValue(Visual.IsVisibleProperty, value: false, BindingPriority.Template);
			((ISupportInitialize)button).EndInit();
			Controls children7 = grid14.Children;
			Button button3;
			Button button4 = (button3 = new Button());
			((ISupportInitialize)button4).BeginInit();
			children7.Add(button4);
			button3.Name = "PART_SecondaryButton";
			element = button3;
			context.AvaloniaNameScope.Register("PART_SecondaryButton", element);
			CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_6(button3, BindingPriority.Template, new TemplateBinding(ContentDialog.SecondaryButtonTextProperty).ProvideValue());
			button3.Bind(InputElement.IsEnabledProperty, new TemplateBinding(ContentDialog.IsSecondaryButtonEnabledProperty).ProvideValue());
			button3.Bind(Button.CommandProperty, new TemplateBinding(ContentDialog.SecondaryButtonCommandProperty).ProvideValue());
			CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_7(button3, BindingPriority.Template, new TemplateBinding(ContentDialog.SecondaryButtonCommandParameterProperty).ProvideValue());
			button3.SetValue(Layoutable.HorizontalAlignmentProperty, HorizontalAlignment.Stretch, BindingPriority.Template);
			button3.SetValue(Layoutable.VerticalAlignmentProperty, VerticalAlignment.Stretch, BindingPriority.Template);
			button3.SetValue(ContentControl.HorizontalContentAlignmentProperty, HorizontalAlignment.Center, BindingPriority.Template);
			button3.SetValue(ContentControl.VerticalContentAlignmentProperty, VerticalAlignment.Center, BindingPriority.Template);
			button3.SetValue(Visual.IsVisibleProperty, value: false, BindingPriority.Template);
			((ISupportInitialize)button3).EndInit();
			((ISupportInitialize)grid14).EndInit();
			context.PopParent();
			((ISupportInitialize)border21).EndInit();
			context.PopParent();
			((ISupportInitialize)grid8).EndInit();
			context.PopParent();
			((ISupportInitialize)border13).EndInit();
			Controls children8 = grid5.Children;
			Button button5;
			Button button6 = (button5 = new Button());
			((ISupportInitialize)button6).BeginInit();
			children8.Add(button6);
			Button button7;
			Button button8 = (button7 = button5);
			context.PushParent(button7);
			button7.Name = "PART_CloseButton";
			element = button7;
			context.AvaloniaNameScope.Register("PART_CloseButton", element);
			button7.SetValue(StyledElement.ThemeProperty, null, BindingPriority.Template);
			button7.SetValue(Layoutable.HorizontalAlignmentProperty, HorizontalAlignment.Right, BindingPriority.Template);
			button7.SetValue(Layoutable.VerticalAlignmentProperty, VerticalAlignment.Top, BindingPriority.Template);
			button7.SetValue(TemplatedControl.PaddingProperty, new Thickness(0.0, 0.0, 0.0, 0.0), BindingPriority.Template);
			button7.SetValue(TemplatedControl.BorderThicknessProperty, new Thickness(0.0, 0.0, 0.0, 0.0), BindingPriority.Template);
			button7.SetValue(Layoutable.MarginProperty, new Thickness(0.0, -40.0, 0.0, 0.0), BindingPriority.Template);
			StyledProperty<IControlTemplate?> templateProperty = TemplatedControl.TemplateProperty;
			ControlTemplate controlTemplate;
			ControlTemplate value = (controlTemplate = new ControlTemplate());
			context.PushParent(controlTemplate);
			controlTemplate.Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&Build_10), context);
			context.PopParent();
			button7.SetValue(templateProperty, value, BindingPriority.Template);
			context.PopParent();
			((ISupportInitialize)button8).EndInit();
			context.PopParent();
			((ISupportInitialize)grid4).EndInit();
			context.PopParent();
			((ISupportInitialize)border9).EndInit();
			context.PopParent();
			((ISupportInitialize)border5).EndInit();
			context.PopParent();
			((ISupportInitialize)intermediateRoot).EndInit();
			return intermediateRoot;
		}

		public static object Build_10(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<ResourceDictionary> context = CreateContext(P_0);
			context.IntermediateRoot = new Border();
			object intermediateRoot = context.IntermediateRoot;
			((ISupportInitialize)intermediateRoot).BeginInit();
			Border border = (Border)intermediateRoot;
			context.PushParent(border);
			border.SetValue(Layoutable.WidthProperty, 32.0, BindingPriority.Template);
			border.SetValue(Layoutable.HeightProperty, 32.0, BindingPriority.Template);
			border.SetValue(Border.CornerRadiusProperty, new CornerRadius(99.0, 99.0, 99.0, 99.0), BindingPriority.Template);
			border.SetValue(Border.BackgroundProperty, new ImmutableSolidColorBrush(4287861651u), BindingPriority.Template);
			Image image;
			Image image2 = (image = new Image());
			((ISupportInitialize)image2).BeginInit();
			border.Child = image2;
			Image image3;
			Image image4 = (image3 = image);
			context.PushParent(image3);
			image3.SetValue(Layoutable.WidthProperty, 30.0, BindingPriority.Template);
			image3.SetValue(Layoutable.HeightProperty, 30.0, BindingPriority.Template);
			SvgImageExtension svgImageExtension = new SvgImageExtension("/Assets/Images/ic_close.svg");
			context.ProvideTargetProperty = Image.SourceProperty;
			object obj = svgImageExtension.ProvideValue(context);
			context.ProvideTargetProperty = null;
			CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_8(image3, BindingPriority.Template, obj);
			context.PopParent();
			((ISupportInitialize)image4).EndInit();
			context.PopParent();
			((ISupportInitialize)intermediateRoot).EndInit();
			return intermediateRoot;
		}

		public unsafe static object Build_11(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<ResourceDictionary> context = CreateContext(P_0);
			ControlTheme controlTheme;
			ControlTheme result = (controlTheme = new ControlTheme());
			context.PushParent(controlTheme);
			controlTheme.TargetType = typeof(ContentDialog);
			Setter setter;
			Setter setter2 = (setter = new Setter());
			context.PushParent(setter);
			Setter setter3 = setter;
			setter3.Property = TemplatedControl.ForegroundProperty;
			DynamicResourceExtension dynamicResourceExtension = new DynamicResourceExtension("ContentDialogForeground");
			context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
			BindingBase value = dynamicResourceExtension.ProvideValue(context);
			context.ProvideTargetProperty = null;
			setter3.Value = value;
			context.PopParent();
			controlTheme.Add(setter2);
			Setter setter4 = (setter = new Setter());
			context.PushParent(setter);
			Setter setter5 = setter;
			setter5.Property = TemplatedControl.BackgroundProperty;
			DynamicResourceExtension dynamicResourceExtension2 = new DynamicResourceExtension("ContentDialogBackground");
			context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
			BindingBase value2 = dynamicResourceExtension2.ProvideValue(context);
			context.ProvideTargetProperty = null;
			setter5.Value = value2;
			context.PopParent();
			controlTheme.Add(setter4);
			Setter setter6 = (setter = new Setter());
			context.PushParent(setter);
			Setter setter7 = setter;
			setter7.Property = TemplatedControl.BorderBrushProperty;
			DynamicResourceExtension dynamicResourceExtension3 = new DynamicResourceExtension("ContentDialogBorderBrush");
			context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
			BindingBase value3 = dynamicResourceExtension3.ProvideValue(context);
			context.ProvideTargetProperty = null;
			setter7.Value = value3;
			context.PopParent();
			controlTheme.Add(setter6);
			Setter setter8 = (setter = new Setter());
			context.PushParent(setter);
			Setter setter9 = setter;
			setter9.Property = TemplatedControl.BorderThicknessProperty;
			DynamicResourceExtension dynamicResourceExtension4 = new DynamicResourceExtension("ContentDialogBorderWidth");
			context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
			BindingBase value4 = dynamicResourceExtension4.ProvideValue(context);
			context.ProvideTargetProperty = null;
			setter9.Value = value4;
			context.PopParent();
			controlTheme.Add(setter8);
			Setter setter10 = (setter = new Setter());
			context.PushParent(setter);
			Setter setter11 = setter;
			setter11.Property = TemplatedControl.CornerRadiusProperty;
			DynamicResourceExtension dynamicResourceExtension5 = new DynamicResourceExtension("OverlayCornerRadius");
			context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
			BindingBase value5 = dynamicResourceExtension5.ProvideValue(context);
			context.ProvideTargetProperty = null;
			setter11.Value = value5;
			context.PopParent();
			controlTheme.Add(setter10);
			Setter setter12 = (setter = new Setter());
			context.PushParent(setter);
			Setter setter13 = setter;
			setter13.Property = TemplatedControl.TemplateProperty;
			ControlTemplate controlTemplate;
			ControlTemplate value6 = (controlTemplate = new ControlTemplate());
			context.PushParent(controlTemplate);
			controlTemplate.Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&Build_12), context);
			context.PopParent();
			setter13.Value = value6;
			context.PopParent();
			controlTheme.Add(setter12);
			context.PopParent();
			return result;
		}

		public static object Build_12(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<ResourceDictionary> context = CreateContext(P_0);
			context.IntermediateRoot = new Border();
			object intermediateRoot = context.IntermediateRoot;
			((ISupportInitialize)intermediateRoot).BeginInit();
			Border border = (Border)intermediateRoot;
			context.PushParent(border);
			Border border2 = border;
			border2.Name = "Container";
			object element = border2;
			context.AvaloniaNameScope.Register("Container", element);
			Border border3;
			Border border4 = (border3 = new Border());
			((ISupportInitialize)border4).BeginInit();
			border2.Child = border4;
			Border border5 = (border = border3);
			context.PushParent(border);
			Border border6 = border;
			border6.Name = "PART_LayoutRoot";
			element = border6;
			context.AvaloniaNameScope.Register("PART_LayoutRoot", element);
			StyledProperty<IBrush?> backgroundProperty = Border.BackgroundProperty;
			DynamicResourceExtension dynamicResourceExtension = new DynamicResourceExtension("ContentDialogSmokeFill");
			context.ProvideTargetProperty = Border.BackgroundProperty;
			BindingBase binding = dynamicResourceExtension.ProvideValue(context);
			context.ProvideTargetProperty = null;
			border6.Bind(backgroundProperty, binding);
			Border border7;
			Border border8 = (border7 = new Border());
			((ISupportInitialize)border8).BeginInit();
			border6.Child = border8;
			Border border9 = (border = border7);
			context.PushParent(border);
			Border border10 = border;
			border10.Name = "BackgroundElement";
			element = border10;
			context.AvaloniaNameScope.Register("BackgroundElement", element);
			border10.Bind(Border.BackgroundProperty, new TemplateBinding(TemplatedControl.BackgroundProperty).ProvideValue());
			StaticResourceExtension staticResourceExtension = new StaticResourceExtension("ContentDialogBorderWidth");
			context.ProvideTargetProperty = Border.BorderThicknessProperty;
			object? obj = staticResourceExtension.ProvideValue(context);
			context.ProvideTargetProperty = null;
			CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_3(border10, BindingPriority.Template, obj);
			border10.Bind(Border.BorderBrushProperty, new TemplateBinding(TemplatedControl.BorderBrushProperty).ProvideValue());
			border10.Bind(Border.CornerRadiusProperty, new TemplateBinding(TemplatedControl.CornerRadiusProperty).ProvideValue());
			border10.SetValue(Layoutable.HorizontalAlignmentProperty, HorizontalAlignment.Center, BindingPriority.Template);
			border10.SetValue(Layoutable.VerticalAlignmentProperty, VerticalAlignment.Center, BindingPriority.Template);
			border10.SetValue(Border.BoxShadowProperty, BoxShadows.Parse("0 8 32 0 #66000000"), BindingPriority.Template);
			Border border11;
			Border border12 = (border11 = new Border());
			((ISupportInitialize)border12).BeginInit();
			border10.Child = border12;
			Border border13 = (border = border11);
			context.PushParent(border);
			Border border14 = border;
			border14.SetValue(Visual.ClipToBoundsProperty, value: true, BindingPriority.Template);
			border14.SetValue(Layoutable.WidthProperty, 380.0, BindingPriority.Template);
			border14.Bind(Border.CornerRadiusProperty, new TemplateBinding(TemplatedControl.CornerRadiusProperty).ProvideValue());
			Grid grid;
			Grid grid2 = (grid = new Grid());
			((ISupportInitialize)grid2).BeginInit();
			border14.Child = grid2;
			Grid grid3;
			Grid grid4 = (grid3 = grid);
			context.PushParent(grid3);
			Grid grid5 = grid3;
			Controls children = grid5.Children;
			ExperimentalAcrylicBorder experimentalAcrylicBorder;
			ExperimentalAcrylicBorder experimentalAcrylicBorder2 = (experimentalAcrylicBorder = new ExperimentalAcrylicBorder());
			((ISupportInitialize)experimentalAcrylicBorder2).BeginInit();
			children.Add(experimentalAcrylicBorder2);
			StyledProperty<ExperimentalAcrylicMaterial?> materialProperty = ExperimentalAcrylicBorder.MaterialProperty;
			ExperimentalAcrylicMaterial experimentalAcrylicMaterial = new ExperimentalAcrylicMaterial();
			experimentalAcrylicMaterial.SetValue(ExperimentalAcrylicMaterial.BackgroundSourceProperty, AcrylicBackgroundSource.None, BindingPriority.Template);
			experimentalAcrylicMaterial.SetValue(ExperimentalAcrylicMaterial.TintColorProperty, Color.FromUInt32(4286283136u), BindingPriority.Template);
			experimentalAcrylicMaterial.SetValue(ExperimentalAcrylicMaterial.TintOpacityProperty, 1.0, BindingPriority.Template);
			experimentalAcrylicMaterial.SetValue(ExperimentalAcrylicMaterial.MaterialOpacityProperty, 0.8, BindingPriority.Template);
			experimentalAcrylicBorder.SetValue(materialProperty, experimentalAcrylicMaterial, BindingPriority.Template);
			((ISupportInitialize)experimentalAcrylicBorder).EndInit();
			Controls children2 = grid5.Children;
			Ellipse ellipse;
			Ellipse ellipse2 = (ellipse = new Ellipse());
			((ISupportInitialize)ellipse2).BeginInit();
			children2.Add(ellipse2);
			ellipse.SetValue(InputElement.IsHitTestVisibleProperty, value: false, BindingPriority.Template);
			ellipse.Bind(Visual.IsVisibleProperty, new TemplateBinding(Control.TagProperty)
			{
				Converter = ObjectConverters.IsNotNull
			}.ProvideValue());
			ellipse.SetValue(Shape.FillProperty, new ImmutableSolidColorBrush(4294926132u), BindingPriority.Template);
			ellipse.SetValue(Layoutable.WidthProperty, 290.0, BindingPriority.Template);
			ellipse.SetValue(Layoutable.HeightProperty, 152.0, BindingPriority.Template);
			ellipse.SetValue(Layoutable.MarginProperty, new Thickness(0.0, -76.0, 0.0, 0.0), BindingPriority.Template);
			ellipse.SetValue(Layoutable.HorizontalAlignmentProperty, HorizontalAlignment.Center, BindingPriority.Template);
			ellipse.SetValue(Layoutable.VerticalAlignmentProperty, VerticalAlignment.Top, BindingPriority.Template);
			ellipse.SetValue(Visual.OpacityProperty, 0.3, BindingPriority.Template);
			StyledProperty<IEffect?> effectProperty = Visual.EffectProperty;
			BlurEffect blurEffect = new BlurEffect();
			blurEffect.SetValue(BlurEffect.RadiusProperty, 40.0, BindingPriority.Template);
			ellipse.SetValue(effectProperty, blurEffect, BindingPriority.Template);
			((ISupportInitialize)ellipse).EndInit();
			Controls children3 = grid5.Children;
			Grid grid6;
			Grid grid7 = (grid6 = new Grid());
			((ISupportInitialize)grid7).BeginInit();
			children3.Add(grid7);
			Grid grid8 = (grid3 = grid6);
			context.PushParent(grid3);
			Grid grid9 = grid3;
			grid9.Name = "DialogSpace";
			element = grid9;
			context.AvaloniaNameScope.Register("DialogSpace", element);
			grid9.SetValue(Layoutable.MarginProperty, new Thickness(24.0, 24.0, 24.0, 24.0), BindingPriority.Template);
			grid9.SetValue(Visual.ClipToBoundsProperty, value: true, BindingPriority.Template);
			RowDefinitions rowDefinitions = new RowDefinitions();
			rowDefinitions.Capacity = 2;
			rowDefinitions.Add(new RowDefinition(new GridLength(1.0, GridUnitType.Star)));
			rowDefinitions.Add(new RowDefinition(new GridLength(0.0, GridUnitType.Auto)));
			grid9.RowDefinitions = rowDefinitions;
			Controls children4 = grid9.Children;
			ScrollViewer scrollViewer;
			ScrollViewer scrollViewer2 = (scrollViewer = new ScrollViewer());
			((ISupportInitialize)scrollViewer2).BeginInit();
			children4.Add(scrollViewer2);
			ScrollViewer scrollViewer3;
			ScrollViewer scrollViewer4 = (scrollViewer3 = scrollViewer);
			context.PushParent(scrollViewer3);
			scrollViewer3.Name = "ContentScrollViewer";
			element = scrollViewer3;
			context.AvaloniaNameScope.Register("ContentScrollViewer", element);
			scrollViewer3.SetValue(Layoutable.MarginProperty, new Thickness(0.0, 0.0, 0.0, 24.0), BindingPriority.Template);
			scrollViewer3.SetValue(ScrollViewer.HorizontalScrollBarVisibilityProperty, ScrollBarVisibility.Disabled, BindingPriority.Template);
			scrollViewer3.SetValue(ScrollViewer.VerticalScrollBarVisibilityProperty, ScrollBarVisibility.Auto, BindingPriority.Template);
			Border border15;
			Border border16 = (border15 = new Border());
			((ISupportInitialize)border16).BeginInit();
			scrollViewer3.Content = border16;
			Border border17 = (border = border15);
			context.PushParent(border);
			Border border18 = border;
			StyledProperty<IBrush?> backgroundProperty2 = Border.BackgroundProperty;
			DynamicResourceExtension dynamicResourceExtension2 = new DynamicResourceExtension("ContentDialogTopOverlay");
			context.ProvideTargetProperty = Border.BackgroundProperty;
			BindingBase binding2 = dynamicResourceExtension2.ProvideValue(context);
			context.ProvideTargetProperty = null;
			border18.Bind(backgroundProperty2, binding2);
			StyledProperty<Thickness> paddingProperty = Decorator.PaddingProperty;
			DynamicResourceExtension dynamicResourceExtension3 = new DynamicResourceExtension("ContentDialogPadding");
			context.ProvideTargetProperty = Decorator.PaddingProperty;
			BindingBase binding3 = dynamicResourceExtension3.ProvideValue(context);
			context.ProvideTargetProperty = null;
			border18.Bind(paddingProperty, binding3);
			StyledProperty<Thickness> borderThicknessProperty = Border.BorderThicknessProperty;
			DynamicResourceExtension dynamicResourceExtension4 = new DynamicResourceExtension("ContentDialogSeparatorThickness");
			context.ProvideTargetProperty = Border.BorderThicknessProperty;
			BindingBase binding4 = dynamicResourceExtension4.ProvideValue(context);
			context.ProvideTargetProperty = null;
			border18.Bind(borderThicknessProperty, binding4);
			StyledProperty<IBrush?> borderBrushProperty = Border.BorderBrushProperty;
			DynamicResourceExtension dynamicResourceExtension5 = new DynamicResourceExtension("ContentDialogSeparatorBorderBrush");
			context.ProvideTargetProperty = Border.BorderBrushProperty;
			BindingBase binding5 = dynamicResourceExtension5.ProvideValue(context);
			context.ProvideTargetProperty = null;
			border18.Bind(borderBrushProperty, binding5);
			Grid grid10;
			Grid grid11 = (grid10 = new Grid());
			((ISupportInitialize)grid11).BeginInit();
			border18.Child = grid11;
			Grid grid12 = (grid3 = grid10);
			context.PushParent(grid3);
			Grid grid13 = grid3;
			RowDefinitions rowDefinitions2 = new RowDefinitions();
			rowDefinitions2.Capacity = 3;
			rowDefinitions2.Add(new RowDefinition(new GridLength(0.0, GridUnitType.Auto)));
			rowDefinitions2.Add(new RowDefinition(new GridLength(0.0, GridUnitType.Auto)));
			rowDefinitions2.Add(new RowDefinition(new GridLength(1.0, GridUnitType.Star)));
			grid13.RowDefinitions = rowDefinitions2;
			Styles styles = grid13.Styles;
			Style style = new Style();
			style.Selector = ((Selector?)null).OfType(typeof(TextBlock));
			Setter setter = new Setter();
			setter.Property = TextBlock.TextWrappingProperty;
			setter.Value = TextWrapping.Wrap;
			style.Add(setter);
			styles.Add(style);
			Controls children5 = grid13.Children;
			ContentPresenter contentPresenter;
			ContentPresenter contentPresenter2 = (contentPresenter = new ContentPresenter());
			((ISupportInitialize)contentPresenter2).BeginInit();
			children5.Add(contentPresenter2);
			contentPresenter.Name = "PART_IconPresenter";
			element = contentPresenter;
			context.AvaloniaNameScope.Register("PART_IconPresenter", element);
			contentPresenter.SetValue(Layoutable.MarginProperty, new Thickness(0.0, 0.0, 0.0, 16.0), BindingPriority.Template);
			contentPresenter.SetValue(Layoutable.VerticalAlignmentProperty, VerticalAlignment.Center, BindingPriority.Template);
			contentPresenter.SetValue(Layoutable.HorizontalAlignmentProperty, HorizontalAlignment.Center, BindingPriority.Template);
			CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(contentPresenter, BindingPriority.Template, new TemplateBinding(Control.TagProperty).ProvideValue());
			contentPresenter.Bind(Visual.IsVisibleProperty, new TemplateBinding(Control.TagProperty)
			{
				Converter = ObjectConverters.IsNotNull
			}.ProvideValue());
			((ISupportInitialize)contentPresenter).EndInit();
			Controls children6 = grid13.Children;
			ContentPresenter contentPresenter3;
			ContentPresenter contentPresenter4 = (contentPresenter3 = new ContentPresenter());
			((ISupportInitialize)contentPresenter4).BeginInit();
			children6.Add(contentPresenter4);
			ContentPresenter contentPresenter5;
			ContentPresenter contentPresenter6 = (contentPresenter5 = contentPresenter3);
			context.PushParent(contentPresenter5);
			ContentPresenter contentPresenter7 = contentPresenter5;
			contentPresenter7.Name = "PART_TitlePresenter";
			element = contentPresenter7;
			context.AvaloniaNameScope.Register("PART_TitlePresenter", element);
			contentPresenter7.SetValue(Layoutable.MarginProperty, new Thickness(0.0, 0.0, 0.0, 8.0), BindingPriority.Template);
			CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(contentPresenter7, BindingPriority.Template, new TemplateBinding(ContentDialog.TitleProperty).ProvideValue());
			contentPresenter7.Bind(ContentPresenter.ContentTemplateProperty, new TemplateBinding(ContentDialog.TitleTemplateProperty).ProvideValue());
			contentPresenter7.SetValue(ContentPresenter.FontSizeProperty, 20.0, BindingPriority.Template);
			contentPresenter7.Bind(Visual.IsVisibleProperty, new TemplateBinding(ContentDialog.TitleProperty)
			{
				Converter = ObjectConverters.IsNotNull
			}.ProvideValue());
			StyledProperty<FontFamily> fontFamilyProperty = ContentPresenter.FontFamilyProperty;
			DynamicResourceExtension dynamicResourceExtension6 = new DynamicResourceExtension("SystemFontFamily");
			context.ProvideTargetProperty = ContentPresenter.FontFamilyProperty;
			BindingBase binding6 = dynamicResourceExtension6.ProvideValue(context);
			context.ProvideTargetProperty = null;
			contentPresenter7.Bind(fontFamilyProperty, binding6);
			contentPresenter7.SetValue(ContentPresenter.FontWeightProperty, FontWeight.DemiBold, BindingPriority.Template);
			contentPresenter7.Bind(ContentPresenter.ForegroundProperty, new TemplateBinding(TemplatedControl.ForegroundProperty).ProvideValue());
			contentPresenter7.SetValue(Layoutable.HorizontalAlignmentProperty, HorizontalAlignment.Center, BindingPriority.Template);
			contentPresenter7.SetValue(Layoutable.VerticalAlignmentProperty, VerticalAlignment.Center, BindingPriority.Template);
			contentPresenter7.SetValue(Grid.RowProperty, 1, BindingPriority.Template);
			context.PopParent();
			((ISupportInitialize)contentPresenter6).EndInit();
			Controls children7 = grid13.Children;
			ContentPresenter contentPresenter8;
			ContentPresenter contentPresenter9 = (contentPresenter8 = new ContentPresenter());
			((ISupportInitialize)contentPresenter9).BeginInit();
			children7.Add(contentPresenter9);
			ContentPresenter contentPresenter10 = (contentPresenter5 = contentPresenter8);
			context.PushParent(contentPresenter5);
			ContentPresenter contentPresenter11 = contentPresenter5;
			contentPresenter11.Name = "PART_ContentPresenter";
			element = contentPresenter11;
			context.AvaloniaNameScope.Register("PART_ContentPresenter", element);
			contentPresenter11.Bind(ContentPresenter.ContentTemplateProperty, new TemplateBinding(ContentControl.ContentTemplateProperty).ProvideValue());
			CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(contentPresenter11, BindingPriority.Template, new TemplateBinding(ContentControl.ContentProperty).ProvideValue());
			contentPresenter11.SetValue(ContentPresenter.TextAlignmentProperty, TextAlignment.Center, BindingPriority.Template);
			contentPresenter11.SetValue(Layoutable.HorizontalAlignmentProperty, HorizontalAlignment.Center, BindingPriority.Template);
			StaticResourceExtension staticResourceExtension2 = new StaticResourceExtension("ControlContentThemeFontSize");
			context.ProvideTargetProperty = ContentPresenter.FontSizeProperty;
			object? obj2 = staticResourceExtension2.ProvideValue(context);
			context.ProvideTargetProperty = null;
			CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_5(contentPresenter11, BindingPriority.Template, obj2);
			StyledProperty<FontFamily> fontFamilyProperty2 = ContentPresenter.FontFamilyProperty;
			DynamicResourceExtension dynamicResourceExtension7 = new DynamicResourceExtension("SystemFontFamily");
			context.ProvideTargetProperty = ContentPresenter.FontFamilyProperty;
			BindingBase binding7 = dynamicResourceExtension7.ProvideValue(context);
			context.ProvideTargetProperty = null;
			contentPresenter11.Bind(fontFamilyProperty2, binding7);
			contentPresenter11.Bind(ContentPresenter.ForegroundProperty, new TemplateBinding(TemplatedControl.ForegroundProperty).ProvideValue());
			contentPresenter11.SetValue(Grid.RowProperty, 2, BindingPriority.Template);
			context.PopParent();
			((ISupportInitialize)contentPresenter10).EndInit();
			context.PopParent();
			((ISupportInitialize)grid12).EndInit();
			context.PopParent();
			((ISupportInitialize)border17).EndInit();
			context.PopParent();
			((ISupportInitialize)scrollViewer4).EndInit();
			Controls children8 = grid9.Children;
			Border border19;
			Border border20 = (border19 = new Border());
			((ISupportInitialize)border20).BeginInit();
			children8.Add(border20);
			Border border21 = (border = border19);
			context.PushParent(border);
			Border border22 = border;
			StyledProperty<Thickness> paddingProperty2 = Decorator.PaddingProperty;
			DynamicResourceExtension dynamicResourceExtension8 = new DynamicResourceExtension("ContentDialogPadding");
			context.ProvideTargetProperty = Decorator.PaddingProperty;
			BindingBase binding8 = dynamicResourceExtension8.ProvideValue(context);
			context.ProvideTargetProperty = null;
			border22.Bind(paddingProperty2, binding8);
			border22.SetValue(Grid.RowProperty, 1, BindingPriority.Template);
			border22.SetValue(Layoutable.HorizontalAlignmentProperty, HorizontalAlignment.Stretch, BindingPriority.Template);
			border22.SetValue(Layoutable.VerticalAlignmentProperty, VerticalAlignment.Bottom, BindingPriority.Template);
			border22.Bind(Border.BackgroundProperty, new TemplateBinding(TemplatedControl.BackgroundProperty).ProvideValue());
			StackPanel stackPanel;
			StackPanel stackPanel2 = (stackPanel = new StackPanel());
			((ISupportInitialize)stackPanel2).BeginInit();
			border22.Child = stackPanel2;
			StackPanel stackPanel3;
			StackPanel stackPanel4 = (stackPanel3 = stackPanel);
			context.PushParent(stackPanel3);
			stackPanel3.Name = "CommandSpace";
			element = stackPanel3;
			context.AvaloniaNameScope.Register("CommandSpace", element);
			stackPanel3.SetValue(StackPanel.SpacingProperty, 16.0, BindingPriority.Template);
			stackPanel3.SetValue(StackPanel.OrientationProperty, Orientation.Horizontal, BindingPriority.Template);
			stackPanel3.SetValue(Layoutable.HorizontalAlignmentProperty, HorizontalAlignment.Center, BindingPriority.Template);
			Controls children9 = stackPanel3.Children;
			Button button;
			Button button2 = (button = new Button());
			((ISupportInitialize)button2).BeginInit();
			children9.Add(button2);
			button.Name = "PART_CloseButton";
			element = button;
			context.AvaloniaNameScope.Register("PART_CloseButton", element);
			button.Classes.Add("base");
			CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_6(button, BindingPriority.Template, new TemplateBinding(ContentDialog.CloseButtonTextProperty).ProvideValue());
			button.Bind(Button.CommandProperty, new TemplateBinding(ContentDialog.CloseButtonCommandProperty).ProvideValue());
			CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_7(button, BindingPriority.Template, new TemplateBinding(ContentDialog.CloseButtonCommandParameterProperty).ProvideValue());
			button.Bind(Visual.IsVisibleProperty, new TemplateBinding(ContentDialog.CloseButtonTextProperty)
			{
				Converter = StringConverters.IsNotNullOrEmpty
			}.ProvideValue());
			button.SetValue(Layoutable.HorizontalAlignmentProperty, HorizontalAlignment.Center, BindingPriority.Template);
			button.SetValue(Layoutable.VerticalAlignmentProperty, VerticalAlignment.Stretch, BindingPriority.Template);
			button.SetValue(ContentControl.HorizontalContentAlignmentProperty, HorizontalAlignment.Center, BindingPriority.Template);
			button.SetValue(ContentControl.VerticalContentAlignmentProperty, VerticalAlignment.Center, BindingPriority.Template);
			((ISupportInitialize)button).EndInit();
			Controls children10 = stackPanel3.Children;
			Button button3;
			Button button4 = (button3 = new Button());
			((ISupportInitialize)button4).BeginInit();
			children10.Add(button4);
			button3.Name = "PART_SecondaryButton";
			element = button3;
			context.AvaloniaNameScope.Register("PART_SecondaryButton", element);
			button3.Classes.Add("base");
			CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_6(button3, BindingPriority.Template, new TemplateBinding(ContentDialog.SecondaryButtonTextProperty).ProvideValue());
			button3.Bind(InputElement.IsEnabledProperty, new TemplateBinding(ContentDialog.IsSecondaryButtonEnabledProperty).ProvideValue());
			button3.Bind(Button.CommandProperty, new TemplateBinding(ContentDialog.SecondaryButtonCommandProperty).ProvideValue());
			CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_7(button3, BindingPriority.Template, new TemplateBinding(ContentDialog.SecondaryButtonCommandParameterProperty).ProvideValue());
			button3.Bind(Visual.IsVisibleProperty, new TemplateBinding(ContentDialog.SecondaryButtonTextProperty)
			{
				Converter = StringConverters.IsNotNullOrEmpty
			}.ProvideValue());
			button3.SetValue(Layoutable.HorizontalAlignmentProperty, HorizontalAlignment.Center, BindingPriority.Template);
			button3.SetValue(Layoutable.VerticalAlignmentProperty, VerticalAlignment.Stretch, BindingPriority.Template);
			button3.SetValue(ContentControl.HorizontalContentAlignmentProperty, HorizontalAlignment.Center, BindingPriority.Template);
			button3.SetValue(ContentControl.VerticalContentAlignmentProperty, VerticalAlignment.Center, BindingPriority.Template);
			((ISupportInitialize)button3).EndInit();
			Controls children11 = stackPanel3.Children;
			Button button5;
			Button button6 = (button5 = new Button());
			((ISupportInitialize)button6).BeginInit();
			children11.Add(button6);
			Button button7;
			Button button8 = (button7 = button5);
			context.PushParent(button7);
			button7.Name = "PART_PrimaryButton";
			element = button7;
			context.AvaloniaNameScope.Register("PART_PrimaryButton", element);
			button7.Classes.Add("base");
			CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_6(button7, BindingPriority.Template, new TemplateBinding(ContentDialog.PrimaryButtonTextProperty).ProvideValue());
			button7.Bind(InputElement.IsEnabledProperty, new TemplateBinding(ContentDialog.IsPrimaryButtonEnabledProperty).ProvideValue());
			button7.Bind(Button.CommandProperty, new TemplateBinding(ContentDialog.PrimaryButtonCommandProperty).ProvideValue());
			CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_7(button7, BindingPriority.Template, new TemplateBinding(ContentDialog.PrimaryButtonCommandParameterProperty).ProvideValue());
			button7.Bind(Visual.IsVisibleProperty, new TemplateBinding(ContentDialog.PrimaryButtonTextProperty)
			{
				Converter = StringConverters.IsNotNullOrEmpty
			}.ProvideValue());
			button7.SetValue(Layoutable.HorizontalAlignmentProperty, HorizontalAlignment.Center, BindingPriority.Template);
			button7.SetValue(Layoutable.VerticalAlignmentProperty, VerticalAlignment.Stretch, BindingPriority.Template);
			button7.SetValue(ContentControl.HorizontalContentAlignmentProperty, HorizontalAlignment.Center, BindingPriority.Template);
			button7.SetValue(ContentControl.VerticalContentAlignmentProperty, VerticalAlignment.Center, BindingPriority.Template);
			Styles styles2 = button7.Styles;
			Style style2;
			Style item = (style2 = new Style());
			context.PushParent(style2);
			Style style3 = style2;
			style3.Selector = ((Selector?)null).OfType(typeof(Button)).Class("base").Template()
				.OfType(typeof(Border))
				.Name("PART_Border");
			Setter setter2;
			Setter setter3 = (setter2 = new Setter());
			context.PushParent(setter2);
			Setter setter4 = setter2;
			setter4.Property = Border.BackgroundProperty;
			DynamicResourceExtension dynamicResourceExtension9 = new DynamicResourceExtension("SystemAccentColorBrush");
			context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
			BindingBase value = dynamicResourceExtension9.ProvideValue(context);
			context.ProvideTargetProperty = null;
			setter4.Value = value;
			context.PopParent();
			style3.Add(setter3);
			Setter setter5 = (setter2 = new Setter());
			context.PushParent(setter2);
			Setter setter6 = setter2;
			setter6.Property = Border.BorderBrushProperty;
			DynamicResourceExtension dynamicResourceExtension10 = new DynamicResourceExtension("SystemAccentColorBrush");
			context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
			BindingBase value2 = dynamicResourceExtension10.ProvideValue(context);
			context.ProvideTargetProperty = null;
			setter6.Value = value2;
			context.PopParent();
			style3.Add(setter5);
			context.PopParent();
			styles2.Add(item);
			Styles styles3 = button7.Styles;
			Style item2 = (style2 = new Style());
			context.PushParent(style2);
			Style style4 = style2;
			style4.Selector = ((Selector?)null).OfType(typeof(Button)).Class("base").Class(":pointerover")
				.Template()
				.OfType(typeof(Border))
				.Name("PART_Border");
			Setter setter7 = (setter2 = new Setter());
			context.PushParent(setter2);
			Setter setter8 = setter2;
			setter8.Property = Border.BackgroundProperty;
			DynamicResourceExtension dynamicResourceExtension11 = new DynamicResourceExtension("SystemAccentColorLight1Brush");
			context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
			BindingBase value3 = dynamicResourceExtension11.ProvideValue(context);
			context.ProvideTargetProperty = null;
			setter8.Value = value3;
			context.PopParent();
			style4.Add(setter7);
			Setter setter9 = (setter2 = new Setter());
			context.PushParent(setter2);
			Setter setter10 = setter2;
			setter10.Property = Border.BorderBrushProperty;
			DynamicResourceExtension dynamicResourceExtension12 = new DynamicResourceExtension("SystemAccentColorLight1Brush");
			context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
			BindingBase value4 = dynamicResourceExtension12.ProvideValue(context);
			context.ProvideTargetProperty = null;
			setter10.Value = value4;
			context.PopParent();
			style4.Add(setter9);
			context.PopParent();
			styles3.Add(item2);
			Styles styles4 = button7.Styles;
			Style item3 = (style2 = new Style());
			context.PushParent(style2);
			Style style5 = style2;
			style5.Selector = ((Selector?)null).OfType(typeof(Button)).Class("base").Class(":pressed")
				.Template()
				.OfType(typeof(Border))
				.Name("PART_Border");
			Setter setter11 = (setter2 = new Setter());
			context.PushParent(setter2);
			Setter setter12 = setter2;
			setter12.Property = Border.BackgroundProperty;
			DynamicResourceExtension dynamicResourceExtension13 = new DynamicResourceExtension("SystemAccentColorDark1Brush");
			context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
			BindingBase value5 = dynamicResourceExtension13.ProvideValue(context);
			context.ProvideTargetProperty = null;
			setter12.Value = value5;
			context.PopParent();
			style5.Add(setter11);
			Setter setter13 = (setter2 = new Setter());
			context.PushParent(setter2);
			Setter setter14 = setter2;
			setter14.Property = Border.BorderBrushProperty;
			DynamicResourceExtension dynamicResourceExtension14 = new DynamicResourceExtension("SystemAccentColorDark1Brush");
			context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
			BindingBase value6 = dynamicResourceExtension14.ProvideValue(context);
			context.ProvideTargetProperty = null;
			setter14.Value = value6;
			context.PopParent();
			style5.Add(setter13);
			context.PopParent();
			styles4.Add(item3);
			context.PopParent();
			((ISupportInitialize)button8).EndInit();
			context.PopParent();
			((ISupportInitialize)stackPanel4).EndInit();
			context.PopParent();
			((ISupportInitialize)border21).EndInit();
			context.PopParent();
			((ISupportInitialize)grid8).EndInit();
			context.PopParent();
			((ISupportInitialize)grid4).EndInit();
			context.PopParent();
			((ISupportInitialize)border13).EndInit();
			context.PopParent();
			((ISupportInitialize)border9).EndInit();
			context.PopParent();
			((ISupportInitialize)border5).EndInit();
			context.PopParent();
			((ISupportInitialize)intermediateRoot).EndInit();
			return intermediateRoot;
		}
	}

	[CompilerGenerated]
	internal class NamespaceInfo_003A_002FControls_002FIconLabel_002Eaxaml : IAvaloniaXamlIlXmlNamespaceInfoProvider
	{
		private IReadOnlyDictionary<string, IReadOnlyList<AvaloniaXamlIlXmlNamespaceInfo>> _xmlNamespaces;

		public static IAvaloniaXamlIlXmlNamespaceInfoProvider Singleton;

		public virtual IReadOnlyDictionary<string, IReadOnlyList<AvaloniaXamlIlXmlNamespaceInfo>> XmlNamespaces
		{
			get
			{
				if (_xmlNamespaces == null)
				{
					_xmlNamespaces = CreateNamespaces();
				}
				return _xmlNamespaces;
			}
		}

		private static AvaloniaXamlIlXmlNamespaceInfo CreateNamespaceInfo(string P_0, string P_1)
		{
			return new AvaloniaXamlIlXmlNamespaceInfo
			{
				ClrNamespace = P_0,
				ClrAssemblyName = P_1
			};
		}

		private static IReadOnlyDictionary<string, IReadOnlyList<AvaloniaXamlIlXmlNamespaceInfo>> CreateNamespaces()
		{
			return new Dictionary<string, IReadOnlyList<AvaloniaXamlIlXmlNamespaceInfo>>(3)
			{
				{
					"",
					new AvaloniaXamlIlXmlNamespaceInfo[37]
					{
						CreateNamespaceInfo("Avalonia", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Animation", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Animation.Easings", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Controls", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Data", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Data.Converters", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Input", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Input.GestureRecognizers", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Input.TextInput", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Layout", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.LogicalTree", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Media", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Media.Imaging", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Media.Transformation", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Styling", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Controls", "Avalonia.Controls.DataGrid"),
						CreateNamespaceInfo("Avalonia.Controls.Collections", "Avalonia.Controls.DataGrid"),
						CreateNamespaceInfo("Avalonia.Controls.Primitives", "Avalonia.Controls.DataGrid"),
						CreateNamespaceInfo("Avalonia", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Automation", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Embedding", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Presenters", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Primitives", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Shapes", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Templates", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Notifications", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Chrome", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Documents", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Fonts.Inter", "Avalonia.Fonts.Inter"),
						CreateNamespaceInfo("Avalonia.Data", "Avalonia.Markup"),
						CreateNamespaceInfo("Avalonia.Markup.Data", "Avalonia.Markup"),
						CreateNamespaceInfo("Avalonia.Markup.Xaml.MarkupExtensions", "Avalonia.Markup.Xaml"),
						CreateNamespaceInfo("Avalonia.Markup.Xaml.Styling", "Avalonia.Markup.Xaml"),
						CreateNamespaceInfo("Avalonia.Markup.Xaml.Templates", "Avalonia.Markup.Xaml"),
						CreateNamespaceInfo("Avalonia.Themes.Fluent", "Avalonia.Themes.Fluent"),
						CreateNamespaceInfo("Avalonia.Svg.Skia", "Svg.Controls.Skia.Avalonia")
					}
				},
				{
					"x",
					new AvaloniaXamlIlXmlNamespaceInfo[0]
				},
				{
					"c",
					new AvaloniaXamlIlXmlNamespaceInfo[1] { CreateNamespaceInfo("SpaceWalker.Controls", null) }
				}
			};
		}

		static NamespaceInfo_003A_002FControls_002FIconLabel_002Eaxaml()
		{
			Singleton = new NamespaceInfo_003A_002FControls_002FIconLabel_002Eaxaml();
		}
	}

	[CompilerGenerated]
	private class XamlClosure_5
	{
		public unsafe static object Build_1(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<Styles> provider = CreateContext(P_0);
			ControlTheme controlTheme = new ControlTheme();
			controlTheme.TargetType = typeof(IconLabel);
			Setter setter = new Setter();
			setter.Property = TemplatedControl.BackgroundProperty;
			setter.Value = new ImmutableSolidColorBrush(16777215u);
			controlTheme.Add(setter);
			Setter setter2 = new Setter();
			setter2.Property = TemplatedControl.PaddingProperty;
			setter2.Value = new Thickness(0.0, 0.0, 0.0, 0.0);
			controlTheme.Add(setter2);
			Setter setter3 = new Setter();
			setter3.Property = Layoutable.VerticalAlignmentProperty;
			setter3.Value = VerticalAlignment.Center;
			controlTheme.Add(setter3);
			Setter setter4 = new Setter();
			setter4.Property = TemplatedControl.TemplateProperty;
			setter4.Value = new ControlTemplate
			{
				Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&Build_2), provider)
			};
			controlTheme.Add(setter4);
			return controlTheme;
		}

		public static CompiledAvaloniaXaml.XamlIlContext.Context<Styles> CreateContext(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<Styles> context = new CompiledAvaloniaXaml.XamlIlContext.Context<Styles>(P_0, new object[1] { NamespaceInfo_003A_002FControls_002FIconLabel_002Eaxaml.Singleton }, "avares://SpaceWalker/Controls/IconLabel.axaml");
			if (P_0 != null)
			{
				object service = P_0.GetService(typeof(IRootObjectProvider));
				if (service != null)
				{
					service = ((IRootObjectProvider)service).RootObject;
					context.RootObject = (Styles)service;
				}
			}
			return context;
		}

		public static object Build_2(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<Styles> context = CreateContext(P_0);
			context.IntermediateRoot = new StackPanel();
			object intermediateRoot = context.IntermediateRoot;
			((ISupportInitialize)intermediateRoot).BeginInit();
			((AvaloniaObject)intermediateRoot).SetValue(StackPanel.OrientationProperty, Orientation.Horizontal, BindingPriority.Template);
			((AvaloniaObject)intermediateRoot).Bind((AvaloniaProperty)StackPanel.SpacingProperty, new TemplateBinding(IconLabel.SpacingProperty).ProvideValue());
			((AvaloniaObject)intermediateRoot).SetValue(Layoutable.VerticalAlignmentProperty, VerticalAlignment.Center, BindingPriority.Template);
			Controls children = ((Panel)intermediateRoot).Children;
			ContentPresenter contentPresenter;
			ContentPresenter contentPresenter2 = (contentPresenter = new ContentPresenter());
			((ISupportInitialize)contentPresenter2).BeginInit();
			children.Add(contentPresenter2);
			contentPresenter.Name = "PART_IconPresenter";
			object element = contentPresenter;
			context.AvaloniaNameScope.Register("PART_IconPresenter", element);
			CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(contentPresenter, BindingPriority.Template, new TemplateBinding(IconLabel.IconProperty).ProvideValue());
			contentPresenter.SetValue(Layoutable.VerticalAlignmentProperty, VerticalAlignment.Center, BindingPriority.Template);
			contentPresenter.Bind(Visual.IsVisibleProperty, new TemplateBinding(IconLabel.IconProperty)
			{
				Converter = ObjectConverters.IsNotNull
			}.ProvideValue());
			((ISupportInitialize)contentPresenter).EndInit();
			Controls children2 = ((Panel)intermediateRoot).Children;
			ContentPresenter contentPresenter3;
			ContentPresenter contentPresenter4 = (contentPresenter3 = new ContentPresenter());
			((ISupportInitialize)contentPresenter4).BeginInit();
			children2.Add(contentPresenter4);
			contentPresenter3.Name = "PART_ContentPresenter";
			element = contentPresenter3;
			context.AvaloniaNameScope.Register("PART_ContentPresenter", element);
			CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(contentPresenter3, BindingPriority.Template, new TemplateBinding(ContentControl.ContentProperty).ProvideValue());
			contentPresenter3.Bind(ContentPresenter.ContentTemplateProperty, new TemplateBinding(ContentControl.ContentTemplateProperty).ProvideValue());
			contentPresenter3.Bind(ContentPresenter.ForegroundProperty, new TemplateBinding(TemplatedControl.ForegroundProperty).ProvideValue());
			contentPresenter3.SetValue(Layoutable.VerticalAlignmentProperty, VerticalAlignment.Center, BindingPriority.Template);
			((ISupportInitialize)contentPresenter3).EndInit();
			((ISupportInitialize)intermediateRoot).EndInit();
			return intermediateRoot;
		}
	}

	[CompilerGenerated]
	internal class NamespaceInfo_003A_002FControls_002FListBox_002Eaxaml : IAvaloniaXamlIlXmlNamespaceInfoProvider
	{
		private IReadOnlyDictionary<string, IReadOnlyList<AvaloniaXamlIlXmlNamespaceInfo>> _xmlNamespaces;

		public static IAvaloniaXamlIlXmlNamespaceInfoProvider Singleton;

		public virtual IReadOnlyDictionary<string, IReadOnlyList<AvaloniaXamlIlXmlNamespaceInfo>> XmlNamespaces
		{
			get
			{
				if (_xmlNamespaces == null)
				{
					_xmlNamespaces = CreateNamespaces();
				}
				return _xmlNamespaces;
			}
		}

		private static AvaloniaXamlIlXmlNamespaceInfo CreateNamespaceInfo(string P_0, string P_1)
		{
			return new AvaloniaXamlIlXmlNamespaceInfo
			{
				ClrNamespace = P_0,
				ClrAssemblyName = P_1
			};
		}

		private static IReadOnlyDictionary<string, IReadOnlyList<AvaloniaXamlIlXmlNamespaceInfo>> CreateNamespaces()
		{
			return new Dictionary<string, IReadOnlyList<AvaloniaXamlIlXmlNamespaceInfo>>(2)
			{
				{
					"",
					new AvaloniaXamlIlXmlNamespaceInfo[37]
					{
						CreateNamespaceInfo("Avalonia", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Animation", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Animation.Easings", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Controls", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Data", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Data.Converters", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Input", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Input.GestureRecognizers", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Input.TextInput", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Layout", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.LogicalTree", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Media", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Media.Imaging", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Media.Transformation", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Styling", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Controls", "Avalonia.Controls.DataGrid"),
						CreateNamespaceInfo("Avalonia.Controls.Collections", "Avalonia.Controls.DataGrid"),
						CreateNamespaceInfo("Avalonia.Controls.Primitives", "Avalonia.Controls.DataGrid"),
						CreateNamespaceInfo("Avalonia", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Automation", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Embedding", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Presenters", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Primitives", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Shapes", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Templates", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Notifications", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Chrome", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Documents", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Fonts.Inter", "Avalonia.Fonts.Inter"),
						CreateNamespaceInfo("Avalonia.Data", "Avalonia.Markup"),
						CreateNamespaceInfo("Avalonia.Markup.Data", "Avalonia.Markup"),
						CreateNamespaceInfo("Avalonia.Markup.Xaml.MarkupExtensions", "Avalonia.Markup.Xaml"),
						CreateNamespaceInfo("Avalonia.Markup.Xaml.Styling", "Avalonia.Markup.Xaml"),
						CreateNamespaceInfo("Avalonia.Markup.Xaml.Templates", "Avalonia.Markup.Xaml"),
						CreateNamespaceInfo("Avalonia.Themes.Fluent", "Avalonia.Themes.Fluent"),
						CreateNamespaceInfo("Avalonia.Svg.Skia", "Svg.Controls.Skia.Avalonia")
					}
				},
				{
					"x",
					new AvaloniaXamlIlXmlNamespaceInfo[0]
				}
			};
		}

		static NamespaceInfo_003A_002FControls_002FListBox_002Eaxaml()
		{
			Singleton = new NamespaceInfo_003A_002FControls_002FListBox_002Eaxaml();
		}
	}

	[CompilerGenerated]
	private class XamlClosure_6
	{
		public static object Build_1(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<Styles> context = CreateContext(P_0);
			context.IntermediateRoot = new Border();
			object intermediateRoot = context.IntermediateRoot;
			((ISupportInitialize)intermediateRoot).BeginInit();
			((AvaloniaObject)intermediateRoot).SetValue(Layoutable.MarginProperty, new Thickness(4.0, 4.0, 4.0, 4.0), BindingPriority.Template);
			Border border;
			Border border2 = (border = new Border());
			((ISupportInitialize)border2).BeginInit();
			((Decorator)intermediateRoot).Child = border2;
			border.Name = "CardBorder";
			object element = border;
			context.AvaloniaNameScope.Register("CardBorder", element);
			border.SetValue(Border.BackgroundProperty, new ImmutableSolidColorBrush(4279900182u), BindingPriority.Template);
			border.SetValue(Border.CornerRadiusProperty, new CornerRadius(4.0, 4.0, 4.0, 4.0), BindingPriority.Template);
			border.SetValue(Border.BorderBrushProperty, new ImmutableSolidColorBrush(16777215u), BindingPriority.Template);
			border.SetValue(Border.BorderThicknessProperty, new Thickness(2.0, 2.0, 2.0, 2.0), BindingPriority.Template);
			border.SetValue(Visual.ClipToBoundsProperty, value: true, BindingPriority.Template);
			StyledProperty<Transitions?> transitionsProperty = Animatable.TransitionsProperty;
			Transitions transitions = new Transitions();
			BrushTransition brushTransition = new BrushTransition();
			brushTransition.Property = Border.BorderBrushProperty;
			brushTransition.Duration = TimeSpan.FromTicks(1500000L);
			transitions.Add(brushTransition);
			border.SetValue(transitionsProperty, transitions, BindingPriority.Template);
			Border border3;
			Border border4 = (border3 = new Border());
			((ISupportInitialize)border4).BeginInit();
			border.Child = border4;
			border3.Name = "HoverOverlay";
			element = border3;
			context.AvaloniaNameScope.Register("HoverOverlay", element);
			border3.SetValue(Border.BackgroundProperty, new ImmutableSolidColorBrush(234881023u), BindingPriority.Template);
			StyledProperty<Transitions?> transitionsProperty2 = Animatable.TransitionsProperty;
			Transitions transitions2 = new Transitions();
			BrushTransition brushTransition2 = new BrushTransition();
			brushTransition2.Property = Border.BackgroundProperty;
			brushTransition2.Duration = TimeSpan.FromTicks(1000000L);
			transitions2.Add(brushTransition2);
			border3.SetValue(transitionsProperty2, transitions2, BindingPriority.Template);
			ContentPresenter contentPresenter;
			ContentPresenter contentPresenter2 = (contentPresenter = new ContentPresenter());
			((ISupportInitialize)contentPresenter2).BeginInit();
			border3.Child = contentPresenter2;
			contentPresenter.Name = "PART_ContentPresenter";
			element = contentPresenter;
			context.AvaloniaNameScope.Register("PART_ContentPresenter", element);
			CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(contentPresenter, BindingPriority.Template, new TemplateBinding(ContentControl.ContentProperty).ProvideValue());
			contentPresenter.Bind(ContentPresenter.ContentTemplateProperty, new TemplateBinding(ContentControl.ContentTemplateProperty).ProvideValue());
			contentPresenter.Bind(Layoutable.HorizontalAlignmentProperty, new TemplateBinding(ContentControl.HorizontalContentAlignmentProperty).ProvideValue());
			contentPresenter.Bind(Layoutable.VerticalAlignmentProperty, new TemplateBinding(ContentControl.VerticalContentAlignmentProperty).ProvideValue());
			((ISupportInitialize)contentPresenter).EndInit();
			((ISupportInitialize)border3).EndInit();
			((ISupportInitialize)border).EndInit();
			((ISupportInitialize)intermediateRoot).EndInit();
			return intermediateRoot;
		}

		public static CompiledAvaloniaXaml.XamlIlContext.Context<Styles> CreateContext(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<Styles> context = new CompiledAvaloniaXaml.XamlIlContext.Context<Styles>(P_0, new object[1] { NamespaceInfo_003A_002FControls_002FListBox_002Eaxaml.Singleton }, "avares://SpaceWalker/Controls/ListBox.axaml");
			if (P_0 != null)
			{
				object service = P_0.GetService(typeof(IRootObjectProvider));
				if (service != null)
				{
					service = ((IRootObjectProvider)service).RootObject;
					context.RootObject = (Styles)service;
				}
			}
			return context;
		}
	}

	[CompilerGenerated]
	internal class NamespaceInfo_003A_002FControls_002FRadioButton_002Eaxaml : IAvaloniaXamlIlXmlNamespaceInfoProvider
	{
		private IReadOnlyDictionary<string, IReadOnlyList<AvaloniaXamlIlXmlNamespaceInfo>> _xmlNamespaces;

		public static IAvaloniaXamlIlXmlNamespaceInfoProvider Singleton;

		public virtual IReadOnlyDictionary<string, IReadOnlyList<AvaloniaXamlIlXmlNamespaceInfo>> XmlNamespaces
		{
			get
			{
				if (_xmlNamespaces == null)
				{
					_xmlNamespaces = CreateNamespaces();
				}
				return _xmlNamespaces;
			}
		}

		private static AvaloniaXamlIlXmlNamespaceInfo CreateNamespaceInfo(string P_0, string P_1)
		{
			return new AvaloniaXamlIlXmlNamespaceInfo
			{
				ClrNamespace = P_0,
				ClrAssemblyName = P_1
			};
		}

		private static IReadOnlyDictionary<string, IReadOnlyList<AvaloniaXamlIlXmlNamespaceInfo>> CreateNamespaces()
		{
			return new Dictionary<string, IReadOnlyList<AvaloniaXamlIlXmlNamespaceInfo>>(2)
			{
				{
					"",
					new AvaloniaXamlIlXmlNamespaceInfo[37]
					{
						CreateNamespaceInfo("Avalonia", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Animation", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Animation.Easings", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Controls", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Data", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Data.Converters", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Input", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Input.GestureRecognizers", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Input.TextInput", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Layout", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.LogicalTree", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Media", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Media.Imaging", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Media.Transformation", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Styling", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Controls", "Avalonia.Controls.DataGrid"),
						CreateNamespaceInfo("Avalonia.Controls.Collections", "Avalonia.Controls.DataGrid"),
						CreateNamespaceInfo("Avalonia.Controls.Primitives", "Avalonia.Controls.DataGrid"),
						CreateNamespaceInfo("Avalonia", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Automation", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Embedding", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Presenters", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Primitives", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Shapes", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Templates", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Notifications", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Chrome", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Documents", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Fonts.Inter", "Avalonia.Fonts.Inter"),
						CreateNamespaceInfo("Avalonia.Data", "Avalonia.Markup"),
						CreateNamespaceInfo("Avalonia.Markup.Data", "Avalonia.Markup"),
						CreateNamespaceInfo("Avalonia.Markup.Xaml.MarkupExtensions", "Avalonia.Markup.Xaml"),
						CreateNamespaceInfo("Avalonia.Markup.Xaml.Styling", "Avalonia.Markup.Xaml"),
						CreateNamespaceInfo("Avalonia.Markup.Xaml.Templates", "Avalonia.Markup.Xaml"),
						CreateNamespaceInfo("Avalonia.Themes.Fluent", "Avalonia.Themes.Fluent"),
						CreateNamespaceInfo("Avalonia.Svg.Skia", "Svg.Controls.Skia.Avalonia")
					}
				},
				{
					"x",
					new AvaloniaXamlIlXmlNamespaceInfo[0]
				}
			};
		}

		static NamespaceInfo_003A_002FControls_002FRadioButton_002Eaxaml()
		{
			Singleton = new NamespaceInfo_003A_002FControls_002FRadioButton_002Eaxaml();
		}
	}

	[CompilerGenerated]
	private class XamlClosure_7
	{
		public static object Build_1(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<Styles> context = CreateContext(P_0);
			context.IntermediateRoot = new Grid();
			object intermediateRoot = context.IntermediateRoot;
			((ISupportInitialize)intermediateRoot).BeginInit();
			Grid grid = (Grid)intermediateRoot;
			context.PushParent(grid);
			ColumnDefinitions columnDefinitions = new ColumnDefinitions();
			columnDefinitions.Capacity = 2;
			columnDefinitions.Add(new ColumnDefinition(new GridLength(0.0, GridUnitType.Auto)));
			columnDefinitions.Add(new ColumnDefinition(new GridLength(1.0, GridUnitType.Star)));
			grid.ColumnDefinitions = columnDefinitions;
			grid.SetValue(Layoutable.VerticalAlignmentProperty, VerticalAlignment.Center, BindingPriority.Template);
			Controls children = grid.Children;
			Panel panel;
			Panel panel2 = (panel = new Panel());
			((ISupportInitialize)panel2).BeginInit();
			children.Add(panel2);
			Panel panel3;
			Panel panel4 = (panel3 = panel);
			context.PushParent(panel3);
			panel3.SetValue(Grid.ColumnProperty, 0, BindingPriority.Template);
			panel3.SetValue(Layoutable.UseLayoutRoundingProperty, value: false, BindingPriority.Template);
			panel3.SetValue(Layoutable.HorizontalAlignmentProperty, HorizontalAlignment.Center, BindingPriority.Template);
			panel3.SetValue(Layoutable.VerticalAlignmentProperty, VerticalAlignment.Center, BindingPriority.Template);
			Controls children2 = panel3.Children;
			Border border;
			Border border2 = (border = new Border());
			((ISupportInitialize)border2).BeginInit();
			children2.Add(border2);
			border.SetValue(Layoutable.WidthProperty, 16.0, BindingPriority.Template);
			border.SetValue(Layoutable.HeightProperty, 16.0, BindingPriority.Template);
			border.Name = "OuterRing";
			object element = border;
			context.AvaloniaNameScope.Register("OuterRing", element);
			border.SetValue(Border.CornerRadiusProperty, new CornerRadius(8.0, 8.0, 8.0, 8.0), BindingPriority.Template);
			border.SetValue(Border.BackgroundProperty, new ImmutableSolidColorBrush(436207616u), BindingPriority.Template);
			border.SetValue(Border.BorderBrushProperty, new ImmutableSolidColorBrush(2063597567u), BindingPriority.Template);
			border.SetValue(Border.BorderThicknessProperty, new Thickness(1.5, 1.5, 1.5, 1.5), BindingPriority.Template);
			border.SetValue(Layoutable.HorizontalAlignmentProperty, HorizontalAlignment.Center, BindingPriority.Template);
			border.SetValue(Layoutable.VerticalAlignmentProperty, VerticalAlignment.Center, BindingPriority.Template);
			StyledProperty<Transitions?> transitionsProperty = Animatable.TransitionsProperty;
			Transitions transitions = new Transitions();
			BrushTransition brushTransition = new BrushTransition();
			brushTransition.Property = Border.BorderBrushProperty;
			brushTransition.Duration = TimeSpan.FromTicks(1500000L);
			transitions.Add(brushTransition);
			border.SetValue(transitionsProperty, transitions, BindingPriority.Template);
			((ISupportInitialize)border).EndInit();
			Controls children3 = panel3.Children;
			Ellipse ellipse;
			Ellipse ellipse2 = (ellipse = new Ellipse());
			((ISupportInitialize)ellipse2).BeginInit();
			children3.Add(ellipse2);
			Ellipse ellipse3;
			Ellipse ellipse4 = (ellipse3 = ellipse);
			context.PushParent(ellipse3);
			ellipse3.SetValue(Layoutable.WidthProperty, 8.41, BindingPriority.Template);
			ellipse3.SetValue(Layoutable.HeightProperty, 8.41, BindingPriority.Template);
			ellipse3.Name = "InnerDot";
			element = ellipse3;
			context.AvaloniaNameScope.Register("InnerDot", element);
			StyledProperty<IBrush?> fillProperty = Shape.FillProperty;
			DynamicResourceExtension dynamicResourceExtension = new DynamicResourceExtension("SystemAccentColorBrush");
			context.ProvideTargetProperty = Shape.FillProperty;
			BindingBase binding = dynamicResourceExtension.ProvideValue(context);
			context.ProvideTargetProperty = null;
			ellipse3.Bind(fillProperty, binding);
			ellipse3.SetValue(Visual.OpacityProperty, 0.0, BindingPriority.Template);
			ellipse3.SetValue(Layoutable.HorizontalAlignmentProperty, HorizontalAlignment.Center, BindingPriority.Template);
			ellipse3.SetValue(Layoutable.VerticalAlignmentProperty, VerticalAlignment.Center, BindingPriority.Template);
			StyledProperty<Transitions?> transitionsProperty2 = Animatable.TransitionsProperty;
			Transitions transitions2 = new Transitions();
			DoubleTransition doubleTransition = new DoubleTransition();
			doubleTransition.Property = Visual.OpacityProperty;
			doubleTransition.Duration = TimeSpan.FromTicks(1500000L);
			transitions2.Add(doubleTransition);
			ellipse3.SetValue(transitionsProperty2, transitions2, BindingPriority.Template);
			context.PopParent();
			((ISupportInitialize)ellipse4).EndInit();
			context.PopParent();
			((ISupportInitialize)panel4).EndInit();
			Controls children4 = grid.Children;
			ContentPresenter contentPresenter;
			ContentPresenter contentPresenter2 = (contentPresenter = new ContentPresenter());
			((ISupportInitialize)contentPresenter2).BeginInit();
			children4.Add(contentPresenter2);
			contentPresenter.Name = "PART_ContentPresenter";
			element = contentPresenter;
			context.AvaloniaNameScope.Register("PART_ContentPresenter", element);
			contentPresenter.SetValue(Grid.ColumnProperty, 1, BindingPriority.Template);
			contentPresenter.SetValue(Layoutable.MarginProperty, new Thickness(8.0, 0.0, 0.0, 0.0), BindingPriority.Template);
			contentPresenter.SetValue(Layoutable.VerticalAlignmentProperty, VerticalAlignment.Center, BindingPriority.Template);
			CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(contentPresenter, BindingPriority.Template, new TemplateBinding(ContentControl.ContentProperty).ProvideValue());
			contentPresenter.Bind(ContentPresenter.ContentTemplateProperty, new TemplateBinding(ContentControl.ContentTemplateProperty).ProvideValue());
			contentPresenter.Bind(ContentPresenter.FontSizeProperty, new TemplateBinding(TemplatedControl.FontSizeProperty).ProvideValue());
			contentPresenter.SetValue(ContentPresenter.RecognizesAccessKeyProperty, value: true, BindingPriority.Template);
			((ISupportInitialize)contentPresenter).EndInit();
			context.PopParent();
			((ISupportInitialize)intermediateRoot).EndInit();
			return intermediateRoot;
		}

		public static CompiledAvaloniaXaml.XamlIlContext.Context<Styles> CreateContext(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<Styles> context = new CompiledAvaloniaXaml.XamlIlContext.Context<Styles>(P_0, new object[1] { NamespaceInfo_003A_002FControls_002FRadioButton_002Eaxaml.Singleton }, "avares://SpaceWalker/Controls/RadioButton.axaml");
			if (P_0 != null)
			{
				object service = P_0.GetService(typeof(IRootObjectProvider));
				if (service != null)
				{
					service = ((IRootObjectProvider)service).RootObject;
					context.RootObject = (Styles)service;
				}
			}
			return context;
		}
	}

	[CompilerGenerated]
	internal class NamespaceInfo_003A_002FControls_002FScrollViewer_002Eaxaml : IAvaloniaXamlIlXmlNamespaceInfoProvider
	{
		private IReadOnlyDictionary<string, IReadOnlyList<AvaloniaXamlIlXmlNamespaceInfo>> _xmlNamespaces;

		public static IAvaloniaXamlIlXmlNamespaceInfoProvider Singleton;

		public virtual IReadOnlyDictionary<string, IReadOnlyList<AvaloniaXamlIlXmlNamespaceInfo>> XmlNamespaces
		{
			get
			{
				if (_xmlNamespaces == null)
				{
					_xmlNamespaces = CreateNamespaces();
				}
				return _xmlNamespaces;
			}
		}

		private static AvaloniaXamlIlXmlNamespaceInfo CreateNamespaceInfo(string P_0, string P_1)
		{
			return new AvaloniaXamlIlXmlNamespaceInfo
			{
				ClrNamespace = P_0,
				ClrAssemblyName = P_1
			};
		}

		private static IReadOnlyDictionary<string, IReadOnlyList<AvaloniaXamlIlXmlNamespaceInfo>> CreateNamespaces()
		{
			return new Dictionary<string, IReadOnlyList<AvaloniaXamlIlXmlNamespaceInfo>>(2)
			{
				{
					"",
					new AvaloniaXamlIlXmlNamespaceInfo[37]
					{
						CreateNamespaceInfo("Avalonia", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Animation", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Animation.Easings", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Controls", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Data", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Data.Converters", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Input", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Input.GestureRecognizers", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Input.TextInput", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Layout", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.LogicalTree", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Media", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Media.Imaging", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Media.Transformation", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Styling", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Controls", "Avalonia.Controls.DataGrid"),
						CreateNamespaceInfo("Avalonia.Controls.Collections", "Avalonia.Controls.DataGrid"),
						CreateNamespaceInfo("Avalonia.Controls.Primitives", "Avalonia.Controls.DataGrid"),
						CreateNamespaceInfo("Avalonia", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Automation", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Embedding", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Presenters", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Primitives", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Shapes", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Templates", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Notifications", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Chrome", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Documents", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Fonts.Inter", "Avalonia.Fonts.Inter"),
						CreateNamespaceInfo("Avalonia.Data", "Avalonia.Markup"),
						CreateNamespaceInfo("Avalonia.Markup.Data", "Avalonia.Markup"),
						CreateNamespaceInfo("Avalonia.Markup.Xaml.MarkupExtensions", "Avalonia.Markup.Xaml"),
						CreateNamespaceInfo("Avalonia.Markup.Xaml.Styling", "Avalonia.Markup.Xaml"),
						CreateNamespaceInfo("Avalonia.Markup.Xaml.Templates", "Avalonia.Markup.Xaml"),
						CreateNamespaceInfo("Avalonia.Themes.Fluent", "Avalonia.Themes.Fluent"),
						CreateNamespaceInfo("Avalonia.Svg.Skia", "Svg.Controls.Skia.Avalonia")
					}
				},
				{
					"x",
					new AvaloniaXamlIlXmlNamespaceInfo[0]
				}
			};
		}

		static NamespaceInfo_003A_002FControls_002FScrollViewer_002Eaxaml()
		{
			Singleton = new NamespaceInfo_003A_002FControls_002FScrollViewer_002Eaxaml();
		}
	}

	[CompilerGenerated]
	private class XamlClosure_8
	{
		public static object Build_1(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<Styles> context = CreateContext(P_0);
			context.IntermediateRoot = new Grid();
			object intermediateRoot = context.IntermediateRoot;
			((ISupportInitialize)intermediateRoot).BeginInit();
			ColumnDefinitions columnDefinitions = new ColumnDefinitions();
			columnDefinitions.Capacity = 3;
			columnDefinitions.Add(new ColumnDefinition(new GridLength(1.0, GridUnitType.Star)));
			columnDefinitions.Add(new ColumnDefinition(new GridLength(6.0, GridUnitType.Pixel)));
			columnDefinitions.Add(new ColumnDefinition(new GridLength(0.0, GridUnitType.Auto)));
			((Grid)intermediateRoot).ColumnDefinitions = columnDefinitions;
			Controls children = ((Panel)intermediateRoot).Children;
			ScrollContentPresenter scrollContentPresenter;
			ScrollContentPresenter scrollContentPresenter2 = (scrollContentPresenter = new ScrollContentPresenter());
			((ISupportInitialize)scrollContentPresenter2).BeginInit();
			children.Add(scrollContentPresenter2);
			scrollContentPresenter.Name = "PART_ContentPresenter";
			object element = scrollContentPresenter;
			context.AvaloniaNameScope.Register("PART_ContentPresenter", element);
			scrollContentPresenter.SetValue(Grid.ColumnProperty, 0, BindingPriority.Template);
			CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(scrollContentPresenter, BindingPriority.Template, new TemplateBinding(ContentControl.ContentProperty).ProvideValue());
			scrollContentPresenter.Bind(ContentPresenter.ContentTemplateProperty, new TemplateBinding(ContentControl.ContentTemplateProperty).ProvideValue());
			scrollContentPresenter.Bind(ContentPresenter.PaddingProperty, new TemplateBinding(TemplatedControl.PaddingProperty).ProvideValue());
			scrollContentPresenter.Bind(ScrollContentPresenter.IsScrollChainingEnabledProperty, new TemplateBinding(ScrollViewer.IsScrollChainingEnabledProperty).ProvideValue());
			((ISupportInitialize)scrollContentPresenter).EndInit();
			Controls children2 = ((Panel)intermediateRoot).Children;
			ScrollBar scrollBar;
			ScrollBar scrollBar2 = (scrollBar = new ScrollBar());
			((ISupportInitialize)scrollBar2).BeginInit();
			children2.Add(scrollBar2);
			scrollBar.Name = "PART_VerticalScrollBar";
			element = scrollBar;
			context.AvaloniaNameScope.Register("PART_VerticalScrollBar", element);
			scrollBar.SetValue(Grid.ColumnProperty, 2, BindingPriority.Template);
			scrollBar.SetValue(ScrollBar.OrientationProperty, Orientation.Vertical, BindingPriority.Template);
			scrollBar.SetValue(ScrollBar.AllowAutoHideProperty, value: false, BindingPriority.Template);
			((ISupportInitialize)scrollBar).EndInit();
			((ISupportInitialize)intermediateRoot).EndInit();
			return intermediateRoot;
		}

		public static CompiledAvaloniaXaml.XamlIlContext.Context<Styles> CreateContext(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<Styles> context = new CompiledAvaloniaXaml.XamlIlContext.Context<Styles>(P_0, new object[1] { NamespaceInfo_003A_002FControls_002FScrollViewer_002Eaxaml.Singleton }, "avares://SpaceWalker/Controls/ScrollViewer.axaml");
			if (P_0 != null)
			{
				object service = P_0.GetService(typeof(IRootObjectProvider));
				if (service != null)
				{
					service = ((IRootObjectProvider)service).RootObject;
					context.RootObject = (Styles)service;
				}
			}
			return context;
		}

		public unsafe static object Build_2(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<Styles> context = CreateContext(P_0);
			context.IntermediateRoot = new Grid();
			object intermediateRoot = context.IntermediateRoot;
			((ISupportInitialize)intermediateRoot).BeginInit();
			((AvaloniaObject)intermediateRoot).SetValue(Layoutable.WidthProperty, 14.0, BindingPriority.Template);
			((AvaloniaObject)intermediateRoot).SetValue(Layoutable.VerticalAlignmentProperty, VerticalAlignment.Stretch, BindingPriority.Template);
			Controls children = ((Panel)intermediateRoot).Children;
			Border border;
			Border border2 = (border = new Border());
			((ISupportInitialize)border2).BeginInit();
			children.Add(border2);
			border.SetValue(Layoutable.WidthProperty, 10.0, BindingPriority.Template);
			border.SetValue(Layoutable.HorizontalAlignmentProperty, HorizontalAlignment.Center, BindingPriority.Template);
			border.SetValue(Layoutable.VerticalAlignmentProperty, VerticalAlignment.Stretch, BindingPriority.Template);
			border.SetValue(Border.CornerRadiusProperty, new CornerRadius(5.0, 5.0, 5.0, 5.0), BindingPriority.Template);
			border.SetValue(Border.BackgroundProperty, new ImmutableSolidColorBrush(872415231u), BindingPriority.Template);
			((ISupportInitialize)border).EndInit();
			Controls children2 = ((Panel)intermediateRoot).Children;
			Track track;
			Track track2 = (track = new Track());
			((ISupportInitialize)track2).BeginInit();
			children2.Add(track2);
			track.Name = "PART_Track";
			object element = track;
			context.AvaloniaNameScope.Register("PART_Track", element);
			track.Bind(Track.MinimumProperty, new TemplateBinding(RangeBase.MinimumProperty).ProvideValue());
			track.Bind(Track.MaximumProperty, new TemplateBinding(RangeBase.MaximumProperty).ProvideValue());
			track.Bind(Track.ValueProperty, new TemplateBinding(RangeBase.ValueProperty)
			{
				Mode = BindingMode.TwoWay
			}.ProvideValue());
			track.Bind(Track.ViewportSizeProperty, new TemplateBinding(ScrollBar.ViewportSizeProperty).ProvideValue());
			track.SetValue(Track.OrientationProperty, Orientation.Vertical, BindingPriority.Template);
			track.SetValue(Track.IsDirectionReversedProperty, value: true, BindingPriority.Template);
			track.SetValue(Layoutable.HorizontalAlignmentProperty, HorizontalAlignment.Center, BindingPriority.Template);
			track.SetValue(Layoutable.WidthProperty, 6.0, BindingPriority.Template);
			track.SetValue(Layoutable.MarginProperty, new Thickness(0.0, 4.0, 0.0, 4.0), BindingPriority.Template);
			StyledProperty<Button?> decreaseButtonProperty = Track.DecreaseButtonProperty;
			RepeatButton repeatButton;
			RepeatButton repeatButton2 = (repeatButton = new RepeatButton());
			((ISupportInitialize)repeatButton2).BeginInit();
			track.SetValue(decreaseButtonProperty, repeatButton2, BindingPriority.Template);
			repeatButton.Name = "PART_PageUpButton";
			element = repeatButton;
			context.AvaloniaNameScope.Register("PART_PageUpButton", element);
			repeatButton.SetValue(InputElement.FocusableProperty, value: false, BindingPriority.Template);
			repeatButton.SetValue(TemplatedControl.TemplateProperty, new ControlTemplate
			{
				Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&Build_3), context)
			}, BindingPriority.Template);
			((ISupportInitialize)repeatButton).EndInit();
			StyledProperty<Button?> increaseButtonProperty = Track.IncreaseButtonProperty;
			RepeatButton repeatButton3;
			RepeatButton repeatButton4 = (repeatButton3 = new RepeatButton());
			((ISupportInitialize)repeatButton4).BeginInit();
			track.SetValue(increaseButtonProperty, repeatButton4, BindingPriority.Template);
			repeatButton3.Name = "PART_PageDownButton";
			element = repeatButton3;
			context.AvaloniaNameScope.Register("PART_PageDownButton", element);
			repeatButton3.SetValue(InputElement.FocusableProperty, value: false, BindingPriority.Template);
			repeatButton3.SetValue(TemplatedControl.TemplateProperty, new ControlTemplate
			{
				Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&Build_4), context)
			}, BindingPriority.Template);
			((ISupportInitialize)repeatButton3).EndInit();
			Thumb thumb;
			Thumb thumb2 = (thumb = new Thumb());
			((ISupportInitialize)thumb2).BeginInit();
			track.Thumb = thumb2;
			thumb.Name = "PART_Thumb";
			element = thumb;
			context.AvaloniaNameScope.Register("PART_Thumb", element);
			thumb.SetValue(TemplatedControl.TemplateProperty, new ControlTemplate
			{
				Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&Build_5), context)
			}, BindingPriority.Template);
			((ISupportInitialize)thumb).EndInit();
			((ISupportInitialize)track).EndInit();
			((ISupportInitialize)intermediateRoot).EndInit();
			return intermediateRoot;
		}

		public static object Build_3(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<Styles> context = CreateContext(P_0);
			context.IntermediateRoot = new Border();
			object intermediateRoot = context.IntermediateRoot;
			((ISupportInitialize)intermediateRoot).BeginInit();
			((AvaloniaObject)intermediateRoot).SetValue(Border.BackgroundProperty, (IBrush)new ImmutableSolidColorBrush(16777215u), BindingPriority.Template);
			((ISupportInitialize)intermediateRoot).EndInit();
			return intermediateRoot;
		}

		public static object Build_4(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<Styles> context = CreateContext(P_0);
			context.IntermediateRoot = new Border();
			object intermediateRoot = context.IntermediateRoot;
			((ISupportInitialize)intermediateRoot).BeginInit();
			((AvaloniaObject)intermediateRoot).SetValue(Border.BackgroundProperty, (IBrush)new ImmutableSolidColorBrush(16777215u), BindingPriority.Template);
			((ISupportInitialize)intermediateRoot).EndInit();
			return intermediateRoot;
		}

		public static object Build_5(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<Styles> context = CreateContext(P_0);
			context.IntermediateRoot = new Border();
			object intermediateRoot = context.IntermediateRoot;
			((ISupportInitialize)intermediateRoot).BeginInit();
			((AvaloniaObject)intermediateRoot).SetValue(Layoutable.MinHeightProperty, 29.0, BindingPriority.Template);
			((AvaloniaObject)intermediateRoot).SetValue(Border.CornerRadiusProperty, new CornerRadius(3.0, 3.0, 3.0, 3.0), BindingPriority.Template);
			((AvaloniaObject)intermediateRoot).SetValue(Border.BackgroundProperty, (IBrush)new ImmutableSolidColorBrush(uint.MaxValue), BindingPriority.Template);
			StyledProperty<IEffect?> effectProperty = Visual.EffectProperty;
			DropShadowEffect dropShadowEffect = new DropShadowEffect();
			dropShadowEffect.SetValue(DropShadowEffect.OffsetXProperty, 0.0, BindingPriority.Template);
			dropShadowEffect.SetValue(DropShadowEffect.OffsetYProperty, 4.0, BindingPriority.Template);
			dropShadowEffect.SetValue(DropShadowEffectBase.BlurRadiusProperty, 4.0, BindingPriority.Template);
			dropShadowEffect.SetValue(DropShadowEffectBase.ColorProperty, Color.FromUInt32(uint.MaxValue), BindingPriority.Template);
			dropShadowEffect.SetValue(DropShadowEffectBase.OpacityProperty, 0.25, BindingPriority.Template);
			((AvaloniaObject)intermediateRoot).SetValue(effectProperty, (IEffect)dropShadowEffect, BindingPriority.Template);
			((ISupportInitialize)intermediateRoot).EndInit();
			return intermediateRoot;
		}
	}

	[CompilerGenerated]
	internal class NamespaceInfo_003A_002FControls_002FSlider_002Eaxaml : IAvaloniaXamlIlXmlNamespaceInfoProvider
	{
		private IReadOnlyDictionary<string, IReadOnlyList<AvaloniaXamlIlXmlNamespaceInfo>> _xmlNamespaces;

		public static IAvaloniaXamlIlXmlNamespaceInfoProvider Singleton;

		public virtual IReadOnlyDictionary<string, IReadOnlyList<AvaloniaXamlIlXmlNamespaceInfo>> XmlNamespaces
		{
			get
			{
				if (_xmlNamespaces == null)
				{
					_xmlNamespaces = CreateNamespaces();
				}
				return _xmlNamespaces;
			}
		}

		private static AvaloniaXamlIlXmlNamespaceInfo CreateNamespaceInfo(string P_0, string P_1)
		{
			return new AvaloniaXamlIlXmlNamespaceInfo
			{
				ClrNamespace = P_0,
				ClrAssemblyName = P_1
			};
		}

		private static IReadOnlyDictionary<string, IReadOnlyList<AvaloniaXamlIlXmlNamespaceInfo>> CreateNamespaces()
		{
			return new Dictionary<string, IReadOnlyList<AvaloniaXamlIlXmlNamespaceInfo>>(3)
			{
				{
					"",
					new AvaloniaXamlIlXmlNamespaceInfo[37]
					{
						CreateNamespaceInfo("Avalonia", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Animation", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Animation.Easings", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Controls", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Data", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Data.Converters", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Input", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Input.GestureRecognizers", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Input.TextInput", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Layout", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.LogicalTree", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Media", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Media.Imaging", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Media.Transformation", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Styling", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Controls", "Avalonia.Controls.DataGrid"),
						CreateNamespaceInfo("Avalonia.Controls.Collections", "Avalonia.Controls.DataGrid"),
						CreateNamespaceInfo("Avalonia.Controls.Primitives", "Avalonia.Controls.DataGrid"),
						CreateNamespaceInfo("Avalonia", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Automation", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Embedding", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Presenters", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Primitives", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Shapes", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Templates", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Notifications", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Chrome", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Documents", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Fonts.Inter", "Avalonia.Fonts.Inter"),
						CreateNamespaceInfo("Avalonia.Data", "Avalonia.Markup"),
						CreateNamespaceInfo("Avalonia.Markup.Data", "Avalonia.Markup"),
						CreateNamespaceInfo("Avalonia.Markup.Xaml.MarkupExtensions", "Avalonia.Markup.Xaml"),
						CreateNamespaceInfo("Avalonia.Markup.Xaml.Styling", "Avalonia.Markup.Xaml"),
						CreateNamespaceInfo("Avalonia.Markup.Xaml.Templates", "Avalonia.Markup.Xaml"),
						CreateNamespaceInfo("Avalonia.Themes.Fluent", "Avalonia.Themes.Fluent"),
						CreateNamespaceInfo("Avalonia.Svg.Skia", "Svg.Controls.Skia.Avalonia")
					}
				},
				{
					"x",
					new AvaloniaXamlIlXmlNamespaceInfo[0]
				},
				{
					"helper",
					new AvaloniaXamlIlXmlNamespaceInfo[1] { CreateNamespaceInfo("SpaceWalker.Helper", null) }
				}
			};
		}

		static NamespaceInfo_003A_002FControls_002FSlider_002Eaxaml()
		{
			Singleton = new NamespaceInfo_003A_002FControls_002FSlider_002Eaxaml();
		}
	}

	[CompilerGenerated]
	private class XamlClosure_9
	{
		public unsafe static object Build_1(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<Styles> context = CreateContext(P_0);
			ControlTheme controlTheme;
			ControlTheme result = (controlTheme = new ControlTheme());
			context.PushParent(controlTheme);
			controlTheme.TargetType = typeof(Slider);
			Setter setter = new Setter();
			setter.Property = Layoutable.MinHeightProperty;
			setter.Value = 22.0;
			controlTheme.Add(setter);
			Setter setter2 = new Setter();
			setter2.Property = TemplatedControl.BackgroundProperty;
			setter2.Value = new ImmutableSolidColorBrush(704643071u);
			controlTheme.Add(setter2);
			Setter setter3;
			Setter setter4 = (setter3 = new Setter());
			context.PushParent(setter3);
			setter3.Property = TemplatedControl.TemplateProperty;
			ControlTemplate controlTemplate;
			ControlTemplate value = (controlTemplate = new ControlTemplate());
			context.PushParent(controlTemplate);
			controlTemplate.Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&Build_2), context);
			context.PopParent();
			setter3.Value = value;
			context.PopParent();
			controlTheme.Add(setter4);
			context.PopParent();
			return result;
		}

		public static CompiledAvaloniaXaml.XamlIlContext.Context<Styles> CreateContext(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<Styles> context = new CompiledAvaloniaXaml.XamlIlContext.Context<Styles>(P_0, new object[1] { NamespaceInfo_003A_002FControls_002FSlider_002Eaxaml.Singleton }, "avares://SpaceWalker/Controls/Slider.axaml");
			if (P_0 != null)
			{
				object service = P_0.GetService(typeof(IRootObjectProvider));
				if (service != null)
				{
					service = ((IRootObjectProvider)service).RootObject;
					context.RootObject = (Styles)service;
				}
			}
			return context;
		}

		public unsafe static object Build_2(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<Styles> context = CreateContext(P_0);
			context.IntermediateRoot = new Grid();
			object intermediateRoot = context.IntermediateRoot;
			((ISupportInitialize)intermediateRoot).BeginInit();
			Grid grid = (Grid)intermediateRoot;
			context.PushParent(grid);
			grid.SetValue(Layoutable.VerticalAlignmentProperty, VerticalAlignment.Center, BindingPriority.Template);
			Controls children = grid.Children;
			Border border;
			Border border2 = (border = new Border());
			((ISupportInitialize)border2).BeginInit();
			children.Add(border2);
			border.SetValue(Layoutable.HeightProperty, 10.0, BindingPriority.Template);
			border.SetValue(Border.CornerRadiusProperty, new CornerRadius(5.0, 5.0, 5.0, 5.0), BindingPriority.Template);
			border.Bind(Border.BackgroundProperty, new TemplateBinding(TemplatedControl.BackgroundProperty).ProvideValue());
			border.SetValue(Layoutable.VerticalAlignmentProperty, VerticalAlignment.Center, BindingPriority.Template);
			((ISupportInitialize)border).EndInit();
			Controls children2 = grid.Children;
			Track track;
			Track track2 = (track = new Track());
			((ISupportInitialize)track2).BeginInit();
			children2.Add(track2);
			Track track3;
			Track track4 = (track3 = track);
			context.PushParent(track3);
			track3.Name = "PART_Track";
			object element = track3;
			context.AvaloniaNameScope.Register("PART_Track", element);
			track3.SetValue(Track.OrientationProperty, Orientation.Horizontal, BindingPriority.Template);
			track3.Bind(Track.MinimumProperty, new TemplateBinding(RangeBase.MinimumProperty).ProvideValue());
			track3.Bind(Track.MaximumProperty, new TemplateBinding(RangeBase.MaximumProperty).ProvideValue());
			track3.Bind(Track.ValueProperty, new TemplateBinding(RangeBase.ValueProperty)
			{
				Mode = BindingMode.TwoWay
			}.ProvideValue());
			StyledProperty<Button?> decreaseButtonProperty = Track.DecreaseButtonProperty;
			RepeatButton repeatButton;
			RepeatButton repeatButton2 = (repeatButton = new RepeatButton());
			((ISupportInitialize)repeatButton2).BeginInit();
			track3.SetValue(decreaseButtonProperty, repeatButton2, BindingPriority.Template);
			RepeatButton repeatButton3;
			RepeatButton repeatButton4 = (repeatButton3 = repeatButton);
			context.PushParent(repeatButton3);
			RepeatButton repeatButton5 = repeatButton3;
			repeatButton5.Name = "PART_DecreaseButton";
			element = repeatButton5;
			context.AvaloniaNameScope.Register("PART_DecreaseButton", element);
			StaticResourceExtension staticResourceExtension = new StaticResourceExtension("SliderAccentFillButton");
			context.ProvideTargetProperty = StyledElement.ThemeProperty;
			object? obj = staticResourceExtension.ProvideValue(context);
			context.ProvideTargetProperty = null;
			CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_9(repeatButton5, BindingPriority.Template, obj);
			context.PopParent();
			((ISupportInitialize)repeatButton4).EndInit();
			StyledProperty<Button?> increaseButtonProperty = Track.IncreaseButtonProperty;
			RepeatButton repeatButton6;
			RepeatButton repeatButton7 = (repeatButton6 = new RepeatButton());
			((ISupportInitialize)repeatButton7).BeginInit();
			track3.SetValue(increaseButtonProperty, repeatButton7, BindingPriority.Template);
			RepeatButton repeatButton8 = (repeatButton3 = repeatButton6);
			context.PushParent(repeatButton3);
			RepeatButton repeatButton9 = repeatButton3;
			repeatButton9.Name = "PART_IncreaseButton";
			element = repeatButton9;
			context.AvaloniaNameScope.Register("PART_IncreaseButton", element);
			StaticResourceExtension staticResourceExtension2 = new StaticResourceExtension("SliderTrackButton");
			context.ProvideTargetProperty = StyledElement.ThemeProperty;
			object? obj2 = staticResourceExtension2.ProvideValue(context);
			context.ProvideTargetProperty = null;
			CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_9(repeatButton9, BindingPriority.Template, obj2);
			repeatButton9.SetValue(TemplatedControl.BackgroundProperty, new ImmutableSolidColorBrush(16777215u), BindingPriority.Template);
			context.PopParent();
			((ISupportInitialize)repeatButton8).EndInit();
			Thumb thumb;
			Thumb thumb2 = (thumb = new Thumb());
			((ISupportInitialize)thumb2).BeginInit();
			track3.Thumb = thumb2;
			thumb.SetValue(Layoutable.WidthProperty, 14.0, BindingPriority.Template);
			thumb.SetValue(Layoutable.HeightProperty, 14.0, BindingPriority.Template);
			thumb.SetValue(TemplatedControl.TemplateProperty, new ControlTemplate
			{
				Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&Build_3), context)
			}, BindingPriority.Template);
			((ISupportInitialize)thumb).EndInit();
			context.PopParent();
			((ISupportInitialize)track4).EndInit();
			context.PopParent();
			((ISupportInitialize)intermediateRoot).EndInit();
			return intermediateRoot;
		}

		public static object Build_3(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<Styles> context = CreateContext(P_0);
			context.IntermediateRoot = new Border();
			object intermediateRoot = context.IntermediateRoot;
			((ISupportInitialize)intermediateRoot).BeginInit();
			((AvaloniaObject)intermediateRoot).SetValue(Layoutable.WidthProperty, 14.0, BindingPriority.Template);
			((AvaloniaObject)intermediateRoot).SetValue(Layoutable.HeightProperty, 14.0, BindingPriority.Template);
			((AvaloniaObject)intermediateRoot).SetValue(Border.CornerRadiusProperty, new CornerRadius(7.0, 7.0, 7.0, 7.0), BindingPriority.Template);
			((AvaloniaObject)intermediateRoot).SetValue(Border.BackgroundProperty, (IBrush)new ImmutableSolidColorBrush(4293784831u), BindingPriority.Template);
			StyledProperty<IEffect?> effectProperty = Visual.EffectProperty;
			DropShadowEffect dropShadowEffect = new DropShadowEffect();
			dropShadowEffect.SetValue(DropShadowEffect.OffsetXProperty, 0.0, BindingPriority.Template);
			dropShadowEffect.SetValue(DropShadowEffect.OffsetYProperty, 2.0, BindingPriority.Template);
			dropShadowEffect.SetValue(DropShadowEffectBase.BlurRadiusProperty, 3.0, BindingPriority.Template);
			dropShadowEffect.SetValue(DropShadowEffectBase.ColorProperty, Color.FromUInt32(4278190080u), BindingPriority.Template);
			dropShadowEffect.SetValue(DropShadowEffectBase.OpacityProperty, 0.1, BindingPriority.Template);
			((AvaloniaObject)intermediateRoot).SetValue(effectProperty, (IEffect)dropShadowEffect, BindingPriority.Template);
			((ISupportInitialize)intermediateRoot).EndInit();
			return intermediateRoot;
		}

		public unsafe static object Build_4(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<Styles> context = CreateContext(P_0);
			ControlTheme controlTheme;
			ControlTheme result = (controlTheme = new ControlTheme());
			context.PushParent(controlTheme);
			controlTheme.TargetType = typeof(Slider);
			Setter setter = new Setter();
			setter.Property = Layoutable.MinHeightProperty;
			setter.Value = 24.0;
			controlTheme.Add(setter);
			Setter setter2;
			Setter setter3 = (setter2 = new Setter());
			context.PushParent(setter2);
			setter2.Property = TemplatedControl.TemplateProperty;
			ControlTemplate controlTemplate;
			ControlTemplate value = (controlTemplate = new ControlTemplate());
			context.PushParent(controlTemplate);
			controlTemplate.Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&Build_5), context);
			context.PopParent();
			setter2.Value = value;
			context.PopParent();
			controlTheme.Add(setter3);
			context.PopParent();
			return result;
		}

		public unsafe static object Build_5(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<Styles> context = CreateContext(P_0);
			context.IntermediateRoot = new Grid();
			object intermediateRoot = context.IntermediateRoot;
			((ISupportInitialize)intermediateRoot).BeginInit();
			Grid grid = (Grid)intermediateRoot;
			context.PushParent(grid);
			grid.SetValue(Layoutable.VerticalAlignmentProperty, VerticalAlignment.Center, BindingPriority.Template);
			Controls children = grid.Children;
			Image image;
			Image image2 = (image = new Image());
			((ISupportInitialize)image2).BeginInit();
			children.Add(image2);
			Image image3;
			Image image4 = (image3 = image);
			context.PushParent(image3);
			image3.SetValue(Image.SourceProperty, (IImage)new BitmapTypeConverter().ConvertFrom(context, CultureInfo.InvariantCulture, "avares://SpaceWalker/Assets/Images/pbz/slider_track.png"), BindingPriority.Template);
			image3.SetValue(Layoutable.HeightProperty, 5.0, BindingPriority.Template);
			image3.SetValue(Image.StretchProperty, Stretch.Fill, BindingPriority.Template);
			image3.SetValue(Visual.OpacityProperty, 0.4, BindingPriority.Template);
			image3.SetValue(Layoutable.VerticalAlignmentProperty, VerticalAlignment.Center, BindingPriority.Template);
			context.PopParent();
			((ISupportInitialize)image4).EndInit();
			Controls children2 = grid.Children;
			Track track;
			Track track2 = (track = new Track());
			((ISupportInitialize)track2).BeginInit();
			children2.Add(track2);
			Track track3;
			Track track4 = (track3 = track);
			context.PushParent(track3);
			track3.Name = "PART_Track";
			object element = track3;
			context.AvaloniaNameScope.Register("PART_Track", element);
			track3.SetValue(Track.OrientationProperty, Orientation.Horizontal, BindingPriority.Template);
			track3.Bind(Track.MinimumProperty, new TemplateBinding(RangeBase.MinimumProperty).ProvideValue());
			track3.Bind(Track.MaximumProperty, new TemplateBinding(RangeBase.MaximumProperty).ProvideValue());
			track3.Bind(Track.ValueProperty, new TemplateBinding(RangeBase.ValueProperty)
			{
				Mode = BindingMode.TwoWay
			}.ProvideValue());
			StyledProperty<Button?> decreaseButtonProperty = Track.DecreaseButtonProperty;
			RepeatButton repeatButton;
			RepeatButton repeatButton2 = (repeatButton = new RepeatButton());
			((ISupportInitialize)repeatButton2).BeginInit();
			track3.SetValue(decreaseButtonProperty, repeatButton2, BindingPriority.Template);
			RepeatButton repeatButton3;
			RepeatButton repeatButton4 = (repeatButton3 = repeatButton);
			context.PushParent(repeatButton3);
			RepeatButton repeatButton5 = repeatButton3;
			repeatButton5.Name = "PART_DecreaseButton";
			element = repeatButton5;
			context.AvaloniaNameScope.Register("PART_DecreaseButton", element);
			StaticResourceExtension staticResourceExtension = new StaticResourceExtension("PbzSliderFillButton");
			context.ProvideTargetProperty = StyledElement.ThemeProperty;
			object? obj = staticResourceExtension.ProvideValue(context);
			context.ProvideTargetProperty = null;
			CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_9(repeatButton5, BindingPriority.Template, obj);
			context.PopParent();
			((ISupportInitialize)repeatButton4).EndInit();
			StyledProperty<Button?> increaseButtonProperty = Track.IncreaseButtonProperty;
			RepeatButton repeatButton6;
			RepeatButton repeatButton7 = (repeatButton6 = new RepeatButton());
			((ISupportInitialize)repeatButton7).BeginInit();
			track3.SetValue(increaseButtonProperty, repeatButton7, BindingPriority.Template);
			RepeatButton repeatButton8 = (repeatButton3 = repeatButton6);
			context.PushParent(repeatButton3);
			RepeatButton repeatButton9 = repeatButton3;
			repeatButton9.Name = "PART_IncreaseButton";
			element = repeatButton9;
			context.AvaloniaNameScope.Register("PART_IncreaseButton", element);
			StaticResourceExtension staticResourceExtension2 = new StaticResourceExtension("SliderTrackButton");
			context.ProvideTargetProperty = StyledElement.ThemeProperty;
			object? obj2 = staticResourceExtension2.ProvideValue(context);
			context.ProvideTargetProperty = null;
			CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_9(repeatButton9, BindingPriority.Template, obj2);
			repeatButton9.SetValue(TemplatedControl.BackgroundProperty, new ImmutableSolidColorBrush(16777215u), BindingPriority.Template);
			context.PopParent();
			((ISupportInitialize)repeatButton8).EndInit();
			Thumb thumb;
			Thumb thumb2 = (thumb = new Thumb());
			((ISupportInitialize)thumb2).BeginInit();
			track3.Thumb = thumb2;
			Thumb thumb3;
			Thumb thumb4 = (thumb3 = thumb);
			context.PushParent(thumb3);
			thumb3.SetValue(Layoutable.WidthProperty, 14.0, BindingPriority.Template);
			thumb3.SetValue(Layoutable.HeightProperty, 23.0, BindingPriority.Template);
			StyledProperty<IControlTemplate?> templateProperty = TemplatedControl.TemplateProperty;
			ControlTemplate controlTemplate;
			ControlTemplate value = (controlTemplate = new ControlTemplate());
			context.PushParent(controlTemplate);
			controlTemplate.Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&Build_6), context);
			context.PopParent();
			thumb3.SetValue(templateProperty, value, BindingPriority.Template);
			context.PopParent();
			((ISupportInitialize)thumb4).EndInit();
			context.PopParent();
			((ISupportInitialize)track4).EndInit();
			context.PopParent();
			((ISupportInitialize)intermediateRoot).EndInit();
			return intermediateRoot;
		}

		public static object Build_6(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<Styles> context = CreateContext(P_0);
			context.IntermediateRoot = new Image();
			object intermediateRoot = context.IntermediateRoot;
			((ISupportInitialize)intermediateRoot).BeginInit();
			Image image = (Image)intermediateRoot;
			context.PushParent(image);
			image.SetValue(Image.SourceProperty, (IImage)new BitmapTypeConverter().ConvertFrom(context, CultureInfo.InvariantCulture, "avares://SpaceWalker/Assets/Images/pbz/slider_thumb.png"), BindingPriority.Template);
			image.SetValue(Layoutable.WidthProperty, 14.0, BindingPriority.Template);
			image.SetValue(Layoutable.HeightProperty, 23.0, BindingPriority.Template);
			image.SetValue(Image.StretchProperty, Stretch.Uniform, BindingPriority.Template);
			context.PopParent();
			((ISupportInitialize)intermediateRoot).EndInit();
			return intermediateRoot;
		}

		public unsafe static object Build_7(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<Styles> provider = CreateContext(P_0);
			ControlTheme controlTheme = new ControlTheme();
			controlTheme.TargetType = typeof(RepeatButton);
			Setter setter = new Setter();
			setter.Property = InputElement.FocusableProperty;
			setter.Value = false;
			controlTheme.Add(setter);
			Setter setter2 = new Setter();
			setter2.Property = Visual.ClipToBoundsProperty;
			setter2.Value = false;
			controlTheme.Add(setter2);
			Setter setter3 = new Setter();
			setter3.Property = TemplatedControl.TemplateProperty;
			setter3.Value = new ControlTemplate
			{
				Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&Build_8), provider)
			};
			controlTheme.Add(setter3);
			return controlTheme;
		}

		public static object Build_8(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<Styles> context = CreateContext(P_0);
			context.IntermediateRoot = new Border();
			object intermediateRoot = context.IntermediateRoot;
			((ISupportInitialize)intermediateRoot).BeginInit();
			((AvaloniaObject)intermediateRoot).SetValue(Layoutable.HeightProperty, 10.0, BindingPriority.Template);
			((AvaloniaObject)intermediateRoot).SetValue(Border.CornerRadiusProperty, new CornerRadius(5.0, 0.0, 0.0, 5.0), BindingPriority.Template);
			((AvaloniaObject)intermediateRoot).Bind((AvaloniaProperty)Border.BackgroundProperty, new TemplateBinding(TemplatedControl.BackgroundProperty).ProvideValue());
			((AvaloniaObject)intermediateRoot).SetValue(Layoutable.MarginProperty, new Thickness(0.0, 0.0, -7.0, 0.0), BindingPriority.Template);
			((AvaloniaObject)intermediateRoot).SetValue(Layoutable.VerticalAlignmentProperty, VerticalAlignment.Center, BindingPriority.Template);
			((ISupportInitialize)intermediateRoot).EndInit();
			return intermediateRoot;
		}

		public unsafe static object Build_9(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<Styles> provider = CreateContext(P_0);
			ControlTheme controlTheme = new ControlTheme();
			controlTheme.TargetType = typeof(RepeatButton);
			Setter setter = new Setter();
			setter.Property = InputElement.FocusableProperty;
			setter.Value = false;
			controlTheme.Add(setter);
			Setter setter2 = new Setter();
			setter2.Property = Visual.ClipToBoundsProperty;
			setter2.Value = false;
			controlTheme.Add(setter2);
			Setter setter3 = new Setter();
			setter3.Property = TemplatedControl.TemplateProperty;
			setter3.Value = new ControlTemplate
			{
				Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&Build_10), provider)
			};
			controlTheme.Add(setter3);
			return controlTheme;
		}

		public static object Build_10(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<Styles> context = CreateContext(P_0);
			context.IntermediateRoot = new Border();
			object intermediateRoot = context.IntermediateRoot;
			((ISupportInitialize)intermediateRoot).BeginInit();
			((AvaloniaObject)intermediateRoot).SetValue(Layoutable.HeightProperty, 10.0, BindingPriority.Template);
			((AvaloniaObject)intermediateRoot).SetValue(Border.CornerRadiusProperty, new CornerRadius(5.0, 0.0, 0.0, 5.0), BindingPriority.Template);
			((AvaloniaObject)intermediateRoot).SetValue(Layoutable.MarginProperty, new Thickness(0.0, 0.0, -7.0, 0.0), BindingPriority.Template);
			((AvaloniaObject)intermediateRoot).SetValue(Layoutable.VerticalAlignmentProperty, VerticalAlignment.Center, BindingPriority.Template);
			StyledProperty<IBrush?> backgroundProperty = Border.BackgroundProperty;
			LinearGradientBrush linearGradientBrush = new LinearGradientBrush();
			linearGradientBrush.SetValue(LinearGradientBrush.StartPointProperty, new RelativePoint(0.0, 0.5, RelativeUnit.Relative), BindingPriority.Template);
			linearGradientBrush.SetValue(LinearGradientBrush.EndPointProperty, new RelativePoint(1.0, 0.5, RelativeUnit.Relative), BindingPriority.Template);
			GradientStops gradientStops = linearGradientBrush.GradientStops;
			GradientStop gradientStop = new GradientStop();
			gradientStop.SetValue(GradientStop.OffsetProperty, 0.437, BindingPriority.Template);
			gradientStop.SetValue(GradientStop.ColorProperty, Color.FromUInt32(4278547452u), BindingPriority.Template);
			gradientStops.Add(gradientStop);
			GradientStops gradientStops2 = linearGradientBrush.GradientStops;
			GradientStop gradientStop2 = new GradientStop();
			gradientStop2.SetValue(GradientStop.OffsetProperty, 0.927, BindingPriority.Template);
			gradientStop2.SetValue(GradientStop.ColorProperty, Color.FromUInt32(4293256677u), BindingPriority.Template);
			gradientStops2.Add(gradientStop2);
			((AvaloniaObject)intermediateRoot).SetValue(backgroundProperty, (IBrush)linearGradientBrush, BindingPriority.Template);
			((ISupportInitialize)intermediateRoot).EndInit();
			return intermediateRoot;
		}

		public unsafe static object Build_11(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<Styles> context = CreateContext(P_0);
			ControlTheme controlTheme;
			ControlTheme result = (controlTheme = new ControlTheme());
			context.PushParent(controlTheme);
			controlTheme.TargetType = typeof(RepeatButton);
			Setter setter = new Setter();
			setter.Property = InputElement.FocusableProperty;
			setter.Value = false;
			controlTheme.Add(setter);
			Setter setter2 = new Setter();
			setter2.Property = Visual.ClipToBoundsProperty;
			setter2.Value = false;
			controlTheme.Add(setter2);
			Setter setter3;
			Setter setter4 = (setter3 = new Setter());
			context.PushParent(setter3);
			setter3.Property = TemplatedControl.TemplateProperty;
			ControlTemplate controlTemplate;
			ControlTemplate value = (controlTemplate = new ControlTemplate());
			context.PushParent(controlTemplate);
			controlTemplate.Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&Build_12), context);
			context.PopParent();
			setter3.Value = value;
			context.PopParent();
			controlTheme.Add(setter4);
			context.PopParent();
			return result;
		}

		public static object Build_12(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<Styles> context = CreateContext(P_0);
			context.IntermediateRoot = new Border();
			object intermediateRoot = context.IntermediateRoot;
			((ISupportInitialize)intermediateRoot).BeginInit();
			Border border = (Border)intermediateRoot;
			context.PushParent(border);
			border.SetValue(Layoutable.HeightProperty, 5.0, BindingPriority.Template);
			border.SetValue(Layoutable.MarginProperty, new Thickness(0.0, 0.0, -7.0, 0.0), BindingPriority.Template);
			border.SetValue(Layoutable.VerticalAlignmentProperty, VerticalAlignment.Center, BindingPriority.Template);
			StyledProperty<IBrush?> backgroundProperty = Border.BackgroundProperty;
			ImageBrush imageBrush;
			ImageBrush value = (imageBrush = new ImageBrush());
			context.PushParent(imageBrush);
			imageBrush.SetValue(ImageBrush.SourceProperty, (IImageBrushSource)new BitmapTypeConverter().ConvertFrom(context, CultureInfo.InvariantCulture, "avares://SpaceWalker/Assets/Images/pbz/slider_fill.png"), BindingPriority.Template);
			imageBrush.SetValue(TileBrush.StretchProperty, Stretch.Fill, BindingPriority.Template);
			context.PopParent();
			border.SetValue(backgroundProperty, value, BindingPriority.Template);
			context.PopParent();
			((ISupportInitialize)intermediateRoot).EndInit();
			return intermediateRoot;
		}
	}

	[CompilerGenerated]
	internal class NamespaceInfo_003A_002FControls_002FStyles_002Eaxaml : IAvaloniaXamlIlXmlNamespaceInfoProvider
	{
		private IReadOnlyDictionary<string, IReadOnlyList<AvaloniaXamlIlXmlNamespaceInfo>> _xmlNamespaces;

		public static IAvaloniaXamlIlXmlNamespaceInfoProvider Singleton;

		public virtual IReadOnlyDictionary<string, IReadOnlyList<AvaloniaXamlIlXmlNamespaceInfo>> XmlNamespaces
		{
			get
			{
				if (_xmlNamespaces == null)
				{
					_xmlNamespaces = CreateNamespaces();
				}
				return _xmlNamespaces;
			}
		}

		private static AvaloniaXamlIlXmlNamespaceInfo CreateNamespaceInfo(string P_0, string P_1)
		{
			return new AvaloniaXamlIlXmlNamespaceInfo
			{
				ClrNamespace = P_0,
				ClrAssemblyName = P_1
			};
		}

		private static IReadOnlyDictionary<string, IReadOnlyList<AvaloniaXamlIlXmlNamespaceInfo>> CreateNamespaces()
		{
			return new Dictionary<string, IReadOnlyList<AvaloniaXamlIlXmlNamespaceInfo>>(3)
			{
				{
					"",
					new AvaloniaXamlIlXmlNamespaceInfo[37]
					{
						CreateNamespaceInfo("Avalonia", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Animation", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Animation.Easings", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Controls", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Data", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Data.Converters", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Input", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Input.GestureRecognizers", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Input.TextInput", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Layout", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.LogicalTree", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Media", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Media.Imaging", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Media.Transformation", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Styling", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Controls", "Avalonia.Controls.DataGrid"),
						CreateNamespaceInfo("Avalonia.Controls.Collections", "Avalonia.Controls.DataGrid"),
						CreateNamespaceInfo("Avalonia.Controls.Primitives", "Avalonia.Controls.DataGrid"),
						CreateNamespaceInfo("Avalonia", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Automation", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Embedding", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Presenters", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Primitives", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Shapes", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Templates", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Notifications", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Chrome", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Documents", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Fonts.Inter", "Avalonia.Fonts.Inter"),
						CreateNamespaceInfo("Avalonia.Data", "Avalonia.Markup"),
						CreateNamespaceInfo("Avalonia.Markup.Data", "Avalonia.Markup"),
						CreateNamespaceInfo("Avalonia.Markup.Xaml.MarkupExtensions", "Avalonia.Markup.Xaml"),
						CreateNamespaceInfo("Avalonia.Markup.Xaml.Styling", "Avalonia.Markup.Xaml"),
						CreateNamespaceInfo("Avalonia.Markup.Xaml.Templates", "Avalonia.Markup.Xaml"),
						CreateNamespaceInfo("Avalonia.Themes.Fluent", "Avalonia.Themes.Fluent"),
						CreateNamespaceInfo("Avalonia.Svg.Skia", "Svg.Controls.Skia.Avalonia")
					}
				},
				{
					"x",
					new AvaloniaXamlIlXmlNamespaceInfo[0]
				},
				{
					"ui",
					new AvaloniaXamlIlXmlNamespaceInfo[1] { CreateNamespaceInfo("Avalonia.Labs.Controls", null) }
				}
			};
		}

		static NamespaceInfo_003A_002FControls_002FStyles_002Eaxaml()
		{
			Singleton = new NamespaceInfo_003A_002FControls_002FStyles_002Eaxaml();
		}
	}

	[CompilerGenerated]
	internal class NamespaceInfo_003A_002FControls_002FTabControl_002Eaxaml : IAvaloniaXamlIlXmlNamespaceInfoProvider
	{
		private IReadOnlyDictionary<string, IReadOnlyList<AvaloniaXamlIlXmlNamespaceInfo>> _xmlNamespaces;

		public static IAvaloniaXamlIlXmlNamespaceInfoProvider Singleton;

		public virtual IReadOnlyDictionary<string, IReadOnlyList<AvaloniaXamlIlXmlNamespaceInfo>> XmlNamespaces
		{
			get
			{
				if (_xmlNamespaces == null)
				{
					_xmlNamespaces = CreateNamespaces();
				}
				return _xmlNamespaces;
			}
		}

		private static AvaloniaXamlIlXmlNamespaceInfo CreateNamespaceInfo(string P_0, string P_1)
		{
			return new AvaloniaXamlIlXmlNamespaceInfo
			{
				ClrNamespace = P_0,
				ClrAssemblyName = P_1
			};
		}

		private static IReadOnlyDictionary<string, IReadOnlyList<AvaloniaXamlIlXmlNamespaceInfo>> CreateNamespaces()
		{
			return new Dictionary<string, IReadOnlyList<AvaloniaXamlIlXmlNamespaceInfo>>(3)
			{
				{
					"",
					new AvaloniaXamlIlXmlNamespaceInfo[37]
					{
						CreateNamespaceInfo("Avalonia", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Animation", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Animation.Easings", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Controls", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Data", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Data.Converters", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Input", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Input.GestureRecognizers", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Input.TextInput", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Layout", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.LogicalTree", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Media", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Media.Imaging", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Media.Transformation", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Styling", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Controls", "Avalonia.Controls.DataGrid"),
						CreateNamespaceInfo("Avalonia.Controls.Collections", "Avalonia.Controls.DataGrid"),
						CreateNamespaceInfo("Avalonia.Controls.Primitives", "Avalonia.Controls.DataGrid"),
						CreateNamespaceInfo("Avalonia", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Automation", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Embedding", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Presenters", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Primitives", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Shapes", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Templates", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Notifications", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Chrome", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Documents", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Fonts.Inter", "Avalonia.Fonts.Inter"),
						CreateNamespaceInfo("Avalonia.Data", "Avalonia.Markup"),
						CreateNamespaceInfo("Avalonia.Markup.Data", "Avalonia.Markup"),
						CreateNamespaceInfo("Avalonia.Markup.Xaml.MarkupExtensions", "Avalonia.Markup.Xaml"),
						CreateNamespaceInfo("Avalonia.Markup.Xaml.Styling", "Avalonia.Markup.Xaml"),
						CreateNamespaceInfo("Avalonia.Markup.Xaml.Templates", "Avalonia.Markup.Xaml"),
						CreateNamespaceInfo("Avalonia.Themes.Fluent", "Avalonia.Themes.Fluent"),
						CreateNamespaceInfo("Avalonia.Svg.Skia", "Svg.Controls.Skia.Avalonia")
					}
				},
				{
					"x",
					new AvaloniaXamlIlXmlNamespaceInfo[0]
				},
				{
					"i18n",
					new AvaloniaXamlIlXmlNamespaceInfo[1] { CreateNamespaceInfo("SpaceWalker.Assets.Languages", null) }
				}
			};
		}

		static NamespaceInfo_003A_002FControls_002FTabControl_002Eaxaml()
		{
			Singleton = new NamespaceInfo_003A_002FControls_002FTabControl_002Eaxaml();
		}
	}

	[CompilerGenerated]
	private class XamlClosure_10
	{
		public static object Build_1(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<Styles> context = CreateContext(P_0);
			context.IntermediateRoot = new Grid();
			object intermediateRoot = context.IntermediateRoot;
			((ISupportInitialize)intermediateRoot).BeginInit();
			ColumnDefinitions columnDefinitions = new ColumnDefinitions();
			columnDefinitions.Capacity = 2;
			columnDefinitions.Add(new ColumnDefinition(new GridLength(0.0, GridUnitType.Auto)));
			columnDefinitions.Add(new ColumnDefinition(new GridLength(1.0, GridUnitType.Star)));
			((Grid)intermediateRoot).ColumnDefinitions = columnDefinitions;
			Controls children = ((Panel)intermediateRoot).Children;
			Border border;
			Border border2 = (border = new Border());
			((ISupportInitialize)border2).BeginInit();
			children.Add(border2);
			border.SetValue(Grid.ColumnProperty, 0, BindingPriority.Template);
			border.SetValue(Layoutable.WidthProperty, 179.0, BindingPriority.Template);
			border.SetValue(Border.BackgroundProperty, new ImmutableSolidColorBrush(520093696u), BindingPriority.Template);
			border.SetValue(Border.CornerRadiusProperty, new CornerRadius(4.0, 4.0, 4.0, 4.0), BindingPriority.Template);
			border.SetValue(Layoutable.MarginProperty, new Thickness(8.0, 8.0, 8.0, 8.0), BindingPriority.Template);
			Grid grid;
			Grid grid2 = (grid = new Grid());
			((ISupportInitialize)grid2).BeginInit();
			border.Child = grid2;
			RowDefinitions rowDefinitions = new RowDefinitions();
			rowDefinitions.Capacity = 3;
			rowDefinitions.Add(new RowDefinition(new GridLength(0.0, GridUnitType.Auto)));
			rowDefinitions.Add(new RowDefinition(new GridLength(1.0, GridUnitType.Star)));
			rowDefinitions.Add(new RowDefinition(new GridLength(0.0, GridUnitType.Auto)));
			grid.RowDefinitions = rowDefinitions;
			grid.SetValue(Layoutable.MarginProperty, new Thickness(8.0, 16.0, 8.0, 16.0), BindingPriority.Template);
			Controls children2 = grid.Children;
			TextBlock textBlock;
			TextBlock textBlock2 = (textBlock = new TextBlock());
			((ISupportInitialize)textBlock2).BeginInit();
			children2.Add(textBlock2);
			textBlock.SetValue(Grid.RowProperty, 0, BindingPriority.Template);
			textBlock.SetValue(TextBlock.TextProperty, Resources.Settings, BindingPriority.Template);
			textBlock.Classes.Add("textcaption");
			textBlock.SetValue(TextBlock.ForegroundProperty, new ImmutableSolidColorBrush(3103784959u), BindingPriority.Template);
			textBlock.SetValue(Layoutable.MarginProperty, new Thickness(12.0, 2.0, 12.0, 12.0), BindingPriority.Template);
			((ISupportInitialize)textBlock).EndInit();
			Controls children3 = grid.Children;
			ScrollViewer scrollViewer;
			ScrollViewer scrollViewer2 = (scrollViewer = new ScrollViewer());
			((ISupportInitialize)scrollViewer2).BeginInit();
			children3.Add(scrollViewer2);
			scrollViewer.SetValue(Grid.RowProperty, 1, BindingPriority.Template);
			scrollViewer.SetValue(ScrollViewer.HorizontalScrollBarVisibilityProperty, ScrollBarVisibility.Disabled, BindingPriority.Template);
			scrollViewer.SetValue(ScrollViewer.VerticalScrollBarVisibilityProperty, ScrollBarVisibility.Disabled, BindingPriority.Template);
			ItemsPresenter itemsPresenter;
			ItemsPresenter itemsPresenter2 = (itemsPresenter = new ItemsPresenter());
			((ISupportInitialize)itemsPresenter2).BeginInit();
			scrollViewer.Content = itemsPresenter2;
			((ISupportInitialize)itemsPresenter).EndInit();
			((ISupportInitialize)scrollViewer).EndInit();
			Controls children4 = grid.Children;
			ContentPresenter contentPresenter;
			ContentPresenter contentPresenter2 = (contentPresenter = new ContentPresenter());
			((ISupportInitialize)contentPresenter2).BeginInit();
			children4.Add(contentPresenter2);
			contentPresenter.SetValue(Grid.RowProperty, 2, BindingPriority.Template);
			contentPresenter.SetValue(Layoutable.MarginProperty, new Thickness(8.0, 8.0, 8.0, 0.0), BindingPriority.Template);
			CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(contentPresenter, BindingPriority.Template, new TemplateBinding(Control.TagProperty).ProvideValue());
			((ISupportInitialize)contentPresenter).EndInit();
			((ISupportInitialize)grid).EndInit();
			((ISupportInitialize)border).EndInit();
			Controls children5 = ((Panel)intermediateRoot).Children;
			ContentPresenter contentPresenter3;
			ContentPresenter contentPresenter4 = (contentPresenter3 = new ContentPresenter());
			((ISupportInitialize)contentPresenter4).BeginInit();
			children5.Add(contentPresenter4);
			contentPresenter3.SetValue(Grid.ColumnProperty, 1, BindingPriority.Template);
			contentPresenter3.SetValue(Layoutable.MarginProperty, new Thickness(16.0, 16.0, 16.0, 16.0), BindingPriority.Template);
			CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(contentPresenter3, BindingPriority.Template, new TemplateBinding(Avalonia.Controls.TabControl.SelectedContentProperty).ProvideValue());
			contentPresenter3.Bind(ContentPresenter.ContentTemplateProperty, new TemplateBinding(Avalonia.Controls.TabControl.SelectedContentTemplateProperty).ProvideValue());
			((ISupportInitialize)contentPresenter3).EndInit();
			((ISupportInitialize)intermediateRoot).EndInit();
			return intermediateRoot;
		}

		public static CompiledAvaloniaXaml.XamlIlContext.Context<Styles> CreateContext(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<Styles> context = new CompiledAvaloniaXaml.XamlIlContext.Context<Styles>(P_0, new object[1] { NamespaceInfo_003A_002FControls_002FTabControl_002Eaxaml.Singleton }, "avares://SpaceWalker/Controls/TabControl.axaml");
			if (P_0 != null)
			{
				object service = P_0.GetService(typeof(IRootObjectProvider));
				if (service != null)
				{
					service = ((IRootObjectProvider)service).RootObject;
					context.RootObject = (Styles)service;
				}
			}
			return context;
		}

		public static object Build_2(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<Styles> context = CreateContext(P_0);
			context.IntermediateRoot = new Border();
			object intermediateRoot = context.IntermediateRoot;
			((ISupportInitialize)intermediateRoot).BeginInit();
			((StyledElement)intermediateRoot).Name = "PART_LayoutRoot";
			object element = intermediateRoot;
			context.AvaloniaNameScope.Register("PART_LayoutRoot", element);
			((AvaloniaObject)intermediateRoot).SetValue(Border.CornerRadiusProperty, new CornerRadius(4.0, 4.0, 4.0, 4.0), BindingPriority.Template);
			((AvaloniaObject)intermediateRoot).SetValue(Border.BorderThicknessProperty, new Thickness(1.0, 0.0, 0.0, 0.0), BindingPriority.Template);
			((AvaloniaObject)intermediateRoot).SetValue(Border.BorderBrushProperty, (IBrush)new ImmutableSolidColorBrush(16777215u), BindingPriority.Template);
			((AvaloniaObject)intermediateRoot).SetValue(Border.BackgroundProperty, (IBrush)new ImmutableSolidColorBrush(16777215u), BindingPriority.Template);
			((AvaloniaObject)intermediateRoot).Bind((AvaloniaProperty)Decorator.PaddingProperty, new TemplateBinding(TemplatedControl.PaddingProperty).ProvideValue());
			ContentPresenter contentPresenter;
			ContentPresenter contentPresenter2 = (contentPresenter = new ContentPresenter());
			((ISupportInitialize)contentPresenter2).BeginInit();
			((Decorator)intermediateRoot).Child = contentPresenter2;
			contentPresenter.Name = "PART_ContentPresenter";
			element = contentPresenter;
			context.AvaloniaNameScope.Register("PART_ContentPresenter", element);
			CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(contentPresenter, BindingPriority.Template, new TemplateBinding(HeaderedContentControl.HeaderProperty).ProvideValue());
			contentPresenter.Bind(ContentPresenter.ContentTemplateProperty, new TemplateBinding(HeaderedContentControl.HeaderTemplateProperty).ProvideValue());
			contentPresenter.SetValue(Layoutable.VerticalAlignmentProperty, VerticalAlignment.Center, BindingPriority.Template);
			contentPresenter.Bind(ContentPresenter.ForegroundProperty, new TemplateBinding(TemplatedControl.ForegroundProperty).ProvideValue());
			((ISupportInitialize)contentPresenter).EndInit();
			((ISupportInitialize)intermediateRoot).EndInit();
			return intermediateRoot;
		}
	}

	[CompilerGenerated]
	internal class NamespaceInfo_003A_002FControls_002FTextStyles_002Eaxaml : IAvaloniaXamlIlXmlNamespaceInfoProvider
	{
		private IReadOnlyDictionary<string, IReadOnlyList<AvaloniaXamlIlXmlNamespaceInfo>> _xmlNamespaces;

		public static IAvaloniaXamlIlXmlNamespaceInfoProvider Singleton;

		public virtual IReadOnlyDictionary<string, IReadOnlyList<AvaloniaXamlIlXmlNamespaceInfo>> XmlNamespaces
		{
			get
			{
				if (_xmlNamespaces == null)
				{
					_xmlNamespaces = CreateNamespaces();
				}
				return _xmlNamespaces;
			}
		}

		private static AvaloniaXamlIlXmlNamespaceInfo CreateNamespaceInfo(string P_0, string P_1)
		{
			return new AvaloniaXamlIlXmlNamespaceInfo
			{
				ClrNamespace = P_0,
				ClrAssemblyName = P_1
			};
		}

		private static IReadOnlyDictionary<string, IReadOnlyList<AvaloniaXamlIlXmlNamespaceInfo>> CreateNamespaces()
		{
			return new Dictionary<string, IReadOnlyList<AvaloniaXamlIlXmlNamespaceInfo>>(2)
			{
				{
					"",
					new AvaloniaXamlIlXmlNamespaceInfo[37]
					{
						CreateNamespaceInfo("Avalonia", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Animation", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Animation.Easings", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Controls", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Data", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Data.Converters", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Input", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Input.GestureRecognizers", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Input.TextInput", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Layout", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.LogicalTree", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Media", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Media.Imaging", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Media.Transformation", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Styling", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Controls", "Avalonia.Controls.DataGrid"),
						CreateNamespaceInfo("Avalonia.Controls.Collections", "Avalonia.Controls.DataGrid"),
						CreateNamespaceInfo("Avalonia.Controls.Primitives", "Avalonia.Controls.DataGrid"),
						CreateNamespaceInfo("Avalonia", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Automation", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Embedding", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Presenters", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Primitives", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Shapes", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Templates", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Notifications", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Chrome", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Documents", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Fonts.Inter", "Avalonia.Fonts.Inter"),
						CreateNamespaceInfo("Avalonia.Data", "Avalonia.Markup"),
						CreateNamespaceInfo("Avalonia.Markup.Data", "Avalonia.Markup"),
						CreateNamespaceInfo("Avalonia.Markup.Xaml.MarkupExtensions", "Avalonia.Markup.Xaml"),
						CreateNamespaceInfo("Avalonia.Markup.Xaml.Styling", "Avalonia.Markup.Xaml"),
						CreateNamespaceInfo("Avalonia.Markup.Xaml.Templates", "Avalonia.Markup.Xaml"),
						CreateNamespaceInfo("Avalonia.Themes.Fluent", "Avalonia.Themes.Fluent"),
						CreateNamespaceInfo("Avalonia.Svg.Skia", "Svg.Controls.Skia.Avalonia")
					}
				},
				{
					"x",
					new AvaloniaXamlIlXmlNamespaceInfo[0]
				}
			};
		}

		static NamespaceInfo_003A_002FControls_002FTextStyles_002Eaxaml()
		{
			Singleton = new NamespaceInfo_003A_002FControls_002FTextStyles_002Eaxaml();
		}
	}

	[CompilerGenerated]
	internal class NamespaceInfo_003A_002FControls_002FToggleSwitch_002Eaxaml : IAvaloniaXamlIlXmlNamespaceInfoProvider
	{
		private IReadOnlyDictionary<string, IReadOnlyList<AvaloniaXamlIlXmlNamespaceInfo>> _xmlNamespaces;

		public static IAvaloniaXamlIlXmlNamespaceInfoProvider Singleton;

		public virtual IReadOnlyDictionary<string, IReadOnlyList<AvaloniaXamlIlXmlNamespaceInfo>> XmlNamespaces
		{
			get
			{
				if (_xmlNamespaces == null)
				{
					_xmlNamespaces = CreateNamespaces();
				}
				return _xmlNamespaces;
			}
		}

		private static AvaloniaXamlIlXmlNamespaceInfo CreateNamespaceInfo(string P_0, string P_1)
		{
			return new AvaloniaXamlIlXmlNamespaceInfo
			{
				ClrNamespace = P_0,
				ClrAssemblyName = P_1
			};
		}

		private static IReadOnlyDictionary<string, IReadOnlyList<AvaloniaXamlIlXmlNamespaceInfo>> CreateNamespaces()
		{
			return new Dictionary<string, IReadOnlyList<AvaloniaXamlIlXmlNamespaceInfo>>(2)
			{
				{
					"",
					new AvaloniaXamlIlXmlNamespaceInfo[37]
					{
						CreateNamespaceInfo("Avalonia", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Animation", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Animation.Easings", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Controls", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Data", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Data.Converters", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Input", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Input.GestureRecognizers", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Input.TextInput", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Layout", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.LogicalTree", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Media", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Media.Imaging", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Media.Transformation", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Styling", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Controls", "Avalonia.Controls.DataGrid"),
						CreateNamespaceInfo("Avalonia.Controls.Collections", "Avalonia.Controls.DataGrid"),
						CreateNamespaceInfo("Avalonia.Controls.Primitives", "Avalonia.Controls.DataGrid"),
						CreateNamespaceInfo("Avalonia", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Automation", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Embedding", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Presenters", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Primitives", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Shapes", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Templates", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Notifications", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Chrome", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Documents", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Fonts.Inter", "Avalonia.Fonts.Inter"),
						CreateNamespaceInfo("Avalonia.Data", "Avalonia.Markup"),
						CreateNamespaceInfo("Avalonia.Markup.Data", "Avalonia.Markup"),
						CreateNamespaceInfo("Avalonia.Markup.Xaml.MarkupExtensions", "Avalonia.Markup.Xaml"),
						CreateNamespaceInfo("Avalonia.Markup.Xaml.Styling", "Avalonia.Markup.Xaml"),
						CreateNamespaceInfo("Avalonia.Markup.Xaml.Templates", "Avalonia.Markup.Xaml"),
						CreateNamespaceInfo("Avalonia.Themes.Fluent", "Avalonia.Themes.Fluent"),
						CreateNamespaceInfo("Avalonia.Svg.Skia", "Svg.Controls.Skia.Avalonia")
					}
				},
				{
					"x",
					new AvaloniaXamlIlXmlNamespaceInfo[0]
				}
			};
		}

		static NamespaceInfo_003A_002FControls_002FToggleSwitch_002Eaxaml()
		{
			Singleton = new NamespaceInfo_003A_002FControls_002FToggleSwitch_002Eaxaml();
		}
	}

	[CompilerGenerated]
	private class XamlClosure_11
	{
		public static object Build_1(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<Styles> context = CreateContext(P_0);
			context.IntermediateRoot = new StackPanel();
			object intermediateRoot = context.IntermediateRoot;
			((ISupportInitialize)intermediateRoot).BeginInit();
			((AvaloniaObject)intermediateRoot).SetValue(StackPanel.OrientationProperty, Orientation.Horizontal, BindingPriority.Template);
			((AvaloniaObject)intermediateRoot).SetValue(StackPanel.SpacingProperty, 8.0, BindingPriority.Template);
			((AvaloniaObject)intermediateRoot).SetValue(Layoutable.VerticalAlignmentProperty, VerticalAlignment.Center, BindingPriority.Template);
			Controls children = ((Panel)intermediateRoot).Children;
			Border border;
			Border border2 = (border = new Border());
			((ISupportInitialize)border2).BeginInit();
			children.Add(border2);
			border.Name = "SwitchTrack";
			object element = border;
			context.AvaloniaNameScope.Register("SwitchTrack", element);
			border.SetValue(Layoutable.WidthProperty, 38.0, BindingPriority.Template);
			border.SetValue(Layoutable.HeightProperty, 22.0, BindingPriority.Template);
			border.SetValue(Border.CornerRadiusProperty, new CornerRadius(11.0, 11.0, 11.0, 11.0), BindingPriority.Template);
			border.SetValue(Border.BackgroundProperty, new ImmutableSolidColorBrush(1392508927u), BindingPriority.Template);
			StyledProperty<Transitions?> transitionsProperty = Animatable.TransitionsProperty;
			Transitions transitions = new Transitions();
			BrushTransition brushTransition = new BrushTransition();
			brushTransition.Property = Border.BackgroundProperty;
			brushTransition.Duration = TimeSpan.FromTicks(1800000L);
			transitions.Add(brushTransition);
			border.SetValue(transitionsProperty, transitions, BindingPriority.Template);
			Canvas canvas;
			Canvas canvas2 = (canvas = new Canvas());
			((ISupportInitialize)canvas2).BeginInit();
			border.Child = canvas2;
			canvas.Name = "PART_KnobsPanel";
			element = canvas;
			context.AvaloniaNameScope.Register("PART_KnobsPanel", element);
			canvas.SetValue(Visual.ClipToBoundsProperty, value: false, BindingPriority.Template);
			canvas.SetValue(Layoutable.UseLayoutRoundingProperty, value: true, BindingPriority.Template);
			Controls children2 = canvas.Children;
			Panel panel;
			Panel panel2 = (panel = new Panel());
			((ISupportInitialize)panel2).BeginInit();
			children2.Add(panel2);
			panel.Name = "PART_MovingKnobs";
			element = panel;
			context.AvaloniaNameScope.Register("PART_MovingKnobs", element);
			panel.SetValue(Layoutable.WidthProperty, 20.0, BindingPriority.Template);
			panel.SetValue(Layoutable.HeightProperty, 20.0, BindingPriority.Template);
			panel.SetValue(Layoutable.MarginProperty, new Thickness(1.0, 1.0, 1.0, 1.0), BindingPriority.Template);
			StyledProperty<Transitions?> transitionsProperty2 = Animatable.TransitionsProperty;
			Transitions transitions2 = new Transitions();
			DoubleTransition doubleTransition = new DoubleTransition();
			doubleTransition.Property = Canvas.LeftProperty;
			doubleTransition.Duration = TimeSpan.FromTicks(1800000L);
			doubleTransition.Easing = Easing.Parse("CubicEaseOut");
			transitions2.Add(doubleTransition);
			panel.SetValue(transitionsProperty2, transitions2, BindingPriority.Template);
			Controls children3 = panel.Children;
			Ellipse ellipse;
			Ellipse ellipse2 = (ellipse = new Ellipse());
			((ISupportInitialize)ellipse2).BeginInit();
			children3.Add(ellipse2);
			ellipse.SetValue(Shape.FillProperty, new ImmutableSolidColorBrush(uint.MaxValue), BindingPriority.Template);
			((ISupportInitialize)ellipse).EndInit();
			Controls children4 = panel.Children;
			Panel panel3;
			Panel panel4 = (panel3 = new Panel());
			((ISupportInitialize)panel4).BeginInit();
			children4.Add(panel4);
			panel3.Name = "PART_SwitchKnob";
			element = panel3;
			context.AvaloniaNameScope.Register("PART_SwitchKnob", element);
			panel3.SetValue(Layoutable.WidthProperty, 16.0, BindingPriority.Template);
			panel3.SetValue(Layoutable.HorizontalAlignmentProperty, HorizontalAlignment.Left, BindingPriority.Template);
			panel3.SetValue(InputElement.IsHitTestVisibleProperty, value: false, BindingPriority.Template);
			((ISupportInitialize)panel3).EndInit();
			((ISupportInitialize)panel).EndInit();
			((ISupportInitialize)canvas).EndInit();
			((ISupportInitialize)border).EndInit();
			Controls children5 = ((Panel)intermediateRoot).Children;
			ContentPresenter contentPresenter;
			ContentPresenter contentPresenter2 = (contentPresenter = new ContentPresenter());
			((ISupportInitialize)contentPresenter2).BeginInit();
			children5.Add(contentPresenter2);
			contentPresenter.SetValue(Layoutable.VerticalAlignmentProperty, VerticalAlignment.Center, BindingPriority.Template);
			CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(contentPresenter, BindingPriority.Template, new TemplateBinding(ContentControl.ContentProperty).ProvideValue());
			contentPresenter.Bind(ContentPresenter.ContentTemplateProperty, new TemplateBinding(ContentControl.ContentTemplateProperty).ProvideValue());
			contentPresenter.Bind(ContentPresenter.ForegroundProperty, new TemplateBinding(TemplatedControl.ForegroundProperty).ProvideValue());
			contentPresenter.Bind(ContentPresenter.FontSizeProperty, new TemplateBinding(TemplatedControl.FontSizeProperty).ProvideValue());
			((ISupportInitialize)contentPresenter).EndInit();
			((ISupportInitialize)intermediateRoot).EndInit();
			return intermediateRoot;
		}

		public static CompiledAvaloniaXaml.XamlIlContext.Context<Styles> CreateContext(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<Styles> context = new CompiledAvaloniaXaml.XamlIlContext.Context<Styles>(P_0, new object[1] { NamespaceInfo_003A_002FControls_002FToggleSwitch_002Eaxaml.Singleton }, "avares://SpaceWalker/Controls/ToggleSwitch.axaml");
			if (P_0 != null)
			{
				object service = P_0.GetService(typeof(IRootObjectProvider));
				if (service != null)
				{
					service = ((IRootObjectProvider)service).RootObject;
					context.RootObject = (Styles)service;
				}
			}
			return context;
		}
	}

	[CompilerGenerated]
	internal class NamespaceInfo_003A_002FThemes_002FColors_002Eaxaml : IAvaloniaXamlIlXmlNamespaceInfoProvider
	{
		private IReadOnlyDictionary<string, IReadOnlyList<AvaloniaXamlIlXmlNamespaceInfo>> _xmlNamespaces;

		public static IAvaloniaXamlIlXmlNamespaceInfoProvider Singleton;

		public virtual IReadOnlyDictionary<string, IReadOnlyList<AvaloniaXamlIlXmlNamespaceInfo>> XmlNamespaces
		{
			get
			{
				if (_xmlNamespaces == null)
				{
					_xmlNamespaces = CreateNamespaces();
				}
				return _xmlNamespaces;
			}
		}

		private static AvaloniaXamlIlXmlNamespaceInfo CreateNamespaceInfo(string P_0, string P_1)
		{
			return new AvaloniaXamlIlXmlNamespaceInfo
			{
				ClrNamespace = P_0,
				ClrAssemblyName = P_1
			};
		}

		private static IReadOnlyDictionary<string, IReadOnlyList<AvaloniaXamlIlXmlNamespaceInfo>> CreateNamespaces()
		{
			return new Dictionary<string, IReadOnlyList<AvaloniaXamlIlXmlNamespaceInfo>>(3)
			{
				{
					"",
					new AvaloniaXamlIlXmlNamespaceInfo[37]
					{
						CreateNamespaceInfo("Avalonia", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Animation", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Animation.Easings", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Controls", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Data", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Data.Converters", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Input", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Input.GestureRecognizers", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Input.TextInput", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Layout", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.LogicalTree", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Media", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Media.Imaging", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Media.Transformation", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Styling", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Controls", "Avalonia.Controls.DataGrid"),
						CreateNamespaceInfo("Avalonia.Controls.Collections", "Avalonia.Controls.DataGrid"),
						CreateNamespaceInfo("Avalonia.Controls.Primitives", "Avalonia.Controls.DataGrid"),
						CreateNamespaceInfo("Avalonia", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Automation", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Embedding", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Presenters", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Primitives", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Shapes", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Templates", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Notifications", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Chrome", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Documents", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Fonts.Inter", "Avalonia.Fonts.Inter"),
						CreateNamespaceInfo("Avalonia.Data", "Avalonia.Markup"),
						CreateNamespaceInfo("Avalonia.Markup.Data", "Avalonia.Markup"),
						CreateNamespaceInfo("Avalonia.Markup.Xaml.MarkupExtensions", "Avalonia.Markup.Xaml"),
						CreateNamespaceInfo("Avalonia.Markup.Xaml.Styling", "Avalonia.Markup.Xaml"),
						CreateNamespaceInfo("Avalonia.Markup.Xaml.Templates", "Avalonia.Markup.Xaml"),
						CreateNamespaceInfo("Avalonia.Themes.Fluent", "Avalonia.Themes.Fluent"),
						CreateNamespaceInfo("Avalonia.Svg.Skia", "Svg.Controls.Skia.Avalonia")
					}
				},
				{
					"x",
					new AvaloniaXamlIlXmlNamespaceInfo[0]
				},
				{
					"helper",
					new AvaloniaXamlIlXmlNamespaceInfo[1] { CreateNamespaceInfo("SpaceWalker.Helper", null) }
				}
			};
		}

		static NamespaceInfo_003A_002FThemes_002FColors_002Eaxaml()
		{
			Singleton = new NamespaceInfo_003A_002FThemes_002FColors_002Eaxaml();
		}
	}

	[CompilerGenerated]
	private class XamlClosure_12
	{
		public static object Build_1(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<ResourceDictionary> context = CreateContext(P_0);
			return new SolidColorBrush
			{
				Color = Color.FromUInt32(4279398911u)
			};
		}

		public static CompiledAvaloniaXaml.XamlIlContext.Context<ResourceDictionary> CreateContext(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<ResourceDictionary> context = new CompiledAvaloniaXaml.XamlIlContext.Context<ResourceDictionary>(P_0, new object[1] { NamespaceInfo_003A_002FThemes_002FColors_002Eaxaml.Singleton }, "avares://SpaceWalker/Themes/Colors.axaml");
			if (P_0 != null)
			{
				object service = P_0.GetService(typeof(IRootObjectProvider));
				if (service != null)
				{
					service = ((IRootObjectProvider)service).RootObject;
					context.RootObject = (ResourceDictionary)service;
				}
			}
			return context;
		}

		public static object Build_2(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<ResourceDictionary> context = CreateContext(P_0);
			return new SolidColorBrush
			{
				Color = Color.FromUInt32(4281370879u)
			};
		}

		public static object Build_3(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<ResourceDictionary> context = CreateContext(P_0);
			return new SolidColorBrush
			{
				Color = Color.FromUInt32(4279001056u)
			};
		}

		public static object Build_4(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<ResourceDictionary> context = CreateContext(P_0);
			return new SolidColorBrush
			{
				Color = Color.FromUInt32(4279398911u)
			};
		}

		public static object Build_5(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<ResourceDictionary> context = CreateContext(P_0);
			return new SolidColorBrush
			{
				Color = Color.FromUInt32(4281370879u)
			};
		}

		public static object Build_6(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<ResourceDictionary> context = CreateContext(P_0);
			return new SolidColorBrush
			{
				Color = Color.FromUInt32(4279001056u)
			};
		}

		public static object Build_7(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<ResourceDictionary> context = CreateContext(P_0);
			return new SolidColorBrush
			{
				Color = Color.FromUInt32(1308622847u)
			};
		}

		public static object Build_8(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<ResourceDictionary> context = CreateContext(P_0);
			return new SolidColorBrush
			{
				Color = Color.FromUInt32(1888193419u)
			};
		}

		public static object Build_9(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<ResourceDictionary> context = CreateContext(P_0);
			return new SolidColorBrush
			{
				Color = Color.FromUInt32(856846847u)
			};
		}

		public static object Build_10(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<ResourceDictionary> context = CreateContext(P_0);
			return new SolidColorBrush
			{
				Color = Color.FromUInt32(4279398911u)
			};
		}

		public static object Build_11(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<ResourceDictionary> context = CreateContext(P_0);
			return new SolidColorBrush
			{
				Color = Color.FromUInt32(4279398911u)
			};
		}

		public static object Build_12(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<ResourceDictionary> context = CreateContext(P_0);
			return new SolidColorBrush
			{
				Color = Color.FromUInt32(856846847u)
			};
		}

		public static object Build_13(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<ResourceDictionary> context = CreateContext(P_0);
			return new SolidColorBrush
			{
				Color = Color.FromUInt32(4279398911u)
			};
		}

		public static object Build_14(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<ResourceDictionary> context = CreateContext(P_0);
			return new SolidColorBrush
			{
				Color = Color.FromUInt32(4281370879u)
			};
		}

		public static object Build_15(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<ResourceDictionary> context = CreateContext(P_0);
			return new SolidColorBrush
			{
				Color = Color.FromUInt32(4279001056u)
			};
		}

		public static object Build_16(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<ResourceDictionary> context = CreateContext(P_0);
			return new FontFamily(((IUriContext)context).BaseUri, "avares://SpaceWalker/Assets/Fonts#Season Sans, fonts:swsc#Noto Sans SC, Noto Sans JP, Noto Sans TC, Noto Sans Thai");
		}

		public static object Build_17(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<ResourceDictionary> context = CreateContext(P_0);
			return new FontFamily(((IUriContext)context).BaseUri, "avares://SpaceWalker/Assets/Fonts#Season Sans, fonts:swsc#Noto Sans SC, Noto Sans JP, Noto Sans TC, Noto Sans Thai");
		}

		public static object Build_18(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<ResourceDictionary> context = CreateContext(P_0);
			return new SolidColorBrush
			{
				Color = Color.FromUInt32(4293918720u)
			};
		}

		public static object Build_19(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<ResourceDictionary> context = CreateContext(P_0);
			return new SolidColorBrush
			{
				Color = Color.FromUInt32(4294914867u)
			};
		}

		public static object Build_20(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<ResourceDictionary> context = CreateContext(P_0);
			return new SolidColorBrush
			{
				Color = Color.FromUInt32(4290772992u)
			};
		}

		public static object Build_21(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<ResourceDictionary> context = CreateContext(P_0);
			LinearGradientBrush linearGradientBrush = new LinearGradientBrush();
			linearGradientBrush.StartPoint = new RelativePoint(0.0, 0.5, RelativeUnit.Relative);
			linearGradientBrush.EndPoint = new RelativePoint(1.0, 0.5, RelativeUnit.Relative);
			GradientStops gradientStops = linearGradientBrush.GradientStops;
			GradientStop gradientStop = new GradientStop();
			gradientStop.Color = Color.FromUInt32(4287234048u);
			gradientStop.Offset = 0.0;
			gradientStops.Add(gradientStop);
			GradientStops gradientStops2 = linearGradientBrush.GradientStops;
			GradientStop gradientStop2 = new GradientStop();
			gradientStop2.Color = Color.FromUInt32(4293918720u);
			gradientStop2.Offset = 1.0;
			gradientStops2.Add(gradientStop2);
			return linearGradientBrush;
		}

		public static object Build_22(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<ResourceDictionary> context = CreateContext(P_0);
			LinearGradientBrush linearGradientBrush = new LinearGradientBrush();
			linearGradientBrush.StartPoint = new RelativePoint(0.0, 0.5, RelativeUnit.Relative);
			linearGradientBrush.EndPoint = new RelativePoint(1.0, 0.5, RelativeUnit.Relative);
			GradientStops gradientStops = linearGradientBrush.GradientStops;
			GradientStop gradientStop = new GradientStop();
			gradientStop.Color = Color.FromUInt32(4288877074u);
			gradientStop.Offset = 0.0;
			gradientStops.Add(gradientStop);
			GradientStops gradientStops2 = linearGradientBrush.GradientStops;
			GradientStop gradientStop2 = new GradientStop();
			gradientStop2.Color = Color.FromUInt32(4294908442u);
			gradientStop2.Offset = 1.0;
			gradientStops2.Add(gradientStop2);
			return linearGradientBrush;
		}

		public static object Build_23(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<ResourceDictionary> context = CreateContext(P_0);
			LinearGradientBrush linearGradientBrush = new LinearGradientBrush();
			linearGradientBrush.StartPoint = new RelativePoint(0.0, 0.5, RelativeUnit.Relative);
			linearGradientBrush.EndPoint = new RelativePoint(1.0, 0.5, RelativeUnit.Relative);
			GradientStops gradientStops = linearGradientBrush.GradientStops;
			GradientStop gradientStop = new GradientStop();
			gradientStop.Color = Color.FromUInt32(4285399040u);
			gradientStop.Offset = 0.0;
			gradientStops.Add(gradientStop);
			GradientStops gradientStops2 = linearGradientBrush.GradientStops;
			GradientStop gradientStop2 = new GradientStop();
			gradientStop2.Color = Color.FromUInt32(4290772992u);
			gradientStop2.Offset = 1.0;
			gradientStops2.Add(gradientStop2);
			return linearGradientBrush;
		}

		public static object Build_24(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<ResourceDictionary> context = CreateContext(P_0);
			return new SolidColorBrush
			{
				Color = Color.FromUInt32(872415231u)
			};
		}

		public static object Build_25(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<ResourceDictionary> context = CreateContext(P_0);
			return new SolidColorBrush
			{
				Color = Color.FromUInt32(704643071u)
			};
		}

		public static object Build_26(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<ResourceDictionary> context = CreateContext(P_0);
			return new SolidColorBrush
			{
				Color = Color.FromUInt32(1375731711u)
			};
		}

		public static object Build_27(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<ResourceDictionary> context = CreateContext(P_0);
			return new SolidColorBrush
			{
				Color = Color.FromUInt32(uint.MaxValue)
			};
		}

		public static object Build_28(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<ResourceDictionary> context = CreateContext(P_0);
			return new SolidColorBrush
			{
				Color = Color.FromUInt32(4293918720u)
			};
		}

		public static object Build_29(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<ResourceDictionary> context = CreateContext(P_0);
			return new SolidColorBrush
			{
				Color = Color.FromUInt32(871366656u)
			};
		}

		public static object Build_30(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<ResourceDictionary> context = CreateContext(P_0);
			return new SolidColorBrush
			{
				Color = Color.FromUInt32(4293918720u)
			};
		}

		public static object Build_31(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<ResourceDictionary> context = CreateContext(P_0);
			return new SolidColorBrush
			{
				Color = Color.FromUInt32(4294914867u)
			};
		}

		public static object Build_32(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<ResourceDictionary> context = CreateContext(P_0);
			return new SolidColorBrush
			{
				Color = Color.FromUInt32(4290772992u)
			};
		}

		public static object Build_33(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<ResourceDictionary> context = CreateContext(P_0);
			return new FontFamily(((IUriContext)context).BaseUri, "avares://SpaceWalker/Assets/Fonts#Chaco, fonts:swsc#Noto Sans SC, Noto Sans JP, Noto Sans TC, Noto Sans Thai");
		}

		public static object Build_34(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<ResourceDictionary> context = CreateContext(P_0);
			return new FontFamily(((IUriContext)context).BaseUri, "avares://SpaceWalker/Assets/Fonts#Alverata CYR Medium, fonts:swsc#Noto Sans SC, Noto Sans JP, Noto Sans TC, Noto Sans Thai");
		}

		public static object Build_35(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<ResourceDictionary> context = CreateContext(P_0);
			return new SolidColorBrush
			{
				Color = Color.FromUInt32(1308622847u)
			};
		}

		public static object Build_36(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<ResourceDictionary> context = CreateContext(P_0);
			return new SolidColorBrush
			{
				Color = Color.FromUInt32(822083583u)
			};
		}

		public static object Build_37(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<ResourceDictionary> context = CreateContext(P_0);
			return new SolidColorBrush
			{
				Color = Color.FromUInt32(536870912u)
			};
		}

		public static object Build_38(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<ResourceDictionary> context = CreateContext(P_0);
			return new SolidColorBrush
			{
				Color = Color.FromUInt32(721420288u)
			};
		}

		public static object Build_39(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<ResourceDictionary> context = CreateContext(P_0);
			return new SolidColorBrush
			{
				Color = Color.FromUInt32(3875536895u)
			};
		}

		public static object Build_40(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<ResourceDictionary> context = CreateContext(P_0);
			return new SolidColorBrush
			{
				Color = Color.FromUInt32(3875536895u)
			};
		}

		public static object Build_41(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<ResourceDictionary> context = CreateContext(P_0);
			return new SolidColorBrush
			{
				Color = Color.FromUInt32(3875536895u)
			};
		}

		public static object Build_42(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<ResourceDictionary> context = CreateContext(P_0);
			return new SolidColorBrush
			{
				Color = Color.FromUInt32(uint.MaxValue)
			};
		}

		public static object Build_43(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<ResourceDictionary> context = CreateContext(P_0);
			return new SolidColorBrush
			{
				Color = Color.FromUInt32(uint.MaxValue)
			};
		}

		public static object Build_44(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<ResourceDictionary> context = CreateContext(P_0);
			return new SolidColorBrush
			{
				Color = Color.FromUInt32(uint.MaxValue)
			};
		}

		public static object Build_45(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<ResourceDictionary> context = CreateContext(P_0);
			return new SolidColorBrush
			{
				Color = Color.FromUInt32(uint.MaxValue)
			};
		}

		public static object Build_46(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<ResourceDictionary> context = CreateContext(P_0);
			return new SolidColorBrush
			{
				Color = Color.FromUInt32(872415231u)
			};
		}

		public static object Build_47(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<ResourceDictionary> context = CreateContext(P_0);
			return new SolidColorBrush
			{
				Color = Color.FromUInt32(872415231u)
			};
		}

		public static object Build_48(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<ResourceDictionary> context = CreateContext(P_0);
			return new SolidColorBrush
			{
				Color = Color.FromUInt32(2164260863u)
			};
		}

		public static object Build_49(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<ResourceDictionary> context = CreateContext(P_0);
			return new SolidColorBrush
			{
				Color = Color.FromUInt32(2164260863u)
			};
		}

		public static object Build_50(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<ResourceDictionary> context = CreateContext(P_0);
			return new SolidColorBrush
			{
				Color = Color.FromUInt32(2164260863u)
			};
		}

		public static object Build_51(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<ResourceDictionary> context = CreateContext(P_0);
			return new SolidColorBrush
			{
				Color = Color.FromUInt32(uint.MaxValue)
			};
		}

		public static object Build_52(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<ResourceDictionary> context = CreateContext(P_0);
			return new SolidColorBrush
			{
				Color = Color.FromUInt32(uint.MaxValue)
			};
		}

		public static object Build_53(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<ResourceDictionary> context = CreateContext(P_0);
			return new SolidColorBrush
			{
				Color = Color.FromUInt32(uint.MaxValue)
			};
		}

		public static object Build_54(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<ResourceDictionary> context = CreateContext(P_0);
			return new SolidColorBrush
			{
				Color = Color.FromUInt32(4281547335u)
			};
		}

		public static object Build_55(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<ResourceDictionary> context = CreateContext(P_0);
			return new SolidColorBrush
			{
				Color = Color.FromUInt32(822083583u)
			};
		}

		public static object Build_56(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<ResourceDictionary> context = CreateContext(P_0);
			return new SolidColorBrush
			{
				Color = Color.FromUInt32(uint.MaxValue)
			};
		}
	}

	[CompilerGenerated]
	internal class NamespaceInfo_003A_002FViews_002FConnectView_002Eaxaml : IAvaloniaXamlIlXmlNamespaceInfoProvider
	{
		private IReadOnlyDictionary<string, IReadOnlyList<AvaloniaXamlIlXmlNamespaceInfo>> _xmlNamespaces;

		public static IAvaloniaXamlIlXmlNamespaceInfoProvider Singleton;

		public virtual IReadOnlyDictionary<string, IReadOnlyList<AvaloniaXamlIlXmlNamespaceInfo>> XmlNamespaces
		{
			get
			{
				if (_xmlNamespaces == null)
				{
					_xmlNamespaces = CreateNamespaces();
				}
				return _xmlNamespaces;
			}
		}

		private static AvaloniaXamlIlXmlNamespaceInfo CreateNamespaceInfo(string P_0, string P_1)
		{
			return new AvaloniaXamlIlXmlNamespaceInfo
			{
				ClrNamespace = P_0,
				ClrAssemblyName = P_1
			};
		}

		private static IReadOnlyDictionary<string, IReadOnlyList<AvaloniaXamlIlXmlNamespaceInfo>> CreateNamespaces()
		{
			return new Dictionary<string, IReadOnlyList<AvaloniaXamlIlXmlNamespaceInfo>>(6)
			{
				{
					"",
					new AvaloniaXamlIlXmlNamespaceInfo[37]
					{
						CreateNamespaceInfo("Avalonia", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Animation", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Animation.Easings", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Controls", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Data", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Data.Converters", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Input", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Input.GestureRecognizers", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Input.TextInput", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Layout", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.LogicalTree", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Media", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Media.Imaging", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Media.Transformation", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Styling", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Controls", "Avalonia.Controls.DataGrid"),
						CreateNamespaceInfo("Avalonia.Controls.Collections", "Avalonia.Controls.DataGrid"),
						CreateNamespaceInfo("Avalonia.Controls.Primitives", "Avalonia.Controls.DataGrid"),
						CreateNamespaceInfo("Avalonia", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Automation", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Embedding", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Presenters", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Primitives", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Shapes", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Templates", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Notifications", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Chrome", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Documents", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Fonts.Inter", "Avalonia.Fonts.Inter"),
						CreateNamespaceInfo("Avalonia.Data", "Avalonia.Markup"),
						CreateNamespaceInfo("Avalonia.Markup.Data", "Avalonia.Markup"),
						CreateNamespaceInfo("Avalonia.Markup.Xaml.MarkupExtensions", "Avalonia.Markup.Xaml"),
						CreateNamespaceInfo("Avalonia.Markup.Xaml.Styling", "Avalonia.Markup.Xaml"),
						CreateNamespaceInfo("Avalonia.Markup.Xaml.Templates", "Avalonia.Markup.Xaml"),
						CreateNamespaceInfo("Avalonia.Themes.Fluent", "Avalonia.Themes.Fluent"),
						CreateNamespaceInfo("Avalonia.Svg.Skia", "Svg.Controls.Skia.Avalonia")
					}
				},
				{
					"x",
					new AvaloniaXamlIlXmlNamespaceInfo[0]
				},
				{
					"d",
					new AvaloniaXamlIlXmlNamespaceInfo[0]
				},
				{
					"mc",
					new AvaloniaXamlIlXmlNamespaceInfo[0]
				},
				{
					"controls",
					new AvaloniaXamlIlXmlNamespaceInfo[1] { CreateNamespaceInfo("SpaceWalker.Controls", null) }
				},
				{
					"i18N",
					new AvaloniaXamlIlXmlNamespaceInfo[1] { CreateNamespaceInfo("SpaceWalker.Assets.Languages", null) }
				}
			};
		}

		static NamespaceInfo_003A_002FViews_002FConnectView_002Eaxaml()
		{
			Singleton = new NamespaceInfo_003A_002FViews_002FConnectView_002Eaxaml();
		}
	}

	[CompilerGenerated]
	internal class NamespaceInfo_003A_002FViews_002FDesktopView_002Eaxaml : IAvaloniaXamlIlXmlNamespaceInfoProvider
	{
		private IReadOnlyDictionary<string, IReadOnlyList<AvaloniaXamlIlXmlNamespaceInfo>> _xmlNamespaces;

		public static IAvaloniaXamlIlXmlNamespaceInfoProvider Singleton;

		public virtual IReadOnlyDictionary<string, IReadOnlyList<AvaloniaXamlIlXmlNamespaceInfo>> XmlNamespaces
		{
			get
			{
				if (_xmlNamespaces == null)
				{
					_xmlNamespaces = CreateNamespaces();
				}
				return _xmlNamespaces;
			}
		}

		private static AvaloniaXamlIlXmlNamespaceInfo CreateNamespaceInfo(string P_0, string P_1)
		{
			return new AvaloniaXamlIlXmlNamespaceInfo
			{
				ClrNamespace = P_0,
				ClrAssemblyName = P_1
			};
		}

		private static IReadOnlyDictionary<string, IReadOnlyList<AvaloniaXamlIlXmlNamespaceInfo>> CreateNamespaces()
		{
			return new Dictionary<string, IReadOnlyList<AvaloniaXamlIlXmlNamespaceInfo>>(10)
			{
				{
					"",
					new AvaloniaXamlIlXmlNamespaceInfo[37]
					{
						CreateNamespaceInfo("Avalonia", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Animation", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Animation.Easings", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Controls", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Data", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Data.Converters", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Input", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Input.GestureRecognizers", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Input.TextInput", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Layout", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.LogicalTree", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Media", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Media.Imaging", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Media.Transformation", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Styling", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Controls", "Avalonia.Controls.DataGrid"),
						CreateNamespaceInfo("Avalonia.Controls.Collections", "Avalonia.Controls.DataGrid"),
						CreateNamespaceInfo("Avalonia.Controls.Primitives", "Avalonia.Controls.DataGrid"),
						CreateNamespaceInfo("Avalonia", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Automation", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Embedding", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Presenters", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Primitives", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Shapes", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Templates", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Notifications", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Chrome", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Documents", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Fonts.Inter", "Avalonia.Fonts.Inter"),
						CreateNamespaceInfo("Avalonia.Data", "Avalonia.Markup"),
						CreateNamespaceInfo("Avalonia.Markup.Data", "Avalonia.Markup"),
						CreateNamespaceInfo("Avalonia.Markup.Xaml.MarkupExtensions", "Avalonia.Markup.Xaml"),
						CreateNamespaceInfo("Avalonia.Markup.Xaml.Styling", "Avalonia.Markup.Xaml"),
						CreateNamespaceInfo("Avalonia.Markup.Xaml.Templates", "Avalonia.Markup.Xaml"),
						CreateNamespaceInfo("Avalonia.Themes.Fluent", "Avalonia.Themes.Fluent"),
						CreateNamespaceInfo("Avalonia.Svg.Skia", "Svg.Controls.Skia.Avalonia")
					}
				},
				{
					"x",
					new AvaloniaXamlIlXmlNamespaceInfo[0]
				},
				{
					"d",
					new AvaloniaXamlIlXmlNamespaceInfo[0]
				},
				{
					"mc",
					new AvaloniaXamlIlXmlNamespaceInfo[0]
				},
				{
					"i18N",
					new AvaloniaXamlIlXmlNamespaceInfo[1] { CreateNamespaceInfo("SpaceWalker.Assets.Languages", null) }
				},
				{
					"swcvt",
					new AvaloniaXamlIlXmlNamespaceInfo[1] { CreateNamespaceInfo("SpaceWalker.Converters", null) }
				},
				{
					"swipc",
					new AvaloniaXamlIlXmlNamespaceInfo[1] { CreateNamespaceInfo("SpaceWalker.Ipc", null) }
				},
				{
					"controls",
					new AvaloniaXamlIlXmlNamespaceInfo[1] { CreateNamespaceInfo("SpaceWalker.Controls", null) }
				},
				{
					"sys",
					new AvaloniaXamlIlXmlNamespaceInfo[1] { CreateNamespaceInfo("System", null) }
				},
				{
					"helper",
					new AvaloniaXamlIlXmlNamespaceInfo[1] { CreateNamespaceInfo("SpaceWalker.Helper", null) }
				}
			};
		}

		static NamespaceInfo_003A_002FViews_002FDesktopView_002Eaxaml()
		{
			Singleton = new NamespaceInfo_003A_002FViews_002FDesktopView_002Eaxaml();
		}
	}

	[CompilerGenerated]
	internal class NamespaceInfo_003A_002FViews_002FLayoutView_002Eaxaml : IAvaloniaXamlIlXmlNamespaceInfoProvider
	{
		private IReadOnlyDictionary<string, IReadOnlyList<AvaloniaXamlIlXmlNamespaceInfo>> _xmlNamespaces;

		public static IAvaloniaXamlIlXmlNamespaceInfoProvider Singleton;

		public virtual IReadOnlyDictionary<string, IReadOnlyList<AvaloniaXamlIlXmlNamespaceInfo>> XmlNamespaces
		{
			get
			{
				if (_xmlNamespaces == null)
				{
					_xmlNamespaces = CreateNamespaces();
				}
				return _xmlNamespaces;
			}
		}

		private static AvaloniaXamlIlXmlNamespaceInfo CreateNamespaceInfo(string P_0, string P_1)
		{
			return new AvaloniaXamlIlXmlNamespaceInfo
			{
				ClrNamespace = P_0,
				ClrAssemblyName = P_1
			};
		}

		private static IReadOnlyDictionary<string, IReadOnlyList<AvaloniaXamlIlXmlNamespaceInfo>> CreateNamespaces()
		{
			return new Dictionary<string, IReadOnlyList<AvaloniaXamlIlXmlNamespaceInfo>>(11)
			{
				{
					"",
					new AvaloniaXamlIlXmlNamespaceInfo[37]
					{
						CreateNamespaceInfo("Avalonia", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Animation", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Animation.Easings", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Controls", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Data", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Data.Converters", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Input", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Input.GestureRecognizers", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Input.TextInput", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Layout", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.LogicalTree", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Media", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Media.Imaging", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Media.Transformation", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Styling", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Controls", "Avalonia.Controls.DataGrid"),
						CreateNamespaceInfo("Avalonia.Controls.Collections", "Avalonia.Controls.DataGrid"),
						CreateNamespaceInfo("Avalonia.Controls.Primitives", "Avalonia.Controls.DataGrid"),
						CreateNamespaceInfo("Avalonia", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Automation", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Embedding", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Presenters", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Primitives", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Shapes", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Templates", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Notifications", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Chrome", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Documents", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Fonts.Inter", "Avalonia.Fonts.Inter"),
						CreateNamespaceInfo("Avalonia.Data", "Avalonia.Markup"),
						CreateNamespaceInfo("Avalonia.Markup.Data", "Avalonia.Markup"),
						CreateNamespaceInfo("Avalonia.Markup.Xaml.MarkupExtensions", "Avalonia.Markup.Xaml"),
						CreateNamespaceInfo("Avalonia.Markup.Xaml.Styling", "Avalonia.Markup.Xaml"),
						CreateNamespaceInfo("Avalonia.Markup.Xaml.Templates", "Avalonia.Markup.Xaml"),
						CreateNamespaceInfo("Avalonia.Themes.Fluent", "Avalonia.Themes.Fluent"),
						CreateNamespaceInfo("Avalonia.Svg.Skia", "Svg.Controls.Skia.Avalonia")
					}
				},
				{
					"x",
					new AvaloniaXamlIlXmlNamespaceInfo[0]
				},
				{
					"d",
					new AvaloniaXamlIlXmlNamespaceInfo[0]
				},
				{
					"mc",
					new AvaloniaXamlIlXmlNamespaceInfo[0]
				},
				{
					"sys",
					new AvaloniaXamlIlXmlNamespaceInfo[1] { CreateNamespaceInfo("System", null) }
				},
				{
					"i18N",
					new AvaloniaXamlIlXmlNamespaceInfo[1] { CreateNamespaceInfo("SpaceWalker.Assets.Languages", null) }
				},
				{
					"cvt",
					new AvaloniaXamlIlXmlNamespaceInfo[1] { CreateNamespaceInfo("SpaceWalker.Converters", null) }
				},
				{
					"vm",
					new AvaloniaXamlIlXmlNamespaceInfo[1] { CreateNamespaceInfo("SpaceWalker.ViewModels", null) }
				},
				{
					"vcl",
					new AvaloniaXamlIlXmlNamespaceInfo[1] { CreateNamespaceInfo("VitureCommonLibrary", null) }
				},
				{
					"swcvt",
					new AvaloniaXamlIlXmlNamespaceInfo[1] { CreateNamespaceInfo("SpaceWalker.Converters", null) }
				},
				{
					"helper",
					new AvaloniaXamlIlXmlNamespaceInfo[1] { CreateNamespaceInfo("SpaceWalker.Helper", null) }
				}
			};
		}

		static NamespaceInfo_003A_002FViews_002FLayoutView_002Eaxaml()
		{
			Singleton = new NamespaceInfo_003A_002FViews_002FLayoutView_002Eaxaml();
		}
	}

	[CompilerGenerated]
	internal class NamespaceInfo_003A_002FViews_002FLoadingView_002Eaxaml : IAvaloniaXamlIlXmlNamespaceInfoProvider
	{
		private IReadOnlyDictionary<string, IReadOnlyList<AvaloniaXamlIlXmlNamespaceInfo>> _xmlNamespaces;

		public static IAvaloniaXamlIlXmlNamespaceInfoProvider Singleton;

		public virtual IReadOnlyDictionary<string, IReadOnlyList<AvaloniaXamlIlXmlNamespaceInfo>> XmlNamespaces
		{
			get
			{
				if (_xmlNamespaces == null)
				{
					_xmlNamespaces = CreateNamespaces();
				}
				return _xmlNamespaces;
			}
		}

		private static AvaloniaXamlIlXmlNamespaceInfo CreateNamespaceInfo(string P_0, string P_1)
		{
			return new AvaloniaXamlIlXmlNamespaceInfo
			{
				ClrNamespace = P_0,
				ClrAssemblyName = P_1
			};
		}

		private static IReadOnlyDictionary<string, IReadOnlyList<AvaloniaXamlIlXmlNamespaceInfo>> CreateNamespaces()
		{
			return new Dictionary<string, IReadOnlyList<AvaloniaXamlIlXmlNamespaceInfo>>(3)
			{
				{
					"",
					new AvaloniaXamlIlXmlNamespaceInfo[37]
					{
						CreateNamespaceInfo("Avalonia", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Animation", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Animation.Easings", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Controls", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Data", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Data.Converters", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Input", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Input.GestureRecognizers", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Input.TextInput", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Layout", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.LogicalTree", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Media", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Media.Imaging", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Media.Transformation", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Styling", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Controls", "Avalonia.Controls.DataGrid"),
						CreateNamespaceInfo("Avalonia.Controls.Collections", "Avalonia.Controls.DataGrid"),
						CreateNamespaceInfo("Avalonia.Controls.Primitives", "Avalonia.Controls.DataGrid"),
						CreateNamespaceInfo("Avalonia", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Automation", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Embedding", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Presenters", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Primitives", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Shapes", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Templates", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Notifications", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Chrome", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Documents", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Fonts.Inter", "Avalonia.Fonts.Inter"),
						CreateNamespaceInfo("Avalonia.Data", "Avalonia.Markup"),
						CreateNamespaceInfo("Avalonia.Markup.Data", "Avalonia.Markup"),
						CreateNamespaceInfo("Avalonia.Markup.Xaml.MarkupExtensions", "Avalonia.Markup.Xaml"),
						CreateNamespaceInfo("Avalonia.Markup.Xaml.Styling", "Avalonia.Markup.Xaml"),
						CreateNamespaceInfo("Avalonia.Markup.Xaml.Templates", "Avalonia.Markup.Xaml"),
						CreateNamespaceInfo("Avalonia.Themes.Fluent", "Avalonia.Themes.Fluent"),
						CreateNamespaceInfo("Avalonia.Svg.Skia", "Svg.Controls.Skia.Avalonia")
					}
				},
				{
					"x",
					new AvaloniaXamlIlXmlNamespaceInfo[0]
				},
				{
					"lang",
					new AvaloniaXamlIlXmlNamespaceInfo[1] { CreateNamespaceInfo("SpaceWalker.Assets.Languages", "SpaceWalker") }
				}
			};
		}

		static NamespaceInfo_003A_002FViews_002FLoadingView_002Eaxaml()
		{
			Singleton = new NamespaceInfo_003A_002FViews_002FLoadingView_002Eaxaml();
		}
	}

	[CompilerGenerated]
	internal class NamespaceInfo_003A_002FViews_002FLoadingWindow_002Eaxaml : IAvaloniaXamlIlXmlNamespaceInfoProvider
	{
		private IReadOnlyDictionary<string, IReadOnlyList<AvaloniaXamlIlXmlNamespaceInfo>> _xmlNamespaces;

		public static IAvaloniaXamlIlXmlNamespaceInfoProvider Singleton;

		public virtual IReadOnlyDictionary<string, IReadOnlyList<AvaloniaXamlIlXmlNamespaceInfo>> XmlNamespaces
		{
			get
			{
				if (_xmlNamespaces == null)
				{
					_xmlNamespaces = CreateNamespaces();
				}
				return _xmlNamespaces;
			}
		}

		private static AvaloniaXamlIlXmlNamespaceInfo CreateNamespaceInfo(string P_0, string P_1)
		{
			return new AvaloniaXamlIlXmlNamespaceInfo
			{
				ClrNamespace = P_0,
				ClrAssemblyName = P_1
			};
		}

		private static IReadOnlyDictionary<string, IReadOnlyList<AvaloniaXamlIlXmlNamespaceInfo>> CreateNamespaces()
		{
			return new Dictionary<string, IReadOnlyList<AvaloniaXamlIlXmlNamespaceInfo>>(5)
			{
				{
					"",
					new AvaloniaXamlIlXmlNamespaceInfo[37]
					{
						CreateNamespaceInfo("Avalonia", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Animation", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Animation.Easings", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Controls", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Data", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Data.Converters", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Input", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Input.GestureRecognizers", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Input.TextInput", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Layout", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.LogicalTree", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Media", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Media.Imaging", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Media.Transformation", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Styling", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Controls", "Avalonia.Controls.DataGrid"),
						CreateNamespaceInfo("Avalonia.Controls.Collections", "Avalonia.Controls.DataGrid"),
						CreateNamespaceInfo("Avalonia.Controls.Primitives", "Avalonia.Controls.DataGrid"),
						CreateNamespaceInfo("Avalonia", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Automation", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Embedding", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Presenters", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Primitives", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Shapes", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Templates", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Notifications", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Chrome", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Documents", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Fonts.Inter", "Avalonia.Fonts.Inter"),
						CreateNamespaceInfo("Avalonia.Data", "Avalonia.Markup"),
						CreateNamespaceInfo("Avalonia.Markup.Data", "Avalonia.Markup"),
						CreateNamespaceInfo("Avalonia.Markup.Xaml.MarkupExtensions", "Avalonia.Markup.Xaml"),
						CreateNamespaceInfo("Avalonia.Markup.Xaml.Styling", "Avalonia.Markup.Xaml"),
						CreateNamespaceInfo("Avalonia.Markup.Xaml.Templates", "Avalonia.Markup.Xaml"),
						CreateNamespaceInfo("Avalonia.Themes.Fluent", "Avalonia.Themes.Fluent"),
						CreateNamespaceInfo("Avalonia.Svg.Skia", "Svg.Controls.Skia.Avalonia")
					}
				},
				{
					"x",
					new AvaloniaXamlIlXmlNamespaceInfo[0]
				},
				{
					"d",
					new AvaloniaXamlIlXmlNamespaceInfo[0]
				},
				{
					"mc",
					new AvaloniaXamlIlXmlNamespaceInfo[0]
				},
				{
					"lang",
					new AvaloniaXamlIlXmlNamespaceInfo[1] { CreateNamespaceInfo("SpaceWalker.Assets.Languages", "SpaceWalker") }
				}
			};
		}

		static NamespaceInfo_003A_002FViews_002FLoadingWindow_002Eaxaml()
		{
			Singleton = new NamespaceInfo_003A_002FViews_002FLoadingWindow_002Eaxaml();
		}
	}

	[CompilerGenerated]
	internal class NamespaceInfo_003A_002FViews_002FMainWindow2_002Eaxaml : IAvaloniaXamlIlXmlNamespaceInfoProvider
	{
		private IReadOnlyDictionary<string, IReadOnlyList<AvaloniaXamlIlXmlNamespaceInfo>> _xmlNamespaces;

		public static IAvaloniaXamlIlXmlNamespaceInfoProvider Singleton;

		public virtual IReadOnlyDictionary<string, IReadOnlyList<AvaloniaXamlIlXmlNamespaceInfo>> XmlNamespaces
		{
			get
			{
				if (_xmlNamespaces == null)
				{
					_xmlNamespaces = CreateNamespaces();
				}
				return _xmlNamespaces;
			}
		}

		private static AvaloniaXamlIlXmlNamespaceInfo CreateNamespaceInfo(string P_0, string P_1)
		{
			return new AvaloniaXamlIlXmlNamespaceInfo
			{
				ClrNamespace = P_0,
				ClrAssemblyName = P_1
			};
		}

		private static IReadOnlyDictionary<string, IReadOnlyList<AvaloniaXamlIlXmlNamespaceInfo>> CreateNamespaces()
		{
			return new Dictionary<string, IReadOnlyList<AvaloniaXamlIlXmlNamespaceInfo>>(5)
			{
				{
					"",
					new AvaloniaXamlIlXmlNamespaceInfo[37]
					{
						CreateNamespaceInfo("Avalonia", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Animation", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Animation.Easings", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Controls", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Data", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Data.Converters", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Input", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Input.GestureRecognizers", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Input.TextInput", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Layout", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.LogicalTree", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Media", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Media.Imaging", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Media.Transformation", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Styling", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Controls", "Avalonia.Controls.DataGrid"),
						CreateNamespaceInfo("Avalonia.Controls.Collections", "Avalonia.Controls.DataGrid"),
						CreateNamespaceInfo("Avalonia.Controls.Primitives", "Avalonia.Controls.DataGrid"),
						CreateNamespaceInfo("Avalonia", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Automation", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Embedding", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Presenters", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Primitives", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Shapes", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Templates", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Notifications", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Chrome", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Documents", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Fonts.Inter", "Avalonia.Fonts.Inter"),
						CreateNamespaceInfo("Avalonia.Data", "Avalonia.Markup"),
						CreateNamespaceInfo("Avalonia.Markup.Data", "Avalonia.Markup"),
						CreateNamespaceInfo("Avalonia.Markup.Xaml.MarkupExtensions", "Avalonia.Markup.Xaml"),
						CreateNamespaceInfo("Avalonia.Markup.Xaml.Styling", "Avalonia.Markup.Xaml"),
						CreateNamespaceInfo("Avalonia.Markup.Xaml.Templates", "Avalonia.Markup.Xaml"),
						CreateNamespaceInfo("Avalonia.Themes.Fluent", "Avalonia.Themes.Fluent"),
						CreateNamespaceInfo("Avalonia.Svg.Skia", "Svg.Controls.Skia.Avalonia")
					}
				},
				{
					"x",
					new AvaloniaXamlIlXmlNamespaceInfo[0]
				},
				{
					"d",
					new AvaloniaXamlIlXmlNamespaceInfo[0]
				},
				{
					"mc",
					new AvaloniaXamlIlXmlNamespaceInfo[0]
				},
				{
					"chrome",
					new AvaloniaXamlIlXmlNamespaceInfo[1] { CreateNamespaceInfo("Avalonia.Controls.Chrome", null) }
				}
			};
		}

		static NamespaceInfo_003A_002FViews_002FMainWindow2_002Eaxaml()
		{
			Singleton = new NamespaceInfo_003A_002FViews_002FMainWindow2_002Eaxaml();
		}
	}

	[CompilerGenerated]
	internal class NamespaceInfo_003A_002FViews_002FSettingsView_002Eaxaml : IAvaloniaXamlIlXmlNamespaceInfoProvider
	{
		private IReadOnlyDictionary<string, IReadOnlyList<AvaloniaXamlIlXmlNamespaceInfo>> _xmlNamespaces;

		public static IAvaloniaXamlIlXmlNamespaceInfoProvider Singleton;

		public virtual IReadOnlyDictionary<string, IReadOnlyList<AvaloniaXamlIlXmlNamespaceInfo>> XmlNamespaces
		{
			get
			{
				if (_xmlNamespaces == null)
				{
					_xmlNamespaces = CreateNamespaces();
				}
				return _xmlNamespaces;
			}
		}

		private static AvaloniaXamlIlXmlNamespaceInfo CreateNamespaceInfo(string P_0, string P_1)
		{
			return new AvaloniaXamlIlXmlNamespaceInfo
			{
				ClrNamespace = P_0,
				ClrAssemblyName = P_1
			};
		}

		private static IReadOnlyDictionary<string, IReadOnlyList<AvaloniaXamlIlXmlNamespaceInfo>> CreateNamespaces()
		{
			return new Dictionary<string, IReadOnlyList<AvaloniaXamlIlXmlNamespaceInfo>>(7)
			{
				{
					"",
					new AvaloniaXamlIlXmlNamespaceInfo[37]
					{
						CreateNamespaceInfo("Avalonia", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Animation", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Animation.Easings", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Controls", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Data", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Data.Converters", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Input", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Input.GestureRecognizers", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Input.TextInput", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Layout", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.LogicalTree", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Media", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Media.Imaging", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Media.Transformation", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Styling", "Avalonia.Base"),
						CreateNamespaceInfo("Avalonia.Controls", "Avalonia.Controls.DataGrid"),
						CreateNamespaceInfo("Avalonia.Controls.Collections", "Avalonia.Controls.DataGrid"),
						CreateNamespaceInfo("Avalonia.Controls.Primitives", "Avalonia.Controls.DataGrid"),
						CreateNamespaceInfo("Avalonia", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Automation", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Embedding", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Presenters", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Primitives", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Shapes", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Templates", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Notifications", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Chrome", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Controls.Documents", "Avalonia.Controls"),
						CreateNamespaceInfo("Avalonia.Fonts.Inter", "Avalonia.Fonts.Inter"),
						CreateNamespaceInfo("Avalonia.Data", "Avalonia.Markup"),
						CreateNamespaceInfo("Avalonia.Markup.Data", "Avalonia.Markup"),
						CreateNamespaceInfo("Avalonia.Markup.Xaml.MarkupExtensions", "Avalonia.Markup.Xaml"),
						CreateNamespaceInfo("Avalonia.Markup.Xaml.Styling", "Avalonia.Markup.Xaml"),
						CreateNamespaceInfo("Avalonia.Markup.Xaml.Templates", "Avalonia.Markup.Xaml"),
						CreateNamespaceInfo("Avalonia.Themes.Fluent", "Avalonia.Themes.Fluent"),
						CreateNamespaceInfo("Avalonia.Svg.Skia", "Svg.Controls.Skia.Avalonia")
					}
				},
				{
					"x",
					new AvaloniaXamlIlXmlNamespaceInfo[0]
				},
				{
					"d",
					new AvaloniaXamlIlXmlNamespaceInfo[0]
				},
				{
					"mc",
					new AvaloniaXamlIlXmlNamespaceInfo[0]
				},
				{
					"controls",
					new AvaloniaXamlIlXmlNamespaceInfo[1] { CreateNamespaceInfo("SpaceWalker.Controls", null) }
				},
				{
					"converters",
					new AvaloniaXamlIlXmlNamespaceInfo[1] { CreateNamespaceInfo("SpaceWalker.Converters", null) }
				},
				{
					"i18n",
					new AvaloniaXamlIlXmlNamespaceInfo[1] { CreateNamespaceInfo("SpaceWalker.Assets.Languages", null) }
				}
			};
		}

		static NamespaceInfo_003A_002FViews_002FSettingsView_002Eaxaml()
		{
			Singleton = new NamespaceInfo_003A_002FViews_002FSettingsView_002Eaxaml();
		}
	}

	public unsafe static void Populate_003A_002FThemes_002FColors_002Eaxaml(IServiceProvider P_0, ResourceDictionary P_1)
	{
		CompiledAvaloniaXaml.XamlIlContext.Context<ResourceDictionary> context = new CompiledAvaloniaXaml.XamlIlContext.Context<ResourceDictionary>(P_0, new object[1] { NamespaceInfo_003A_002FThemes_002FColors_002Eaxaml.Singleton }, "avares://SpaceWalker/Themes/Colors.axaml")
		{
			RootObject = P_1,
			IntermediateRoot = P_1
		};
		if (P_1 is ResourceDictionary resourceDictionary)
		{
			resourceDictionary.EnsureCapacity(resourceDictionary.Count + 22);
		}
		IDictionary<ThemeVariant, IThemeVariantProvider> themeDictionaries = P_1.ThemeDictionaries;
		ThemeVariant light = ThemeVariant.Light;
		ResourceDictionary resourceDictionary2 = new ResourceDictionary();
		if (resourceDictionary2 is ResourceDictionary resourceDictionary3)
		{
			resourceDictionary3.EnsureCapacity(resourceDictionary3.Count + 20);
		}
		((IThemeVariantProvider)resourceDictionary2).Key = ThemeVariant.Light;
		((IDictionary<object, object>)resourceDictionary2).Add((object)"SystemAccentColor", (object)Color.FromUInt32(4279398911u));
		((IDictionary<object, object>)resourceDictionary2).Add((object)"SystemAccentColorLight1", (object)Color.FromUInt32(4281370879u));
		((IDictionary<object, object>)resourceDictionary2).Add((object)"SystemAccentColorDark1", (object)Color.FromUInt32(4279001056u));
		resourceDictionary2.AddDeferred("SystemAccentColorBrush", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_12.Build_1), context));
		resourceDictionary2.AddDeferred("SystemAccentColorLight1Brush", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_12.Build_2), context));
		resourceDictionary2.AddDeferred("SystemAccentColorDark1Brush", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_12.Build_3), context));
		resourceDictionary2.AddDeferred("AccentButtonBackgroundBrush", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_12.Build_4), context));
		resourceDictionary2.AddDeferred("AccentButtonBackgroundHoverBrush", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_12.Build_5), context));
		resourceDictionary2.AddDeferred("AccentButtonBackgroundPressedBrush", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_12.Build_6), context));
		resourceDictionary2.AddDeferred("LayoutItemHoverBrush", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_12.Build_7), context));
		resourceDictionary2.AddDeferred("LayoutItemPressedBrush", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_12.Build_8), context));
		resourceDictionary2.AddDeferred("LayoutItemSelectedBrush", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_12.Build_9), context));
		resourceDictionary2.AddDeferred("LayoutItemSelectedBorderBrush", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_12.Build_10), context));
		resourceDictionary2.AddDeferred("ListBoxSelectedBorderBrush", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_12.Build_11), context));
		resourceDictionary2.AddDeferred("ThemeChipSelectedBackgroundBrush", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_12.Build_12), context));
		resourceDictionary2.AddDeferred("ComboBoxItemBackgroundSelected", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_12.Build_13), context));
		resourceDictionary2.AddDeferred("ComboBoxItemBackgroundSelectedPointerOver", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_12.Build_14), context));
		resourceDictionary2.AddDeferred("ComboBoxItemBackgroundSelectedPressed", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_12.Build_15), context));
		resourceDictionary2.AddDeferred("SystemFontFamily", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_12.Build_16), context));
		resourceDictionary2.AddDeferred("HeadlineFontFamily", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_12.Build_17), context));
		themeDictionaries.Add(light, resourceDictionary2);
		IDictionary<ThemeVariant, IThemeVariantProvider> themeDictionaries2 = P_1.ThemeDictionaries;
		ThemeVariant phantomBladeZeroVariant = ThemeManager.PhantomBladeZeroVariant;
		ResourceDictionary resourceDictionary4 = new ResourceDictionary();
		if (resourceDictionary4 is ResourceDictionary resourceDictionary5)
		{
			resourceDictionary5.EnsureCapacity(resourceDictionary5.Count + 20);
		}
		((IThemeVariantProvider)resourceDictionary4).Key = ThemeManager.PhantomBladeZeroVariant;
		((IDictionary<object, object>)resourceDictionary4).Add((object)"SystemAccentColor", (object)Color.FromUInt32(4293918720u));
		((IDictionary<object, object>)resourceDictionary4).Add((object)"SystemAccentColorLight1", (object)Color.FromUInt32(4294914867u));
		((IDictionary<object, object>)resourceDictionary4).Add((object)"SystemAccentColorDark1", (object)Color.FromUInt32(4290772992u));
		resourceDictionary4.AddDeferred("SystemAccentColorBrush", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_12.Build_18), context));
		resourceDictionary4.AddDeferred("SystemAccentColorLight1Brush", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_12.Build_19), context));
		resourceDictionary4.AddDeferred("SystemAccentColorDark1Brush", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_12.Build_20), context));
		resourceDictionary4.AddDeferred("AccentButtonBackgroundBrush", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_12.Build_21), context));
		resourceDictionary4.AddDeferred("AccentButtonBackgroundHoverBrush", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_12.Build_22), context));
		resourceDictionary4.AddDeferred("AccentButtonBackgroundPressedBrush", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_12.Build_23), context));
		resourceDictionary4.AddDeferred("LayoutItemHoverBrush", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_12.Build_24), context));
		resourceDictionary4.AddDeferred("LayoutItemPressedBrush", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_12.Build_25), context));
		resourceDictionary4.AddDeferred("LayoutItemSelectedBrush", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_12.Build_26), context));
		resourceDictionary4.AddDeferred("LayoutItemSelectedBorderBrush", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_12.Build_27), context));
		resourceDictionary4.AddDeferred("ListBoxSelectedBorderBrush", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_12.Build_28), context));
		resourceDictionary4.AddDeferred("ThemeChipSelectedBackgroundBrush", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_12.Build_29), context));
		resourceDictionary4.AddDeferred("ComboBoxItemBackgroundSelected", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_12.Build_30), context));
		resourceDictionary4.AddDeferred("ComboBoxItemBackgroundSelectedPointerOver", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_12.Build_31), context));
		resourceDictionary4.AddDeferred("ComboBoxItemBackgroundSelectedPressed", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_12.Build_32), context));
		resourceDictionary4.AddDeferred("SystemFontFamily", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_12.Build_33), context));
		resourceDictionary4.AddDeferred("HeadlineFontFamily", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_12.Build_34), context));
		themeDictionaries2.Add(phantomBladeZeroVariant, resourceDictionary4);
		P_1.AddDeferred("ComboBoxDropDownBackground", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_12.Build_35), context));
		P_1.AddDeferred("ComboBoxDropDownBorderBrush", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_12.Build_36), context));
		P_1.AddDeferred("ComboBoxItemBackgroundPointerOver", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_12.Build_37), context));
		P_1.AddDeferred("ComboBoxItemBackgroundPressed", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_12.Build_38), context));
		P_1.AddDeferred("ComboBoxItemForeground", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_12.Build_39), context));
		P_1.AddDeferred("ComboBoxItemForegroundPointerOver", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_12.Build_40), context));
		P_1.AddDeferred("ComboBoxItemForegroundPressed", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_12.Build_41), context));
		P_1.AddDeferred("ComboBoxItemForegroundSelected", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_12.Build_42), context));
		P_1.AddDeferred("ComboBoxItemForegroundSelectedPointerOver", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_12.Build_43), context));
		P_1.AddDeferred("ComboBoxItemForegroundSelectedPressed", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_12.Build_44), context));
		P_1.AddDeferred("ComboBoxForegroundFocusedPressed", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_12.Build_45), context));
		P_1.AddDeferred("TextControlBackgroundPointerOver", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_12.Build_46), context));
		P_1.AddDeferred("TextControlBackgroundFocused", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_12.Build_47), context));
		P_1.AddDeferred("TextControlPlaceholderForeground", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_12.Build_48), context));
		P_1.AddDeferred("TextControlPlaceholderForegroundPointerOver", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_12.Build_49), context));
		P_1.AddDeferred("TextControlPlaceholderForegroundFocused", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_12.Build_50), context));
		P_1.AddDeferred("TextControlForeground", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_12.Build_51), context));
		P_1.AddDeferred("TextControlForegroundPointerOver", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_12.Build_52), context));
		P_1.AddDeferred("TextControlForegroundFocused", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_12.Build_53), context));
		P_1.AddDeferred("ToolTipBackground", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_12.Build_54), context));
		P_1.AddDeferred("ToolTipBorderBrush", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_12.Build_55), context));
		P_1.AddDeferred("ToolTipForeground", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_12.Build_56), context));
		if (P_1 is StyledElement styled)
		{
			NameScope.SetNameScope(styled, context.AvaloniaNameScope);
		}
		context.AvaloniaNameScope.Complete();
	}

	public static ResourceDictionary Build_003A_002FThemes_002FColors_002Eaxaml(IServiceProvider P_0)
	{
		ResourceDictionary resourceDictionary = new ResourceDictionary();
		Populate_003A_002FThemes_002FColors_002Eaxaml(P_0, resourceDictionary);
		return resourceDictionary;
	}

	public static void Populate_003A_002FControls_002FStyles_002Eaxaml(IServiceProvider P_0, Styles P_1)
	{
		CompiledAvaloniaXaml.XamlIlContext.Context<Styles> context = new CompiledAvaloniaXaml.XamlIlContext.Context<Styles>(P_0, new object[1] { NamespaceInfo_003A_002FControls_002FStyles_002Eaxaml.Singleton }, "avares://SpaceWalker/Controls/Styles.axaml")
		{
			RootObject = P_1,
			IntermediateRoot = P_1
		};
		Styles styles;
		Styles styles2 = (styles = P_1);
		context.PushParent(styles);
		ResourceDictionary resourceDictionary;
		ResourceDictionary resources = (resourceDictionary = new ResourceDictionary());
		context.PushParent(resourceDictionary);
		resourceDictionary.MergedDictionaries.Add(Build_003A_002FControls_002FContentDialog_002Eaxaml(XamlIlRuntimeHelpers.CreateRootServiceProviderV3(context)));
		context.PopParent();
		styles.Resources = resources;
		styles.Add(Build_003A_002FControls_002FTabControl_002Eaxaml(XamlIlRuntimeHelpers.CreateRootServiceProviderV3(context)));
		styles.Add(Build_003A_002FControls_002FListBox_002Eaxaml(XamlIlRuntimeHelpers.CreateRootServiceProviderV3(context)));
		styles.Add(Build_003A_002FControls_002FSlider_002Eaxaml(XamlIlRuntimeHelpers.CreateRootServiceProviderV3(context)));
		styles.Add(Build_003A_002FControls_002FRadioButton_002Eaxaml(XamlIlRuntimeHelpers.CreateRootServiceProviderV3(context)));
		styles.Add(Build_003A_002FControls_002FCheckBox_002Eaxaml(XamlIlRuntimeHelpers.CreateRootServiceProviderV3(context)));
		styles.Add(Build_003A_002FControls_002FComboBox_002Eaxaml(XamlIlRuntimeHelpers.CreateRootServiceProviderV3(context)));
		styles.Add(Build_003A_002FControls_002FToggleSwitch_002Eaxaml(XamlIlRuntimeHelpers.CreateRootServiceProviderV3(context)));
		styles.Add(Build_003A_002FControls_002FButton_002Eaxaml(XamlIlRuntimeHelpers.CreateRootServiceProviderV3(context)));
		styles.Add(Build_003A_002FControls_002FTextStyles_002Eaxaml(XamlIlRuntimeHelpers.CreateRootServiceProviderV3(context)));
		styles.Add(Build_003A_002FControls_002FScrollViewer_002Eaxaml(XamlIlRuntimeHelpers.CreateRootServiceProviderV3(context)));
		styles.Add(Build_003A_002FControls_002FIconLabel_002Eaxaml(XamlIlRuntimeHelpers.CreateRootServiceProviderV3(context)));
		Style style;
		Style item = (style = new Style());
		context.PushParent(style);
		Style style2 = style;
		style2.Selector = ((Selector?)null).OfType(typeof(ContentDialog)).Class("message");
		Setter setter;
		Setter setter2 = (setter = new Setter());
		context.PushParent(setter);
		Setter setter3 = setter;
		setter3.Property = StyledElement.ThemeProperty;
		DynamicResourceExtension dynamicResourceExtension = new DynamicResourceExtension("MessageContentDialogTheme");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
		BindingBase value = dynamicResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		setter3.Value = value;
		context.PopParent();
		style2.Add(setter2);
		context.PopParent();
		styles.Add(item);
		Style item2 = (style = new Style());
		context.PushParent(style);
		Style style3 = style;
		style3.Selector = ((Selector?)null).OfType(typeof(ContentDialog)).Class("dialog");
		Setter setter4 = (setter = new Setter());
		context.PushParent(setter);
		Setter setter5 = setter;
		setter5.Property = StyledElement.ThemeProperty;
		DynamicResourceExtension dynamicResourceExtension2 = new DynamicResourceExtension("BaseContentDialogTheme");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
		BindingBase value2 = dynamicResourceExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		setter5.Value = value2;
		context.PopParent();
		style3.Add(setter4);
		context.PopParent();
		styles.Add(item2);
		Style item3 = (style = new Style());
		context.PushParent(style);
		Style style4 = style;
		style4.Selector = ((Selector?)null).OfType(typeof(Window));
		Setter setter6 = (setter = new Setter());
		context.PushParent(setter);
		Setter setter7 = setter;
		setter7.Property = TemplatedControl.FontFamilyProperty;
		DynamicResourceExtension dynamicResourceExtension3 = new DynamicResourceExtension("SystemFontFamily");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
		BindingBase value3 = dynamicResourceExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		setter7.Value = value3;
		context.PopParent();
		style4.Add(setter6);
		context.PopParent();
		styles.Add(item3);
		Style item4 = (style = new Style());
		context.PushParent(style);
		Style style5 = style;
		style5.Selector = ((Selector?)null).OfType(typeof(TextBlock));
		Setter setter8 = (setter = new Setter());
		context.PushParent(setter);
		Setter setter9 = setter;
		setter9.Property = TextBlock.FontFamilyProperty;
		DynamicResourceExtension dynamicResourceExtension4 = new DynamicResourceExtension("SystemFontFamily");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
		BindingBase value4 = dynamicResourceExtension4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		setter9.Value = value4;
		context.PopParent();
		style5.Add(setter8);
		context.PopParent();
		styles.Add(item4);
		Style item5 = (style = new Style());
		context.PushParent(style);
		Style style6 = style;
		style6.Selector = ((Selector?)null).OfType(typeof(TextBox));
		Setter setter10 = (setter = new Setter());
		context.PushParent(setter);
		Setter setter11 = setter;
		setter11.Property = TemplatedControl.FontFamilyProperty;
		DynamicResourceExtension dynamicResourceExtension5 = new DynamicResourceExtension("SystemFontFamily");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
		BindingBase value5 = dynamicResourceExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		setter11.Value = value5;
		context.PopParent();
		style6.Add(setter10);
		context.PopParent();
		styles.Add(item5);
		context.PopParent();
		if (styles2 is StyledElement styled)
		{
			NameScope.SetNameScope(styled, context.AvaloniaNameScope);
		}
		context.AvaloniaNameScope.Complete();
	}

	public static Styles Build_003A_002FControls_002FStyles_002Eaxaml(IServiceProvider P_0)
	{
		Styles styles = new Styles();
		Populate_003A_002FControls_002FStyles_002Eaxaml(P_0, styles);
		return styles;
	}

	public unsafe static void Populate_003A_002FControls_002FContentDialog_002Eaxaml(IServiceProvider P_0, ResourceDictionary P_1)
	{
		CompiledAvaloniaXaml.XamlIlContext.Context<ResourceDictionary> context = new CompiledAvaloniaXaml.XamlIlContext.Context<ResourceDictionary>(P_0, new object[1] { NamespaceInfo_003A_002FControls_002FContentDialog_002Eaxaml.Singleton }, "avares://SpaceWalker/Controls/ContentDialog.axaml")
		{
			RootObject = P_1,
			IntermediateRoot = P_1
		};
		ResourceDictionary resourceDictionary;
		ResourceDictionary resourceDictionary2 = (resourceDictionary = P_1);
		context.PushParent(resourceDictionary);
		if (resourceDictionary is ResourceDictionary resourceDictionary3)
		{
			resourceDictionary3.EnsureCapacity(resourceDictionary3.Count + 14);
		}
		resourceDictionary.AddDeferred("ContentDialogSmokeFill", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_4.Build_1), context));
		resourceDictionary.AddDeferred("ContentDialogTopOverlay", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_4.Build_2), context));
		resourceDictionary.AddDeferred("ContentDialogBackground", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_4.Build_3), context));
		resourceDictionary.AddDeferred("ContentDialogSeparatorBorderBrush", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_4.Build_4), context));
		resourceDictionary.AddDeferred("ContentDialogForeground", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_4.Build_5), context));
		resourceDictionary.AddDeferred("ContentDialogBorderBrush", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_4.Build_6), context));
		((IDictionary<object, object>)resourceDictionary).Add((object)"ContentDialogSeparatorThickness", (object)new Thickness(0.0, 0.0, 0.0, 0.0));
		((IDictionary<object, object>)resourceDictionary).Add((object)"ContentDialogBorderWidth", (object)new Thickness(1.0, 1.0, 1.0, 1.0));
		((IDictionary<object, object>)resourceDictionary).Add((object)"ContentDialogPadding", (object)new Thickness(0.0, 0.0, 0.0, 0.0));
		((IDictionary<object, object>)resourceDictionary).Add((object)"ContentDialogTitleIsVisible", (object)false);
		resourceDictionary.AddDeferred("ContentDialogAcrylicTintColor", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_4.Build_7), context));
		((IDictionary<object, object>)resourceDictionary).Add((object)"ContentDialogAcrylicBlurRadius", (object)25.0);
		resourceDictionary.AddDeferred("BaseContentDialogTheme", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_4.Build_8), context));
		resourceDictionary.AddDeferred("MessageContentDialogTheme", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_4.Build_11), context));
		context.PopParent();
		if (resourceDictionary2 is StyledElement styled)
		{
			NameScope.SetNameScope(styled, context.AvaloniaNameScope);
		}
		context.AvaloniaNameScope.Complete();
	}

	public static ResourceDictionary Build_003A_002FControls_002FContentDialog_002Eaxaml(IServiceProvider P_0)
	{
		ResourceDictionary resourceDictionary = new ResourceDictionary();
		Populate_003A_002FControls_002FContentDialog_002Eaxaml(P_0, resourceDictionary);
		return resourceDictionary;
	}

	public unsafe static void Populate_003A_002FControls_002FTabControl_002Eaxaml(IServiceProvider P_0, Styles P_1)
	{
		CompiledAvaloniaXaml.XamlIlContext.Context<Styles> context = new CompiledAvaloniaXaml.XamlIlContext.Context<Styles>(P_0, new object[1] { NamespaceInfo_003A_002FControls_002FTabControl_002Eaxaml.Singleton }, "avares://SpaceWalker/Controls/TabControl.axaml")
		{
			RootObject = P_1,
			IntermediateRoot = P_1
		};
		Style style = new Style();
		style.Selector = ((Selector?)null).OfType(typeof(Avalonia.Controls.TabControl)).Class("verticaltabcontrol");
		Setter setter = new Setter();
		setter.Property = TemplatedControl.PaddingProperty;
		setter.Value = new Thickness(0.0, 0.0, 0.0, 0.0);
		style.Add(setter);
		Setter setter2 = new Setter();
		setter2.Property = TemplatedControl.TemplateProperty;
		setter2.Value = new ControlTemplate
		{
			Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_10.Build_1), context)
		};
		style.Add(setter2);
		P_1.Add(style);
		Style style2 = new Style();
		style2.Selector = ((Selector?)null).OfType(typeof(Avalonia.Controls.TabControl)).Class("verticaltabcontrol").Child()
			.OfType(typeof(Avalonia.Controls.TabItem));
		Setter setter3 = new Setter();
		setter3.Property = TemplatedControl.PaddingProperty;
		setter3.Value = new Thickness(12.0, 10.0, 12.0, 10.0);
		style2.Add(setter3);
		Setter setter4 = new Setter();
		setter4.Property = TemplatedControl.FontSizeProperty;
		setter4.Value = 13.0;
		style2.Add(setter4);
		Setter setter5 = new Setter();
		setter5.Property = TemplatedControl.ForegroundProperty;
		setter5.Value = new ImmutableSolidColorBrush(uint.MaxValue);
		style2.Add(setter5);
		Setter setter6 = new Setter();
		setter6.Property = Layoutable.MarginProperty;
		setter6.Value = new Thickness(0.0, 0.0, 0.0, 4.0);
		style2.Add(setter6);
		Setter setter7 = new Setter();
		setter7.Property = Layoutable.MinHeightProperty;
		setter7.Value = 36.0;
		style2.Add(setter7);
		Setter setter8 = new Setter();
		setter8.Property = Visual.OpacityProperty;
		setter8.Value = 0.72;
		style2.Add(setter8);
		Setter setter9 = new Setter();
		setter9.Property = ContentControl.HorizontalContentAlignmentProperty;
		setter9.Value = HorizontalAlignment.Stretch;
		style2.Add(setter9);
		Setter setter10 = new Setter();
		setter10.Property = TemplatedControl.TemplateProperty;
		setter10.Value = new ControlTemplate
		{
			Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_10.Build_2), context)
		};
		style2.Add(setter10);
		P_1.Add(style2);
		Style style3 = new Style();
		style3.Selector = ((Selector?)null).OfType(typeof(Avalonia.Controls.TabControl)).Class("verticaltabcontrol").Child()
			.OfType(typeof(Avalonia.Controls.TabItem))
			.Class(":pointerover")
			.Template()
			.OfType(typeof(Border))
			.Name("PART_LayoutRoot");
		Setter setter11 = new Setter();
		setter11.Property = Border.BackgroundProperty;
		setter11.Value = new ImmutableSolidColorBrush(536870911u);
		style3.Add(setter11);
		P_1.Add(style3);
		Style style4 = new Style();
		style4.Selector = ((Selector?)null).OfType(typeof(Avalonia.Controls.TabControl)).Class("verticaltabcontrol").Child()
			.OfType(typeof(Avalonia.Controls.TabItem))
			.Class(":selected")
			.Template()
			.OfType(typeof(Border))
			.Name("PART_LayoutRoot");
		Setter setter12 = new Setter();
		setter12.Property = Border.BackgroundProperty;
		LinearGradientBrush linearGradientBrush = new LinearGradientBrush();
		linearGradientBrush.SetValue(LinearGradientBrush.StartPointProperty, new RelativePoint(0.0, 0.5, RelativeUnit.Relative), BindingPriority.Template);
		linearGradientBrush.SetValue(LinearGradientBrush.EndPointProperty, new RelativePoint(1.0, 0.5, RelativeUnit.Relative), BindingPriority.Template);
		GradientStops gradientStops = linearGradientBrush.GradientStops;
		GradientStop gradientStop = new GradientStop();
		gradientStop.SetValue(GradientStop.OffsetProperty, 0.0, BindingPriority.Template);
		gradientStop.SetValue(GradientStop.ColorProperty, Color.FromUInt32(1728053247u), BindingPriority.Template);
		gradientStops.Add(gradientStop);
		GradientStops gradientStops2 = linearGradientBrush.GradientStops;
		GradientStop gradientStop2 = new GradientStop();
		gradientStop2.SetValue(GradientStop.OffsetProperty, 0.57, BindingPriority.Template);
		gradientStop2.SetValue(GradientStop.ColorProperty, Color.FromUInt32(536870911u), BindingPriority.Template);
		gradientStops2.Add(gradientStop2);
		GradientStops gradientStops3 = linearGradientBrush.GradientStops;
		GradientStop gradientStop3 = new GradientStop();
		gradientStop3.SetValue(GradientStop.OffsetProperty, 1.0, BindingPriority.Template);
		gradientStop3.SetValue(GradientStop.ColorProperty, Color.FromUInt32(536870911u), BindingPriority.Template);
		gradientStops3.Add(gradientStop3);
		setter12.Value = linearGradientBrush;
		style4.Add(setter12);
		Setter setter13 = new Setter();
		setter13.Property = Border.BorderBrushProperty;
		setter13.Value = new ImmutableSolidColorBrush(uint.MaxValue);
		style4.Add(setter13);
		P_1.Add(style4);
		Style style5 = new Style();
		style5.Selector = ((Selector?)null).OfType(typeof(Avalonia.Controls.TabControl)).Class("verticaltabcontrol").Child()
			.OfType(typeof(Avalonia.Controls.TabItem))
			.Class(":selected");
		Setter setter14 = new Setter();
		setter14.Property = Visual.OpacityProperty;
		setter14.Value = 1.0;
		style5.Add(setter14);
		P_1.Add(style5);
		if (P_1 is StyledElement styled)
		{
			NameScope.SetNameScope(styled, context.AvaloniaNameScope);
		}
		context.AvaloniaNameScope.Complete();
	}

	public static Styles Build_003A_002FControls_002FTabControl_002Eaxaml(IServiceProvider P_0)
	{
		Styles styles = new Styles();
		Populate_003A_002FControls_002FTabControl_002Eaxaml(P_0, styles);
		return styles;
	}

	public unsafe static void Populate_003A_002FControls_002FListBox_002Eaxaml(IServiceProvider P_0, Styles P_1)
	{
		CompiledAvaloniaXaml.XamlIlContext.Context<Styles> context = new CompiledAvaloniaXaml.XamlIlContext.Context<Styles>(P_0, new object[1] { NamespaceInfo_003A_002FControls_002FListBox_002Eaxaml.Singleton }, "avares://SpaceWalker/Controls/ListBox.axaml")
		{
			RootObject = P_1,
			IntermediateRoot = P_1
		};
		Styles styles;
		Styles styles2 = (styles = P_1);
		context.PushParent(styles);
		Style style = new Style();
		style.Selector = ((Selector?)null).OfType(typeof(ListBox)).Class("base");
		Setter setter = new Setter();
		setter.Property = TemplatedControl.BackgroundProperty;
		setter.Value = new ImmutableSolidColorBrush(16777215u);
		style.Add(setter);
		Setter setter2 = new Setter();
		setter2.Property = TemplatedControl.BorderThicknessProperty;
		setter2.Value = new Thickness(0.0, 0.0, 0.0, 0.0);
		style.Add(setter2);
		Setter setter3 = new Setter();
		setter3.Property = TemplatedControl.PaddingProperty;
		setter3.Value = new Thickness(0.0, 0.0, 0.0, 0.0);
		style.Add(setter3);
		Setter setter4 = new Setter();
		setter4.Property = ListBox.SelectionModeProperty;
		setter4.Value = SelectionMode.Single;
		style.Add(setter4);
		Setter setter5 = new Setter();
		setter5.Property = ScrollViewer.HorizontalScrollBarVisibilityProperty;
		setter5.Value = ScrollBarVisibility.Disabled;
		style.Add(setter5);
		styles.Add(style);
		Style style2 = new Style();
		style2.Selector = ((Selector?)null).OfType(typeof(ListBox)).Class("base").Descendant()
			.OfType(typeof(ListBoxItem));
		Setter setter6 = new Setter();
		setter6.Property = TemplatedControl.PaddingProperty;
		setter6.Value = new Thickness(0.0, 0.0, 0.0, 0.0);
		style2.Add(setter6);
		Setter setter7 = new Setter();
		setter7.Property = Layoutable.MarginProperty;
		setter7.Value = new Thickness(0.0, 0.0, 0.0, 0.0);
		style2.Add(setter7);
		Setter setter8 = new Setter();
		setter8.Property = TemplatedControl.BackgroundProperty;
		setter8.Value = new ImmutableSolidColorBrush(16777215u);
		style2.Add(setter8);
		Setter setter9 = new Setter();
		setter9.Property = InputElement.CursorProperty;
		setter9.Value = new Cursor(StandardCursorType.Hand);
		style2.Add(setter9);
		Setter setter10 = new Setter();
		setter10.Property = TemplatedControl.TemplateProperty;
		setter10.Value = new ControlTemplate
		{
			Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_6.Build_1), context)
		};
		style2.Add(setter10);
		styles.Add(style2);
		Style style3;
		Style item = (style3 = new Style());
		context.PushParent(style3);
		style3.Selector = ((Selector?)null).OfType(typeof(ListBox)).Class("base").Descendant()
			.OfType(typeof(ListBoxItem))
			.Class(":selected")
			.Template()
			.OfType(typeof(Border))
			.Name("CardBorder");
		Setter setter11;
		Setter setter12 = (setter11 = new Setter());
		context.PushParent(setter11);
		setter11.Property = Border.BorderBrushProperty;
		DynamicResourceExtension dynamicResourceExtension = new DynamicResourceExtension("ListBoxSelectedBorderBrush");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
		BindingBase value = dynamicResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		setter11.Value = value;
		context.PopParent();
		style3.Add(setter12);
		context.PopParent();
		styles.Add(item);
		Style style4 = new Style();
		style4.Selector = ((Selector?)null).OfType(typeof(ListBox)).Class("base").Descendant()
			.OfType(typeof(ListBoxItem))
			.Class(":pointerover")
			.Template()
			.OfType(typeof(Border))
			.Name("HoverOverlay");
		Setter setter13 = new Setter();
		setter13.Property = Border.BackgroundProperty;
		setter13.Value = new ImmutableSolidColorBrush(452984831u);
		style4.Add(setter13);
		styles.Add(style4);
		Style style5 = new Style();
		style5.Selector = ((Selector?)null).OfType(typeof(ListBox)).Class("base").Descendant()
			.OfType(typeof(ListBoxItem))
			.Class(":selected")
			.Class(":pointerover")
			.Template()
			.OfType(typeof(Border))
			.Name("HoverOverlay");
		Setter setter14 = new Setter();
		setter14.Property = Border.BackgroundProperty;
		setter14.Value = new ImmutableSolidColorBrush(234881023u);
		style5.Add(setter14);
		styles.Add(style5);
		context.PopParent();
		if (styles2 is StyledElement styled)
		{
			NameScope.SetNameScope(styled, context.AvaloniaNameScope);
		}
		context.AvaloniaNameScope.Complete();
	}

	public static Styles Build_003A_002FControls_002FListBox_002Eaxaml(IServiceProvider P_0)
	{
		Styles styles = new Styles();
		Populate_003A_002FControls_002FListBox_002Eaxaml(P_0, styles);
		return styles;
	}

	public unsafe static void Populate_003A_002FControls_002FSlider_002Eaxaml(IServiceProvider P_0, Styles P_1)
	{
		CompiledAvaloniaXaml.XamlIlContext.Context<Styles> context = new CompiledAvaloniaXaml.XamlIlContext.Context<Styles>(P_0, new object[1] { NamespaceInfo_003A_002FControls_002FSlider_002Eaxaml.Singleton }, "avares://SpaceWalker/Controls/Slider.axaml")
		{
			RootObject = P_1,
			IntermediateRoot = P_1
		};
		Styles styles;
		Styles styles2 = (styles = P_1);
		context.PushParent(styles);
		ResourceDictionary resourceDictionary;
		ResourceDictionary resources = (resourceDictionary = new ResourceDictionary());
		context.PushParent(resourceDictionary);
		ResourceDictionary resourceDictionary2 = resourceDictionary;
		if (resourceDictionary2 is ResourceDictionary resourceDictionary3)
		{
			resourceDictionary3.EnsureCapacity(resourceDictionary3.Count + 3);
		}
		IDictionary<ThemeVariant, IThemeVariantProvider> themeDictionaries = resourceDictionary2.ThemeDictionaries;
		ThemeVariant light = ThemeVariant.Light;
		ResourceDictionary value = (resourceDictionary = new ResourceDictionary());
		context.PushParent(resourceDictionary);
		ResourceDictionary resourceDictionary4 = resourceDictionary;
		((IThemeVariantProvider)resourceDictionary4).Key = ThemeVariant.Light;
		resourceDictionary4.AddDeferred("base", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_9.Build_1), context));
		context.PopParent();
		themeDictionaries.Add(light, value);
		IDictionary<ThemeVariant, IThemeVariantProvider> themeDictionaries2 = resourceDictionary2.ThemeDictionaries;
		ThemeVariant phantomBladeZeroVariant = ThemeManager.PhantomBladeZeroVariant;
		ResourceDictionary value2 = (resourceDictionary = new ResourceDictionary());
		context.PushParent(resourceDictionary);
		ResourceDictionary resourceDictionary5 = resourceDictionary;
		((IThemeVariantProvider)resourceDictionary5).Key = ThemeManager.PhantomBladeZeroVariant;
		resourceDictionary5.AddDeferred("base", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_9.Build_4), context));
		context.PopParent();
		themeDictionaries2.Add(phantomBladeZeroVariant, value2);
		resourceDictionary2.AddDeferred("SliderTrackButton", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_9.Build_7), context));
		resourceDictionary2.AddDeferred("SliderAccentFillButton", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_9.Build_9), context));
		resourceDictionary2.AddDeferred("PbzSliderFillButton", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_9.Build_11), context));
		context.PopParent();
		styles.Resources = resources;
		context.PopParent();
		if (styles2 is StyledElement styled)
		{
			NameScope.SetNameScope(styled, context.AvaloniaNameScope);
		}
		context.AvaloniaNameScope.Complete();
	}

	public static Styles Build_003A_002FControls_002FSlider_002Eaxaml(IServiceProvider P_0)
	{
		Styles styles = new Styles();
		Populate_003A_002FControls_002FSlider_002Eaxaml(P_0, styles);
		return styles;
	}

	public unsafe static void Populate_003A_002FControls_002FRadioButton_002Eaxaml(IServiceProvider P_0, Styles P_1)
	{
		CompiledAvaloniaXaml.XamlIlContext.Context<Styles> context = new CompiledAvaloniaXaml.XamlIlContext.Context<Styles>(P_0, new object[1] { NamespaceInfo_003A_002FControls_002FRadioButton_002Eaxaml.Singleton }, "avares://SpaceWalker/Controls/RadioButton.axaml")
		{
			RootObject = P_1,
			IntermediateRoot = P_1
		};
		Styles styles;
		Styles styles2 = (styles = P_1);
		context.PushParent(styles);
		Style style;
		Style item = (style = new Style());
		context.PushParent(style);
		Style style2 = style;
		style2.Selector = ((Selector?)null).OfType(typeof(RadioButton)).Class("base");
		Setter setter = new Setter();
		setter.Property = TemplatedControl.ForegroundProperty;
		setter.Value = new ImmutableSolidColorBrush(uint.MaxValue);
		style2.Add(setter);
		Setter setter2 = new Setter();
		setter2.Property = InputElement.CursorProperty;
		setter2.Value = new Cursor(StandardCursorType.Hand);
		style2.Add(setter2);
		Setter setter3;
		Setter setter4 = (setter3 = new Setter());
		context.PushParent(setter3);
		Setter setter5 = setter3;
		setter5.Property = TemplatedControl.TemplateProperty;
		ControlTemplate controlTemplate;
		ControlTemplate value = (controlTemplate = new ControlTemplate());
		context.PushParent(controlTemplate);
		controlTemplate.Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_7.Build_1), context);
		context.PopParent();
		setter5.Value = value;
		context.PopParent();
		style2.Add(setter4);
		context.PopParent();
		styles.Add(item);
		Style item2 = (style = new Style());
		context.PushParent(style);
		Style style3 = style;
		style3.Selector = ((Selector?)null).OfType(typeof(RadioButton)).Class("base").Class(":checked")
			.Template()
			.OfType(typeof(Border))
			.Name("OuterRing");
		Setter setter6 = (setter3 = new Setter());
		context.PushParent(setter3);
		Setter setter7 = setter3;
		setter7.Property = Border.BorderBrushProperty;
		DynamicResourceExtension dynamicResourceExtension = new DynamicResourceExtension("SystemAccentColorBrush");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
		BindingBase value2 = dynamicResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		setter7.Value = value2;
		context.PopParent();
		style3.Add(setter6);
		context.PopParent();
		styles.Add(item2);
		Style style4 = new Style();
		style4.Selector = ((Selector?)null).OfType(typeof(RadioButton)).Class("base").Class(":checked")
			.Template()
			.OfType(typeof(Ellipse))
			.Name("InnerDot");
		Setter setter8 = new Setter();
		setter8.Property = Visual.OpacityProperty;
		setter8.Value = 1.0;
		style4.Add(setter8);
		styles.Add(style4);
		Style style5 = new Style();
		style5.Selector = Selectors.Or(new List<Selector>
		{
			((Selector?)null).OfType(typeof(RadioButton)).Class("base").Descendant()
				.OfType(typeof(TextBlock)),
			((Selector?)null).OfType(typeof(RadioButton)).Class("base").Descendant()
				.OfType(typeof(AccessText))
		});
		Setter setter9 = new Setter();
		setter9.Property = TextBlock.ForegroundProperty;
		setter9.Value = new ImmutableSolidColorBrush(uint.MaxValue);
		style5.Add(setter9);
		styles.Add(style5);
		context.PopParent();
		if (styles2 is StyledElement styled)
		{
			NameScope.SetNameScope(styled, context.AvaloniaNameScope);
		}
		context.AvaloniaNameScope.Complete();
	}

	public static Styles Build_003A_002FControls_002FRadioButton_002Eaxaml(IServiceProvider P_0)
	{
		Styles styles = new Styles();
		Populate_003A_002FControls_002FRadioButton_002Eaxaml(P_0, styles);
		return styles;
	}

	public unsafe static void Populate_003A_002FControls_002FCheckBox_002Eaxaml(IServiceProvider P_0, Styles P_1)
	{
		CompiledAvaloniaXaml.XamlIlContext.Context<Styles> context = new CompiledAvaloniaXaml.XamlIlContext.Context<Styles>(P_0, new object[1] { NamespaceInfo_003A_002FControls_002FCheckBox_002Eaxaml.Singleton }, "avares://SpaceWalker/Controls/CheckBox.axaml")
		{
			RootObject = P_1,
			IntermediateRoot = P_1
		};
		Styles styles;
		Styles styles2 = (styles = P_1);
		context.PushParent(styles);
		Style style = new Style();
		style.Selector = ((Selector?)null).OfType(typeof(CheckBox)).Class("base");
		Setter setter = new Setter();
		setter.Property = TemplatedControl.ForegroundProperty;
		setter.Value = new ImmutableSolidColorBrush(uint.MaxValue);
		style.Add(setter);
		Setter setter2 = new Setter();
		setter2.Property = InputElement.CursorProperty;
		setter2.Value = new Cursor(StandardCursorType.Hand);
		style.Add(setter2);
		Setter setter3 = new Setter();
		setter3.Property = TemplatedControl.TemplateProperty;
		setter3.Value = new ControlTemplate
		{
			Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_2.Build_1), context)
		};
		style.Add(setter3);
		styles.Add(style);
		Style style2;
		Style item = (style2 = new Style());
		context.PushParent(style2);
		style2.Selector = ((Selector?)null).OfType(typeof(CheckBox)).Class("base").Class(":checked")
			.Template()
			.OfType(typeof(Border))
			.Name("OuterBox");
		Setter setter4;
		Setter setter5 = (setter4 = new Setter());
		context.PushParent(setter4);
		Setter setter6 = setter4;
		setter6.Property = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension = new DynamicResourceExtension("SystemAccentColorBrush");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
		BindingBase value = dynamicResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		setter6.Value = value;
		context.PopParent();
		style2.Add(setter5);
		Setter setter7 = (setter4 = new Setter());
		context.PushParent(setter4);
		Setter setter8 = setter4;
		setter8.Property = Border.BorderBrushProperty;
		DynamicResourceExtension dynamicResourceExtension2 = new DynamicResourceExtension("SystemAccentColorBrush");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
		BindingBase value2 = dynamicResourceExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		setter8.Value = value2;
		context.PopParent();
		style2.Add(setter7);
		context.PopParent();
		styles.Add(item);
		Style style3 = new Style();
		style3.Selector = ((Selector?)null).OfType(typeof(CheckBox)).Class("base").Class(":checked")
			.Template()
			.OfType(typeof(Path))
			.Name("CheckMark");
		Setter setter9 = new Setter();
		setter9.Property = Visual.OpacityProperty;
		setter9.Value = 1.0;
		style3.Add(setter9);
		styles.Add(style3);
		Style style4 = new Style();
		style4.Selector = Selectors.Or(new List<Selector>
		{
			((Selector?)null).OfType(typeof(CheckBox)).Class("base").Descendant()
				.OfType(typeof(TextBlock)),
			((Selector?)null).OfType(typeof(CheckBox)).Class("base").Descendant()
				.OfType(typeof(AccessText))
		});
		Setter setter10 = new Setter();
		setter10.Property = TextBlock.ForegroundProperty;
		setter10.Value = new ImmutableSolidColorBrush(uint.MaxValue);
		style4.Add(setter10);
		styles.Add(style4);
		context.PopParent();
		if (styles2 is StyledElement styled)
		{
			NameScope.SetNameScope(styled, context.AvaloniaNameScope);
		}
		context.AvaloniaNameScope.Complete();
	}

	public static Styles Build_003A_002FControls_002FCheckBox_002Eaxaml(IServiceProvider P_0)
	{
		Styles styles = new Styles();
		Populate_003A_002FControls_002FCheckBox_002Eaxaml(P_0, styles);
		return styles;
	}

	public unsafe static void Populate_003A_002FControls_002FComboBox_002Eaxaml(IServiceProvider P_0, Styles P_1)
	{
		CompiledAvaloniaXaml.XamlIlContext.Context<Styles> context = new CompiledAvaloniaXaml.XamlIlContext.Context<Styles>(P_0, new object[1] { NamespaceInfo_003A_002FControls_002FComboBox_002Eaxaml.Singleton }, "avares://SpaceWalker/Controls/ComboBox.axaml")
		{
			RootObject = P_1,
			IntermediateRoot = P_1
		};
		Styles styles;
		Styles styles2 = (styles = P_1);
		context.PushParent(styles);
		if (styles.Resources is ResourceDictionary resourceDictionary)
		{
			resourceDictionary.EnsureCapacity(resourceDictionary.Count + 7);
		}
		((ResourceDictionary)styles.Resources).AddDeferred((object)"ComboBoxBaseBackground", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_3.Build_1), context));
		((ResourceDictionary)styles.Resources).AddDeferred((object)"ComboBoxBaseBackgroundPointerOver", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_3.Build_2), context));
		((ResourceDictionary)styles.Resources).AddDeferred((object)"ComboBoxBaseBackgroundPressed", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_3.Build_3), context));
		((ResourceDictionary)styles.Resources).AddDeferred((object)"ComboBoxBaseBorderBrush", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_3.Build_4), context));
		((ResourceDictionary)styles.Resources).AddDeferred((object)"ComboBoxItemDividerBrush", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_3.Build_5), context));
		((ResourceDictionary)styles.Resources).AddDeferred((object)"ComboBoxDownGlyph", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_3.Build_6), context));
		((ResourceDictionary)styles.Resources).AddDeferred((object)"ComboBoxRightGlyph", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_3.Build_7), context));
		Style style = new Style();
		style.Selector = ((Selector?)null).OfType(typeof(ComboBox)).Class("base").Template()
			.OfType(typeof(Popup))
			.Name("PART_Popup")
			.Child()
			.OfType(typeof(PopupRoot));
		Setter setter = new Setter();
		setter.Property = TopLevel.TransparencyLevelHintProperty;
		setter.Value = (IReadOnlyList<WindowTransparencyLevel>)new WindowTransparencyLevel[1] { WindowTransparencyLevel.AcrylicBlur };
		style.Add(setter);
		Setter setter2 = new Setter();
		setter2.Property = TemplatedControl.BackgroundProperty;
		setter2.Value = new ImmutableSolidColorBrush(16777215u);
		style.Add(setter2);
		Setter setter3 = new Setter();
		setter3.Property = PopupCornerHelper.RoundCornersProperty;
		setter3.Value = true;
		style.Add(setter3);
		styles.Add(style);
		Style style2 = new Style();
		style2.Selector = ((Selector?)null).OfType(typeof(ComboBox)).Class("base").Template()
			.OfType(typeof(Popup))
			.Name("PART_Popup");
		Setter setter4 = new Setter();
		setter4.Property = PopupGapHelper.SymmetricVerticalGapProperty;
		setter4.Value = 5.0;
		style2.Add(setter4);
		styles.Add(style2);
		Style style3 = new Style();
		style3.Selector = ((Selector?)null).OfType(typeof(ComboBox)).Class("base").Template()
			.OfType(typeof(Border))
			.Name("PopupBorder");
		Setter setter5 = new Setter();
		setter5.Property = Decorator.PaddingProperty;
		setter5.Value = new Thickness(8.0, 8.0, 8.0, 8.0);
		style3.Add(setter5);
		Setter setter6 = new Setter();
		setter6.Property = Border.CornerRadiusProperty;
		setter6.Value = new CornerRadius(8.0, 8.0, 8.0, 8.0);
		style3.Add(setter6);
		Setter setter7 = new Setter();
		setter7.Property = Border.BorderThicknessProperty;
		setter7.Value = new Thickness(0.0, 0.0, 0.0, 0.0);
		style3.Add(setter7);
		styles.Add(style3);
		Style style4 = new Style();
		style4.Selector = ((Selector?)null).OfType(typeof(ComboBox)).Class("base").Template()
			.OfType(typeof(ItemsPresenter))
			.Name("PART_ItemsPresenter");
		Setter setter8 = new Setter();
		setter8.Property = Layoutable.MarginProperty;
		setter8.Value = new Thickness(0.0, 0.0, 0.0, 0.0);
		style4.Add(setter8);
		styles.Add(style4);
		Style style5;
		Style item = (style5 = new Style());
		context.PushParent(style5);
		Style style6 = style5;
		style6.Selector = ((Selector?)null).OfType(typeof(ComboBox)).Class("base");
		Setter setter9;
		Setter setter10 = (setter9 = new Setter());
		context.PushParent(setter9);
		Setter setter11 = setter9;
		setter11.Property = TemplatedControl.BackgroundProperty;
		StaticResourceExtension staticResourceExtension = new StaticResourceExtension("ComboBoxBaseBackground");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
		object? value = staticResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		setter11.Value = value;
		context.PopParent();
		style6.Add(setter10);
		Setter setter12 = (setter9 = new Setter());
		context.PushParent(setter9);
		Setter setter13 = setter9;
		setter13.Property = TemplatedControl.BorderBrushProperty;
		StaticResourceExtension staticResourceExtension2 = new StaticResourceExtension("ComboBoxBaseBorderBrush");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
		object? value2 = staticResourceExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		setter13.Value = value2;
		context.PopParent();
		style6.Add(setter12);
		Setter setter14 = new Setter();
		setter14.Property = TemplatedControl.BorderThicknessProperty;
		setter14.Value = new Thickness(1.0, 1.0, 1.0, 1.0);
		style6.Add(setter14);
		Setter setter15 = new Setter();
		setter15.Property = TemplatedControl.ForegroundProperty;
		setter15.Value = new ImmutableSolidColorBrush(uint.MaxValue);
		style6.Add(setter15);
		Setter setter16 = new Setter();
		setter16.Property = TemplatedControl.PaddingProperty;
		setter16.Value = new Thickness(12.0, 0.0, 12.0, 0.0);
		style6.Add(setter16);
		Setter setter17 = new Setter();
		setter17.Property = Layoutable.MinHeightProperty;
		setter17.Value = 40.0;
		style6.Add(setter17);
		Setter setter18 = new Setter();
		setter18.Property = TemplatedControl.CornerRadiusProperty;
		setter18.Value = new CornerRadius(4.0, 4.0, 4.0, 4.0);
		style6.Add(setter18);
		context.PopParent();
		styles.Add(item);
		Style item2 = (style5 = new Style());
		context.PushParent(style5);
		Style style7 = style5;
		style7.Selector = ((Selector?)null).OfType(typeof(ComboBox)).Class("base").Class(":pointerover")
			.Template()
			.OfType(typeof(Border))
			.Name("Background");
		Setter setter19 = (setter9 = new Setter());
		context.PushParent(setter9);
		Setter setter20 = setter9;
		setter20.Property = Border.BackgroundProperty;
		StaticResourceExtension staticResourceExtension3 = new StaticResourceExtension("ComboBoxBaseBackgroundPointerOver");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
		object? value3 = staticResourceExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		setter20.Value = value3;
		context.PopParent();
		style7.Add(setter19);
		Setter setter21 = (setter9 = new Setter());
		context.PushParent(setter9);
		Setter setter22 = setter9;
		setter22.Property = Border.BorderBrushProperty;
		ReflectionBindingExtension reflectionBindingExtension = new ReflectionBindingExtension("$parent[ComboBox].BorderBrush");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
		ReflectionBinding value4 = reflectionBindingExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		setter22.Value = value4;
		context.PopParent();
		style7.Add(setter21);
		context.PopParent();
		styles.Add(item2);
		Style item3 = (style5 = new Style());
		context.PushParent(style5);
		Style style8 = style5;
		style8.Selector = ((Selector?)null).OfType(typeof(ComboBox)).Class("base").Class(":pressed")
			.Template()
			.OfType(typeof(Border))
			.Name("Background");
		Setter setter23 = (setter9 = new Setter());
		context.PushParent(setter9);
		Setter setter24 = setter9;
		setter24.Property = Border.BackgroundProperty;
		StaticResourceExtension staticResourceExtension4 = new StaticResourceExtension("ComboBoxBaseBackgroundPressed");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
		object? value5 = staticResourceExtension4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		setter24.Value = value5;
		context.PopParent();
		style8.Add(setter23);
		Setter setter25 = (setter9 = new Setter());
		context.PushParent(setter9);
		Setter setter26 = setter9;
		setter26.Property = Border.BorderBrushProperty;
		ReflectionBindingExtension reflectionBindingExtension2 = new ReflectionBindingExtension("$parent[ComboBox].BorderBrush");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
		ReflectionBinding value6 = reflectionBindingExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		setter26.Value = value6;
		context.PopParent();
		style8.Add(setter25);
		context.PopParent();
		styles.Add(item3);
		Style item4 = (style5 = new Style());
		context.PushParent(style5);
		Style style9 = style5;
		style9.Selector = ((Selector?)null).OfType(typeof(ComboBox)).Class("base").Class(":disabled")
			.Template()
			.OfType(typeof(Border))
			.Name("Background");
		Setter setter27 = (setter9 = new Setter());
		context.PushParent(setter9);
		Setter setter28 = setter9;
		setter28.Property = Border.BorderBrushProperty;
		ReflectionBindingExtension reflectionBindingExtension3 = new ReflectionBindingExtension("$parent[ComboBox].BorderBrush");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
		ReflectionBinding value7 = reflectionBindingExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		setter28.Value = value7;
		context.PopParent();
		style9.Add(setter27);
		context.PopParent();
		styles.Add(item4);
		Style style10 = new Style();
		style10.Selector = ((Selector?)null).OfType(typeof(ComboBox)).Class("base").Class("ghost");
		Setter setter29 = new Setter();
		setter29.Property = TemplatedControl.BackgroundProperty;
		setter29.Value = new ImmutableSolidColorBrush(16777215u);
		style10.Add(setter29);
		Setter setter30 = new Setter();
		setter30.Property = TemplatedControl.BorderThicknessProperty;
		setter30.Value = new Thickness(0.0, 0.0, 0.0, 0.0);
		style10.Add(setter30);
		Setter setter31 = new Setter();
		setter31.Property = Layoutable.MinHeightProperty;
		setter31.Value = 0.0;
		style10.Add(setter31);
		styles.Add(style10);
		Style style11 = new Style();
		style11.Selector = Selectors.Or(new List<Selector>
		{
			((Selector?)null).OfType(typeof(ComboBox)).Class("base").Class("ghost")
				.Class(":pointerover")
				.Template()
				.OfType(typeof(Border))
				.Name("Background"),
			((Selector?)null).OfType(typeof(ComboBox)).Class("base").Class("ghost")
				.Class(":pressed")
				.Template()
				.OfType(typeof(Border))
				.Name("Background")
		});
		Setter setter32 = new Setter();
		setter32.Property = Border.BackgroundProperty;
		setter32.Value = new ImmutableSolidColorBrush(16777215u);
		style11.Add(setter32);
		styles.Add(style11);
		Style style12 = new Style();
		style12.Selector = ((Selector?)null).OfType(typeof(ComboBox)).Class("base").Class("ghost")
			.Template()
			.OfType(typeof(ContentControl))
			.Name("ContentPresenter");
		Setter setter33 = new Setter();
		setter33.Property = Layoutable.MarginProperty;
		setter33.Value = new Thickness(6.0, 0.0, -15.0, 0.0);
		style12.Add(setter33);
		styles.Add(style12);
		Style item5 = (style5 = new Style());
		context.PushParent(style5);
		Style style13 = style5;
		style13.Selector = ((Selector?)null).OfType(typeof(ComboBox)).Class("base").Template()
			.OfType(typeof(PathIcon))
			.Name("DropDownGlyph");
		Setter setter34 = (setter9 = new Setter());
		context.PushParent(setter9);
		Setter setter35 = setter9;
		setter35.Property = PathIcon.DataProperty;
		StaticResourceExtension staticResourceExtension5 = new StaticResourceExtension("ComboBoxDownGlyph");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
		object? value8 = staticResourceExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		setter35.Value = value8;
		context.PopParent();
		style13.Add(setter34);
		Setter setter36 = new Setter();
		setter36.Property = Layoutable.WidthProperty;
		setter36.Value = 19.2;
		style13.Add(setter36);
		Setter setter37 = new Setter();
		setter37.Property = Layoutable.HeightProperty;
		setter37.Value = 19.2;
		style13.Add(setter37);
		Setter setter38 = new Setter();
		setter38.Property = Layoutable.MarginProperty;
		setter38.Value = new Thickness(0.0, 0.0, 12.0, 0.0);
		style13.Add(setter38);
		Setter setter39 = new Setter();
		setter39.Property = TemplatedControl.ForegroundProperty;
		setter39.Value = new ImmutableSolidColorBrush(uint.MaxValue);
		style13.Add(setter39);
		Setter setter40 = new Setter();
		setter40.Property = Visual.OpacityProperty;
		setter40.Value = 0.64;
		style13.Add(setter40);
		context.PopParent();
		styles.Add(item5);
		Style item6 = (style5 = new Style());
		context.PushParent(style5);
		Style style14 = style5;
		style14.Selector = ((Selector?)null).OfType(typeof(ComboBox)).Class("base").Class("ghost")
			.Template()
			.OfType(typeof(PathIcon))
			.Name("DropDownGlyph");
		Setter setter41 = (setter9 = new Setter());
		context.PushParent(setter9);
		Setter setter42 = setter9;
		setter42.Property = PathIcon.DataProperty;
		StaticResourceExtension staticResourceExtension6 = new StaticResourceExtension("ComboBoxRightGlyph");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
		object? value9 = staticResourceExtension6.ProvideValue(context);
		context.ProvideTargetProperty = null;
		setter42.Value = value9;
		context.PopParent();
		style14.Add(setter41);
		Setter setter43 = new Setter();
		setter43.Property = Layoutable.WidthProperty;
		setter43.Value = 7.0;
		style14.Add(setter43);
		Setter setter44 = new Setter();
		setter44.Property = Layoutable.HeightProperty;
		setter44.Value = 11.0;
		style14.Add(setter44);
		Setter setter45 = new Setter();
		setter45.Property = Layoutable.MarginProperty;
		setter45.Value = new Thickness(0.0, 0.0, 6.0, 0.0);
		style14.Add(setter45);
		context.PopParent();
		styles.Add(item6);
		Style item7 = (style5 = new Style());
		context.PushParent(style5);
		Style style15 = style5;
		style15.Selector = ((Selector?)null).OfType(typeof(ComboBox)).Class("base").Descendant()
			.OfType(typeof(ComboBoxItem));
		Setter setter46 = new Setter();
		setter46.Property = TemplatedControl.FontSizeProperty;
		setter46.Value = 14.0;
		style15.Add(setter46);
		Setter setter47 = new Setter();
		setter47.Property = TemplatedControl.FontWeightProperty;
		setter47.Value = FontWeight.Medium;
		style15.Add(setter47);
		Setter setter48 = new Setter();
		setter48.Property = TemplatedControl.PaddingProperty;
		setter48.Value = new Thickness(6.0, 4.0, 6.0, 4.0);
		style15.Add(setter48);
		Setter setter49 = new Setter();
		setter49.Property = TemplatedControl.CornerRadiusProperty;
		setter49.Value = new CornerRadius(4.0, 4.0, 4.0, 4.0);
		style15.Add(setter49);
		Setter setter50 = new Setter();
		setter50.Property = ContentControl.VerticalContentAlignmentProperty;
		setter50.Value = VerticalAlignment.Center;
		style15.Add(setter50);
		Setter setter51 = (setter9 = new Setter());
		context.PushParent(setter9);
		Setter setter52 = setter9;
		setter52.Property = TemplatedControl.TemplateProperty;
		ControlTemplate controlTemplate;
		ControlTemplate value10 = (controlTemplate = new ControlTemplate());
		context.PushParent(controlTemplate);
		controlTemplate.Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_3.Build_8), context);
		context.PopParent();
		setter52.Value = value10;
		context.PopParent();
		style15.Add(setter51);
		context.PopParent();
		styles.Add(item7);
		Style style16 = new Style();
		style16.Selector = ((Selector?)null).OfType(typeof(ComboBox)).Class("base").Descendant()
			.OfType(typeof(ComboBoxItem))
			.NthLastChild(0, 1)
			.Template()
			.OfType(typeof(Border))
			.Name("DividerBorder");
		Setter setter53 = new Setter();
		setter53.Property = Visual.IsVisibleProperty;
		setter53.Value = false;
		style16.Add(setter53);
		styles.Add(style16);
		context.PopParent();
		if (styles2 is StyledElement styled)
		{
			NameScope.SetNameScope(styled, context.AvaloniaNameScope);
		}
		context.AvaloniaNameScope.Complete();
	}

	public static Styles Build_003A_002FControls_002FComboBox_002Eaxaml(IServiceProvider P_0)
	{
		Styles styles = new Styles();
		Populate_003A_002FControls_002FComboBox_002Eaxaml(P_0, styles);
		return styles;
	}

	public unsafe static void Populate_003A_002FControls_002FToggleSwitch_002Eaxaml(IServiceProvider P_0, Styles P_1)
	{
		CompiledAvaloniaXaml.XamlIlContext.Context<Styles> context = new CompiledAvaloniaXaml.XamlIlContext.Context<Styles>(P_0, new object[1] { NamespaceInfo_003A_002FControls_002FToggleSwitch_002Eaxaml.Singleton }, "avares://SpaceWalker/Controls/ToggleSwitch.axaml")
		{
			RootObject = P_1,
			IntermediateRoot = P_1
		};
		Styles styles;
		Styles styles2 = (styles = P_1);
		context.PushParent(styles);
		Style style = new Style();
		style.Selector = ((Selector?)null).OfType(typeof(ToggleSwitch)).Class("base");
		Setter setter = new Setter();
		setter.Property = TemplatedControl.ForegroundProperty;
		setter.Value = new ImmutableSolidColorBrush(uint.MaxValue);
		style.Add(setter);
		Setter setter2 = new Setter();
		setter2.Property = InputElement.CursorProperty;
		setter2.Value = new Cursor(StandardCursorType.Hand);
		style.Add(setter2);
		Setter setter3 = new Setter();
		setter3.Property = TemplatedControl.TemplateProperty;
		setter3.Value = new ControlTemplate
		{
			Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_11.Build_1), context)
		};
		style.Add(setter3);
		styles.Add(style);
		Style style2 = new Style();
		style2.Selector = ((Selector?)null).OfType(typeof(ToggleSwitch)).Class("base").Class(":pointerover")
			.Template()
			.OfType(typeof(Border))
			.Name("SwitchTrack");
		Setter setter4 = new Setter();
		setter4.Property = Border.BackgroundProperty;
		setter4.Value = new ImmutableSolidColorBrush(1728053247u);
		style2.Add(setter4);
		styles.Add(style2);
		Style style3 = new Style();
		style3.Selector = ((Selector?)null).OfType(typeof(ToggleSwitch)).Class("base").Class(":pressed")
			.Template()
			.OfType(typeof(Border))
			.Name("SwitchTrack");
		Setter setter5 = new Setter();
		setter5.Property = Border.BackgroundProperty;
		setter5.Value = new ImmutableSolidColorBrush(1040187391u);
		style3.Add(setter5);
		styles.Add(style3);
		Style style4;
		Style item = (style4 = new Style());
		context.PushParent(style4);
		Style style5 = style4;
		style5.Selector = ((Selector?)null).OfType(typeof(ToggleSwitch)).Class("base").Class(":checked")
			.Template()
			.OfType(typeof(Border))
			.Name("SwitchTrack");
		Setter setter6;
		Setter setter7 = (setter6 = new Setter());
		context.PushParent(setter6);
		Setter setter8 = setter6;
		setter8.Property = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension = new DynamicResourceExtension("SystemAccentColorBrush");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
		BindingBase value = dynamicResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		setter8.Value = value;
		context.PopParent();
		style5.Add(setter7);
		context.PopParent();
		styles.Add(item);
		Style item2 = (style4 = new Style());
		context.PushParent(style4);
		Style style6 = style4;
		style6.Selector = ((Selector?)null).OfType(typeof(ToggleSwitch)).Class("base").Class(":checked")
			.Class(":pointerover")
			.Template()
			.OfType(typeof(Border))
			.Name("SwitchTrack");
		Setter setter9 = (setter6 = new Setter());
		context.PushParent(setter6);
		Setter setter10 = setter6;
		setter10.Property = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension2 = new DynamicResourceExtension("SystemAccentColorLight1Brush");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
		BindingBase value2 = dynamicResourceExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		setter10.Value = value2;
		context.PopParent();
		style6.Add(setter9);
		context.PopParent();
		styles.Add(item2);
		Style item3 = (style4 = new Style());
		context.PushParent(style4);
		Style style7 = style4;
		style7.Selector = ((Selector?)null).OfType(typeof(ToggleSwitch)).Class("base").Class(":checked")
			.Class(":pressed")
			.Template()
			.OfType(typeof(Border))
			.Name("SwitchTrack");
		Setter setter11 = (setter6 = new Setter());
		context.PushParent(setter6);
		Setter setter12 = setter6;
		setter12.Property = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension3 = new DynamicResourceExtension("SystemAccentColorDark1Brush");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
		BindingBase value3 = dynamicResourceExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		setter12.Value = value3;
		context.PopParent();
		style7.Add(setter11);
		context.PopParent();
		styles.Add(item3);
		Style style8 = new Style();
		style8.Selector = ((Selector?)null).OfType(typeof(ToggleSwitch)).Class("base").Class(":disabled");
		Setter setter13 = new Setter();
		setter13.Property = Visual.OpacityProperty;
		setter13.Value = 0.4;
		style8.Add(setter13);
		styles.Add(style8);
		context.PopParent();
		if (styles2 is StyledElement styled)
		{
			NameScope.SetNameScope(styled, context.AvaloniaNameScope);
		}
		context.AvaloniaNameScope.Complete();
	}

	public static Styles Build_003A_002FControls_002FToggleSwitch_002Eaxaml(IServiceProvider P_0)
	{
		Styles styles = new Styles();
		Populate_003A_002FControls_002FToggleSwitch_002Eaxaml(P_0, styles);
		return styles;
	}

	public unsafe static void Populate_003A_002FControls_002FButton_002Eaxaml(IServiceProvider P_0, Styles P_1)
	{
		CompiledAvaloniaXaml.XamlIlContext.Context<Styles> context = new CompiledAvaloniaXaml.XamlIlContext.Context<Styles>(P_0, new object[1] { NamespaceInfo_003A_002FControls_002FButton_002Eaxaml.Singleton }, "avares://SpaceWalker/Controls/Button.axaml")
		{
			RootObject = P_1,
			IntermediateRoot = P_1
		};
		Styles styles;
		Styles styles2 = (styles = P_1);
		context.PushParent(styles);
		Style style = new Style();
		style.Selector = ((Selector?)null).OfType(typeof(Button)).Class("base");
		Setter setter = new Setter();
		setter.Property = TemplatedControl.ForegroundProperty;
		setter.Value = new ImmutableSolidColorBrush(uint.MaxValue);
		style.Add(setter);
		Setter setter2 = new Setter();
		setter2.Property = TemplatedControl.PaddingProperty;
		setter2.Value = new Thickness(20.0, 8.0, 20.0, 8.0);
		style.Add(setter2);
		Setter setter3 = new Setter();
		setter3.Property = TemplatedControl.FontWeightProperty;
		setter3.Value = FontWeight.DemiBold;
		style.Add(setter3);
		Setter setter4 = new Setter();
		setter4.Property = InputElement.CursorProperty;
		setter4.Value = new Cursor(StandardCursorType.Hand);
		style.Add(setter4);
		Setter setter5 = new Setter();
		setter5.Property = ContentControl.HorizontalContentAlignmentProperty;
		setter5.Value = HorizontalAlignment.Center;
		style.Add(setter5);
		Setter setter6 = new Setter();
		setter6.Property = ContentControl.VerticalContentAlignmentProperty;
		setter6.Value = VerticalAlignment.Center;
		style.Add(setter6);
		Setter setter7 = new Setter();
		setter7.Property = TemplatedControl.TemplateProperty;
		setter7.Value = new ControlTemplate
		{
			Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_1.Build_1), context)
		};
		style.Add(setter7);
		styles.Add(style);
		Style style2 = new Style();
		style2.Selector = ((Selector?)null).OfType(typeof(Button)).Class("base").Class(":pointerover")
			.Template()
			.OfType(typeof(Border))
			.Name("PART_Border");
		Setter setter8 = new Setter();
		setter8.Property = Border.BackgroundProperty;
		setter8.Value = new ImmutableSolidColorBrush(1308622847u);
		style2.Add(setter8);
		styles.Add(style2);
		Style style3 = new Style();
		style3.Selector = ((Selector?)null).OfType(typeof(Button)).Class("base").Class(":pressed")
			.Template()
			.OfType(typeof(Border))
			.Name("PART_Border");
		Setter setter9 = new Setter();
		setter9.Property = Border.BackgroundProperty;
		setter9.Value = new ImmutableSolidColorBrush(1308622847u);
		style3.Add(setter9);
		styles.Add(style3);
		Style style4 = new Style();
		style4.Selector = ((Selector?)null).OfType(typeof(Button)).Class("base").Class(":disabled");
		Setter setter10 = new Setter();
		setter10.Property = Visual.OpacityProperty;
		setter10.Value = 0.5;
		style4.Add(setter10);
		styles.Add(style4);
		Style style5;
		Style item = (style5 = new Style());
		context.PushParent(style5);
		Style style6 = style5;
		style6.Selector = ((Selector?)null).OfType(typeof(Button)).Class("base").Class("accent")
			.Template()
			.OfType(typeof(Border))
			.Name("PART_Border");
		Setter setter11;
		Setter setter12 = (setter11 = new Setter());
		context.PushParent(setter11);
		Setter setter13 = setter11;
		setter13.Property = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension = new DynamicResourceExtension("AccentButtonBackgroundBrush");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
		BindingBase value = dynamicResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		setter13.Value = value;
		context.PopParent();
		style6.Add(setter12);
		Setter setter14 = (setter11 = new Setter());
		context.PushParent(setter11);
		Setter setter15 = setter11;
		setter15.Property = Border.BorderBrushProperty;
		DynamicResourceExtension dynamicResourceExtension2 = new DynamicResourceExtension("AccentButtonBackgroundBrush");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
		BindingBase value2 = dynamicResourceExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		setter15.Value = value2;
		context.PopParent();
		style6.Add(setter14);
		Setter setter16 = new Setter();
		setter16.Property = Border.BoxShadowProperty;
		setter16.Value = BoxShadows.Parse("0 0 5 0 #1A000000");
		style6.Add(setter16);
		context.PopParent();
		styles.Add(item);
		Style item2 = (style5 = new Style());
		context.PushParent(style5);
		Style style7 = style5;
		style7.Selector = ((Selector?)null).OfType(typeof(Button)).Class("base").Class("accent")
			.Class(":pointerover")
			.Template()
			.OfType(typeof(Border))
			.Name("PART_Border");
		Setter setter17 = (setter11 = new Setter());
		context.PushParent(setter11);
		Setter setter18 = setter11;
		setter18.Property = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension3 = new DynamicResourceExtension("AccentButtonBackgroundHoverBrush");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
		BindingBase value3 = dynamicResourceExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		setter18.Value = value3;
		context.PopParent();
		style7.Add(setter17);
		Setter setter19 = (setter11 = new Setter());
		context.PushParent(setter11);
		Setter setter20 = setter11;
		setter20.Property = Border.BorderBrushProperty;
		DynamicResourceExtension dynamicResourceExtension4 = new DynamicResourceExtension("AccentButtonBackgroundHoverBrush");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
		BindingBase value4 = dynamicResourceExtension4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		setter20.Value = value4;
		context.PopParent();
		style7.Add(setter19);
		context.PopParent();
		styles.Add(item2);
		Style item3 = (style5 = new Style());
		context.PushParent(style5);
		Style style8 = style5;
		style8.Selector = ((Selector?)null).OfType(typeof(Button)).Class("base").Class("accent")
			.Class(":pressed")
			.Template()
			.OfType(typeof(Border))
			.Name("PART_Border");
		Setter setter21 = (setter11 = new Setter());
		context.PushParent(setter11);
		Setter setter22 = setter11;
		setter22.Property = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension5 = new DynamicResourceExtension("AccentButtonBackgroundPressedBrush");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
		BindingBase value5 = dynamicResourceExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		setter22.Value = value5;
		context.PopParent();
		style8.Add(setter21);
		Setter setter23 = (setter11 = new Setter());
		context.PushParent(setter11);
		Setter setter24 = setter11;
		setter24.Property = Border.BorderBrushProperty;
		DynamicResourceExtension dynamicResourceExtension6 = new DynamicResourceExtension("AccentButtonBackgroundPressedBrush");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
		BindingBase value6 = dynamicResourceExtension6.ProvideValue(context);
		context.ProvideTargetProperty = null;
		setter24.Value = value6;
		context.PopParent();
		style8.Add(setter23);
		context.PopParent();
		styles.Add(item3);
		Style style9 = new Style();
		style9.Selector = ((Selector?)null).OfType(typeof(Button)).Class("base").Class("accent")
			.Class(":disabled");
		Setter setter25 = new Setter();
		setter25.Property = Visual.OpacityProperty;
		setter25.Value = 1.0;
		style9.Add(setter25);
		styles.Add(style9);
		Style style10 = new Style();
		style10.Selector = ((Selector?)null).OfType(typeof(Button)).Class("base").Class("accent")
			.Class(":disabled")
			.Template()
			.OfType(typeof(Border))
			.Name("PART_Border");
		Setter setter26 = new Setter();
		setter26.Property = Border.BackgroundProperty;
		setter26.Value = new ImmutableSolidColorBrush(872415231u);
		style10.Add(setter26);
		Setter setter27 = new Setter();
		setter27.Property = Border.BorderBrushProperty;
		setter27.Value = new ImmutableSolidColorBrush(452984831u);
		style10.Add(setter27);
		Setter setter28 = new Setter();
		setter28.Property = Border.BoxShadowProperty;
		setter28.Value = BoxShadows.Parse("0 0 5 0 #1A000000");
		style10.Add(setter28);
		styles.Add(style10);
		Style style11 = new Style();
		style11.Selector = ((Selector?)null).Is(typeof(Button)).Class("flat");
		Setter setter29 = new Setter();
		setter29.Property = TemplatedControl.ForegroundProperty;
		setter29.Value = new ImmutableSolidColorBrush(uint.MaxValue);
		style11.Add(setter29);
		Setter setter30 = new Setter();
		setter30.Property = TemplatedControl.PaddingProperty;
		setter30.Value = new Thickness(20.0, 8.0, 20.0, 8.0);
		style11.Add(setter30);
		Setter setter31 = new Setter();
		setter31.Property = TemplatedControl.FontWeightProperty;
		setter31.Value = FontWeight.DemiBold;
		style11.Add(setter31);
		Setter setter32 = new Setter();
		setter32.Property = InputElement.CursorProperty;
		setter32.Value = new Cursor(StandardCursorType.Hand);
		style11.Add(setter32);
		Setter setter33 = new Setter();
		setter33.Property = ContentControl.HorizontalContentAlignmentProperty;
		setter33.Value = HorizontalAlignment.Center;
		style11.Add(setter33);
		Setter setter34 = new Setter();
		setter34.Property = ContentControl.VerticalContentAlignmentProperty;
		setter34.Value = VerticalAlignment.Center;
		style11.Add(setter34);
		Setter setter35 = new Setter();
		setter35.Property = TemplatedControl.TemplateProperty;
		setter35.Value = new ControlTemplate
		{
			Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_1.Build_2), context)
		};
		style11.Add(setter35);
		styles.Add(style11);
		Style style12 = new Style();
		style12.Selector = ((Selector?)null).Is(typeof(Button)).Class("flat").Class(":pointerover")
			.Template()
			.OfType(typeof(Border))
			.Name("PART_Border");
		Setter setter36 = new Setter();
		setter36.Property = Visual.OpacityProperty;
		setter36.Value = 0.8;
		style12.Add(setter36);
		styles.Add(style12);
		Style style13 = new Style();
		style13.Selector = ((Selector?)null).Is(typeof(Button)).Class("flat").Class(":pressed")
			.Template()
			.OfType(typeof(Border))
			.Name("PART_Border");
		Setter setter37 = new Setter();
		setter37.Property = Visual.OpacityProperty;
		setter37.Value = 0.9;
		style13.Add(setter37);
		styles.Add(style13);
		Style style14 = new Style();
		style14.Selector = ((Selector?)null).Is(typeof(Button)).Class("flat").Class(":disabled");
		Setter setter38 = new Setter();
		setter38.Property = Visual.OpacityProperty;
		setter38.Value = 0.5;
		style14.Add(setter38);
		styles.Add(style14);
		Style style15 = new Style();
		style15.Selector = ((Selector?)null).OfType(typeof(Button)).Class("textboxclear");
		Setter setter39 = new Setter();
		setter39.Property = Layoutable.WidthProperty;
		setter39.Value = 16.0;
		style15.Add(setter39);
		Setter setter40 = new Setter();
		setter40.Property = Layoutable.HeightProperty;
		setter40.Value = 16.0;
		style15.Add(setter40);
		Setter setter41 = new Setter();
		setter41.Property = TemplatedControl.CornerRadiusProperty;
		setter41.Value = new CornerRadius(8.0, 8.0, 8.0, 8.0);
		style15.Add(setter41);
		Setter setter42 = new Setter();
		setter42.Property = TemplatedControl.PaddingProperty;
		setter42.Value = new Thickness(0.0, 0.0, 0.0, 0.0);
		style15.Add(setter42);
		Setter setter43 = new Setter();
		setter43.Property = Layoutable.MarginProperty;
		setter43.Value = new Thickness(0.0, 0.0, 8.0, 0.0);
		style15.Add(setter43);
		Setter setter44 = new Setter();
		setter44.Property = Layoutable.VerticalAlignmentProperty;
		setter44.Value = VerticalAlignment.Center;
		style15.Add(setter44);
		Setter setter45 = new Setter();
		setter45.Property = InputElement.FocusableProperty;
		setter45.Value = false;
		style15.Add(setter45);
		Setter setter46 = new Setter();
		setter46.Property = InputElement.CursorProperty;
		setter46.Value = new Cursor(StandardCursorType.Hand);
		style15.Add(setter46);
		Setter setter47 = new Setter();
		setter47.Property = TemplatedControl.TemplateProperty;
		setter47.Value = new ControlTemplate
		{
			Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_1.Build_3), context)
		};
		style15.Add(setter47);
		styles.Add(style15);
		Style style16 = new Style();
		style16.Selector = ((Selector?)null).OfType(typeof(Button)).Class("textboxclear").Class(":pointerover")
			.Template()
			.OfType(typeof(Border))
			.Name("PART_Border");
		Setter setter48 = new Setter();
		setter48.Property = Border.BackgroundProperty;
		setter48.Value = new ImmutableSolidColorBrush(872415231u);
		style16.Add(setter48);
		styles.Add(style16);
		Style style17 = new Style();
		style17.Selector = ((Selector?)null).OfType(typeof(Button)).Class("textboxclear").Class(":pressed")
			.Template()
			.OfType(typeof(Border))
			.Name("PART_Border");
		Setter setter49 = new Setter();
		setter49.Property = Border.BackgroundProperty;
		setter49.Value = new ImmutableSolidColorBrush(704643071u);
		style17.Add(setter49);
		styles.Add(style17);
		context.PopParent();
		if (styles2 is StyledElement styled)
		{
			NameScope.SetNameScope(styled, context.AvaloniaNameScope);
		}
		context.AvaloniaNameScope.Complete();
	}

	public static Styles Build_003A_002FControls_002FButton_002Eaxaml(IServiceProvider P_0)
	{
		Styles styles = new Styles();
		Populate_003A_002FControls_002FButton_002Eaxaml(P_0, styles);
		return styles;
	}

	public static void Populate_003A_002FControls_002FTextStyles_002Eaxaml(IServiceProvider P_0, Styles P_1)
	{
		CompiledAvaloniaXaml.XamlIlContext.Context<Styles> context = new CompiledAvaloniaXaml.XamlIlContext.Context<Styles>(P_0, new object[1] { NamespaceInfo_003A_002FControls_002FTextStyles_002Eaxaml.Singleton }, "avares://SpaceWalker/Controls/TextStyles.axaml")
		{
			RootObject = P_1,
			IntermediateRoot = P_1
		};
		Styles styles;
		Styles styles2 = (styles = P_1);
		context.PushParent(styles);
		Style style;
		Style item = (style = new Style());
		context.PushParent(style);
		Style style2 = style;
		style2.Selector = ((Selector?)null).OfType(typeof(TextBlock)).Class("textdisplay");
		Setter setter = new Setter();
		setter.Property = TextBlock.FontSizeProperty;
		setter.Value = 24.0;
		style2.Add(setter);
		Setter setter2 = new Setter();
		setter2.Property = TextBlock.FontWeightProperty;
		setter2.Value = FontWeight.DemiBold;
		style2.Add(setter2);
		Setter setter3;
		Setter setter4 = (setter3 = new Setter());
		context.PushParent(setter3);
		Setter setter5 = setter3;
		setter5.Property = TextBlock.FontFamilyProperty;
		DynamicResourceExtension dynamicResourceExtension = new DynamicResourceExtension("HeadlineFontFamily");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
		BindingBase value = dynamicResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		setter5.Value = value;
		context.PopParent();
		style2.Add(setter4);
		context.PopParent();
		styles.Add(item);
		Style style3 = new Style();
		style3.Selector = ((Selector?)null).OfType(typeof(TextBlock)).Class("textheadline");
		Setter setter6 = new Setter();
		setter6.Property = TextBlock.FontSizeProperty;
		setter6.Value = 18.0;
		style3.Add(setter6);
		Setter setter7 = new Setter();
		setter7.Property = TextBlock.FontWeightProperty;
		setter7.Value = FontWeight.DemiBold;
		style3.Add(setter7);
		styles.Add(style3);
		Style style4 = new Style();
		style4.Selector = ((Selector?)null).OfType(typeof(TextBlock)).Class("texttitle");
		Setter setter8 = new Setter();
		setter8.Property = TextBlock.FontSizeProperty;
		setter8.Value = 16.0;
		style4.Add(setter8);
		Setter setter9 = new Setter();
		setter9.Property = TextBlock.FontWeightProperty;
		setter9.Value = FontWeight.DemiBold;
		style4.Add(setter9);
		styles.Add(style4);
		Style style5 = new Style();
		style5.Selector = ((Selector?)null).OfType(typeof(TextBlock)).Class("textbody");
		Setter setter10 = new Setter();
		setter10.Property = TextBlock.FontSizeProperty;
		setter10.Value = 14.0;
		style5.Add(setter10);
		Setter setter11 = new Setter();
		setter11.Property = TextBlock.FontWeightProperty;
		setter11.Value = FontWeight.Medium;
		style5.Add(setter11);
		styles.Add(style5);
		Style style6 = new Style();
		style6.Selector = ((Selector?)null).OfType(typeof(TextBlock)).Class("textlabel");
		Setter setter12 = new Setter();
		setter12.Property = TextBlock.FontSizeProperty;
		setter12.Value = 14.0;
		style6.Add(setter12);
		Setter setter13 = new Setter();
		setter13.Property = TextBlock.FontWeightProperty;
		setter13.Value = FontWeight.DemiBold;
		style6.Add(setter13);
		styles.Add(style6);
		Style style7 = new Style();
		style7.Selector = ((Selector?)null).OfType(typeof(TextBlock)).Class("textcaption");
		Setter setter14 = new Setter();
		setter14.Property = TextBlock.FontSizeProperty;
		setter14.Value = 12.0;
		style7.Add(setter14);
		Setter setter15 = new Setter();
		setter15.Property = TextBlock.FontWeightProperty;
		setter15.Value = FontWeight.Medium;
		style7.Add(setter15);
		styles.Add(style7);
		Style style8 = new Style();
		style8.Selector = ((Selector?)null).OfType(typeof(Border)).Class("base");
		Setter setter16 = new Setter();
		setter16.Property = Border.CornerRadiusProperty;
		setter16.Value = new CornerRadius(4.0, 4.0, 4.0, 4.0);
		style8.Add(setter16);
		Setter setter17 = new Setter();
		setter17.Property = Border.BackgroundProperty;
		setter17.Value = new ImmutableSolidColorBrush(436207616u);
		style8.Add(setter17);
		styles.Add(style8);
		Style style9 = new Style();
		style9.Selector = ((Selector?)null).OfType(typeof(TextBlock)).Class("base");
		Setter setter18 = new Setter();
		setter18.Property = Layoutable.VerticalAlignmentProperty;
		setter18.Value = VerticalAlignment.Center;
		style9.Add(setter18);
		Setter setter19 = new Setter();
		setter19.Property = TextBlock.ForegroundProperty;
		setter19.Value = new ImmutableSolidColorBrush(uint.MaxValue);
		style9.Add(setter19);
		styles.Add(style9);
		Style style10 = new Style();
		style10.Selector = Selectors.Or(new List<Selector>
		{
			((Selector?)null).OfType(typeof(TextBlock)).Class("base"),
			((Selector?)null).OfType(typeof(CheckBox)).Class("base").Template()
				.OfType(typeof(ContentPresenter))
				.Name("PART_ContentPresenter"),
			((Selector?)null).OfType(typeof(RadioButton)).Class("base").Template()
				.OfType(typeof(ContentPresenter))
				.Name("PART_ContentPresenter"),
			((Selector?)null).OfType(typeof(ComboBox)).Class("base").Descendant()
				.OfType(typeof(TextBlock))
		});
		Setter setter20 = new Setter();
		setter20.Property = Visual.EffectProperty;
		DropShadowEffect dropShadowEffect = new DropShadowEffect();
		dropShadowEffect.BlurRadius = 3.0;
		dropShadowEffect.OffsetX = 0.0;
		dropShadowEffect.OffsetY = 0.0;
		dropShadowEffect.Color = Color.FromUInt32(4278190080u);
		dropShadowEffect.Opacity = 0.4;
		setter20.Value = dropShadowEffect;
		style10.Add(setter20);
		styles.Add(style10);
		Style style11 = new Style();
		style11.Selector = ((Selector?)null).OfType(typeof(TextBox)).Class("base");
		Setter setter21 = new Setter();
		setter21.Property = TemplatedControl.ForegroundProperty;
		setter21.Value = new ImmutableSolidColorBrush(uint.MaxValue);
		style11.Add(setter21);
		styles.Add(style11);
		Style item2 = (style = new Style());
		context.PushParent(style);
		Style style12 = style;
		style12.Selector = Selectors.Or(new List<Selector>
		{
			((Selector?)null).OfType(typeof(TextBox)).Class("base").Class(":pointerover")
				.Template()
				.OfType(typeof(Border))
				.Name("PART_BorderElement"),
			((Selector?)null).OfType(typeof(TextBox)).Class("base").Class(":focus")
				.Template()
				.OfType(typeof(Border))
				.Name("PART_BorderElement"),
			((Selector?)null).OfType(typeof(TextBox)).Class("base").Class(":focus-within")
				.Template()
				.OfType(typeof(Border))
				.Name("PART_BorderElement")
		});
		Setter setter22 = (setter3 = new Setter());
		context.PushParent(setter3);
		Setter setter23 = setter3;
		setter23.Property = Border.BackgroundProperty;
		ReflectionBindingExtension reflectionBindingExtension = new ReflectionBindingExtension("$parent[TextBox].Background");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
		ReflectionBinding value2 = reflectionBindingExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		setter23.Value = value2;
		context.PopParent();
		style12.Add(setter22);
		Setter setter24 = (setter3 = new Setter());
		context.PushParent(setter3);
		Setter setter25 = setter3;
		setter25.Property = Border.BorderBrushProperty;
		ReflectionBindingExtension reflectionBindingExtension2 = new ReflectionBindingExtension("$parent[TextBox].BorderBrush");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
		ReflectionBinding value3 = reflectionBindingExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		setter25.Value = value3;
		context.PopParent();
		style12.Add(setter24);
		Setter setter26 = (setter3 = new Setter());
		context.PushParent(setter3);
		Setter setter27 = setter3;
		setter27.Property = Border.BorderThicknessProperty;
		ReflectionBindingExtension reflectionBindingExtension3 = new ReflectionBindingExtension("$parent[TextBox].BorderThickness");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
		ReflectionBinding value4 = reflectionBindingExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		setter27.Value = value4;
		context.PopParent();
		style12.Add(setter26);
		context.PopParent();
		styles.Add(item2);
		context.PopParent();
		if (styles2 is StyledElement styled)
		{
			NameScope.SetNameScope(styled, context.AvaloniaNameScope);
		}
		context.AvaloniaNameScope.Complete();
	}

	public static Styles Build_003A_002FControls_002FTextStyles_002Eaxaml(IServiceProvider P_0)
	{
		Styles styles = new Styles();
		Populate_003A_002FControls_002FTextStyles_002Eaxaml(P_0, styles);
		return styles;
	}

	public unsafe static void Populate_003A_002FControls_002FScrollViewer_002Eaxaml(IServiceProvider P_0, Styles P_1)
	{
		CompiledAvaloniaXaml.XamlIlContext.Context<Styles> context = new CompiledAvaloniaXaml.XamlIlContext.Context<Styles>(P_0, new object[1] { NamespaceInfo_003A_002FControls_002FScrollViewer_002Eaxaml.Singleton }, "avares://SpaceWalker/Controls/ScrollViewer.axaml")
		{
			RootObject = P_1,
			IntermediateRoot = P_1
		};
		Style style = new Style();
		style.Selector = ((Selector?)null).OfType(typeof(ScrollViewer)).Class("base");
		Setter setter = new Setter();
		setter.Property = TemplatedControl.TemplateProperty;
		setter.Value = new ControlTemplate
		{
			Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_8.Build_1), context)
		};
		style.Add(setter);
		P_1.Add(style);
		Style style2 = new Style();
		style2.Selector = ((Selector?)null).OfType(typeof(ScrollViewer)).Class("base").Template()
			.OfType(typeof(ScrollBar))
			.PropertyEquals((AvaloniaProperty)ScrollBar.OrientationProperty, (object?)Orientation.Vertical);
		Setter setter2 = new Setter();
		setter2.Property = Layoutable.HeightProperty;
		setter2.Value = 80.0;
		style2.Add(setter2);
		Setter setter3 = new Setter();
		setter3.Property = Layoutable.WidthProperty;
		setter3.Value = 14.0;
		style2.Add(setter3);
		Setter setter4 = new Setter();
		setter4.Property = TemplatedControl.TemplateProperty;
		setter4.Value = new ControlTemplate
		{
			Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_8.Build_2), context)
		};
		style2.Add(setter4);
		P_1.Add(style2);
		if (P_1 is StyledElement styled)
		{
			NameScope.SetNameScope(styled, context.AvaloniaNameScope);
		}
		context.AvaloniaNameScope.Complete();
	}

	public static Styles Build_003A_002FControls_002FScrollViewer_002Eaxaml(IServiceProvider P_0)
	{
		Styles styles = new Styles();
		Populate_003A_002FControls_002FScrollViewer_002Eaxaml(P_0, styles);
		return styles;
	}

	public unsafe static void Populate_003A_002FControls_002FIconLabel_002Eaxaml(IServiceProvider P_0, Styles P_1)
	{
		CompiledAvaloniaXaml.XamlIlContext.Context<Styles> context = new CompiledAvaloniaXaml.XamlIlContext.Context<Styles>(P_0, new object[1] { NamespaceInfo_003A_002FControls_002FIconLabel_002Eaxaml.Singleton }, "avares://SpaceWalker/Controls/IconLabel.axaml")
		{
			RootObject = P_1,
			IntermediateRoot = P_1
		};
		ResourceDictionary resourceDictionary = new ResourceDictionary();
		resourceDictionary.AddDeferred(typeof(IconLabel), XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_5.Build_1), context));
		P_1.Resources = resourceDictionary;
		if (P_1 is StyledElement styled)
		{
			NameScope.SetNameScope(styled, context.AvaloniaNameScope);
		}
		context.AvaloniaNameScope.Complete();
	}

	public static Styles Build_003A_002FControls_002FIconLabel_002Eaxaml(IServiceProvider P_0)
	{
		Styles styles = new Styles();
		Populate_003A_002FControls_002FIconLabel_002Eaxaml(P_0, styles);
		return styles;
	}
}
