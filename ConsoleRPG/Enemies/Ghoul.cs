using ConsoleRPG.Enums;
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
    [Serializable]
    internal class Ghoul : Enemy
    {
        public Ghoul(int level) : base(level)
        {
            SerializableRandom random = new SerializableRandom();
            Equipment equipment = new Equipment();
            Name = "Вурдалак";
            MaxHealth = random.Next(7, 11) * Level;
            CurrentHealth = MaxHealth;
			ID = EnemyIdentifierEnum.Ghoul;
			GetComponent<PhysicalDamageCharacteristic>().PhysicalDamage = random.Next(3 + Level, 7 + Level);

            //Экипировка
            Equipment.WearEquip(this, new BloodLetter(Level), EquipmentSlot.LeftHand);

            DropList = new Item[] { new Gold(), Equipment.Equip[EquipmentSlot.LeftHand], new BloodStone() };
        }

        public override void FightLogic(Player Player, Unit unit)
        {
            if (!IsDead)
            {
                if (Energy >= 3)
                {
                    for (int i = 0; i < new SerializableRandom().Next(2, 5); i++)
                    {
                        if (!unit.IsDead)
                        {
                            foreach (var entity in GetDamageEntities())
                                Attack(unit, entity);
                        }
                    }
                    Energy = 0;
                }
                else
                {
                    foreach (var entity in GetDamageEntities())
                        Attack(unit, entity);
                }
                Energy++;
            }
            else
            {
                Death(Player);
            }
        }
    }
}
