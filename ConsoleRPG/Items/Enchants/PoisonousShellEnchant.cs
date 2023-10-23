using ConsoleRPG.Enums;
using Spectre.Console;

namespace ConsoleRPG.Items.Enchants
{
    internal class PoisonousShell : BaseEnchant
    {
        public PoisonousShell()
        {
            Name += "Ядовитая оболочка";
            Cost = 3;
            EnchantChance = 100;
            EnchantBoost = 2;

            EnchantPurpose.Add(ItemType.Armor);

            NeededItems.Add(ItemIdentifier.SpiderPoison, 4);
            NeededItems.Add(ItemIdentifier.SpiderEye, 4);

        }

        protected override void LevelUp()
        {
            if (EnchantChance < 100 && CurrentLevel % 2 == 0)
            {
                EnchantChance++;
                AnsiConsole.MarkupLine($"{Name}, улучшен шанс успешного зачарования, текущий шанс {EnchantChance}");
            }

            if (CurrentLevel % 50 == 0)
            {
                NeededItems[ItemIdentifier.SpiderPoison] -= 1;
                NeededItems[ItemIdentifier.SpiderEye] -= 1;
                AnsiConsole.MarkupLine($"{Name}, улучшен, теперь необходимо меньше ресурсов");
            }

            if (CurrentLevel % 5 == 0)
            {
                EnchantBoost++;
                AnsiConsole.MarkupLine($"{Name}, улучшен бонус от зачарования, текущий бонус {EnchantBoost} к урону или сопротивлению от ({new DamageTypesNames().Names[DamageTypes.Poison]})");
            }
        }

        public override bool IsEnchantable(Item item)
        {
            if (item.GetEnchant<PoisonousShell>() != null)
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
				DamageTypesNames damageTypesNames = new DamageTypesNames();
                DamageTypes damageType = DamageTypes.Poison;

				string boostType;

				if (new Random().Next(1, 3) != 0)
				{
					item.AddNeededElementalResistance(item, damageType, EnchantBoost, EnchantBoost);
					boostType = $"% к элементальному сопротивлению ({damageTypesNames.Names[damageType]})";
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
                AnsiConsole.MarkupLine("[red]Неудача[/], попробуйте ещё раз :(");
            }
        }
    }
}
