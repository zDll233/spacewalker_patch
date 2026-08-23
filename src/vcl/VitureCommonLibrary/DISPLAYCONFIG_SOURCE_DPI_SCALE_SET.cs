using Windows.Win32.Devices.Display;

namespace VitureCommonLibrary;

public struct DISPLAYCONFIG_SOURCE_DPI_SCALE_SET
{
	public DISPLAYCONFIG_DEVICE_INFO_HEADER header;

	public int scaleRel;
}
