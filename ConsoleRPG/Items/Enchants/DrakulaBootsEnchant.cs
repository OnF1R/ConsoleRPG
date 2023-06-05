using Spectre.Console;

namespace ConsoleRPG.Items.Enchants
{
    internal class DrakulaBootsEnchant : BaseEnchant
    {
        public DrakulaBootsEnchant()
        {
            Name += "Сапоги дракулы";
            Cost = 2;
            EnchantChance = 10;
            EnchantBoost = 2;

            EnchantPurpose.Add(ItemType.Boots);

            NeededItems.Add(ItemIdentifier.BloodStone, 5);

        }

        protected override void LevelUp()
        {
            if (EnchantChance < 100 && CurrentLevel % 2 == 0)
            {
                EnchantChance++;
                AnsiConsole.MarkupLine($"{Name}, улучшен шанс успешного зачарования, текущий шанс {EnchantChance}");
            }

            if (CurrentLevel % 25 == 0)
            {
                NeededItems[ItemIdentifier.BloodStone] -= 1;
                AnsiConsole.MarkupLine($"{Name}, улучшен, теперь необходимо меньше ресурсов");
            }

            if (CurrentLevel % 25 == 0)
            {
                EnchantBoost++;
                AnsiConsole.MarkupLine($"{Name}, улучшен бонус от зачарования, текущий бонус {EnchantBoost} к вампиризму");
            }
        }

        public override bool IsEnchantable(Item item)
        {
            if (item.GetEnchant<DrakulaBootsEnchant>() != null)
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
            if (new Random().Next(1, 101) + luck < EnchantChance)
            {
                if (item.GetComponent<VampirismCharacteristic>() != null)
                {
                    item.GetComponent<VampirismCharacteristic>().VampirismPercent += EnchantBoost;
                }
                else
                {
                    item.AddComponent(new VampirismCharacteristic() { VampirismPercent = EnchantBoost });
                }

                item.AddEnchant(this);

                AnsiConsole.MarkupLine($"Предмет {item.Name} получил от зачарования, {EnchantBoost}% к вампиризму");
            }
            else
            {
                AnsiConsole.MarkupLine($"[red]Неудача[/], попробуйте ещё раз :(");
            }
        }
    }
}
