using ConsoleRPG.Items.Armors.Helmets;
using ConsoleRPG.Items.Currencies;
using ConsoleRPG.Items.Shields;
using ConsoleRPG.Items.StacableItems;
using ConsoleRPG.Items.Weapons;
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
    internal class SteelKnight : Enemy
    {
        public SteelKnight()
        {
            Random random = new Random();
            Equipment equipment = new Equipment();
            Name = "[grey]Стальной[/] рыцарь";
            Level = random.Next(1, 6);
            MaxHealth = random.Next(4, 11) * Level;
            CurrentHealth = MaxHealth;
            Damage = random.Next(2, 4);

            //Экипировка

            switch (random.Next(0, 3))
            {
                case 0:
                    Equipment.WearEquip(new SteelAxe(), EquipmentSlot.LeftHand);
                    break;
                case 1:
                    Equipment.WearEquip(new SteelDagger(), EquipmentSlot.LeftHand);
                    break;
                case 2:
                    Equipment.WearEquip(new SteelSword(), EquipmentSlot.LeftHand);
                    break;
            }
            Equipment.WearEquip(new SteelHelmet(), EquipmentSlot.Helmet);
            Equipment.WearEquip(new SteelShield(), EquipmentSlot.RightHand);

            DropList = new Item[]
            {
                new Gold(),
                new IronOre(),
                Equipment.Equip[EquipmentSlot.LeftHand],
                Equipment.Equip[EquipmentSlot.RightHand],
                Equipment.Equip[EquipmentSlot.Helmet],
            };
        }

        public override void FightLogic(Fight CurrentFight, Player Player, Item PlayerWeapon, int TakedDamage)
        {
            if (Energy >= 3)
            {
                AnsiConsole.MarkupLine("Блок щитом!");
                CurrentFight.TakeDamage(this, Player, TakedDamage / 2, PlayerWeapon);
            }
            else
            {
                CurrentFight.TakeDamage(this, Player, TakedDamage, PlayerWeapon);
            }

            if (!IsDead)
            {
                Attack(CurrentFight, Player);
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
    }
}
