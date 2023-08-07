
namespace ConsoleRPG.Items.Shields
{
    internal class SpikedShield : Shield
    {
        public SpikedShield(int level) : base(level)
        {
            Random rand = new Random();
            Quality Quality = new Quality();
            Name = "[bold]Шипастый[/] щит";
            int Chance = rand.Next(1, 101);

            ID = ItemIdentifier.SteelShield;

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
                AddComponent(new ArmorCharacteristic { Armor = rand.Next(2, 5) });
                RarityId = 1;
                Level = 2;
            }
            else if (Chance > 85 && Chance != 100)
            {
                string QualityName = Quality.GetGoodQuality();
                Name = QualityName + " " + Name;
                AddComponent(new ValueCharacteristic { Cost = rand.Next(9, 13) });
                AddComponent(new ArmorCharacteristic { Armor = rand.Next(3, 6) });
                AddComponent(new SpikeCharacteristic { SpikeDamage = rand.Next(1, 5) });
                RarityId = 2;
                Level = 3;
            }
            else if (Chance == 100)
            {
                string QualityName = Quality.GetBestQuality();
                Name = QualityName + " " + Name;
                AddComponent(new ValueCharacteristic { Cost = rand.Next(13, 22) });
                AddComponent(new ArmorCharacteristic { Armor = rand.Next(6, 11) });
                AddComponent(new SpikeCharacteristic { SpikeDamage = rand.Next(2, 8) });
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
