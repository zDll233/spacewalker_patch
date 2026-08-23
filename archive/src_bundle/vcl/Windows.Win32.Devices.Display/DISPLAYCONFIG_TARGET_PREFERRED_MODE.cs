using System.CodeDom.Compiler;

namespace Windows.Win32.Devices.Display;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.275+02bc0c298e.RR")]
public struct DISPLAYCONFIG_TARGET_PREFERRED_MODE
{
	public DISPLAYCONFIG_DEVICE_INFO_HEADER header;

	public uint width;

	public uint height;

	public DISPLAYCONFIG_TARGET_MODE targetMode;
}
