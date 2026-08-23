using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LiteDB;
using VitureCommonLibrary;

namespace SpaceWalker.Database;

public class DbManager
{
	private SettingsData? settings;

	private LiteDatabase? liteDatabase;

	private ILiteCollection<SettingsData>? settingsCol;

	private static readonly Lazy<DbManager> instance = new Lazy<DbManager>(() => new DbManager());

	public SettingsData Settings
	{
		get
		{
			if (settings == null)
			{
				settings = settingsCol?.FindById("settings") ?? new SettingsData();
				MergeDefaultHotkeys(settings);
			}
			return settings;
		}
	}

	public static DbManager Instance => instance.Value;

	private static void MergeDefaultHotkeys(SettingsData data)
	{
		if (data.GlobalHotkeys == null)
		{
			Dictionary<string, string> dictionary2 = (data.GlobalHotkeys = new Dictionary<string, string>());
		}
		data.GlobalHotkeys.Remove("CalibrationHeader");
		foreach (KeyValuePair<string, string> globalHotkey in new SettingsData().GlobalHotkeys)
		{
			if (!data.GlobalHotkeys.ContainsKey(globalHotkey.Key) && !data.GlobalHotkeys.ContainsValue(globalHotkey.Value))
			{
				data.GlobalHotkeys[globalHotkey.Key] = globalHotkey.Value;
			}
		}
	}

	private DbManager()
	{
		InitDataBase();
	}

	~DbManager()
	{
		liteDatabase?.Dispose();
	}

	private void InitDataBase()
	{
		string text = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "VITURE", "SpaceWalker");
		if (!Directory.Exists(text))
		{
			Directory.CreateDirectory(text);
		}
		string connectionString = Path.Combine(text, "SpaceWalker.db");
		try
		{
			liteDatabase = new LiteDatabase(connectionString);
		}
		catch (Exception ex)
		{
			Logger.Error(ex.Message, ex.StackTrace);
		}
		if (liteDatabase != null)
		{
			settingsCol = liteDatabase?.GetCollection<SettingsData>("settings");
			settingsCol?.EnsureIndex((SettingsData x) => x.Id, unique: true);
			ILiteCollection<SettingsData>? liteCollection = settingsCol;
			if (liteCollection != null && liteCollection.FindAll().Count() == 0)
			{
				settingsCol?.Insert(new SettingsData());
			}
		}
	}

	public void SaveSettings()
	{
		if (settings != null)
		{
			settingsCol?.Update(settings);
		}
	}

	public void ResetSettings()
	{
		settings = new SettingsData();
		settingsCol?.Upsert(settings);
	}
}
