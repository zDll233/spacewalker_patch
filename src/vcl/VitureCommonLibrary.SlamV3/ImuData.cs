namespace VitureCommonLibrary.SlamV3;

public struct ImuData
{
	public ulong timestamp;

	public float temperature;

	public Vector3 gyr;

	public Vector3 acc;

	public Vector3 mag;
}
