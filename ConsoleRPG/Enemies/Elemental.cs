using ConsoleRPG.Enums;
using ConsoleRPG.Items.Armors.Helmets;
using ConsoleRPG.Items.Armors.Rings;
using ConsoleRPG.Items.Currencies;
using ConsoleRPG.Items.StacableItems;
using ConsoleRPG.Items.Weapons;
using ConsoleRPG.Races;
using ConsoleRPG.Spells.DamageSpells;


namespace ConsoleRPG.Enemies
{
    [Serializable]
    internal class Elemental : Enemy
    {
        public Elemental(int level) : base(level)
        {
            SerializableRandom random = new SerializableRandom();
            //Equipment equipment = new Equipment();
            Name = "[bold]Элементаль[/]";
            MaxHealth = random.Next(7, 11) * Level;
            CurrentHealth = MaxHealth;
			ID = EnemyIdentifierEnum.Elemental;
			GetComponent<PhysicalDamageCharacteristic>().PhysicalDamage = random.Next(2 + Level, 5 + Level);
            MyRace = new Races.Elemental();
            Equipment.WearEquip(this, new EnchantedHat(Level), EquipmentSlot.Helmet);
            Equipment.WearEquip(this, new ElementalRing(Level), EquipmentSlot.FirstRing);

            DropList = new Item[]
            {
                new Gold(),
                new RainbowShard(),
                Equipment.Equip[EquipmentSlot.Helmet],
                Equipment.Equip[EquipmentSlot.FirstRing]
            };
        }

        public override void FightLogic(Player Player, Unit unit)
        {
            if (!IsDead)
            {
                if (Energy >= 3)
                {
                    ElementalSplash(unit);
                    Energy = 0;
                }
                else
                {
                    ElementalSplash(unit);
                }
                Energy++;
            }
            else
            {
                Death(Player);
            }
        }

        public void ElementalSplash(Unit unit)
        {
            BaseSpell Spell = new ElementalSplash();
            Spell.Use(this, unit);
        }
    }
}
