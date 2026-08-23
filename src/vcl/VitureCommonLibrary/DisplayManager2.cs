using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Windows.Win32.Devices.Display;

namespace VitureCommonLibrary;

public class DisplayManager2
{
	public const string VitureDisplayName = "VITURE";

	public const uint VitureUltraWideWidth = 3840u;

	public const uint VitureStandardWidth = 1920u;

	private readonly DisplayChangeMonitor _changeMonitor = new DisplayChangeMonitor();

	public const int HDPI_SCALE = 125;

	public const int VD_WIDTH = 1920;

	public const int VD_HEIGHT = 1080;

	public const int VD_HEIGHT_1200P = 1200;

	public const int NATIVE_3DOF_VD_WIDTH = 950;

	public const int VD_HDPI_WIDTH = 2560;

	public const int VD_HDPI_HEIGHT = 1440;

	public const int VD_HDPI_HEIGHT_1200P = 1600;

	public const int NATIVE_3DOF_VD_HDPI_WIDTH = 1260;

	public const int WD_WIDTH = 2880;

	public const int WD_HEIGHT = 1080;

	public const int WD_HEIGHT_1200P = 1200;

	public const int WD_HDPI_WIDTH = 3840;

	public const int WD_HDPI_HEIGHT = 1440;

	public const int WD_HDPI_HEIGHT_1200P = 1600;

	public const int ULTRA_WD_WIDTH = 3840;

	public const int ULTRA_WD_HEIGHT = 1080;

	public const int ULTRA_WD_HEIGHT_1200P = 1200;

	public const int ULTRA_HDPI_WD_WIDTH = 5120;

	public const int ULTRA_HDPI_WD_HEIGHT = 1440;

	public const int ULTRA_HDPI_WD_HEIGHT_1200P = 1600;

	private LayoutMode layoutMode;

	public Action<int>? PhysicalMonitorChanged;

	public Action<bool, bool>? VitureDisplayConnectChanged;

	public Action<bool>? WideDisplayConnectChanged;

	public static DisplayManager2 Instance { get; }

	public bool EnableHighDpiScale { get; set; }

	public (uint Width, uint Height) UltraWide { get; set; } = (Width: 3840u, Height: 1080u);


	public (uint Width, uint Height) Standard { get; set; } = (Width: 1920u, Height: 1080u);


	public (uint Width, uint Height) SidePanel { get; set; } = (Width: 950u, Height: 1080u);


	public LayoutMode LayoutMode
	{
		get
		{
			return layoutMode;
		}
		set
		{
			if (layoutMode != value)
			{
				layoutMode = value;
			}
		}
	}

	public bool TurnOffBuildInScreen { get; set; }

	public bool UseUltraWideSize { get; set; } = true;


	public bool VitureHasConnected { get; set; }

	public bool SingleExtend { get; set; }

	public bool VitureDisplayConnected => GlassesMonitorHelper._vitureDisplayConnected;

	public bool VitureDisplayActive => GlassesMonitorHelper._vitureDisplayActive;

	public bool WideDisplayConnected => GlassesMonitorHelper._wideDisplayConnected;

	private bool Support1200P => GlassesDeviceManager.Instance.Support1200P;

	private bool SupportNative3Dof => GlassesDeviceManager.Instance.SupportNative3Dof;

	public Size VitureSize
	{
		get
		{
			string? name = Assembly.GetEntryAssembly().GetName().Name;
			bool support1200P = Support1200P;
			if (name == "SpaceWalker")
			{
				if (SupportNative3Dof)
				{
					if (support1200P)
					{
						if (layoutMode == LayoutMode.HorizonExtend1 || layoutMode == LayoutMode.HorizonMirror1)
						{
							return new Size(1920, 1200);
						}
						return new Size(3840, 1200);
					}
					if (layoutMode == LayoutMode.HorizonExtend1 || layoutMode == LayoutMode.HorizonMirror1)
					{
						return new Size(1920, 1080);
					}
					return new Size(3840, 1080);
				}
				if (support1200P)
				{
					return new Size(1920, 1200);
				}
				return new Size(1920, 1080);
			}
			if (support1200P)
			{
				return new Size(3840, 1200);
			}
			return new Size(3840, 1080);
		}
	}

	public Size VdSize
	{
		get
		{
			if (Support1200P)
			{
				if (!EnableHighDpiScale)
				{
					return new Size(1920, 1200);
				}
				return new Size(2560, 1600);
			}
			if (!EnableHighDpiScale)
			{
				return new Size(1920, 1080);
			}
			return new Size(2560, 1440);
		}
	}

	public Size Native3DofVdSize
	{
		get
		{
			if (Support1200P)
			{
				if (!EnableHighDpiScale)
				{
					return new Size(950, 1200);
				}
				return new Size(1260, 1600);
			}
			if (!EnableHighDpiScale)
			{
				return new Size(950, 1080);
			}
			return new Size(1260, 1440);
		}
	}

	public Size WdSize
	{
		get
		{
			if (Support1200P)
			{
				if (!EnableHighDpiScale)
				{
					if (!UseUltraWideSize)
					{
						return new Size(2880, 1200);
					}
					return new Size(3840, 1200);
				}
				if (!UseUltraWideSize)
				{
					return new Size(3840, 1600);
				}
				return new Size(5120, 1600);
			}
			if (!EnableHighDpiScale)
			{
				if (!UseUltraWideSize)
				{
					return new Size(2880, 1080);
				}
				return new Size(3840, 1080);
			}
			if (!UseUltraWideSize)
			{
				return new Size(3840, 1440);
			}
			return new Size(5120, 1440);
		}
	}

	public DisplayInfo? VitureDisplay => GetVitureDisplay();

	public DisplayInfo? WideDisplay => GetWideDisplay();

	public DisplayInfo[] VddDisplays => GetVirtuals(onlyActive: true).ToDisplayInfos();

	public DisplayInfo[] CurrentViewDisplays => GetCurrentViewDisplays();

	public DisplayInfo? PrimaryDisplay => GetPrimary()?.ToDisplayInfo();

	public DisplayInfo[] AllDisplays => GetAll(onlyActive: true).ToDisplayInfos();

	public DisplayInfo[] PhysicalDisplays
	{
		get
		{
			if (!TurnOffBuildInScreen)
			{
				return GetPhysicalDisplays();
			}
			return Array.Empty<DisplayInfo>();
		}
	}

