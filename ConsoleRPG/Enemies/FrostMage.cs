using ConsoleRPG.Enums;
using ConsoleRPG.Items.Armors.Capes;
using ConsoleRPG.Items.Armors.Helmets;
using ConsoleRPG.Items.Currencies;
using ConsoleRPG.Items.Shields;
using ConsoleRPG.Items.StacableItems;
using ConsoleRPG.Items.Weapons;
using ConsoleRPG.Spells.DamageSpells;
using ConsoleRPG.Spells.DefenceSpells;

namespace ConsoleRPG.Enemies
{
    [Serializable]
    internal class FrostMage : Enemy
    {
        public FrostMage(int level) : base(level)
        {
            SerializableRandom random = new SerializableRandom();
            Equipment equipment = new Equipment();
            Name = "[aqua]Морозный[/] маг";
            MaxHealth = random.Next(7, 11) * Level;
            CurrentHealth = MaxHealth;
			ID = EnemyIdentifierEnum.FrostMage;
			GetComponent<PhysicalDamageCharacteristic>().PhysicalDamage = random.Next(Level, Level + 3);

            //Экипировка
            Equipment.WearEquip(this, new EnchantedHat(Level), EquipmentSlot.Helmet);
            Equipment.WearEquip(this, new MagicStaff(Level), EquipmentSlot.LeftHand);
            Equipment.WearEquip(this, new FrostCape(Level), EquipmentSlot.Cape);

            DropList = new Item[]
            {
                new Gold(),
                new EnchantedIceShard(),
                Equipment.Equip[EquipmentSlot.LeftHand],
                Equipment.Equip[EquipmentSlot.Helmet],
                Equipment.Equip[EquipmentSlot.Cape],
            };
        }

        public override void FightLogic(Player Player, Unit unit)
        {
            bool IsUseIceBlock = false;

            if (!IsDead)
            {
                if (Energy <= 3)
                {
                    FrostBolt(unit);
                    Energy = 0;
                }
                else
                {
                    if (!IsUseIceBlock && CurrentHealthPercent() <= 65)
                    {
                        IsUseIceBlock = true;
                        IceBlock();
                        Energy -= 1;
                        return;
					}

                    if (new SerializableRandom().Next(0, 2) == 0)
                    {
                        FrostBall(unit);
                    }
                    else
                    {
                        foreach (var entity in GetDamageEntities())
                            Attack(unit, entity);
                    }
                }
                Energy++;
            }
            else
            {
                Death(Player);
            }
        }

        public void FrostBall(Unit unit)
        {
            BaseSpell Spell = new FrostBall();
			Spell.Use(this, unit);

		}

        public void FrostBolt(Unit unit)
        {
            BaseSpell Spell = new FrostBolt();
			Spell.Use(this, unit);
		}

        public void IceBlock()
        {
            BaseSpell Spell = new IceBlock();
            Spell.Use(this, this);
		}
    }
}
