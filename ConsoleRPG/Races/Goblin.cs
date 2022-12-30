using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleRPG.Races
{
    internal class Goblin : Race
    {
        public Goblin()
        {
            Name = "Гоблин";
            RaceType = RacesType.Goblin;

            AddComponent(new Stat_Strength { PerLevel = 1.1 });
            AddComponent(new Stat_Agility { PerLevel = 1.7 });
            AddComponent(new Stat_Intelligence { PerLevel = 2.7 });
            AddComponent(new Stat_Evasion { PerLevel = 0.125 });
        }

        public override string RaceInfo()
        {
            return $"Прирост характеристик Гоблина за уровень: " +
                $"Сила + {this.GetComponent<Stat_Strength>().PerLevel}" +
                $", Ловкость {this.GetComponent<Stat_Agility>().PerLevel}" +
                $", Интеллект {this.GetComponent<Stat_Intelligence>().PerLevel}" +
                $", Уклонение + {this.GetComponent<Stat_Evasion>().PerLevel}%";
        }
    }
}
