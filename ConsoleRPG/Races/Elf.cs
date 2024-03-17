using ConsoleRPG.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG.Races
{
    [Serializable]
    internal class Elf : Race
    {
        public Elf()
        {
            Name = "Эльф";
            RaceType = RacesType.Elf;

            AddComponent(new StrengthCharacteristic { StrengthPerLevel = 0.6 });
            AddComponent(new AgilityCharacteristic { AgilityPerLevel = 4.6 });
            AddComponent(new IntelligenceCharacteristic { IntelligencePerLevel = 1.6 });
        }

        public override string RaceInfo()
        {
            return $"Прирост характеристик Эльфа за уровень: " +
                $"Сила + {this.GetComponent<StrengthCharacteristic>().StrengthPerLevel}" +
                $", Ловкость {this.GetComponent<AgilityCharacteristic>().AgilityPerLevel}" +
                $", Интеллект {this.GetComponent<IntelligenceCharacteristic>().IntelligencePerLevel}";
        }
    }
}
