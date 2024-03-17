using ConsoleRPG.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleRPG.Races
{
    [Serializable]
    internal class Goblin : Race
    {
        public Goblin()
        {
            Name = "Гоблин";
            RaceType = RacesType.Goblin;

            AddComponent(new StrengthCharacteristic { StrengthPerLevel = 1.1 });
            AddComponent(new AgilityCharacteristic { AgilityPerLevel = 1.7 });
            AddComponent(new IntelligenceCharacteristic { IntelligencePerLevel = 2.7 });
            AddComponent(new EvasionCharacteristic { EvasionPerLevel = 0.125 });
        }

        public override string RaceInfo()
        {
            return $"Прирост характеристик Гоблина за уровень: " +
                $"Сила + {this.GetComponent<StrengthCharacteristic>().StrengthPerLevel}" +
                $", Ловкость {this.GetComponent<AgilityCharacteristic>().AgilityPerLevel}" +
                $", Интеллект {this.GetComponent<IntelligenceCharacteristic>().IntelligencePerLevel}" +
                $", Уклонение + {this.GetComponent<EvasionCharacteristic>().EvasionPerLevel}%";
        }
    }
}
