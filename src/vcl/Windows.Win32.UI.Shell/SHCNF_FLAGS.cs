using System;
using System.CodeDom.Compiler;

namespace Windows.Win32.UI.Shell;

[Flags]
[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.275+02bc0c298e.RR")]
public enum SHCNF_FLAGS : uint
{
	SHCNF_IDLIST = 0u,
	SHCNF_PATHA = 1u,
	SHCNF_PRINTERA = 2u,
	SHCNF_DWORD = 3u,
	SHCNF_PATHW = 5u,
	SHCNF_PRINTERW = 6u,
	SHCNF_TYPE = 0xFFu,
	SHCNF_FLUSH = 0x1000u,
	SHCNF_FLUSHNOWAIT = 0x3000u,
	SHCNF_NOTIFYRECURSIVE = 0x10000u,
	SHCNF_PATH = 5u,
	SHCNF_PRINTER = 6u
}
