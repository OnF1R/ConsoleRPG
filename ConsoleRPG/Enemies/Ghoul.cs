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
            GetComponent<PhysicalDamageCharacteristic>().PhysicalDamage = random.Next(7, 12);

            //Экипировка
            Equipment.WearEquip(this, new BloodLetter(), EquipmentSlot.LeftHand);

            DropList = new Item[] { new Gold(), Equipment.Equip[EquipmentSlot.LeftHand], new BloodStone() };
        }

        public override void FightLogic(Player Player, Dictionary<DamageTypes, int> TakedDamage)
        {
            foreach(DamageTypes type in TakedDamage.Keys)
            {
                if (!IsDead)
                    TakeDamage(TakedDamage[type], type);
            }

            if (!IsDead)
            {
                if (Energy >= 3)
                {
                    for (int i = 0; i < new Random().Next(2,5); i++) {
                        if (!Player.IsDead)
                        {
                            Attack(Player);
                            
                        }
                    }
                    Energy = 0;
                }
                else
                {
                    Attack(Player);
                }
                Energy++;
            }
            else
            {
                DeathDropLoot(Player);
            }
        }
    }
}
