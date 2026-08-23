using System;
using Windows.Win32;
using Windows.Win32.Devices.Display;

namespace VitureCommonLibrary;

public class DisplayDeviceInfo
{
	private readonly DisplayConfig _config;

	private const DISPLAYCONFIG_DEVICE_INFO_TYPE DpiScaleGetCode = (DISPLAYCONFIG_DEVICE_INFO_TYPE)(-3);

	private const DISPLAYCONFIG_DEVICE_INFO_TYPE DpiScaleSetCode = (DISPLAYCONFIG_DEVICE_INFO_TYPE)(-4);

	private static readonly uint[] DpiVals = new uint[12]
	{
		100u, 125u, 150u, 175u, 200u, 225u, 250u, 300u, 350u, 400u,
		450u, 500u
	};

	public bool IsConnected
	{
		get
		{
			if (!_config.TargetInfo.Available)
			{
				return false;
			}
			DISPLAYCONFIG_VIDEO_OUTPUT_TECHNOLOGY outputTechnology = _config.TargetInfo.OutputTechnology;
			if (((uint)outputTechnology > 3u && outputTechnology != DISPLAYCONFIG_VIDEO_OUTPUT_TECHNOLOGY.DISPLAYCONFIG_OUTPUT_TECHNOLOGY_D_JPN && outputTechnology != DISPLAYCONFIG_VIDEO_OUTPUT_TECHNOLOGY.DISPLAYCONFIG_OUTPUT_TECHNOLOGY_SDTVDONGLE) || 1 == 0)
			{
				return true;
			}
			if (_config.IsActive)
			{
				return true;
			}
			DISPLAYCONFIG_TARGET_DEVICE_NAME? targetDeviceName = GetTargetDeviceName();
			if (targetDeviceName.HasValue)
			{
				return targetDeviceName.Value.flags.Anonymous.Anonymous.edidIdsValid;
			}
			return false;
		}
	}

	public DisplayDeviceInfo(DisplayConfig config)
	{
		_config = config;
	}

	public DISPLAYCONFIG_TARGET_DEVICE_NAME? GetTargetDeviceName()
	{
		DISPLAYCONFIG_TARGET_DEVICE_NAME pkt = default(DISPLAYCONFIG_TARGET_DEVICE_NAME);
		if (GetDeviceInfo(ref pkt, _config.TargetInfo.AdapterId, _config.TargetInfo.Id) != 0)
		{
			return null;
		}
		return pkt;
	}

	public DISPLAYCONFIG_ADAPTER_NAME? GetAdapterName()
	{
		DISPLAYCONFIG_ADAPTER_NAME pkt = default(DISPLAYCONFIG_ADAPTER_NAME);
		if (GetDeviceInfo(ref pkt, _config.TargetInfo.AdapterId, 0u) != 0)
		{
			return null;
		}
		return pkt;
	}

	public DISPLAYCONFIG_TARGET_PREFERRED_MODE? GetTargetPreferredMode()
	{
		DISPLAYCONFIG_TARGET_PREFERRED_MODE pkt = default(DISPLAYCONFIG_TARGET_PREFERRED_MODE);
		if (GetDeviceInfo(ref pkt, _config.TargetInfo.AdapterId, _config.TargetInfo.Id) != 0)
		{
			return null;
		}
		return pkt;
	}

	public DISPLAYCONFIG_SDR_WHITE_LEVEL? GetSdrWhiteLevel()
	{
		DISPLAYCONFIG_SDR_WHITE_LEVEL pkt = default(DISPLAYCONFIG_SDR_WHITE_LEVEL);
		if (GetDeviceInfo(ref pkt, _config.TargetInfo.AdapterId, _config.TargetInfo.Id) != 0)
		{
			return null;
		}
		return pkt;
	}

	public DISPLAYCONFIG_SUPPORT_VIRTUAL_RESOLUTION? GetSupportVirtualResolution()
	{
		DISPLAYCONFIG_SUPPORT_VIRTUAL_RESOLUTION pkt = default(DISPLAYCONFIG_SUPPORT_VIRTUAL_RESOLUTION);
		if (GetDeviceInfo(ref pkt, _config.TargetInfo.AdapterId, _config.TargetInfo.Id) != 0)
		{
			return null;
		}
		return pkt;
	}

