using System.CodeDom.Compiler;
using System.Drawing;

namespace Windows.Win32.Foundation;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.275+02bc0c298e.RR")]
public struct RECT
{
	public int left;

	public int top;

	public int right;

	public int bottom;

	public readonly int Width => right - left;

	public readonly int Height => bottom - top;

	public readonly bool IsEmpty
	{
		get
		{
			if (left == 0 && top == 0 && right == 0)
			{
				return bottom == 0;
			}
			return false;
		}
	}

	public readonly int X => left;

	public readonly int Y => top;

	public readonly Size Size => new Size(Width, Height);

	public RECT(Rectangle value)
		: this(value.Left, value.Top, value.Right, value.Bottom)
	{
	}

	public RECT(Point location, Size size)
		: this(location.X, location.Y, location.X + size.Width, location.Y + size.Height)
	{
	}

	public RECT(int left, int top, int right, int bottom)
	{
		this.left = left;
		this.top = top;
		this.right = right;
		this.bottom = bottom;
	}

	public static RECT FromXYWH(int x, int y, int width, int height)
	{
		return new RECT(x, y, x + width, y + height);
	}

	public static implicit operator Rectangle(RECT value)
	{
		return new Rectangle(value.left, value.top, value.Width, value.Height);
	}

	public static implicit operator RectangleF(RECT value)
	{
		return new RectangleF(value.left, value.top, value.Width, value.Height);
	}

	public static implicit operator RECT(Rectangle value)
	{
		return new RECT(value);
	}
}
