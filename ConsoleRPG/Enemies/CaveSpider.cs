﻿using ConsoleRPG.Effects.Debuffs;
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
	internal class CaveSpider : Enemy
	{
		public CaveSpider(int level) : base(level)
		{
			Random random = new Random();
			//Equipment equipment = new Equipment();
			Name = "[bold]Пещёрный паучок[/]";
			MaxHealth = random.Next(3, 5) * Level;
			CurrentHealth = MaxHealth;
			ID = EnemyIdentifierEnum.CaveSpider;
			GetComponent<PhysicalDamageCharacteristic>().PhysicalDamage = random.Next(2 + Level, 5 + Level);
			//MyRace = new Races.Undead();
			MyRace = new Human();

			DropList = new Item[]
			{
				new RainbowShard(),
				new DarkShard(),
				new SpiderLeg(),
				new SpiderPoison(),
				new SpiderEye(),
			};
		}

		public override void FightLogic(Player Player)
		{
			if (!IsDead)
			{
				SpiderBite(Player);
			}
			else
			{
				DeathDropLoot(Player);
			}
		}

		public void SpiderBite(Unit player)
		{
			BaseSpell Spell = new SpiderBite();
			Spell.Use(this, player);
		}
	}
}
