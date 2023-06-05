
namespace ConsoleRPG.Items.Armors.Trinkets
{
    internal class EmelardNecklace : Item
    {
        public EmelardNecklace(int level) : base(level)
        {
            Random rand = new Random();
            Quality Quality = new Quality();
            Name = "Изумрудное ожерелье";
            int Chance = rand.Next(1, 101);

            ID = ItemIdentifier.EmeraldNecklace;

            if (Chance <= 10)
            {
                string QualityName = Quality.GetBadQuality();
                Name = QualityName + " " + Name;
                AddComponent(new ValueCharacteristic { Cost = 2 });
                int resist = rand.Next(1, 4);
                AddComponent(new ExperienceBooster { PercentBoost = 1 });
                RarityId = 0;
                Level = 1;
            }
            else if (Chance <= 85)
            {
                int resist = rand.Next(1, 3);
                AddComponent(new ValueCharacteristic { Cost = 6 });
                AddComponent(new ArmorCharacteristic { Armor = resist });
                AddComponent(new ExperienceBooster { PercentBoost = 3 });
                RarityId = 1;
                Level = 1;
            }
            else if (Chance > 85 && Chance != 100)
            {
                string QualityName = Quality.GetGoodQuality();
                Name = QualityName + " " + Name;
                AddComponent(new ValueCharacteristic { Cost = 12 });
                int resist = rand.Next(2, 5);
                AddComponent(new ArmorCharacteristic { Armor = resist });
                AddComponent(new ExperienceBooster { PercentBoost = 5 });
                RarityId = 1;
                Level = 2;
            }
            else if (Chance == 100)
            {
                string QualityName = Quality.GetBestQuality();
                Name = QualityName + " " + Name;
                AddComponent(new ValueCharacteristic { Cost = 18 });
                int resist = rand.Next(2, 8);
                int intelligence = rand.Next(1, 5);
                AddComponent(new ArmorCharacteristic { Armor = resist });
                AddComponent(new ExperienceBooster { PercentBoost = 7 });
                AddComponent(new IntelligenceCharacteristic { Intelligence = intelligence });
                RarityId = 3;
                Level = 2;
            }


            Type = ItemType.Trinket;

            DropChance = 7f;

            IsStacable = false;
            IsEquapable = true;
            IsEquiped = false;

            SetRarity(RarityId);
        }
    }
}
