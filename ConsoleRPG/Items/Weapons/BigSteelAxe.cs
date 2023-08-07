
namespace ConsoleRPG.Items.Weapons
{
    internal class BigSteelAxe : Weapon
    {
        public BigSteelAxe(int level) : base(level)
        {
            Random rand = new Random();
            Quality Quality = new Quality();
            Name = "[bold]Большой[/] [grey]cтальной[/] топор";
            int Chance = rand.Next(1, 101);

            AddComponent(new PhysicalDamageCharacteristic { PhysicalDamage = rand.Next(Level, Level + Level) + 5 });
            AddComponent(new ValueCharacteristic { Cost = rand.Next(Level, Level + Level) });

            ID = ItemIdentifier.SteelAxe;

            if (Chance <= 10)
            {
                string QualityName = Quality.GetBadQuality();
                Name = QualityName + " " + Name;
                RarityId = 1;
            }
            else if (Chance <= 85)
            {
                GetComponent<PhysicalDamageCharacteristic>().PhysicalDamage += rand.Next(Level, Level + 2);
                GetComponent<ValueCharacteristic>().Cost += rand.Next(Level, Level + 2);
                RarityId = 2;
            }
            else if (Chance > 85 && Chance != 100)
            {
                string QualityName = Quality.GetGoodQuality();
                Name = QualityName + " " + Name;
                GetComponent<PhysicalDamageCharacteristic>().PhysicalDamage += rand.Next(Level, Level + Level / 2);
                GetComponent<ValueCharacteristic>().Cost += rand.Next(Level, Level + Level / 2);
                RarityId = 3;
            }
            else if (Chance == 100)
            {
                string QualityName = Quality.GetBestQuality();
                Name = QualityName + " " + Name;
                GetComponent<PhysicalDamageCharacteristic>().PhysicalDamage += rand.Next(Level, Level + Level);
                GetComponent<ValueCharacteristic>().Cost += rand.Next(Level, Level + Level);
                RarityId = 3;

            }


            Type = ItemType.Axe;

            DropChance = 4.5f;

            IsStacable = false;
            IsEquapable = true;
            IsEquiped = false;

            SetRarity(RarityId);
        }
    }
}

