using System.CodeDom.Compiler;

namespace Windows.Win32.Devices.Display;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.275+02bc0c298e.RR")]
public struct DISPLAYCONFIG_TARGET_DEVICE_NAME
{
	public DISPLAYCONFIG_DEVICE_INFO_HEADER header;

	public DISPLAYCONFIG_TARGET_DEVICE_NAME_FLAGS flags;

	public DISPLAYCONFIG_VIDEO_OUTPUT_TECHNOLOGY outputTechnology;

	public ushort edidManufactureId;

	public ushort edidProductCodeId;

	public uint connectorInstance;

	public __char_64 monitorFriendlyDeviceName;

	public __char_128 monitorDevicePath;
}
