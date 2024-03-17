
using ConsoleRPG.Enums;

namespace ConsoleRPG.Items.Armors.Gloves
{
    [Serializable]
    internal class CrafterGloves : Armor
    {
        public CrafterGloves(int level) : base(level)
        {
            SerializableRandom rand = new SerializableRandom();
            Quality Quality = new Quality();
            Name = "Перчатки ремесленника";
            int Chance = rand.Next(1, 101);

            ID = ItemIdentifier.CrafterGloves;

            if (Chance <= 10)
            {
                string QualityName = Quality.GetBadQuality();
                Name = QualityName + " " + Name;
                AddComponent(new ValueCharacteristic { Cost = 3 });
                int luck = rand.Next(-3, 3);
                AddComponent(new LuckCharacteristic { Luck = luck });
                AddComponent(new ArmorCharacteristic { Armor = rand.Next(1, 5) });
                RarityId = 0;
                Level = 1;
            }
            else if (Chance <= 85)
            {
                int luck = rand.Next(1, 5);
                AddComponent(new ValueCharacteristic { Cost = 5 });
                AddComponent(new LuckCharacteristic { Luck = luck });
                RarityId = 1;
                Level = 1;
            }
            else if (Chance > 85 && Chance != 100)
            {
                string QualityName = Quality.GetGoodQuality();
                Name = QualityName + " " + Name;
                AddComponent(new ValueCharacteristic { Cost = 7 });
                int luck = rand.Next(2, 8);
                AddComponent(new LuckCharacteristic { Luck = luck });
                RarityId = 1;
                Level = 2;
            }
            else if (Chance == 100)
            {
                string QualityName = Quality.GetBestQuality();
                Name = QualityName + " " + Name;
                AddComponent(new ValueCharacteristic { Cost = 12 });
                int luck = rand.Next(5, 11);
                AddComponent(new LuckCharacteristic { Luck = luck });
                RarityId = 2;
                Level = 2;
            }


            Type = ItemType.Gloves;

            DropChance = 9.5f;

            IsStacable = false;
            IsEquapable = true;
            IsEquiped = false;

            SetRarity(RarityId);
        }
    }
}
