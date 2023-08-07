using ConsoleRPG.Items.Armors.Leggs;
using ConsoleRPG.Items.Armors.Trinkets;
using ConsoleRPG.Items.Currencies;
using ConsoleRPG.Items.StacableItems;
using ConsoleRPG.Items.Weapons.BaseEnemyWeapons;
using ConsoleRPG.Spells.DamageSpells;


namespace ConsoleRPG.Enemies
{
    internal class Mekanism : Enemy
    {
        public Mekanism(int level) : base(level)
        {
            Random random = new Random();
            //Equipment equipment = new Equipment();
            Name = "[bold]Меканизм[/]";
            MaxHealth = random.Next(7, 11) * Level;
            CurrentHealth = MaxHealth;
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

        public override void FightLogic(Player Player)
        {

            if (!IsDead)
            {
                if (Energy >= 3)
                {
                    Laser(Player);
                    Energy = 0;
                }
                else
                {
                    foreach (var entity in GetDamageEntities())
                        Attack(Player, entity);
                }

                Energy++;
            }
            else
            {
                DeathDropLoot(Player);
            }
        }

        public void Laser(Player Player)
        {
            BaseSpell Spell = new Laser();
            Dictionary<DamageTypes, int> elemDamage = Spell.GetComponent<ElementalDamageCharacteristic>().ElemDamage;
            foreach (DamageTypes type in elemDamage.Keys)
            {
                DealDamage(Player, elemDamage[type], type, Spell);
            }
        }
    }
}
