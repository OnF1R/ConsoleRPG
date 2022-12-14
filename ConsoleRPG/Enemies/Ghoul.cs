using ConsoleRPG.Items.Armors.Helmets;
using ConsoleRPG.Items.Currencies;
using ConsoleRPG.Items.StacableItems;
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
    internal class Ghoul : Enemy
    {
        public Ghoul()
        {
            Random random = new Random();
            Equipment equipment = new Equipment();
            Name = "Вурдалак";
            Level = random.Next(3, 7);
            MaxHealth = random.Next(7, 11) * Level;
            CurrentHealth = MaxHealth;
            Damage = random.Next(7, 12);

            //Экипировка
            Equipment.WearEquip(new BloodLetter(), EquipmentSlot.LeftHand);

            DropList = new Item[] { new Gold(), Equipment.Equip[EquipmentSlot.LeftHand], new BloodStone() };
        }

        public override void FightLogic(Fight CurrentFight, Player Player, Item PlayerWeapon, int TakedDamage)
        {
            CurrentFight.TakeDamage(this, Player, TakedDamage, PlayerWeapon);

            if (!IsDead)
            {
                if (Energy >= 3)
                {
                    for (int i = 0; i < new Random().Next(2,5); i++) {
                        if (!Player.IsDead)
                        {
                            Bite(CurrentFight, Player);
                            
                        }
                    }
                    Energy = 0;
                }
                else
                {
                    Bite(CurrentFight, Player);
                }
                Energy++;
            }
        }

        public void Bite(Fight CurrentFight, Player Player)
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
    }
}
