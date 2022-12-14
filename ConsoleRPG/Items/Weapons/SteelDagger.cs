using ConsoleRPG.Items.ItemsComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleRPG.Items.Weapons
{
    internal class SteelDagger : Weapon
    {
        public SteelDagger()
        {
            Random rand = new Random();
            Quality Quality = new Quality();
            Name = "[grey]Стальной[/] кинжал";
            int Chance = rand.Next(1, 101);

            if (Chance <= 10)
            {
                string QualityName = Quality.GetBadQuality();
                Name = QualityName + " " + Name;
                AddComponent(new Valuable { Cost = rand.Next(1, 5) });
                AddComponent(new PhysicalDamage { Physical = rand.Next(1, 6) });
                AddComponent(new DamageType { Type = DamageTypes.Physical });
                RarityId = 0;
                Level = 1;
            }
            else if (Chance <= 85)
            {
                AddComponent(new Valuable { Cost = rand.Next(6, 9) });
                AddComponent(new PhysicalDamage { Physical = rand.Next(6, 11) });
                AddComponent(new DamageType { Type = DamageTypes.Physical });
                RarityId = 1;
                Level = 2;
            }
            else if (Chance > 85 || Chance != 100)
            {
                string QualityName = Quality.GetGoodQuality();
                Name = QualityName + " " + Name;
                AddComponent(new Valuable { Cost = rand.Next(9, 13) });
                AddComponent(new PhysicalDamage { Physical = rand.Next(11, 16) });
                AddComponent(new DamageType { Type = DamageTypes.Physical });
                RarityId = 2;
                Level = 3;
            }
            else if (Chance == 100)
            {
                string QualityName = Quality.GetBestQuality();
                Name = QualityName + " " + Name;
                AddComponent(new Valuable { Cost = rand.Next(13, 22) });
                AddComponent(new PhysicalDamage { Physical = rand.Next(14, 22) });
                AddComponent(new DamageType { Type = DamageTypes.Physical });
                RarityId = 3;
                Level = 4;
            }


            Type = ItemType.Dagger;

            DropChance = 17.5f;

            IsStacable = false;
            IsEquapable = true;
            IsEquiped = false;

            SetRarity(RarityId);
        }
    }
}
