using ConsoleRPG.Races;
using ConsoleRPG.Enums;
using ConsoleRPG.Spells.DamageSpells;

namespace ConsoleRPG.PlayableUnits
{
    [Serializable]
    internal class MagePlayableUnit : PlayableUnit
    {
        public MagePlayableUnit()
        {
            List<string> names = new List<string>()
            {
                "Альбо",
                "Альведо",
                "Альбедо",
                "Вургеш",
                "Векрук",
                "Гнуз",
                "Дульдр",
                "Нёхш",
                "Снупз",
                "Измог",
                "Хловит",
                "Кнобус",
                "Шмопс",
                "Шумира",
                "Евнуп",
                "Знович",
                "Алексей",
                "Тигрон",
                "Жунндор",
                "Свиойн",
                "Экзом",
                "Плазмик",
                "Водун",
                "Ведну",
                "Хромидий",
                "Фурдой",
                "Злуб",
                "Шинишошеш",
                "Елул",
                "Юла",
            };

            List<string> prefixs = new List<string>()
            {
                "[purple]Пустотный[/]",
                "[red]Яростный[/]",
                "[green4]Любитель природы[/]",
                "[darkorange3]Любитель толпы[/]",
                "[rosybrown]Слепой[/]",
                "[yellow3]Социофоб[/]",
                "[deeppink3]Путешественник во времени[/]",
                "[hotpink2]Семьянин[/]",
            };

            var rand = new SerializableRandom();

            switch (rand.Next(1,3))
            {
                case 1:
                    Name = names[rand.Next(names.Count)] + " " + prefixs[rand.Next(prefixs.Count)];
                    break;
                case 2:
                    Name = prefixs[rand.Next(prefixs.Count)] + " " + names[rand.Next(names.Count)];
                    break;
                default: 
                    break;
            }

            Level = 1;
            MaxHealth = rand.Next(33, 101);
            CurrentHealth = MaxHealth;
            CurrentExp = 0;
            NextLevelExp = 100;
            IsDead = false;

            switch (rand.Next(1, 5))
            {
                case 1:
                    MyRace = new DarkElf();
                    UnitSpells.Add
                        (SpellIdentifierEnum.Fireball,
                        new FireBall());
                    break;
                case 2:
                    MyRace = new Human();
                    UnitSpells.Add
                        (SpellIdentifierEnum.Frostbolt,
                        new FrostBolt());
                    break;
                case 3:
                    MyRace = new Gnome();
                    UnitSpells.Add
                        (SpellIdentifierEnum.Iceblock,
                        new Spells.DefenceSpells.IceBlock());
                    break;
                case 4:
                    MyRace = new Elf();
                    UnitSpells.Add
                        (SpellIdentifierEnum.DarkStrike,
                        new DarkStrike());
                    break;
                default:
                    MyRace = new Orc();
                    UnitSpells.Add
                        (SpellIdentifierEnum.PoisonSpit,
                        new PoisonSpit());
                    break;
            }
        }
    }
}
