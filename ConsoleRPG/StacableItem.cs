namespace ConsoleRPG
{
    [Serializable]
    abstract internal class StacableItem : Item
    {
        public StacableItem(int level = 1) : base(level = 1)
        {
            IsStacable = true;
            IsEquapable = false;
            IsEquiped = false;
        }
    }
}