	public DisplayInfo[] ExtDisplays
	{
		get
		{
			if (!TurnOffBuildInScreen)
			{
				return GetPhysicalDisplays().Skip(1).ToArray();
			}
			return Array.Empty<DisplayInfo>();
		}
	}

	public DisplayInfo? PhyVitureDisplay => (from x in GetVitures(onlyActive: true)
		select x.ToDisplayInfo()).FirstOrDefault();

	public event Action DisplayChanged = delegate
	{
	};

	private DisplayManager2()
	{
	}

	public Task InitAsync(CancellationToken cancellation = default(CancellationToken))
	{
		DisplayChangeMonitor changeMonitor = _changeMonitor;
		changeMonitor.OnDisplayChanged = (Action)Delegate.Combine(changeMonitor.OnDisplayChanged, new Action(DisplayManager2_DisplayChanged));
		_changeMonitor.Start();
		return Task.CompletedTask;
	}

	private void DisplayManager2_DisplayChanged()
	{
		this.DisplayChanged?.Invoke();
	}

	public async Task ShutdownAsync(CancellationToken cancellation = default(CancellationToken))
	{
		try
		{
			DisplayChangeMonitor changeMonitor = _changeMonitor;
			changeMonitor.OnDisplayChanged = (Action)Delegate.Remove(changeMonitor.OnDisplayChanged, new Action(DisplayManager2_DisplayChanged));
			_changeMonitor.Dispose();
		}
		catch
		{
		}
		await RestoreDesktopAsync(cancellation);
	}

	public async Task RestoreDesktopAsync(CancellationToken cancellation = default(CancellationToken))
	{
		_ = 2;
		try
		{
			await Task.Run(delegate
			{
				DisplayConfigs.SetAllExtended();
			}, cancellation);
			DisplayConfigs dcs = await DisplayConfigs.QueryDisplayConfigAsync(QUERY_DISPLAY_CONFIG_FLAGS.QDC_ONLY_ACTIVE_PATHS, cancellation);
			DisplayConfig[] array = dcs.ToArray();
			DisplayConfig displayConfig = FilterAndOrder(array, IsInternal).FirstOrDefault();
			if (displayConfig != null && !displayConfig.IsPrimary)
			{
				Primary(array, displayConfig.GetDevicePath());
				SET_DISPLAY_CONFIG_FLAGS flags = SET_DISPLAY_CONFIG_FLAGS.SDC_USE_SUPPLIED_DISPLAY_CONFIG | SET_DISPLAY_CONFIG_FLAGS.SDC_APPLY | SET_DISPLAY_CONFIG_FLAGS.SDC_ALLOW_CHANGES;
				await Task.Run(delegate
				{
					dcs.SetDisplayConfig(flags);
				}, cancellation);
			}
		}
		catch
		{
		}
	}

	public DisplayConfig? GetPrimary()
	{
		return DisplayConfigs.QueryDisplayConfig(QUERY_DISPLAY_CONFIG_FLAGS.QDC_ONLY_ACTIVE_PATHS).ToArray().FirstOrDefault((DisplayConfig x) => x.IsPrimary);
	}

	public IEnumerable<DisplayConfig> GetVitures(bool onlyActive = false)
	{
		return FilterAndOrder(DisplayConfigs.QueryDisplayConfig((!onlyActive) ? QUERY_DISPLAY_CONFIG_FLAGS.QDC_ALL_PATHS : QUERY_DISPLAY_CONFIG_FLAGS.QDC_ONLY_ACTIVE_PATHS).Distinct().ToArray(), IsViture);
	}

	public IEnumerable<DisplayConfig> GetInternals(bool onlyActive = false)
	{
		return FilterAndOrder(DisplayConfigs.QueryDisplayConfig((!onlyActive) ? QUERY_DISPLAY_CONFIG_FLAGS.QDC_ALL_PATHS : QUERY_DISPLAY_CONFIG_FLAGS.QDC_ONLY_ACTIVE_PATHS).Distinct().ToArray(), IsInternal);
	}

	public IEnumerable<DisplayConfig> GetVirtuals(bool onlyActive = false)
	{
		return FilterAndOrder(DisplayConfigs.QueryDisplayConfig((!onlyActive) ? QUERY_DISPLAY_CONFIG_FLAGS.QDC_ALL_PATHS : QUERY_DISPLAY_CONFIG_FLAGS.QDC_ONLY_ACTIVE_PATHS).Distinct().ToArray(), IsVirtual);
	}

	public IEnumerable<DisplayConfig> GetAll(bool onlyActive = false)
	{
		return DisplayConfigs.QueryDisplayConfig((!onlyActive) ? QUERY_DISPLAY_CONFIG_FLAGS.QDC_ALL_PATHS : QUERY_DISPLAY_CONFIG_FLAGS.QDC_ONLY_ACTIVE_PATHS).Distinct().ToArray();
	}

	private static IEnumerable<DisplayConfig> FilterAndOrder(IEnumerable<DisplayConfig> displays, Func<DisplayConfig, bool> kind)
	{
		Func<DisplayConfig, bool> kind2 = kind;
		return displays.Where((DisplayConfig x) => x.DeviceInfo.IsConnected && kind2(x)).OrderByDescending(delegate(DisplayConfig x)
		{
			if (!x.IsActive)
			{
				return 0;
			}
			return (!x.IsPrimary) ? 1 : 2;
		}).ThenBy<DisplayConfig, string>((DisplayConfig x) => x.DeviceInfo.GetSourceDeviceName()?.viewGdiDeviceName.ToString(), GdiDeviceNameComparer.Instance);
	}

	public void SetRefreshRate(int refreshRate, Func<DisplayConfig, bool>? filter = null)
	{
		try
		{
			DisplayConfigs displayConfigs = DisplayConfigs.QueryDisplayConfig(QUERY_DISPLAY_CONFIG_FLAGS.QDC_ONLY_ACTIVE_PATHS);
			IEnumerable<DisplayConfig> enumerable = displayConfigs.ToArray().Where(filter ?? new Func<DisplayConfig, bool>(IsViture));
			bool flag = false;
			foreach (DisplayConfig item in enumerable)
			{
				DisplayTargetMode targetMode = item.GetTargetMode();
				if (targetMode != null && Math.Abs(targetMode.VSyncFreq.ToDouble() - (double)refreshRate) > 0.01)
				{
					targetMode.VSyncFreq = DisplayConfigsExtensions.ToDisplayConfigRational(refreshRate);
					flag = true;
				}
			}
			Logger.Info(string.Format("[RefreshRate] set {0}Hz filter={1} isNeedApply={2}", refreshRate, (filter == null) ? "<viture>" : "<custom>", flag));
			if (flag)
			{
				displayConfigs.SetDisplayConfig(SET_DISPLAY_CONFIG_FLAGS.SDC_USE_SUPPLIED_DISPLAY_CONFIG | SET_DISPLAY_CONFIG_FLAGS.SDC_APPLY | SET_DISPLAY_CONFIG_FLAGS.SDC_ALLOW_CHANGES);
			}
		}
		catch (Exception ex)
		{
			Logger.Error($"[RefreshRate] set {refreshRate}Hz failed: {ex.Message}", ex.StackTrace);
		}
	}

