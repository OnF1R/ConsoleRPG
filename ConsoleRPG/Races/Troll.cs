using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG.Races
{
    internal class Troll : Race
    {
        public Troll()
        {
            Name = "Тролль";
            RaceType = RacesType.Troll;

            AddComponent(new Stat_Strength { PerLevel = 1.3 });
            AddComponent(new Stat_Agility { PerLevel = 2.3 });
            AddComponent(new Stat_Intelligence { PerLevel = 0.2 });
            AddComponent(new Stat_Damage { PerLevel = 1 });
        }

        public override string RaceInfo()
        {
            return $"Прирост характеристик Человека за уровень: " +
                $"Сила + {this.GetComponent<Stat_Strength>().PerLevel}" +
                $", Ловкость {this.GetComponent<Stat_Agility>().PerLevel}" +
                $", Интеллект {this.GetComponent<Stat_Intelligence>().PerLevel}" +
                $", Урон + {this.GetComponent<Stat_Damage>().PerLevel}";
        }
    }
}
