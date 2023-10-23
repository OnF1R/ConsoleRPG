using ConsoleRPG.Races;
using ConsoleRPG.Enums;

namespace ConsoleRPG
{
    abstract class Race
    {
        public string Name { get; set; }
        public RacesType RaceType { get; set; }

        public Race AddComponent(Characteristics component)
        {
            _components.Add(component);
            return this;
        }

        public T GetComponent<T>()
        {
            return (T)_components.OfType<T>().FirstOrDefault();
        }

        private List<Characteristics> _components = new List<Characteristics>();

        public abstract string RaceInfo();
    }

    class RacesNames
    {
        public Dictionary<Race, string> racesNames;

        public RacesNames()
        {
			racesNames = new()
			{
				{ new Human(), new Human().Name },
				{ new Orc(), new Orc().Name },
				{ new Goblin(), new Goblin().Name },
				{ new Troll(), new Troll().Name },
				{ new Elf(), new Elf().Name },
				{ new DarkElf(), new DarkElf().Name },
				{ new Gnome(), new Gnome().Name }
			};
		}
    }

    
}
