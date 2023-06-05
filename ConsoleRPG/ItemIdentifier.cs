using ConsoleRPG.Items.StacableItems;

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
        };
    }

    public enum ItemIdentifier
    {
        #region Helmets
        EnchantedHat,
        SteelHelmet,


        FireKeeperCrown,
        #endregion

        #region Capes
        WetCape,

        RaAyatAlQaiwai,
        #endregion

        #region Chestplates


        Immortal,
        #endregion

        #region Gloves
        CrafterGloves,

        Toxicity,
        #endregion

        #region Leggs



        MechanicalLeggs,
        #endregion

        #region Boots
        SpikedSandals,
        DragonClawBoots,
        #endregion

        #region Rings
        ElementalRing,
        ProtectionRing,
        VampirismRing,
        #endregion

        #region Trinkets
        EmeraldNecklace,
        ArcaneNecklace,
        RubyNecklace,
        #endregion

        #region StacableItems
        BloodStone,
        RainbowShard,
        RainbowStone,
        Cog,  //Шестерёнка
        IronIngot,
        IronOre,
        AdamantiteIngot,
        AdamantiteOre,


        OceanRune,
        DragonClaw,
        #endregion

        #region Shields
        SteelShield,
        RunicShield,
        SpikedShield,
        #endregion

        #region Weapons
        BloodLetter,
        FireSword,
        SteelAxe,
        SteelDagger,
        SteelSword,



        MiaymotoMusasi,
        #endregion

        #region Currencies
        Gold,
        #endregion

        #region Spells


        Gigantism,
        #endregion
    }
}
