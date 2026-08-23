using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Windows.Win32.Devices.Display;

namespace VitureCommonLibrary;

public static class SudoVirtualDisplayExtensions
{
	public static async Task<IEnumerable<DisplayTargetToken>> WaitVirtualDisplayAsync(this SudoVirtualDisplay display, IEnumerable<DisplayTargetToken> list, CancellationToken cancellation = default(CancellationToken))
	{
		List<DisplayTargetToken> pending = list.ToList();
		Logger.Info(string.Format("[VDD] wait-ready begin: {0} pending [{1}]", pending.Count, string.Join(", ", pending)));
		for (int i = 0; i < 30; i++)
		{
			if (pending.Count <= 0)
			{
				break;
			}
			HashSet<DisplayTargetToken> ready = await QueryActiveAsync(requireDevicePath: true, cancellation);
			int count = pending.Count;
			pending.RemoveAll((DisplayTargetToken s) => ready.Contains(s));
			if (pending.Count != count || i % 5 == 0)
			{
				Logger.Info($"[VDD] wait-ready poll #{i}: readyActive(devPath)={ready.Count} stillPending={pending.Count}");
			}
			if (pending.Count == 0)
			{
				break;
			}
			await Task.Delay(300, cancellation);
		}
		if (pending.Count > 0)
		{
			Logger.Warning(string.Format("[VDD] wait-ready TIMEOUT after 30 polls(~10s): stillPending={0} [{1}] - VDD added but never enumerated ready (virtual display driver likely hung)", pending.Count, string.Join(", ", pending)));
		}
		else
		{
			Logger.Info("[VDD] wait-ready done: all ready");
		}
		return pending;
	}

	public static async Task<IEnumerable<DisplayTargetToken>> AddVirtualDisplaysAsync(this SudoVirtualDisplay display, IEnumerable<(uint Width, uint Height, int RefreshRate)> vdds, CancellationToken cancellation = default(CancellationToken))
	{
		if (display == null)
		{
			throw new ArgumentNullException("display");
		}
		List<(uint Width, uint Height, int RefreshRate)> specs = vdds.ToList();
		DisplayTargetToken[] tokens = new DisplayTargetToken[specs.Count];
		Guid[] guids = new Guid[specs.Count];
		for (int idx2 = 0; idx2 < specs.Count; idx2++)
		{
			DisplayTargetToken[] array = tokens;
			int num = idx2;
			_ = ref array[num];
			Guid[] array2 = guids;
			int num2 = idx2;
			_ = ref array2[num2];
			(DisplayTargetToken, Guid) tuple = await AddOneAsync(display, specs[idx2], idx2, cancellation);
			array[num] = tuple.Item1;
			array2[num2] = tuple.Item2;
		}
		Logger.Info($"[VDD] added {specs.Count} display(s), waiting for ready...");
		HashSet<DisplayTargetToken> notReady = new HashSet<DisplayTargetToken>(await display.WaitVirtualDisplayAsync(tokens, cancellation));
		if (notReady.Count > 0)
		{
			Logger.Warning($"[VDD] {notReady.Count} display(s) never enumerated ready; removing stuck target(s) and re-adding once");
			List<DisplayTargetToken> readdTokens = new List<DisplayTargetToken>();
			for (int idx2 = 0; idx2 < specs.Count; idx2++)
			{
				if (notReady.Contains(tokens[idx2]))
				{
					try
					{
						display.RemoveVirtualDisplay(guids[idx2]);
					}
					catch (Exception ex)
					{
						Logger.Warning($"[VDD] remove stuck target {guids[idx2]} failed: {ex.Message}");
					}
					DisplayTargetToken[] array = tokens;
					int num2 = idx2;
					_ = ref array[num2];
					Guid[] array2 = guids;
					int num = idx2;
					_ = ref array2[num];
					(DisplayTargetToken, Guid) tuple = await AddOneAsync(display, specs[idx2], idx2, cancellation);
					array[num2] = tuple.Item1;
					array2[num] = tuple.Item2;
					readdTokens.Add(tokens[idx2]);
				}
			}
			IEnumerable<DisplayTargetToken> enumerable = await display.WaitVirtualDisplayAsync(readdTokens, cancellation);
			if (enumerable.Any())
			{
				throw new TimeoutException("Timeout waiting for virtual display ready for [" + string.Join(", ", enumerable) + "]");
			}
		}
		return tokens.ToList();
	}

	private static async Task<(DisplayTargetToken Token, Guid Guid)> AddOneAsync(SudoVirtualDisplay display, (uint Width, uint Height, int RefreshRate) vdd, int index, CancellationToken cancellation)
	{
		int i = 0;
		while (true)
		{
			cancellation.ThrowIfCancellationRequested();
			if (display.AddVirtualDisplay(vdd.Width, vdd.Height, (uint)vdd.RefreshRate, string.Empty, string.Empty, out (DisplayTargetToken, Guid) output))
			{
				Logger.Info($"[VDD] add ok [{index}] {vdd.Width}x{vdd.Height}@{vdd.RefreshRate}Hz token={output.Item1} (attempt {i + 1})");
				return (Token: output.Item1, Guid: output.Item2);
			}
			Logger.Warning($"[VDD] add failed [{index}] {vdd.Width}x{vdd.Height}@{vdd.RefreshRate}Hz attempt {i + 1}/3");
			if (i + 1 >= 3)
			{
				break;
			}
			await Task.Delay(300, cancellation);
			i++;
		}
		throw new InvalidOperationException($"Add virtual display failed for [{index}]{vdd.Width}x{vdd.Height} @ {vdd.RefreshRate}Hz");
	}

	private static async Task<HashSet<DisplayTargetToken>> QueryActiveAsync(bool requireDevicePath = false, CancellationToken cancellation = default(CancellationToken))
	{
		IEnumerable<DisplayConfig> source = (await DisplayConfigs.QueryDisplayConfigAsync(QUERY_DISPLAY_CONFIG_FLAGS.QDC_ONLY_ACTIVE_PATHS, cancellation)).ToArray().AsEnumerable();
		if (requireDevicePath)
		{
			source = source.Where((DisplayConfig x) => !string.IsNullOrEmpty(x.GetDevicePath()));
		}
		HashSet<DisplayTargetToken> hashSet = new HashSet<DisplayTargetToken>();
		foreach (DisplayTargetToken item in source.Select((DisplayConfig x) => x.GetTargetToken()))
		{
			hashSet.Add(item);
		}
		return hashSet;
	}
}
