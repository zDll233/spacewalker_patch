using Windows.Win32.Devices.Display;

namespace VitureCommonLibrary;

public struct DISPLAYCONFIG_SOURCE_DPI_SCALE_GET
{
	public DISPLAYCONFIG_DEVICE_INFO_HEADER header;

	public int minScaleRel;

	public int curScaleRel;

	public int maxScaleRel;
}
