using ConsoleRPG.Items.ItemsComponents;
using ConsoleRPG.Spells.SpellsComponents;
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

        public Spell AddComponent(SpellComponent component)
        {
            _components.Add(component);
            return this;
        }

        public T GetComponent<T>()
        {
            return (T)_components.OfType<T>().FirstOrDefault();
        }

        public void AddNeededElementalTypesDamage(Spell Spell, int minDmg, int maxDmg) 
        {
            if (Spell.GetComponent<SpellDamageType>().Type == DamageTypes.Fire)
            {
                Spell.AddComponent(new SpellElementalDamage { Fire = new Random().Next(minDmg, maxDmg) });
            }
            if (Spell.GetComponent<SpellDamageType>().Type == DamageTypes.Electric)
            {
                Spell.AddComponent(new SpellElementalDamage { Electric = new Random().Next(minDmg, maxDmg) });
            }
            if (Spell.GetComponent<SpellDamageType>().Type == DamageTypes.Nature)
            {
                Spell.AddComponent(new SpellElementalDamage { Nature = new Random().Next(minDmg, maxDmg) });
            }
            if (Spell.GetComponent<SpellDamageType>().Type == DamageTypes.Frost)
            {
                Spell.AddComponent(new SpellElementalDamage { Frost = new Random().Next(minDmg, maxDmg) });
            }
            if (Spell.GetComponent<SpellDamageType>().Type == DamageTypes.Water)
            {
                Spell.AddComponent(new SpellElementalDamage { Water = new Random().Next(minDmg, maxDmg) });
            }
            if (Spell.GetComponent<SpellDamageType>().Type == DamageTypes.Earth)
            {
                Spell.AddComponent(new SpellElementalDamage { Earth = new Random().Next(minDmg, maxDmg) });
            }
            if (Spell.GetComponent<SpellDamageType>().Type == DamageTypes.Wind)
            {
                Spell.AddComponent(new SpellElementalDamage { Wind = new Random().Next(minDmg, maxDmg) });
            }
            if (Spell.GetComponent<SpellDamageType>().Type == DamageTypes.Dark)
            {
                Spell.AddComponent(new SpellElementalDamage { Dark = new Random().Next(minDmg, maxDmg) });
            }
            if (Spell.GetComponent<SpellDamageType>().Type == DamageTypes.Holy)
            {
                Spell.AddComponent(new SpellElementalDamage { Holy = new Random().Next(minDmg, maxDmg) });
            }
            if (Spell.GetComponent<SpellDamageType>().Type == DamageTypes.Poison)
            {
                Spell.AddComponent(new SpellElementalDamage { Poison = new Random().Next(minDmg, maxDmg) });
            }
            if (Spell.GetComponent<SpellDamageType>().Type == DamageTypes.Necrotic)
            {
                Spell.AddComponent(new SpellElementalDamage { Necrotic = new Random().Next(minDmg, maxDmg) });
            }
            if (Spell.GetComponent<SpellDamageType>().Type == DamageTypes.Psychic)
            {
                Spell.AddComponent(new SpellElementalDamage { Psychic = new Random().Next(minDmg, maxDmg) });
            }
            if (Spell.GetComponent<SpellDamageType>().Type == DamageTypes.Arcane)
            {
                Spell.AddComponent(new SpellElementalDamage { Arcane = new Random().Next(minDmg, maxDmg) });
            }
        }

        private List<SpellComponent> _components = new List<SpellComponent>();
    }
}
