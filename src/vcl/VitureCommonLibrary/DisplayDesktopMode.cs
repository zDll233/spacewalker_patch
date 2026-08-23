using System.Diagnostics;
using Windows.Win32.Devices.Display;

namespace VitureCommonLibrary;

[DebuggerDisplay("{ToString()}")]
public class DisplayDesktopMode
{
	private readonly DisplayConfigs _configs;

	private ref DISPLAYCONFIG_DESKTOP_IMAGE_INFO Mode => ref _configs.Modes[ModeIndex].Anonymous.desktopImageInfo;

	public int ModeIndex { get; }

	public uint SourceWidth
	{
		get
		{
			return (uint)Mode.PathSourceSize.x;
		}
		set
		{
			Mode.PathSourceSize.x = (int)value;
		}
	}

	public uint SourceHeight
	{
		get
		{
			return (uint)Mode.PathSourceSize.y;
		}
		set
		{
			Mode.PathSourceSize.y = (int)value;
		}
	}

	public int RegionLeft
	{
		get
		{
			return Mode.DesktopImageRegion.left;
		}
		set
		{
			Mode.DesktopImageRegion.left = value;
		}
	}

	public int RegionTop
	{
		get
		{
			return Mode.DesktopImageRegion.top;
		}
		set
		{
			Mode.DesktopImageRegion.top = value;
		}
	}

	public int RegionRight
	{
		get
		{
			return Mode.DesktopImageRegion.right;
		}
		set
		{
			Mode.DesktopImageRegion.right = value;
		}
	}

	public int RegionBottom
	{
		get
		{
			return Mode.DesktopImageRegion.bottom;
		}
		set
		{
			Mode.DesktopImageRegion.bottom = value;
		}
	}

	public uint RegionWidth => (uint)(Mode.DesktopImageRegion.right - Mode.DesktopImageRegion.left);

	public uint RegionHeight => (uint)(Mode.DesktopImageRegion.bottom - Mode.DesktopImageRegion.top);

	public int ClipLeft
	{
		get
		{
			return Mode.DesktopImageClip.left;
		}
		set
		{
			Mode.DesktopImageClip.left = value;
		}
	}

	public int ClipTop
	{
		get
		{
			return Mode.DesktopImageClip.top;
		}
		set
		{
			Mode.DesktopImageClip.top = value;
		}
	}

	public int ClipRight
	{
		get
		{
			return Mode.DesktopImageClip.right;
		}
		set
		{
			Mode.DesktopImageClip.right = value;
		}
	}

	public int ClipBottom
	{
		get
		{
			return Mode.DesktopImageClip.bottom;
		}
		set
		{
			Mode.DesktopImageClip.bottom = value;
		}
	}

	public uint ClipWidth => (uint)(Mode.DesktopImageClip.right - Mode.DesktopImageClip.left);

	public uint ClipHeight => (uint)(Mode.DesktopImageClip.bottom - Mode.DesktopImageClip.top);

	public DisplayDesktopMode(DisplayConfigs configs, int modeIndex)
	{
		_configs = configs;
		ModeIndex = modeIndex;
	}

	public override string ToString()
	{
		return $"[{ModeIndex}] SRC:{SourceWidth}x{SourceHeight} " + $"REGION:{RegionLeft},{RegionTop},{RegionRight},{RegionBottom} " + $"CLIP:{ClipLeft},{ClipTop},{ClipRight},{ClipBottom}";
	}
}
