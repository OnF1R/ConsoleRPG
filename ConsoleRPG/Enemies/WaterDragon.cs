using ConsoleRPG.Items.Currencies;
using Spectre.Console;
using ConsoleRPG.Items.Armors.Capes;
using ConsoleRPG.Items.StacableItems;
using ConsoleRPG.Spells.DamageSpells;
using ConsoleRPG.Enums;

namespace ConsoleRPG.Enemies
{
    [Serializable]
    internal class WaterDragon : Enemy
    {
        public WaterDragon(int playerLevel) : base(playerLevel)
        {
            SerializableRandom random = new SerializableRandom();
            Equipment equipment = new Equipment();
            Name = "[dodgerblue1]Водный[/] дракон";
            MaxHealth = random.Next(8, 18) * Level;
            CurrentHealth = MaxHealth;
			ID = EnemyIdentifierEnum.WaterDragon;
			GetComponent<PhysicalDamageCharacteristic>().PhysicalDamage = random.Next(2 + Level, Level + 6);
            var elemResistance = GetComponent<ElementalResistanceCharacteristic>().ElemResistance;
            elemResistance[elemResistance.First(x => x.Key == DamageTypes.Water).Key] += 25;

            //Экипировка

            Equipment.WearEquip(this, new WetCape(Level), EquipmentSlot.Cape);

            DropList = new Item[]
            {
                new Gold(),
                new OceanRune(),
                new RainbowShard(),
                new DragonClaw(),
                Equipment.Equip[EquipmentSlot.Cape],
            };
        }

        public override void FightLogic(Player Player, Unit unit)
        {
            if (!IsDead)
            {
                if (Energy > 0)
                {
                    WaterBall(unit);
                }
                else
                {
                    AnsiConsole.MarkupLine($"{Name} проснулся из спячки");
                }

                Energy++;
            }
            else
            {
                Death(Player);
            }
        }

        public void WaterBall(Unit unit)
        {
            BaseSpell Spell = new WaterBall();
			Spell.Use(this, unit);
		}
    }
}
