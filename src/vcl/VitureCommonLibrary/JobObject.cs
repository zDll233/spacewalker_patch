using System;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;
using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.Security;
using Windows.Win32.System.JobObjects;

namespace VitureCommonLibrary;

public static class JobObject
{
	private static readonly SafeFileHandle? _jobHandle;

	static JobObject()
	{
		SafeFileHandle safeFileHandle = PInvoke.CreateJobObject((SECURITY_ATTRIBUTES?)null, (string)null);
		if (safeFileHandle.IsInvalid)
		{
			Logger.Warning("JobObject: CreateJobObject failed");
		}
		else if (!ApplyKillOnClose(safeFileHandle))
		{
			safeFileHandle.Dispose();
		}
		else
		{
			_jobHandle = safeFileHandle;
		}
	}

	private unsafe static bool ApplyKillOnClose(SafeFileHandle handle)
	{
		JOBOBJECT_EXTENDED_LIMIT_INFORMATION jOBOBJECT_EXTENDED_LIMIT_INFORMATION = default(JOBOBJECT_EXTENDED_LIMIT_INFORMATION);
		jOBOBJECT_EXTENDED_LIMIT_INFORMATION.BasicLimitInformation.LimitFlags = JOB_OBJECT_LIMIT.JOB_OBJECT_LIMIT_KILL_ON_JOB_CLOSE;
		bool num = PInvoke.SetInformationJobObject((HANDLE)handle.DangerousGetHandle(), JOBOBJECTINFOCLASS.JobObjectExtendedLimitInformation, &jOBOBJECT_EXTENDED_LIMIT_INFORMATION, (uint)sizeof(JOBOBJECT_EXTENDED_LIMIT_INFORMATION));
		if (!num)
		{
			Logger.Warning($"JobObject: SetInformationJobObject failed (error {Marshal.GetLastWin32Error()})");
		}
		return num;
	}

	public static void AddProcess(IntPtr processHandle)
	{
		if (_jobHandle == null || _jobHandle.IsInvalid)
		{
			return;
		}
		try
		{
			if (!PInvoke.AssignProcessToJobObject((HANDLE)_jobHandle.DangerousGetHandle(), (HANDLE)processHandle))
			{
				Logger.Warning($"JobObject: AssignProcessToJobObject failed (error {Marshal.GetLastWin32Error()})");
			}
		}
		catch (Exception ex)
		{
			Logger.Warning("JobObject: AddProcess failed — " + ex.Message);
		}
	}
}