	public int? GetVitureRefreshRate()
	{
		try
		{
			foreach (DisplayConfig item in DisplayConfigs.QueryDisplayConfig(QUERY_DISPLAY_CONFIG_FLAGS.QDC_ONLY_ACTIVE_PATHS).ToArray().Where(IsViture))
			{
				DisplayTargetMode targetMode = item.GetTargetMode();
				if (targetMode != null)
				{
					return (int)Math.Round(targetMode.VSyncFreq.ToDouble());
				}
			}
		}
		catch (Exception ex)
		{
			Logger.Error("[RefreshRate] query failed: " + ex.Message, ex.StackTrace);
		}
		return null;
	}

	public async Task<Rectangle> SetLayoutAsync(VitureLayoutMode mode, VitureLayoutType type, int refreshRate, string? viturePath = null, CancellationToken cancellation = default(CancellationToken))
	{
		string viturePath2 = viturePath;
		Logger.Info(string.Format("[Layout] begin mode={0} type={1} fps={2} viturePath={3} UltraWide={4} Standard={5} SidePanel={6} hidpi={7}", mode, type, refreshRate, string.IsNullOrEmpty(viturePath2) ? "<auto>" : viturePath2, UltraWide, Standard, SidePanel, EnableHighDpiScale));
		DisplayConfig[] array = (await DisplayConfigs.QueryDisplayConfigAsync(QUERY_DISPLAY_CONFIG_FLAGS.QDC_ALL_PATHS, cancellation)).ToArray();
		DisplayConfig displayConfig = (string.IsNullOrEmpty(viturePath2) ? FilterAndOrder(array, IsViture).FirstOrDefault((DisplayConfig x) => !string.IsNullOrEmpty(x.GetDevicePath())) : FilterAndOrder(array, (DisplayConfig _) => true).FirstOrDefault((DisplayConfig x) => x.GetDevicePath() == viturePath2));
		if (displayConfig == null || string.IsNullOrEmpty(displayConfig.GetDevicePath()))
		{
			Logger.Warning("[Layout] VITURE display not found (viturePath=" + viturePath2 + ")");
			throw new ArgumentException((string.IsNullOrEmpty(viturePath2) ? "VITURE" : viturePath2) + " not found.");
		}
		viturePath2 = displayConfig.GetDevicePath();
		string primaryPath2 = array.First((DisplayConfig x) => x.IsPrimary).GetDevicePath();
		Logger.Info("[Layout] viturePath=" + viturePath2 + " primaryPath=" + primaryPath2);
		List<string> vddPaths = (await BuildVddsAsync(mode, type, 60, cancellation)).ToList();
		Logger.Info(string.Format("[Layout] BuildVdds done: count={0} [{1}]", vddPaths.Count, string.Join(" | ", vddPaths)));
		primaryPath2 = await ConfigureDisplaysAsync(mode, type, refreshRate, primaryPath2, viturePath2, vddPaths, cancellation);
		Logger.Info("[Layout] ConfigureDisplays done: newPrimary=" + primaryPath2);
		Rectangle rectangle = await PlaceDisplaysAsync(mode, type, primaryPath2, viturePath2, vddPaths, cancellation);
		Logger.Info($"[Layout] PlaceDisplays done: vitureRect={rectangle}");
		return rectangle;
	}

	public Task ChangeNotifyAsync(CancellationToken cancellation = default(CancellationToken))
	{
		return Task.Run((Action)DisplayConfigs.ChangeNotify, cancellation);
	}

	public Task ResetLayoutAsync(bool all = false, CancellationToken cancellation = default(CancellationToken))
	{
		return Task.Run(delegate
		{
			DisplayConfigs.ResetDisplayConfig(all);
		}, cancellation);
	}

	private async Task<IEnumerable<string>> BuildVddsAsync(VitureLayoutMode mode, VitureLayoutType type, int refreshRate, CancellationToken cancellation = default(CancellationToken))
	{
		List<(uint, uint, int, DISPLAYCONFIG_ROTATION)> list = new List<(uint, uint, int, DISPLAYCONFIG_ROTATION)>();
		DISPLAYCONFIG_ROTATION item = DISPLAYCONFIG_ROTATION.DISPLAYCONFIG_ROTATION_IDENTITY;
		DISPLAYCONFIG_ROTATION item2 = DISPLAYCONFIG_ROTATION.DISPLAYCONFIG_ROTATION_ROTATE90;
		switch (mode)
		{
		case VitureLayoutMode.Horizontal1:
			if (type != 0)
			{
				list.Add((Standard.Width, Standard.Height, refreshRate, item));
			}
			break;
		case VitureLayoutMode.Horizontal2:
			list.Add((Standard.Width, Standard.Height, refreshRate, item));
			if (type != 0)
			{
				list.Add((Standard.Width, Standard.Height, refreshRate, item));
			}
			break;
		case VitureLayoutMode.Horizontal3:
			list.Add((Standard.Width, Standard.Height, refreshRate, item));
			list.Add((Standard.Width, Standard.Height, refreshRate, item));
			if (type != 0)
			{
				list.Add((Standard.Width, Standard.Height, refreshRate, item));
			}
			break;
		case VitureLayoutMode.Vertical3:
			list.Add((Standard.Width, Standard.Height, refreshRate, item));
			list.Add((Standard.Width, Standard.Height, refreshRate, item));
			if (type != 0)
			{
				list.Add((Standard.Width, Standard.Height, refreshRate, item));
			}
			break;
		case VitureLayoutMode.UltraWide:
			list.Add((UltraWide.Width, UltraWide.Height, refreshRate, item));
			break;
		case VitureLayoutMode.HorizontalPortrait:
			list.Add((Standard.Width, Standard.Height, refreshRate, item2));
			if (type != 0)
			{
				list.Add((Standard.Width, Standard.Height, refreshRate, item));
			}
			list.Add((Standard.Width, Standard.Height, refreshRate, item2));
			break;
		case VitureLayoutMode.Horizontal3A:
			list.Add((SidePanel.Width, SidePanel.Height, refreshRate, item));
			if (type != 0)
			{
				list.Add((Standard.Width, Standard.Height, refreshRate, item));
			}
			list.Add((SidePanel.Width, SidePanel.Height, refreshRate, item));
			break;
		case VitureLayoutMode.Horizontal2A:
			list.Add((Standard.Width, Standard.Height, refreshRate, item));
			if (type != 0)
			{
				list.Add((Standard.Width, Standard.Height, refreshRate, item));
			}
			break;
		}
		return await SyncVddsAsync(list, cancellation);
	}

