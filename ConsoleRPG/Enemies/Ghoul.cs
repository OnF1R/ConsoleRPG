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
        public Ghoul(int playerLevel) : base(playerLevel)
        {
            Random random = new Random();
            Equipment equipment = new Equipment();
            Name = "Вурдалак";
            MaxHealth = random.Next(7, 11) * Level;
            CurrentHealth = MaxHealth;
            GetComponent<PhysicalDamageCharacteristic>().PhysicalDamage = random.Next(3 + Level, 7 + Level);

            //Экипировка
            Equipment.WearEquip(this, new BloodLetter(Level), EquipmentSlot.LeftHand);

            DropList = new Item[] { new Gold(), Equipment.Equip[EquipmentSlot.LeftHand], new BloodStone(Level) };
        }

        public override void FightLogic(Player Player, Dictionary<DamageTypes, int> TakedDamage)
        {
            foreach(DamageTypes type in TakedDamage.Keys)
            {
                if (!IsDead)
                    TakeDamage(Player, TakedDamage[type], type);
            }

            if (!IsDead)
            {
                Player.AfterAttackBehaviour(this);

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

                AfterAttackBehaviour(Player);

                Energy++;
            }
            else
            {
                DeathDropLoot(Player);
            }
        }
    }
}
