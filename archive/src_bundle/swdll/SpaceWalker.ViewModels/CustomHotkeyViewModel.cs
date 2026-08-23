using System.Collections.Generic;
using System.Collections.ObjectModel;
using ReactiveUI;
using SpaceWalker.Database;

namespace SpaceWalker.ViewModels;

public class CustomHotkeyViewModel : ReactiveObject
{
	private ObservableCollection<GlobalHotkeyItem> globalHotkeys = new ObservableCollection<GlobalHotkeyItem>();

	public ObservableCollection<GlobalHotkeyItem> GlobalHotkeys
	{
		get
		{
			return globalHotkeys;
		}
		set
		{
			this.RaiseAndSetIfChanged(ref globalHotkeys, value, "GlobalHotkeys");
		}
	}

	public CustomHotkeyViewModel()
	{
		loadHotKeys();
	}

	private void loadHotKeys()
	{
		GlobalHotkeys.Clear();
		foreach (KeyValuePair<string, string> globalHotkey in DbManager.Instance.Settings.GlobalHotkeys)
		{
			if (!(globalHotkey.Key == "CalibrationHeader"))
			{
				GlobalHotkeys.Add(new GlobalHotkeyItem
				{
					Header = globalHotkey.Key,
					HotKey = globalHotkey.Value
				});
			}
		}
	}
}
