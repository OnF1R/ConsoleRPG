using ConsoleRPG.Enums;
using ConsoleRPG.Items.Armors.Boots;
using ConsoleRPG.Items.Currencies;
using ConsoleRPG.Items.Shields;
using ConsoleRPG.Items.Weapons;
using Spectre.Console;

namespace ConsoleRPG.Enemies
{
    [Serializable]
    internal class SpikedKnight : Enemy
    {
        public SpikedKnight(int playerLevel) : base(playerLevel)
        {
            SerializableRandom random = new SerializableRandom();
            Equipment equipment = new Equipment();
            Name = "[grey]Шипастый рыцарь[/]";
            MaxHealth = random.Next(4, 11) * Level;
            CurrentHealth = MaxHealth;
			ID = EnemyIdentifierEnum.SpikedKnight;
			GetComponent<PhysicalDamageCharacteristic>().PhysicalDamage = random.Next(Level, 4 + Level);
            GetComponent<SpikeCharacteristic>().SpikeDamage += Level / 3;

            //Экипировка
            Equipment.WearEquip(this, new SteelDagger(Level), EquipmentSlot.LeftHand);
            Equipment.WearEquip(this, new SpikedSandals(Level), EquipmentSlot.Boots);
            Equipment.WearEquip(this, new SpikedShield(Level), EquipmentSlot.RightHand);

            DropList = new Item[]
            {
                new Gold(),
                Equipment.Equip[EquipmentSlot.Boots],
                Equipment.Equip[EquipmentSlot.RightHand],
            };
        }

        public override void FightLogic(Player Player, Unit unit)
        {
            if (!IsDead)
            {
                if (Energy >= 4)
                {
                    SpikedAttack(unit);
                    Energy = 0;
                }
                else
                {
                    foreach (var entity in GetDamageEntities())
                        Attack(unit, entity);

                    Energy++;
                }
            }

            else
            {
                Death(Player);
            }
        }

        public void SpikedAttack(Unit unit)
        {
            AnsiConsole.MarkupLine($"{Name} использует [bold]Шипастый набег[/]");
            for (int i = 0; i < 5; i++)
				TakeSpikeDamage(unit);
        }
    }
}
