
using ConsoleRPG.Effects.Debuffs;
using ConsoleRPG.Enums;

namespace ConsoleRPG.Items.Armors.Capes
{
	internal class WetCape : Armor
	{
		public WetCape(int level) : base(level)
		{
			Random rand = new Random();
			Quality Quality = new Quality();
			Name = "[aqua]Промокший[/] плащ";
			int Chance = rand.Next(1, 101);

			ID = ItemIdentifier.WetCape;

			if (Chance <= 10)
			{
				string QualityName = Quality.GetBadQuality();
				Name = QualityName + " " + Name;
				AddComponent(new ValueCharacteristic { Cost = 1 });
				int resist = rand.Next(1, 3);
				AddComponent(new ArmorCharacteristic { Armor = resist });
				RarityId = 0;
				Level = 1;
			}
			else if (Chance <= 85)
			{
				int resist = rand.Next(2, 5);
				AddComponent(new ValueCharacteristic { Cost = 2 });
				AddComponent(new ArmorCharacteristic { Armor = resist });
				RarityId = 1;
				Level = 1;
			}
			else if (Chance > 85 && Chance != 100)
			{
				string QualityName = Quality.GetGoodQuality();
				Name = QualityName + " " + Name;
				AddComponent(new ValueCharacteristic { Cost = 5 });
				int resist = rand.Next(2, 8);
				int elemResist = rand.Next(2, 8);
				AddComponent(new ArmorCharacteristic { Armor = resist });
				AddComponent(new ElementalResistanceCharacteristic(new Dictionary<DamageTypes, int>() { { DamageTypes.Water, elemResist } }));
				RarityId = 1;
				Level = 2;
			}
			else if (Chance == 100)
			{
				string QualityName = Quality.GetBestQuality();
				Name = QualityName + " " + Name;
				AddComponent(new ValueCharacteristic { Cost = 7 });
				int resist = rand.Next(5, 11);
				int elemResist = rand.Next(5, 11);
				AddComponent(new ArmorCharacteristic { Armor = resist });
				AddComponent(new ElementalResistanceCharacteristic(new Dictionary<DamageTypes, int>() { { DamageTypes.Water, elemResist } }));
				RarityId = 2;
				Level = 2;
			}


			Type = ItemType.Cape;

			DropChance = 11.5f;

			IsStacable = false;
			IsEquapable = true;
			IsEquiped = false;

			SetRarity(RarityId);
		}
	}
}
