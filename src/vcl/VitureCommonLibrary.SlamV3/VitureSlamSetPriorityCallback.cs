using System.Runtime.InteropServices;

namespace VitureCommonLibrary.SlamV3;

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate void VitureSlamSetPriorityCallback(ulong threadId, ThreadPriorityLevel priority);
