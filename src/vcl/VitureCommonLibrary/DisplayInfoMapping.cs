using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Windows.Win32.Devices.Display;

namespace VitureCommonLibrary;

internal static class DisplayInfoMapping
{
	public static DisplayInfo ToDisplayInfo(this DisplayConfig dc)
	{
		DisplaySourceMode sourceMode = dc.GetSourceMode();
		string displayName = dc.DeviceInfo.GetSourceDeviceName()?.viewGdiDeviceName.ToString() ?? string.Empty;
		DISPLAYCONFIG_ROTATION rotation = dc.TargetInfo.Rotation;
		bool flag = rotation == DISPLAYCONFIG_ROTATION.DISPLAYCONFIG_ROTATION_ROTATE90 || rotation == DISPLAYCONFIG_ROTATION.DISPLAYCONFIG_ROTATION_ROTATE270;
		return new DisplayInfo
		{
			DisplayName = displayName,
			IsConnected = dc.DeviceInfo.IsConnected,
			IsActive = dc.IsActive,
			IsGDIPrimary = dc.IsPrimary,
			CurrentSetting = new DisplaySettingInfo
			{
				Position = ((sourceMode != null) ? new Point(sourceMode.Left, sourceMode.Top) : Point.Empty),
				Resolution = ((sourceMode == null) ? Size.Empty : (flag ? new Size((int)sourceMode.Height, (int)sourceMode.Width) : new Size((int)sourceMode.Width, (int)sourceMode.Height)))
			}
		};
	}

	public static DisplayInfo[] ToDisplayInfos(this IEnumerable<DisplayConfig> dcs)
	{
		return dcs.Select(ToDisplayInfo).ToArray();
	}
}
