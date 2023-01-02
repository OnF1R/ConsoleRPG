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

            AddComponent(new StrengthCharacteristic { StrengthPerLevel = 1.3 });
            AddComponent(new AgilityCharacteristic { AgilityPerLevel = 2.3 });
            AddComponent(new IntelligenceCharacteristic { IntelligencePerLevel = 0.2 });
            AddComponent(new PhysicalDamageCharacteristic { PhysicalDamagePerLevel = 1 });
        }

        public override string RaceInfo()
        {
            return $"Прирост характеристик Тролля за уровень: " +
                $"Сила + {this.GetComponent<StrengthCharacteristic>().StrengthPerLevel}" +
                $", Ловкость {this.GetComponent<AgilityCharacteristic>().AgilityPerLevel}" +
                $", Интеллект {this.GetComponent<IntelligenceCharacteristic>().IntelligencePerLevel}" +
                $", Урон + {this.GetComponent<PhysicalDamageCharacteristic>().PhysicalDamagePerLevel}";
        }
    }
}
