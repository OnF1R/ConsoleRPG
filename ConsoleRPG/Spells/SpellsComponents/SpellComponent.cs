using ConsoleRPG.Items.ItemsComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG.Spells.SpellsComponents
{
    internal class SpellComponent
    {
    }

    class SpellDamageType : SpellComponent
    {
        public DamageTypes Type { get; set; }
    }

    class SpellCriticals : ItemComponent
    {
        public double CritChance { get; set; }
        public double CritDamage { get; set; }
    }

    class SpellDamageColor : SpellComponent
    {
        public string Color { get; set; }
    }

    class SpellPhysicalDamage : SpellComponent
    {
        public int Physical { get; set; }
    }

    class SpellUniqueDamage : SpellComponent
    {
        public int Abyss { get; set; }
        public int Pierce { get; set; }
        public int Execute { get; set; }
    }

    class SpellElementalDamage : SpellComponent
    {
        public int Fire { get; set; }
        public int Electric { get; set; }
        public int Nature { get; set; }
        public int Frost { get; set; }
        public int Water { get; set; }
        public int Earth { get; set; }
        public int Wind { get; set; }
        public int Dark { get; set; }
        public int Holy { get; set; }
        public int Poison { get; set; }
        public int Necrotic { get; set; }
        public int Psychic { get; set; }
        public int Arcane { get; set; }
        public int All { get; set; }
    }
}

