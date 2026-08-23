using System;
using System.Runtime.CompilerServices;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Styling;

namespace CompiledAvaloniaXaml;

[CompilerGenerated]
internal class XamlDynamicSetters
{
	public static void _003C_003EXamlDynamicSetter_1(ContentPresenter P_0, BindingPriority P_1, BindingBase P_2)
	{
		if (P_2 != null)
		{
			BindingBase binding = P_2;
			P_0.Bind(ContentPresenter.ContentProperty, binding);
		}
		else
		{
			object value = P_2;
			int priority = (int)P_1;
			P_0.SetValue(ContentPresenter.ContentProperty, value, (BindingPriority)priority);
		}
	}

	public static void _003C_003EXamlDynamicSetter_2(Border P_0, BindingPriority P_1, object P_2)
	{
		if (P_2 is BindingBase)
		{
			BindingBase binding = (BindingBase)P_2;
			P_0.Bind(Border.BackgroundProperty, binding);
			return;
		}
		if (P_2 is IBrush)
		{
			IBrush value = (IBrush)P_2;
			int priority = (int)P_1;
			P_0.SetValue(Border.BackgroundProperty, value, (BindingPriority)priority);
			return;
		}
		if (P_2 == null)
		{
			IBrush value = (IBrush)P_2;
			int priority = (int)P_1;
			P_0.SetValue(Border.BackgroundProperty, value, (BindingPriority)priority);
			return;
		}
		throw new InvalidCastException();
	}

	public static void _003C_003EXamlDynamicSetter_3(Border P_0, BindingPriority P_1, object P_2)
	{
		if (P_2 is BindingBase)
		{
			BindingBase binding = (BindingBase)P_2;
			P_0.Bind(Border.BorderThicknessProperty, binding);
			return;
		}
		if (P_2 is Thickness value)
		{
			int priority = (int)P_1;
			P_0.SetValue(Border.BorderThicknessProperty, value, (BindingPriority)priority);
			return;
		}
		if (P_2 == null)
		{
			throw new NullReferenceException();
		}
		throw new InvalidCastException();
	}

	public static void _003C_003EXamlDynamicSetter_4(Layoutable P_0, BindingPriority P_1, object P_2)
	{
		if (P_2 is BindingBase)
		{
			BindingBase binding = (BindingBase)P_2;
			P_0.Bind(Layoutable.MarginProperty, binding);
			return;
		}
		if (P_2 is Thickness value)
		{
			int priority = (int)P_1;
			P_0.SetValue(Layoutable.MarginProperty, value, (BindingPriority)priority);
			return;
		}
		if (P_2 == null)
		{
			throw new NullReferenceException();
		}
		throw new InvalidCastException();
	}

	public static void _003C_003EXamlDynamicSetter_5(ContentPresenter P_0, BindingPriority P_1, object P_2)
	{
		if (P_2 is BindingBase)
		{
			BindingBase binding = (BindingBase)P_2;
			P_0.Bind(ContentPresenter.FontSizeProperty, binding);
			return;
		}
		if (P_2 is double value)
		{
			int priority = (int)P_1;
			P_0.SetValue(ContentPresenter.FontSizeProperty, value, (BindingPriority)priority);
			return;
		}
		if (P_2 == null)
		{
			throw new NullReferenceException();
		}
		throw new InvalidCastException();
	}

	public static void _003C_003EXamlDynamicSetter_6(ContentControl P_0, BindingPriority P_1, BindingBase P_2)
	{
		if (P_2 != null)
		{
			BindingBase binding = P_2;
			P_0.Bind(ContentControl.ContentProperty, binding);
		}
		else
		{
			object value = P_2;
			int priority = (int)P_1;
			P_0.SetValue(ContentControl.ContentProperty, value, (BindingPriority)priority);
		}
	}

	public static void _003C_003EXamlDynamicSetter_7(Button P_0, BindingPriority P_1, BindingBase P_2)
	{
		if (P_2 != null)
		{
			BindingBase binding = P_2;
			P_0.Bind(Button.CommandParameterProperty, binding);
		}
		else
		{
			object value = P_2;
			int priority = (int)P_1;
			P_0.SetValue(Button.CommandParameterProperty, value, (BindingPriority)priority);
		}
	}

