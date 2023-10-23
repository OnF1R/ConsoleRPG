namespace ConsoleRPG.Enums
{
	[Flags]
	public enum QualityTypes
	{
		Broken = 1 << 0,
		Used = 1 << 1,
		Anchient = 1 << 2,
		Rust = 1 << 3,
		Bad = Broken | Used | Anchient | Rust,
		Encanted = 1 << 4,
		Mad = 1 << 5,
		Blessed = 1 << 6,
		Demonical = 1 << 7,
		Godlike = 1 << 8,
		Good = Mad | Blessed | Demonical | Godlike,
		GodKiller = 1 << 9,
		Antimaterial = 1 << 10,
		FromAbyss = 1 << 11,
		Best = GodKiller | Antimaterial | FromAbyss,
	}
}
