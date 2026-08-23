using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace VitureCommonLibrary;

internal static class LibUsbHidHelper
{
	private static IntPtr _ctx = IntPtr.Zero;

	private static bool _initialized = false;

	private static readonly object _initLock = new object();

	internal static IntPtr GetContext()
	{
		EnsureInitialized();
		return _ctx;
	}

	internal static void EnsureInitialized()
	{
		if (_initialized)
		{
			return;
		}
		lock (_initLock)
		{
			if (!_initialized && LibUsbNative.libusb_init(ref _ctx) == 0)
			{
				_initialized = true;
			}
		}
	}

	private static List<LibUsbHidInterfaceInfo> FindHidInterfaces(IntPtr dev)
	{
		List<LibUsbHidInterfaceInfo> list = new List<LibUsbHidInterfaceInfo>();
		if (LibUsbNative.libusb_get_config_descriptor(dev, 0, out var config) != 0 || config == IntPtr.Zero)
		{
			return list;
		}
		try
		{
			LibUsbNative.libusb_config_descriptor libusb_config_descriptor = Marshal.PtrToStructure<LibUsbNative.libusb_config_descriptor>(config);
			int num = Marshal.SizeOf<LibUsbNative.libusb_interface>();
			for (int i = 0; i < libusb_config_descriptor.bNumInterfaces; i++)
			{
				LibUsbNative.libusb_interface libusb_interface = Marshal.PtrToStructure<LibUsbNative.libusb_interface>(new IntPtr(libusb_config_descriptor.@interface.ToInt64() + i * num));
				if (libusb_interface.num_altsetting == 0 || libusb_interface.altsetting == IntPtr.Zero)
				{
					continue;
				}
				LibUsbNative.libusb_interface_descriptor libusb_interface_descriptor = Marshal.PtrToStructure<LibUsbNative.libusb_interface_descriptor>(libusb_interface.altsetting);
				if (libusb_interface_descriptor.bInterfaceClass != 3)
				{
					continue;
				}
				byte b = 0;
				byte endpointOut = 0;
				ushort maxPacketSize = 64;
				int num2 = Marshal.SizeOf<LibUsbNative.libusb_endpoint_descriptor>();
				for (int j = 0; j < libusb_interface_descriptor.bNumEndpoints; j++)
				{
					LibUsbNative.libusb_endpoint_descriptor libusb_endpoint_descriptor = Marshal.PtrToStructure<LibUsbNative.libusb_endpoint_descriptor>(new IntPtr(libusb_interface_descriptor.endpoint.ToInt64() + j * num2));
					if ((byte)(libusb_endpoint_descriptor.bmAttributes & 3) == 3)
					{
						if ((libusb_endpoint_descriptor.bEndpointAddress & 0x80) == 128)
						{
							b = libusb_endpoint_descriptor.bEndpointAddress;
							maxPacketSize = libusb_endpoint_descriptor.wMaxPacketSize;
						}
						else
						{
							endpointOut = libusb_endpoint_descriptor.bEndpointAddress;
						}
					}
				}
				if (b != 0)
				{
					list.Add(new LibUsbHidInterfaceInfo
					{
						InterfaceNumber = libusb_interface_descriptor.bInterfaceNumber,
						EndpointIn = b,
						EndpointOut = endpointOut,
						MaxPacketSize = maxPacketSize
					});
				}
			}
			return list;
		}
		finally
		{
			LibUsbNative.libusb_free_config_descriptor(config);
		}
	}

	internal static List<LibUsbHidDeviceInfo> Enumerate(int vendorId)
	{
		EnsureInitialized();
		List<LibUsbHidDeviceInfo> list = new List<LibUsbHidDeviceInfo>();
		IntPtr list2;
		int num = LibUsbNative.libusb_get_device_list(_ctx, out list2);
		if (num < 0 || list2 == IntPtr.Zero)
		{
			return list;
		}
		try
		{
			for (int i = 0; i < num; i++)
			{
				IntPtr intPtr = Marshal.ReadIntPtr(list2, i * IntPtr.Size);
				if (intPtr == IntPtr.Zero)
				{
					break;
				}
				if (LibUsbNative.libusb_get_device_descriptor(intPtr, out var desc) != 0 || desc.idVendor != (ushort)vendorId)
				{
					continue;
				}
				List<LibUsbHidInterfaceInfo> list3 = FindHidInterfaces(intPtr);
				byte b = LibUsbNative.libusb_get_bus_number(intPtr);
				byte b2 = LibUsbNative.libusb_get_device_address(intPtr);
				foreach (LibUsbHidInterfaceInfo item in list3)
				{
					list.Add(new LibUsbHidDeviceInfo
					{
						VendorId = desc.idVendor,
						ProductId = desc.idProduct,
						Path = $"libusb:{b}:{b2}:{item.InterfaceNumber}"
					});
				}
			}
		}
		finally
		{
			LibUsbNative.libusb_free_device_list(list2, 1);
		}
		return list;
	}

	internal static List<LibUsbHidDevice> EnumerateDevices(int vendorId)
	{
		EnsureInitialized();
		List<LibUsbHidDevice> list = new List<LibUsbHidDevice>();
		IntPtr list2;
		int num = LibUsbNative.libusb_get_device_list(_ctx, out list2);
		if (num < 0 || list2 == IntPtr.Zero)
		{
			return list;
		}
		try
		{
			for (int i = 0; i < num; i++)
			{
				IntPtr intPtr = Marshal.ReadIntPtr(list2, i * IntPtr.Size);
				if (intPtr == IntPtr.Zero)
				{
					break;
				}
				if (LibUsbNative.libusb_get_device_descriptor(intPtr, out var desc) != 0 || desc.idVendor != (ushort)vendorId)
				{
					continue;
				}
				List<LibUsbHidInterfaceInfo> list3 = FindHidInterfaces(intPtr);
				byte b = LibUsbNative.libusb_get_bus_number(intPtr);
				byte b2 = LibUsbNative.libusb_get_device_address(intPtr);
				foreach (LibUsbHidInterfaceInfo item in list3)
				{
					IntPtr devRef = LibUsbNative.libusb_ref_device(intPtr);
					string path = $"libusb:{b}:{b2}:{item.InterfaceNumber}";
					list.Add(new LibUsbHidDevice(devRef, desc.idProduct, path, item.InterfaceNumber, item.EndpointIn, item.EndpointOut, item.MaxPacketSize));
				}
			}
		}
		finally
		{
			LibUsbNative.libusb_free_device_list(list2, 1);
		}
		return list;
	}
}
