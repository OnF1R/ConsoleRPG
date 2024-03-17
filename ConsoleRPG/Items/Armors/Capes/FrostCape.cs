
using ConsoleRPG.Enums;

namespace ConsoleRPG.Items.Armors.Capes
{
    [Serializable]
    internal class FrostCape : Armor
	{
		public FrostCape(int level) : base(level)
		{
			SerializableRandom rand = new SerializableRandom();
			Quality Quality = new Quality();
			Name = "[aqua]Морозный[/] плащ";
			int Chance = rand.Next(1, 101);

			ID = ItemIdentifier.FrostCape;

			if (Chance <= 10)
			{
				string QualityName = Quality.GetBadQuality();
				Name = QualityName + " " + Name;
				AddComponent(new ValueCharacteristic { Cost = rand.Next(1, 5) });
				int resist = rand.Next(1, 3);
				AddNeededElementalResistance(this, DamageTypes.Frost, resist, resist);
				Level = 1;
			}
			else if (Chance <= 85)
			{
				int resist = rand.Next(2, 5);
				AddComponent(new ValueCharacteristic { Cost = rand.Next(2, 9) });
				AddNeededElementalResistance(this, DamageTypes.Frost, resist, resist);
				RarityId = 2;
				Level = 1;
			}
			else if (Chance > 85 && Chance != 100)
			{
				string QualityName = Quality.GetGoodQuality();
				Name = QualityName + " " + Name;
				AddComponent(new ValueCharacteristic { Cost = rand.Next(5, 16) });
				int resist = rand.Next(2, 8);
				int damage = rand.Next(1, 5);
				AddNeededElementalTypesDamage(this, DamageTypes.Frost, damage, damage);
				AddNeededElementalResistance(this, DamageTypes.Frost, resist, resist);
				RarityId = 2;
				Level = 2;
			}
			else if (Chance == 100)
			{
				string QualityName = Quality.GetBestQuality();
				Name = QualityName + " " + Name;
				AddComponent(new ValueCharacteristic { Cost = rand.Next(7, 19) });
				int resist = rand.Next(5, 11);
				int damage = rand.Next(3, 11);
				AddNeededElementalTypesDamage(this, DamageTypes.Frost, damage, damage);
				AddNeededElementalResistance(this, DamageTypes.Frost, resist, resist);
				RarityId = 3;
				Level = 2;
			}


			Type = ItemType.Cape;

			DropChance = 6.5f;

			IsStacable = false;
			IsEquapable = true;
			IsEquiped = false;

			SetRarity(RarityId);
		}
	}
}
