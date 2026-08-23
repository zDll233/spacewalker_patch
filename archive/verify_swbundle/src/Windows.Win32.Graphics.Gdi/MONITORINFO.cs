using System.CodeDom.Compiler;
using Windows.Win32.Foundation;

namespace Windows.Win32.Graphics.Gdi;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.275+02bc0c298e.RR")]
public struct MONITORINFO
{
	public uint cbSize;

	public RECT rcMonitor;

	public RECT rcWork;

	public uint dwFlags;
}
