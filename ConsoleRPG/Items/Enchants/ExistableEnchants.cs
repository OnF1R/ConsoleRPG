
namespace ConsoleRPG.Items.Enchants
{
    [Serializable]
    internal static class ExistableEnchants
    {
        public static List<BaseEnchant> enchants = new List<BaseEnchant>()
        {
            new DrakulaBootsEnchant(),
            new FoolAndElementEnchant(),
            new PoisonousShell(),
            new DodgingTheRainEnchant(),
        };
    }
}
