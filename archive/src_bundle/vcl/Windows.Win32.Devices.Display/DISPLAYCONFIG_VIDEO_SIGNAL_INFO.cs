using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Windows.Win32.Devices.Display;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.275+02bc0c298e.RR")]
public struct DISPLAYCONFIG_VIDEO_SIGNAL_INFO
{
	[StructLayout(LayoutKind.Explicit)]
	[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.275+02bc0c298e.RR")]
	public struct _Anonymous_e__Union
	{
		[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.275+02bc0c298e.RR")]
		public struct _AdditionalSignalInfo_e__Struct
		{
			public uint _bitfield;

			public ushort videoStandard
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

			public byte vSyncFreqDivider
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				readonly get
				{
					return (byte)((_bitfield >> 16) & 0x3Fu);
				}
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				set
				{
					_bitfield = (_bitfield & 0xFFC0FFFFu) | (uint)((value & 0x3F) << 16);
				}
			}

			public ushort reserved
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				readonly get
				{
					return (ushort)((_bitfield >> 22) & 0x3FFu);
				}
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				set
				{
					_bitfield = (_bitfield & 0x3FFFFFu) | (uint)((value & 0x3FF) << 22);
				}
			}
		}

		[FieldOffset(0)]
		public _AdditionalSignalInfo_e__Struct AdditionalSignalInfo;

		[FieldOffset(0)]
		public uint videoStandard;
	}

	public ulong pixelRate;

	public DISPLAYCONFIG_RATIONAL hSyncFreq;

	public DISPLAYCONFIG_RATIONAL vSyncFreq;

	public DISPLAYCONFIG_2DREGION activeSize;

	public DISPLAYCONFIG_2DREGION totalSize;

	public _Anonymous_e__Union Anonymous;

	public DISPLAYCONFIG_SCANLINE_ORDERING scanLineOrdering;
}
