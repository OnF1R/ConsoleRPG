using Console = Colorful.Console;
using Spectre.Console;
using ConsoleRPG.Items.Currencies;
using ConsoleRPG.Items.CraftRecipes;
using ConsoleRPG.Items.Enchants;
using System.Linq;
using System.Xml;

namespace ConsoleRPG
{
    internal class Inventory
    {
        public List<Item> ListInventory { get; set; }

        public List<BaseRecipe> ListRecipes { get; set; }

        public List<BaseEnchant> ListEnchants { get; set; }

        public List<BaseSpell> ListSpells { get; set; }

        public int maxSlotsInInventory { get; set; }

        public Inventory()
        {
            ListInventory = new List<Item>();
            ListRecipes = new List<BaseRecipe>();
            ListEnchants = new List<BaseEnchant>();
            ListSpells = new List<BaseSpell>();
            maxSlotsInInventory = 40;
            ListInventory.Capacity = maxSlotsInInventory;
        }

        public void UpgradeInventoryCapacity(int upgradeSlots)
        {
            maxSlotsInInventory += upgradeSlots;
            ListInventory.Capacity = maxSlotsInInventory;
        }

        public List<string> GetNamesWithCost(bool IsStacable)
        {
            List<string> namesWithCost = new List<string>();
            if (IsStacable)
            {
                foreach (Item item in ListInventory)
                {
                    if (!item.IsEquiped)
                    {
                        namesWithCost.Add(item.Name + " [gold1]" + item.GetComponent<ValueCharacteristic>().Cost + " золота[/]");
                    }
                }
            }
            else
            {
                foreach (Item item in ListInventory)
                {
                    if (!item.IsStacable && !item.IsEquiped)
                    {
                        namesWithCost.Add(item.Name + " [gold1]" + item.GetComponent<ValueCharacteristic>().Cost + " золота[/]");
                    }
                }
            }

            return namesWithCost;
        }

        public void GetItemsAndCost(out List<string> namesWithCost, out List<Item> items)
        {
            namesWithCost = GetNamesWithCost(false);
            items = new List<Item>();

            for (int i = 0; i < ListInventory.Count; i++)
            {
                if (!ListInventory[i].IsStacable && !ListInventory[i].IsEquiped)
                    items.Add(ListInventory[i]);
            }
        }

        public List<Item> GetNotStacableItems()
        {
            List<Item> items = new List<Item>();

            for (int i = 0; i < ListInventory.Count; i++)
            {
                if (!ListInventory[i].IsStacable && !ListInventory[i].IsEquiped)
                    items.Add(ListInventory[i]);
            }
            return items;
        }

        public List<string> GetNumerableItems()
        {
            List<string> items = GetNamesWithCost(false);
            List<string> numerableItems = new List<string>();
            for (int i = 0; i < items.Count; i++)
            {
                numerableItems.Add($"{i + 1}. {items[i]}");
            }

            return numerableItems;
        }

        public List<Item> GetCurrencies()
        {
            List<Item> CurrenciesList = new List<Item>();
            foreach (Item item in ListInventory)
            {
                if (item.Type == ItemType.Currency)
                {
                    CurrenciesList.Add(item);
                }
            }

            return CurrenciesList;
        }

        public bool IsEnoughCurrency(int Cost)
        {
            return GetItemsCost(GetCurrencies().ToArray()) >= Cost;
        }

        public bool IsHave(Item needle)
        {
            Item item = ListInventory.FirstOrDefault(needle);

            if (needle == item)
                return true;

            return false;
        }

        public bool IsHave(Item needle, int count)
        {
            Item item = ListInventory.FirstOrDefault(needle);

            if (needle == item)
                if (count <= item.Count)
                    return true;

            return false;
        }

        public bool IsHaveStacableItemByItemID(ItemIdentifier id, int count)
        {
            foreach (Item item in ListInventory)
            {
                if (item.IsStacable && item.ID == id && item.Count >= count) return true;
            }

            return false;
        }

