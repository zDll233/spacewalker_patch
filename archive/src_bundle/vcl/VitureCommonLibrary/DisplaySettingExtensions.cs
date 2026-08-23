using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace VitureCommonLibrary;

public static class DisplaySettingExtensions
{
	public enum DisplayCategory
	{
		Physical,
		Viture,
		Vdd
	}

	private struct DEVMODE_LITE
	{
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
		public string dmDeviceName;

		public ushort dmSpecVersion;

		public ushort dmDriverVersion;

		public ushort dmSize;

		public ushort dmDriverExtra;

		public uint dmFields;

		public int dmPositionX;

		public int dmPositionY;

		public uint dmDisplayOrientation;

		public uint dmDisplayFixedOutput;

		public short dmColor;

		public short dmDuplex;

		public short dmYResolution;

		public short dmTTOption;

		public short dmCollate;

		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
		public string dmFormName;

		public ushort dmLogPixels;

		public uint dmBitsPerPel;

		public int dmPelsWidth;

		public int dmPelsHeight;

		public uint dmDisplayFlags;

		public uint dmDisplayFrequency;

		public uint dmICMMethod;

		public uint dmICMIntent;

		public uint dmMediaType;

		public uint dmDitherType;

		public uint dmReserved1;

		public uint dmReserved2;

		public uint dmPanningWidth;

		public uint dmPanningHeight;
	}

	private class TopologyEntry
	{
		public string? GdiSourceName;

		public string? FriendlyName;

		public string? MonitorDevicePath;

		public DISPLAYCONFIG_VIDEO_OUTPUT_TECHNOLOGY OutputTechnology;

		public ushort EdidManufactureId;
	}

	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	private struct LUID
	{
		public uint LowPart;

		public int HighPart;
	}

	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	private struct DISPLAYCONFIG_PATH_SOURCE_INFO
	{
		public LUID adapterId;

		public uint id;

		public uint modeInfoIdx;

		public uint statusFlags;
	}

	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	private struct DISPLAYCONFIG_PATH_TARGET_INFO
	{
		public LUID adapterId;

		public uint id;

		public uint modeInfoIdx;

		public DISPLAYCONFIG_VIDEO_OUTPUT_TECHNOLOGY outputTechnology;

		public uint rotation;

		public uint scaling;

		public ulong refreshRate;

		public uint scanLineOrdering;

		[MarshalAs(UnmanagedType.Bool)]
		public bool targetAvailable;

		public uint statusFlags;
	}

	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	private struct DISPLAYCONFIG_PATH_INFO
	{
		public DISPLAYCONFIG_PATH_SOURCE_INFO sourceInfo;

		public DISPLAYCONFIG_PATH_TARGET_INFO targetInfo;

		public uint flags;
	}

	[StructLayout(LayoutKind.Sequential, Pack = 4, Size = 64)]
	private struct DISPLAYCONFIG_MODE_INFO
	{
	}

	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	private struct DISPLAYCONFIG_DEVICE_INFO_HEADER
	{
		public DISPLAYCONFIG_DEVICE_INFO_TYPE type;

		public uint size;

		public LUID adapterId;

		public uint id;
	}

