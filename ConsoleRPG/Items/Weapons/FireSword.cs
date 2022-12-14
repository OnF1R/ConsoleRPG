using ConsoleRPG.Items.ItemsComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG.Items.Weapons
{
    internal class FireSword : Weapon
    {
        public FireSword()
        {
            Random rand = new Random();
            Quality Quality = new Quality();
            Name = "[orangered1]Огненный[/] меч";
            int Chance = rand.Next(1, 101);

            if (Chance <= 5)
            {
                string QualityName = Quality.GetBadQuality();
                Name = QualityName + " " + Name;
                AddComponent(new Valuable { Cost = rand.Next(5, 12) });
                int resist = rand.Next(-12, 2);
                if (resist != 0) AddComponent(new ElementalResistance { Fire = resist });
                AddComponent(new PhysicalDamage { Physical = rand.Next(1, 3) });
                AddComponent(new DamageType { Type = DamageTypes.Physical });
                RarityId = 0;
                Level = 1;
            }
            else if (Chance <= 10)
            {
                string QualityName = Quality.GetBadQuality();
                Name = QualityName + " " + Name;
                AddComponent(new Valuable { Cost = rand.Next(5, 12) });
                AddComponent(new ElementalDamage { Fire = rand.Next(1, 3) });
                int resist = rand.Next(-12, 2);
                if (resist != 0) AddComponent(new ElementalResistance { Fire = resist });
                AddComponent(new DamageType { Type = DamageTypes.Fire });
                RarityId = 0;
                Level = 1;
            }
            else if (Chance <= 85)
            {
                AddComponent(new Valuable { Cost = rand.Next(12, 25) });
                AddComponent(new ElementalDamage { Fire = rand.Next(3, 8) });
                int resist = rand.Next(-6, 6);
                if (resist != 0) AddComponent(new ElementalResistance { Fire = resist });
                AddComponent(new PhysicalDamage { Physical = rand.Next(3, 11) });
                AddComponent(new DamageType { Type = DamageTypes.Burst });
                RarityId = 1;
                Level = 2;
            }
            else if(Chance > 85 || Chance != 100)
            {
                string QualityName = Quality.GetGoodQuality();
                Name = QualityName + " " + Name;
                AddComponent(new Valuable { Cost = rand.Next(20, 35) });
                AddComponent(new ElementalDamage { Fire = rand.Next(8, 14) });
                int resist = rand.Next(0, 8);
                if (resist != 0) AddComponent(new ElementalResistance { Fire = resist });
                AddComponent(new PhysicalDamage { Physical = rand.Next(11, 16) });
                AddComponent(new DamageType { Type = DamageTypes.Burst });
                RarityId = 2;
                Level = 3;
            }
            else if(Chance == 100)
            {
                string QualityName = Quality.GetBestQuality();
                Name = QualityName + " " + Name;
                AddComponent(new Valuable { Cost = rand.Next(27, 50) });
                AddComponent(new ElementalDamage { Fire = rand.Next(12, 20) });
                int resist = rand.Next(0, 12);
                if (resist != 0) AddComponent(new ElementalResistance { Fire = resist });
                AddComponent(new PhysicalDamage { Physical = rand.Next(14, 22) });
                AddComponent(new DamageType { Type = DamageTypes.Burst });
                RarityId = 3;
                Level = 4;
            }


            Type = ItemType.Sword;
            
            DropChance = 5f;

            IsStacable = false;
            IsEquapable = true;
            IsEquiped = false;

            SetRarity(RarityId);
        }
    }
}
