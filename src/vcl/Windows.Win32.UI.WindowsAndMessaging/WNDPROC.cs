using System.CodeDom.Compiler;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;

namespace Windows.Win32.UI.WindowsAndMessaging;

[UnmanagedFunctionPointer(CallingConvention.Winapi)]
[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.275+02bc0c298e.RR")]
public delegate LRESULT WNDPROC(HWND param0, uint param1, WPARAM param2, LPARAM param3);
