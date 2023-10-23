
using ConsoleRPG.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleRPG.Items.StacableItems
{
    internal class IronOre : StacableItem
    {
        public IronOre(int level = 1) : base(level)
        {
            Name = "[grey]Железная[/] руда";
            Type = ItemType.Stacable;
            AddComponent(new ValueCharacteristic { Cost = 1 });
            RarityId = 0;
            Level = 1;
            DropChance = 32.5f;
            Count = 1;

            IsStacable = true;
            IsEquapable = false;
            IsEquiped = false;
            ID = ItemIdentifier.IronOre;
            SetRarity(RarityId);
        }
    }
}
