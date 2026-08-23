using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Win32;
using Windows.Win32;
using Windows.Win32.Devices.Display;
using Windows.Win32.Foundation;
using Windows.Win32.UI.Shell;

namespace VitureCommonLibrary;

public class DisplayConfigs : IReadOnlyCollection<DisplayConfig>, IEnumerable<DisplayConfig>, IEnumerable
{
	[CompilerGenerated]
	private sealed class _003CGetEnumerator_003Ed__5 : IEnumerator<DisplayConfig>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private DisplayConfig _003C_003E2__current;

		public DisplayConfigs _003C_003E4__this;

		private int _003Ci_003E5__2;

		DisplayConfig IEnumerator<DisplayConfig>.Current
		{
			[DebuggerHidden]
			get
			{
				return _003C_003E2__current;
			}
		}

		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return _003C_003E2__current;
			}
		}

		[DebuggerHidden]
		public _003CGetEnumerator_003Ed__5(int _003C_003E1__state)
		{
			this._003C_003E1__state = _003C_003E1__state;
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
			_003C_003E1__state = -2;
		}

		private bool MoveNext()
		{
			int num = _003C_003E1__state;
			DisplayConfigs displayConfigs = _003C_003E4__this;
			switch (num)
			{
			default:
				return false;
			case 0:
				_003C_003E1__state = -1;
				_003Ci_003E5__2 = 0;
				break;
			case 1:
				_003C_003E1__state = -1;
				_003Ci_003E5__2++;
				break;
			}
			if (_003Ci_003E5__2 < displayConfigs.Paths.Length)
			{
				_003C_003E2__current = new DisplayConfig(displayConfigs, _003Ci_003E5__2);
				_003C_003E1__state = 1;
				return true;
			}
			return false;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}
	}

	public QUERY_DISPLAY_CONFIG_FLAGS Flags;

	public DISPLAYCONFIG_TOPOLOGY_ID Topology;

	public DISPLAYCONFIG_PATH_INFO[] Paths;

	public DISPLAYCONFIG_MODE_INFO[] Modes;

	public int Count => Paths.Length;

	public DisplayConfigs(DISPLAYCONFIG_PATH_INFO[] paths, DISPLAYCONFIG_MODE_INFO[] modes, QUERY_DISPLAY_CONFIG_FLAGS flags, DISPLAYCONFIG_TOPOLOGY_ID topology)
	{
		Paths = paths;
		Modes = modes;
		Flags = flags;
		Topology = topology;
	}

	[IteratorStateMachine(typeof(_003CGetEnumerator_003Ed__5))]
	public IEnumerator<DisplayConfig> GetEnumerator()
	{
		//yield-return decompiler failed: Unexpected instruction in Iterator.Dispose()
		return new _003CGetEnumerator_003Ed__5(0)
		{
			_003C_003E4__this = this
		};
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}

	public static DisplayConfigs QueryDisplayConfig(QUERY_DISPLAY_CONFIG_FLAGS flags)
	{
		int num = 0;
		for (int i = 0; i < 5; i++)
		{
			num = TryQueryDisplayConfig(flags, out DisplayConfigs configs);
			if (num == 0)
			{
				return configs;
			}
		}
		throw new Win32Exception(num);
	}

	public unsafe static int TryQueryDisplayConfig(QUERY_DISPLAY_CONFIG_FLAGS flags, out DisplayConfigs configs)
	{
		configs = null;
		WIN32_ERROR displayConfigBufferSizes = PInvoke.GetDisplayConfigBufferSizes(flags, out var numPathArrayElements, out var numModeInfoArrayElements);
		if (displayConfigBufferSizes != 0)
		{
			return (int)displayConfigBufferSizes;
		}
		DISPLAYCONFIG_PATH_INFO[] array = new DISPLAYCONFIG_PATH_INFO[numPathArrayElements];
		DISPLAYCONFIG_MODE_INFO[] array2 = new DISPLAYCONFIG_MODE_INFO[numModeInfoArrayElements];
		DISPLAYCONFIG_TOPOLOGY_ID topology = (DISPLAYCONFIG_TOPOLOGY_ID)0;
		fixed (DISPLAYCONFIG_PATH_INFO* pathArray = array)
		{
			fixed (DISPLAYCONFIG_MODE_INFO* modeInfoArray = array2)
			{
				displayConfigBufferSizes = PInvoke.QueryDisplayConfig(flags, &numPathArrayElements, pathArray, &numModeInfoArrayElements, modeInfoArray, ((flags & QUERY_DISPLAY_CONFIG_FLAGS.QDC_DATABASE_CURRENT) != 0) ? (&topology) : null);
				if (displayConfigBufferSizes != 0)
				{
					return (int)displayConfigBufferSizes;
				}
			}
		}
		Array.Resize(ref array, (int)numPathArrayElements);
		Array.Resize(ref array2, (int)numModeInfoArrayElements);
		configs = new DisplayConfigs(array, array2, flags, topology);
		return 0;
	}

	public static async Task<DisplayConfigs> QueryDisplayConfigAsync(QUERY_DISPLAY_CONFIG_FLAGS flags, CancellationToken cancellation = default(CancellationToken))
	{
		return await Task.Run(delegate
		{
			int num = 0;
			for (int i = 0; i < 17; i++)
			{
				num = TryQueryDisplayConfig(flags, out DisplayConfigs configs);
				if (num == 0)
				{
					return configs;
				}
				if (i != 16 && cancellation.WaitHandle.WaitOne(300))
				{
					cancellation.ThrowIfCancellationRequested();
				}
			}
			throw new Win32Exception(num);
		}, cancellation);
	}

	public unsafe static void SetDisplayConfig(DISPLAYCONFIG_PATH_INFO[]? paths, DISPLAYCONFIG_MODE_INFO[]? modes, SET_DISPLAY_CONFIG_FLAGS flags)
	{
		fixed (DISPLAYCONFIG_PATH_INFO* ptr = paths)
		{
			fixed (DISPLAYCONFIG_MODE_INFO* ptr2 = modes)
			{
				int num = PInvoke.SetDisplayConfig((paths != null) ? ((uint)paths.Length) : 0u, (paths != null && paths.Length != 0) ? ptr : null, (modes != null) ? ((uint)modes.Length) : 0u, (modes != null && modes.Length != 0) ? ptr2 : null, flags);
				if (num != 0)
				{
					Logger.Error($"[CCD] SetDisplayConfig failed: win32={num} flags={flags} paths={((paths != null) ? paths.Length : 0)} modes={((modes != null) ? modes.Length : 0)}");
					throw new Win32Exception(num);
				}
			}
		}
	}

	public void SetDisplayConfig(SET_DISPLAY_CONFIG_FLAGS flags)
	{
		SetDisplayConfig(Paths, Modes, flags);
	}

	public unsafe static void ChangeNotify()
	{
		try
		{
			PInvoke.PostMessage(HWND.HWND_BROADCAST, 126u, 0u, 0);
			PInvoke.PostMessage(HWND.HWND_BROADCAST, 26u, 0u, 0);
			PInvoke.SHChangeNotify(SHCNE_ID.SHCNE_UPDATEIMAGE, SHCNF_FLAGS.SHCNF_FLUSHNOWAIT, null, null);
		}
		catch
		{
		}
	}

	public static void ResetDisplayConfig(bool all = false)
	{
		string[] array = new string[4] { "Configuration", "Connectivity", "MonitorDataStore", "ScaleFactors" };
		if (all)
		{
			try
			{
				RegistryKey localMachine = Registry.LocalMachine;
				string[] array2 = array;
				foreach (string text in array2)
				{
					using RegistryKey registryKey = localMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\GraphicsDrivers\\" + text, writable: true);
					if (registryKey != null)
					{
						string[] subKeyNames = registryKey.GetSubKeyNames();
						foreach (string subkey in subKeyNames)
						{
							try
							{
								registryKey.DeleteSubKeyTree(subkey);
							}
							catch (ArgumentException)
							{
							}
						}
					}
				}
				return;
			}
			catch (Exception ex2) when (ex2 is SecurityException || ex2 is UnauthorizedAccessException)
			{
				string s = "$baseSubKey='HKLM:\\SYSTEM\\CurrentControlSet\\Control\\GraphicsDrivers';foreach($sub in @('" + string.Join("','", array) + "')){Get-ChildItem \"$baseSubKey\\$sub\" -EA 0|Remove-Item -Recurse -Force -EA 0}";
				string text2 = Convert.ToBase64String(Encoding.Unicode.GetBytes(s));
				using Process process = new Process
				{
					StartInfo = new ProcessStartInfo
					{
						FileName = "powershell.exe",
						Arguments = "-NoProfile -NonInteractive -WindowStyle Hidden -EncodedCommand " + text2,
						Verb = "runas",
						UseShellExecute = true,
						WindowStyle = ProcessWindowStyle.Hidden
					}
				};
				process.Start();
				process.WaitForExit(30000);
				return;
			}
			catch
			{
				return;
			}
		}
		DisplayConfigs source = QueryDisplayConfig(QUERY_DISPLAY_CONFIG_FLAGS.QDC_ALL_PATHS).Distinct();
		HashSet<string> prefixes = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
		DisplayConfig[] array3 = source.Where((DisplayConfig x) => x.DeviceInfo.IsConnected).ToArray();
		for (int i = 0; i < array3.Length; i++)
		{
			DISPLAYCONFIG_TARGET_DEVICE_NAME? targetDeviceName = array3[i].DeviceInfo.GetTargetDeviceName();
			if (targetDeviceName.HasValue && targetDeviceName.Value.monitorFriendlyDeviceName.ToString().ToUpper().Contains("VITURE"))
			{
				string text3 = targetDeviceName.Value.monitorDevicePath.ToString().Split(new char[1] { '#' }).ElementAtOrDefault(1);
				if (text3 != null && !string.IsNullOrWhiteSpace(text3))
				{
					prefixes.Add(text3);
				}
			}
		}
		if (prefixes.Count == 0)
		{
			return;
		}
		prefixes.Add("SMKD1CE123456789ABCD");
		try
		{
			RegistryKey localMachine2 = Registry.LocalMachine;
			string[] array2 = array;
			foreach (string text4 in array2)
			{
				using RegistryKey registryKey2 = localMachine2.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\GraphicsDrivers\\" + text4, writable: true);
				if (registryKey2 == null)
				{
					continue;
				}
				foreach (string item in registryKey2.GetSubKeyNames().Where(IsMatch))
				{
					try
					{
						registryKey2.DeleteSubKeyTree(item);
					}
					catch (ArgumentException)
					{
					}
				}
			}
		}
		catch (Exception ex4) when (ex4 is SecurityException || ex4 is UnauthorizedAccessException)
		{
			string text5 = string.Join("','", prefixes);
			string s2 = "$baseSubKey='HKLM:\\SYSTEM\\CurrentControlSet\\Control\\GraphicsDrivers';$prefixes=@('" + text5 + "');foreach($sub in @('" + string.Join("','", array) + "')){Get-ChildItem \"$baseSubKey\\$sub\" -EA 0|Where-Object{$name=$_.PSChildName;$segs=$name -split '[+^]';$segs=if($segs.Count -gt 1){$segs[0..($segs.Count-2)]}else{$segs};$segs|Where-Object{$seg=$_;$prefixes|Where-Object{$seg.StartsWith($_,[StringComparison]::OrdinalIgnoreCase)}}}|Remove-Item -Recurse -Force -EA 0}";
			string text6 = Convert.ToBase64String(Encoding.Unicode.GetBytes(s2));
			using Process process2 = new Process
			{
				StartInfo = new ProcessStartInfo
				{
					FileName = "powershell.exe",
					Arguments = "-NoProfile -NonInteractive -WindowStyle Hidden -EncodedCommand " + text6,
					Verb = "runas",
					UseShellExecute = true,
					WindowStyle = ProcessWindowStyle.Hidden
				}
			};
			process2.Start();
			process2.WaitForExit(30000);
		}
		catch
		{
		}
		bool IsMatch(string name)
		{
			string[] array4 = name.Split('+', '^');
			IEnumerable<string> source2;
			if (array4.Length != 1)
			{
				source2 = array4.Take(array4.Length - 1);
			}
			else
			{
				IEnumerable<string> enumerable = array4;
				source2 = enumerable;
			}
			return source2.Any((string seg) => prefixes.Any((string p) => seg.StartsWith(p, StringComparison.OrdinalIgnoreCase)));
		}
	}

	public DisplayConfigs Distinct()
	{
		if (!Flags.HasFlag(QUERY_DISPLAY_CONFIG_FLAGS.QDC_ALL_PATHS))
		{
			return this;
		}
		List<int> list = new List<int>(Paths.Length);
		Dictionary<ulong, int> dictionary = new Dictionary<ulong, int>(Paths.Length);
		Func<DISPLAYCONFIG_PATH_INFO, int> func = (DISPLAYCONFIG_PATH_INFO pi) => ((pi.flags & 1) == 0) ? (((bool)pi.targetInfo.targetAvailable) ? 1 : 0) : 2;
		for (int i = 0; i < Paths.Length; i++)
		{
			ref DISPLAYCONFIG_PATH_INFO reference = ref Paths[i];
			ref DISPLAYCONFIG_PATH_TARGET_INFO targetInfo = ref reference.targetInfo;
			ulong key = (targetInfo.adapterId.ToUInt64() << 32) | targetInfo.id;
			if (!dictionary.TryGetValue(key, out var value))
			{
				dictionary[key] = list.Count;
				list.Add(i);
			}
			else if (func(reference) > func(Paths[list[value]]))
			{
				list[value] = i;
			}
		}
		DISPLAYCONFIG_PATH_INFO[] array = new DISPLAYCONFIG_PATH_INFO[list.Count];
		for (int j = 0; j < list.Count; j++)
		{
			array[j] = Paths[list[j]];
		}
		return new DisplayConfigs(array, (DISPLAYCONFIG_MODE_INFO[])Modes.Clone(), Flags, Topology);
	}

	public static void SetAllExtended(bool forceReset = false)
	{
		DisplayConfigs dcs = QueryDisplayConfig(QUERY_DISPLAY_CONFIG_FLAGS.QDC_ONLY_ACTIVE_PATHS | QUERY_DISPLAY_CONFIG_FLAGS.QDC_VIRTUAL_MODE_AWARE).Distinct();
		List<DisplayConfig> list = dcs.ToArray().ToList();
		if (list.Count == 0)
		{
			return;
		}
		if (!forceReset)
		{
			bool flag = true;
			HashSet<ushort> hashSet = new HashSet<ushort>();
			HashSet<(ulong, uint)> hashSet2 = new HashSet<(ulong, uint)>();
			foreach (DisplayConfig item in list)
			{
				if ((item.Flags & 8u) != 0)
				{
					ushort? cloneGroupId = item.SourceInfo.CloneGroupId;
					if (!cloneGroupId.HasValue || !hashSet.Add(cloneGroupId.Value))
					{
						flag = false;
						break;
					}
				}
				else if (!hashSet2.Add((item.SourceInfo.AdapterId, item.SourceInfo.Id)))
				{
					flag = false;
					break;
				}
			}
			if (flag)
			{
				return;
			}
		}
		bool flag2 = list.Any((DisplayConfig p) => (p.Flags & 8) != 0);
		ushort num = 0;
		Dictionary<ulong, uint> dictionary = new Dictionary<ulong, uint>();
		foreach (DisplayConfig item2 in list)
		{
			if (flag2)
			{
				item2.Flags |= 8u;
			}
			bool num2 = (item2.Flags & 8) != 0;
			item2.SourceInfo.ModeInfoIdx = uint.MaxValue;
			item2.TargetInfo.ModeInfoIdx = uint.MaxValue;
			dictionary.TryGetValue(item2.SourceInfo.AdapterId, out var value);
			item2.SourceInfo.Id = value;
			dictionary[item2.SourceInfo.AdapterId] = value + 1;
			if (num2)
			{
				item2.SourceInfo.CloneGroupId = num++;
				item2.TargetInfo.DesktopModeInfoIdx = null;
			}
		}
		SET_DISPLAY_CONFIG_FLAGS flags = SET_DISPLAY_CONFIG_FLAGS.SDC_USE_SUPPLIED_DISPLAY_CONFIG | SET_DISPLAY_CONFIG_FLAGS.SDC_APPLY | SET_DISPLAY_CONFIG_FLAGS.SDC_ALLOW_CHANGES | SET_DISPLAY_CONFIG_FLAGS.SDC_VIRTUAL_MODE_AWARE;
		foreach (DisplayConfig item3 in list)
		{
			bool flag3 = (item3.Flags & 8) != 0;
			Logger.Info("[CCD] extend path: name='" + item3.GetDeviceName() + "' gdiPath='" + item3.DeviceInfo.GetSourceDeviceName()?.viewGdiDeviceName.ToString() + "' devPath='" + item3.GetDevicePath() + "' " + $"virtual={flag3} adapter={item3.SourceInfo.AdapterId} srcId={item3.SourceInfo.Id} tgtId={item3.TargetInfo.Id} " + $"clone={item3.SourceInfo.CloneGroupId} srcModeIdx={item3.SourceInfo.ModeInfoIdx} tgtModeIdx={item3.TargetInfo.ModeInfoIdx} " + $"desktopIdx={item3.TargetInfo.DesktopModeInfoIdx} flags=0x{item3.Flags:X} active={item3.IsActive} primary={item3.IsPrimary}");
		}
		SetDisplayConfig(list.Select((DisplayConfig p) => dcs.Paths[p.PathIndex]).ToArray(), null, flags);
	}

	public static void SetActive(bool active = true, Func<DisplayConfig, bool>? predicate = null)
	{
		DisplayConfigs displayConfigs = QueryDisplayConfig(active ? QUERY_DISPLAY_CONFIG_FLAGS.QDC_ALL_PATHS : QUERY_DISPLAY_CONFIG_FLAGS.QDC_ONLY_ACTIVE_PATHS).Distinct();
		List<DisplayConfig> list = (from x in displayConfigs.ToArray()
			where x.DeviceInfo.IsConnected
			select x).ToList();
		if (list.Count == 0)
		{
			return;
		}
		bool flag = false;
		foreach (DisplayConfig item in list)
		{
			if (item.IsActive != active && (predicate == null || predicate(item)))
			{
				item.IsActive = active;
				flag = true;
			}
		}
		if (flag)
		{
			displayConfigs.SetDisplayConfig(SET_DISPLAY_CONFIG_FLAGS.SDC_USE_SUPPLIED_DISPLAY_CONFIG | SET_DISPLAY_CONFIG_FLAGS.SDC_APPLY | SET_DISPLAY_CONFIG_FLAGS.SDC_ALLOW_CHANGES);
		}
	}

	public static void MakeMirror(string dstDevicePath, string srcDevicePath, ushort? cloneGroupId = null)
	{
		string srcDevicePath2 = srcDevicePath;
		string dstDevicePath2 = dstDevicePath;
		if (cloneGroupId == ushort.MaxValue)
		{
			throw new ArgumentOutOfRangeException("cloneGroupId", "0xFFFF 是 CloneGroupId 的无效值，不能用作克隆组标识。");
		}
		if (dstDevicePath2 == srcDevicePath2)
		{
			return;
		}
		DisplayConfigs displayConfigs = QueryDisplayConfig(QUERY_DISPLAY_CONFIG_FLAGS.QDC_ONLY_ACTIVE_PATHS | QUERY_DISPLAY_CONFIG_FLAGS.QDC_VIRTUAL_MODE_AWARE);
		DisplayConfig[] dcs = displayConfigs.ToArray();
		DisplayConfig displayConfig = dcs.First((DisplayConfig x) => x.GetDevicePath() == srcDevicePath2);
		DisplayConfig displayConfig2 = dcs.First((DisplayConfig x) => x.GetDevicePath() == dstDevicePath2);
		bool num = displayConfig.SourceInfo.AdapterId == displayConfig2.SourceInfo.AdapterId && displayConfig.SourceInfo.Id == displayConfig2.SourceInfo.Id;
		ushort? cloneGroupId2 = displayConfig.SourceInfo.CloneGroupId;
		int num2;
		if (cloneGroupId2.HasValue)
		{
			ushort valueOrDefault = cloneGroupId2.GetValueOrDefault();
			if (valueOrDefault != ushort.MaxValue)
			{
				num2 = ((valueOrDefault == displayConfig2.SourceInfo.CloneGroupId) ? 1 : 0);
				goto IL_015f;
			}
		}
		num2 = 0;
		goto IL_015f;
		IL_01bc:
		int num3;
		bool flag = (byte)num3 != 0;
		bool flag2;
		if (num || flag2 || flag)
		{
			return;
		}
		bool num4 = (displayConfig.Flags & 8) != 0;
		bool flag3 = (displayConfig2.Flags & 8) != 0;
		if (num4 != flag3)
		{
			throw new InvalidOperationException("Primary and clone paths do not have the same support for virtual topology.");
		}
		if (num4)
		{
			DisplaySourceMode sourceMode = displayConfig.GetSourceMode();
			DisplaySourceMode sourceMode2 = displayConfig2.GetSourceMode();
			if (sourceMode != null && sourceMode2 != null)
			{
				sourceMode2.Width = sourceMode.Width;
				sourceMode2.Height = sourceMode.Height;
				sourceMode2.Left = sourceMode.Left;
				sourceMode2.Top = sourceMode.Top;
				sourceMode2.PixelFormat = sourceMode.PixelFormat;
				displayConfig2.TargetInfo.DesktopModeInfoIdx = ushort.MaxValue;
			}
			else
			{
				ushort value = cloneGroupId ?? PickFreeCloneGroupId();
				displayConfig.SourceInfo.ModeInfoIdx = uint.MaxValue;
				displayConfig.TargetInfo.DesktopModeInfoIdx = ushort.MaxValue;
				displayConfig.TargetInfo.ModeInfoIdx = uint.MaxValue;
				displayConfig.SourceInfo.CloneGroupId = value;
				displayConfig2.SourceInfo.ModeInfoIdx = uint.MaxValue;
				displayConfig2.TargetInfo.DesktopModeInfoIdx = ushort.MaxValue;
				displayConfig2.TargetInfo.ModeInfoIdx = uint.MaxValue;
				displayConfig2.SourceInfo.CloneGroupId = value;
			}
		}
		else
		{
			if (displayConfig.SourceInfo.AdapterId != displayConfig2.SourceInfo.AdapterId)
			{
				throw new InvalidOperationException("Primary and clone paths are not on the same adapter.");
			}
			displayConfig2.SourceInfo.Id = displayConfig.SourceInfo.Id;
			displayConfig.TargetInfo.ModeInfoIdx = uint.MaxValue;
			displayConfig.TargetInfo.DesktopModeInfoIdx = ushort.MaxValue;
			displayConfig2.SourceInfo.ModeInfoIdx = uint.MaxValue;
			displayConfig2.TargetInfo.ModeInfoIdx = uint.MaxValue;
			displayConfig2.TargetInfo.DesktopModeInfoIdx = ushort.MaxValue;
		}
		displayConfigs.SetDisplayConfig(SET_DISPLAY_CONFIG_FLAGS.SDC_USE_SUPPLIED_DISPLAY_CONFIG | SET_DISPLAY_CONFIG_FLAGS.SDC_APPLY | SET_DISPLAY_CONFIG_FLAGS.SDC_ALLOW_CHANGES | SET_DISPLAY_CONFIG_FLAGS.SDC_VIRTUAL_MODE_AWARE);
		return;
		IL_015f:
		flag2 = (byte)num2 != 0;
		DisplaySourceMode sourceMode3 = displayConfig.GetSourceMode();
		if (sourceMode3 != null)
		{
			DisplaySourceMode sourceMode4 = displayConfig2.GetSourceMode();
			if (sourceMode4 != null && sourceMode3.Width == sourceMode4.Width && sourceMode3.Height == sourceMode4.Height && sourceMode3.Left == sourceMode4.Left)
			{
				num3 = ((sourceMode3.Top == sourceMode4.Top) ? 1 : 0);
				goto IL_01bc;
			}
		}
		num3 = 0;
		goto IL_01bc;
		ushort PickFreeCloneGroupId()
		{
			HashSet<ushort> hashSet = new HashSet<ushort>();
			DisplayConfig[] array = dcs;
			for (int i = 0; i < array.Length; i++)
			{
				ushort? cloneGroupId3 = array[i].SourceInfo.CloneGroupId;
				if (cloneGroupId3.HasValue)
				{
					ushort valueOrDefault2 = cloneGroupId3.GetValueOrDefault();
					if (valueOrDefault2 != ushort.MaxValue)
					{
						hashSet.Add(valueOrDefault2);
					}
				}
			}
			for (ushort num5 = 0; num5 < ushort.MaxValue; num5++)
			{
				if (!hashSet.Contains(num5))
				{
					return num5;
				}
			}
			throw new InvalidOperationException("CloneGroupId 已用尽 (0..0xFFFE)。");
		}
	}
}
