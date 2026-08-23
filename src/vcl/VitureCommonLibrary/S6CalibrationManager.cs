using System;
using System.Buffers.Binary;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace VitureCommonLibrary;

public sealed class S6CalibrationManager
{
	private static readonly Lazy<S6CalibrationManager> _lazy = new Lazy<S6CalibrationManager>(() => new S6CalibrationManager());

	private const string CacheRootSubpath = "VITURE\\SpaceWalker\\S6";

	private const string DllInboxFolder = "dll_inbox";

	private const string CacheFileExtension = ".bin";

	private static readonly byte[] FileMagic = Encoding.ASCII.GetBytes("VITS6CAL");

	private const ushort FileFormatVersion = 1;

	private readonly object _gate = new object();

	private S6Calibration? _current;

	public static S6CalibrationManager Instance => _lazy.Value;

	public S6Calibration? Current
	{
		get
		{
			lock (_gate)
			{
				return _current;
			}
		}
	}

	private S6CalibrationManager()
	{
	}

	public Task<bool> EnsureCalibrationLoaded(string sn, string firmwareVersion, CancellationToken ct = default(CancellationToken))
	{
		string sn2 = sn;
		string firmwareVersion2 = firmwareVersion;
		return Task.Run(() => EnsureCalibrationLoadedSync(sn2, firmwareVersion2, ct), ct);
	}

	public bool LoadFromCache(string sn)
	{
		if (string.IsNullOrWhiteSpace(sn))
		{
			return false;
		}
		string cacheDir = GetCacheDir(sn);
		if (!Directory.Exists(cacheDir))
		{
			Logger.Warning("S6CalibrationManager.LoadFromCache: dir not exist " + cacheDir);
			return false;
		}
		S6Calibration s6Calibration = new S6Calibration
		{
			Sn = sn
		};
		bool flag = false;
		foreach (S6CalibSection value2 in Enum.GetValues(typeof(S6CalibSection)))
		{
			string sectionPath = GetSectionPath(cacheDir, value2);
			if (!File.Exists(sectionPath))
			{
				Logger.Warning($"S6CalibrationManager.LoadFromCache: missing {value2} at {sectionPath}");
				continue;
			}
			if (!TryReadSectionFile(sectionPath, out byte[] payload, out string firmware))
			{
				Logger.Warning("S6CalibrationManager.LoadFromCache: invalid file " + sectionPath);
				continue;
			}
			s6Calibration.Buffers[value2] = payload;
			if (!string.IsNullOrEmpty(firmware))
			{
				s6Calibration.FirmwareVersion = firmware;
			}
			flag = true;
		}
		if (s6Calibration.Buffers.TryGetValue(S6CalibSection.DisplayOptical, out byte[] value) && S6DisplayOpticalParam.TryParse(value, out S6DisplayOpticalParam parsed))
		{
			s6Calibration.DisplayOptical = parsed;
		}
		if (flag)
		{
			lock (_gate)
			{
				_current = s6Calibration;
			}
			Logger.Info($"S6CalibrationManager.LoadFromCache: {s6Calibration}");
			return true;
		}
		return false;
	}

	public bool TryGetSection(S6CalibSection section, out byte[] buffer)
	{
		buffer = Array.Empty<byte>();
		S6Calibration current = Current;
		if (current == null)
		{
			return false;
		}
		if (current.Buffers.TryGetValue(section, out byte[] value))
		{
			buffer = value;
			return true;
		}
		return false;
	}

	public S6DisplayOpticalParam? GetDisplayOptical()
	{
		return Current?.DisplayOptical;
	}

	public void ClearMemory()
	{
		lock (_gate)
		{
			_current = null;
		}
	}

	public void ClearCache(string sn)
	{
		ClearMemory();
		if (string.IsNullOrWhiteSpace(sn))
		{
			return;
		}
		string cacheDir = GetCacheDir(sn);
		try
		{
			if (Directory.Exists(cacheDir))
			{
				Directory.Delete(cacheDir, recursive: true);
			}
		}
		catch (Exception ex)
		{
			Logger.Warning("S6CalibrationManager.ClearCache failed for " + sn + ": " + ex.Message);
		}
	}

