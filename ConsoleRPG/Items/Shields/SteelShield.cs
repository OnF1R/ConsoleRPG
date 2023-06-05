
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleRPG.Items.Shields
{
    internal class SteelShield : Weapon
    {
        public SteelShield(int level) : base(level)
        {
            Random rand = new Random();
            Quality Quality = new Quality();
            Name = "[grey]Стальной[/] щит";
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
                AddComponent(new ArmorCharacteristic { Armor = rand.Next(5, 8) });
                RarityId = 2;
                Level = 3;
            }
            else if (Chance == 100)
            {
                string QualityName = Quality.GetBestQuality();
                Name = QualityName + " " + Name;
                AddComponent(new ValueCharacteristic { Cost = rand.Next(13, 22) });
                AddComponent(new ArmorCharacteristic { Armor = rand.Next(8, 17) });
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
