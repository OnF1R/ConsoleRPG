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
            GetComponent<PhysicalDamageCharacteristic>().PhysicalDamage = random.Next(2, 4);

            //Экипировка
            Equipment.WearEquip(this, new ProtectionRing(), EquipmentSlot.FirstRing);

            DropList = new Item[]
            {
                new Gold(),
                new BloodStone(),
                Equipment.Equip[EquipmentSlot.FirstRing],
            };
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
                if (Energy >= 4)
                {
                    NecroticExplosion(Player);
                    Energy = 0;
                } 
                else
                {
                    Spit(Player);
                    Energy++;
                }
            }
            else
            {
                DeathDropLoot(Player);
            }
        }

        public void Spit(Player Player)
        {
            Spell Spell = new PoisonSpit();
            Dictionary<DamageTypes, int> elemDamage = Spell.GetComponent<ElementalDamageCharacteristic>().ElemDamage;
            foreach (DamageTypes type in elemDamage.Keys)
            {
                Player.TakeDamage(elemDamage[type], type);
            }
        }

        public void NecroticExplosion(Player Player)
        {
            Spell Spell = new NecroticExplosion();
            Dictionary<DamageTypes, int> elemDamage = Spell.GetComponent<ElementalDamageCharacteristic>().ElemDamage;
            foreach (DamageTypes type in elemDamage.Keys)
            {
                Player.TakeDamage(elemDamage[type], type);
            }
        }
    }
}
