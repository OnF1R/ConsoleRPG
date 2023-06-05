using Spectre.Console;
using ConsoleRPG.Items.StacableItems;
using ConsoleRPG.Races;
using ConsoleRPG.Items.Armors.Capes;
using ConsoleRPG.Spells.DamageSpells;

namespace ConsoleRPG.Enemies.Bosses
{
    internal class AbyUdRiRisaAlha : Enemy
    {
        public AbyUdRiRisaAlha(int playerLevel) : base(playerLevel)
        {
            Random random = new Random();
            MyRace = new Human();
            IsBoss = true;
            Equipment equipment = new Equipment();
            Name = "[red]БОСС[/] [bold]Абу Уд'ри Риса Аль-ха[/]";
            MaxHealth = random.Next(20, 45) * Level;
            CurrentHealth = MaxHealth;
            GetComponent<PhysicalDamageCharacteristic>().PhysicalDamage = random.Next(2 + Level, Level + 6);

            //Экипировка

            Equipment.WearEquip(this, new RaAyatAlQaiwai(Level), EquipmentSlot.Cape);

            DropList = new Item[]
            {
                new RainbowStone(),
                new RainbowStone(),
                new RainbowStone(),
                new RainbowStone(),
                new RainbowStone(),
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

                if (Energy > 6)
                {
                    ArdaBintBaaniAlTaa(Player);
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

        public void ArdaBintBaaniAlTaa(Player Player)
        {
            Spell Spell = new ArdaBintBaaniAlTaa();
            Dictionary<DamageTypes, int> elemDamage = Spell.GetComponent<ElementalDamageCharacteristic>().ElemDamage;
            foreach (DamageTypes type in elemDamage.Keys)
            {
                Player.TakeDamage(this, elemDamage[type] + Level + GetPhysicalDamage(), type);
            }
        }
    }
}
