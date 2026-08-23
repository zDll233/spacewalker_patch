using System;
using System.Runtime.InteropServices;

namespace VitureCommonLibrary.SlamV3;

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate void VitureSlamSaveCustomFileCallback(IntPtr savedDataPtr, int size);
