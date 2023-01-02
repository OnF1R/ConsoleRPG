using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Console = Colorful.Console;
using Spectre.Console;
using ConsoleRPG.Items.Weapons;

namespace ConsoleRPG
{
    internal class Player : Unit
    {
        public int CurrentExp { get; set; }
        public int NextLevelExp { get; set; }

        public Player()
        {
        }

        public void LevelUp()
        {
            Level += 1;
            AnsiConsole.MarkupLine("[bold]{0}[/] повысил уровень, текущий уровень [bold]{1}[/]", Name, Level);
            NextLevelExp = (int)Math.Floor(NextLevelExp * 1.3);
            UpgradeStats();
        }

        public void ShowCharacteristics()
        {
            var mainTable = new Table();
            mainTable.Title($"[bold]Характеристики {Name}[/]");
            mainTable.AddColumn($"[bold]Базовое[/]");
            mainTable.AddColumn("[bold]Элементальное сопротивление[/]");
            mainTable.AddColumn("[bold]Элементальный урон[/]");

            var table = new Table().BorderColor(Spectre.Console.Color.Black).HideHeaders();
            var table2 = new Table().BorderColor(Spectre.Console.Color.Black).HideHeaders();
            var table3 = new Table().BorderColor(Spectre.Console.Color.Black).HideHeaders();
            table.AddColumn("").Centered();
            table2.AddColumn("").Centered();
            table3.AddColumn("").Centered();

            table.AddRow($"Уровень: {Level}");
            table.AddRow($"Опыт: {CurrentExp}/{NextLevelExp}");
            table.AddRow($"Здоровье: {CurrentHealth}/{MaxHealth}");
            if (GetComponent<StrengthCharacteristic>() != null) { table.AddRow($"Сила: {GetComponent<StrengthCharacteristic>().Strength}"); }
            if (GetComponent<AgilityCharacteristic>() != null) { table.AddRow($"Ловкость: {GetComponent<AgilityCharacteristic>().Agility}"); }
            if (GetComponent<IntelligenceCharacteristic>() != null) { table.AddRow($"Интеллект: {GetComponent<IntelligenceCharacteristic>().Intelligence}"); }
            if (GetComponent<ArmorCharacteristic>() != null) { table.AddRow($"Броня: {GetComponent<ArmorCharacteristic>().Armor}"); }
            if (GetComponent<PhysicalDamageCharacteristic>() != null) { table.AddRow($"Физичский урон: {GetComponent<PhysicalDamageCharacteristic>().PhysicalDamage}"); }
            if (GetComponent<MissCharacteristic>() != null) { table.AddRow($"Шанс промаха: {GetComponent<MissCharacteristic>().MissChance}%"); }
            if (GetComponent<EvasionCharacteristic>() != null) { table.AddRow($"Шанс уклонения: {GetComponent<EvasionCharacteristic>().EvasionChance}%"); }
            if (GetComponent<LuckCharacteristic>() != null) { table.AddRow($"Удача: {GetComponent<LuckCharacteristic>().Luck}"); }
            if (GetComponent<CriticalDamageCharacteristic>() != null) { table.AddRow($"Крит. урон: {GetComponent<CriticalDamageCharacteristic>().CriticalDamage}%"); }
            if (GetComponent<CriticalChanceCharacteristic>() != null) { table.AddRow($"Крит. шанс: {GetComponent<CriticalChanceCharacteristic>().CriticalChance}%"); }
            if (GetComponent<MagicAmplificationCharacteristic>() != null)
            {
                table.AddRow($"Усиление магии: {GetComponent<MagicAmplificationCharacteristic>().Amplification}%");
            }

            if (GetComponent<ElementalResistanceCharacteristic>() != null)
            {
                foreach (DamageTypes type in GetComponent<ElementalResistanceCharacteristic>().ElemResistance.Keys)
                {
                    table2.AddRow($"{new DamageTypesNames().Names[type]}: {GetComponent<ElementalResistanceCharacteristic>().ElemResistance[type]}%");
                }
            }

            if (GetComponent<ElementalDamageCharacteristic>() != null)
            {
                foreach (DamageTypes type in GetComponent<ElementalDamageCharacteristic>().ElemDamage.Keys)
                {
                    table3.AddRow($"{new DamageTypesNames().Names[type]}: {GetComponent<ElementalDamageCharacteristic>().ElemDamage[type]}");
                }
            }

            mainTable.AddRow(table, table2, table3);

            AnsiConsole.Write(mainTable);
        }

        public void TakeExp(int Exp)
        {
            CurrentExp += Exp;
            if (CurrentExp >= NextLevelExp)
            {
                CurrentExp -= NextLevelExp;
                LevelUp();
            }
            else
            {
                AnsiConsole.MarkupLine("{0} получил {1} опыта", Name, Exp);
            }
        }
    }
}