	public string GetCacheDir(string sn)
	{
		return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "VITURE\\SpaceWalker\\S6", sn ?? "_unknown");
	}

	public string GetDllInboxDir(string sn)
	{
		return Path.Combine(GetCacheDir(sn), "dll_inbox");
	}

	private bool EnsureCalibrationLoadedSync(string sn, string firmwareVersion, CancellationToken ct)
	{
		if (string.IsNullOrWhiteSpace(sn))
		{
			Logger.Error("S6CalibrationManager.EnsureCalibrationLoaded: sn is empty");
			return false;
		}
		string cacheDir = GetCacheDir(sn);
		Directory.CreateDirectory(cacheDir);
		S6Calibration s6Calibration = new S6Calibration
		{
			Sn = sn,
			FirmwareVersion = (firmwareVersion ?? string.Empty),
			ReadAtUtc = DateTime.UtcNow
		};
		if (TryReuseCache(sn, cacheDir, s6Calibration))
		{
			Logger.Info("S6CalibrationManager: cache hit for " + sn + ", skipping HID read.");
			FinalizeCalibration(s6Calibration);
			return true;
		}
		foreach (S6CalibSection value2 in Enum.GetValues(typeof(S6CalibSection)))
		{
			ct.ThrowIfCancellationRequested();
			if (s6Calibration.Buffers.ContainsKey(value2))
			{
				continue;
			}
			if (value2 == S6CalibSection.AccTempDrift || value2 == S6CalibSection.MagTempDrift)
			{
				Logger.Info($"S6CalibrationManager: skipping {value2} (schema TBD, not consumed by SDK)");
				continue;
			}
			ushort readMsgId = S6CalibSectionMap.GetReadMsgId(value2);
			Logger.Info($"S6CalibrationManager: reading {value2} (0x{readMsgId:X4})");
			R6NewerLongPacketReader instance = R6NewerLongPacketReader.Instance;
			byte[] array = instance.ReadLongPacket(readMsgId);
			if (array == null)
			{
				Logger.Warning(string.Format("S6CalibrationManager: read {0} (0x{1:X4}) failed — reason: {2}", value2, readMsgId, instance.LastFailReason ?? "unknown"));
				continue;
			}
			s6Calibration.Buffers[value2] = array;
			TryWriteSectionFile(cacheDir, value2, array, s6Calibration.FirmwareVersion);
		}
		if (s6Calibration.Buffers.TryGetValue(S6CalibSection.DisplayOptical, out byte[] value) && S6DisplayOpticalParam.TryParse(value, out S6DisplayOpticalParam parsed))
		{
			s6Calibration.DisplayOptical = parsed;
		}
		FinalizeCalibration(s6Calibration);
		bool flag = s6Calibration.IsEssentialComplete();
		bool flag2 = s6Calibration.IsComplete();
		Logger.Info($"S6CalibrationManager.EnsureCalibrationLoaded: sn={sn} essential={flag} all={flag2}, {s6Calibration}");
		return flag;
	}

	private bool TryReuseCache(string sn, string dir, S6Calibration calib)
	{
		bool flag = false;
		foreach (S6CalibSection value in Enum.GetValues(typeof(S6CalibSection)))
		{
			string sectionPath = GetSectionPath(dir, value);
			if (File.Exists(sectionPath) && TryReadSectionFile(sectionPath, out byte[] payload, out string firmware))
			{
				calib.Buffers[value] = payload;
				if (!string.IsNullOrEmpty(firmware) && string.IsNullOrEmpty(calib.FirmwareVersion))
				{
					calib.FirmwareVersion = firmware;
				}
				flag = true;
			}
		}
		if (!flag)
		{
			return false;
		}
		foreach (S6CalibSection value2 in Enum.GetValues(typeof(S6CalibSection)))
		{
			if (!calib.Buffers.ContainsKey(value2))
			{
				return false;
			}
		}
		return true;
	}

	private void FinalizeCalibration(S6Calibration calib)
	{
		lock (_gate)
		{
			_current = calib;
		}
	}

	private string GetSectionPath(string dir, S6CalibSection section)
	{
		return Path.Combine(dir, S6CalibSectionMap.GetSectionName(section) + ".bin");
	}

	private void TryWriteSectionFile(string dir, S6CalibSection section, byte[] payload, string firmwareVersion)
	{
		try
		{
			byte[] array = new byte[32];
			if (!string.IsNullOrEmpty(firmwareVersion))
			{
				byte[] bytes = Encoding.ASCII.GetBytes(firmwareVersion);
				Array.Copy(bytes, 0, array, 0, Math.Min(bytes.Length, array.Length));
			}
			using FileStream fileStream = File.Create(GetSectionPath(dir, section));
			fileStream.Write(FileMagic, 0, FileMagic.Length);
			byte[] array2 = new byte[2];
			BinaryPrimitives.WriteUInt16LittleEndian(array2, 1);
			fileStream.Write(array2, 0, array2.Length);
			fileStream.WriteByte((byte)section);
			byte[] array3 = new byte[4];
			BinaryPrimitives.WriteUInt32LittleEndian(array3, (uint)payload.Length);
			fileStream.Write(array3, 0, array3.Length);
			fileStream.Write(array, 0, array.Length);
			fileStream.Write(payload, 0, payload.Length);
		}
		catch (Exception ex)
		{
			Logger.Warning($"S6CalibrationManager.TryWriteSectionFile {section}: {ex.Message}");
		}
	}

	private bool TryReadSectionFile(string path, out byte[] payload, out string firmware)
	{
		payload = Array.Empty<byte>();
		firmware = string.Empty;
		try
		{
			byte[] array = File.ReadAllBytes(path);
			if (array.Length < FileMagic.Length + 2 + 1 + 4 + 32)
			{
				return false;
			}
			int num = 0;
			for (int i = 0; i < FileMagic.Length; i++)
			{
				if (array[num + i] != FileMagic[i])
				{
					return false;
				}
			}
			num += FileMagic.Length;
			ushort num2 = BinaryPrimitives.ReadUInt16LittleEndian(array.AsSpan(num, 2));
			num += 2;
			if (num2 != 1)
			{
				Logger.Warning($"S6CalibrationManager: file format version mismatch {num2} vs {(ushort)1} ({path})");
				return false;
			}
			_ = array[num];
			num++;
			uint num3 = BinaryPrimitives.ReadUInt32LittleEndian(array.AsSpan(num, 4));
			num += 4;
			int num4 = 32;
			int num5 = Array.IndexOf(array, (byte)0, num, num4);
			if (num5 < 0)
			{
				num5 = num + num4;
			}
			firmware = Encoding.ASCII.GetString(array, num, num5 - num);
			num += num4;
			if (num + num3 > array.Length)
			{
				return false;
			}
			payload = new byte[num3];
			Buffer.BlockCopy(array, num, payload, 0, (int)num3);
			return true;
		}
		catch (Exception ex)
		{
			Logger.Warning("S6CalibrationManager.TryReadSectionFile " + path + ": " + ex.Message);
			return false;
		}
	}

	private void TryDumpToInbox(string sn, S6CalibSection section, byte[] payload)
	{
		try
		{
			string dllInboxDir = GetDllInboxDir(sn);
			Directory.CreateDirectory(dllInboxDir);
			ushort readMsgId = S6CalibSectionMap.GetReadMsgId(section);
			string path = $"0x{readMsgId:X4}_{S6CalibSectionMap.GetSectionName(section)}.bin";
			File.WriteAllBytes(Path.Combine(dllInboxDir, path), payload);
		}
		catch (Exception ex)
		{
			Logger.Warning($"S6CalibrationManager.TryDumpToInbox {section}: {ex.Message}");
		}
	}
}
