using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG.Items.ItemsComponents
{
    abstract class ItemComponent
    {
    }
    
    class DamageType : ItemComponent
    {
        public DamageTypes Type { get; set; }
    }

    class DamageColor : ItemComponent
    {
        public string Color { get; set; }
    }

    class Defence : ItemComponent
    {
        public int ArmorPoints { get; set; }
    }

    class Valuable : ItemComponent
    {
        public int Cost { get; set; }

        public override string ToString()
        {
            return "Предмет стоит " + Cost + " золота.";
        }
    }

    class Criticals : ItemComponent
    {
        public double CritChance { get; set; }
        public double CritDamage { get; set; }
    }

    class PhysicalDamage : ItemComponent
    {
        public int Physical { get; set; }
    }

    class UniqueDamage : ItemComponent
    {
        public int Abyss { get; set; }
        public int Pierce { get; set; }
        public int Execute { get; set; }
    }

    class ElementalDamage : ItemComponent
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

        // Типы могут объединяться

        //public override string ToString()
        //{
        //   return string.Format("It does {0}/{1}/{2}/{3} elemental damage.", Fire, Water, Earth, Wind);
        //}
    }

    class ElementalResistance : ItemComponent
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

        public override string ToString()
        {
            return string.Format("It does {0}/{1}/{2}/{3} elemental resistance.", Fire, Water, Earth, Wind);
        }
    }
}
