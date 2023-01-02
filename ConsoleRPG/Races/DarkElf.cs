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

            AddComponent(new StrengthCharacteristic { StrengthPerLevel = 1.6 });
            AddComponent(new AgilityCharacteristic { AgilityPerLevel = 1.5 });
            AddComponent(new IntelligenceCharacteristic { IntelligencePerLevel = 2.8 });
            AddComponent(new LuckCharacteristic { LuckPerLevel = 0.6 });
        }

        public override string RaceInfo()
        {
            return $"Прирост характеристик Темного Эльфа за уровень: " +
                $"Сила + {this.GetComponent<StrengthCharacteristic>().StrengthPerLevel}" +
                $", Ловкость {this.GetComponent<AgilityCharacteristic>().AgilityPerLevel}" +
                $", Интеллект {this.GetComponent<IntelligenceCharacteristic>().IntelligencePerLevel}" +
                $", Удача + {this.GetComponent<LuckCharacteristic>().LuckPerLevel}";
        }
    }
}
