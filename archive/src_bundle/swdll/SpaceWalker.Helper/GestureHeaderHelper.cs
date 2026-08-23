using SpaceWalker.Assets.Languages;
using SpaceWalker.ViewModels;
using VitureCommonLibrary;

namespace SpaceWalker.Helper;

public static class GestureHeaderHelper
{
	public static bool Equal(string gestureHeader, string resourceStr)
	{
		return resourceStr == Resources.ResourceManager.GetString(gestureHeader, Resources.Culture);
	}

	public static LayoutMode GestureHeaderToLayoutMode(string gestureHeader)
	{
		LayoutMode result = LayoutMode.UltraWide;
		if (Equal(gestureHeader, Resources.UltraWideHeader))
		{
			result = LayoutMode.UltraWide;
		}
		if (Equal(gestureHeader, Resources.SingleMirroredHeader))
		{
			result = LayoutMode.HorizonMirror1;
		}
		if (Equal(gestureHeader, Resources.TwoMirroredHeader))
		{
			result = LayoutMode.HorizonMirror2;
		}
		if (Equal(gestureHeader, Resources.ThreeMirroredHeader))
		{
			result = LayoutMode.HorizonMirror3;
		}
		if (Equal(gestureHeader, Resources.SingleExtentedHeader))
		{
			result = LayoutMode.HorizonExtend1;
		}
		if (Equal(gestureHeader, Resources.TwoExtendedHeader))
		{
			result = LayoutMode.HorizonExtend2;
		}
		if (Equal(gestureHeader, Resources.ThreeExtendedHeader))
		{
			result = LayoutMode.HorizonExtend3;
		}
		if (Equal(gestureHeader, Resources.ThreeStackedHeader))
		{
			result = LayoutMode.VerticalMirror3;
		}
		if (Equal(gestureHeader, Resources.PLPExtentedHeader))
		{
			result = LayoutMode.HorizonPortraitExtend;
		}
		if (Equal(gestureHeader, Resources.PLPMirroredHeader))
		{
			result = LayoutMode.HorizonPortraitMirror;
		}
		return result;
	}

	public static (VitureLayoutMode, LayoutType)? ToLayout(bool isNative3DofMode, string gestureHeader)
	{
		if (Equal(gestureHeader, Resources.UltraWideHeader))
		{
			return (isNative3DofMode ? VitureLayoutMode.UltraWideA : VitureLayoutMode.UltraWide, LayoutType.Extend);
		}
		if (Equal(gestureHeader, Resources.SingleMirroredHeader))
		{
			return (isNative3DofMode ? VitureLayoutMode.Horizontal1A : VitureLayoutMode.Horizontal1, LayoutType.Mirror);
		}
		if (Equal(gestureHeader, Resources.TwoMirroredHeader))
		{
			return ((!isNative3DofMode) ? VitureLayoutMode.Horizontal2 : VitureLayoutMode.Horizontal2A, LayoutType.Mirror);
		}
		if (Equal(gestureHeader, Resources.ThreeMirroredHeader))
		{
			return (isNative3DofMode ? VitureLayoutMode.Horizontal3A : VitureLayoutMode.Horizontal3, LayoutType.Mirror);
		}
		if (Equal(gestureHeader, Resources.SingleExtentedHeader))
		{
			return (isNative3DofMode ? VitureLayoutMode.Horizontal1A : VitureLayoutMode.Horizontal1, LayoutType.Extend);
		}
		if (Equal(gestureHeader, Resources.TwoExtendedHeader))
		{
			return ((!isNative3DofMode) ? VitureLayoutMode.Horizontal2 : VitureLayoutMode.Horizontal2A, LayoutType.Extend);
		}
		if (Equal(gestureHeader, Resources.ThreeExtendedHeader))
		{
			return (isNative3DofMode ? VitureLayoutMode.Horizontal3A : VitureLayoutMode.Horizontal3, LayoutType.Extend);
		}
		if (isNative3DofMode)
		{
			return null;
		}
		if (Equal(gestureHeader, Resources.ThreeStackedHeader))
		{
			return (VitureLayoutMode.Vertical3, LayoutType.Mirror);
		}
		if (Equal(gestureHeader, Resources.PLPExtentedHeader))
		{
			return (VitureLayoutMode.HorizontalPortrait, LayoutType.Extend);
		}
		if (Equal(gestureHeader, Resources.PLPMirroredHeader))
		{
			return (VitureLayoutMode.HorizontalPortrait, LayoutType.Mirror);
		}
		return null;
	}
}
