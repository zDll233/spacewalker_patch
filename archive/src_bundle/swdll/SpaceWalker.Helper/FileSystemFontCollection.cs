using System;
using System.Collections.Generic;
using System.IO;
using Avalonia.Media;
using Avalonia.Media.Fonts;

namespace SpaceWalker.Helper;

internal sealed class FileSystemFontCollection : FontCollectionBase
{
	private readonly Uri _key;

	public override Uri Key => _key;

	public FileSystemFontCollection(Uri key, IEnumerable<string> ttfFiles)
	{
		_key = key;
		foreach (string ttfFile in ttfFiles)
		{
			try
			{
				using FileStream stream = File.OpenRead(ttfFile);
				TryAddGlyphTypeface(stream, out GlyphTypeface _);
			}
			catch
			{
			}
		}
	}
}
