using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;

namespace Windows.Win32.Devices.Display;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.275+02bc0c298e.RR")]
public struct DISPLAYCONFIG_PATH_SOURCE_INFO
{
	[StructLayout(LayoutKind.Explicit)]
	[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.275+02bc0c298e.RR")]
	public struct _Anonymous_e__Union
	{
		[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.275+02bc0c298e.RR")]
		public struct _Anonymous_e__Struct
		{
			public uint _bitfield;

			public ushort cloneGroupId
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				readonly get
				{
					return (ushort)(_bitfield & 0xFFFFu);
				}
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				set
				{
					_bitfield = (_bitfield & 0xFFFF0000u) | (value & 0xFFFFu);
				}
			}

			public ushort sourceModeInfoIdx
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				readonly get
				{
					return (ushort)((_bitfield >> 16) & 0xFFFFu);
				}
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				set
				{
					_bitfield = (_bitfield & 0xFFFFu) | (uint)((value & 0xFFFF) << 16);
				}
			}
		}

		[FieldOffset(0)]
		public uint modeInfoIdx;

		[FieldOffset(0)]
		public _Anonymous_e__Struct Anonymous;
	}

	public LUID adapterId;

	public uint id;

	public _Anonymous_e__Union Anonymous;

	public uint statusFlags;
}
