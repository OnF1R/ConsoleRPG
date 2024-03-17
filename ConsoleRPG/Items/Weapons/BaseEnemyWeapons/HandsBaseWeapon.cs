using ConsoleRPG.Enums;
namespace ConsoleRPG.Items.Weapons.BaseEnemyWeapons
{
    [Serializable]
    internal class HandsBaseWeapon : Weapon
    {
        public HandsBaseWeapon(int level) : base(level)
        {
            Name = "Кулаки";

            Type = ItemType.BaseWeapon;

            DropChance = -1f;

            IsStacable = false;
            IsEquapable = true;
            IsEquiped = false;
        }
    }
}
