using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Win32.Graphics.Gdi;

namespace Windows.Win32.Devices.Display;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.275+02bc0c298e.RR")]
public struct DISPLAYCONFIG_GET_ADVANCED_COLOR_INFO
{
	[StructLayout(LayoutKind.Explicit)]
	[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.275+02bc0c298e.RR")]
	public struct _Anonymous_e__Union
	{
		[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.275+02bc0c298e.RR")]
		public struct _Anonymous_e__Struct
		{
			public uint _bitfield;

			public bool advancedColorSupported
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				readonly get
				{
					return (_bitfield & 1) != 0;
				}
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				set
				{
					_bitfield = (value ? (_bitfield | 1u) : (_bitfield & 0xFFFFFFFEu));
				}
			}

			public bool advancedColorEnabled
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				readonly get
				{
					return (_bitfield & 2) != 0;
				}
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				set
				{
					_bitfield = (value ? (_bitfield | 2u) : (_bitfield & 0xFFFFFFFDu));
				}
			}

			public bool wideColorEnforced
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				readonly get
				{
					return (_bitfield & 4) != 0;
				}
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				set
				{
					_bitfield = (value ? (_bitfield | 4u) : (_bitfield & 0xFFFFFFFBu));
				}
			}

			public bool advancedColorForceDisabled
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				readonly get
				{
					return (_bitfield & 8) != 0;
				}
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				set
				{
					_bitfield = (value ? (_bitfield | 8u) : (_bitfield & 0xFFFFFFF7u));
				}
			}

			public uint reserved
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				readonly get
				{
					return (_bitfield >> 4) & 0xFFFFFFFu;
				}
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				set
				{
					_bitfield = (_bitfield & 0xFu) | ((value & 0xFFFFFFF) << 4);
				}
			}
		}

		[FieldOffset(0)]
		public _Anonymous_e__Struct Anonymous;

		[FieldOffset(0)]
		public uint value;
	}

	public DISPLAYCONFIG_DEVICE_INFO_HEADER header;

	public _Anonymous_e__Union Anonymous;

	public DISPLAYCONFIG_COLOR_ENCODING colorEncoding;

	public uint bitsPerColorChannel;
}
