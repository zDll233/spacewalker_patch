namespace VitureCommonLibrary.SlamV3;

public struct Camera
{
	public int camera_id;

	public int width;

	public int height;

	public Quaternion q_camera_imu;

	public Vector3 t_camera_imu;

	public CameraModel camera_model;

	public unsafe fixed float intrinsics_coefficients[6];

	public DistortionModel distortion_model;

	public unsafe fixed float distortion_coefficients[14];

	public float timeshift_cam_imu;
}
