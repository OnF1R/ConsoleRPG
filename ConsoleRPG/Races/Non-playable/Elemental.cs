
using ConsoleRPG.Enums;

namespace ConsoleRPG.Races
{
    [Serializable]
    internal class Elemental : Race
    {
        public Elemental()
        {
            Name = "Элементаль";
            RaceType = RacesType.Elemental;

            AddComponent(new StrengthCharacteristic { StrengthPerLevel = 0 });
            AddComponent(new AgilityCharacteristic { AgilityPerLevel = 0 });
            AddComponent(new IntelligenceCharacteristic { IntelligencePerLevel = 0 });
            AddComponent(new PhysicalDamageCharacteristic { PhysicalDamagePerLevel = 0 });
        }

        public override string RaceInfo()
        {
            return $"Прирост характеристик Элементаля за уровень: " +
                $"Сила + {this.GetComponent<StrengthCharacteristic>().StrengthPerLevel}" +
                $", Ловкость {this.GetComponent<AgilityCharacteristic>().AgilityPerLevel}" +
                $", Интеллект {this.GetComponent<IntelligenceCharacteristic>().IntelligencePerLevel}" +
                $", Урон + {this.GetComponent<PhysicalDamageCharacteristic>().PhysicalDamagePerLevel}";
        }
    }
}
