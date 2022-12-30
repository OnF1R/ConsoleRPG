using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG.Races
{
    internal class DarkElf : Race
    {
        public DarkElf()
        {
            Name = "Темный эльф";
            RaceType = RacesType.DarkElf;

            AddComponent(new Stat_Strength { PerLevel = 1.6 });
            AddComponent(new Stat_Agility { PerLevel = 1.5 });
            AddComponent(new Stat_Intelligence { PerLevel = 2.8 });
            AddComponent(new Stat_Luck { PerLevel = 0.6 });
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
