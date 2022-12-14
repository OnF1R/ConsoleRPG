using ConsoleRPG.Items.Armors.Helmets;
using ConsoleRPG.Items.Armors.Rings;
using ConsoleRPG.Items.Currencies;
using ConsoleRPG.Items.Shields;
using ConsoleRPG.Items.StacableItems;
using ConsoleRPG.Items.Weapons;
using ConsoleRPG.Spells.DamageSpells;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleRPG.Enemies
{
    internal class ExplosiveBug : Enemy
    {
        public ExplosiveBug()
        {
            Random random = new Random();
            Equipment equipment = new Equipment();
            Name = "[orangered1]Взрывной жук[/]";
            Level = random.Next(1, 6);
            MaxHealth = random.Next(4, 11) * Level;
            CurrentHealth = MaxHealth;
            Damage = random.Next(2, 4);

            //Экипировка
            Equipment.WearEquip(new ProtectionRing(), EquipmentSlot.FirstRing);

            DropList = new Item[]
            {
                new Gold(),
                new BloodStone(),
                Equipment.Equip[EquipmentSlot.FirstRing],
            };
        }

        public override void FightLogic(Fight CurrentFight, Player Player, Item PlayerWeapon, int TakedDamage)
        {
            CurrentFight.TakeDamage(this, Player, TakedDamage, PlayerWeapon);

            if (!IsDead)
            {
                if (Energy >= 4)
                {
                    NecroticExplosion(CurrentFight, Player);
                    Energy = 0;
                } 
                else
                {
                    Spit(CurrentFight, Player);
                    Energy++;
                }
            }
        }

        public void Spit(Fight CurrentFight, Player Player)
        {
            Spell Spell = new PoisonSpit();
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

        public void NecroticExplosion(Fight CurrentFight, Player Player)
        {
            Spell Spell = new NecroticExplosion();
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
