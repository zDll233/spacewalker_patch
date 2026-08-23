using System.CodeDom.Compiler;

namespace Windows.Win32.UI.WindowsAndMessaging;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.275+02bc0c298e.RR")]
public enum WINDOWS_HOOK_ID
{
	WH_CALLWNDPROC = 4,
	WH_CALLWNDPROCRET = 12,
	WH_CBT = 5,
	WH_DEBUG = 9,
	WH_FOREGROUNDIDLE = 11,
	WH_GETMESSAGE = 3,
	WH_JOURNALPLAYBACK = 1,
	WH_JOURNALRECORD = 0,
	WH_KEYBOARD = 2,
	WH_KEYBOARD_LL = 13,
	WH_MOUSE = 7,
	WH_MOUSE_LL = 14,
	WH_MSGFILTER = -1,
	WH_SHELL = 10,
	WH_SYSMSGFILTER = 6
}
