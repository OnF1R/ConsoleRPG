
using ConsoleRPG.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG.Items.Weapons
{
    internal class BloodLetter : Weapon
    {
        public BloodLetter(int level) : base(level)
        {
            Random rand = new Random();
            Quality Quality = new Quality();
            Name = "[red]Кровопускатель[/]";
            int Chance = rand.Next(1, 101);

            AddComponent(new PhysicalDamageCharacteristic { PhysicalDamage = rand.Next(Level, Level + Level) });
            AddComponent(new ValueCharacteristic { Cost = rand.Next(Level, Level + Level) });

            ID = ItemIdentifier.BloodLetter;

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
                RarityId = 0;
            }
            else if (Chance > 85 && Chance != 100)
            {
                string QualityName = Quality.GetGoodQuality();
                Name = QualityName + " " + Name;
                GetComponent<PhysicalDamageCharacteristic>().PhysicalDamage += rand.Next(Level, Level + Level / 2);
                GetComponent<ValueCharacteristic>().Cost += rand.Next(Level, Level + Level / 2);
                AddComponent(new CriticalChanceCharacteristic { CriticalChance = rand.Next(3, 6) });
                AddComponent(new CriticalDamageCharacteristic { CriticalDamage = rand.Next(33, 51) });
                RarityId = 1;
            }
            else if (Chance == 100)
            {
                string QualityName = Quality.GetBestQuality();
                Name = QualityName + " " + Name;
                GetComponent<PhysicalDamageCharacteristic>().PhysicalDamage += rand.Next(Level, Level + Level);
                GetComponent<ValueCharacteristic>().Cost += rand.Next(Level, Level + Level);
                AddComponent(new CriticalChanceCharacteristic { CriticalChance = rand.Next(5, 8) });
                AddComponent(new CriticalDamageCharacteristic { CriticalDamage = rand.Next(45, 88) });
                RarityId = 2;
            }

            Type = ItemType.Sword;

            DropChance = 10f;

            IsStacable = false;
            IsEquapable = true;
            IsEquiped = false;

            SetRarity(RarityId);
        }
    }
}