        public Item SearchStacableItemByItemId(ItemIdentifier id)
        {
            return ListInventory.FirstOrDefault(x => x.ID == id);
        }

        public Item SearchByUniqID(string uniqID)
        {
            foreach (Item item in ListInventory)
            {
                if (item.UniqID == uniqID) return item;
            }

            return null;
        }

        public bool isHaveByUniqID(Item needle)
        {
            foreach (Item item in ListInventory)
            {
                if (needle.UniqID == item.UniqID) return true;
            }

            return false;
        }

        public void BuyRecipe(int Cost, BaseEnchant enchant)
        {
            List<Item> CurrenciesList = GetCurrencies();

            if (GetItemsCost(CurrenciesList.ToArray()) == Cost)
            {
                foreach (Item item in CurrenciesList.ToArray())
                {
                    RemoveItem(item);
                }
            }

            if (GetItemsCost(CurrenciesList.ToArray()) > Cost)
            {
                foreach (Item item in CurrenciesList.ToArray())
                {
                    RemoveItem(item, Cost);
                }
            }

            AddEnchant(enchant);
        }

        public void BuyRecipe(int Cost, BaseRecipe recipe)
        {
            List<Item> CurrenciesList = GetCurrencies();

            if (GetItemsCost(CurrenciesList.ToArray()) == Cost)
            {
                foreach (Item item in CurrenciesList.ToArray())
                {
                    RemoveItem(item);
                }
            }

            if (GetItemsCost(CurrenciesList.ToArray()) > Cost)
            {
                foreach (Item item in CurrenciesList.ToArray())
                {
                    RemoveItem(item, Cost);
                }
            }

            AddCraft(recipe);
        }

        public void BuyItem(int Cost, Item BoughtItem)
        {
            List<Item> CurrenciesList = GetCurrencies();

            if (GetItemsCost(CurrenciesList.ToArray()) == Cost)
            {
                foreach (Item item in CurrenciesList.ToArray())
                {
                    RemoveItem(item);
                }
            }

            if (GetItemsCost(CurrenciesList.ToArray()) > Cost)
            {
                foreach (Item item in CurrenciesList.ToArray())
                {
                    RemoveItem(item, Cost);
                }
            }

            AddItem(BoughtItem);
        }

        public void SellItem(Item item)
        {
            RemoveItem(item);
            Item gold = new Gold();
            gold.Count = 1 * item.GetComponent<ValueCharacteristic>().Cost;
            AddItem(gold);
        }

        public int GetItemsCost(params Item[] items)
        {
            int SumCost = 0;
            foreach (Item item in items)
            {
                if (item.IsStacable)
                {
                    SumCost += item.GetComponent<ValueCharacteristic>().Cost * item.Count;
                }
                else
                {
                    SumCost += item.GetComponent<ValueCharacteristic>().Cost;
                }
            }

            return SumCost;
        }

        public void ShowInventory()
        {
            ListInventory = SortInventory();

            if (ListInventory.Count > 0)
            {
                for (int i = 0; i < ListInventory.Count; i++)
                {
                    int Number = i + 1;
                    if (ListInventory[i].IsStacable)
                    {
                        AnsiConsole.MarkupLine("{0}. {1} ([{2}]{3}[/]) x{4} ([gold1]{5} золота[/])",
                            Number,
                            ListInventory[i].Name,
                            ListInventory[i].RarityColor,
                            ListInventory[i].Rarity,
                            ListInventory[i].Count,
                            ListInventory[i].GetComponent<ValueCharacteristic>().Cost * ListInventory[i].Count
                            );
                        continue;
                    }

                    if (ListInventory[i].IsEquiped)
                    {
                        AnsiConsole.Markup("{0}. {1} ([{2}]{3}[/]) ([gold1]{4} золота[/]) [bold]Экипировано[/]: ",
                            Number,
                            ListInventory[i].Name,
                            ListInventory[i].RarityColor,
                            ListInventory[i].Rarity,
                            ListInventory[i].GetComponent<ValueCharacteristic>().Cost
                            );
                        ListInventory[i].ItemInfo(ListInventory[i]);
                        continue;
                    }

                    AnsiConsole.Markup("{0}. {1} ([{2}]{3}[/]) ([gold1]{4} золота[/]): ",
                            Number,
                            ListInventory[i].Name,
                            ListInventory[i].RarityColor,
                            ListInventory[i].Rarity,
                            ListInventory[i].GetComponent<ValueCharacteristic>().Cost
                            );
                    ListInventory[i].ItemInfo(ListInventory[i]);
                }
            }
            else
            {
                AnsiConsole.MarkupLine("Инвентарь пуст");
            }
        }

