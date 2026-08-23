using SpaceWalker.ViewModels;

namespace SpaceWalker;

public class LayoutTypeOption
{
	public string Header { get; }

	public LayoutType Type { get; }

	public LayoutTypeOption(string header, LayoutType type)
	{
		Header = header;
		Type = type;
	}
}
