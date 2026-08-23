namespace VitureCommonLibrary;

public record struct DisplayTargetToken
{
	public ulong AdapterLuid;

	public uint TargetId;

	public DisplayTargetToken(ulong adapterId, uint targetId)
	{
		AdapterLuid = adapterId;
		TargetId = targetId;
	}

	public void Deconstruct(out ulong adapterId, out uint targetId)
	{
		adapterId = AdapterLuid;
		targetId = TargetId;
	}
}
