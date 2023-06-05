using ConsoleRPG.Items.Armors.Helmets;
using ConsoleRPG.Items.Armors.Rings;
using ConsoleRPG.Items.Currencies;
using ConsoleRPG.Items.StacableItems;
using ConsoleRPG.Items.Weapons;
using ConsoleRPG.Races;
using ConsoleRPG.Spells.DamageSpells;


namespace ConsoleRPG.Enemies
{
    internal class Elemental : Enemy
    {
        public Elemental(int playerLevel) : base(playerLevel)
        {
            Random random = new Random();
            //Equipment equipment = new Equipment();
            Name = "[bold]Элементаль[/]";
            MaxHealth = random.Next(7, 11) * Level;
            CurrentHealth = MaxHealth;
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
                    ElementalSplash(Player);
                    Energy = 0;
                }
                else
                {
                    ElementalSplash(Player);
                }
                Energy++;

                AfterAttackBehaviour(Player);
            }
            else
            {
                DeathDropLoot(Player);
            }
        }

        public void ElementalSplash(Player Player)
        {
            Spell Spell = new ElementalSplash();
            Dictionary<DamageTypes, int> elemDamage = Spell.GetComponent<ElementalDamageCharacteristic>().ElemDamage;
            foreach (DamageTypes type in elemDamage.Keys)
            {
                Player.TakeDamage(this, elemDamage[type] + Level + GetPhysicalDamage(), type);
            }
            //if (IsCrit())
            //{
            //}
            //else
            //{
            //}
        }
    }
}