	public static void _003C_003EXamlDynamicSetter_8(Image P_0, BindingPriority P_1, object P_2)
	{
		if (P_2 is BindingBase)
		{
			BindingBase binding = (BindingBase)P_2;
			P_0.Bind(Image.SourceProperty, binding);
			return;
		}
		if (P_2 is IImage)
		{
			IImage value = (IImage)P_2;
			int priority = (int)P_1;
			P_0.SetValue(Image.SourceProperty, value, (BindingPriority)priority);
			return;
		}
		if (P_2 == null)
		{
			IImage value = (IImage)P_2;
			int priority = (int)P_1;
			P_0.SetValue(Image.SourceProperty, value, (BindingPriority)priority);
			return;
		}
		throw new InvalidCastException();
	}

	public static void _003C_003EXamlDynamicSetter_9(StyledElement P_0, BindingPriority P_1, object P_2)
	{
		if (P_2 is BindingBase)
		{
			BindingBase binding = (BindingBase)P_2;
			P_0.Bind(StyledElement.ThemeProperty, binding);
			return;
		}
		if (P_2 is ControlTheme)
		{
			ControlTheme value = (ControlTheme)P_2;
			int priority = (int)P_1;
			P_0.SetValue(StyledElement.ThemeProperty, value, (BindingPriority)priority);
			return;
		}
		if (P_2 == null)
		{
			ControlTheme value = (ControlTheme)P_2;
			int priority = (int)P_1;
			P_0.SetValue(StyledElement.ThemeProperty, value, (BindingPriority)priority);
			return;
		}
		throw new InvalidCastException();
	}

	public static void _003C_003EXamlDynamicSetter_10(Button P_0, ReflectionBinding P_1)
	{
		if (P_1 != null)
		{
			BindingBase binding = P_1;
			P_0.Bind(Button.CommandParameterProperty, binding);
		}
		else
		{
			P_0.CommandParameter = P_1;
		}
	}

	public static void _003C_003EXamlDynamicSetter_11(Image P_0, object P_1)
	{
		if (P_1 is UnsetValueType)
		{
			P_0.SetValue(Image.SourceProperty, AvaloniaProperty.UnsetValue);
			return;
		}
		if (P_1 is BindingBase)
		{
			BindingBase binding = (BindingBase)P_1;
			P_0.Bind(Image.SourceProperty, binding);
			return;
		}
		if (P_1 is IImage)
		{
			P_0.Source = (IImage?)P_1;
			return;
		}
		if (P_1 == null)
		{
			P_0.Source = (IImage?)P_1;
			return;
		}
		throw new InvalidCastException();
	}

	public static void _003C_003EXamlDynamicSetter_12(SelectingItemsControl P_0, ReflectionBinding P_1)
	{
		if (P_1 != null)
		{
			BindingBase binding = P_1;
			P_0.Bind(SelectingItemsControl.SelectedItemProperty, binding);
		}
		else
		{
			P_0.SelectedItem = P_1;
		}
	}

	public static void _003C_003EXamlDynamicSetter_13(ToolTip P_0, ReflectionBinding P_1)
	{
		if (P_1 != null)
		{
			BindingBase binding = P_1;
			P_0.Bind(ToolTip.TipProperty, binding);
		}
		else
		{
			ToolTip.SetTip(P_0, P_1);
		}
	}

	public static void _003C_003EXamlDynamicSetter_14(ContentControl P_0, ReflectionBinding P_1)
	{
		if (P_1 != null)
		{
			BindingBase binding = P_1;
			P_0.Bind(ContentControl.ContentProperty, binding);
		}
		else
		{
			P_0.Content = P_1;
		}
	}

	public static void _003C_003EXamlDynamicSetter_15(StyledElement P_0, object P_1)
	{
		if (P_1 is UnsetValueType)
		{
			P_0.SetValue(StyledElement.ThemeProperty, AvaloniaProperty.UnsetValue);
			return;
		}
		if (P_1 is BindingBase)
		{
			BindingBase binding = (BindingBase)P_1;
			P_0.Bind(StyledElement.ThemeProperty, binding);
			return;
		}
		if (P_1 is ControlTheme)
		{
			P_0.Theme = (ControlTheme?)P_1;
			return;
		}
		if (P_1 == null)
		{
			P_0.Theme = (ControlTheme?)P_1;
			return;
		}
		throw new InvalidCastException();
	}
}
