using ConsoleRPG.Enums;

namespace ConsoleRPG.Items.Weapons.BaseEnemyWeapons
{
    [Serializable]
    internal class SmallLootingGoblinBaseWeapon : Weapon
    {
        public SmallLootingGoblinBaseWeapon(int level) : base(level)
        {
            Name = "Гоблинские лапки";

            Type = ItemType.BaseWeapon;

            DropChance = -1f;

            IsStacable = false;
            IsEquapable = true;
            IsEquiped = false;
        }
    }
}
