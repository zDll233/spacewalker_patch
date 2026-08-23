namespace VitureCommonLibrary;

public class DisplaySourceInfo
{
	private readonly DisplayConfig _path;

	public ulong AdapterId
	{
		get
		{
			return _path.PathRef.sourceInfo.adapterId.ToUInt64();
		}
		set
		{
			_path.PathRef.sourceInfo.adapterId = value.ToLuid();
		}
	}

	public uint Id
	{
		get
		{
			return _path.PathRef.sourceInfo.id;
		}
		set
		{
			_path.PathRef.sourceInfo.id = value;
		}
	}

	public uint ModeInfoIdx
	{
		get
		{
			if ((_path.Flags & 8) == 0)
			{
				return _path.PathRef.sourceInfo.Anonymous.modeInfoIdx;
			}
			return _path.PathRef.sourceInfo.Anonymous.Anonymous.sourceModeInfoIdx;
		}
		set
		{
			if ((_path.Flags & 8u) != 0)
			{
				_path.PathRef.sourceInfo.Anonymous.Anonymous.sourceModeInfoIdx = (ushort)value;
			}
			else
			{
				_path.PathRef.sourceInfo.Anonymous.modeInfoIdx = value;
			}
		}
	}

	public ushort? CloneGroupId
	{
		get
		{
			if ((_path.Flags & 8) == 0)
			{
				return null;
			}
			return _path.PathRef.sourceInfo.Anonymous.Anonymous.cloneGroupId;
		}
		set
		{
			if ((_path.Flags & 8u) != 0)
			{
				_path.PathRef.sourceInfo.Anonymous.Anonymous.cloneGroupId = value.GetValueOrDefault(ushort.MaxValue);
			}
		}
	}

	public uint StatusFlags
	{
		get
		{
			return _path.PathRef.sourceInfo.statusFlags;
		}
		set
		{
			_path.PathRef.sourceInfo.statusFlags = value;
		}
	}

	public DisplaySourceInfo(DisplayConfig path)
	{
		_path = path;
	}
}