	private async Task<IEnumerable<string>> SyncVddsAsync(IEnumerable<(uint Width, uint Height, int RefreshRate, DISPLAYCONFIG_ROTATION Rotation)> vdds, CancellationToken cancellation = default(CancellationToken))
	{
		SudoVirtualDisplay display = SudoVirtualDisplay.Instance;
		List<(uint Width, uint Height, int RefreshRate, DISPLAYCONFIG_ROTATION Rotation)> vddList = vdds.ToList();
		if (vddList.Count == 0)
		{
			display.RemoveVirtualDisplay(((int Index, int Count, DisplayTargetToken Token, uint Width, uint Height, uint RefreshRate, Guid MonitorGuid, string DeviceName, string SerialNumber) x) => x.Rest.Item1 == "VIT-VDD");
			return Array.Empty<string>();
		}
		Dictionary<DisplayTargetToken, DisplayConfig> paths = await QueryTokenMap();
		List<(uint Width, uint Height, int RefreshRate)> missing = vddList.Select(((uint Width, uint Height, int RefreshRate, DISPLAYCONFIG_ROTATION Rotation) x) => (Width: x.Width, Height: x.Height, RefreshRate: x.RefreshRate)).ToList();
		Dictionary<(uint, uint, int), Queue<DisplayTargetToken>> tokenPool = new Dictionary<(uint, uint, int), Queue<DisplayTargetToken>>();
		display.RemoveVirtualDisplay(delegate((int Index, int Count, DisplayTargetToken Token, uint Width, uint Height, uint RefreshRate, Guid MonitorGuid, string DeviceName, string SerialNumber) x)
		{
			if (x.Rest.Item1 != "VIT-VDD")
			{
				return false;
			}
			(uint, uint, int) tuple2 = (x.Width, x.Height, (int)x.RefreshRate);
			if (paths.ContainsKey(x.Token) && missing.Remove(tuple2))
			{
				Pool(tuple2, x.Token);
				return false;
			}
			return true;
		});
		int count = (await QueryTokenMap()).Count;
		if (count >= 2)
		{
			try
			{
				DisplayConfigs.SetDisplayConfig(null, null, SET_DISPLAY_CONFIG_FLAGS.SDC_TOPOLOGY_EXTEND | SET_DISPLAY_CONFIG_FLAGS.SDC_APPLY);
			}
			catch (Win32Exception ex)
			{
				Logger.Warning($"[Layout] 预设 TopologyExtend 失败 win32={ex.NativeErrorCode} (activeTargets={count})，" + "跳过——后续 SetAllExtended 会用真实 path 重建扩展");
			}
		}
		else
		{
			Logger.Info($"[Layout] 跳过预设 TopologyExtend：活跃目标仅 {count} 个(无头/单屏)，" + "无镜像风险且单目标无法套用扩展预设(避免 win32=31 中止建屏)");
		}
		List<DisplayTargetToken> second = (await display.AddVirtualDisplaysAsync(missing, cancellation)).ToList();
		foreach (var (spec2, token2) in missing.Zip(second, ((uint Width, uint Height, int RefreshRate) spec, DisplayTargetToken tok) => (spec: spec, tok: tok)))
		{
			Pool(spec2, token2);
		}
		paths = await QueryTokenMap();
		DisplayConfigs dcs = null;
		bool flag = false;
		List<string> result = new List<string>();
		foreach (var item in vddList)
		{
			(uint, uint, int) key = (item.Width, item.Height, item.RefreshRate);
			if (!tokenPool.TryGetValue(key, out Queue<DisplayTargetToken> value) || value.Count == 0)
			{
				continue;
			}
			DisplayTargetToken key2 = value.Dequeue();
			if (paths.TryGetValue(key2, out DisplayConfig value2))
			{
				result.Add(value2.GetDevicePath());
				value2.DeviceInfo.SetSourceDpiScaleValue(EnableHighDpiScale ? 125u : 100u);
				if (value2.TargetInfo.Rotation != item.Rotation)
				{
					value2.TargetInfo.Rotation = item.Rotation;
					dcs = value2.Owner;
					flag = true;
				}
			}
		}
		if (flag && dcs != null)
		{
			await Task.Run(delegate
			{
				dcs.SetDisplayConfig(SET_DISPLAY_CONFIG_FLAGS.SDC_USE_SUPPLIED_DISPLAY_CONFIG | SET_DISPLAY_CONFIG_FLAGS.SDC_APPLY | SET_DISPLAY_CONFIG_FLAGS.SDC_ALLOW_CHANGES);
			}, cancellation);
		}
		return result;
		void Pool((uint, uint, int) spec, DisplayTargetToken token)
		{
			if (!tokenPool.TryGetValue(spec, out Queue<DisplayTargetToken> value3))
			{
				value3 = (tokenPool[spec] = new Queue<DisplayTargetToken>());
			}
			value3.Enqueue(token);
		}
		async Task<Dictionary<DisplayTargetToken, DisplayConfig>> QueryTokenMap()
		{
			return (from x in (await DisplayConfigs.QueryDisplayConfigAsync(QUERY_DISPLAY_CONFIG_FLAGS.QDC_ONLY_ACTIVE_PATHS, cancellation)).ToArray()
				group x by x.GetTargetToken()).ToDictionary((IGrouping<DisplayTargetToken, DisplayConfig> x) => x.Key, (IGrouping<DisplayTargetToken, DisplayConfig> x) => x.First());
		}
	}

