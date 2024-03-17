
using ConsoleRPG.Enums;

namespace ConsoleRPG.Items.Armors.Trinkets
{
    [Serializable]
    internal class SwormTeethNecklace : Item
    {
        public SwormTeethNecklace(int level) : base(level)
        {
            SerializableRandom rand = new SerializableRandom();
            Quality Quality = new Quality();
            Name = "[bold]Колье из зубов червей[/]";
            int Chance = rand.Next(1, 101);

            ID = ItemIdentifier.SwormTeethNecklace;

            if (Chance <= 10)
            {
                string QualityName = Quality.GetBadQuality();
                Name = QualityName + " " + Name;
                AddComponent(new ValueCharacteristic { Cost = 7 });
                AddComponent(new SpikeCharacteristic { SpikeDamage = 3 });
                RarityId = 1;
                Level = 1;
            }
            else if (Chance <= 85)
            {
                int intelligence = rand.Next(1, 5);
                AddComponent(new ValueCharacteristic { Cost = 12 });
				AddComponent(new CriticalDamageCharacteristic { CriticalDamage = rand.Next(10, 21) });
				RarityId = 2;
                Level = 1;
            }
            else if (Chance > 85 && Chance != 100)
            {
                string QualityName = Quality.GetGoodQuality();
                Name = QualityName + " " + Name;
                AddComponent(new ValueCharacteristic { Cost = 15 });
				AddComponent(new CriticalDamageCharacteristic { CriticalDamage = rand.Next(15, 31) });
				RarityId = 3;
                Level = 2;
            }
            else if (Chance == 100)
            {
                string QualityName = Quality.GetBestQuality();
                Name = QualityName + " " + Name;
                AddComponent(new ValueCharacteristic { Cost = 25 });
				AddComponent(new CriticalChanceCharacteristic { CriticalChance = rand.Next(5, 8) });
				AddComponent(new CriticalDamageCharacteristic { CriticalDamage = rand.Next(25, 41) });
				RarityId = 4;
                Level = 2;
            }

            Type = ItemType.Trinket;

            DropChance = 0f;

            IsStacable = false;
            IsEquapable = true;
            IsEquiped = false;

            SetRarity(RarityId);
        }
    }
}
