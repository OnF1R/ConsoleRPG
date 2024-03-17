namespace ConsoleRPG.Items.Shields
{
    [Serializable]
    internal class Shield : Armor
    {
        public Shield(int level) : base(level)
        {
            this.IsStacable = false;
            this.IsEquapable = true;
        }
    }
}
