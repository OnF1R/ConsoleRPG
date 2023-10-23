using ConsoleRPG.Items.Currencies;
using Spectre.Console;
using ConsoleRPG.Items.Armors.Capes;
using ConsoleRPG.Items.StacableItems;
using ConsoleRPG.Spells.DamageSpells;
using ConsoleRPG.Enums;

namespace ConsoleRPG.Enemies
{
    internal class WaterDragon : Enemy
    {
        public WaterDragon(int playerLevel) : base(playerLevel)
        {
            Random random = new Random();
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

        public override void FightLogic(Player Player)
        {
            if (!IsDead)
            {
                if (Energy > 0)
                {
                    WaterBall(Player);
                }
                else
                {
                    AnsiConsole.MarkupLine($"{Name} проснулся из спячки");
                }

                Energy++;
            }
            else
            {
                DeathDropLoot(Player);
            }
        }

        public void WaterBall(Player Player)
        {
            BaseSpell Spell = new WaterBall();
			Spell.Use(this, Player);
		}
    }
}
