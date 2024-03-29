﻿
using ConsoleRPG.Enums;

namespace ConsoleRPG.Items.Armors.Helmets
{
    [Serializable]
    internal class SteelHelmet : Armor
    {
        public SteelHelmet(int level) : base(level)
        {
            SerializableRandom rand = new SerializableRandom();
            Quality Quality = new Quality();
            Name = "Стальной шлем";
            int Chance = rand.Next(1, 101);

            ID = ItemIdentifier.SteelHelmet;

            if (Chance <= 10)
            {
                string QualityName = Quality.GetBadQuality();
                Name = QualityName + " " + Name;
                AddComponent(new ValueCharacteristic { Cost = 1 });
                int resist = rand.Next(1, 4);
                AddComponent(new ArmorCharacteristic { Armor = resist });
                RarityId = 0;
                Level = 1;
            }
            else if (Chance <= 85)
            {
                int resist = rand.Next(4, 7);
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
                int resist = rand.Next(6, 9);
                AddComponent(new ArmorCharacteristic { Armor = resist });
                RarityId = 1;
                Level = 2;
            }
            else if (Chance == 100)
            {
                string QualityName = Quality.GetBestQuality();
                Name = QualityName + " " + Name;
                AddComponent(new ValueCharacteristic { Cost = 7 });
                int resist = rand.Next(8, 12);
                AddComponent(new ArmorCharacteristic { Armor = resist });
                RarityId = 3;
                Level = 2;
            }


            Type = ItemType.Helmet;

            DropChance = 17.5f;

            IsStacable = false;
            IsEquapable = true;
            IsEquiped = false;

            SetRarity(RarityId);
        }
    }
}
