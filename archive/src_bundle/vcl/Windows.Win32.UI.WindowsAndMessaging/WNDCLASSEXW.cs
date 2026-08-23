using System.CodeDom.Compiler;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Gdi;

namespace Windows.Win32.UI.WindowsAndMessaging;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.275+02bc0c298e.RR")]
public struct WNDCLASSEXW
{
	public uint cbSize;

	public WNDCLASS_STYLES style;

	public WNDPROC lpfnWndProc;

	public int cbClsExtra;

	public int cbWndExtra;

	public HINSTANCE hInstance;

	public HICON hIcon;

	public HCURSOR hCursor;

	public HBRUSH hbrBackground;

	public PCWSTR lpszMenuName;

	public PCWSTR lpszClassName;

	public HICON hIconSm;
}
