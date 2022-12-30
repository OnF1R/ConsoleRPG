using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleRPG.Races
{
    internal class Orc : Race
    {
        public Orc()
        {
            Name = "Орк";
            RaceType = RacesType.Orc;

            AddComponent(new Stat_Strength { PerLevel = 4.1 });
            AddComponent(new Stat_Agility { PerLevel = 1.5 });
            AddComponent(new Stat_Intelligence { PerLevel = 0.5 });
            AddComponent(new Stat_CritDamage { PerLevel = 1.25 });
        }

        public override string RaceInfo()
        {
            return $"Прирост характеристик Орка за уровень: " +
                $"Сила + {this.GetComponent<Stat_Strength>().PerLevel}" +
                $", Ловкость {this.GetComponent<Stat_Agility>().PerLevel}" +
                $", Интеллект {this.GetComponent<Stat_Intelligence>().PerLevel}" +
                $", Крит. урон + {this.GetComponent<Stat_CritDamage>().PerLevel}%";
        }
    }
}
