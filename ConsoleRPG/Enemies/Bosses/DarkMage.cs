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
    internal class DarkMage : Enemy
    {
        public DarkMage(int level) : base(level)
        {
            Random random = new Random();
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

        public override void FightLogic(Player Player)
        {
            if (!IsDead)
            {
                if (Energy > 4)
                {
                    ChaosBolt(Player);
                    Energy = 0;
                }
                else
                {
                    if (new Random().Next(1,3) > 1)
                    {
                        DarkStrike(Player);
                    }
                    else
                    {
						foreach (var entity in GetDamageEntities())
							Attack(Player, entity);
					}
                }

                Energy++;
            }
            else
            {
                DeathDropLoot(Player);
            }
        }

        public void DarkStrike(Player Player)
        {
            BaseSpell Spell = new DarkStrike();
			Spell.Use(this, Player);
		}

		public void ChaosBolt(Player Player)
		{
			BaseSpell Spell = new ChaosBolt();
			Spell.Use(this, Player);
		}
	}
}
