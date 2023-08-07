﻿using ConsoleRPG.Items.Currencies;
using ConsoleRPG.Items.StacableItems;
using ConsoleRPG.Spells.DamageSpells;


namespace ConsoleRPG.Enemies
{
    internal class Wisp : Enemy
    {
        public Wisp(int playerLevel) : base(playerLevel)
        {
            Random random = new Random();
            //Equipment equipment = new Equipment();
            Name = "[bold]Светлячок[/]";
            MaxHealth = random.Next(3, 6) * Level;
            CurrentHealth = MaxHealth;
            GetComponent<PhysicalDamageCharacteristic>().PhysicalDamage = random.Next(Level, 2 + Level);
            MyRace = new Races.Elemental();

            DropList = new Item[]
            {
                new Gold(),
                new RainbowShard(),
                new RainbowStone(),
            };
        }

        public override void FightLogic(Player Player)
        {
            if (!IsDead)
            {
                WillOWisp(Player);
            }
            else
            {
                DeathDropLoot(Player);
            }
        }

        public void WillOWisp(Player Player)
        {
            BaseSpell Spell = new WillOWisp();
            Dictionary<DamageTypes, int> elemDamage = Spell.GetComponent<ElementalDamageCharacteristic>().ElemDamage;
            foreach (DamageTypes type in elemDamage.Keys)
            {
                DealDamage(Player, elemDamage[type], type, Spell);
            }
        }
    }
}
