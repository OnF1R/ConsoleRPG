using ConsoleRPG.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleRPG.Items.Weapons
{
    [Serializable]
    internal class SteelAxe : Weapon
    {
        public SteelAxe(int level) : base(level)
        {
            SerializableRandom rand = new SerializableRandom();
            Quality Quality = new Quality();
            Name = "[grey]Стальной[/] топор";
            int Chance = rand.Next(1, 101);

            AddComponent(new PhysicalDamageCharacteristic { PhysicalDamage = rand.Next(Level, Level + Level) });
            AddComponent(new ValueCharacteristic { Cost = rand.Next(Level, Level + Level) });

            ID = ItemIdentifier.SteelAxe;

            if (Chance <= 10)
            {
                string QualityName = Quality.GetBadQuality();
                Name = QualityName + " " + Name;
                RarityId = 0;
            }
            else if (Chance <= 85)
            {
                GetComponent<PhysicalDamageCharacteristic>().PhysicalDamage += rand.Next(Level, Level + 2);
                GetComponent<ValueCharacteristic>().Cost += rand.Next(Level, Level + 2);
                RarityId = 1;
            }
            else if (Chance > 85 && Chance != 100)
            {
                string QualityName = Quality.GetGoodQuality();
                Name = QualityName + " " + Name;
                GetComponent<PhysicalDamageCharacteristic>().PhysicalDamage += rand.Next(Level, Level + Level / 2);
                GetComponent<ValueCharacteristic>().Cost += rand.Next(Level, Level + Level / 2);
                RarityId = 2;
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

            DropChance = 13.5f;

            IsStacable = false;
            IsEquapable = true;
            IsEquiped = false;

            SetRarity(RarityId);
        }
    }
}