	private async Task<string> ConfigureDisplaysAsync(VitureLayoutMode mode, VitureLayoutType type, int refreshRate, string primaryPath, string viturePath, IEnumerable<string> vddPaths, CancellationToken cancellation = default(CancellationToken))
	{
		string viturePath2 = viturePath;
		string primaryPath2 = primaryPath;
		Func<DisplayConfig, bool> shouldActive = ((type == VitureLayoutType.External) ? ((Func<DisplayConfig, bool>)((DisplayConfig x) => IsViture(x) || x.GetDevicePath() == viturePath2)) : ((Func<DisplayConfig, bool>)((DisplayConfig _) => true)));
		await Task.Run(delegate
		{
			DisplayConfigs.SetActive(active: true, shouldActive);
			DisplayConfigs.SetAllExtended();
		}, cancellation);
		Logger.Info("[Layout] Configure: SetActive(" + ((type == VitureLayoutType.External) ? "viture" : "all") + ")+SetAllExtended done");
		DisplayConfigs dcs0 = await DisplayConfigs.QueryDisplayConfigAsync(QUERY_DISPLAY_CONFIG_FLAGS.QDC_ONLY_ACTIVE_PATHS, cancellation);
		DisplayConfig[] array = dcs0.ToArray();
		DisplayConfig displayConfig = array.FirstOrDefault((DisplayConfig x) => x.GetDevicePath() == viturePath2);
		DisplayConfig[] source = FilterAndOrder(array, (DisplayConfig x) => IsInternal(x) && x.GetDevicePath() != viturePath2).ToArray();
		string text = (source.Any((DisplayConfig x) => x.GetDevicePath() == primaryPath2) ? primaryPath2 : (source.FirstOrDefault()?.GetDevicePath() ?? primaryPath2));
		string text2;
		switch (mode)
		{
		case VitureLayoutMode.Horizontal1:
			text2 = ((type == VitureLayoutType.Mirror) ? text : vddPaths.ElementAtOrDefault(0));
			break;
		case VitureLayoutMode.Horizontal2:
		case VitureLayoutMode.Horizontal2A:
			text2 = ((type == VitureLayoutType.Mirror) ? text : vddPaths.ElementAtOrDefault(0));
			break;
		case VitureLayoutMode.Horizontal3:
		case VitureLayoutMode.Vertical3:
		case VitureLayoutMode.HorizontalPortrait:
		case VitureLayoutMode.Horizontal3A:
			text2 = ((type == VitureLayoutType.Mirror) ? text : vddPaths.ElementAtOrDefault(1));
			break;
		case VitureLayoutMode.UltraWide:
			text2 = vddPaths.ElementAtOrDefault(0);
			break;
		case VitureLayoutMode.Horizontal1A:
			text2 = ((type == VitureLayoutType.Mirror) ? text : viturePath2);
			break;
		case VitureLayoutMode.UltraWideA:
			text2 = viturePath2;
			break;
		default:
			text2 = primaryPath2;
			break;
		}
		string dstPrimaryPath = text2 ?? primaryPath2;
		bool flag = false;
		if (type == VitureLayoutType.External)
		{
			HashSet<string> keep = new HashSet<string>(vddPaths.Concat(new _003C_003Ez__ReadOnlySingleElementList<string>(viturePath2)));
			DisplayConfig[] array2 = array.Where((DisplayConfig x) => !keep.Contains(x.GetDevicePath())).ToArray();
			Logger.Info(string.Format("[Layout] External deactivate: keep=[{0}] deactivating={1} ", string.Join(" | ", keep), array2.Length) + "[" + string.Join(" | ", array2.Select((DisplayConfig d) => $"{d.GetDevicePath()}(active={d.IsActive},primary={d.IsPrimary})")) + "]");
			DisplayConfig[] array3 = array2;
			for (int i = 0; i < array3.Length; i++)
			{
				array3[i].IsActive = false;
				flag = true;
			}
		}
		if (!array.First((DisplayConfig x) => x.GetDevicePath() == dstPrimaryPath).IsPrimary)
		{
			Primary(array, dstPrimaryPath);
			flag = true;
		}
		if (mode >= VitureLayoutMode.Horizontal1A && displayConfig != null)
		{
			DisplaySourceMode sourceMode = displayConfig.GetSourceMode();
			if (sourceMode != null)
			{
				uint num = displayConfig.GetTargetMode()?.ActiveWidth ?? 0;
				uint num2 = displayConfig.GetTargetMode()?.ActiveHeight ?? 0;
				if (mode == VitureLayoutMode.Horizontal1A)
				{
					if (type == VitureLayoutType.Mirror)
					{
						DISPLAYCONFIG_TARGET_PREFERRED_MODE? targetPreferredMode = displayConfig.DeviceInfo.GetTargetPreferredMode();
						uint num3 = targetPreferredMode?.width ?? num;
						uint num4 = targetPreferredMode?.height ?? num2;
						if (num3 != 0 && num4 != 0 && (sourceMode.Width != num3 || sourceMode.Height != num4))
						{
							sourceMode.Width = num3;
							sourceMode.Height = num4;
							flag = true;
						}
					}
					else if (num >= 1920 && sourceMode.Width != 1920)
					{
						sourceMode.Width = 1920u;
						sourceMode.Height = num2;
						flag = true;
					}
				}
				else if (num >= 3840 && sourceMode.Width != 3840)
				{
					sourceMode.Width = 3840u;
					sourceMode.Height = num2;
					flag = true;
				}
			}
		}
		if (displayConfig != null)
		{
			DisplayTargetMode targetMode = displayConfig.GetTargetMode();
			if (targetMode != null && Math.Abs(targetMode.VSyncFreq.ToDouble() - (double)refreshRate) > 0.01)
			{
				targetMode.VSyncFreq = DisplayConfigsExtensions.ToDisplayConfigRational(refreshRate);
				flag = true;
			}
		}
		Logger.Info($"[Layout] Configure: dstPrimary={dstPrimaryPath} isNeedApply={flag}");
		if (flag)
		{
			await Task.Run(delegate
			{
				dcs0.SetDisplayConfig(SET_DISPLAY_CONFIG_FLAGS.SDC_USE_SUPPLIED_DISPLAY_CONFIG | SET_DISPLAY_CONFIG_FLAGS.SDC_APPLY | SET_DISPLAY_CONFIG_FLAGS.SDC_ALLOW_CHANGES);
			}, cancellation);
			Logger.Info("[Layout] Configure: apply(primary/resolution/refresh) done");
			if (type == VitureLayoutType.External)
			{
				try
				{
					DisplayConfig[] array4 = (await DisplayConfigs.QueryDisplayConfigAsync(QUERY_DISPLAY_CONFIG_FLAGS.QDC_ONLY_ACTIVE_PATHS, cancellation)).ToArray();
					DisplayConfig[] array5 = array4.Where((DisplayConfig x) => IsInternal(x) && x.GetDevicePath() != viturePath2).ToArray();
					Logger.Info($"[Layout] External post-apply: activePaths={array4.Length} " + $"internalStillActive={array5.Length} " + "[" + string.Join(" | ", array5.Select((DisplayConfig d) => d.GetDevicePath())) + "]");
				}
				catch (Exception ex)
				{
					Logger.Warning("[Layout] External post-apply query failed: " + ex.Message);
				}
			}
		}
		if (mode == VitureLayoutMode.Horizontal1A && type == VitureLayoutType.Mirror)
		{
			await Task.Run(delegate
			{
				DisplayConfigs.MakeMirror(viturePath2, dstPrimaryPath);
			}, cancellation);
			Logger.Info("[Layout] Configure: MakeMirror(viture<-" + dstPrimaryPath + ") done");
		}
		return dstPrimaryPath;
	}

