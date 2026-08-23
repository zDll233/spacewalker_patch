using System;

namespace VitureCommonLibrary.SlamV3;

public struct Imu
{
	public Vector3 gyr_bias;

	public Matrix3 gyr_correction_matrix;

	public float gyr_noise_density;

	public float gyr_random_walk;

	public float acc_scale;

	public Vector3 acc_bias;

	public Matrix3 acc_correction_matrix;

	public float acc_noise_density;

	public float acc_random_walk;

	public Vector3 mag_bias;

	public Matrix3 mag_correction_matrix;

	public int num_thermal_gyr_biases;

	public IntPtr thermal_gyr_biases;
}
