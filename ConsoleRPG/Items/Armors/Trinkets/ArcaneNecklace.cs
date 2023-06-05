
namespace ConsoleRPG.Items.Armors.Trinkets
{
    internal class ArcaneNecklace : Item
    {
        public ArcaneNecklace(int level) : base(level)
        {
            Random rand = new Random();
            Quality Quality = new Quality();
            Name = "Тайное ожерелье";
            int Chance = rand.Next(1, 101);

            ID = ItemIdentifier.ArcaneNecklace;

            if (Chance <= 10)
            {
                string QualityName = Quality.GetBadQuality();
                Name = QualityName + " " + Name;
                AddComponent(new ValueCharacteristic { Cost = 5 });
                RarityId = 1;
                Level = 1;
            }
            else if (Chance <= 85)
            {
                int intelligence = rand.Next(1, 5);
                AddComponent(new ValueCharacteristic { Cost = 6 });
                AddComponent(new IntelligenceCharacteristic { Intelligence = intelligence });
                RarityId = 2;
                Level = 1;
            }
            else if (Chance > 85 && Chance != 100)
            {
                string QualityName = Quality.GetGoodQuality();
                Name = QualityName + " " + Name;
                AddComponent(new ValueCharacteristic { Cost = 7 });
                int intelligence = rand.Next(3, 9);
                int resist = rand.Next(4, 12);
                AddComponent(new IntelligenceCharacteristic { Intelligence = intelligence });
                AddComponent(new ElementalResistanceCharacteristic
                {
                    ElemResistance = new Dictionary<DamageTypes, int>()
                    {
                        { DamageTypes.Arcane, resist},
                    }
                });
                RarityId = 2;
                Level = 2;
            }
            else if (Chance == 100)
            {
                string QualityName = Quality.GetBestQuality();
                Name = QualityName + " " + Name;
                AddComponent(new ValueCharacteristic { Cost = 8 });
                int intelligence = rand.Next(5, 16);
                int resist = rand.Next(8, 21);
                AddComponent(new IntelligenceCharacteristic { Intelligence = intelligence });
                AddComponent(new ElementalResistanceCharacteristic
                {
                    ElemResistance = new Dictionary<DamageTypes, int>()
                    {
                        { DamageTypes.Arcane, resist},
                    }
                });
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
