﻿
using ConsoleRPG.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG.Items.Currencies
{
    [Serializable]
    internal class Gold : StacableItem
    {
        public Gold(int level = 1) : base(level = 1)
        {
            Name = "Золото";
            Type = ItemType.Currency;
            AddComponent(new ValueCharacteristic { Cost = 1 });
            RarityId = 9;
            Level = 1;
            DropChance = 25f;
            Count = new SerializableRandom().Next(1,3);

            IsStacable = true;
            IsEquapable = false;
            IsEquiped = false;
            ID = ItemIdentifier.Gold;
            SetRarity(RarityId);
        }
    }
}
