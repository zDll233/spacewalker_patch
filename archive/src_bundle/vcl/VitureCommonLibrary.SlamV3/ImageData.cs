using System;

namespace VitureCommonLibrary.SlamV3;

public struct ImageData
{
	public int camera_id;

	public int width;

	public int height;

	public Color color;

	public IntPtr data;
}
