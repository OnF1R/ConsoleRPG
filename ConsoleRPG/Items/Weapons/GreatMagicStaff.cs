using ConsoleRPG.Enums;

namespace ConsoleRPG.Items.Weapons
{
	internal class GreatMagicStaff : Weapon
	{
		public GreatMagicStaff(int level) : base(level)
		{
			Random rand = new Random();

			Name = $"[bold]Большой[/] магический посох";

			AddComponent(new PhysicalDamageCharacteristic { PhysicalDamage = rand.Next(Level, Level + Level) });

			AddComponent(new ValueCharacteristic { Cost = rand.Next(Level + 75, Level + Level + 300) });

			AddComponent(new MagicAmplificationCharacteristic { Amplification = rand.Next(33, 101)});

			ID = ItemIdentifier.GreatMagicStaff;
			Type = ItemType.Staff;
			Level = 5;
			RarityId = 6;

			DropChance = 100f;

			IsStacable = false;
			IsEquapable = true;
			IsEquiped = false;

			SetRarity(RarityId);
		}
	}
}
