using System.CodeDom.Compiler;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;

namespace Windows.Win32.Devices.Display;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.275+02bc0c298e.RR")]
public struct DISPLAYCONFIG_MODE_INFO
{
	[StructLayout(LayoutKind.Explicit)]
	[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.275+02bc0c298e.RR")]
	public struct _Anonymous_e__Union
	{
		[FieldOffset(0)]
		public DISPLAYCONFIG_TARGET_MODE targetMode;

		[FieldOffset(0)]
		public DISPLAYCONFIG_SOURCE_MODE sourceMode;

		[FieldOffset(0)]
		public DISPLAYCONFIG_DESKTOP_IMAGE_INFO desktopImageInfo;
	}

	public DISPLAYCONFIG_MODE_INFO_TYPE infoType;

	public uint id;

	public LUID adapterId;

	public _Anonymous_e__Union Anonymous;
}
