using System.CodeDom.Compiler;

namespace Windows.Win32.Storage.FileSystem;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.275+02bc0c298e.RR")]
public enum FILE_CREATION_DISPOSITION : uint
{
	CREATE_NEW = 1u,
	CREATE_ALWAYS,
	OPEN_EXISTING,
	OPEN_ALWAYS,
	TRUNCATE_EXISTING
}
