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
	internal class Butcher : Enemy
	{
		public Butcher(int level) : base(level)
		{
			Random random = new Random();
			MyRace = new Human();
			IsBoss = true;
			Equipment equipment = new Equipment();
			Name = "[red]БОСС[/] [bold]Мясник[/]";
			MaxHealth = random.Next(13, 22) * Level;
			CurrentHealth = MaxHealth;
			ID = EnemyIdentifierEnum.Butcher;
			GetComponent<PhysicalDamageCharacteristic>().PhysicalDamage = random.Next(3 + Level, Level + 6);
			GetComponent<ArmorCharacteristic>().Armor += 15;

			//Экипировка

			Equipment.WearEquip(this, new MeatHook(Level), EquipmentSlot.LeftHand);

			DropList = new Item[]
			{
				new RainbowStone(),
				new RainbowStone(),
				new BloodStone(),
				new BloodStone(),
				Equipment.Equip[EquipmentSlot.LeftHand],
			};
		}

		public override void FightLogic(Player Player)
		{
			if (!IsDead)
			{
				if (Energy > 4)
				{
					Dismember(Player);
					Energy = 0;
				}
				else
				{
					var rand = new Random().Next(1, 4);

					if (rand == 1)
					{
						Hook(Player);
					}
					else if (rand == 2)
					{
						VomitOdor(Player);
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

		public void Hook(Player Player)
		{
			BaseSpell Spell = new Hook();
			Spell.Use(this, Player);
		}

		public void VomitOdor(Player Player)
		{
			BaseSpell Spell = new VomitOdor();
			Spell.Use(this, Player);
		}

		public void Dismember(Player Player)
		{
			BaseSpell Spell = new Dismember();
			Spell.Use(this, Player);
		}
	}
}
