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

            AddComponent(new StrengthCharacteristic { StrengthPerLevel = 4.1 });
            AddComponent(new AgilityCharacteristic { AgilityPerLevel = 1.5 });
            AddComponent(new IntelligenceCharacteristic { IntelligencePerLevel = 0.5 });
            AddComponent(new CriticalDamageCharacteristic { CriticalDamagePerLevel = 1.25 });
        }

        public override string RaceInfo()
        {
            return $"Прирост характеристик Орка за уровень: " +
                $"Сила + {this.GetComponent<StrengthCharacteristic>().StrengthPerLevel}" +
                $", Ловкость {this.GetComponent<AgilityCharacteristic>().AgilityPerLevel}" +
                $", Интеллект {this.GetComponent<IntelligenceCharacteristic>().IntelligencePerLevel}" +
                $", Крит. урон + {this.GetComponent<CriticalDamageCharacteristic>().CriticalDamagePerLevel}%";
        }
    }
}
