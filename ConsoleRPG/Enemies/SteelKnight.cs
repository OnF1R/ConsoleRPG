using ConsoleRPG.Items.Armors.Helmets;
using ConsoleRPG.Items.Armors.Boots;
using ConsoleRPG.Items.Currencies;
using ConsoleRPG.Items.Shields;
using ConsoleRPG.Items.StacableItems;
using ConsoleRPG.Items.Weapons;
using Spectre.Console;

namespace ConsoleRPG.Enemies
{
    internal class SteelKnight : Enemy
    {
        public SteelKnight(int playerLevel) : base(playerLevel)
        {
            Random random = new Random();
            Equipment equipment = new Equipment();
            Name = "[grey]Стальной[/] рыцарь";
            MaxHealth = random.Next(4, 11) * Level;
            CurrentHealth = MaxHealth;
            GetComponent<PhysicalDamageCharacteristic>().PhysicalDamage = random.Next(Level, Level + 3);

            //Экипировка

            switch (random.Next(0, 3))
            {
                case 0:
                    Equipment.WearEquip(this, new SteelAxe(Level), EquipmentSlot.LeftHand);
                    break;
                case 1:
                    Equipment.WearEquip(this, new SteelDagger(Level), EquipmentSlot.LeftHand);
                    break;
                case 2:
                    Equipment.WearEquip(this, new SteelSword(Level), EquipmentSlot.LeftHand);
                    break;
            }
            Equipment.WearEquip(this, new SteelHelmet(Level), EquipmentSlot.Helmet);
            Equipment.WearEquip(this, new SteelShield(Level), EquipmentSlot.RightHand);
            Equipment.WearEquip(this, new SpikedSandals(Level), EquipmentSlot.Boots);

            DropList = new Item[]
            {
                new Gold(),
                new IronOre(),
                Equipment.Equip[EquipmentSlot.LeftHand],
                Equipment.Equip[EquipmentSlot.RightHand],
                Equipment.Equip[EquipmentSlot.Helmet],
                Equipment.Equip[EquipmentSlot.Boots],
            };
        }

        public override void FightLogic(Player Player, Dictionary<DamageTypes, int> TakedDamage)
        {
            if (Energy >= 3)
            {
                AnsiConsole.MarkupLine("{0} использовал Блок щитом!", Name);
                foreach (DamageTypes type in TakedDamage.Keys)
                {
                    if (!IsDead) TakeDamage(Player, TakedDamage[type] / 2, type);
                }
                Energy = 0;
            }
            else
            {
                foreach (DamageTypes type in TakedDamage.Keys)
                {
                    if (!IsDead) TakeDamage(Player, TakedDamage[type], type);
                }
            }

            if (!IsDead)
            {
                Player.BeforeAttackBehaviour(this);

                Attack(Player);

                AfterAttackBehaviour(Player);
                Energy++;
            }
            else
            {
                DeathDropLoot(Player);
            }
        }
    }
}
