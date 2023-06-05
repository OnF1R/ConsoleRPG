
namespace ConsoleRPG.Items.Armors.Trinkets
{
    internal class RubyNecklace : Item
    {
        public RubyNecklace(int level) : base(level)
        {
            Random rand = new Random();
            Quality Quality = new Quality();
            Name = "Рубиновое ожерелье";
            int Chance = rand.Next(1, 101);

            ID = ItemIdentifier.RubyNecklace;

            if (Chance <= 10)
            {
                string QualityName = Quality.GetBadQuality();
                Name = QualityName + " " + Name;
                AddComponent(new ValueCharacteristic { Cost = 15 });
                RarityId = 1;
                Level = 1;
            }
            else if (Chance <= 85)
            {
                int intelligence = rand.Next(1, 5);
                AddComponent(new ValueCharacteristic { Cost = 25 });
                RarityId = 2;
                Level = 1;
            }
            else if (Chance > 85 && Chance != 100)
            {
                string QualityName = Quality.GetGoodQuality();
                Name = QualityName + " " + Name;
                AddComponent(new ValueCharacteristic { Cost = 35 });
                RarityId = 3;
                Level = 2;
            }
            else if (Chance == 100)
            {
                string QualityName = Quality.GetBestQuality();
                Name = QualityName + " " + Name;
                AddComponent(new ValueCharacteristic { Cost = 100 });
                RarityId = 4;
                Level = 2;
            }

            Type = ItemType.Trinket;

            DropChance = 5f;

            IsStacable = false;
            IsEquapable = true;
            IsEquiped = false;

            SetRarity(RarityId);
        }
    }
}
