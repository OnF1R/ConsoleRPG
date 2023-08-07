using ConsoleRPG.Items.Armors.Gloves;
using ConsoleRPG.Items.Armors.Leggs;
using ConsoleRPG.Items.Armors.Trinkets;
using ConsoleRPG.Items.Currencies;
using ConsoleRPG.Items.StacableItems;
using ConsoleRPG.Items.Weapons.BaseEnemyWeapons;
using ConsoleRPG.Spells.DamageSpells;


namespace ConsoleRPG.Enemies
{
    internal class SmallLootingGoblin : Enemy
    {
        public SmallLootingGoblin(int playerLevel) : base(playerLevel)
        {
            Random random = new Random();
            //Equipment equipment = new Equipment();
            Name = "[bold]Маленький богатый гоблин[/]";
            MaxHealth = random.Next(3, 6) * Level;
            CurrentHealth = MaxHealth;
            GetComponent<PhysicalDamageCharacteristic>().PhysicalDamage = random.Next(Level, 2 + Level);
            MyRace = new Races.Goblin();


            Equipment.WearEquip(this, new SmallLootingGoblinBaseWeapon(Level), EquipmentSlot.LeftHand);


            Equipment.WearEquip(this, new EmelardNecklace(Level), EquipmentSlot.Trinket);
            Equipment.WearEquip(this, new CrafterGloves(Level), EquipmentSlot.Gloves);

            DropList = new Item[]
            {
                new Gold(),
                new Gold(),
                new RainbowShard(),
                new RainbowShard(),
                new RainbowStone(),
                new RainbowStone(),
                new AdamantiteIngot(),
                new AdamantiteIngot(),
                new IronIngot(),
                new IronIngot(),
                new BloodStone(),
                new BloodStone(),
                Equipment.Equip[EquipmentSlot.Trinket],
                Equipment.Equip[EquipmentSlot.Gloves],
            };
        }

        public override void FightLogic(Player Player)
        {
            if (!IsDead)
            {
                foreach (var entity in GetDamageEntities())
                    Attack(Player, entity);
            }
            else
            {
                DeathDropLoot(Player);
            }
        }
    }
}
