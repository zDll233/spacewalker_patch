using System.CodeDom.Compiler;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;

namespace Windows.Win32.Graphics.Gdi;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.275+02bc0c298e.RR")]
public struct DEVMODEW
{
	[StructLayout(LayoutKind.Explicit)]
	[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.275+02bc0c298e.RR")]
	public struct _Anonymous1_e__Union
	{
		[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.275+02bc0c298e.RR")]
		public struct _Anonymous1_e__Struct
		{
			public short dmOrientation;

			public short dmPaperSize;

			public short dmPaperLength;

			public short dmPaperWidth;

			public short dmScale;

			public short dmCopies;

			public short dmDefaultSource;

			public short dmPrintQuality;
		}

		[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.275+02bc0c298e.RR")]
		public struct _Anonymous2_e__Struct
		{
			public POINTL dmPosition;

			public DEVMODE_DISPLAY_ORIENTATION dmDisplayOrientation;

			public DEVMODE_DISPLAY_FIXED_OUTPUT dmDisplayFixedOutput;
		}

		[FieldOffset(0)]
		public _Anonymous1_e__Struct Anonymous1;

		[FieldOffset(0)]
		public _Anonymous2_e__Struct Anonymous2;
	}

	[StructLayout(LayoutKind.Explicit)]
	[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.275+02bc0c298e.RR")]
	public struct _Anonymous2_e__Union
	{
		[FieldOffset(0)]
		public uint dmDisplayFlags;

		[FieldOffset(0)]
		public uint dmNup;
	}

	public __char_32 dmDeviceName;

	public ushort dmSpecVersion;

	public ushort dmDriverVersion;

	public ushort dmSize;

	public ushort dmDriverExtra;

	public DEVMODE_FIELD_FLAGS dmFields;

	public _Anonymous1_e__Union Anonymous1;

	public DEVMODE_COLOR dmColor;

	public DEVMODE_DUPLEX dmDuplex;

	public short dmYResolution;

	public DEVMODE_TRUETYPE_OPTION dmTTOption;

	public DEVMODE_COLLATE dmCollate;

	public __char_32 dmFormName;

	public ushort dmLogPixels;

	public uint dmBitsPerPel;

	public uint dmPelsWidth;

	public uint dmPelsHeight;

	public _Anonymous2_e__Union Anonymous2;

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
