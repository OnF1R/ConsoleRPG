using ConsoleRPG.Enums;
using ConsoleRPG.Items.Armors.Leggs;
using ConsoleRPG.Items.Armors.Trinkets;
using ConsoleRPG.Items.Currencies;
using ConsoleRPG.Items.StacableItems;
using ConsoleRPG.Items.Weapons.BaseEnemyWeapons;
using ConsoleRPG.Spells.DamageSpells;


namespace ConsoleRPG.Enemies
{
    [Serializable]
    internal class Mekanism : Enemy
    {
        public Mekanism(int level) : base(level)
        {
            SerializableRandom random = new SerializableRandom();
            //Equipment equipment = new Equipment();
            Name = "[bold]Меканизм[/]";
            MaxHealth = random.Next(7, 11) * Level;
            CurrentHealth = MaxHealth;
			ID = EnemyIdentifierEnum.Mekanism;
			GetComponent<PhysicalDamageCharacteristic>().PhysicalDamage = random.Next(2 + Level, 5 + Level);
            MyRace = new Races.Elemental();

            Equipment.WearEquip(this, new MekanismBaseWeapon(Level), EquipmentSlot.LeftHand);

            Equipment.WearEquip(this, new MechanicalLeggs(Level), EquipmentSlot.Leggs);
            Equipment.WearEquip(this, new EmelardNecklace(Level), EquipmentSlot.Trinket);

            DropList = new Item[]
            {
                new Gold(),
                new RainbowShard(),
                new RainbowStone(),
                new Cog(),
                Equipment.Equip[EquipmentSlot.Leggs],
                Equipment.Equip[EquipmentSlot.Trinket]
            };
        }

        public override void FightLogic(Player Player, Unit unit)
        {

            if (!IsDead)
            {
                if (Energy >= 3)
                {
                    Laser(unit);
                    Energy = 0;
                }
                else
                {
                    foreach (var entity in GetDamageEntities())
                        Attack(unit, entity);
                }

                Energy++;
            }
            else
            {
                Death(Player);
            }
        }

        public void Laser(Unit unit)
        {
            BaseSpell Spell = new Laser();
			Spell.Use(this, unit);
		}
    }
}
