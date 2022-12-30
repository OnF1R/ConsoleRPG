using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG.Races
{
    internal class Gnome : Race
    {
        public Gnome()
        {
            Name = "Гном";
            RaceType = RacesType.Gnome;

            AddComponent(new Stat_Strength { PerLevel = 2.6 });
            AddComponent(new Stat_Agility { PerLevel = 1.4 });
            AddComponent(new Stat_Intelligence { PerLevel = 2.2 });
            AddComponent(new Stat_Armor { PerLevel = 0.25 });
        }

        public override string RaceInfo()
        {
            return $"Прирост характеристик Гнома за уровень: " +
                $"Сила + {this.GetComponent<Stat_Strength>().PerLevel}" +
                $", Ловкость {this.GetComponent<Stat_Agility>().PerLevel}" +
                $", Интеллект {this.GetComponent<Stat_Intelligence>().PerLevel}" +
                $", Броня + {this.GetComponent<Stat_Armor>().PerLevel}";
        }
    }
}
