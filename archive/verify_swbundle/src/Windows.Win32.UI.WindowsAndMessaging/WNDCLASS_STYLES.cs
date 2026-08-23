using System;
using System.CodeDom.Compiler;

namespace Windows.Win32.UI.WindowsAndMessaging;

[Flags]
[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.275+02bc0c298e.RR")]
public enum WNDCLASS_STYLES : uint
{
	CS_VREDRAW = 1u,
	CS_HREDRAW = 2u,
	CS_DBLCLKS = 8u,
	CS_OWNDC = 0x20u,
	CS_CLASSDC = 0x40u,
	CS_PARENTDC = 0x80u,
	CS_NOCLOSE = 0x200u,
	CS_SAVEBITS = 0x800u,
	CS_BYTEALIGNCLIENT = 0x1000u,
	CS_BYTEALIGNWINDOW = 0x2000u,
	CS_GLOBALCLASS = 0x4000u,
	CS_IME = 0x10000u,
	CS_DROPSHADOW = 0x20000u
}
