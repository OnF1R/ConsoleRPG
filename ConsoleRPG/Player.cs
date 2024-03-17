using ConsoleRPG.Enums;
using ConsoleRPG.Interfaces;
using ConsoleRPG.Items;
using ConsoleRPG.Items.Weapons.BaseEnemyWeapons;
using ConsoleRPG.Quests;
using Spectre.Console;

namespace ConsoleRPG
{
    [Serializable]
    internal class Player : Unit
    {
        public int CurrentExp { get; set; }
        public int NextLevelExp { get; set; }

        public List<Quest> CurrentQuests { get; set; } = new List<Quest>();

        public Dictionary<EnemyIdentifierEnum, int> KillCount { get; set; } = new Dictionary<EnemyIdentifierEnum, int>();

        public List<PlayableUnit> Team { get; set; } = new List<PlayableUnit>();

        public Player()
        {
            Team.Capacity = 3;
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

        public void TeamControl()
        {
            if (Team.Count > 0)
            {
                var choise = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold]Выберите действие: [/]")
                    .MoreChoicesText("[grey](Пролистайте ниже, чтобы увидеть все доступные варианты)[/]")
                    .AddChoices(MenuChoises.TeamControlChoises().Values));

                switch (MenuChoises.TeamControlChoises().FirstOrDefault(x => x.Value == choise).Key)
                {
                    case 1:
                        foreach (var unit in Team)
                            unit.ShowCharacteristics();
                        break;
                    case 2:
                        var teammate = ChooseTeammate();
                        teammate.Equipment.EquipmentMenu(teammate, this);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                ShowMessage("[red]У вас нет команды![/]");
            }
        }

        private PlayableUnit ChooseTeammate()
        {
            Dictionary<PlayableUnit, string> keyValuePairs = new();

            foreach (var unit in Team)
            {
                keyValuePairs.Add(unit, unit.Name);
            }

            var choise = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[bold]Выберите напарника: [/]")
                .MoreChoicesText("[grey](Пролистайте ниже, чтобы увидеть все доступные варианты)[/]")
                .AddChoices(keyValuePairs.Values));

            return keyValuePairs.FirstOrDefault(x => x.Value == choise).Key;
        }

        public void AddToParty(PlayableUnit unit)
        {
            if (Team.Count <= Team.Capacity)
            {
                Team.Add(unit);
            }
        }

