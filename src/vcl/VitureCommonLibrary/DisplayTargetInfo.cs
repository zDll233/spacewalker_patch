using Windows.Win32.Devices.Display;

namespace VitureCommonLibrary;

public class DisplayTargetInfo
{
	private readonly DisplayConfig _path;

	public ulong AdapterId
	{
		get
		{
			return _path.PathRef.targetInfo.adapterId.ToUInt64();
		}
		set
		{
			_path.PathRef.targetInfo.adapterId = value.ToLuid();
		}
	}

	public uint Id
	{
		get
		{
			return _path.PathRef.targetInfo.id;
		}
		set
		{
			_path.PathRef.targetInfo.id = value;
		}
	}

	public uint ModeInfoIdx
	{
		get
		{
			if ((_path.Flags & 8) == 0)
			{
				return _path.PathRef.targetInfo.Anonymous.modeInfoIdx;
			}
			return _path.PathRef.targetInfo.Anonymous.Anonymous.targetModeInfoIdx;
		}
		set
		{
			if ((_path.Flags & 8u) != 0)
			{
				_path.PathRef.targetInfo.Anonymous.Anonymous.targetModeInfoIdx = (ushort)value;
			}
			else
			{
				_path.PathRef.targetInfo.Anonymous.modeInfoIdx = value;
			}
		}
	}

	public ushort? DesktopModeInfoIdx
	{
		get
		{
			if ((_path.Flags & 8) == 0)
			{
				return null;
			}
			return _path.PathRef.targetInfo.Anonymous.Anonymous.desktopModeInfoIdx;
		}
		set
		{
			if ((_path.Flags & 8u) != 0)
			{
				_path.PathRef.targetInfo.Anonymous.Anonymous.desktopModeInfoIdx = value.GetValueOrDefault(ushort.MaxValue);
			}
		}
	}

	public DISPLAYCONFIG_VIDEO_OUTPUT_TECHNOLOGY OutputTechnology
	{
		get
		{
			return _path.PathRef.targetInfo.outputTechnology;
		}
		set
		{
			_path.PathRef.targetInfo.outputTechnology = value;
		}
	}

	public DISPLAYCONFIG_ROTATION Rotation
	{
		get
		{
			return _path.PathRef.targetInfo.rotation;
		}
		set
		{
			_path.PathRef.targetInfo.rotation = value;
		}
	}

	public DISPLAYCONFIG_SCALING Scaling
	{
		get
		{
			return _path.PathRef.targetInfo.scaling;
		}
		set
		{
			_path.PathRef.targetInfo.scaling = value;
		}
	}

	public DISPLAYCONFIG_RATIONAL RefreshRate
	{
		get
		{
			return _path.PathRef.targetInfo.refreshRate;
		}
		set
		{
			_path.PathRef.targetInfo.refreshRate = value;
		}
	}

	public DISPLAYCONFIG_SCANLINE_ORDERING ScanLineOrdering
	{
		get
		{
			return _path.PathRef.targetInfo.scanLineOrdering;
		}
		set
		{
			_path.PathRef.targetInfo.scanLineOrdering = value;
		}
	}

	public bool Available
	{
		get
		{
			return _path.PathRef.targetInfo.targetAvailable;
		}
		set
		{
			_path.PathRef.targetInfo.targetAvailable = value;
		}
	}

	public uint StatusFlags
	{
		get
		{
			return _path.PathRef.targetInfo.statusFlags;
		}
		set
		{
			_path.PathRef.targetInfo.statusFlags = value;
		}
	}

	public DisplayTargetInfo(DisplayConfig path)
	{
		_path = path;
	}
}
