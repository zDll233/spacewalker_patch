using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using VitureCommonLibrary.SlamV3;

namespace VitureCommonLibrary;

public static class S6DeviceInfoBuilder
{
	private const int Header = 8;

	private const int ThermalCapacity = 500;

	public static bool TryBuild(S6Calibration? calib, out Device device, out IntPtr thermalArrayPtr)
	{
		device = default(Device);
		thermalArrayPtr = IntPtr.Zero;
		if (calib == null)
		{
			Logger.Warning("S6DeviceInfoBuilder.TryBuild: calib is null");
			return false;
		}
		device.sn = calib.Sn ?? string.Empty;
		if (device.sn.Length > 31)
		{
			device.sn = device.sn.Substring(0, 31);
		}
		if (calib.Buffers.TryGetValue(S6CalibSection.ImuCalibration, out byte[] value) && value != null && value.Length >= 8)
		{
			try
			{
				int off = 8;
				device.imu.gyr_bias = ReadVec3(value, ref off);
				device.imu.gyr_correction_matrix = ReadMat3(value, ref off);
				device.imu.gyr_noise_density = ReadFloat(value, ref off);
				device.imu.gyr_random_walk = ReadFloat(value, ref off);
				ReadFloat(value, ref off);
				device.imu.acc_scale = 1f;
				device.imu.acc_bias = ReadVec3(value, ref off);
				device.imu.acc_correction_matrix = ReadMat3(value, ref off);
				device.imu.acc_noise_density = ReadFloat(value, ref off);
				device.imu.acc_random_walk = ReadFloat(value, ref off);
			}
			catch (Exception ex)
			{
				Logger.Warning("S6DeviceInfoBuilder: ImuCalibration parse exception: " + ex.Message);
			}
		}
		else
		{
			Logger.Warning("S6DeviceInfoBuilder: ImuCalibration section missing — IMU fields stay zero-initialized");
		}
		if (calib.Buffers.TryGetValue(S6CalibSection.MagCalibration, out byte[] value2) && value2 != null && value2.Length >= 8)
		{
			try
			{
				int off2 = 8;
				device.imu.mag_bias = ReadVec3(value2, ref off2);
				device.imu.mag_correction_matrix = ReadMat3(value2, ref off2);
			}
			catch (Exception ex2)
			{
				Logger.Warning("S6DeviceInfoBuilder: MagCalibration parse exception: " + ex2.Message);
			}
		}
		if (calib.Buffers.TryGetValue(S6CalibSection.ImuGyroTempDrift, out byte[] value3) && value3 != null && value3.Length >= 12)
		{
			try
			{
				int num = 8;
				uint val = BinaryPrimitives.ReadUInt32LittleEndian(value3.AsSpan(num, 4));
				num += 4;
				int num2 = (int)Math.Min(val, 500u);
				List<ThermalBias> list = new List<ThermalBias>(num2);
				for (int i = 0; i < num2; i++)
				{
					if (num + 16 > value3.Length)
					{
						break;
					}
					float num3 = ReadFloat(value3, ref num);
					float num4 = ReadFloat(value3, ref num);
					float num5 = ReadFloat(value3, ref num);
					float num6 = ReadFloat(value3, ref num);
					if (!float.IsNaN(num3) && !float.IsNaN(num4) && !float.IsNaN(num5) && !float.IsNaN(num6))
					{
						list.Add(new ThermalBias
						{
							temperature = num3,
							bias = new Vector3
							{
								x = num4,
								y = num5,
								z = num6
							}
						});
					}
				}
				if (list.Count > 0)
				{
					int num7 = Marshal.SizeOf<ThermalBias>();
					thermalArrayPtr = Marshal.AllocHGlobal(list.Count * num7);
					for (int j = 0; j < list.Count; j++)
					{
						Marshal.StructureToPtr(list[j], thermalArrayPtr + j * num7, fDeleteOld: false);
					}
					device.imu.num_thermal_gyr_biases = list.Count;
					device.imu.thermal_gyr_biases = thermalArrayPtr;
				}
				else
				{
					device.imu.num_thermal_gyr_biases = 0;
					device.imu.thermal_gyr_biases = IntPtr.Zero;
				}
			}
			catch (Exception ex3)
			{
				Logger.Warning("S6DeviceInfoBuilder: ImuGyroTempDrift parse exception: " + ex3.Message);
				device.imu.num_thermal_gyr_biases = 0;
				device.imu.thermal_gyr_biases = IntPtr.Zero;
			}
		}
		else
		{
			device.imu.num_thermal_gyr_biases = 0;
			device.imu.thermal_gyr_biases = IntPtr.Zero;
		}
		S6DisplayOpticalParam displayOptical = calib.DisplayOptical;
		if (displayOptical != null)
		{
			FillDisplay(ref device.display_left, displayOptical, useRightEye: false);
			FillDisplay(ref device.display_right, displayOptical, useRightEye: true);
		}
		else
		{
			Logger.Warning("S6DeviceInfoBuilder: DisplayOptical missing — Device.display_left/right left zero-initialized");
		}
		device.num_cameras = 0;
		device.cameras = IntPtr.Zero;
		return true;
	}

	public static void FreeNative(ref Device device, ref IntPtr thermalArrayPtr)
	{
		if (thermalArrayPtr != IntPtr.Zero)
		{
			Marshal.FreeHGlobal(thermalArrayPtr);
			thermalArrayPtr = IntPtr.Zero;
		}
		device.imu.num_thermal_gyr_biases = 0;
		device.imu.thermal_gyr_biases = IntPtr.Zero;
	}

	private unsafe static void FillDisplay(ref Display d, S6DisplayOpticalParam disp, bool useRightEye)
	{
		d.width = (int)disp.Width;
		d.height = (int)disp.Height;
		float[] array = (useRightEye ? disp.QRightImu : disp.QLeftImu);
		float[] array2 = (useRightEye ? disp.TRightImu : disp.TLeftImu);
		d.q_display_imu = new Quaternion
		{
			w = array[0],
			x = array[1],
			y = array[2],
			z = array[3]
		};
		d.t_display_imu = new Vector3
		{
			x = array2[0],
			y = array2[1],
			z = array2[2]
		};
		float[] array3 = (useRightEye ? disp.InnerRightIntrinsics : disp.InnerLeftIntrinsics);
		for (int i = 0; i < 6 && i < array3.Length; i++)
		{
			d.frustum_coefficients[i] = array3[i];
		}
	}

	private static Vector3 ReadVec3(byte[] buf, ref int off)
	{
		float x = ReadFloat(buf, ref off);
		float y = ReadFloat(buf, ref off);
		float z = ReadFloat(buf, ref off);
		Vector3 result = default(Vector3);
		result.x = x;
		result.y = y;
		result.z = z;
		return result;
	}

	private static Matrix3 ReadMat3(byte[] buf, ref int off)
	{
		Matrix3 result = default(Matrix3);
		result.m00 = ReadFloat(buf, ref off);
		result.m01 = ReadFloat(buf, ref off);
		result.m02 = ReadFloat(buf, ref off);
		result.m10 = ReadFloat(buf, ref off);
		result.m11 = ReadFloat(buf, ref off);
		result.m12 = ReadFloat(buf, ref off);
		result.m20 = ReadFloat(buf, ref off);
		result.m21 = ReadFloat(buf, ref off);
		result.m22 = ReadFloat(buf, ref off);
		return result;
	}

	private static float ReadFloat(byte[] buf, ref int off)
	{
		float result = MemoryMarshal.Read<float>(buf.AsSpan(off, 4));
		off += 4;
		return result;
	}
}