	public DISPLAYCONFIG_TARGET_BASE_TYPE? GetTargetBaseType()
	{
		DISPLAYCONFIG_TARGET_BASE_TYPE pkt = default(DISPLAYCONFIG_TARGET_BASE_TYPE);
		if (GetDeviceInfo(ref pkt, _config.TargetInfo.AdapterId, _config.TargetInfo.Id) != 0)
		{
			return null;
		}
		return pkt;
	}

	public DISPLAYCONFIG_SOURCE_DEVICE_NAME? GetSourceDeviceName()
	{
		DISPLAYCONFIG_SOURCE_DEVICE_NAME pkt = default(DISPLAYCONFIG_SOURCE_DEVICE_NAME);
		if (GetDeviceInfo(ref pkt, _config.SourceInfo.AdapterId, _config.SourceInfo.Id) != 0)
		{
			return null;
		}
		return pkt;
	}

	public DISPLAYCONFIG_GET_ADVANCED_COLOR_INFO? GetAdvancedColorInfo()
	{
		DISPLAYCONFIG_GET_ADVANCED_COLOR_INFO pkt = default(DISPLAYCONFIG_GET_ADVANCED_COLOR_INFO);
		if (GetDeviceInfo(ref pkt, _config.TargetInfo.AdapterId, _config.TargetInfo.Id) != 0)
		{
			return null;
		}
		return pkt;
	}

	public DISPLAYCONFIG_GET_MONITOR_SPECIALIZATION? GetMonitorSpecialization()
	{
		DISPLAYCONFIG_GET_MONITOR_SPECIALIZATION pkt = default(DISPLAYCONFIG_GET_MONITOR_SPECIALIZATION);
		if (GetDeviceInfo(ref pkt, _config.TargetInfo.AdapterId, _config.TargetInfo.Id) != 0)
		{
			return null;
		}
		return pkt;
	}

	public DISPLAYCONFIG_SOURCE_DPI_SCALE_GET? GetSourceDpiScale()
	{
		DISPLAYCONFIG_SOURCE_DPI_SCALE_GET pkt = default(DISPLAYCONFIG_SOURCE_DPI_SCALE_GET);
		if (GetDeviceInfo(ref pkt, _config.SourceInfo.AdapterId, _config.SourceInfo.Id) != 0)
		{
			return null;
		}
		return pkt;
	}

	public int SetTargetPersistence(bool bootPersistenceOn)
	{
		DISPLAYCONFIG_SET_TARGET_PERSISTENCE pkt = default(DISPLAYCONFIG_SET_TARGET_PERSISTENCE);
		pkt.Anonymous.value = (bootPersistenceOn ? 1u : 0u);
		return SetDeviceInfo(ref pkt, _config.TargetInfo.AdapterId, _config.TargetInfo.Id);
	}

	public int SetSupportVirtualResolution(bool disabled)
	{
		DISPLAYCONFIG_SUPPORT_VIRTUAL_RESOLUTION pkt = default(DISPLAYCONFIG_SUPPORT_VIRTUAL_RESOLUTION);
		pkt.Anonymous.value = (disabled ? 1u : 0u);
		return SetDeviceInfo(ref pkt, _config.TargetInfo.AdapterId, _config.TargetInfo.Id);
	}

	public int SetAdvancedColorState(bool enableAdvancedColor)
	{
		DISPLAYCONFIG_SET_ADVANCED_COLOR_STATE pkt = default(DISPLAYCONFIG_SET_ADVANCED_COLOR_STATE);
		pkt.Anonymous.value = (enableAdvancedColor ? 1u : 0u);
		return SetDeviceInfo(ref pkt, _config.TargetInfo.AdapterId, _config.TargetInfo.Id);
	}

	public int SetMonitorSpecialization(DISPLAYCONFIG_SET_MONITOR_SPECIALIZATION pkt)
	{
		return SetDeviceInfo(ref pkt, _config.TargetInfo.AdapterId, _config.TargetInfo.Id);
	}

