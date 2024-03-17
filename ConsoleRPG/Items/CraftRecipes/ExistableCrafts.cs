
namespace ConsoleRPG.Items.CraftRecipes
{
    [Serializable]
    internal static class ExistableCrafts
    {
        public static List<BaseItemRecipe> recipes = new List<BaseItemRecipe>()
        {
            new DragonClawBootsRecipe(),
            new SwormTeethNecklaceRecipe(),
        };
    }
}
