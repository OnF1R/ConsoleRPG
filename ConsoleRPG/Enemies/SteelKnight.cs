﻿using ConsoleRPG.Items.Armors.Helmets;
using ConsoleRPG.Items.Armors.Boots;
using ConsoleRPG.Items.Currencies;
using ConsoleRPG.Items.Shields;
using ConsoleRPG.Items.StacableItems;
using ConsoleRPG.Items.Weapons;
using Spectre.Console;
using ConsoleRPG.Effects.Debuffs;
using ConsoleRPG.Enums;
using ConsoleRPG.Spells.DefenceSpells;

namespace ConsoleRPG.Enemies
{
    [Serializable]
    internal class SteelKnight : Enemy
    {
        public SteelKnight(int playerLevel) : base(playerLevel)
        {
            SerializableRandom random = new SerializableRandom();
            Equipment equipment = new Equipment();
            Name = "[grey]Стальной[/] рыцарь";
            MaxHealth = random.Next(4, 11) * Level;
            CurrentHealth = MaxHealth;
			ID = EnemyIdentifierEnum.SteelKnight;
			GetComponent<PhysicalDamageCharacteristic>().PhysicalDamage = random.Next(Level, Level + 3);

            //Экипировка

            switch (random.Next(0, 3))
            {
                case 0:
                    Equipment.WearEquip(this, new SteelAxe(Level), EquipmentSlot.LeftHand);
                    break;
                case 1:
                    Equipment.WearEquip(this, new SteelDagger(Level), EquipmentSlot.LeftHand);
                    break;
                case 2:
                    Equipment.WearEquip(this, new SteelSword(Level), EquipmentSlot.LeftHand);
                    break;
            }
            Equipment.WearEquip(this, new SteelHelmet(Level), EquipmentSlot.Helmet);
            Equipment.WearEquip(this, new SteelShield(Level), EquipmentSlot.RightHand);
            Equipment.WearEquip(this, new SpikedSandals(Level), EquipmentSlot.Boots);

            DropList = new Item[]
            {
                new Gold(),
                new IronOre(),
                Equipment.Equip[EquipmentSlot.LeftHand],
                Equipment.Equip[EquipmentSlot.RightHand],
                Equipment.Equip[EquipmentSlot.Helmet],
                Equipment.Equip[EquipmentSlot.Boots],
            };
        }

        public override void FightLogic(Player Player, Unit unit)
        {
            if (!IsDead)
            {
                if (Energy >= 5)
                {
                    ShieldBlock();
                    Energy = 0;
                }
                else
                {
                    foreach (var entity in GetDamageEntities())
                        Attack(unit, entity);

                    Energy++;
                }
            }
            else
            {
                Death(Player);
            }
        }

        private void ShieldBlock()
        {
            BaseSpell Spell = new SmallPhysicalDefence();
            Spell.Use(this, this);
        }
    }
}
