using SpaceWalker.Ipc;
using VitureCommonLibrary;

namespace SpaceWalker;

public class ProcessParam
{
	public int FilmAngle { get; set; } = 30;


	public bool TurnOffScreen { get; set; } = true;


	public string GlassesModel { get; set; } = string.Empty;


	public LayoutMode LayoutMode { get; set; }

	public LockAxisState LockAxis { get; set; }

	public int Duty { get; set; } = 99;


	public string Skybox { get; set; } = string.Empty;


	public bool HandTrack { get; set; }

	public bool SmoothFollow { get; set; } = true;


	public override string ToString()
	{
		return $"-layoutMode {LayoutMode} -filmAngle {FilmAngle} -turnOffScreen {TurnOffScreen} -glassesModel {GlassesModel} -lockAxis {LockAxis} -duty {Duty} -skybox {Skybox} -handTrack {HandTrack} -smoothFollow {SmoothFollow}";
	}
}
