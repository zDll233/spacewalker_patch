using System;
using System.Collections.Generic;
using LiteDB;

namespace SpaceWalker.Database;

public class SettingsData
{
	[BsonId]
	public string Id { get; set; } = "settings";


	public int LockAxis { get; set; }

	public int RefreshRate { get; set; } = 120;


	public int FilmControlAngle { get; set; } = 30;


	public bool EnableMouseShake { get; set; } = true;


	public bool EnableReduceMotionBlur { get; set; } = true;


	public bool EnableHighDPIScale { get; set; }

	public bool TurnOffBuildInScreen { get; set; }

	public bool SvsEnable { get; set; } = true;


	public bool SvsDebug { get; set; } = true;


	public string DialogShowedVersion { get; set; } = string.Empty;


	public DateTime TipsDate { get; set; } = DateTime.MinValue;


	public string Skybox { get; set; } = string.Empty;


	public string Theme { get; set; } = "Default";


	public bool HandTrack { get; set; } = true;


	public bool UseUltraWideSize { get; set; } = true;


	public bool SmoothFollow { get; set; } = true;


	public Dictionary<string, string> GlobalHotkeys { get; set; } = new Dictionary<string, string>
	{
		{ "UltraWideHeader", "Ctrl+Shift+Alt+D0" },
		{ "SingleMirroredHeader", "Ctrl+Shift+Alt+D1" },
		{ "TwoMirroredHeader", "Ctrl+Shift+Alt+D2" },
		{ "ThreeMirroredHeader", "Ctrl+Shift+Alt+D3" },
		{ "SingleExtentedHeader", "Ctrl+Shift+Alt+D4" },
		{ "TwoExtendedHeader", "Ctrl+Shift+Alt+D5" },
		{ "ThreeExtendedHeader", "Ctrl+Shift+Alt+D6" },
		{ "ThreeStackedHeader", "Ctrl+Shift+Alt+D7" },
		{ "PLPExtentedHeader", "Ctrl+Shift+Alt+D8" },
		{ "PLPMirroredHeader", "Ctrl+Shift+Alt+D9" },
		{ "ZoomInHeader", "Ctrl+Shift+Alt+Up" },
		{ "ZoomOutHeader", "Ctrl+Shift+Alt+Down" },
		{ "RecenterHeader", "Ctrl+Shift+Alt+R" },
		{ "LockXHeader", "Ctrl+Shift+Alt+X" },
		{ "LockYHeader", "Ctrl+Shift+Alt+Y" },
		{ "LockZHeader", "Ctrl+Shift+Alt+Z" },
		{ "ShowMainWindow", "Ctrl+Shift+Alt+S" },
		{ "QuitHeader", "Ctrl+Shift+Alt+Q" },
		{ "IncreaseVolume", "Ctrl+Shift+Alt+Right" },
		{ "DecreaseVolume", "Ctrl+Shift+Alt+Left" },
		{ "IncreaseBrightness", "Ctrl+Shift+Alt+OemPlus" },
		{ "DecreaseBrightness", "Ctrl+Shift+Alt+OemMinus" }
	};

}
