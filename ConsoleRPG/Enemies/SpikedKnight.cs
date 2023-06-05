using ConsoleRPG.Items.Armors.Boots;
using ConsoleRPG.Items.Armors.Rings;
using ConsoleRPG.Items.Currencies;
using ConsoleRPG.Items.Shields;
using ConsoleRPG.Items.StacableItems;
using ConsoleRPG.Spells.DamageSpells;
using Spectre.Console;

namespace ConsoleRPG.Enemies
{
    internal class SpikedKnight : Enemy
    {
        public SpikedKnight(int playerLevel) : base(playerLevel)
        {
            Random random = new Random();
            Equipment equipment = new Equipment();
            Name = "[grey]Шипастый рыцарь[/]";
            MaxHealth = random.Next(4, 11) * Level;
            CurrentHealth = MaxHealth;
            GetComponent<PhysicalDamageCharacteristic>().PhysicalDamage = random.Next(Level, 4 + Level);

            //Экипировка
            Equipment.WearEquip(this, new SpikedSandals(Level), EquipmentSlot.Boots);
            Equipment.WearEquip(this, new SpikedShield(Level), EquipmentSlot.RightHand);

            DropList = new Item[]
            {
                new Gold(),
                Equipment.Equip[EquipmentSlot.Boots],
                Equipment.Equip[EquipmentSlot.RightHand],
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

                if (Energy >= 4)
                {
                    SpikedAttack(Player);
                    Energy = 0;
                }
                else
                {
                    Attack(Player);
                    Energy++;
                }

                AfterAttackBehaviour(Player);
            }

            else
            {
                DeathDropLoot(Player);
            }
        }

        public void SpikedAttack(Player player)
        {
            AnsiConsole.MarkupLine($"{Name} использует Шипастый набег");
            TakeSpikeDamage(player);
            TakeSpikeDamage(player);
            TakeSpikeDamage(player);
            TakeSpikeDamage(player);
            TakeSpikeDamage(player);
        }
    }
}
