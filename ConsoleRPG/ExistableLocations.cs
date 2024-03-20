using ConsoleRPG.Locations;

namespace ConsoleRPG
{
    internal class ExistableLocations
    {
        public static List<BaseLocation> Locations = new List<BaseLocation>()
        {
            new CastleLocation(),
            new DesertLocation(),
			//new SwampLocation(),
			new GorgeLocation(),
            new MageTowerLocaiton(),
			//new VillageOfTearsLocation(),
		};

        public static BaseLocation GetRandomLocation()
        {
            var rand = new SerializableRandom();
            return Locations[rand.Next(Locations.Count)];
        }
    }
}
