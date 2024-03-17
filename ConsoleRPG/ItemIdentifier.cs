using ConsoleRPG.Items.StacableItems;
using ConsoleRPG.Enums;

namespace ConsoleRPG
{
    internal static class ItemID
    {
        public static Dictionary<ItemIdentifier, string> Names = new Dictionary<ItemIdentifier, string>()
        {
            { ItemIdentifier.BloodStone, new BloodStone().Name },
            { ItemIdentifier.IronIngot, new IronIngot().Name },
            { ItemIdentifier.IronOre, new IronOre().Name },
            { ItemIdentifier.RainbowShard, new RainbowShard().Name },
            { ItemIdentifier.RainbowStone, new RainbowStone().Name },
            { ItemIdentifier.Cog, new Cog().Name },
            { ItemIdentifier.AdamantiteIngot, new AdamantiteIngot().Name },
            { ItemIdentifier.AdamantiteOre, new AdamantiteOre().Name },
            { ItemIdentifier.OceanRune, new OceanRune().Name },
            { ItemIdentifier.DragonClaw, new DragonClaw().Name },
            { ItemIdentifier.DarkShard, new DarkShard().Name },
            { ItemIdentifier.DarkStone, new DarkStone().Name },
            { ItemIdentifier.SpiderPoison, new SpiderPoison().Name },
            { ItemIdentifier.SpiderEye, new SpiderEye().Name },
            { ItemIdentifier.SwormTooth, new SwormTooth().Name },
            { ItemIdentifier.DesertRune, new DesertRune().Name },
        };
    }
}
