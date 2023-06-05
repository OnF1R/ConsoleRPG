using ConsoleRPG.Items.Armors.Leggs;
using ConsoleRPG.Items.Armors.Trinkets;
using ConsoleRPG.Items.Currencies;
using ConsoleRPG.Items.StacableItems;
using ConsoleRPG.Spells.DamageSpells;


namespace ConsoleRPG.Enemies
{
    internal class Mekanism : Enemy
    {
        public Mekanism(int playerLevel) : base(playerLevel)
        {
            Random random = new Random();
            //Equipment equipment = new Equipment();
            Name = "[bold]Меканизм[/]";
            MaxHealth = random.Next(7, 11) * Level;
            CurrentHealth = MaxHealth;
            GetComponent<PhysicalDamageCharacteristic>().PhysicalDamage = random.Next(2 + Level, 5 + Level);
            MyRace = new Races.Elemental();


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

        public override void FightLogic(Player Player, Dictionary<DamageTypes, int> TakedDamage)
        {
            foreach (DamageTypes type in TakedDamage.Keys)
            {
                if (!IsDead)
                    TakeDamage(Player, TakedDamage[type], type);
            }
            if (!IsDead)
            {
                Player.AfterAttackBehaviour(this);

                if (Energy >= 3)
                {
                    Laser(Player);
                    Energy = 0;
                }
                else
                {
                    Attack(Player);
                }

                Energy++;

                AfterAttackBehaviour(Player);
            }
            else
            {
                DeathDropLoot(Player);
            }
        }

        public void Laser(Player Player)
        {
            Spell Spell = new Laser();
            Dictionary<DamageTypes, int> elemDamage = Spell.GetComponent<ElementalDamageCharacteristic>().ElemDamage;
            foreach (DamageTypes type in elemDamage.Keys)
            {
                Player.TakeDamage(this, elemDamage[type] + Level + GetPhysicalDamage(), type);
            }
        }
    }
}
