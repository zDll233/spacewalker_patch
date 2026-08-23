using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Windows.Win32.Devices.Display;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.275+02bc0c298e.RR")]
public struct DISPLAYCONFIG_SUPPORT_VIRTUAL_RESOLUTION
{
	[StructLayout(LayoutKind.Explicit)]
	[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.275+02bc0c298e.RR")]
	public struct _Anonymous_e__Union
	{
		[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.275+02bc0c298e.RR")]
		public struct _Anonymous_e__Struct
		{
			public uint _bitfield;

			public bool disableMonitorVirtualResolution
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

			public uint reserved
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				readonly get
				{
					return (_bitfield >> 1) & 0x7FFFFFFFu;
				}
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				set
				{
					_bitfield = (_bitfield & 1u) | ((value & 0x7FFFFFFF) << 1);
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
}
