using ConsoleRPG.Items.Armors.Helmets;
using ConsoleRPG.Items.Currencies;
using ConsoleRPG.Items.Weapons;
using ConsoleRPG.Spells.DamageSpells;
using ConsoleRPG.Spells.SpellsComponents;
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
            Damage = random.Next(3, 6);

            //Экипировка
            Equipment.WearEquip(new EnchantedHat(), EquipmentSlot.Helmet);
            Equipment.WearEquip(new FireSword(), EquipmentSlot.LeftHand);

            DropList = new Item[] {new Gold(), Equipment.Equip[EquipmentSlot.LeftHand], Equipment.Equip[EquipmentSlot.Helmet] };
        }

        public override void FightLogic(Fight CurrentFight, Player Player, Item PlayerWeapon , int TakedDamage)
        {
            CurrentFight.TakeDamage(this, Player, TakedDamage, PlayerWeapon);

            if(!IsDead)
            {
                if(Energy >= 3)
                {
                    Pyroblast(CurrentFight, Player);
                    Energy = 0;
                }
                else
                {
                    if (new Random().Next(0,2) == 0)
                    {
                        FireBall(CurrentFight, Player);
                    } else
                    {
                        Attack(CurrentFight, Player);
                    }
                    
                }
                Energy++;
            }
        }

        public void Attack(Fight CurrentFight, Player Player)
        {
            int Damage = CurrentFight.GetDamage(this, Equipment.Equip[EquipmentSlot.LeftHand]);
            if (CurrentFight.IsCrit(Equipment.GetCriticalChance()))
            {
                Damage = CurrentFight.CalcCritDamage(CurrentFight.GetDamage(this, Equipment.Equip[EquipmentSlot.LeftHand]), Equipment.GetCriticalDamage());
                CurrentFight.TakeCritDamage(Player, Damage, Equipment.Equip[EquipmentSlot.LeftHand]);
            }
            else
            {
                CurrentFight.TakeDamage(Player, Damage, Equipment.Equip[EquipmentSlot.LeftHand]);
            }
        }

        public void FireBall(Fight CurrentFight, Player Player)
        {
            Spell Spell = new FireBall();
            int Damage = CurrentFight.GetDamage(this, Spell);
            if (CurrentFight.IsCrit(Equipment.GetCriticalChance()))
            {
                Damage = CurrentFight.CalcCritDamage(CurrentFight.GetDamage(this, Spell),Equipment.GetCriticalDamage());
                CurrentFight.TakeCritDamage(Player, Damage, Spell);
            }
            else
            {
                CurrentFight.TakeDamage(Player,Damage, Spell);
            }
            
        }

        public void Pyroblast(Fight CurrentFight, Player Player)
        {
            Spell Spell = new Pyroblast();
            int Damage = CurrentFight.GetDamage(this, Spell);
            if (CurrentFight.IsCrit(Equipment.GetCriticalChance()))
            {
                Damage = CurrentFight.CalcCritDamage(CurrentFight.GetDamage(this, Spell), Equipment.GetCriticalDamage());
                CurrentFight.TakeCritDamage(Player, Damage, Spell);
            }
            else
            {
                CurrentFight.TakeDamage(Player, Damage, Spell);
            }
        }
    }
}
