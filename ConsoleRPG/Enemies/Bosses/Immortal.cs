using Spectre.Console;
using ConsoleRPG.Items.StacableItems;
using ConsoleRPG.Races;
using ConsoleRPG.Items.Armors.Chestsplates;

namespace ConsoleRPG.Enemies.Bosses
{
    internal class Immortal : Enemy
    {
        public Immortal(int playerLevel) : base(playerLevel)
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

            DropList = new Item[]
            {
                new RainbowStone(),
                new RainbowStone(),
                new RainbowStone(),
                new RainbowStone(),
                new RainbowStone(),
                Equipment.Equip[EquipmentSlot.Chest],
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

                if (Energy > 8)
                {
                    Smash(Player);
                    Energy = 0;
                }
                else
                {
                    Attack(Player);
                }

                AfterAttackBehaviour(Player);

                Energy++;
            }
            else
            {
                DeathDropLoot(Player);
            }
        }

        public void Smash(Player Player)
        {
            int damage = (GetPhysicalDamage() + GetArmor()) * 2;
            AnsiConsole.MarkupLine($"{Name}: [bold]ИДУ ДАВИТЬ![/]");
            Player.TakeDamage(this, damage, DamageTypes.Physical);
        }
    }
}
