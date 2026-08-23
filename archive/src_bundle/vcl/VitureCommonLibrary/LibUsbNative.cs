using System;
using System.Runtime.InteropServices;

namespace VitureCommonLibrary;

internal static class LibUsbNative
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct libusb_device_descriptor
	{
		public byte bLength;

		public byte bDescriptorType;

		public ushort bcdUSB;

		public byte bDeviceClass;

		public byte bDeviceSubClass;

		public byte bDeviceProtocol;

		public byte bMaxPacketSize0;

		public ushort idVendor;

		public ushort idProduct;

		public ushort bcdDevice;

		public byte iManufacturer;

		public byte iProduct;

		public byte iSerialNumber;

		public byte bNumConfigurations;
	}

	public struct libusb_config_descriptor
	{
		public byte bLength;

		public byte bDescriptorType;

		public ushort wTotalLength;

		public byte bNumInterfaces;

		public byte bConfigurationValue;

		public byte iConfiguration;

		public byte bmAttributes;

		public byte MaxPower;

		public IntPtr @interface;

		public IntPtr extra;

		public int extra_length;
	}

	public struct libusb_interface
	{
		public IntPtr altsetting;

		public int num_altsetting;
	}

	public struct libusb_interface_descriptor
	{
		public byte bLength;

		public byte bDescriptorType;

		public byte bInterfaceNumber;

		public byte bAlternateSetting;

		public byte bNumEndpoints;

		public byte bInterfaceClass;

		public byte bInterfaceSubClass;

		public byte bInterfaceProtocol;

		public byte iInterface;

		public IntPtr endpoint;

		public IntPtr extra;

		public int extra_length;
	}

	public struct libusb_endpoint_descriptor
	{
		public byte bLength;

		public byte bDescriptorType;

		public byte bEndpointAddress;

		public byte bmAttributes;

		public ushort wMaxPacketSize;

		public byte bInterval;

		public byte bRefresh;

		public byte bSynchAddress;

		public IntPtr extra;

		public int extra_length;
	}

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate int libusb_hotplug_callback_fn(IntPtr ctx, IntPtr device, int hotplugEvent, IntPtr userData);

	private const string DLL = "libusb-1.0";

	public const int LIBUSB_SUCCESS = 0;

	public const int LIBUSB_ERROR_IO = -1;

	public const int LIBUSB_ERROR_INVALID_PARAM = -2;

	public const int LIBUSB_ERROR_ACCESS = -3;

	public const int LIBUSB_ERROR_NO_DEVICE = -4;

	public const int LIBUSB_ERROR_NOT_FOUND = -5;

	public const int LIBUSB_ERROR_BUSY = -6;

	public const int LIBUSB_ERROR_TIMEOUT = -7;

	public const int LIBUSB_ERROR_OVERFLOW = -8;

	public const int LIBUSB_ERROR_PIPE = -9;

	public const int LIBUSB_ERROR_INTERRUPTED = -10;

	public const int LIBUSB_ERROR_NO_MEM = -11;

	public const int LIBUSB_ERROR_NOT_SUPPORTED = -12;

	public const int LIBUSB_ERROR_OTHER = -99;

	public const byte LIBUSB_CLASS_HID = 3;

	public const byte LIBUSB_ENDPOINT_DIR_MASK = 128;

	public const byte LIBUSB_ENDPOINT_IN = 128;

	public const byte LIBUSB_ENDPOINT_OUT = 0;

	public const byte LIBUSB_TRANSFER_TYPE_MASK = 3;

	public const byte LIBUSB_TRANSFER_TYPE_INTERRUPT = 3;

	public const byte LIBUSB_REQUEST_TYPE_CLASS = 32;

	public const byte LIBUSB_RECIPIENT_INTERFACE = 1;

	public const byte HID_SET_REPORT = 9;

	public const ushort HID_REPORT_TYPE_OUTPUT = 512;

	public const uint LIBUSB_CAP_HAS_HOTPLUG = 1u;

	public const uint LIBUSB_CAP_HAS_HID_ACCESS = 256u;

	public const uint LIBUSB_CAP_SUPPORTS_DETACH_KERNEL_DRIVER = 257u;

	public const int LIBUSB_HOTPLUG_EVENT_DEVICE_ARRIVED = 1;

	public const int LIBUSB_HOTPLUG_EVENT_DEVICE_LEFT = 2;

	public const int LIBUSB_HOTPLUG_MATCH_ANY = -1;

	public const int LIBUSB_HOTPLUG_NO_FLAGS = 0;

	public const int LIBUSB_HOTPLUG_ENUMERATE = 1;

	[DllImport("libusb-1.0", CallingConvention = CallingConvention.Cdecl)]
	public static extern int libusb_init(ref IntPtr ctx);

	[DllImport("libusb-1.0", CallingConvention = CallingConvention.Cdecl)]
	public static extern void libusb_exit(IntPtr ctx);

	[DllImport("libusb-1.0", CallingConvention = CallingConvention.Cdecl)]
	public static extern int libusb_has_capability(uint capability);

	[DllImport("libusb-1.0", CallingConvention = CallingConvention.Cdecl)]
	public static extern int libusb_get_device_list(IntPtr ctx, out IntPtr list);

	[DllImport("libusb-1.0", CallingConvention = CallingConvention.Cdecl)]
	public static extern void libusb_free_device_list(IntPtr list, int unref_devices);

	[DllImport("libusb-1.0", CallingConvention = CallingConvention.Cdecl)]
	public static extern int libusb_get_device_descriptor(IntPtr dev, out libusb_device_descriptor desc);

	[DllImport("libusb-1.0", CallingConvention = CallingConvention.Cdecl)]
	public static extern int libusb_get_config_descriptor(IntPtr dev, byte config_index, out IntPtr config);

	[DllImport("libusb-1.0", CallingConvention = CallingConvention.Cdecl)]
	public static extern void libusb_free_config_descriptor(IntPtr config);

	[DllImport("libusb-1.0", CallingConvention = CallingConvention.Cdecl)]
	public static extern int libusb_open(IntPtr dev, out IntPtr handle);

	[DllImport("libusb-1.0", CallingConvention = CallingConvention.Cdecl)]
	public static extern void libusb_close(IntPtr handle);

	[DllImport("libusb-1.0", CallingConvention = CallingConvention.Cdecl)]
	public static extern int libusb_claim_interface(IntPtr handle, int interface_number);

	[DllImport("libusb-1.0", CallingConvention = CallingConvention.Cdecl)]
	public static extern int libusb_release_interface(IntPtr handle, int interface_number);

	[DllImport("libusb-1.0", CallingConvention = CallingConvention.Cdecl)]
	public static extern int libusb_set_auto_detach_kernel_driver(IntPtr handle, int enable);

	[DllImport("libusb-1.0", CallingConvention = CallingConvention.Cdecl)]
	public static extern int libusb_detach_kernel_driver(IntPtr handle, int interface_number);

	[DllImport("libusb-1.0", CallingConvention = CallingConvention.Cdecl)]
	public static extern int libusb_kernel_driver_active(IntPtr handle, int interface_number);

	[DllImport("libusb-1.0", CallingConvention = CallingConvention.Cdecl)]
	public static extern int libusb_interrupt_transfer(IntPtr handle, byte endpoint, byte[] data, int length, out int actual_length, uint timeout);

	[DllImport("libusb-1.0", CallingConvention = CallingConvention.Cdecl)]
	public static extern int libusb_control_transfer(IntPtr handle, byte request_type, byte bRequest, ushort wValue, ushort wIndex, byte[] data, ushort wLength, uint timeout);

	[DllImport("libusb-1.0", CallingConvention = CallingConvention.Cdecl)]
	public static extern int libusb_clear_halt(IntPtr handle, byte endpoint);

	[DllImport("libusb-1.0", CallingConvention = CallingConvention.Cdecl)]
	public static extern IntPtr libusb_ref_device(IntPtr dev);

	[DllImport("libusb-1.0", CallingConvention = CallingConvention.Cdecl)]
	public static extern void libusb_unref_device(IntPtr dev);

	[DllImport("libusb-1.0", CallingConvention = CallingConvention.Cdecl)]
	public static extern byte libusb_get_bus_number(IntPtr dev);

	[DllImport("libusb-1.0", CallingConvention = CallingConvention.Cdecl)]
	public static extern byte libusb_get_device_address(IntPtr dev);

	[DllImport("libusb-1.0", CallingConvention = CallingConvention.Cdecl)]
	public static extern int libusb_hotplug_register_callback(IntPtr ctx, int events, int flags, int vendor_id, int product_id, int dev_class, libusb_hotplug_callback_fn cb_fn, IntPtr user_data, out int callback_handle);

	[DllImport("libusb-1.0", CallingConvention = CallingConvention.Cdecl)]
	public static extern void libusb_hotplug_deregister_callback(IntPtr ctx, int callback_handle);

	[DllImport("libusb-1.0", CallingConvention = CallingConvention.Cdecl)]
	public static extern int libusb_handle_events(IntPtr ctx);

	[DllImport("libusb-1.0", CallingConvention = CallingConvention.Cdecl)]
	public static extern void libusb_interrupt_event_handler(IntPtr ctx);
}
