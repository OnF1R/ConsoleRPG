using Spectre.Console;
using ConsoleRPG.Items.StacableItems;
using ConsoleRPG.Races;
using ConsoleRPG.Items.Armors.Chestsplates;
using ConsoleRPG.Items.Weapons;
using ConsoleRPG.Spells.DamageSpells;
using ConsoleRPG.Enums;

namespace ConsoleRPG.Enemies.Bosses
{
    [Serializable]
    internal class Immortal : Enemy
    {
        public Immortal(int level) : base(level)
        {
            SerializableRandom random = new SerializableRandom();
            MyRace = new Human();
            IsBoss = true;
            Equipment equipment = new Equipment();
            Name = "[red]БОСС[/] [bold]Бессмертный[/]";
            MaxHealth = random.Next(30, 51) * Level;
            CurrentHealth = MaxHealth;
			ID = EnemyIdentifierEnum.Immortal;
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

        public override void FightLogic(Player Player, Unit unit)
        {
            if (!IsDead)
            {
                if (Energy > 8)
                {
                    Smash(unit);
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

        public void Smash(Unit unit)
        {
            BaseSpell Spell = new Smash(this);
            Spell.Use(this, unit);
        }
    }
}
