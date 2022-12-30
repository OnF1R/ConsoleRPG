using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG.Races
{
    internal class Elf : Race
    {
        public Elf()
        {
            Name = "Эльф";
            RaceType = RacesType.Elf;

            AddComponent(new Stat_Strength { PerLevel = 0.6 });
            AddComponent(new Stat_Agility { PerLevel = 4.6 });
            AddComponent(new Stat_Intelligence { PerLevel = 1.6 });
        }

        public override string RaceInfo()
        {
            return $"Прирост характеристик Эльфа за уровень: " +
                $"Сила + {this.GetComponent<Stat_Strength>().PerLevel}" +
                $", Ловкость {this.GetComponent<Stat_Agility>().PerLevel}" +
                $", Интеллект {this.GetComponent<Stat_Intelligence>().PerLevel}";
        }
    }
}
