using ConsoleRPG.Effects.Debuffs;
using ConsoleRPG.Enums;
using ConsoleRPG.Items.Armors.Helmets;
using ConsoleRPG.Items.Armors.Rings;
using ConsoleRPG.Items.Currencies;
using ConsoleRPG.Items.StacableItems;
using ConsoleRPG.Items.Weapons;
using ConsoleRPG.Items.Weapons.BaseEnemyWeapons;
using ConsoleRPG.Races;
using ConsoleRPG.Spells.DamageSpells;
using ConsoleRPG.Spells.DefenceSpells;

namespace ConsoleRPG.Enemies
{
    [Serializable]
    internal class Gargoyle : Enemy
	{
		public Gargoyle(int level) : base(level)
		{
			SerializableRandom random = new SerializableRandom();
			//Equipment equipment = new Equipment();
			Name = "[bold]Гаргулья[/]";
			MaxHealth = random.Next(7, 11) * Level;
			CurrentHealth = MaxHealth;
			ID = EnemyIdentifierEnum.Gargoyle;
			GetComponent<PhysicalDamageCharacteristic>().PhysicalDamage = random.Next(2 + Level, 5 + Level);
			//MyRace = new Races.Undead();
			MyRace = new Human();

			Equipment.WearEquip(this, new ClawsBaseWeapon(Level), EquipmentSlot.LeftHand);

			DropList = new Item[]
			{
				new RainbowShard(),
				new DarkShard(),
				new GargoyleClaw(),
				new GargoyleWing(),
			};
		}

		public override void FightLogic(Player Player, Unit unit)
        {
			if (!IsDead)
			{
				if (Energy >= 4)
				{
					Fosillization();
					Energy = -2;

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

		public void Fosillization()
		{
			BaseSpell Spell = new Fossilization();
			Spell.Use(this, this);
		}
	}
}
