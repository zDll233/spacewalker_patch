using System.Diagnostics;
using Windows.Win32.Devices.Display;

namespace VitureCommonLibrary;

[DebuggerDisplay("{ToString()}")]
public class DisplaySourceMode
{
	private readonly DisplayConfigs _configs;

	private ref DISPLAYCONFIG_SOURCE_MODE Mode => ref _configs.Modes[ModeIndex].Anonymous.sourceMode;

	public int ModeIndex { get; }

	public uint Width
	{
		get
		{
			return Mode.width;
		}
		set
		{
			Mode.width = value;
		}
	}

	public uint Height
	{
		get
		{
			return Mode.height;
		}
		set
		{
			Mode.height = value;
		}
	}

	public int Left
	{
		get
		{
			return Mode.position.x;
		}
		set
		{
			Mode.position.x = value;
		}
	}

	public int Top
	{
		get
		{
			return Mode.position.y;
		}
		set
		{
			Mode.position.y = value;
		}
	}

	public int Right => Mode.position.x + (int)Mode.width;

	public int Bottom => Mode.position.y + (int)Mode.height;

	public DISPLAYCONFIG_PIXELFORMAT PixelFormat
	{
		get
		{
			return Mode.pixelFormat;
		}
		set
		{
			Mode.pixelFormat = value;
		}
	}

	public DisplaySourceMode(DisplayConfigs configs, int modeIndex)
	{
		_configs = configs;
		ModeIndex = modeIndex;
	}

	public override string ToString()
	{
		return $"[{ModeIndex}] SIZE:{Width}x{Height} FMT:{PixelFormat} RECT:{Left},{Top},{Bottom},{Right}";
	}
}
