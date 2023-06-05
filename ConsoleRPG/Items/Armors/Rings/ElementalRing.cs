using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG.Items.Armors.Rings
{
    internal class ElementalRing : Armor
    {
        public ElementalRing(int level) : base(level)
        {
            Random rand = new Random();
            Quality Quality = new Quality();
            
            int Chance = rand.Next(1, 101);
            DamageTypesNames damageTypesNames = new DamageTypesNames();
            Name = $"[bold]Элементальное[/] кольцо";

            ID = ItemIdentifier.ElementalRing;

            if (Chance <= 10)
            {
                string QualityName = Quality.GetBadQuality();
                Name = QualityName + " " + Name;
                AddComponent(new ValueCharacteristic { Cost = 3 });
                AddRandomElementalResist(this, rand.Next(1, 3));
                RarityId = 0;
                Level = 1;
            }
            else if (Chance <= 85)
            {
                AddComponent(new ValueCharacteristic { Cost = 7 });
                AddRandomElementalResist(this, rand.Next(3, 6));
                RarityId = 1;
                Level = 1;
            }
            else if (Chance > 85 && Chance != 100)
            {
                string QualityName = Quality.GetGoodQuality();
                Name = QualityName + " " + Name;
                AddComponent(new ValueCharacteristic { Cost = 9 });
                AddRandomElementalResist(this, rand.Next(6, 11));
                RarityId = 1;
                Level = 2;
            }
            else if (Chance == 100)
            {
                string QualityName = Quality.GetBestQuality();
                Name = QualityName + " " + Name;
                AddComponent(new ValueCharacteristic { Cost = 14 });
                AddRandomElementalResist(this, rand.Next(11, 16));
                RarityId = 2;
                Level = 2;
            }

            Type = ItemType.Ring;

            DropChance = 4.5f;

            IsStacable = false;
            IsEquapable = true;
            IsEquiped = false;

            SetRarity(RarityId);
        }
    }
}
