﻿
using ConsoleRPG.Enums;

namespace ConsoleRPG.Items.Shields
{
    [Serializable]
    internal class RunicShield : Shield
    {
        public RunicShield(int level) : base(level)
        {
            SerializableRandom rand = new SerializableRandom();
            Quality Quality = new Quality();
            Name = "[lightslateblue]Рунический[/] щит";
            int Chance = rand.Next(1, 101);

            ID = ItemIdentifier.RunicShield;

            if (Chance <= 10)
            {
                string QualityName = Quality.GetBadQuality();
                Name = QualityName + " " + Name;
                AddComponent(new ValueCharacteristic { Cost = rand.Next(1, 5) });
                AddComponent(new ArmorCharacteristic { Armor = rand.Next(1, 3) });
                RarityId = 0;
                Level = 1;
            }
            else if (Chance <= 85)
            {
                AddComponent(new ValueCharacteristic { Cost = rand.Next(6, 9) });
                AddComponent(new ArmorCharacteristic { Armor = rand.Next(2, 4) });
                RarityId = 1;
                Level = 2;
            }
            else if (Chance > 85 && Chance != 100)
            {
                string QualityName = Quality.GetGoodQuality();
                Name = QualityName + " " + Name;
                AddComponent(new ValueCharacteristic { Cost = rand.Next(9, 13) });
                AddComponent(new ArmorCharacteristic { Armor = rand.Next(3, 6) });
                AddRandomElementalResist(this, rand.Next(5, 11));
                RarityId = 2;
                Level = 3;
            }
            else if (Chance == 100)
            {
                string QualityName = Quality.GetBestQuality();
                Name = QualityName + " " + Name;
                AddComponent(new ValueCharacteristic { Cost = rand.Next(13, 22) });
                AddComponent(new ArmorCharacteristic { Armor = rand.Next(5, 11) });
                AddRandomElementalResist(this, rand.Next(5, 11));
                AddRandomElementalResist(this, rand.Next(5, 11));
                RarityId = 3;
                Level = 4;
            }


            Type = ItemType.Shield;

            DropChance = 12.5f;

            IsStacable = false;
            IsEquapable = true;
            IsEquiped = false;

            SetRarity(RarityId);
        }
    }
}
