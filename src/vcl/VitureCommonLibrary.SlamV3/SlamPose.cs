namespace VitureCommonLibrary.SlamV3;

public struct SlamPose
{
	public SlamMode slam_mode;

	public ulong timestamp;

	public Quaternion orientation;

	public Vector3 angular_velocity;

	public Vector3 angular_acceleration;

	public Vector3 position;

	public Vector3 linear_velocity;

	public Vector3 linear_acceleration;
}