	private static async Task<Rectangle> PlaceDisplaysAsync(VitureLayoutMode mode, VitureLayoutType type, string primaryPath, string viturePath, IEnumerable<string> vddPaths, CancellationToken cancellation = default(CancellationToken))
	{
		string viturePath2 = viturePath;
		string primaryPath2 = primaryPath;
		DisplayConfigs dcs0 = await DisplayConfigs.QueryDisplayConfigAsync(QUERY_DISPLAY_CONFIG_FLAGS.QDC_ONLY_ACTIVE_PATHS, cancellation);
		DisplayConfig[] array = dcs0.ToArray();
		DisplayConfig item = array.First((DisplayConfig x) => x.GetDevicePath() == viturePath2);
		array.First((DisplayConfig x) => x.GetDevicePath() == primaryPath2);
		IEnumerable<DisplayConfig> displays = vddPaths.GetDisplays(array);
		HashSet<string> includePaths = new HashSet<string>(vddPaths.Concat(new _003C_003Ez__ReadOnlyArray<string>(new string[2] { primaryPath2, viturePath2 })));
		List<DisplayConfig> list = (from x in array
			where !includePaths.Contains(x.GetDevicePath())
			orderby x.GetSourceMode()?.Left ?? int.MaxValue
			select x).ToList();
		List<string> second = list.Select((DisplayConfig x) => x.GetDevicePath()).ToList();
		if (mode == VitureLayoutMode.Horizontal1 || mode == VitureLayoutMode.Horizontal2 || mode == VitureLayoutMode.Horizontal2A)
		{
			Horizontal(new string[1] { primaryPath2 }.Concat(vddPaths).Concat(second).Concat(new _003C_003Ez__ReadOnlySingleElementList<string>(viturePath2))
				.GetDisplays(array), primaryPath2);
		}
		if (mode == VitureLayoutMode.Horizontal3 || mode == VitureLayoutMode.HorizontalPortrait || mode == VitureLayoutMode.Horizontal3A)
		{
			if (type == VitureLayoutType.Mirror)
			{
				Horizontal(new string[3]
				{
					vddPaths.First(),
					primaryPath2,
					vddPaths.Last()
				}.Concat(second).Concat(new _003C_003Ez__ReadOnlySingleElementList<string>(viturePath2)).GetDisplays(array), primaryPath2);
			}
			else
			{
				Horizontal(vddPaths.Concat(second).Concat(new _003C_003Ez__ReadOnlySingleElementList<string>(viturePath2)).GetDisplays(array), primaryPath2);
			}
		}
		if (mode == VitureLayoutMode.Vertical3)
		{
			if (type == VitureLayoutType.Mirror)
			{
				IEnumerable<DisplayConfig> displays2 = new string[3]
				{
					vddPaths.First(),
					primaryPath2,
					vddPaths.Last()
				}.GetDisplays(array);
				Vertical(displays2, primaryPath2);
				Horizontal(new _003C_003Ez__ReadOnlyArray<IEnumerable<DisplayConfig>>(new IEnumerable<DisplayConfig>[3]
				{
					displays2,
					list,
					new _003C_003Ez__ReadOnlySingleElementList<DisplayConfig>(item)
				}), 0);
			}
			else
			{
				Vertical(displays, primaryPath2);
				Horizontal(new _003C_003Ez__ReadOnlyArray<IEnumerable<DisplayConfig>>(new IEnumerable<DisplayConfig>[3]
				{
					displays,
					list,
					new _003C_003Ez__ReadOnlySingleElementList<DisplayConfig>(item)
				}), 0);
			}
		}
		if (mode == VitureLayoutMode.UltraWide)
		{
			Horizontal(vddPaths.Concat(second).Concat(new _003C_003Ez__ReadOnlySingleElementList<string>(viturePath2)).GetDisplays(array), primaryPath2);
		}
		if (mode == VitureLayoutMode.Horizontal1A)
		{
			_ = type;
			if (type == VitureLayoutType.Extend)
			{
				Horizontal(new string[1] { primaryPath2 }.Concat(second).GetDisplays(array), primaryPath2);
			}
			_ = type;
			_ = 2;
		}
		if (mode == VitureLayoutMode.UltraWideA)
		{
			if (type == VitureLayoutType.Mirror || type == VitureLayoutType.Extend)
			{
				Horizontal(new string[1] { primaryPath2 }.Concat(second).GetDisplays(array), primaryPath2);
			}
			_ = type;
			_ = 2;
		}
		Logger.Info($"[Layout] Place: applying positions mode={mode} type={type} " + string.Format("primary={0} vdds=[{1}] others={2} viture={3}", primaryPath2, string.Join(",", vddPaths), list.Count, viturePath2));
		await Task.Run(delegate
		{
			dcs0.SetDisplayConfig(SET_DISPLAY_CONFIG_FLAGS.SDC_USE_SUPPLIED_DISPLAY_CONFIG | SET_DISPLAY_CONFIG_FLAGS.SDC_APPLY | SET_DISPLAY_CONFIG_FLAGS.SDC_ALLOW_CHANGES);
		}, cancellation);
		Logger.Info("[Layout] Place: apply done");
		array = (await DisplayConfigs.QueryDisplayConfigAsync(QUERY_DISPLAY_CONFIG_FLAGS.QDC_ONLY_ACTIVE_PATHS, cancellation)).ToArray();
		return GetBounds(array.Where((DisplayConfig x) => x.GetDevicePath() == viturePath2));
	}

