using ConsoleRPG.Effects.Debuffs;
using ConsoleRPG.Effects;
using ConsoleRPG.Enums;

namespace ConsoleRPG.Items.Weapons
{
    [Serializable]
    internal class MeatHook : Weapon
	{
		public MeatHook(int level) : base(level)
		{
			SerializableRandom rand = new SerializableRandom();

			Name = $"[bold red]Мясной[/] крюк";

			AddComponent(new PhysicalDamageCharacteristic { PhysicalDamage = rand.Next(Level, Level + Level) });

			AddComponent(new ValueCharacteristic { Cost = rand.Next(Level + 75, Level + Level + 300) });

			AddComponent(new VampirismCharacteristic { VampirismPercent = rand.Next(5, 16)});

			AddComponent(new StatusEffectsCharacteristic(new Dictionary<BaseEffect, double>()
				{
					{ new Bleed(), 15 },
				}));

			ID = ItemIdentifier.MeatHook;
			Type = ItemType.Axe;
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
