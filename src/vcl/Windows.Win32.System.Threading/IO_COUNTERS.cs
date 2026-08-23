using System.CodeDom.Compiler;

namespace Windows.Win32.System.Threading;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.275+02bc0c298e.RR")]
public struct IO_COUNTERS
{
	public ulong ReadOperationCount;

	public ulong WriteOperationCount;

	public ulong OtherOperationCount;

	public ulong ReadTransferCount;

	public ulong WriteTransferCount;

	public ulong OtherTransferCount;
}
