using System.Collections.Generic;
using SpaceWalker.ViewModels;
using VitureCommonLibrary;

namespace SpaceWalker;

public class LayoutModeOption
{
	public string Header { get; }

	public VitureLayoutMode Mode { get; }

	public IReadOnlyList<LayoutType> SupportedTypes { get; }

	public LayoutModeOption(string header, VitureLayoutMode mode, IReadOnlyList<LayoutType> supportedTypes)
	{
		Header = header;
		Mode = mode;
		SupportedTypes = supportedTypes;
	}
}
