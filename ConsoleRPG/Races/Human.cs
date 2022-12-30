using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG.Races
{
    internal class Human : Race
    {
        public Human()
        {
            Name = "Человек";
            RaceType = RacesType.Human;

            AddComponent(new Stat_Strength { PerLevel = 2.3 });
            AddComponent(new Stat_Agility { PerLevel = 1.8 });
            AddComponent(new Stat_Intelligence { PerLevel = 2.1 });
            AddComponent(new Stat_Luck { PerLevel = 0.5 });


        }

        public override string RaceInfo()
        {
            return $"Прирост характеристик Человека за уровень: " +
                $"Сила + {this.GetComponent<Stat_Strength>().PerLevel}" +
                $", Ловкость {this.GetComponent<Stat_Agility>().PerLevel}" +
                $", Интеллект {this.GetComponent<Stat_Intelligence>().PerLevel}" +
                $", Удача + {this.GetComponent<Stat_Luck>().PerLevel}";
        }
    }
}
