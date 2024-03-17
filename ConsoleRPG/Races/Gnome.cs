using ConsoleRPG.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG.Races
{
    [Serializable]
    internal class Gnome : Race
    {
        public Gnome()
        {
            Name = "Гном";
            RaceType = RacesType.Gnome;

            AddComponent(new StrengthCharacteristic { StrengthPerLevel = 2.6 });
            AddComponent(new AgilityCharacteristic { AgilityPerLevel = 1.4 });
            AddComponent(new IntelligenceCharacteristic { IntelligencePerLevel = 2.2 });
            AddComponent(new ArmorCharacteristic { ArmorPerLevel = 0.25 });
        }

        public override string RaceInfo()
        {
            return $"Прирост характеристик Гнома за уровень: " +
                $"Сила + {this.GetComponent<StrengthCharacteristic>().StrengthPerLevel}" +
                $", Ловкость {this.GetComponent<AgilityCharacteristic>().AgilityPerLevel}" +
                $", Интеллект {this.GetComponent<IntelligenceCharacteristic>().IntelligencePerLevel}" +
                $", Броня + {this.GetComponent<ArmorCharacteristic>().ArmorPerLevel}";
        }
    }
}
