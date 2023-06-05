using ConsoleRPG.Items.Currencies;
using Spectre.Console;
using ConsoleRPG.Items.Armors.Capes;
using ConsoleRPG.Items.StacableItems;
using ConsoleRPG.Spells.DamageSpells;

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

        public override void FightLogic(Player Player, Dictionary<DamageTypes, int> TakedDamage)
        {
            foreach (DamageTypes type in TakedDamage.Keys)
            {
                if (!IsDead) TakeDamage(Player, TakedDamage[type], type);
            }

            if (!IsDead)
            {
                Player.BeforeAttackBehaviour(this);

                if (Energy > 0)
                {
                    WaterBall(Player);

                    AfterAttackBehaviour(Player);
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
            Spell Spell = new WaterBall();
            Dictionary<DamageTypes, int> elemDamage = Spell.GetComponent<ElementalDamageCharacteristic>().ElemDamage;
            foreach (DamageTypes type in elemDamage.Keys)
            {
                Player.TakeDamage(this, elemDamage[type] + Level + GetPhysicalDamage(), type);
            }
        }
    }
}
