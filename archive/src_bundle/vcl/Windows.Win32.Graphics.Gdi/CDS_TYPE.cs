using System;
using System.CodeDom.Compiler;

namespace Windows.Win32.Graphics.Gdi;

[Flags]
[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.275+02bc0c298e.RR")]
public enum CDS_TYPE : uint
{
	CDS_FULLSCREEN = 4u,
	CDS_GLOBAL = 8u,
	CDS_NORESET = 0x10000000u,
	CDS_RESET = 0x40000000u,
	CDS_SET_PRIMARY = 0x10u,
	CDS_TEST = 2u,
	CDS_UPDATEREGISTRY = 1u,
	CDS_VIDEOPARAMETERS = 0x20u,
	CDS_ENABLE_UNSAFE_MODES = 0x100u,
	CDS_DISABLE_UNSAFE_MODES = 0x200u,
	CDS_RESET_EX = 0x20000000u
}
