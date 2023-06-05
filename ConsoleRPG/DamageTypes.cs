
namespace ConsoleRPG
{
    class DamageTypesNames
    {
        public Dictionary<DamageTypes, string> Color = new Dictionary<DamageTypes, string>();
        public Dictionary<DamageTypes, string> Names = new Dictionary<DamageTypes, string>();

        public List<DamageTypes> ArrayBasicElementalDamageTypes = new List<DamageTypes>()
        {
            { DamageTypes.Fire },
            { DamageTypes.Electric },
            { DamageTypes.Nature },
            { DamageTypes.Frost },
            { DamageTypes.Water },
            { DamageTypes.Earth },
            { DamageTypes.Wind },
            { DamageTypes.Dark },
            { DamageTypes.Holy },
            { DamageTypes.Poison },
            { DamageTypes.Necrotic },
            { DamageTypes.Psychic },
            { DamageTypes.Arcane },
        };

        public DamageTypes[] ArrayDamageTypesValues()
        {
            return Enum.GetValues(typeof(DamageTypes)).Cast<DamageTypes>().ToArray();
        }

        public DamageTypes GetNotGroupDamageType()
        {
            DamageTypes RandomType = RandomDamageTypesEnum();

            //if (RandomType == DamageTypes.Overcharge || RandomType == DamageTypes.Flame || RandomType == DamageTypes.Melting
            //    || RandomType == DamageTypes.Fume || RandomType == DamageTypes.Meteor || RandomType == DamageTypes.Fireblast
            //    || RandomType == DamageTypes.Load || RandomType == DamageTypes.Icecharge || RandomType == DamageTypes.Shock
            //    || RandomType == DamageTypes.Stream || RandomType == DamageTypes.Storm || RandomType == DamageTypes.Chaos
            //    || RandomType == DamageTypes.Burst)
            //{
            return RandomType;
        }

        public DamageTypes GetRandomElementalDamageType()
        {
            DamageTypes RandomType = GetNotGroupDamageType();
            if (RandomType == DamageTypes.Physical || RandomType == DamageTypes.Abyss || RandomType == DamageTypes.Pure || RandomType == DamageTypes.Execute)
            {
                return GetRandomElementalDamageType();
            }
            else
            {
                return RandomType;
            }
        }

        public string RandomDamageTypesString()
        {
            DamageTypes[] AllDamageTypes = ArrayDamageTypesValues();
            DamageTypes DamageType = AllDamageTypes[new Random().Next(AllDamageTypes.Length)];
            return Names[DamageType];
        }

        public DamageTypes RandomDamageTypesEnum()
        {
            DamageTypes[] AllDamageTypes = ArrayDamageTypesValues();
            DamageTypes DamageType = AllDamageTypes[new Random().Next(AllDamageTypes.Length)];
            return DamageType;
        }