        public void KickFromParty(PlayableUnit unit)
        {
            try
            {
                Team.Remove(Team.Find(x => x == unit));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Unit> GetTeamWithMe()
        {
            List<Unit> list = new List<Unit>();
            if (Team != null)
            {
                list.AddRange(Team);
            }
            list.Add(this);
            return list;
        }

        public Unit GetRandomTeammate()
        {
            SerializableRandom rand = new SerializableRandom();
            return Team[rand.Next(Team.Count)];
        }

        public Unit GetRandomTeammateWithMe()
        {
            var list = GetTeamWithMe();
            SerializableRandom rand = new SerializableRandom();
            return list[rand.Next(list.Count)];
        }

        public override void ShowCharacteristics()
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

        public override IDamageDealerEntity[] GetDamageEntities()
        {
            Type classType = typeof(NothingItem);
            Type interfaceType = typeof(IDamageDealerEntity);
            List<Weapon> items = new List<Weapon>();

            foreach (var equip in Equipment.Equip.Keys)
            {
                if (Equipment.Equip[equip].GetType() == classType)
                {
                    if (equip == EquipmentSlot.LeftHand)
                        if (Equipment.Equip[EquipmentSlot.RightHand].GetType() == classType)
                            items.Add(new HandsBaseWeapon(Level));

                }
                else if (Equipment.Equip[equip].GetType().GetInterfaces().Contains(interfaceType))
                {
                    items.Add((Weapon)Equipment.Equip[equip]);
                }
            }

            return items.ToArray();
        }

        public void AddSpell(BaseSpell spell)
        {
            if (UnitSpells.ContainsKey(spell.ID))
            {
                return;
            }
            else
            {
                UnitSpells.Add(spell.ID, spell);
                ShowMessage($"Вы выучили заклинание: {spell.GetName()}");
            }
        }

        public void UseSpell(BaseSpell spell, Unit target)
        {
            spell.Use(this, target);
        }

        public override BaseSpell ChooseSpell()
        {
            if (UnitSpells.Count > 0)
            {
                List<string> spellsNames = new List<string>();

                foreach (var spell in UnitSpells.Values)
                {
                    spellsNames.Add(spell.GetName());
                }

                spellsNames.Add("[red]Отменить[/]");

                var choise = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[bold]Выберите заклинание[/]")
                        .PageSize(10)
                        .MoreChoicesText("[grey](Пролистайте ниже, чтобы увидеть все доступные варианты)[/]")
                        .AddChoices(spellsNames));

                return UnitSpells.Values.FirstOrDefault(x => x.GetName() == choise);
            }
            else
            {
                ShowMessage("[red]Нет заклинаний![/]");
            }

            return null;
        }

        public void AddQuest(Quest quest)
        {
            CurrentQuests.Add(quest);
            ShowMessage($"{Name} получил задание, {quest.Name}");
        }

        public Quest QuestsInfo()
        {
            if (CurrentQuests.Count > 0)
            {
                List<string> questsNames = new List<string>();
                int i = 1;
                foreach (var quest in CurrentQuests)
                {
                    questsNames.Add($"{i}: {quest.Name}");
                    i++;
                }

                questsNames.Add("[red]Отменить[/]");

                var choise = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[bold]Выберите задание, про которое хотите узнать подробности[/]")
                        .PageSize(10)
                        .MoreChoicesText("[grey](Пролистайте ниже, чтобы увидеть все доступные варианты)[/]")
                        .AddChoices(questsNames));

                return CurrentQuests[questsNames.IndexOf(choise)];
            }
            else
            {
                ShowMessage("[red]Нет заданий.[/]");
            }

            return null;
        }

        public void KillingQuestUpdate(EnemyIdentifierEnum type)
        {
            List<int> indexesToDelete = new List<int>();

            foreach (var quest in CurrentQuests)
            {
                if (quest.QuestType == QuestType.Killing && quest.GetComponent<QuestKillCondition>().RequiredEnemyKillType == type)
                {
                    quest.GetComponent<QuestKillCondition>().CurrentKillCount++;

                    if (quest.IsCompleteQuest())
                    {
                        quest.CompleteQuest(this);
                        indexesToDelete.Add(CurrentQuests.IndexOf(quest));
                    }
                }
            }

            foreach (var index in indexesToDelete)
            {
                CurrentQuests.Remove(CurrentQuests[index]);
            }
        }

        public void KillCountUpdate(EnemyIdentifierEnum type)
        {
            if (KillCount.ContainsKey(type))
            {
                KillCount[type]++;
            }
            else
            {
                KillCount.Add(type, 1);
            }
        }

        public void KillCountInfo()
        {
            if (KillCount.Count > 0)
            {
                var table = new Table();

                table.AddColumn("Враг");
                table.AddColumn(new TableColumn("Количество убийств"));

                foreach (var kill in KillCount.Keys)
                {
                    table.AddRow(ExistableEnemies.EnemyNames[kill], KillCount[kill].ToString());
                }

                AnsiConsole.Write(table);
            }
            else
            {
                ShowMessage($"{Name} ещё никого не убивал.");
            }
        }

        public int KillCountNumber()
        {
            int killCount = 0;

            foreach (var count in KillCount.Keys)
            {
                killCount += KillCount[count];
            }

            return killCount;
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
                    AnsiConsole.MarkupLine("{0} получил {1} опыта {2} от бонуса к опыту", Name, Exp + BonusExp, BonusExp);
                }
                else
                {
                    AnsiConsole.MarkupLine("{0} получил {1} опыта", Name, Exp);
                }
            }
        }
    }
}
