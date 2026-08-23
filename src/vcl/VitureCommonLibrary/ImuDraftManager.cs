using System;
using System.Collections.Generic;

namespace VitureCommonLibrary;

public class ImuDraftManager
{
	private static readonly Lazy<ImuDraftManager> instance = new Lazy<ImuDraftManager>(() => new ImuDraftManager());

	private const int MAX_BUCKET_SIZE = 200;

	private const int QUEUE_MAX_SIZE = 700;

	private const int START_USING_COUNT = 400;

	private const int HISTOGRAM_THRESHOLD = 20;

	private Queue<int> historyQueue = new Queue<int>();

	private int[] frequencies = new int[700];

	private Vector3D lastOriginImu = Vector3D.zero;

	private int histogramCount;

	private Vector3D diffHistogram = Vector3D.zero;

	private float maxCompensation = 0.008f;

	private float compensateImu;

	private float totalCompensate;

	private ulong lastLogTimestamp;

	private float p6MaxDiffPitch = 0.003f;

	private float p6MaxCompensation = 0.02f;

	private float defaultMaxDiffPitch = 0.002f;

	private float defaultMaxCompensation = 0.06f;

	private float p6Offset = 0.002f;

	private float defaultOffset = 0.004f;

	public static ImuDraftManager Instance => instance.Value;

	public bool EnableCorrection { get; set; }

	public float Totalcompensate => totalCompensate;

	private ImuDraftManager()
	{
		ResetFrequencies();
		lastLogTimestamp = UnixTimestampHelper.GetMillisecondTimestamp();
	}

	public void CalculateFrequencies(float pitch, float yaw, float roll)
	{
		if (!EnableCorrection)
		{
			return;
		}
		bool p6Series = GlassesDeviceManager.Instance.P6Series;
		Vector3D vector3D = new Vector3D(yaw, pitch, roll);
		Vector3D vector3D2 = vector3D - lastOriginImu;
		if (vector3D2.X != 0f && p6Series)
		{
			float num = (p6Series ? p6MaxDiffPitch : defaultMaxDiffPitch);
			maxCompensation = (p6Series ? p6MaxCompensation : defaultMaxCompensation);
			if (Math.Abs(vector3D2.Y) < num && Math.Abs(vector3D2.Z) < num && Math.Abs(vector3D2.X) < maxCompensation)
			{
				AppendDiss(vector3D2.X);
			}
			lastOriginImu = vector3D;
			if (histogramCount > 20)
			{
				float num2 = CalculateMean();
				float num3 = 1f + Math.Abs(num2) * 0.3f;
				float num4 = (p6Series ? p6Offset : defaultOffset);
				compensateImu = (0f - (num2 + num4) * num3) * maxCompensation;
				histogramCount = 0;
			}
			histogramCount++;
			if (historyQueue.Count > 400 || historyQueue.Count >= 700)
			{
				totalCompensate += compensateImu;
			}
			ulong millisecondTimestamp = UnixTimestampHelper.GetMillisecondTimestamp();
			if (millisecondTimestamp - lastLogTimestamp > 30000)
			{
				lastLogTimestamp = millisecondTimestamp;
				Logger.Info($"Total compensation: {totalCompensate}");
			}
		}
	}

	private void AppendDiss(float diffX)
	{
		int num = (int)(diffX * (1f / maxCompensation) * 100f + 100f);
		if (num <= 0 || num >= 700)
		{
			return;
		}
		frequencies[num]++;
		if (historyQueue.Count >= 700)
		{
			int num2 = historyQueue.Dequeue();
			if (frequencies[num2] > 0)
			{
				frequencies[num2]--;
			}
		}
		historyQueue.Enqueue(num);
	}

	private float CalculateMean()
	{
		int num = 0;
		int[] array = frequencies;
		foreach (int num2 in array)
		{
			num += num2;
		}
		if (num <= 0)
		{
			return 0f;
		}
		float num3 = 0f;
		for (int j = 0; j < frequencies.Length; j++)
		{
			float num4 = -1f + (float)j * 0.01f;
			num4 += 0.005f;
			num3 += num4 * (float)frequencies[j];
		}
		return num3 / (float)num;
	}

	public void ResetFrequencies()
	{
		for (int i = 0; i < frequencies.Length; i++)
		{
			frequencies[i] = 0;
		}
		totalCompensate = 0f;
		lastLogTimestamp = UnixTimestampHelper.GetMillisecondTimestamp();
	}
}
