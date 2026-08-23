using System;
using System.CodeDom.Compiler;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.Win32.SafeHandles;
using Windows.Win32.Devices.DeviceAndDriverInstallation;
using Windows.Win32.Devices.Display;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Gdi;
using Windows.Win32.Security;
using Windows.Win32.Storage.FileSystem;
using Windows.Win32.System.JobObjects;
using Windows.Win32.System.Threading;
using Windows.Win32.UI.Shell;
using Windows.Win32.UI.WindowsAndMessaging;

namespace Windows.Win32;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.275+02bc0c298e.RR")]
public static class PInvoke
{
	public const uint THREAD_POWER_THROTTLING_CURRENT_VERSION = 1u;

	public const uint THREAD_POWER_THROTTLING_EXECUTION_SPEED = 1u;

	public unsafe static CONFIGRET CM_Get_Device_Interface_List_Size(out uint pulLen, in Guid InterfaceClassGuid, [Optional] PWSTR pDeviceID, CM_GET_DEVICE_INTERFACE_LIST_FLAGS ulFlags)
	{
		fixed (Guid* interfaceClassGuid = &InterfaceClassGuid)
		{
			fixed (uint* pulLen2 = &pulLen)
			{
				return CM_Get_Device_Interface_List_Size(pulLen2, interfaceClassGuid, pDeviceID, ulFlags);
			}
		}
	}