	public int SetMonitorSpecialization(bool isSpecializationEnabled, Guid specializationType, Guid specializationSubType, string specializationApplicationName)
	{
		DISPLAYCONFIG_SET_MONITOR_SPECIALIZATION monitorSpecialization = default(DISPLAYCONFIG_SET_MONITOR_SPECIALIZATION);
		monitorSpecialization.Anonymous.value = (isSpecializationEnabled ? 1u : 0u);
		monitorSpecialization.specializationType = specializationType;
		monitorSpecialization.specializationSubType = specializationSubType;
		if (!string.IsNullOrEmpty(specializationApplicationName))
		{
			monitorSpecialization.specializationApplicationName = specializationApplicationName;
		}
		return SetMonitorSpecialization(monitorSpecialization);
	}

	public int SetSourceDpiScale(int scaleRel)
	{
		DISPLAYCONFIG_SOURCE_DPI_SCALE_SET pkt = default(DISPLAYCONFIG_SOURCE_DPI_SCALE_SET);
		pkt.scaleRel = scaleRel;
		return SetDeviceInfo(ref pkt, _config.SourceInfo.AdapterId, _config.SourceInfo.Id);
	}

	public uint? GetSourceDpiScaleValue()
	{
		DISPLAYCONFIG_SOURCE_DPI_SCALE_GET? sourceDpiScale = GetSourceDpiScale();
		if (!sourceDpiScale.HasValue)
		{
			return null;
		}
		int num = Math.Abs(sourceDpiScale.Value.minScaleRel) + sourceDpiScale.Value.curScaleRel;
		if ((uint)num >= (uint)DpiVals.Length)
		{
			return null;
		}
		return DpiVals[num];
	}

	public int SetSourceDpiScaleValue(uint dpiPercent)
	{
		DISPLAYCONFIG_SOURCE_DPI_SCALE_GET? sourceDpiScale = GetSourceDpiScale();
		if (!sourceDpiScale.HasValue)
		{
			return -2147024809;
		}
		int num = Math.Abs(sourceDpiScale.Value.minScaleRel);
		if (num >= DpiVals.Length)
		{
			return -2147024809;
		}
		uint val = DpiVals[num];
		int num2 = Math.Min(num + sourceDpiScale.Value.maxScaleRel, DpiVals.Length - 1);
		uint val2 = DpiVals[num2];
		dpiPercent = Math.Max(val, Math.Min(val2, dpiPercent));
		int num3 = Array.IndexOf(DpiVals, dpiPercent);
		if (num3 == -1)
		{
			return -2147024809;
		}
		return SetSourceDpiScale(num3 - num);
	}

