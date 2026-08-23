using System;
using System.Runtime.InteropServices;

namespace VitureCommonLibrary;

public class DisplaySwitcher
{
	[Flags]
	public enum SetDisplayConfigFlags : uint
	{
		SDC_TOPOLOGY_INTERNAL = 1u,
		SDC_TOPOLOGY_CLONE = 2u,
		SDC_TOPOLOGY_EXTEND = 4u,
		SDC_TOPOLOGY_EXTERNAL = 8u,
		SDC_APPLY = 0x80u
	}

	[DllImport("user32.dll", CharSet = CharSet.Unicode)]
	private static extern long SetDisplayConfig(uint numPathArrayElements, IntPtr pathArray, uint numModeArrayElements, IntPtr modeArray, SetDisplayConfigFlags flags);

	public static void CloneDisplays()
	{
		SetDisplayConfig(0u, IntPtr.Zero, 0u, IntPtr.Zero, SetDisplayConfigFlags.SDC_TOPOLOGY_CLONE | SetDisplayConfigFlags.SDC_APPLY);
	}

	internal static void ExtendDisplays()
	{
		SetDisplayConfig(0u, IntPtr.Zero, 0u, IntPtr.Zero, SetDisplayConfigFlags.SDC_TOPOLOGY_EXTEND | SetDisplayConfigFlags.SDC_APPLY);
	}

	public static void InternalDisplay()
	{
		SetDisplayConfig(0u, IntPtr.Zero, 0u, IntPtr.Zero, SetDisplayConfigFlags.SDC_TOPOLOGY_INTERNAL | SetDisplayConfigFlags.SDC_APPLY);
	}

	public static void ExternalDisplay()
	{
		SetDisplayConfig(0u, IntPtr.Zero, 0u, IntPtr.Zero, SetDisplayConfigFlags.SDC_TOPOLOGY_EXTERNAL | SetDisplayConfigFlags.SDC_APPLY);
	}
}
