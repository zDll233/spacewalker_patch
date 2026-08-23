using System.Diagnostics;
using Windows.Win32.Devices.Display;

namespace VitureCommonLibrary;

[DebuggerDisplay("{ToString()}")]
public class DisplayTargetMode
{
	private readonly DisplayConfigs _configs;

	private ref DISPLAYCONFIG_VIDEO_SIGNAL_INFO VideoSignalInfo => ref _configs.Modes[ModeIndex].Anonymous.targetMode.targetVideoSignalInfo;

	public int ModeIndex { get; }

	public ulong PixelRate
	{
		get
		{
			return VideoSignalInfo.pixelRate;
		}
		set
		{
			VideoSignalInfo.pixelRate = value;
		}
	}

	public DISPLAYCONFIG_RATIONAL HSyncFreq
	{
		get
		{
			return VideoSignalInfo.hSyncFreq;
		}
		set
		{
			VideoSignalInfo.hSyncFreq = value;
		}
	}

	public DISPLAYCONFIG_RATIONAL VSyncFreq
	{
		get
		{
			return VideoSignalInfo.vSyncFreq;
		}
		set
		{
			VideoSignalInfo.vSyncFreq = value;
		}
	}

	public uint ActiveWidth
	{
		get
		{
			return VideoSignalInfo.activeSize.cx;
		}
		set
		{
			VideoSignalInfo.activeSize.cx = value;
		}
	}

	public uint ActiveHeight
	{
		get
		{
			return VideoSignalInfo.activeSize.cy;
		}
		set
		{
			VideoSignalInfo.activeSize.cy = value;
		}
	}

	public uint TotalWidth
	{
		get
		{
			return VideoSignalInfo.totalSize.cx;
		}
		set
		{
			VideoSignalInfo.totalSize.cx = value;
		}
	}

	public uint TotalHeight
	{
		get
		{
			return VideoSignalInfo.totalSize.cy;
		}
		set
		{
			VideoSignalInfo.totalSize.cy = value;
		}
	}

	public DISPLAYCONFIG_SCANLINE_ORDERING ScanLineOrdering
	{
		get
		{
			return VideoSignalInfo.scanLineOrdering;
		}
		set
		{
			VideoSignalInfo.scanLineOrdering = value;
		}
	}

	public ushort VideoStandard
	{
		get
		{
			return VideoSignalInfo.Anonymous.AdditionalSignalInfo.videoStandard;
		}
		set
		{
			VideoSignalInfo.Anonymous.AdditionalSignalInfo.videoStandard = value;
		}
	}

	public DisplayTargetMode(DisplayConfigs configs, int modeIndex)
	{
		_configs = configs;
		ModeIndex = modeIndex;
	}

	public override string ToString()
	{
		return $"[{ModeIndex}] ACT:{ActiveWidth}x{ActiveHeight} HZ:{PixelRate} TOL:{TotalWidth}x{TotalHeight} HSF:{HSyncFreq.ToDouble()} VSF:{VSyncFreq.ToDouble()} SLO:{ScanLineOrdering} VS:{VideoStandard}";
	}
}
