using System.CodeDom.Compiler;

namespace Windows.Win32.System.JobObjects;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.275+02bc0c298e.RR")]
public struct JOBOBJECT_BASIC_LIMIT_INFORMATION
{
	public long PerProcessUserTimeLimit;

	public long PerJobUserTimeLimit;

	public JOB_OBJECT_LIMIT LimitFlags;

	public nuint MinimumWorkingSetSize;

	public nuint MaximumWorkingSetSize;

	public uint ActiveProcessLimit;

	public nuint Affinity;

	public uint PriorityClass;

	public uint SchedulingClass;
}