	private unsafe static int GetDeviceInfo<T>(ref T pkt, ulong adapterId, uint id) where T : unmanaged
	{
		DISPLAYCONFIG_DEVICE_INFO_TYPE type;
		if (typeof(T) == typeof(DISPLAYCONFIG_TARGET_DEVICE_NAME))
		{
			type = DISPLAYCONFIG_DEVICE_INFO_TYPE.DISPLAYCONFIG_DEVICE_INFO_GET_TARGET_NAME;
		}
		else if (typeof(T) == typeof(DISPLAYCONFIG_SOURCE_DEVICE_NAME))
		{
			type = DISPLAYCONFIG_DEVICE_INFO_TYPE.DISPLAYCONFIG_DEVICE_INFO_GET_SOURCE_NAME;
		}
		else if (typeof(T) == typeof(DISPLAYCONFIG_ADAPTER_NAME))
		{
			type = DISPLAYCONFIG_DEVICE_INFO_TYPE.DISPLAYCONFIG_DEVICE_INFO_GET_ADAPTER_NAME;
		}
		else if (typeof(T) == typeof(DISPLAYCONFIG_TARGET_PREFERRED_MODE))
		{
			type = DISPLAYCONFIG_DEVICE_INFO_TYPE.DISPLAYCONFIG_DEVICE_INFO_GET_TARGET_PREFERRED_MODE;
		}
		else if (typeof(T) == typeof(DISPLAYCONFIG_SDR_WHITE_LEVEL))
		{
			type = DISPLAYCONFIG_DEVICE_INFO_TYPE.DISPLAYCONFIG_DEVICE_INFO_GET_SDR_WHITE_LEVEL;
		}
		else if (typeof(T) == typeof(DISPLAYCONFIG_SUPPORT_VIRTUAL_RESOLUTION))
		{
			type = DISPLAYCONFIG_DEVICE_INFO_TYPE.DISPLAYCONFIG_DEVICE_INFO_GET_SUPPORT_VIRTUAL_RESOLUTION;
		}
		else if (typeof(T) == typeof(DISPLAYCONFIG_TARGET_BASE_TYPE))
		{
			type = DISPLAYCONFIG_DEVICE_INFO_TYPE.DISPLAYCONFIG_DEVICE_INFO_GET_TARGET_BASE_TYPE;
		}
		else if (typeof(T) == typeof(DISPLAYCONFIG_GET_ADVANCED_COLOR_INFO))
		{
			type = DISPLAYCONFIG_DEVICE_INFO_TYPE.DISPLAYCONFIG_DEVICE_INFO_GET_ADVANCED_COLOR_INFO;
		}
		else if (typeof(T) == typeof(DISPLAYCONFIG_GET_MONITOR_SPECIALIZATION))
		{
			type = DISPLAYCONFIG_DEVICE_INFO_TYPE.DISPLAYCONFIG_DEVICE_INFO_GET_MONITOR_SPECIALIZATION;
		}
		else
		{
			if (!(typeof(T) == typeof(DISPLAYCONFIG_SOURCE_DPI_SCALE_GET)))
			{
				throw new NotSupportedException("GetDeviceInfo<" + typeof(T).Name + "> not supported");
			}
			type = (DISPLAYCONFIG_DEVICE_INFO_TYPE)(-3);
		}
		fixed (T* ptr = &pkt)
		{
			DISPLAYCONFIG_DEVICE_INFO_HEADER* ptr2 = (DISPLAYCONFIG_DEVICE_INFO_HEADER*)ptr;
			ptr2->type = type;
			ptr2->size = (uint)sizeof(T);
			ptr2->adapterId = adapterId.ToLuid();
			ptr2->id = id;
			return PInvoke.DisplayConfigGetDeviceInfo(ptr2);
		}
	}

	private unsafe static int SetDeviceInfo<T>(ref T pkt, ulong adapterId, uint id) where T : unmanaged
	{
		DISPLAYCONFIG_DEVICE_INFO_TYPE type;
		if (typeof(T) == typeof(DISPLAYCONFIG_SUPPORT_VIRTUAL_RESOLUTION))
		{
			type = DISPLAYCONFIG_DEVICE_INFO_TYPE.DISPLAYCONFIG_DEVICE_INFO_SET_SUPPORT_VIRTUAL_RESOLUTION;
		}
		else if (typeof(T) == typeof(DISPLAYCONFIG_SET_TARGET_PERSISTENCE))
		{
			type = DISPLAYCONFIG_DEVICE_INFO_TYPE.DISPLAYCONFIG_DEVICE_INFO_SET_TARGET_PERSISTENCE;
		}
		else if (typeof(T) == typeof(DISPLAYCONFIG_SET_ADVANCED_COLOR_STATE))
		{
			type = DISPLAYCONFIG_DEVICE_INFO_TYPE.DISPLAYCONFIG_DEVICE_INFO_SET_ADVANCED_COLOR_STATE;
		}
		else if (typeof(T) == typeof(DISPLAYCONFIG_SET_MONITOR_SPECIALIZATION))
		{
			type = DISPLAYCONFIG_DEVICE_INFO_TYPE.DISPLAYCONFIG_DEVICE_INFO_SET_MONITOR_SPECIALIZATION;
		}
		else
		{
			if (!(typeof(T) == typeof(DISPLAYCONFIG_SOURCE_DPI_SCALE_SET)))
			{
				throw new NotSupportedException("SetDeviceInfo<" + typeof(T).Name + "> not supported");
			}
			type = (DISPLAYCONFIG_DEVICE_INFO_TYPE)(-4);
		}
		fixed (T* ptr = &pkt)
		{
			DISPLAYCONFIG_DEVICE_INFO_HEADER* ptr2 = (DISPLAYCONFIG_DEVICE_INFO_HEADER*)ptr;
			ptr2->type = type;
			ptr2->size = (uint)sizeof(T);
			ptr2->adapterId = adapterId.ToLuid();
			ptr2->id = id;
			return PInvoke.DisplayConfigSetDeviceInfo(ptr2);
		}
	}
}
