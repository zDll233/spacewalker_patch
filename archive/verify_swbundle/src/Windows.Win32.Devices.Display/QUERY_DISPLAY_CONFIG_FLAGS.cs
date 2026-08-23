using System;
using System.CodeDom.Compiler;

namespace Windows.Win32.Devices.Display;

[Flags]
[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.275+02bc0c298e.RR")]
public enum QUERY_DISPLAY_CONFIG_FLAGS : uint
{
	QDC_ALL_PATHS = 1u,
	QDC_ONLY_ACTIVE_PATHS = 2u,
	QDC_DATABASE_CURRENT = 4u,
	QDC_VIRTUAL_MODE_AWARE = 0x10u,
	QDC_INCLUDE_HMD = 0x20u,
	QDC_VIRTUAL_REFRESH_RATE_AWARE = 0x40u
}
