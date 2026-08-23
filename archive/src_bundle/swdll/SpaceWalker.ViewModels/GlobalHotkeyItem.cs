using LiteDB;
using ReactiveUI;
using SpaceWalker.Assets.Languages;

namespace SpaceWalker.ViewModels;

public class GlobalHotkeyItem : ReactiveObject
{
	private string hotKey = string.Empty;

	public string Header { get; set; } = string.Empty;


	public string HotKey
	{
		get
		{
			return hotKey;
		}
		set
		{
			this.RaiseAndSetIfChanged(ref hotKey, value, "HotKey");
		}
	}

	[BsonIgnore]
	public string? Desc => Resources.ResourceManager.GetString(Header, Resources.Culture);
}
