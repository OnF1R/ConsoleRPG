using ConsoleRPG.Items.Armors.Helmets;
using ConsoleRPG.Items.Armors.Rings;
using ConsoleRPG.Items.Currencies;
using ConsoleRPG.Items.Weapons;
using ConsoleRPG.Spells.DamageSpells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleRPG.Enemies
{
    internal class Elemental : Enemy
    {
        public Elemental()
        {
            Random random = new Random();
            //Equipment equipment = new Equipment();
            Name = "[bold]Элементаль[/]";
            Level = random.Next(1, 6);
            MaxHealth = random.Next(7, 11) * Level;
            CurrentHealth = MaxHealth;
            GetComponent<PhysicalDamageCharacteristic>().PhysicalDamage = random.Next(2, 5);

            Equipment.WearEquip(new EnchantedHat(), EquipmentSlot.Helmet);
            Equipment.WearEquip(new ElementalRing(), EquipmentSlot.FirstRing);

            DropList = new Item[] { new Gold(), Equipment.Equip[EquipmentSlot.Helmet], Equipment.Equip[EquipmentSlot.FirstRing] };
        }

        public override void FightLogic(Player Player, Dictionary<DamageTypes, int> TakedDamage)
        {
            foreach (DamageTypes type in TakedDamage.Keys)
            {
                TakeDamage(TakedDamage[type], type);
            }

            if (!IsDead)
            {
                if (Energy >= 3)
                {
                    ElementalSplash(Player);
                    Energy = 0;
                }
                else
                {
                    ElementalSplash(Player);
                }
                Energy++;
            }
            else
            {
                DeathDropLoot(Player);
            }
        }

        public void ElementalSplash(Player Player)
        {
            Spell Spell = new ElementalSplash();
            Dictionary<DamageTypes, int> elemDamage = Spell.GetComponent<ElementalDamageCharacteristic>().ElemDamage;
            foreach (DamageTypes type in elemDamage.Keys)
            {
                Player.TakeDamage(elemDamage[type], type);
            }
            //if (IsCrit())
            //{
            //}
            //else
            //{
            //}
        }
    }
}
