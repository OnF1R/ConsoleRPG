using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG
{
    internal class Spell
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public Spell AddComponent(Characteristics component)
        {
            _components.Add(component);
            return this;
        }

        public T GetComponent<T>()
        {
            return (T)_components.OfType<T>().FirstOrDefault();
        }

        private List<Characteristics> _components = new List<Characteristics>();
    }
}
