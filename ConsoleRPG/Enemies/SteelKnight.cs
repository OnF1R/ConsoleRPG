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
            GetComponent<PhysicalDamageCharacteristic>().PhysicalDamage = random.Next(2, 4);

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

        public override void FightLogic(Player Player, Dictionary<DamageTypes, int> TakedDamage)
        {

            

            if (Energy >= 3)
            {
                AnsiConsole.MarkupLine("{0} использовал Блок щитом!", Name);
                foreach (DamageTypes type in TakedDamage.Keys)
                {
                    TakeDamage(TakedDamage[type]/2, type);
                }
                Energy = 0;
            }
            else
            {
                foreach (DamageTypes type in TakedDamage.Keys)
                {
                    TakeDamage(TakedDamage[type], type);
                }
            }

            if (!IsDead)
            {
                Attack(Player);
                Energy++;
            }
            else
            {
                DeathDropLoot(Player);
            }
        }
    }
}
