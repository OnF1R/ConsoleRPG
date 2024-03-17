
using ConsoleRPG.Enums;

namespace ConsoleRPG.Items.Armors.Capes
{
    [Serializable]
    internal class RaAyatAlQaiwai : Armor
    {
        public RaAyatAlQaiwai(int level) : base(level)
        {
            SerializableRandom rand = new SerializableRandom();
            Name = "Ра'йаат Аль-кувай";

            ID = ItemIdentifier.RaAyatAlQaiwai;
            int cost = rand.Next(75, 300);
            AddComponent(new ValueCharacteristic { Cost = cost });
            int miss = rand.Next(25, 41);
            AddComponent(new EvasionCharacteristic { EvasionChance = miss });
            RarityId = 6;
            Level = 5;

            Type = ItemType.Cape;

            DropChance = 100f;

            IsStacable = false;
            IsEquapable = true;
            IsEquiped = false;

            SetRarity(RarityId);
        }
    }
}
