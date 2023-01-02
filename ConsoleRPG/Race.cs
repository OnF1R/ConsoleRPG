
using ConsoleRPG.Races;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

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
            racesNames = new();
            racesNames.Add(new Human(), new Human().Name);
            racesNames.Add(new Orc(), new Orc().Name);
            racesNames.Add(new Goblin(), new Goblin().Name);
            racesNames.Add(new Troll(), new Troll().Name);
            racesNames.Add(new Elf(), new Elf().Name);
            racesNames.Add(new DarkElf(), new DarkElf().Name);
            racesNames.Add(new Gnome(), new Gnome().Name);
        }
    }

    public enum RacesType
    {
        Human,
        Orc,
        Goblin,
        Troll,
        Elf,
        DarkElf,
        Gnome,
        Demon,
        Undead,
        Dragon,
        Elemental,
        Mechanism,
        Insect,
    }
}
