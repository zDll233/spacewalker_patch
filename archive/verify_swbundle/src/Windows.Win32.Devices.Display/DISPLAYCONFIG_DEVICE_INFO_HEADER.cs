using System.CodeDom.Compiler;
using Windows.Win32.Foundation;

namespace Windows.Win32.Devices.Display;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.275+02bc0c298e.RR")]
public struct DISPLAYCONFIG_DEVICE_INFO_HEADER
{
	public DISPLAYCONFIG_DEVICE_INFO_TYPE type;

	public uint size;

	public LUID adapterId;

	public uint id;
}
