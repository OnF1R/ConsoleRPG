using ConsoleRPG.Items.ItemsComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG.Items.Currencies
{
    internal class Gold : StacableItem
    {
        public Gold()
        {
            Name = "Золото";
            Type = ItemType.Currency;
            AddComponent(new Valuable { Cost = 1 });
            RarityId = 9;
            Level = 1;
            DropChance = 25f;
            Count = new Random().Next(1,3);

            IsStacable = true;
            IsEquapable = false;
            IsEquiped = false;

            SetRarity(RarityId);
        }
    }
}
