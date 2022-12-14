using ConsoleRPG.Items.ItemsComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG.Items.Weapons
{
    internal class BloodLetter  : Weapon
    {
        public BloodLetter()
        {
            Random rand = new Random();
            Quality Quality = new Quality();
            Name = "[red]Кровопускатель[/]";
            int Chance = rand.Next(1, 101);

            if (Chance <= 10)
            {
                string QualityName = Quality.GetBadQuality();
                Name = QualityName + " " + Name;
                AddComponent(new Valuable { Cost = rand.Next(1, 6) });
                AddComponent(new PhysicalDamage { Physical = rand.Next(1, 7) });
                AddComponent(new DamageType { Type = DamageTypes.Physical });
                RarityId = 0;
                Level = 1;
            }
            else if (Chance <= 85)
            {
                AddComponent(new Valuable { Cost = rand.Next(4, 9) });
                AddComponent(new PhysicalDamage { Physical = rand.Next(5, 10) });
                AddComponent(new DamageType { Type = DamageTypes.Physical });
                
                RarityId = 0;
                Level = 2;
            }
            else if (Chance > 85 || Chance != 100)
            {
                string QualityName = Quality.GetGoodQuality();
                Name = QualityName + " " + Name;
                AddComponent(new Valuable { Cost = rand.Next(8, 14) });
                AddComponent(new PhysicalDamage { Physical = rand.Next(7, 13) });
                AddComponent(new DamageType { Type = DamageTypes.Physical });
                AddComponent(new Criticals { CritChance = rand.Next(3,6), CritDamage = rand.Next(33,51) });
                RarityId = 1;
                Level = 2;
            }
            else if (Chance == 100)
            {
                string QualityName = Quality.GetBestQuality();
                Name = QualityName + " " + Name;
                AddComponent(new Valuable { Cost = rand.Next(8, 14) });
                AddComponent(new PhysicalDamage { Physical = rand.Next(10, 21) });
                AddComponent(new DamageType { Type = DamageTypes.Physical });
                AddComponent(new Criticals { CritChance = rand.Next(5,8), CritDamage = rand.Next(45, 88) });
                RarityId = 2;
                Level = 3;
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

