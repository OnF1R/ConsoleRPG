using ConsoleRPG.Enemies;
using ConsoleRPG.Enemies.Bosses;

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
    }
}
