using System;
using System.Collections.Generic;
using VitureCommonLibrary;

internal class Statistics
{
	internal static void ProcessStatistics(float duration, ref ulong imuDataCount, ref ulong frameCount, List<ulong> imuTimestamps)
	{
		if (duration > 0f)
		{
			float num = (float)imuDataCount / duration;
			float num2 = (float)frameCount / duration;
			Logger.Info($"duration: {duration:F3} frameRate:{num2} imuRate: {num}");
			Logger.Info($"frameRate:{num2} imuRate: {num}");
		}
		if (imuTimestamps.Count > 0)
		{
			double num3 = CalculateMean(imuTimestamps);
			double num4 = CalculateVariance(imuTimestamps, num3);
			double num5 = CalculateStandardDeviation(imuTimestamps, num3);
			Logger.Info($"mean:{num3} variance: {num4} standardDeviation: {num5}");
			imuTimestamps.Clear();
		}
		imuDataCount = 0uL;
		frameCount = 0uL;
	}

	private static double CalculateMean(List<ulong> values)
	{
		double num = 0.0;
		foreach (ulong value in values)
		{
			num += (double)value;
		}
		return num / (double)values.Count;
	}

	private static double CalculateVariance(List<ulong> values, double mean)
	{
		double num = 0.0;
		foreach (ulong value in values)
		{
			num += Math.Pow((double)value - mean, 2.0);
		}
		return num / (double)(values.Count - 1);
	}

	private static double CalculateStandardDeviation(List<ulong> values, double mean)
	{
		return Math.Sqrt(CalculateVariance(values, mean));
	}

	private static int RoundUpToNearestFive(float number)
	{
		int num = (int)Math.Ceiling(number);
		int num2 = num % 5;
		if (num2 != 0)
		{
			num += 5 - num2;
		}
		return num;
	}
}
