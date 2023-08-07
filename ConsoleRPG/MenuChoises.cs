﻿
using System.Runtime.InteropServices;

namespace ConsoleRPG
{
    public static class MenuChoises
    {
        public static Dictionary<int, string> StartMenuChoises()
        {
            Dictionary<int, string> menuChoises = new Dictionary<int, string>();
            menuChoises.Add(1, "Новая игра");
            menuChoises.Add(2, "Загрузить");
            menuChoises.Add(3, "Выйти");

            return menuChoises;
        }

        public static Dictionary<int, string> MainMenuChoises()
        {
            Dictionary<int, string> menuChoises = new Dictionary<int, string>();
            menuChoises.Add(1, "Отправиться в приключение");
            menuChoises.Add(2, "Торговец");
            menuChoises.Add(3, "Инвентарь");
            menuChoises.Add(4, "Экипировка");
            menuChoises.Add(5, "Характеристики");
            menuChoises.Add(6, "Крафт");

            return menuChoises;
        }


        public static Dictionary<int, string> MerchantChoises()
        {
            Dictionary<int, string> menuChoises = new Dictionary<int, string>();
            menuChoises.Add(1, "Посмотреть предметы");
            menuChoises.Add(2, "Купить предметы");
            menuChoises.Add(3, "Продать предметы");
            menuChoises.Add(4, "Купить рецепты");
            menuChoises.Add(5, "Купить зачарования");
            menuChoises.Add(6, "Выйти");

            return menuChoises;
        }

        public static Dictionary<int, string> EquipmentChoises()
        {
            Dictionary<int, string> menuChoises = new Dictionary<int, string>();
            menuChoises.Add(1, "Посмотреть экипировку");
            menuChoises.Add(2, "Изменить экипировку");
            menuChoises.Add(3, "Выйти");

            return menuChoises;
        }

        public static Dictionary<int, string> EquipmentSlotsChoises()
        {
            int index = 0;
            Dictionary<int, string> menuChoises = new Dictionary<int, string>();

            foreach (EquipmentSlot slot in Enum.GetValues(typeof(EquipmentSlot)))
            {
                index++;
                menuChoises.Add(index, EquipmentSlots.Names[slot]);
            }

            return menuChoises;
        }

        public static Dictionary<int, string> ActionMenuChoises()
        {
			Dictionary<int, string> menuChoises = new Dictionary<int, string>
			{
				{ 1, "Атаковать" },
				{ 2, "Заклинания" },
				{ 3, "Сбежать" }
			};

			return menuChoises;
        }

        public static Dictionary<int, string> CraftMenuChoises()
        {
            Dictionary<int, string> menuChoises = new Dictionary<int, string>();
            menuChoises.Add(1, "Создать предмет");
            //menuChoises.Add(2, "Улучшить предмет");
            menuChoises.Add(2, "Зачаровать предмет");
            menuChoises.Add(3, "Выйти");

            return menuChoises;
        }
    }
}
