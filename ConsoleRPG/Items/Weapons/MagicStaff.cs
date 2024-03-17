using ConsoleRPG.Enums;

namespace ConsoleRPG.Items.Weapons
{
    [Serializable]
    internal class MagicStaff : Weapon
	{
		public MagicStaff(int level) : base(level)
		{
			SerializableRandom rand = new SerializableRandom();
			Quality Quality = new Quality();

			int Chance = rand.Next(1, 101);

			DamageTypesNames damageTypesNames = new DamageTypesNames();
			DamageTypes damageType = damageTypesNames.GetRandomElementalDamageType();
			string damageTypeColor = damageTypesNames.Color[damageType];

			Name = $"[{damageTypeColor}]Магический[/] посох";

			AddComponent(new ElementalDamageCharacteristic(new Dictionary<DamageTypes, int>()
			{
				{ damageType, rand.Next(Level, Level + Level)},
			}));

			AddComponent(new ValueCharacteristic { Cost = rand.Next(Level, Level + Level) });

			ID = ItemIdentifier.MagicStaff;

			if (Chance <= 5)
			{
				string QualityName = Quality.GetBadQuality();
				Name = QualityName + " " + Name;
				int resist = rand.Next(-12, 2);
				AddNeededElementalResistance(this, damageType, resist, resist);
				RarityId = 0;
			}
			else if (Chance <= 10)
			{
				string QualityName = Quality.GetBadQuality();
				Name = QualityName + " " + Name;
				int resist = rand.Next(-12, 2);
				AddNeededElementalResistance(this, damageType, resist, resist);
				RarityId = 0;
			}
			else if (Chance <= 85)
			{
				int resist = rand.Next(-6, 6);
				AddNeededElementalResistance(this, damageType, resist, resist);

				GetComponent<ValueCharacteristic>().Cost += rand.Next(Level, Level + 2);
				RarityId = 1;
			}
			else if (Chance > 85 && Chance != 100)
			{
				string QualityName = Quality.GetGoodQuality();
				Name = QualityName + " " + Name;
				int resist = rand.Next(1, 11);
				AddNeededElementalResistance(this, damageType, resist, resist);

				GetComponent<ValueCharacteristic>().Cost += rand.Next(Level, Level + Level / 2);
			}
			else if (Chance == 100)
			{
				string QualityName = Quality.GetBestQuality();
				Name = QualityName + " " + Name;
				int resist = rand.Next(3, 15);
				AddNeededElementalResistance(this, damageType, resist, resist);
				GetComponent<ValueCharacteristic>().Cost += rand.Next(Level, Level + Level);

				RarityId = 3;
			}


			Type = ItemType.Staff;

			DropChance = 5f;

			IsStacable = false;
			IsEquapable = true;
			IsEquiped = false;

			SetRarity(RarityId);
		}
	}
}