	[DllImport("CFGMGR32.dll", EntryPoint = "CM_Get_Device_Interface_List_SizeW", ExactSpelling = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	public unsafe static extern CONFIGRET CM_Get_Device_Interface_List_Size(uint* pulLen, Guid* InterfaceClassGuid, [Optional] PWSTR pDeviceID, CM_GET_DEVICE_INTERFACE_LIST_FLAGS ulFlags);

	public unsafe static CONFIGRET CM_Get_Device_Interface_List(in Guid InterfaceClassGuid, [Optional] PWSTR pDeviceID, PZZWSTR Buffer, uint BufferLen, CM_GET_DEVICE_INTERFACE_LIST_FLAGS ulFlags)
	{
		fixed (Guid* interfaceClassGuid = &InterfaceClassGuid)
		{
			return CM_Get_Device_Interface_List(interfaceClassGuid, pDeviceID, Buffer, BufferLen, ulFlags);
		}
	}

	[DllImport("CFGMGR32.dll", EntryPoint = "CM_Get_Device_Interface_ListW", ExactSpelling = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	public unsafe static extern CONFIGRET CM_Get_Device_Interface_List(Guid* InterfaceClassGuid, [Optional] PWSTR pDeviceID, PZZWSTR Buffer, uint BufferLen, CM_GET_DEVICE_INTERFACE_LIST_FLAGS ulFlags);

	[DllImport("GDI32.dll", ExactSpelling = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	public static extern BOOL DeleteObject(HGDIOBJ ho);

	[DllImport("KERNEL32.dll", ExactSpelling = true, SetLastError = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	public static extern BOOL CloseHandle(HANDLE hObject);

	public unsafe static SafeFileHandle CreateFile(string lpFileName, uint dwDesiredAccess, FILE_SHARE_MODE dwShareMode, [Optional] SECURITY_ATTRIBUTES? lpSecurityAttributes, FILE_CREATION_DISPOSITION dwCreationDisposition, FILE_FLAGS_AND_ATTRIBUTES dwFlagsAndAttributes, [Optional] SafeHandle hTemplateFile)
	{
		bool success = false;
		try
		{
			fixed (char* ptr = lpFileName)
			{
				SECURITY_ATTRIBUTES valueOrDefault = lpSecurityAttributes.GetValueOrDefault();
				HANDLE hTemplateFile2;
				if (hTemplateFile != null)
				{
					hTemplateFile.DangerousAddRef(ref success);
					hTemplateFile2 = (HANDLE)hTemplateFile.DangerousGetHandle();
				}
				else
				{
					hTemplateFile2 = (HANDLE)new IntPtr(0L);
				}
				return new SafeFileHandle(CreateFile(ptr, dwDesiredAccess, dwShareMode, lpSecurityAttributes.HasValue ? (&valueOrDefault) : null, dwCreationDisposition, dwFlagsAndAttributes, hTemplateFile2), ownsHandle: true);
			}
		}
		finally
		{
			if (success)
			{
				hTemplateFile.DangerousRelease();
			}
		}
	}

	[DllImport("KERNEL32.dll", EntryPoint = "CreateFileW", ExactSpelling = true, SetLastError = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	public unsafe static extern HANDLE CreateFile(PCWSTR lpFileName, uint dwDesiredAccess, FILE_SHARE_MODE dwShareMode, [Optional] SECURITY_ATTRIBUTES* lpSecurityAttributes, FILE_CREATION_DISPOSITION dwCreationDisposition, FILE_FLAGS_AND_ATTRIBUTES dwFlagsAndAttributes, [Optional] HANDLE hTemplateFile);

	public unsafe static BOOL DeviceIoControl(SafeHandle hDevice, uint dwIoControlCode, [Optional] ReadOnlySpan<byte> lpInBuffer, [Optional] Span<byte> lpOutBuffer, out uint lpBytesReturned, [Optional] NativeOverlapped* lpOverlapped)
	{
		bool success = false;
		try
		{
			fixed (uint* lpBytesReturned2 = &lpBytesReturned)
			{
				fixed (byte* lpOutBuffer2 = lpOutBuffer)
				{
					fixed (byte* lpInBuffer2 = lpInBuffer)
					{
						if (hDevice != null)
						{
							hDevice.DangerousAddRef(ref success);
							HANDLE hDevice2 = (HANDLE)hDevice.DangerousGetHandle();
							return DeviceIoControl(hDevice2, dwIoControlCode, lpInBuffer2, (uint)lpInBuffer.Length, lpOutBuffer2, (uint)lpOutBuffer.Length, lpBytesReturned2, lpOverlapped);
						}
						throw new ArgumentNullException("hDevice");
					}
				}
			}
		}
		finally
		{
			if (success)
			{
				hDevice.DangerousRelease();
			}
		}
	}

	public unsafe static BOOL DeviceIoControl(SafeHandle hDevice, uint dwIoControlCode, [Optional] ReadOnlySpan<byte> lpInBuffer, [Optional] Span<byte> lpOutBuffer, [Optional] NativeOverlapped* lpOverlapped)
	{
		bool success = false;
		try
		{
			fixed (byte* lpOutBuffer2 = lpOutBuffer)
			{
				fixed (byte* lpInBuffer2 = lpInBuffer)
				{
					if (hDevice != null)
					{
						hDevice.DangerousAddRef(ref success);
						HANDLE hDevice2 = (HANDLE)hDevice.DangerousGetHandle();
						return DeviceIoControl(hDevice2, dwIoControlCode, lpInBuffer2, (uint)lpInBuffer.Length, lpOutBuffer2, (uint)lpOutBuffer.Length, null, lpOverlapped);
					}
					throw new ArgumentNullException("hDevice");
				}
			}
		}
		finally
		{
			if (success)
			{
				hDevice.DangerousRelease();
			}
		}
	}

	[DllImport("KERNEL32.dll", ExactSpelling = true, SetLastError = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	public unsafe static extern BOOL DeviceIoControl(HANDLE hDevice, uint dwIoControlCode, [Optional] void* lpInBuffer, uint nInBufferSize, [Optional] void* lpOutBuffer, uint nOutBufferSize, [Optional] uint* lpBytesReturned, [Optional] NativeOverlapped* lpOverlapped);

	[DllImport("KERNEL32.dll", ExactSpelling = true, SetLastError = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	public static extern BOOL FreeLibrary(HMODULE hLibModule);

	public unsafe static FreeLibrarySafeHandle GetModuleHandle([Optional] string lpModuleName)
	{
		fixed (char* ptr = lpModuleName)
		{
			return new FreeLibrarySafeHandle(GetModuleHandle(ptr), ownsHandle: false);
		}
	}

	[DllImport("KERNEL32.dll", EntryPoint = "GetModuleHandleW", ExactSpelling = true, SetLastError = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	public static extern HMODULE GetModuleHandle([Optional] PCWSTR lpModuleName);

	public unsafe static SafeFileHandle CreateJobObject([Optional] SECURITY_ATTRIBUTES? lpJobAttributes, [Optional] string lpName)
	{
		fixed (char* ptr = lpName)
		{
			SECURITY_ATTRIBUTES valueOrDefault = lpJobAttributes.GetValueOrDefault();
			return new SafeFileHandle(CreateJobObject(lpJobAttributes.HasValue ? (&valueOrDefault) : null, ptr), ownsHandle: true);
		}
	}

	[DllImport("KERNEL32.dll", EntryPoint = "CreateJobObjectW", ExactSpelling = true, SetLastError = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	public unsafe static extern HANDLE CreateJobObject([Optional] SECURITY_ATTRIBUTES* lpJobAttributes, [Optional] PCWSTR lpName);

	public static BOOL AssignProcessToJobObject(SafeHandle hJob, SafeHandle hProcess)
	{
		bool success = false;
		bool success2 = false;
		try
		{
			if (hJob != null)
			{
				hJob.DangerousAddRef(ref success);
				HANDLE hJob2 = (HANDLE)hJob.DangerousGetHandle();
				if (hProcess != null)
				{
					hProcess.DangerousAddRef(ref success2);
					HANDLE hProcess2 = (HANDLE)hProcess.DangerousGetHandle();
					return AssignProcessToJobObject(hJob2, hProcess2);
				}
				throw new ArgumentNullException("hProcess");
			}
			throw new ArgumentNullException("hJob");
		}
		finally
		{
			if (success)
			{
				hJob.DangerousRelease();
			}
			if (success2)
			{
				hProcess.DangerousRelease();
			}
		}
	}

	[DllImport("KERNEL32.dll", ExactSpelling = true, SetLastError = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	public static extern BOOL AssignProcessToJobObject(HANDLE hJob, HANDLE hProcess);

	public unsafe static BOOL SetInformationJobObject(SafeHandle hJob, JOBOBJECTINFOCLASS JobObjectInformationClass, ReadOnlySpan<byte> lpJobObjectInformation)
	{
		bool success = false;
		try
		{
			fixed (byte* lpJobObjectInformation2 = lpJobObjectInformation)
			{
				if (hJob != null)
				{
					hJob.DangerousAddRef(ref success);
					HANDLE hJob2 = (HANDLE)hJob.DangerousGetHandle();
					return SetInformationJobObject(hJob2, JobObjectInformationClass, lpJobObjectInformation2, (uint)lpJobObjectInformation.Length);
				}
				throw new ArgumentNullException("hJob");
			}
		}
		finally
		{
			if (success)
			{
				hJob.DangerousRelease();
			}
		}
	}

	[DllImport("KERNEL32.dll", ExactSpelling = true, SetLastError = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	public unsafe static extern BOOL SetInformationJobObject(HANDLE hJob, JOBOBJECTINFOCLASS JobObjectInformationClass, void* lpJobObjectInformation, uint cbJobObjectInformationLength);

	public static SafeFileHandle GetCurrentThread_SafeHandle()
	{
		return new SafeFileHandle(GetCurrentThread(), ownsHandle: true);
	}

	[DllImport("KERNEL32.dll", ExactSpelling = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	public static extern HANDLE GetCurrentThread();

	public unsafe static BOOL SetThreadInformation(SafeHandle hThread, THREAD_INFORMATION_CLASS ThreadInformationClass, ReadOnlySpan<byte> ThreadInformation)
	{
		bool success = false;
		try
		{
			fixed (byte* threadInformation = ThreadInformation)
			{
				if (hThread != null)
				{
					hThread.DangerousAddRef(ref success);
					HANDLE hThread2 = (HANDLE)hThread.DangerousGetHandle();
					return SetThreadInformation(hThread2, ThreadInformationClass, threadInformation, (uint)ThreadInformation.Length);
				}
				throw new ArgumentNullException("hThread");
			}
		}
		finally
		{
			if (success)
			{
				hThread.DangerousRelease();
			}
		}
	}

	[DllImport("KERNEL32.dll", ExactSpelling = true, SetLastError = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	public unsafe static extern BOOL SetThreadInformation(HANDLE hThread, THREAD_INFORMATION_CLASS ThreadInformationClass, void* ThreadInformation, uint ThreadInformationSize);

	public unsafe static void SHChangeNotify([MarshalAs(UnmanagedType.I4)] SHCNE_ID wEventId, SHCNF_FLAGS uFlags, [Optional] void* dwItem1, [Optional] void* dwItem2)
	{
		LocalExternFunction(wEventId, uFlags, dwItem1, dwItem2);
		[DllImport("SHELL32.dll", EntryPoint = "SHChangeNotify", ExactSpelling = true)]
		[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
		static extern unsafe void LocalExternFunction([MarshalAs(UnmanagedType.I4)] SHCNE_ID wEventId, SHCNF_FLAGS uFlags, [Optional] void* dwItem1, [Optional] void* dwItem2);
	}

	public unsafe static BOOL EnumDisplayMonitors([Optional] HDC hdc, [Optional] RECT? lprcClip, MONITORENUMPROC lpfnEnum, LPARAM dwData)
	{
		RECT valueOrDefault = lprcClip.GetValueOrDefault();
		return EnumDisplayMonitors(hdc, lprcClip.HasValue ? (&valueOrDefault) : null, lpfnEnum, dwData);
	}

	[DllImport("USER32.dll", ExactSpelling = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	public unsafe static extern BOOL EnumDisplayMonitors([Optional] HDC hdc, [Optional] RECT* lprcClip, [MarshalAs(UnmanagedType.FunctionPtr)] MONITORENUMPROC lpfnEnum, LPARAM dwData);

	public unsafe static BOOL EnumDisplaySettingsEx([Optional] string lpszDeviceName, ENUM_DISPLAY_SETTINGS_MODE iModeNum, ref DEVMODEW lpDevMode, ENUM_DISPLAY_SETTINGS_FLAGS dwFlags)
	{
		fixed (DEVMODEW* lpDevMode2 = &lpDevMode)
		{
			fixed (char* ptr = lpszDeviceName)
			{
				return EnumDisplaySettingsEx(ptr, iModeNum, lpDevMode2, dwFlags);
			}
		}
	}

	[DllImport("USER32.dll", EntryPoint = "EnumDisplaySettingsExW", ExactSpelling = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	public unsafe static extern BOOL EnumDisplaySettingsEx([Optional] PCWSTR lpszDeviceName, ENUM_DISPLAY_SETTINGS_MODE iModeNum, DEVMODEW* lpDevMode, ENUM_DISPLAY_SETTINGS_FLAGS dwFlags);

	public unsafe static DISP_CHANGE ChangeDisplaySettingsEx([Optional] string lpszDeviceName, [Optional] DEVMODEW? lpDevMode, CDS_TYPE dwflags, [Optional] void* lParam)
	{
		fixed (char* ptr = lpszDeviceName)
		{
			DEVMODEW valueOrDefault = lpDevMode.GetValueOrDefault();
			return ChangeDisplaySettingsEx(ptr, lpDevMode.HasValue ? (&valueOrDefault) : null, default(HWND), dwflags, lParam);
		}
	}

	[DllImport("USER32.dll", EntryPoint = "ChangeDisplaySettingsExW", ExactSpelling = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	public unsafe static extern DISP_CHANGE ChangeDisplaySettingsEx([Optional] PCWSTR lpszDeviceName, [Optional] DEVMODEW* lpDevMode, [Optional] HWND hwnd, CDS_TYPE dwflags, [Optional] void* lParam);

	public unsafe static BOOL GetMonitorInfo(HMONITOR hMonitor, ref MONITORINFO lpmi)
	{
		fixed (MONITORINFO* lpmi2 = &lpmi)
		{
			return GetMonitorInfo(hMonitor, lpmi2);
		}
	}

	[DllImport("USER32.dll", EntryPoint = "GetMonitorInfoW", ExactSpelling = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	public unsafe static extern BOOL GetMonitorInfo(HMONITOR hMonitor, MONITORINFO* lpmi);

	public unsafe static WIN32_ERROR GetDisplayConfigBufferSizes(QUERY_DISPLAY_CONFIG_FLAGS flags, out uint numPathArrayElements, out uint numModeInfoArrayElements)
	{
		fixed (uint* numModeInfoArrayElements2 = &numModeInfoArrayElements)
		{
			fixed (uint* numPathArrayElements2 = &numPathArrayElements)
			{
				return GetDisplayConfigBufferSizes(flags, numPathArrayElements2, numModeInfoArrayElements2);
			}
		}
	}

	[DllImport("USER32.dll", ExactSpelling = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	public unsafe static extern WIN32_ERROR GetDisplayConfigBufferSizes(QUERY_DISPLAY_CONFIG_FLAGS flags, uint* numPathArrayElements, uint* numModeInfoArrayElements);

	public unsafe static WIN32_ERROR QueryDisplayConfig(QUERY_DISPLAY_CONFIG_FLAGS flags, ref uint numPathArrayElements, Span<DISPLAYCONFIG_PATH_INFO> pathArray, ref uint numModeInfoArrayElements, Span<DISPLAYCONFIG_MODE_INFO> modeInfoArray, ref DISPLAYCONFIG_TOPOLOGY_ID currentTopologyId)
	{
		fixed (DISPLAYCONFIG_TOPOLOGY_ID* currentTopologyId2 = &currentTopologyId)
		{
			fixed (DISPLAYCONFIG_MODE_INFO* modeInfoArray2 = modeInfoArray)
			{
				fixed (uint* numModeInfoArrayElements2 = &numModeInfoArrayElements)
				{
					fixed (DISPLAYCONFIG_PATH_INFO* pathArray2 = pathArray)
					{
						fixed (uint* numPathArrayElements2 = &numPathArrayElements)
						{
							return QueryDisplayConfig(flags, numPathArrayElements2, pathArray2, numModeInfoArrayElements2, modeInfoArray2, currentTopologyId2);
						}
					}
				}
			}
		}
	}

	public unsafe static WIN32_ERROR QueryDisplayConfig(QUERY_DISPLAY_CONFIG_FLAGS flags, ref uint numPathArrayElements, Span<DISPLAYCONFIG_PATH_INFO> pathArray, ref uint numModeInfoArrayElements, Span<DISPLAYCONFIG_MODE_INFO> modeInfoArray)
	{
		fixed (DISPLAYCONFIG_MODE_INFO* modeInfoArray2 = modeInfoArray)
		{
			fixed (uint* numModeInfoArrayElements2 = &numModeInfoArrayElements)
			{
				fixed (DISPLAYCONFIG_PATH_INFO* pathArray2 = pathArray)
				{
					fixed (uint* numPathArrayElements2 = &numPathArrayElements)
					{
						return QueryDisplayConfig(flags, numPathArrayElements2, pathArray2, numModeInfoArrayElements2, modeInfoArray2, null);
					}
				}
			}
		}
	}

	[DllImport("USER32.dll", ExactSpelling = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	public unsafe static extern WIN32_ERROR QueryDisplayConfig(QUERY_DISPLAY_CONFIG_FLAGS flags, uint* numPathArrayElements, DISPLAYCONFIG_PATH_INFO* pathArray, uint* numModeInfoArrayElements, DISPLAYCONFIG_MODE_INFO* modeInfoArray, [Optional] DISPLAYCONFIG_TOPOLOGY_ID* currentTopologyId);

	public unsafe static int SetDisplayConfig([Optional] ReadOnlySpan<DISPLAYCONFIG_PATH_INFO> pathArray, [Optional] ReadOnlySpan<DISPLAYCONFIG_MODE_INFO> modeInfoArray, SET_DISPLAY_CONFIG_FLAGS flags)
	{
		fixed (DISPLAYCONFIG_MODE_INFO* modeInfoArray2 = modeInfoArray)
		{
			fixed (DISPLAYCONFIG_PATH_INFO* pathArray2 = pathArray)
			{
				return SetDisplayConfig((uint)pathArray.Length, pathArray2, (uint)modeInfoArray.Length, modeInfoArray2, flags);
			}
		}
	}

	[DllImport("USER32.dll", ExactSpelling = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	public unsafe static extern int SetDisplayConfig(uint numPathArrayElements, [Optional] DISPLAYCONFIG_PATH_INFO* pathArray, uint numModeInfoArrayElements, [Optional] DISPLAYCONFIG_MODE_INFO* modeInfoArray, SET_DISPLAY_CONFIG_FLAGS flags);

	public unsafe static int DisplayConfigGetDeviceInfo(ref DISPLAYCONFIG_DEVICE_INFO_HEADER requestPacket)
	{
		fixed (DISPLAYCONFIG_DEVICE_INFO_HEADER* requestPacket2 = &requestPacket)
		{
			return DisplayConfigGetDeviceInfo(requestPacket2);
		}
	}

	[DllImport("USER32.dll", ExactSpelling = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	public unsafe static extern int DisplayConfigGetDeviceInfo(DISPLAYCONFIG_DEVICE_INFO_HEADER* requestPacket);

	public unsafe static int DisplayConfigSetDeviceInfo(in DISPLAYCONFIG_DEVICE_INFO_HEADER setPacket)
	{
		fixed (DISPLAYCONFIG_DEVICE_INFO_HEADER* setPacket2 = &setPacket)
		{
			return DisplayConfigSetDeviceInfo(setPacket2);
		}
	}

	[DllImport("USER32.dll", ExactSpelling = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	public unsafe static extern int DisplayConfigSetDeviceInfo(DISPLAYCONFIG_DEVICE_INFO_HEADER* setPacket);

	[DllImport("USER32.dll", EntryPoint = "PostMessageW", ExactSpelling = true, SetLastError = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	public static extern BOOL PostMessage([Optional] HWND hWnd, uint Msg, WPARAM wParam, LPARAM lParam);

	[DllImport("USER32.dll", ExactSpelling = true, SetLastError = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	public static extern BOOL DestroyIcon(HICON hIcon);

	[DllImport("USER32.dll", ExactSpelling = true, SetLastError = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	public static extern BOOL DestroyCursor(HCURSOR hCursor);

	[DllImport("USER32.dll", EntryPoint = "RegisterClassExW", ExactSpelling = true, SetLastError = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	public static extern ushort RegisterClassEx(in WNDCLASSEXW param0);

	[DllImport("USER32.dll", ExactSpelling = true, SetLastError = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	public static extern BOOL DestroyMenu(HMENU hMenu);

	public unsafe static HWND CreateWindowEx(WINDOW_EX_STYLE dwExStyle, [Optional] string lpClassName, [Optional] string lpWindowName, WINDOW_STYLE dwStyle, int X, int Y, int nWidth, int nHeight, [Optional] HWND hWndParent, [Optional] SafeHandle hMenu, [Optional] SafeHandle hInstance, [Optional] void* lpParam)
	{
		bool success = false;
		bool success2 = false;
		try
		{
			fixed (char* ptr2 = lpWindowName)
			{
				fixed (char* ptr = lpClassName)
				{
					HMENU hMenu2;
					if (hMenu != null)
					{
						hMenu.DangerousAddRef(ref success);
						hMenu2 = (HMENU)hMenu.DangerousGetHandle();
					}
					else
					{
						hMenu2 = (HMENU)new IntPtr(0L);
					}
					HINSTANCE hInstance2;
					if (hInstance != null)
					{
						hInstance.DangerousAddRef(ref success2);
						hInstance2 = (HINSTANCE)hInstance.DangerousGetHandle();
					}
					else
					{
						hInstance2 = (HINSTANCE)new IntPtr(0L);
					}
					return CreateWindowEx(dwExStyle, ptr, ptr2, dwStyle, X, Y, nWidth, nHeight, hWndParent, hMenu2, hInstance2, lpParam);
				}
			}
		}
		finally
		{
			if (success)
			{
				hMenu.DangerousRelease();
			}
			if (success2)
			{
				hInstance.DangerousRelease();
			}
		}
	}

	[DllImport("USER32.dll", EntryPoint = "CreateWindowExW", ExactSpelling = true, SetLastError = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	public unsafe static extern HWND CreateWindowEx(WINDOW_EX_STYLE dwExStyle, [Optional] PCWSTR lpClassName, [Optional] PCWSTR lpWindowName, WINDOW_STYLE dwStyle, int X, int Y, int nWidth, int nHeight, [Optional] HWND hWndParent, [Optional] HMENU hMenu, [Optional] HINSTANCE hInstance, [Optional] void* lpParam);

	[DllImport("USER32.dll", ExactSpelling = true, SetLastError = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	public static extern BOOL DestroyWindow(HWND hWnd);

	public unsafe static BOOL UnregisterClass(string lpClassName, [Optional] SafeHandle hInstance)
	{
		bool success = false;
		try
		{
			fixed (char* ptr = lpClassName)
			{
				HINSTANCE hInstance2;
				if (hInstance != null)
				{
					hInstance.DangerousAddRef(ref success);
					hInstance2 = (HINSTANCE)hInstance.DangerousGetHandle();
				}
				else
				{
					hInstance2 = (HINSTANCE)new IntPtr(0L);
				}
				return UnregisterClass(ptr, hInstance2);
			}
		}
		finally
		{
			if (success)
			{
				hInstance.DangerousRelease();
			}
		}
	}

	[DllImport("USER32.dll", EntryPoint = "UnregisterClassW", ExactSpelling = true, SetLastError = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	public static extern BOOL UnregisterClass(PCWSTR lpClassName, [Optional] HINSTANCE hInstance);

	[DllImport("USER32.dll", EntryPoint = "DefWindowProcW", ExactSpelling = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	public static extern LRESULT DefWindowProc(HWND hWnd, uint Msg, WPARAM wParam, LPARAM lParam);

	public unsafe static BOOL GetMessage(out MSG lpMsg, [Optional] HWND hWnd, uint wMsgFilterMin, uint wMsgFilterMax)
	{
		fixed (MSG* lpMsg2 = &lpMsg)
		{
			return GetMessage(lpMsg2, hWnd, wMsgFilterMin, wMsgFilterMax);
		}
	}

	[DllImport("USER32.dll", EntryPoint = "GetMessageW", ExactSpelling = true, SetLastError = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	public unsafe static extern BOOL GetMessage(MSG* lpMsg, [Optional] HWND hWnd, uint wMsgFilterMin, uint wMsgFilterMax);

	public unsafe static BOOL TranslateMessage(in MSG lpMsg)
	{
		fixed (MSG* lpMsg2 = &lpMsg)
		{
			return TranslateMessage(lpMsg2);
		}
	}

	[DllImport("USER32.dll", ExactSpelling = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	public unsafe static extern BOOL TranslateMessage(MSG* lpMsg);

	public unsafe static LRESULT DispatchMessage(in MSG lpMsg)
	{
		fixed (MSG* lpMsg2 = &lpMsg)
		{
			return DispatchMessage(lpMsg2);
		}
	}

	[DllImport("USER32.dll", EntryPoint = "DispatchMessageW", ExactSpelling = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	public unsafe static extern LRESULT DispatchMessage(MSG* lpMsg);

	public unsafe static BOOL ClipCursor([Optional] RECT? lpRect)
	{
		RECT valueOrDefault = lpRect.GetValueOrDefault();
		return ClipCursor(lpRect.HasValue ? (&valueOrDefault) : null);
	}

	[DllImport("USER32.dll", ExactSpelling = true, SetLastError = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	public unsafe static extern BOOL ClipCursor([Optional] RECT* lpRect);

	public unsafe static BOOL GetClipCursor(out RECT lpRect)
	{
		fixed (RECT* lpRect2 = &lpRect)
		{
			return GetClipCursor(lpRect2);
		}
	}

	[DllImport("USER32.dll", ExactSpelling = true, SetLastError = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	public unsafe static extern BOOL GetClipCursor(RECT* lpRect);

	[DllImport("USER32.dll", ExactSpelling = true, SetLastError = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	public static extern BOOL UnhookWindowsHookEx(HHOOK hhk);

	public static UnhookWindowsHookExSafeHandle SetWindowsHookEx(WINDOWS_HOOK_ID idHook, HOOKPROC lpfn, [Optional] SafeHandle hmod, uint dwThreadId)
	{
		bool success = false;
		try
		{
			HINSTANCE hmod2;
			if (hmod != null)
			{
				hmod.DangerousAddRef(ref success);
				hmod2 = (HINSTANCE)hmod.DangerousGetHandle();
			}
			else
			{
				hmod2 = (HINSTANCE)new IntPtr(0L);
			}
			return new UnhookWindowsHookExSafeHandle(SetWindowsHookEx(idHook, lpfn, hmod2, dwThreadId));
		}
		finally
		{
			if (success)
			{
				hmod.DangerousRelease();
			}
		}
	}

	[DllImport("USER32.dll", EntryPoint = "SetWindowsHookExW", ExactSpelling = true, SetLastError = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	public static extern HHOOK SetWindowsHookEx(WINDOWS_HOOK_ID idHook, [MarshalAs(UnmanagedType.FunctionPtr)] HOOKPROC lpfn, [Optional] HINSTANCE hmod, uint dwThreadId);

	public static LRESULT CallNextHookEx([Optional] SafeHandle hhk, int nCode, WPARAM wParam, LPARAM lParam)
	{
		bool success = false;
		try
		{
			HHOOK hhk2;
			if (hhk != null)
			{
				hhk.DangerousAddRef(ref success);
				hhk2 = (HHOOK)hhk.DangerousGetHandle();
			}
			else
			{
				hhk2 = (HHOOK)new IntPtr(0L);
			}
			return CallNextHookEx(hhk2, nCode, wParam, lParam);
		}
		finally
		{
			if (success)
			{
				hhk.DangerousRelease();
			}
		}
	}

	[DllImport("USER32.dll", ExactSpelling = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	public static extern LRESULT CallNextHookEx([Optional] HHOOK hhk, int nCode, WPARAM wParam, LPARAM lParam);

	[DllImport("USER32.dll", ExactSpelling = true, SetLastError = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	public static extern BOOL SetCursorPos(int X, int Y);

	public unsafe static BOOL GetCursorPos(out Point lpPoint)
	{
		fixed (Point* lpPoint2 = &lpPoint)
		{
			return GetCursorPos(lpPoint2);
		}
	}

	[DllImport("USER32.dll", ExactSpelling = true, SetLastError = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	public unsafe static extern BOOL GetCursorPos(Point* lpPoint);
}
