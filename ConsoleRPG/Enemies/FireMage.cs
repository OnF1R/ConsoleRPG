using ConsoleRPG.Items.Armors.Helmets;
using ConsoleRPG.Items.Currencies;
using ConsoleRPG.Items.Weapons;
using ConsoleRPG.Spells.DamageSpells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG.Enemies
{
    internal class FireMage : Enemy
    {
        public FireMage()
        {
            Random random = new Random();
            Equipment equipment = new Equipment();
            Name = "[orangered1]Огненный[/] маг";
            Level = random.Next(1, 4);
            MaxHealth = random.Next(7, 11) * Level;
            CurrentHealth = MaxHealth;
            GetComponent<PhysicalDamageCharacteristic>().PhysicalDamage = random.Next(3, 6);

            //Экипировка
            Equipment.WearEquip(this, new EnchantedHat(), EquipmentSlot.Helmet);
            Equipment.WearEquip(this, new FireSword(), EquipmentSlot.LeftHand);

            DropList = new Item[] {new Gold(), Equipment.Equip[EquipmentSlot.LeftHand], Equipment.Equip[EquipmentSlot.Helmet] };
        }

        public override void FightLogic(Player Player, Dictionary<DamageTypes, int> TakedDamage)
        {
            foreach (DamageTypes type in TakedDamage.Keys)
            {
                if (!IsDead)
                    TakeDamage(TakedDamage[type], type);
            }

            if (!IsDead)
            {
                if(Energy >= 3)
                {
                    Pyroblast(Player);
                    Energy = 0;
                }
                else
                {
                    if (new Random().Next(0,2) == 0)
                    {
                        FireBall(Player);
                    } else
                    {
                        Attack(Player);
                    }
                    
                }

                Energy++;
            }
            else
            {
                DeathDropLoot(Player);
            }
        }

        public void FireBall(Player Player)
        {
            Spell Spell = new FireBall();
            Dictionary<DamageTypes, int> elemDamage = Spell.GetComponent<ElementalDamageCharacteristic>().ElemDamage;
            foreach (DamageTypes type in elemDamage.Keys)
            {
                Player.TakeDamage(elemDamage[type], type);
            }

        }

        public void Pyroblast(Player Player)
        {
            Spell Spell = new Pyroblast();
            Dictionary<DamageTypes, int> elemDamage = Spell.GetComponent<ElementalDamageCharacteristic>().ElemDamage;
            foreach (DamageTypes type in elemDamage.Keys)
            {
                Player.TakeDamage(elemDamage[type], type);
            }
        }
    }
}
