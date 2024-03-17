using ConsoleRPG.Enums;

namespace ConsoleRPG
{
    public class Quality
    {
        public Dictionary<QualityTypes, string> Names = new Dictionary<QualityTypes, string>();
        Dictionary<QualityTypes, string> Colors = new Dictionary<QualityTypes, string>();

        public Quality()
        {
            Names.Add(QualityTypes.Broken, "[grey]Сломанный[/]");
            Names.Add(QualityTypes.Used, "[grey37]Использованный[/]");
            Names.Add(QualityTypes.Anchient, "[grey84]Древний[/]");
            Names.Add(QualityTypes.Rust, "[salmon1]Ржавый[/]");
            Names.Add(QualityTypes.Encanted, "[plum4]Зачарованный[/]");
            Names.Add(QualityTypes.Mad, "[darkred]Яростный[/]");
            Names.Add(QualityTypes.Blessed, "[silver]Благословленный[/]");
            Names.Add(QualityTypes.Demonical, "[deeppink3]Демонический[/]");
            Names.Add(QualityTypes.Godlike, "[lightgoldenrod1]Божественный[/]");
            Names.Add(QualityTypes.GodKiller, "[hotpink]Свергнувший богов[/]");
            Names.Add(QualityTypes.Antimaterial, "[lightslategrey]Антиматериальный[/]");
            Names.Add(QualityTypes.FromAbyss, "[lightskyblue1]Созданный в Бездне[/]");

            Colors.Add(QualityTypes.Broken, "grey");
            Colors.Add(QualityTypes.Used, "grey37");
            Colors.Add(QualityTypes.Anchient, "grey84");
            Colors.Add(QualityTypes.Rust, "salmon1");
            Colors.Add(QualityTypes.Encanted, "plum4");
            Colors.Add(QualityTypes.Mad, "darkred");
            Colors.Add(QualityTypes.Blessed, "silver");
            Colors.Add(QualityTypes.Demonical, "deeppink3");
            Colors.Add(QualityTypes.Godlike, "lightgoldenrod1");
            Colors.Add(QualityTypes.GodKiller, "hotpink");
            Colors.Add(QualityTypes.Antimaterial, "lightslategrey");
            Colors.Add(QualityTypes.FromAbyss, "lightskyblue1");

        }

        public QualityTypes GetNotGroupQuality()
        {
            QualityTypes RandomType = RandomQualityEnum();

            if (RandomType == QualityTypes.Bad || RandomType == QualityTypes.Good || RandomType == QualityTypes.Best)
            {
                return GetNotGroupQuality();
            }
            else
            {
                return RandomType;
            }
        }

        public string GetBadQuality()
        {
            QualityTypes RandomType = GetNotGroupQuality();

            if (QualityTypes.Bad.HasFlag(RandomType))
            {
                return Names[RandomType];
            }
            else
            {
                return GetBadQuality();
            }
        }

        public string GetGoodQuality()
        {
            QualityTypes RandomType = GetNotGroupQuality();

            if (QualityTypes.Good.HasFlag(RandomType))
            {
                return Names[RandomType];
            }
            else
            {
                return GetGoodQuality();
            }
        }

        public string GetBestQuality()
        {
            QualityTypes RandomType = GetNotGroupQuality();

            if (QualityTypes.Best.HasFlag(RandomType))
            {
                return Names[RandomType];
            }
            else
            {
                return GetBestQuality();
            }
        }

        public QualityTypes[] ArrayQualityValues()
        {
            return Enum.GetValues(typeof(QualityTypes)).Cast<QualityTypes>().ToArray();
        }

        public string RandomQualityString()
        {
            QualityTypes[] AllQualityTypes = ArrayQualityValues();
            QualityTypes QualityType = AllQualityTypes[new SerializableRandom().Next(AllQualityTypes.Length)];
            return Names[QualityType];
        }

        public QualityTypes RandomQualityEnum()
        {
            QualityTypes[] AllQualityTypes = ArrayQualityValues();
            QualityTypes QualityType = AllQualityTypes[new SerializableRandom().Next(AllQualityTypes.Length)];
            return QualityType;
        }
    }
}