	private static void Horizontal(IEnumerable<IEnumerable<DisplayConfig>> pathGroups, int groupIndex, VerticalAlign align = VerticalAlign.Center)
	{
		List<List<DisplayConfig>> list = pathGroups.Select((IEnumerable<DisplayConfig> g) => g.Where(IsActiveSourceMode).ToList()).ToList();
		if (list.Count == 0)
		{
			return;
		}
		Rectangle[] array = list.Select(GetBounds).ToArray();
		Rectangle baseBounds = array[groupIndex];
		int num = baseBounds.Left - array.Take(groupIndex).Sum((Rectangle b) => b.Right - b.Left);
		for (int i = 0; i < list.Count; i++)
		{
			if (i != groupIndex)
			{
				TranslateGroup(list[i], num - array[i].Left, CalcDy((Top: array[i].Top, Bottom: array[i].Bottom)));
			}
			num += array[i].Right - array[i].Left;
		}
		int CalcDy((int Top, int Bottom) b)
		{
			return align switch
			{
				VerticalAlign.Top => baseBounds.Top - b.Top, 
				VerticalAlign.Center => (baseBounds.Top + baseBounds.Bottom) / 2 - (b.Top + b.Bottom) / 2, 
				VerticalAlign.Bottom => baseBounds.Bottom - b.Bottom, 
				_ => 0, 
			};
		}
	}

	private static void Vertical(IEnumerable<IEnumerable<DisplayConfig>> pathGroups, int groupIndex, HorizontalAlign align = HorizontalAlign.Center)
	{
		List<List<DisplayConfig>> list = pathGroups.Select((IEnumerable<DisplayConfig> g) => g.Where(IsActiveSourceMode).ToList()).ToList();
		if (list.Count == 0)
		{
			return;
		}
		Rectangle[] array = list.Select(GetBounds).ToArray();
		Rectangle baseBounds = array[groupIndex];
		int num = baseBounds.Top - array.Take(groupIndex).Sum((Rectangle b) => b.Bottom - b.Top);
		for (int i = 0; i < list.Count; i++)
		{
			if (i != groupIndex)
			{
				TranslateGroup(list[i], CalcDx((Left: array[i].Left, Right: array[i].Right)), num - array[i].Top);
			}
			num += array[i].Bottom - array[i].Top;
		}
		int CalcDx((int Left, int Right) b)
		{
			return align switch
			{
				HorizontalAlign.Left => baseBounds.Left - b.Left, 
				HorizontalAlign.Center => (baseBounds.Left + baseBounds.Right) / 2 - (b.Left + b.Right) / 2, 
				HorizontalAlign.Right => baseBounds.Right - b.Right, 
				_ => 0, 
			};
		}
	}

	private static void Horizontal(IEnumerable<DisplayConfig> paths, string path, VerticalAlign align = VerticalAlign.Center)
	{
		string path2 = path;
		List<DisplayConfig> list = paths.ToList();
		int num = list.FindIndex((DisplayConfig p) => p.GetDevicePath() == path2);
		if (num >= 0)
		{
			Horizontal(((IEnumerable<DisplayConfig>)list).Select((Func<DisplayConfig, IEnumerable<DisplayConfig>>)((DisplayConfig p) => new _003C_003Ez__ReadOnlySingleElementList<DisplayConfig>(p))), num, align);
		}
	}

	private static void Vertical(IEnumerable<DisplayConfig> paths, string path, HorizontalAlign align = HorizontalAlign.Center)
	{
		string path2 = path;
		List<DisplayConfig> list = paths.ToList();
		int num = list.FindIndex((DisplayConfig p) => p.GetDevicePath() == path2);
		if (num >= 0)
		{
			Vertical(((IEnumerable<DisplayConfig>)list).Select((Func<DisplayConfig, IEnumerable<DisplayConfig>>)((DisplayConfig p) => new _003C_003Ez__ReadOnlySingleElementList<DisplayConfig>(p))), num, align);
		}
	}

	private static void Primary(IEnumerable<DisplayConfig> paths, string path)
	{
		string path2 = path;
		List<DisplayConfig> list = paths.ToList();
		if (list.Count == 0)
		{
			return;
		}
		DisplaySourceMode displaySourceMode = list.FirstOrDefault((DisplayConfig p) => p.GetDevicePath() == path2)?.GetSourceMode();
		if (displaySourceMode == null)
		{
			return;
		}
		int left = displaySourceMode.Left;
		int top = displaySourceMode.Top;
		if (left == 0 && top == 0)
		{
			return;
		}
		foreach (DisplayConfig item in list)
		{
			if (IsActiveSourceMode(item))
			{
				item.GetSourceMode().Left -= left;
				item.GetSourceMode().Top -= top;
			}
		}
	}

	private static void Right(IEnumerable<DisplayConfig> paths, string path)
	{
		string path2 = path;
		List<DisplayConfig> list = paths.ToList();
		if (list.Count == 0)
		{
			return;
		}
		DisplayConfig displayConfig = (from p in list
			where p.GetDevicePath() != path2 && IsActiveSourceMode(p)
			orderby p.GetSourceMode().Left + GetEffectiveSize(p).Width descending
			select p).FirstOrDefault();
		if (displayConfig != null)
		{
			DisplayConfig displayConfig2 = list.FirstOrDefault((DisplayConfig p) => p.GetDevicePath() == path2);
			DisplaySourceMode displaySourceMode = displayConfig2?.GetSourceMode();
			if (displaySourceMode != null)
			{
				DisplaySourceMode sourceMode = displayConfig.GetSourceMode();
				(int Width, int Height) effectiveSize = GetEffectiveSize(displayConfig);
				int item = effectiveSize.Width;
				int item2 = effectiveSize.Height;
				int item3 = GetEffectiveSize(displayConfig2).Height;
				displaySourceMode.Left = sourceMode.Left + item;
				displaySourceMode.Top = sourceMode.Top + item2 / 2 - item3 / 2;
			}
		}
	}

