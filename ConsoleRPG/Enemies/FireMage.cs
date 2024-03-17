using ConsoleRPG.Enums;
using ConsoleRPG.Items.Armors.Helmets;
using ConsoleRPG.Items.Currencies;
using ConsoleRPG.Items.Shields;
using ConsoleRPG.Items.Weapons;
using ConsoleRPG.Spells.DamageSpells;

namespace ConsoleRPG.Enemies
{
    [Serializable]
    internal class FireMage : Enemy
    {
        public FireMage(int level) : base(level)
        {
            SerializableRandom random = new SerializableRandom();
            Equipment equipment = new Equipment();
            Name = "[orangered1]Огненный[/] маг";
            MaxHealth = random.Next(7, 11) * Level;
            CurrentHealth = MaxHealth;
			ID = EnemyIdentifierEnum.FireMage;
			GetComponent<PhysicalDamageCharacteristic>().PhysicalDamage = random.Next(Level, Level + 3);

            //Экипировка
            Equipment.WearEquip(this, new EnchantedHat(Level), EquipmentSlot.Helmet);
            Equipment.WearEquip(this, new FireSword(Level), EquipmentSlot.LeftHand);
            Equipment.WearEquip(this, new RunicShield(Level), EquipmentSlot.RightHand);

            //Team.Add(ExistableEnemies.GetRandomEnemy(level, ))

            DropList = new Item[]
            {
                new Gold(),
                Equipment.Equip[EquipmentSlot.LeftHand],
                Equipment.Equip[EquipmentSlot.Helmet],
                Equipment.Equip[EquipmentSlot.RightHand]
            };
        }

        public override void FightLogic(Player Player, Unit unit)
        {
            if (!IsDead)
            {
                if (Energy >= 3)
                {
                    Pyroblast(unit);
                    Energy = 0;
                }
                else
                {
                    if (new SerializableRandom().Next(0, 2) == 0)
                    {
                        FireBall(unit);
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

        public void FireBall(Unit unit)
        {
            BaseSpell Spell = new FireBall();
            Spell.Use(this, unit);
        }

        public void Pyroblast(Unit unit)
        {
            BaseSpell Spell = new Pyroblast();
            Spell.Use(this, unit);
        }
    }
}
