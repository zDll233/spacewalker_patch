using System.CodeDom.Compiler;
using System.Drawing;
using Windows.Win32.Foundation;

namespace Windows.Win32.UI.WindowsAndMessaging;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.275+02bc0c298e.RR")]
public struct MSG
{
	public HWND hwnd;

	public uint message;

	public WPARAM wParam;

	public LPARAM lParam;

	public uint time;

	public Point pt;
}
