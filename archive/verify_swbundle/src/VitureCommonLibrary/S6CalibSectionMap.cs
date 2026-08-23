using System;

namespace VitureCommonLibrary;

public static class S6CalibSectionMap
{
	public static ushort GetReadMsgId(S6CalibSection section)
	{
		return section switch
		{
			S6CalibSection.ImuGyroTempDrift => 13058, 
			S6CalibSection.ImuCalibration => 13059, 
			S6CalibSection.MagCalibration => 13060, 
			S6CalibSection.AccTempDrift => 13061, 
			S6CalibSection.MagTempDrift => 13062, 
			S6CalibSection.DisplayOptical => 12545, 
			_ => throw new ArgumentOutOfRangeException("section", section, "Unknown S6CalibSection"), 
		};
	}

	public static string GetSectionName(S6CalibSection section)
	{
		return section switch
		{
			S6CalibSection.ImuGyroTempDrift => "imu_gyro_temp_drift", 
			S6CalibSection.ImuCalibration => "imu_calibration", 
			S6CalibSection.MagCalibration => "mag_calibration", 
			S6CalibSection.AccTempDrift => "acc_temp_drift", 
			S6CalibSection.MagTempDrift => "mag_temp_drift", 
			S6CalibSection.DisplayOptical => "display_optical", 
			_ => $"section_{(byte)section}", 
		};
	}
}
