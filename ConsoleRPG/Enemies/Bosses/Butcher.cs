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
    internal class Butcher : Enemy
	{
		public Butcher(int level) : base(level)
		{
			SerializableRandom random = new SerializableRandom();
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

		public override void FightLogic(Player Player, Unit unit)
        {
			if (!IsDead)
			{
				if (Energy > 4)
				{
					Dismember(unit);
					Energy = 0;
				}
				else
				{
					var rand = new SerializableRandom().Next(1, 4);

					if (rand == 1)
					{
						Hook(unit);
					}
					else if (rand == 2)
					{
						VomitOdor(unit);
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

		public void Hook(Unit unit)
		{
			BaseSpell Spell = new Hook();
			Spell.Use(this, unit);
		}

		public void VomitOdor(Unit unit)
		{
			BaseSpell Spell = new VomitOdor();
			Spell.Use(this, unit);
		}

		public void Dismember(Unit unit)
		{
			BaseSpell Spell = new Dismember();
			Spell.Use(this, unit);
		}
	}
}
