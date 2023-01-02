
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleRPG.Items.Weapons
{
    internal class SteelSword : Weapon
    {
        public SteelSword()
        {
            Random rand = new Random();
            Quality Quality = new Quality();
            Name = "[grey]Стальной[/] меч";
            int Chance = rand.Next(1, 101);

            if (Chance <= 10)
            {
                string QualityName = Quality.GetBadQuality();
                Name = QualityName + " " + Name;
                AddComponent(new ValueCharacteristic { Cost = rand.Next(1, 5) });
                AddComponent(new PhysicalDamageCharacteristic { PhysicalDamage = rand.Next(1, 6) });
                RarityId = 0;
                Level = 1;
            }
            else if (Chance <= 85)
            {
                AddComponent(new ValueCharacteristic { Cost = rand.Next(6, 9) });
                AddComponent(new PhysicalDamageCharacteristic { PhysicalDamage = rand.Next(6, 11) });
                RarityId = 1;
                Level = 2;
            }
            else if (Chance > 85 || Chance != 100)
            {
                string QualityName = Quality.GetGoodQuality();
                Name = QualityName + " " + Name;
                AddComponent(new ValueCharacteristic { Cost = rand.Next(9, 13) });
                AddComponent(new PhysicalDamageCharacteristic { PhysicalDamage = rand.Next(11, 16) });
                RarityId = 2;
                Level = 3;
            }
            else if (Chance == 100)
            {
                string QualityName = Quality.GetBestQuality();
                Name = QualityName + " " + Name;
                AddComponent(new ValueCharacteristic { Cost = rand.Next(13, 22) });
                AddComponent(new PhysicalDamageCharacteristic { PhysicalDamage = rand.Next(14, 22) });
                RarityId = 3;
                Level = 4;
            }


            Type = ItemType.Sword;

            DropChance = 13f;

            IsStacable = false;
            IsEquapable = true;
            IsEquiped = false;

            SetRarity(RarityId);
        }
    }
}
