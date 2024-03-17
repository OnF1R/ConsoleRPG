using Spectre.Console;
using ConsoleRPG.Items.StacableItems;
using ConsoleRPG.Races;
using ConsoleRPG.Items.Armors.Capes;
using ConsoleRPG.Spells.DamageSpells;
using ConsoleRPG.Interfaces;
using ConsoleRPG.Items.Weapons.BaseEnemyWeapons;
using ConsoleRPG.Items;
using ConsoleRPG.Enums;
using ConsoleRPG.Items.Weapons;

namespace ConsoleRPG.Enemies.Bosses
{
    [Serializable]
    internal class DarkMage : Enemy
    {
        public DarkMage(int level) : base(level)
        {
            SerializableRandom random = new SerializableRandom();
            MyRace = new Human();
            IsBoss = true;
            Equipment equipment = new Equipment();
            Name = "[red]БОСС[/] [bold]Тёмный маг[/]";
            MaxHealth = random.Next(13, 22) * Level;
            CurrentHealth = MaxHealth;
            ID = EnemyIdentifierEnum.DarkMage;
            GetComponent<PhysicalDamageCharacteristic>().PhysicalDamage = random.Next(1 + Level, Level + 3);
            GetComponent<MagicAmplificationCharacteristic>().Amplification += 50;

            //Экипировка

            Equipment.WearEquip(this, new GreatMagicStaff(Level), EquipmentSlot.LeftHand);

            DropList = new Item[]
            {
                new RainbowStone(),
                new RainbowStone(),
                new DarkStone(),
                new DarkStone(),
                Equipment.Equip[EquipmentSlot.LeftHand],
            };
        }

        public override void FightLogic(Player Player, Unit unit)
        {
            if (!IsDead)
            {
                if (Energy > 4)
                {
                    ChaosBolt(unit);
                    Energy = 0;
                }
                else
                {
                    if (new SerializableRandom().Next(1,3) > 1)
                    {
                        DarkStrike(unit);
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

        public void DarkStrike(Unit unit)
        {
            BaseSpell Spell = new DarkStrike();
			Spell.Use(this, unit);
		}

		public void ChaosBolt(Unit unit)
		{
			BaseSpell Spell = new ChaosBolt();
			Spell.Use(this, unit);
		}
	}
}
