using System;
using System.Runtime.InteropServices;

namespace VitureCommonLibrary;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct Vector3D
{
	public float X;

	public float Y;

	public float Z;

	public static readonly Vector3D zero = new Vector3D(0f, 0f, 0f);

	public override string ToString()
	{
		return $"({X}, {Y}, {Z})";
	}

	public Vector3D()
	{
		X = 0f;
		Y = 0f;
		Z = 0f;
	}

	public Vector3D(float x, float y, float z)
	{
		X = x;
		Y = y;
		Z = z;
	}

	public static Vector3D operator +(Vector3D a, Vector3D b)
	{
		return new Vector3D(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
	}

	public static Vector3D operator -(Vector3D a, Vector3D b)
	{
		return new Vector3D(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
	}

	public static Vector3D operator -(Vector3D v)
	{
		return new Vector3D(0f - v.X, 0f - v.Y, 0f - v.Z);
	}

	public static Vector3D operator *(Vector3D v, float scalar)
	{
		return new Vector3D(v.X * scalar, v.Y * scalar, v.Z * scalar);
	}

	public static Vector3D operator *(float scalar, Vector3D v)
	{
		return v * scalar;
	}

	public static Vector3D FromBytes(byte[] bytes, int startIndex = 0)
	{
		Vector3D vector3D = default(Vector3D);
		int num = Marshal.SizeOf(vector3D);
		IntPtr intPtr = Marshal.AllocHGlobal(num);
		Marshal.Copy(bytes, startIndex, intPtr, num);
		vector3D = (Vector3D)Marshal.PtrToStructure(intPtr, vector3D.GetType());
		Marshal.FreeHGlobal(intPtr);
		return vector3D;
	}

	public byte[] ToBytes()
	{
		int num = Marshal.SizeOf(this);
		byte[] array = new byte[num];
		IntPtr intPtr = Marshal.AllocHGlobal(num);
		Marshal.StructureToPtr(this, intPtr, fDeleteOld: true);
		Marshal.Copy(intPtr, array, 0, num);
		Marshal.FreeHGlobal(intPtr);
		return array;
	}

	public static Vector3D FromBytesBigEndian(byte[] bytes, int startIndex = 0)
	{
		Vector3D result = default(Vector3D);
		byte[] array = new byte[4];
		byte[] array2 = new byte[4];
		byte[] array3 = new byte[4];
		Array.Copy(bytes, startIndex, array, 0, 4);
		Array.Copy(bytes, startIndex + 4, array2, 0, 4);
		Array.Copy(bytes, startIndex + 8, array3, 0, 4);
		Array.Reverse((Array)array);
		Array.Reverse((Array)array2);
		Array.Reverse((Array)array3);
		result.X = BitConverter.ToSingle(array, 0);
		result.Y = BitConverter.ToSingle(array2, 0);
		result.Z = BitConverter.ToSingle(array3, 0);
		return result;
	}

	public byte[] ToBytesBigEndian()
	{
		int num = Marshal.SizeOf(this);
		byte[] array = new byte[num];
		IntPtr hglobal = Marshal.AllocHGlobal(num);
		byte[] bytes = BitConverter.GetBytes(X);
		byte[] bytes2 = BitConverter.GetBytes(Y);
		byte[] bytes3 = BitConverter.GetBytes(Z);
		Array.Reverse((Array)bytes);
		Array.Reverse((Array)bytes2);
		Array.Reverse((Array)bytes3);
		Array.Copy(bytes, 0, array, 0, 4);
		Array.Copy(bytes2, 0, array, 4, 4);
		Array.Copy(bytes3, 0, array, 8, 4);
		Marshal.FreeHGlobal(hglobal);
		return array;
	}
}
