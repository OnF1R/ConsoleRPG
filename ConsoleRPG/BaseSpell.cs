
using ConsoleRPG.Effects;
using ConsoleRPG.Interfaces;

namespace ConsoleRPG
{
    internal abstract class BaseSpell : IDamageDealerEntity, IAppyStatusEffectEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public BaseSpell AddComponent(Characteristics component)
        {
            _components.Add(component);
            return this;
        }

        public T GetComponent<T>()
        {
            return (T)_components.OfType<T>().FirstOrDefault();
        }

        public string GetName()
        {
            return Name;
        }

        public Dictionary<BaseEffect, double> GetEffects()
        {
            if (GetComponent<StatusEffectsCharacteristic>() != null)
            {
                return GetComponent<StatusEffectsCharacteristic>().Effects;
            }

            return new Dictionary<BaseEffect, double>();
        }

        private List<Characteristics> _components = new List<Characteristics>();
    }
}
