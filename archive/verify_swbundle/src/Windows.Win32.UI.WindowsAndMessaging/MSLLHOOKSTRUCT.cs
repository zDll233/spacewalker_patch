using System.CodeDom.Compiler;
using System.Drawing;

namespace Windows.Win32.UI.WindowsAndMessaging;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.275+02bc0c298e.RR")]
public struct MSLLHOOKSTRUCT
{
	public Point pt;

	public uint mouseData;

	public uint flags;

	public uint time;

	public nuint dwExtraInfo;
}
