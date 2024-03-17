using ConsoleRPG.Enums;

namespace ConsoleRPG.Items.Weapons.BaseEnemyWeapons
{
    [Serializable]
    internal class MekanismBaseWeapon : Weapon
    {
        public MekanismBaseWeapon(int level) : base(level)
        {
            Name = "Механические клешни";

            Type = ItemType.BaseWeapon;

            DropChance = -1f;

            IsStacable = false;
            IsEquapable = true;
            IsEquiped = false;
        }
    }
}
