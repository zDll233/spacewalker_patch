namespace VitureCommonLibrary.SlamV3;

public struct Display
{
	public int width;

	public int height;

	public Quaternion q_display_imu;

	public Vector3 t_display_imu;

	public unsafe fixed float frustum_coefficients[6];
}
