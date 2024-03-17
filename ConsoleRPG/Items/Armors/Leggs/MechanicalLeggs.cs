using ConsoleRPG.Enums;

namespace ConsoleRPG.Items.Armors.Leggs
{
    [Serializable]
    internal class MechanicalLeggs : Armor
    {
        public MechanicalLeggs(int level) : base(level)
        {
            SerializableRandom rand = new SerializableRandom();
            Quality Quality = new Quality();
            Name = "[bold]Механические ноги[/]";
            int Chance = rand.Next(1, 101);
            ID = ItemIdentifier.MechanicalLeggs;

            if (Chance <= 10)
            {
                string QualityName = Quality.GetBadQuality();
                Name = QualityName + " " + Name;
                int resist = rand.Next(1, 5);
                AddRandomElementalResist(this, resist);
                AddComponent(new ValueCharacteristic { Cost = 3 });
                RarityId = 1;
            }
            else if (Chance <= 85)
            {
                int resist = rand.Next(2, 8);
                AddRandomElementalResist(this, resist);
                AddComponent(new ValueCharacteristic { Cost = 4 });
                AddComponent(new ArmorCharacteristic { Armor = 2 });
                RarityId = 2;
            }
            else if (Chance > 85 && Chance != 100)
            {
                string QualityName = Quality.GetGoodQuality();
                Name = QualityName + " " + Name;
                AddComponent(new ValueCharacteristic { Cost = 5 });
                AddRandomElementalResist(this, rand.Next(3, 11));
                AddRandomElementalResist(this, rand.Next(3, 11));
                AddRandomElementalResist(this, rand.Next(3, 11));
                AddRandomElementalResist(this, rand.Next(3, 11));
                AddComponent(new ArmorCharacteristic { Armor = rand.Next(1, 5) });
                RarityId = 3;
            }
            else if (Chance == 100)
            {
                string QualityName = Quality.GetBestQuality();
                Name = QualityName + " " + Name;
                AddComponent(new ValueCharacteristic { Cost = 7 });
                AddRandomElementalResist(this, rand.Next(3, 11));
                AddRandomElementalResist(this, rand.Next(3, 11));
                AddRandomElementalResist(this, rand.Next(3, 11));
                AddRandomElementalResist(this, rand.Next(3, 11));
                AddRandomElementalResist(this, rand.Next(3, 11));
                AddRandomElementalResist(this, rand.Next(3, 11));
                AddComponent(new ArmorCharacteristic { Armor = rand.Next(3, 9) });
                RarityId = 4;
            }


            Type = ItemType.Leggs;

            DropChance = 9.5f;

            IsStacable = false;
            IsEquapable = true;
            IsEquiped = false;

            SetRarity(RarityId);
        }
    }
}
