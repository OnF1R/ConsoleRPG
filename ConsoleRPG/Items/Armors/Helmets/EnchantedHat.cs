
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleRPG.Items.Armors.Helmets
{
    internal class EnchantedHat : Armor
    {
        public EnchantedHat()
        {
            Random rand = new Random();
            Quality Quality = new Quality();
            Name = "[purple4 slowblink]Зачарованная[/] шляпа";
            int Chance = rand.Next(1, 101);

            ID = ItemIdentifier.EnchantedHat;

            if (Chance <= 10)
            {
                string QualityName = Quality.GetBadQuality();
                Name = QualityName + " " + Name;
                int resist = rand.Next(-5, 5);
                if (resist == 0) if (rand.Next(0, 2) == 1) { resist++; } else { resist--; }
                AddRandomElementalResist(this, resist);
                AddComponent(new ValueCharacteristic { Cost = 1 });
                RarityId = 0;
                Level = 1;
            }
            else if (Chance <= 85)
            {
                int resist = rand.Next(-2, 8);
                if (resist == 0) if (rand.Next(0, 2) == 1) { resist++; } else { resist--; }
                AddRandomElementalResist(this, resist);
                AddComponent(new ValueCharacteristic { Cost = 2 });
                RarityId = 1;
                Level = 1;
            }
            else if (Chance > 85 && Chance != 100)
            {
                string QualityName = Quality.GetGoodQuality();
                Name = QualityName + " " + Name;
                AddComponent(new ValueCharacteristic { Cost = 5 });
                int resist = rand.Next(3, 11);
                if (resist == 0) if (rand.Next(0, 2) == 1) { resist++; } else { resist--; }
                AddRandomElementalResist(this, resist);
                AddComponent(new ArmorCharacteristic { Armor = rand.Next(1, 5) });
                RarityId = 1;
                Level = 2;
            }
            else if (Chance == 100)
            {
                string QualityName = Quality.GetBestQuality();
                Name = QualityName + " " + Name;
                AddComponent(new ValueCharacteristic { Cost = 7 });
                int resist = rand.Next(5, 16);
                if (resist == 0) if (rand.Next(0, 2) == 1) { resist++; } else { resist--; }
                AddRandomElementalResist(this, resist);
                AddRandomElementalResist(this, resist);
                AddComponent(new ArmorCharacteristic { Armor = rand.Next(3, 9) });
                RarityId = 3;
                Level = 2;
            }


            Type = ItemType.Helmet;

            DropChance = 13.5f;

            IsStacable = false;
            IsEquapable = true;
            IsEquiped = false;

            SetRarity(RarityId);
        }
    }
}
