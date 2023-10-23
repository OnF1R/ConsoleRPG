using ConsoleRPG.Effects.Debuffs;
using ConsoleRPG.Effects;
using ConsoleRPG.Enums;
namespace ConsoleRPG.Items.Weapons.BaseEnemyWeapons
{
	internal class TeethBaseWeapon : Weapon
	{
		public TeethBaseWeapon(int level) : base(level)
		{
			Name = "Зубы";

			Type = ItemType.BaseWeapon;

			AddComponent(new StatusEffectsCharacteristic(new Dictionary<BaseEffect, double>()
				{
					{ new Bleed(), 5 },
				}));

			DropChance = -1f;

			IsStacable = false;
			IsEquapable = true;
			IsEquiped = false;
		}
	}
}