        public void GetEquipableItem(string SlotName)
        {

        }

        public List<Item> SortInventory()
        {
            List<Item> StacableItems = new List<Item> { };
            List<Item> NotStacableItems = new List<Item> { };

            foreach (var item in ListInventory)
            {
                if (item.IsStacable)
                {
                    StacableItems.Add(item);
                }
                else
                {
                    NotStacableItems.Add(item);
                }
            }

            List<Item> SortedInvenoty = new List<Item> { };
            SortedInvenoty = SortedInvenoty.Concat(StacableItems).Concat(NotStacableItems).ToList();

            return SortedInvenoty;
        }

        public List<Item> SortInventoryForType(params ItemType[] Type)
        {
            List<Item> SortedInventory = new List<Item> { };

            foreach (var item in ListInventory)
            {
                foreach (var type in Type)
                {
                    if (item.Type == type)
                    {
                        SortedInventory.Add(item);
                    }
                }
            }

            return SortedInventory;

        }

        public List<Item> SortInventoryForEquip(params ItemType[] Type)
        {
            List<Item> SortedInventory = new List<Item> { };

            foreach (var item in ListInventory)
            {
                foreach (var type in Type)
                {
                    if ((item.Type == type || (item.Type & type) > 0) && !item.IsEquiped)
                    {
                        SortedInventory.Add(item);
                    }
                }
            }

            return SortedInventory;

        }

        public void RemoveItem(Item item)
        {
            ListInventory.Remove(item);
        }

        public void RemoveItem(Item item, int count)
        {
            try
            {
                if ((item.Count - count) <= 0)
                {
                    RemoveItem(item);
                }
                else
                {
                    item.Count -= count;
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Предмет не стакающийся!!!");
                throw;
            }
        }

        public void AddEnchant(BaseEnchant enchant)
        {
            if (ListEnchants.Contains(enchant))
            {
                enchant.UpgradeSelf();
            }
            else
            {
                ListEnchants.Add(enchant);
                AnsiConsole.MarkupLine($"'{enchant.Name}' добавлен");
            }
        }

        public void AddCraft(BaseRecipe recipe)
        {
            if (ListRecipes.Contains(recipe))
            {
                recipe.UpgradeSelf();
            }
            else
            {
                ListRecipes.Add(recipe);
                AnsiConsole.MarkupLine($"'{recipe.Name}' добавлен");
            }
        }

        public void AddItem(Item Item)
        {
            if (Item.IsStacable)
            {
                List<string> ItemsNames = new List<string>();
                if (ListInventory != null)
                {
                    foreach (Item AlreadyInInventory in ListInventory)
                    {
                        ItemsNames.Add(AlreadyInInventory.Name);
                    }

                    if (ItemsNames.Contains(Item.Name))
                    {
                        ListInventory.Find(Needle => Needle.Name == Item.Name).Count += Item.Count;
                    }
                    else
                    {
                        ListInventory.Add(Item);
                    }
                }

            }
            else
            {
                ListInventory.Add(Item);
            }
        }
    }
}
