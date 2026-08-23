using System;
using System.CodeDom.Compiler;

namespace Windows.Win32.Storage.FileSystem;

[Flags]
[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.275+02bc0c298e.RR")]
public enum FILE_SHARE_MODE : uint
{
	FILE_SHARE_NONE = 0u,
	FILE_SHARE_DELETE = 4u,
	FILE_SHARE_READ = 1u,
	FILE_SHARE_WRITE = 2u
}
