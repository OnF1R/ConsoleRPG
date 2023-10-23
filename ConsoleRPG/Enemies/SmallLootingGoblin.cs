using ConsoleRPG.Enums;
using ConsoleRPG.Items.Armors.Gloves;
using ConsoleRPG.Items.Armors.Leggs;
using ConsoleRPG.Items.Armors.Trinkets;
using ConsoleRPG.Items.Currencies;
using ConsoleRPG.Items.StacableItems;
using ConsoleRPG.Items.Weapons.BaseEnemyWeapons;
using ConsoleRPG.Spells.DamageSpells;


namespace ConsoleRPG.Enemies
{
	internal class SmallLootingGoblin : Enemy
	{
		public SmallLootingGoblin(int playerLevel) : base(playerLevel)
		{
			Random random = new Random();
			//Equipment equipment = new Equipment();
			Name = "[bold]Маленький богатый гоблин[/]";
			MaxHealth = random.Next(3, 6) * Level;
			CurrentHealth = MaxHealth;
			ID = EnemyIdentifierEnum.SmallLootingGoblin;
			GetComponent<PhysicalDamageCharacteristic>().PhysicalDamage = random.Next(Level, 2 + Level);
			MyRace = new Races.Goblin();


			Equipment.WearEquip(this, new SmallLootingGoblinBaseWeapon(Level), EquipmentSlot.LeftHand);

			switch (random.Next(1, 4))
			{
				case 1:
					Equipment.WearEquip(this, new EmelardNecklace(Level), EquipmentSlot.Trinket);
					break;
				case 2:
					Equipment.WearEquip(this, new ArcaneNecklace(Level), EquipmentSlot.Trinket);
					break;
				case 3:
					Equipment.WearEquip(this, new RubyNecklace(Level), EquipmentSlot.Trinket);
					break;
				default:
					break;
			}

			Equipment.WearEquip(this, new CrafterGloves(Level), EquipmentSlot.Gloves);

			DropList = new Item[]
			{
				new Gold(),
				new Gold(),
				new RainbowShard(),
				new RainbowShard(),
				new RainbowStone(),
				new RainbowStone(),
				new AdamantiteIngot(),
				new AdamantiteIngot(),
				new IronIngot(),
				new IronIngot(),
				Equipment.Equip[EquipmentSlot.Trinket],
				Equipment.Equip[EquipmentSlot.Gloves],
			};
		}

		public override void FightLogic(Player Player)
		{
			if (!IsDead)
			{
				foreach (var entity in GetDamageEntities())
					Attack(Player, entity);
			}
			else
			{
				DeathDropLoot(Player);
			}
		}
	}
}
