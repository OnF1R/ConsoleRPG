using ConsoleRPG.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG.Items.Armors.Chestsplates
{
    [Serializable]
    internal class ImmortalChestplate : Armor
    {
        public ImmortalChestplate(int level) : base(level)
        {
            SerializableRandom rand = new SerializableRandom();
            Name = "[bold]'Бессмертный'[/]";

            ID = ItemIdentifier.Immortal;
            int cost = rand.Next(75, 301);
            AddComponent(new ValueCharacteristic { Cost = cost });
            int resist = rand.Next(15, 31);
            AddComponent(new ArmorCharacteristic { Armor = resist });
            RarityId = 6;
            Level = 5;

            Type = ItemType.Chest;

            DropChance = 100f;

            IsStacable = false;
            IsEquapable = true;
            IsEquiped = false;

            SetRarity(RarityId);
        }
    }
}
