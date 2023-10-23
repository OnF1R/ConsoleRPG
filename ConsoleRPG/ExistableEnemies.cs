using ConsoleRPG.Enemies;
using ConsoleRPG.Enemies.Bosses;
using ConsoleRPG.Enums;
using ConsoleRPG.Locations;
using static ConsoleRPG.Enums.LocationIdentifierEnum;

namespace ConsoleRPG
{
    internal class ExistableEnemies
    {
        public static Enemy GetRandomEnemy(int playerLevel)
        {
            Enemy[] enemies = new Enemy[] {
                new FireMage(playerLevel),
                new Elemental(playerLevel),
                new Ghoul(playerLevel),
                new SteelKnight(playerLevel),
                new ExplosiveBug(playerLevel),
                new WaterDragon(playerLevel),
                new SmallOreStone(playerLevel),
                new Wisp(playerLevel),
                new SpikedKnight(playerLevel),
                new Mekanism(playerLevel),
                new SmallLootingGoblin(playerLevel),
            };

            Random rand = new Random();
            Enemy enemy = enemies[rand.Next(enemies.Length)];
            return enemy;
        }

        public static Enemy GetRandomEnemy(LocationIdentifier locationId, int playerLevel)
        {
            List<Enemy> enemies = new List<Enemy>();
			switch(locationId)
            {
                case LocationIdentifier.Castle:
                    enemies.Add(new SteelKnight(playerLevel));
                    enemies.Add(new SpikedKnight(playerLevel));
                    break;
                case LocationIdentifier.Desert:
                    enemies.Add(new Wisp(playerLevel));
					enemies.Add(new SmallOreStone(playerLevel));
					enemies.Add(new DesertSworm(playerLevel));
					enemies.Add(new Elemental(playerLevel));
					break;
				case LocationIdentifier.Swamp:
					//enemies.Add(new Elemental(playerLevel));
					//enemies.Add(new ExplosiveBug(playerLevel));
					//6enemies.Add(new Ent(playerLevel));
					//enemies.Add(new ExplosiveBug(playerLevel));
					break;
				case LocationIdentifier.MageTower:
					enemies.Add(new FireMage(playerLevel));
					enemies.Add(new FrostMage(playerLevel));
                    enemies.Add(new WaterDragon(playerLevel));
					break;
				case LocationIdentifier.Gorge:
					enemies.Add(new Ghoul(playerLevel));
					enemies.Add(new SmallOreStone(playerLevel));
                    enemies.Add(new Gargoyle(playerLevel));
                    enemies.Add(new CaveSpider(playerLevel));
					enemies.Add(new ExplosiveBug(playerLevel));
					break;
				default:
                    break;
			}


            enemies.Add(new SmallLootingGoblin(playerLevel));
            enemies.Add(new Mekanism(playerLevel));

			Random rand = new Random();
			Enemy enemy = enemies[rand.Next(enemies.Count)];
			return enemy;
		}


		public static Enemy GetRandomBoss(int playerLevel)
        {
            Enemy[] enemies = new Enemy[] {
                new Immortal(playerLevel),
                new AbyUdRiRisaAlha(playerLevel),
            };
            Random rand = new Random();
            Enemy enemy = enemies[rand.Next(enemies.Length)];
            return enemy;
        }

		public static Enemy GetRandomBoss(LocationIdentifier locationId, int playerLevel)
		{
			List<Enemy> enemies = new List<Enemy>();
			switch (locationId)
			{
				case LocationIdentifier.Castle:
					enemies.Add(new Immortal(playerLevel));
					break;
				case LocationIdentifier.Desert:
					enemies.Add(new AbyUdRiRisaAlha(playerLevel));
					break;
				case LocationIdentifier.Swamp:
					//enemies.Add(new SwampTroll(playerLevel));
					break;
				case LocationIdentifier.MageTower:
					enemies.Add(new DarkMage(playerLevel));
					break;
				case LocationIdentifier.Gorge:
					enemies.Add(new Butcher(playerLevel));
					break;
				default:
					break;
			}

			Random rand = new Random();
			Enemy enemy = enemies[rand.Next(enemies.Count)];
			return enemy;
		}

		public static Dictionary<EnemyIdentifierEnum, string> EnemyNames = new Dictionary<EnemyIdentifierEnum, string>()
        {
            { EnemyIdentifierEnum.Elemental, new Elemental(1).Name },
            { EnemyIdentifierEnum.ExplosiveBug, new ExplosiveBug(1).Name },
            { EnemyIdentifierEnum.FireMage, new FireMage(1).Name },
            { EnemyIdentifierEnum.Ghoul, new Ghoul(1).Name },
            { EnemyIdentifierEnum.Mekanism, new Mekanism(1).Name },
            { EnemyIdentifierEnum.SmallLootingGoblin, new SmallLootingGoblin(1).Name },
            { EnemyIdentifierEnum.SmallOreStone, new SmallOreStone(1).Name },
            { EnemyIdentifierEnum.SpikedKnight, new SpikedKnight(1).Name },
            { EnemyIdentifierEnum.SteelKnight, new SteelKnight(1).Name },
            { EnemyIdentifierEnum.WaterDragon, new WaterDragon(1).Name },
            { EnemyIdentifierEnum.Wisp, new Wisp(1).Name },


            { EnemyIdentifierEnum.AbyUdRiRisaAlha, new AbyUdRiRisaAlha(1).Name },
            { EnemyIdentifierEnum.Immortal, new Immortal(1).Name },
        };
    }
}
