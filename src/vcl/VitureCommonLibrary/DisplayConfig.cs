using System.Diagnostics;
using Windows.Win32.Devices.Display;

namespace VitureCommonLibrary;

[DebuggerDisplay("{ToString()}")]
public class DisplayConfig
{
	public const uint DISPLAYCONFIG_PATH_ACTIVE = 1u;

	public const uint DISPLAYCONFIG_PATH_SUPPORT_VIRTUAL_MODE = 8u;

	public const uint DISPLAYCONFIG_PATH_BOOST_REFRESH_RATE = 16u;

	public const uint DISPLAYCONFIG_SOURCE_IN_USE = 1u;

	public const uint DISPLAYCONFIG_MODE_IDX_INVALID = uint.MaxValue;

	public const ushort DISPLAYCONFIG_GROUP_ID_INVALID = ushort.MaxValue;

	public ref DISPLAYCONFIG_PATH_INFO PathRef => ref Owner.Paths[PathIndex];

	public DisplayConfigs Owner { get; }

	public int PathIndex { get; }

	public bool IsPrimary
	{
		get
		{
			if (IsActive)
			{
				DisplaySourceMode sourceMode = GetSourceMode();
				if (sourceMode != null && sourceMode.Left == 0)
				{
					return sourceMode.Top == 0;
				}
				return false;
			}
			return false;
		}
	}

	public bool IsActive
	{
		get
		{
			return (PathRef.flags & 1) != 0;
		}
		set
		{
			PathRef.flags = (value ? (PathRef.flags | 1u) : (PathRef.flags & 0xFFFFFFFEu));
		}
	}

	public uint Flags
	{
		get
		{
			return PathRef.flags;
		}
		set
		{
			PathRef.flags = value;
		}
	}

	public DisplayDeviceInfo DeviceInfo { get; }

	public DisplaySourceInfo SourceInfo { get; }

	public DisplayTargetInfo TargetInfo { get; }

	public override string ToString()
	{
		DISPLAYCONFIG_TARGET_DEVICE_NAME? targetDeviceName = DeviceInfo.GetTargetDeviceName();
		return $"({TargetInfo.AdapterId},{TargetInfo.Id},{SourceInfo.AdapterId},{SourceInfo.Id})" + $"{targetDeviceName?.monitorFriendlyDeviceName}:{targetDeviceName?.monitorDevicePath}";
	}

	public DisplayConfig(DisplayConfigs configs, int index)
	{
		Owner = configs;
		PathIndex = index;
		DeviceInfo = new DisplayDeviceInfo(this);
		SourceInfo = new DisplaySourceInfo(this);
		TargetInfo = new DisplayTargetInfo(this);
	}

	public DisplaySourceMode? GetSourceMode()
	{
		uint modeInfoIdx = SourceInfo.ModeInfoIdx;
		if (modeInfoIdx >= (uint)Owner.Modes.Length || Owner.Modes[modeInfoIdx].infoType != DISPLAYCONFIG_MODE_INFO_TYPE.DISPLAYCONFIG_MODE_INFO_TYPE_SOURCE)
		{
			return null;
		}
		return new DisplaySourceMode(Owner, (int)modeInfoIdx);
	}

	public DisplayTargetMode? GetTargetMode()
	{
		uint modeInfoIdx = TargetInfo.ModeInfoIdx;
		if (modeInfoIdx >= (uint)Owner.Modes.Length || Owner.Modes[modeInfoIdx].infoType != DISPLAYCONFIG_MODE_INFO_TYPE.DISPLAYCONFIG_MODE_INFO_TYPE_TARGET)
		{
			return null;
		}
		return new DisplayTargetMode(Owner, (int)modeInfoIdx);
	}

	public DisplayDesktopMode? GetDesktopMode()
	{
		ushort? desktopModeInfoIdx = TargetInfo.DesktopModeInfoIdx;
		if (!((uint?)desktopModeInfoIdx < (uint?)Owner.Modes.Length) || Owner.Modes[desktopModeInfoIdx.Value].infoType != DISPLAYCONFIG_MODE_INFO_TYPE.DISPLAYCONFIG_MODE_INFO_TYPE_DESKTOP_IMAGE)
		{
			return null;
		}
		return new DisplayDesktopMode(Owner, desktopModeInfoIdx.Value);
	}
}
