using ConsoleRPG.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG.Races
{
    [Serializable]
    internal class Human : Race
    {
        public Human()
        {
            Name = "Человек";
            RaceType = RacesType.Human;

            AddComponent(new StrengthCharacteristic { StrengthPerLevel = 2.3 });
            AddComponent(new AgilityCharacteristic { AgilityPerLevel = 1.8 });
            AddComponent(new IntelligenceCharacteristic { IntelligencePerLevel = 2.1 });
            AddComponent(new LuckCharacteristic { LuckPerLevel = 0.5 });


        }

        public override string RaceInfo()
        {
            return $"Прирост характеристик Человека за уровень: " +
                $"Сила + {this.GetComponent<StrengthCharacteristic>().StrengthPerLevel}" +
                $", Ловкость {this.GetComponent<AgilityCharacteristic>().AgilityPerLevel}" +
                $", Интеллект {this.GetComponent<IntelligenceCharacteristic>().IntelligencePerLevel}" +
                $", Удача + {this.GetComponent<LuckCharacteristic>().LuckPerLevel}";
        }
    }
}
