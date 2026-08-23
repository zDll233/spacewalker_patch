using System.CodeDom.Compiler;
using Windows.Win32.Foundation;

namespace Windows.Win32.Devices.Display;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.275+02bc0c298e.RR")]
public struct DISPLAYCONFIG_SOURCE_MODE
{
	public uint width;

	public uint height;

	public DISPLAYCONFIG_PIXELFORMAT pixelFormat;

	public POINTL position;
}
