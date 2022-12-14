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
            Damage = random.Next(2, 5);

            //Экипировка
            Equipment.WearEquip(new EnchantedHat(), EquipmentSlot.Helmet);
            Equipment.WearEquip(new ElementalRing(), EquipmentSlot.FirstRing);

            DropList = new Item[] { new Gold(), Equipment.Equip[EquipmentSlot.Helmet], Equipment.Equip[EquipmentSlot.FirstRing] };
        }

        public override void FightLogic(Fight CurrentFight, Player Player, Item PlayerWeapon, int TakedDamage)
        {
            CurrentFight.TakeDamage(this, Player, TakedDamage, PlayerWeapon);

            if (!IsDead)
            {
                if (Energy >= 3)
                {
                    ElementalSplash(CurrentFight, Player);
                    Energy = 0;
                }
                else
                {
                    ElementalSplash(CurrentFight, Player);
                }
                Energy++;
            }
        }

        public void ElementalSplash(Fight CurrentFight, Player Player)
        {
            Spell Spell = new ElementalSplash();
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
