using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Windows.Win32.Devices.Display;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.275+02bc0c298e.RR")]
public struct DISPLAYCONFIG_TARGET_DEVICE_NAME_FLAGS
{
	[StructLayout(LayoutKind.Explicit)]
	[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.275+02bc0c298e.RR")]
	public struct _Anonymous_e__Union
	{
		[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.275+02bc0c298e.RR")]
		public struct _Anonymous_e__Struct
		{
			public uint _bitfield;

			public bool friendlyNameFromEdid
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

			public bool friendlyNameForced
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

			public bool edidIdsValid
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

			public uint reserved
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				readonly get
				{
					return (_bitfield >> 3) & 0x1FFFFFFFu;
				}
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				set
				{
					_bitfield = (_bitfield & 7u) | ((value & 0x1FFFFFFF) << 3);
				}
			}
		}

		[FieldOffset(0)]
		public _Anonymous_e__Struct Anonymous;

		[FieldOffset(0)]
		public uint value;
	}

	public _Anonymous_e__Union Anonymous;
}
