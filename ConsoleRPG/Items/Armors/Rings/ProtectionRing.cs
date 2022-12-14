﻿using ConsoleRPG.Items.ItemsComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG.Items.Armors.Rings
{
    internal class ProtectionRing : Armor
    {
        public ProtectionRing()
        {
            Random rand = new Random();
            Quality Quality = new Quality();
            Name = "Кольцо защиты";
            int Chance = rand.Next(1, 101);

            if (Chance <= 10)
            {
                string QualityName = Quality.GetBadQuality();
                Name = QualityName + " " + Name;
                AddComponent(new Valuable { Cost = 3 });
                AddComponent(new Defence { ArmorPoints = rand.Next(1, 3) });
                RarityId = 0;
                Level = 1;
            }
            else if (Chance <= 85)
            {
                AddComponent(new Valuable { Cost = 7 });
                AddComponent(new Defence { ArmorPoints = rand.Next(2, 6) });
                RarityId = 1;
                Level = 1;
            }
            else if (Chance > 85 && Chance != 100)
            {
                string QualityName = Quality.GetGoodQuality();
                Name = QualityName + " " + Name;
                AddComponent(new Valuable { Cost = 9 });
                AddComponent(new Defence { ArmorPoints = rand.Next(5, 10) });
                RarityId = 1;
                Level = 2;
            }
            else if (Chance == 100)
            {
                string QualityName = Quality.GetBestQuality();
                Name = QualityName + " " + Name;
                AddComponent(new Valuable { Cost = 14 });
                AddComponent(new Defence { ArmorPoints = rand.Next(11, 16) });
                RarityId = 2;
                Level = 2;
            }


            Type = ItemType.Ring;

            DropChance = 5.5f;

            IsStacable = false;
            IsEquapable = true;
            IsEquiped = false;

            SetRarity(RarityId);
        }
    }
}
