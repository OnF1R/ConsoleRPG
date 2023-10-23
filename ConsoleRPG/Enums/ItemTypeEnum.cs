namespace ConsoleRPG.Enums
{
	[Flags]
	public enum ItemType
	{
		Sword = 1 << 0,
		TwoHandedSword = 1 << 1,
		Axe = 1 << 2,
		TwoHandedAxe = 1 << 3,
		Dagger = 1 << 4,
		Shield = 1 << 5,
		Staff = 1 << 6,
		TwoHandenStaff = 1 << 7,
		Bow = 1 << 8,

		Damaging = Sword | TwoHandedSword | Axe | TwoHandedAxe | Dagger | Staff | TwoHandenStaff | Bow,

		Helmet = 1 << 9,
		Chest = 1 << 10,
		Gloves = 1 << 11,
		Leggs = 1 << 12,
		Boots = 1 << 13,
		Ring = 1 << 14,
		Cape = 1 << 15,
		Trinket = 1 << 16,

		Armor = Helmet | Chest | Gloves | Leggs | Boots | Ring | Cape | Trinket,

		Currency = 1 << 17,
		Stacable = 1 << 18,

		BaseWeapon = 1 << 19,
	}
}