	private static (int Width, int Height) GetEffectiveSize(DisplayConfig p)
	{
		DisplaySourceMode sourceMode = p.GetSourceMode();
		DISPLAYCONFIG_ROTATION rotation = p.TargetInfo.Rotation;
		if (rotation != DISPLAYCONFIG_ROTATION.DISPLAYCONFIG_ROTATION_ROTATE90 && rotation != DISPLAYCONFIG_ROTATION.DISPLAYCONFIG_ROTATION_ROTATE270)
		{
			return (Width: (int)sourceMode.Width, Height: (int)sourceMode.Height);
		}
		return (Width: (int)sourceMode.Height, Height: (int)sourceMode.Width);
	}

	private static Rectangle GetBounds(IEnumerable<DisplayConfig> group)
	{
		List<DisplayConfig> list = group.Where(IsActiveSourceMode).ToList();
		if (list.Count == 0)
		{
			return default(Rectangle);
		}
		int num = int.MaxValue;
		int num2 = int.MaxValue;
		int num3 = int.MinValue;
		int num4 = int.MinValue;
		foreach (DisplayConfig item3 in list)
		{
			DisplaySourceMode sourceMode = item3.GetSourceMode();
			(int Width, int Height) effectiveSize = GetEffectiveSize(item3);
			int item = effectiveSize.Width;
			int item2 = effectiveSize.Height;
			num = Math.Min(num, sourceMode.Left);
			num2 = Math.Min(num2, sourceMode.Top);
			num3 = Math.Max(num3, sourceMode.Left + item);
			num4 = Math.Max(num4, sourceMode.Top + item2);
		}
		return Rectangle.FromLTRB(num, num2, num3, num4);
	}

	private static void TranslateGroup(IEnumerable<DisplayConfig> group, int dx, int dy)
	{
		if (dx == 0 && dy == 0)
		{
			return;
		}
		foreach (DisplayConfig item in group)
		{
			if (IsActiveSourceMode(item))
			{
				item.GetSourceMode().Left += dx;
				item.GetSourceMode().Top += dy;
			}
		}
	}

	private static bool IsActiveSourceMode(DisplayConfig display)
	{
		if (display.IsActive)
		{
			return display.GetSourceMode() != null;
		}
		return false;
	}

	private static bool IsInternal(DisplayConfig x)
	{
		if (!IsViture(x))
		{
			return !IsVirtual(x);
		}
		return false;
	}

	private static bool IsViture(DisplayConfig x)
	{
		return (x.DeviceInfo.GetTargetDeviceName()?.monitorFriendlyDeviceName.ToString())?.ToUpper().Contains("VITURE") ?? false;
	}

	private static bool IsVirtual(DisplayConfig x)
	{
		return (x.DeviceInfo.GetTargetDeviceName()?.monitorFriendlyDeviceName.ToString())?.ToUpper().Contains("VIT-VDD") ?? false;
	}

	static DisplayManager2()
	{
		Instance = new DisplayManager2();
		GlassesMonitorHelper.Start();
	}

	internal DisplayInfo? GetVitureDisplay()
	{
		try
		{
			return (from x in GetVitures()
				select x.ToDisplayInfo()).FirstOrDefault();
		}
		catch (Exception ex)
		{
			Logger.Error(ex.Message, ex.StackTrace);
			return null;
		}
	}

	internal DisplayInfo? GetWideDisplay()
	{
		try
		{
			return (from x in GetAll(onlyActive: true)
				select x.ToDisplayInfo()).FirstOrDefault((DisplayInfo d) => IsWideRatio(d.CurrentSetting.Resolution));
		}
		catch (Exception ex)
		{
			Logger.Error(ex.Message, ex.StackTrace);
			return null;
		}
	}

	private static bool IsWideRatio(Size r)
	{
		if (r.Height > 0)
		{
			if (r.Width * 9 != r.Height * 32)
			{
				return r.Width * 10 == r.Height * 32;
			}
			return true;
		}
		return false;
	}

	internal DisplayInfo[] GetVitureDisplays()
	{
		try
		{
			return GetVitures().ToDisplayInfos();
		}
		catch (Exception ex)
		{
			Logger.Error("GetVitureDisplays error: " + ex.Message, ex.StackTrace);
			return Array.Empty<DisplayInfo>();
		}
	}

	internal DisplayInfo[] GetPhysicalDisplays()
	{
		try
		{
			return GetInternals(onlyActive: true).ToDisplayInfos();
		}
		catch (Exception ex)
		{
			Logger.Error(ex.Message, ex.StackTrace);
			return Array.Empty<DisplayInfo>();
		}
	}

	private DisplayInfo[] GetCurrentViewDisplays()
	{
		List<DisplayInfo> list = (from x in GetVirtuals(onlyActive: true)
			select x.ToDisplayInfo()).ToList();
		DisplayInfo displayInfo = GetPrimary()?.ToDisplayInfo();
		switch (layoutMode)
		{
		case LayoutMode.HorizonMirror1:
			if (displayInfo != null)
			{
				list.Insert(0, displayInfo);
			}
			break;
		case LayoutMode.HorizonMirror2:
		case LayoutMode.HorizonMirror3:
		case LayoutMode.HorizonPortraitMirror:
			if (displayInfo != null && list.Count > 0)
			{
				list.Insert(1, displayInfo);
			}
			break;
		case LayoutMode.VerticalMirror3:
			if (GetPhysicalDisplays().Length != 0 && displayInfo != null && list.Count > 0)
			{
				list.Insert(1, displayInfo);
			}
			break;
		}
		return list.ToArray();
	}

	public Rectangle? GetVitureRect()
	{
		try
		{
			List<DisplayConfig> list = GetVitures(onlyActive: true).ToList();
			if (list.Count == 0)
			{
				return null;
			}
			Rectangle bounds = GetBounds(list);
			return bounds.IsEmpty ? null : new Rectangle?(bounds);
		}
		catch (Exception ex)
		{
			Logger.Error(ex.Message, ex.StackTrace);
			return null;
		}
	}

	public void SetAllDisplayExtend()
	{
		try
		{
			DisplayConfigs.SetAllExtended();
			Logger.Info("Config all display extend mode success!");
		}
		catch (Exception ex)
		{
			Logger.Error(ex.Message, ex.StackTrace);
		}
	}

	public void DisableAllPhyDisplays()
	{
		try
		{
			DisplayConfigs.SetActive(active: false, IsInternal);
			Logger.Info("DisableAllPhyDisplays done");
		}
		catch (Exception ex)
		{
			Logger.Error(ex.Message, ex.StackTrace);
		}
	}
}
