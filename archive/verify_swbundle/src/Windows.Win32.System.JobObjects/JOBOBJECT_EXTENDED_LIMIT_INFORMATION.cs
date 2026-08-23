using System.CodeDom.Compiler;
using Windows.Win32.System.Threading;

namespace Windows.Win32.System.JobObjects;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.275+02bc0c298e.RR")]
public struct JOBOBJECT_EXTENDED_LIMIT_INFORMATION
{
	public JOBOBJECT_BASIC_LIMIT_INFORMATION BasicLimitInformation;

	public IO_COUNTERS IoInfo;

	public nuint ProcessMemoryLimit;

	public nuint JobMemoryLimit;

	public nuint PeakProcessMemoryUsed;

	public nuint PeakJobMemoryUsed;
}
