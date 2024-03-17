using ConsoleRPG.Enums;
using Spectre.Console;

namespace ConsoleRPG.Items.Enchants
{
    [Serializable]
    internal class DodgingTheRainEnchant : BaseEnchant
    {
        public DodgingTheRainEnchant()
        {
            Name += "Уклоняющийся от дождя";
            Cost = 2;
            EnchantChance = 10;
            EnchantBoost = 2;

            EnchantPurpose.Add(ItemType.Armor);

            NeededItems.Add(ItemIdentifier.DesertRune, 8);

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
                NeededItems[ItemIdentifier.DesertRune] -= 1;
                AnsiConsole.MarkupLine($"{Name}, улучшен, теперь необходимо меньше ресурсов");
            }

            if (CurrentLevel % 25 == 0)
            {
                EnchantBoost++;
                AnsiConsole.MarkupLine($"{Name}, улучшен бонус от зачарования, текущий бонус {EnchantBoost} к уклонению");
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
            if (new SerializableRandom().Next(1, 101) + luck < EnchantChance)
            {
                if (item.GetComponent<EvasionCharacteristic>() != null)
                {
                    item.GetComponent<EvasionCharacteristic>().EvasionChance += EnchantBoost;
                }
                else
                {
                    item.AddComponent(new EvasionCharacteristic() { EvasionChance = EnchantBoost });
                }

                item.AddEnchant(this);

                AnsiConsole.MarkupLine($"Предмет {item.Name} получил от зачарования, {EnchantBoost}% к уклонению");
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Неудача[/], попробуйте ещё раз :(");
            }
        }
    }
}