	private enum DISPLAYCONFIG_DEVICE_INFO_TYPE : uint
	{
		GET_SOURCE_NAME = 1u,
		GET_TARGET_NAME
	}

	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 4)]
	private struct DISPLAYCONFIG_TARGET_DEVICE_NAME
	{
		public DISPLAYCONFIG_DEVICE_INFO_HEADER header;

		public uint flags;

		public DISPLAYCONFIG_VIDEO_OUTPUT_TECHNOLOGY outputTechnology;

		public ushort edidManufactureId;

		public ushort edidProductCodeId;

		public uint connectorInstance;

		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
		public string monitorFriendlyDeviceName;

		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		public string monitorDevicePath;
	}

	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 4)]
	private struct DISPLAYCONFIG_SOURCE_DEVICE_NAME
	{
		public DISPLAYCONFIG_DEVICE_INFO_HEADER header;

		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
		public string viewGdiDeviceName;
	}

	internal enum DISPLAYCONFIG_VIDEO_OUTPUT_TECHNOLOGY : uint
	{
		OTHER = uint.MaxValue,
		HD15 = 0u,
		SVIDEO = 1u,
		COMPOSITE_VIDEO = 2u,
		COMPONENT_VIDEO = 3u,
		DVI = 4u,
		HDMI = 5u,
		LVDS = 6u,
		D_JPN = 8u,
		SDI = 9u,
		DISPLAYPORT_EXTERNAL = 10u,
		DISPLAYPORT_EMBEDDED = 11u,
		UDI_EXTERNAL = 12u,
		UDI_EMBEDDED = 13u,
		SDTVDONGLE = 14u,
		MIRACAST = 15u,
		INDIRECT_WIRED = 16u,
		INDIRECT_VIRTUAL = 17u,
		INTERNAL = 2147483648u
	}

	[ThreadStatic]
	private static bool? _viture1200Cache;

	[ThreadStatic]
	private static int _viture1200TickCount;

	private const int ENUM_CURRENT_SETTINGS = -1;

	private const uint DM_POSITION = 32u;

	private const uint DM_PELSWIDTH = 524288u;

	private const uint DM_PELSHEIGHT = 1048576u;

	private const uint CDS_UPDATEREGISTRY = 1u;

	private const uint CDS_NORESET = 268435456u;

	[ThreadStatic]
	private static Dictionary<string, TopologyEntry>? _snapshotCache;

	[ThreadStatic]
	private static int _snapshotTickCount;

	public static bool IsInternalGdiName(string? gdiName)
	{
		if (string.IsNullOrEmpty(gdiName))
		{
			return false;
		}
		DISPLAYCONFIG_VIDEO_OUTPUT_TECHNOLOGY dISPLAYCONFIG_VIDEO_OUTPUT_TECHNOLOGY = LookupOutputTechnologyByGdi(gdiName);
		if (dISPLAYCONFIG_VIDEO_OUTPUT_TECHNOLOGY != DISPLAYCONFIG_VIDEO_OUTPUT_TECHNOLOGY.INTERNAL && dISPLAYCONFIG_VIDEO_OUTPUT_TECHNOLOGY != DISPLAYCONFIG_VIDEO_OUTPUT_TECHNOLOGY.DISPLAYPORT_EMBEDDED)
		{
			return dISPLAYCONFIG_VIDEO_OUTPUT_TECHNOLOGY == DISPLAYCONFIG_VIDEO_OUTPUT_TECHNOLOGY.UDI_EMBEDDED;
		}
		return true;
	}

	public static List<string> GetActiveDisplayNames(DisplayCategory category)
	{
		Dictionary<string, TopologyEntry> activeTopologySnapshot = GetActiveTopologySnapshot();
		if (activeTopologySnapshot == null)
		{
			return new List<string>();
		}
		IEnumerable<TopologyEntry> source = activeTopologySnapshot.Values.Where((TopologyEntry e) => !string.IsNullOrEmpty(e.GdiSourceName) && CategorizeEntry(e) == category);
		if (category == DisplayCategory.Physical)
		{
			source = source.OrderByDescending((TopologyEntry e) => IsInternalTech(e.OutputTechnology));
		}
		return source.Select((TopologyEntry e) => e.GdiSourceName).ToList();
	}

	private static DisplayCategory CategorizeEntry(TopologyEntry e)
	{
		string text = e.FriendlyName ?? string.Empty;
		if (text.Contains("VITURE"))
		{
			return DisplayCategory.Viture;
		}
		if (text.Contains("VIT-VDD"))
		{
			return DisplayCategory.Vdd;
		}
		if (IsVitureEdid(e.EdidManufactureId))
		{
			return DisplayCategory.Viture;
		}
		if (e.OutputTechnology == DISPLAYCONFIG_VIDEO_OUTPUT_TECHNOLOGY.INDIRECT_VIRTUAL || e.OutputTechnology == DISPLAYCONFIG_VIDEO_OUTPUT_TECHNOLOGY.INDIRECT_WIRED || e.OutputTechnology == DISPLAYCONFIG_VIDEO_OUTPUT_TECHNOLOGY.MIRACAST)
		{
			return DisplayCategory.Vdd;
		}
		return DisplayCategory.Physical;
	}

	private static bool IsVitureEdid(ushort packed)
	{
		int num = (packed >> 10) & 0x1F;
		int num2 = (packed >> 5) & 0x1F;
		int num3 = packed & 0x1F;
		if (num == 22 && num2 == 9)
		{
			return num3 == 20;
		}
		return false;
	}

	public static List<string> GetActivePhysicalDisplayNames()
	{
		return GetActiveDisplayNames(DisplayCategory.Physical);
	}

	private static bool IsInternalTech(DISPLAYCONFIG_VIDEO_OUTPUT_TECHNOLOGY tech)
	{
		if (tech != DISPLAYCONFIG_VIDEO_OUTPUT_TECHNOLOGY.INTERNAL && tech != DISPLAYCONFIG_VIDEO_OUTPUT_TECHNOLOGY.DISPLAYPORT_EMBEDDED)
		{
			return tech == DISPLAYCONFIG_VIDEO_OUTPUT_TECHNOLOGY.UDI_EMBEDDED;
		}
		return true;
	}

	public static (int Width, int Height) GetCurrentResolutionByGdi(string gdiName)
	{
		DEVMODE_LITE lpDevMode = MakeDevMode();
		if (EnumDisplaySettings(gdiName, -1, ref lpDevMode))
		{
			return (Width: lpDevMode.dmPelsWidth, Height: lpDevMode.dmPelsHeight);
		}
		return (Width: 0, Height: 0);
	}

	public static bool? VitureDisplaySupports1200P()
	{
		int tickCount = Environment.TickCount;
		if (_viture1200Cache.HasValue && (uint)(tickCount - _viture1200TickCount) < 1500u)
		{
			return _viture1200Cache;
		}
		bool? flag = null;
		try
		{
			foreach (string activeDisplayName in GetActiveDisplayNames(DisplayCategory.Viture))
			{
				flag = false;
				if (GdiSupportsHeight(activeDisplayName, 1200))
				{
					flag = true;
					break;
				}
			}
		}
		catch
		{
			flag = null;
		}
		_viture1200Cache = flag;
		_viture1200TickCount = tickCount;
		return flag;
	}

	private static bool GdiSupportsHeight(string gdiName, int targetHeight)
	{
		int num = 0;
		while (true)
		{
			DEVMODE_LITE lpDevMode = MakeDevMode();
			if (!EnumDisplaySettings(gdiName, num, ref lpDevMode))
			{
				break;
			}
			if (lpDevMode.dmPelsHeight == targetHeight)
			{
				return true;
			}
			num++;
		}
		return false;
	}

	public static void DisableByGdiName(string gdiName)
	{
		DEVMODE_LITE lpDevMode = MakeDevMode();
		lpDevMode.dmFields = 1572896u;
		lpDevMode.dmPelsWidth = 0;
		lpDevMode.dmPelsHeight = 0;
		lpDevMode.dmPositionX = 0;
		lpDevMode.dmPositionY = 0;
		ChangeDisplaySettingsEx(gdiName, ref lpDevMode, IntPtr.Zero, 268435457u, IntPtr.Zero);
	}

	public static void CommitDisplayChanges()
	{
		ChangeDisplaySettingsEx(null, IntPtr.Zero, IntPtr.Zero, 0u, IntPtr.Zero);
	}

	public static void MoveByGdiName(string gdiName, int x, int y)
	{
		DEVMODE_LITE lpDevMode = MakeDevMode();
		if (EnumDisplaySettings(gdiName, -1, ref lpDevMode))
		{
			lpDevMode.dmFields |= 32u;
			lpDevMode.dmPositionX = x;
			lpDevMode.dmPositionY = y;
			ChangeDisplaySettingsEx(gdiName, ref lpDevMode, IntPtr.Zero, 268435457u, IntPtr.Zero);
		}
	}

	private static DEVMODE_LITE MakeDevMode()
	{
		DEVMODE_LITE result = default(DEVMODE_LITE);
		result.dmDeviceName = string.Empty;
		result.dmFormName = string.Empty;
		result.dmSize = (ushort)Marshal.SizeOf<DEVMODE_LITE>();
		return result;
	}

	[DllImport("user32.dll", CharSet = CharSet.Ansi)]
	private static extern bool EnumDisplaySettings(string lpszDeviceName, int iModeNum, ref DEVMODE_LITE lpDevMode);

	[DllImport("user32.dll", CharSet = CharSet.Ansi)]
	private static extern int ChangeDisplaySettingsEx(string? lpszDeviceName, ref DEVMODE_LITE lpDevMode, IntPtr hwnd, uint dwflags, IntPtr lParam);

	[DllImport("user32.dll", CharSet = CharSet.Ansi, EntryPoint = "ChangeDisplaySettingsExA")]
	private static extern int ChangeDisplaySettingsEx(string? lpszDeviceName, IntPtr lpDevMode, IntPtr hwnd, uint dwflags, IntPtr lParam);

	private static DISPLAYCONFIG_VIDEO_OUTPUT_TECHNOLOGY LookupOutputTechnologyByGdi(string gdiName)
	{
		Dictionary<string, TopologyEntry> activeTopologySnapshot = GetActiveTopologySnapshot();
		if (activeTopologySnapshot != null && activeTopologySnapshot.TryGetValue(gdiName, out var value))
		{
			return value.OutputTechnology;
		}
		return DISPLAYCONFIG_VIDEO_OUTPUT_TECHNOLOGY.OTHER;
	}

	private static Dictionary<string, TopologyEntry>? GetActiveTopologySnapshot()
	{
		int tickCount = Environment.TickCount;
		if (_snapshotCache != null && (uint)(tickCount - _snapshotTickCount) < 1500u)
		{
			return _snapshotCache;
		}
		Dictionary<string, TopologyEntry>? result = (_snapshotCache = BuildSnapshot());
		_snapshotTickCount = tickCount;
		return result;
	}

	private static Dictionary<string, TopologyEntry>? BuildSnapshot()
	{
		if (GetDisplayConfigBufferSizes(2u, out var numPathArrayElements, out var numModeInfoArrayElements) != 0 || numPathArrayElements == 0)
		{
			return null;
		}
		DISPLAYCONFIG_PATH_INFO[] array = new DISPLAYCONFIG_PATH_INFO[numPathArrayElements];
		DISPLAYCONFIG_MODE_INFO[] modeInfoArray = new DISPLAYCONFIG_MODE_INFO[numModeInfoArrayElements];
		if (QueryDisplayConfig(2u, ref numPathArrayElements, array, ref numModeInfoArrayElements, modeInfoArray, IntPtr.Zero) != 0)
		{
			return null;
		}
		Dictionary<string, TopologyEntry> dictionary = new Dictionary<string, TopologyEntry>(StringComparer.OrdinalIgnoreCase);
		for (int i = 0; i < numPathArrayElements; i++)
		{
			DISPLAYCONFIG_SOURCE_DEVICE_NAME dISPLAYCONFIG_SOURCE_DEVICE_NAME = default(DISPLAYCONFIG_SOURCE_DEVICE_NAME);
			dISPLAYCONFIG_SOURCE_DEVICE_NAME.header = new DISPLAYCONFIG_DEVICE_INFO_HEADER
			{
				type = DISPLAYCONFIG_DEVICE_INFO_TYPE.GET_SOURCE_NAME,
				size = (uint)Marshal.SizeOf<DISPLAYCONFIG_SOURCE_DEVICE_NAME>(),
				adapterId = array[i].sourceInfo.adapterId,
				id = array[i].sourceInfo.id
			};
			DISPLAYCONFIG_SOURCE_DEVICE_NAME requestPacket = dISPLAYCONFIG_SOURCE_DEVICE_NAME;
			string text = null;
			if (DisplayConfigGetDeviceInfo(ref requestPacket) == 0)
			{
				text = requestPacket.viewGdiDeviceName?.TrimEnd(new char[1]);
			}
			DISPLAYCONFIG_TARGET_DEVICE_NAME dISPLAYCONFIG_TARGET_DEVICE_NAME = default(DISPLAYCONFIG_TARGET_DEVICE_NAME);
			dISPLAYCONFIG_TARGET_DEVICE_NAME.header = new DISPLAYCONFIG_DEVICE_INFO_HEADER
			{
				type = DISPLAYCONFIG_DEVICE_INFO_TYPE.GET_TARGET_NAME,
				size = (uint)Marshal.SizeOf<DISPLAYCONFIG_TARGET_DEVICE_NAME>(),
				adapterId = array[i].targetInfo.adapterId,
				id = array[i].targetInfo.id
			};
			DISPLAYCONFIG_TARGET_DEVICE_NAME requestPacket2 = dISPLAYCONFIG_TARGET_DEVICE_NAME;
			if (DisplayConfigGetDeviceInfo(ref requestPacket2) == 0 && !string.IsNullOrEmpty(text))
			{
				TopologyEntry topologyEntry = new TopologyEntry();
				topologyEntry.GdiSourceName = text;
				topologyEntry.FriendlyName = requestPacket2.monitorFriendlyDeviceName?.TrimEnd(new char[1]);
				topologyEntry.MonitorDevicePath = requestPacket2.monitorDevicePath?.TrimEnd(new char[1]);
				topologyEntry.OutputTechnology = requestPacket2.outputTechnology;
				topologyEntry.EdidManufactureId = requestPacket2.edidManufactureId;
				TopologyEntry value = topologyEntry;
				dictionary[text] = value;
			}
		}
		return dictionary;
	}

	[DllImport("user32.dll")]
	private static extern int GetDisplayConfigBufferSizes(uint flags, out uint numPathArrayElements, out uint numModeInfoArrayElements);

	[DllImport("user32.dll")]
	private static extern int QueryDisplayConfig(uint flags, ref uint numPathArrayElements, [Out] DISPLAYCONFIG_PATH_INFO[] pathArray, ref uint numModeInfoArrayElements, [Out] DISPLAYCONFIG_MODE_INFO[] modeInfoArray, IntPtr currentTopologyId);

	[DllImport("user32.dll")]
	private static extern int DisplayConfigGetDeviceInfo(ref DISPLAYCONFIG_TARGET_DEVICE_NAME requestPacket);

	[DllImport("user32.dll")]
	private static extern int DisplayConfigGetDeviceInfo(ref DISPLAYCONFIG_SOURCE_DEVICE_NAME requestPacket);
}
