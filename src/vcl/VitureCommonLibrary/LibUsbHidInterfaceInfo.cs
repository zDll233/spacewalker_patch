namespace VitureCommonLibrary;

internal struct LibUsbHidInterfaceInfo
{
	public int InterfaceNumber;

	public byte EndpointIn;

	public byte EndpointOut;

	public ushort MaxPacketSize;
}
