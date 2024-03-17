
using ConsoleRPG.Enums;

namespace ConsoleRPG.Items.Armors.Boots
{
    [Serializable]
    internal class DragonClawBoots : Armor
    {
        public DragonClawBoots(int level) : base(level)
        {
            SerializableRandom rand = new SerializableRandom();
            Quality Quality = new Quality();
            Name = "[bold]Драконьи зубы[/]";
            int Chance = rand.Next(1, 101);

            ID = ItemIdentifier.SpikedSandals;

            if (Chance <= 10)
            {
                string QualityName = Quality.GetBadQuality();
                Name = QualityName + " " + Name;
                AddComponent(new ValueCharacteristic { Cost = 4 });
                int resist = rand.Next(1, 4);
                AddComponent(new ArmorCharacteristic { Armor = resist });
                AddComponent(new SpikeCharacteristic { SpikeDamage = 1 });
                RarityId = 0;
                Level = 1;
            }
            else if (Chance <= 85)
            {
                int resist = rand.Next(4, 7);
                AddComponent(new ValueCharacteristic { Cost = 8 });
                AddComponent(new ArmorCharacteristic { Armor = resist });
                AddComponent(new SpikeCharacteristic { SpikeDamage = 3 });
                RarityId = 1;
                Level = 1;
            }
            else if (Chance > 85 && Chance != 100)
            {
                string QualityName = Quality.GetGoodQuality();
                Name = QualityName + " " + Name;
                AddComponent(new ValueCharacteristic { Cost = 12 });
                int resist = rand.Next(6, 9);
                AddComponent(new ArmorCharacteristic { Armor = resist });
                AddComponent(new SpikeCharacteristic { SpikeDamage = 5 });
                RarityId = 1;
                Level = 2;
            }
            else if (Chance == 100)
            {
                string QualityName = Quality.GetBestQuality();
                Name = QualityName + " " + Name;
                AddComponent(new ValueCharacteristic { Cost = 25 });
                int resist = rand.Next(8, 12);
                AddComponent(new ArmorCharacteristic { Armor = resist });
                AddComponent(new SpikeCharacteristic { SpikeDamage = 7 });
                RarityId = 3;
                Level = 2;
            }


            Type = ItemType.Boots;

            DropChance = 0f;

            IsStacable = false;
            IsEquapable = true;
            IsEquiped = false;

            SetRarity(RarityId);
        }
    }
}
