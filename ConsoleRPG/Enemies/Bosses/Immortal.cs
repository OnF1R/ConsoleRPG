using Spectre.Console;
using ConsoleRPG.Items.StacableItems;
using ConsoleRPG.Races;
using ConsoleRPG.Items.Armors.Chestsplates;
using ConsoleRPG.Items.Weapons;
using ConsoleRPG.Spells.DamageSpells;

namespace ConsoleRPG.Enemies.Bosses
{
    internal class Immortal : Enemy
    {
        public Immortal(int level) : base(level)
        {
            Random random = new Random();
            MyRace = new Human();
            IsBoss = true;
            Equipment equipment = new Equipment();
            Name = "[red]БОСС[/] [bold]Бессмертный[/]";
            MaxHealth = random.Next(30, 51) * Level;
            CurrentHealth = MaxHealth;
            GetComponent<PhysicalDamageCharacteristic>().PhysicalDamage = random.Next(4 + Level, Level + 8);
            GetComponent<MissCharacteristic>().MissChance += 15;
            GetComponent<ArmorCharacteristic>().Armor += 25;

            //Экипировка

            Equipment.WearEquip(this, new ImmortalChestplate(Level), EquipmentSlot.Chest);
            Equipment.WearEquip(this, new BigSteelAxe(Level), EquipmentSlot.RightHand);

            DropList = new Item[]
            {
                new RainbowStone(),
                new RainbowStone(),
                new RainbowStone(), 
                new RainbowStone(), 
                new RainbowStone(),
                Equipment.Equip[EquipmentSlot.Chest],
                Equipment.Equip[EquipmentSlot.RightHand],
            };
        }

        public override void FightLogic(Player Player)
        {
            if (!IsDead)
            {
                if (Energy > 8)
                {
                    Smash(Player);
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

        public void Smash(Player Player)
        {
            BaseSpell Spell = new Smash(this);
            int damage = Spell.GetComponent<PhysicalDamageCharacteristic>().PhysicalDamage;
            DealDamage(Player, damage, DamageTypes.Physical, Spell);
        }
    }
}
