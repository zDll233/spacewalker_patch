namespace VitureCommonLibrary;

public sealed class DisplayInfo
{
	public string DisplayName { get; set; } = string.Empty;


	public string DeviceName => DisplayName;

	public bool IsConnected { get; set; }

	public bool IsActive { get; set; }

	public bool IsGDIPrimary { get; set; }

	public bool IsAvailable => IsConnected;

	public DisplaySettingInfo CurrentSetting { get; set; } = new DisplaySettingInfo();

}
