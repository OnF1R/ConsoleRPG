using Spectre.Console;

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
            HealFullHealth();
        }

        public void HealFullHealth()
        {
            CurrentHealth = MaxHealth;
        }

        public void ShowCharacteristics()
        {
            var mainTable = new Table();
            mainTable.Title($"[bold]Характеристики {Name}[/]");
            mainTable.AddColumn($"[bold]Базовое[/]");
            mainTable.AddColumn("[bold]Элементальное сопротивление[/]");
            mainTable.AddColumn("[bold]Элементальный урон[/]");

            var table = new Table().BorderColor(Color.Black).HideHeaders();
            var table2 = new Table().BorderColor(Color.Black).HideHeaders();
            var table3 = new Table().BorderColor(Color.Black).HideHeaders();
            table.AddColumn("").Centered();
            table2.AddColumn("").Centered();
            table3.AddColumn("").Centered();

            table.AddRow($"Уровень: {Level}");
            table.AddRow($"Опыт: {CurrentExp}/{NextLevelExp}");
            table.AddRow($"Здоровье: {CurrentHealth}/{MaxHealth}");
            if (GetComponent<StrengthCharacteristic>() != null) { table.AddRow($"Сила: {GetComponent<StrengthCharacteristic>().Strength + GetComponent<StrengthCharacteristic>().ItemsStrength}"); }
            if (GetComponent<AgilityCharacteristic>() != null) { table.AddRow($"Ловкость: {GetComponent<AgilityCharacteristic>().Agility + GetComponent<AgilityCharacteristic>().ItemsAgility}"); }
            if (GetComponent<IntelligenceCharacteristic>() != null) { table.AddRow($"Интеллект: {GetComponent<IntelligenceCharacteristic>().Intelligence + GetComponent<IntelligenceCharacteristic>().ItemsIntelligence}"); }
            if (GetComponent<ArmorCharacteristic>() != null) { table.AddRow($"Броня: {GetComponent<ArmorCharacteristic>().Armor}"); }
            if (GetComponent<PhysicalDamageCharacteristic>() != null) { table.AddRow($"Физический урон: {GetComponent<PhysicalDamageCharacteristic>().PhysicalDamage}"); }
            if (GetComponent<MissCharacteristic>() != null) { table.AddRow($"Шанс промаха: {GetComponent<MissCharacteristic>().MissChance}%"); }
            if (GetComponent<EvasionCharacteristic>() != null) { table.AddRow($"Шанс уклонения: {GetComponent<EvasionCharacteristic>().EvasionChance}%"); }
            if (GetComponent<LuckCharacteristic>() != null) { table.AddRow($"Удача: {GetComponent<LuckCharacteristic>().Luck}"); }
            if (GetComponent<CriticalDamageCharacteristic>() != null) { table.AddRow($"Крит. урон: {GetComponent<CriticalDamageCharacteristic>().CriticalDamage}%"); }
            if (GetComponent<CriticalChanceCharacteristic>() != null) { table.AddRow($"Крит. шанс: {GetComponent<CriticalChanceCharacteristic>().CriticalChance}%"); }
            if (GetComponent<MagicAmplificationCharacteristic>() != null)
            {
                table.AddRow($"Усиление магии: {GetComponent<MagicAmplificationCharacteristic>().Amplification}%");
            }

            if (GetComponent<SpikeCharacteristic>() != null)
            {
                table.AddRow($"Урон от шипов: {GetComponent<SpikeCharacteristic>().SpikeDamage}");
            }

            if (GetComponent<ExperienceBooster>() != null)
            {
                table.AddRow($"Бонус к опыту: {GetComponent<ExperienceBooster>().PercentBoost}%");
            }

            if (GetComponent<HealAmplificationCharacteristic>() != null)
            {
                table.AddRow($"Усиление исцеления: {GetComponent<HealAmplificationCharacteristic>().Amplification}%");
            }

            if (GetComponent<VampirismCharacteristic>() != null)
            {
                table.AddRow($"Вампиризм: {GetComponent<VampirismCharacteristic>().VampirismPercent}%");
            }

            if (GetComponent<ParryCharacteristic>() != null)
            {
                table.AddRow($"Парирование: {GetComponent<ParryCharacteristic>().ParryPercent}%");
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
            int percentConvert = 100;
            int BonusExp = (int)(Exp * ExperienceBoost() / percentConvert);
            if (BonusExp != 0)
                Exp += BonusExp;

            CurrentExp += Exp;
            if (CurrentExp >= NextLevelExp)
            {
                CurrentExp -= NextLevelExp;
                LevelUp();
            }
            else
            {
                if (BonusExp > 0)
                {
                    AnsiConsole.MarkupLine("{0} получил {1} опыта + {2} от бонуса к опыту", Name, Exp - BonusExp, BonusExp);
                }
                else if (BonusExp < 0)
                {
                    AnsiConsole.MarkupLine("{0} получил {1} опыта - {2} от бонуса к опыту", Name, Exp + BonusExp, BonusExp);
                }
                else
                {
                    AnsiConsole.MarkupLine("{0} получил {1} опыта", Name, Exp);
                }
            }
        }
    }
}