        public DamageTypesNames()
        {
            Names.Add(DamageTypes.Fire, "[orangered1]Огонь[/]");
            //Names.Add(DamageTypes.Overcharge, "[darkmagenta]Перегрузка[/]");
            //Names.Add(DamageTypes.Flame, "[orange3]Возгорание[/]");
            //Names.Add(DamageTypes.Fume, "[grey50]Пар[/]");
            //Names.Add(DamageTypes.Meteor, "[darkorange]Метеор[/]");
            //Names.Add(DamageTypes.Fireblast, "[orange3]Огненный взрыв[/]");
            Names.Add(DamageTypes.Electric, "[purple]Электричество[/]");
            //Names.Add(DamageTypes.Load, "[stateblue3]Заряженный ток[/]");
            //Names.Add(DamageTypes.Icecharge, "[stateblue1]Заряженный лед[/]");
            //Names.Add(DamageTypes.Shock, "[orchid]Шок[/]");
            //Names.Add(DamageTypes.Storm, "[skyblue1]Шторм[/]");
            Names.Add(DamageTypes.Nature, "[green]Природа[/]");
            Names.Add(DamageTypes.Frost, "[aqua]Мороз[/]");
            Names.Add(DamageTypes.Water, "[dodgerblue1]Вода[/]");
            Names.Add(DamageTypes.Earth, "[darkgreen]Земля[/]");
            Names.Add(DamageTypes.Wind, "[darkseagreen1]Воздух[/]");
            Names.Add(DamageTypes.Dark, "[grey30]Тьма[/]");
            Names.Add(DamageTypes.Holy, "[lightgoldenrod1]Свет[/]");
            //Names.Add(DamageTypes.Chaos, "[black on white]Хаос[/]");
            Names.Add(DamageTypes.Abyss, "[mediumpurple4]Пустота[/]");
            Names.Add(DamageTypes.Pure, "[gold1]Чистый[/]");
            Names.Add(DamageTypes.Execute, "[bold red]КАЗНЬ[/]");
            Names.Add(DamageTypes.Physical, "[bold white]Физический[/]");
            //Names.Add(DamageTypes.Burst, "[orange1]Взрыв[/]");
            Names.Add(DamageTypes.Poison, "[lime]Яд[/]");
            Names.Add(DamageTypes.Necrotic, "[yellow4]Некроз[/]");
            Names.Add(DamageTypes.Psychic, "[pink1]Психический[/]");
            Names.Add(DamageTypes.Arcane, "[violet]Тайный[/]");

            Color.Add(DamageTypes.Fire, "orangered1");
            //Color.Add(DamageTypes.Overcharge, "darkmagenta");
            //Color.Add(DamageTypes.Flame, "orange3");
            //Color.Add(DamageTypes.Fume, "grey50");
            //Color.Add(DamageTypes.Meteor, "darkorange");
            //Color.Add(DamageTypes.Fireblast, "orange3");
            Color.Add(DamageTypes.Electric, "purple");
            //Color.Add(DamageTypes.Load, "stateblue3");
            //Color.Add(DamageTypes.Icecharge, "stateblue1");
            //Color.Add(DamageTypes.Shock, "orchid");
            //Color.Add(DamageTypes.Storm, "skyblue1");
            //Color.Add(DamageTypes.Thunder, "yellow");
            //Color.Add(DamageTypes.Lightning, "skyblue2");
            Color.Add(DamageTypes.Nature, "green");
            Color.Add(DamageTypes.Frost, "aqua");
            //Color.Add(DamageTypes.Cold, "mediumturquoise");
            //Color.Add(DamageTypes.Ice, "lightsteelblue1");
            Color.Add(DamageTypes.Water, "dodgerblue1");
            Color.Add(DamageTypes.Earth, "darkgreen");
            //Color.Add(DamageTypes.Stone, "gray37");
            Color.Add(DamageTypes.Wind, "darkseagreen1");
            Color.Add(DamageTypes.Dark, "grey30");
            //Color.Add(DamageTypes.Nightmare, "navyblue");
            Color.Add(DamageTypes.Holy, "lightgoldenrod1");
            //Color.Add(DamageTypes.Chaos, "black on white");
            //Color.Add(DamageTypes.Radiant, "silver");
            Color.Add(DamageTypes.Abyss, "mediumpurple4");
            //Color.Add(DamageTypes.Astral, "steelblue1");
            Color.Add(DamageTypes.Pure, "gold1");
            Color.Add(DamageTypes.Execute, "red");
            Color.Add(DamageTypes.Physical, "white");
            //Color.Add(DamageTypes.Burst, "orange1");
            Color.Add(DamageTypes.Poison, "lime");
            //Color.Add(DamageTypes.Acid, "darkgreen");
            //Color.Add(DamageTypes.Gas, "mediumspringgreen");
            Color.Add(DamageTypes.Necrotic, "yellow4");
            Color.Add(DamageTypes.Psychic, "pink1");
            Color.Add(DamageTypes.Arcane, "violet");
        }
    }

    [Flags]
    public enum DamageTypes
    {
        Fire = 1 << 0,
        Electric = 1 << 1,
        //Thunder = Storm,
        //Lightning = Electric,
        Nature = 1 << 2,
        Frost = 1 << 3,
        //Cold = Frost,
        //Ice = Frost,
        Water = 1 << 4,
        Earth = 1 << 5,
        //Stone = Earth,
        Wind = 1 << 6,
        Dark = 1 << 7,
        //Nightmare = Dark,
        Holy = 1 << 8,
        //Chaos = Dark + Holy,
        //Radiant = Holy,
        Abyss = 1 << 9,
        //Astral = Abyss,
        Pure = 1 << 10,
        Execute = 1 << 11,
        Physical = 1 << 12,
        Poison = 1 << 13,
        //Acid = Poison,
        //Gas = Poison,
        Necrotic = 1 << 14,
        Psychic = 1 << 15,
        Arcane = 1 << 16,
    }


}
