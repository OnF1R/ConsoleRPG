using ConsoleRPG.Enums;
using Spectre.Console;

namespace ConsoleRPG.Items.Enchants
{
    [Serializable]
    internal class FoolAndElementEnchant : BaseEnchant
    {
        public FoolAndElementEnchant()
        {
            Name += "Дурак и стихии";
            Cost = 1;
            EnchantChance = 15;
            EnchantBoost = 3;

            EnchantPurpose.Add(ItemType.Armor);

            NeededItems.Add(ItemIdentifier.RainbowShard, 8);

        }

        protected override void LevelUp()
        {
            if (EnchantChance < 100 && CurrentLevel % 2 == 0)
            {
                EnchantChance++;
                AnsiConsole.MarkupLine($"{Name}, улучшен шанс успешного зачарования, текущий шанс {EnchantChance}");
            }

            if (CurrentLevel % 15 == 0)
            {
                NeededItems[ItemIdentifier.RainbowShard] -= 1;
                AnsiConsole.MarkupLine($"{Name}, улучшен, теперь необходимо меньше ресурсов");
            }

            if (CurrentLevel % 10 == 0)
            {
                EnchantBoost++;
                AnsiConsole.MarkupLine($"{Name}, улучшен бонус от зачарования, текущий бонус {EnchantBoost} к элементальному урону/сопротивлению");
            }
        }

        public override bool IsEnchantable(Item item)
        {
            if (item.GetEnchant<FoolAndElementEnchant>() != null)
            {
                return false;
            }

            return true;
        }

        public override string EnchantNeededItems()
        {
            string info = "";

            foreach (var item in NeededItems)
            {
                info += $"{ItemID.Names[item.Key]}: {item.Value} шт.\n";
            }

            return info;
        }

        public override void EnchantItem(Item item, int luck)
        {
            if (new SerializableRandom().Next(1, 101) + luck < EnchantChance)
            {
                DamageTypesNames damageTypesNames = new DamageTypesNames();
                DamageTypes damageType = damageTypesNames.GetRandomElementalDamageType();

                string boostType;

                if (new SerializableRandom().Next(1, 3) == 1)
                {
                    item.AddNeededElementalResistance(item, damageType, EnchantBoost, EnchantBoost);
                    boostType = $"% к элементальному сопротивлению ({damageTypesNames.Names[damageType]}";
                }
                else
                {
                    item.AddNeededElementalTypesDamage(item, damageType, EnchantBoost, EnchantBoost);
                    boostType = $" к элементальному урону ({damageTypesNames.Names[damageType]})";
                }

                item.AddEnchant(this);

                AnsiConsole.MarkupLine($"Предмет {item.Name} получил от зачарования, {EnchantBoost}{boostType}");
            }
            else
            {
                AnsiConsole.MarkupLine($"[red]Неудача[/], попробуйте ещё раз :(");
            }
        }
    }
}
