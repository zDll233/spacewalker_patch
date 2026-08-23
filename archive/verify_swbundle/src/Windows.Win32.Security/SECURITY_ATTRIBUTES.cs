using System.CodeDom.Compiler;
using Windows.Win32.Foundation;

namespace Windows.Win32.Security;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.275+02bc0c298e.RR")]
public struct SECURITY_ATTRIBUTES
{
	public uint nLength;

	public unsafe void* lpSecurityDescriptor;

	public BOOL bInheritHandle;
}
