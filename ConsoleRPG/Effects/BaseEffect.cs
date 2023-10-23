using ConsoleRPG.Interfaces;
using ICloneable = ConsoleRPG.Interfaces.ICloneable;

namespace ConsoleRPG.Effects
{
    internal abstract class BaseEffect : IStatusEffectEntity, ICloneable
    {
        public string Name { get; set; }
        public int CurrentDuration { get; set; }
        public int Duration { get; protected set; }

        public EffectType EffectType { get; set; }

        public BaseEffect AddComponent(Characteristics component)
        {
            _components.Add(component);
            return this;
        }

        public T GetComponent<T>()
        {
            return (T)_components.OfType<T>().FirstOrDefault();
        }

        private List<Characteristics> _components = new List<Characteristics>();

        public abstract void ApplyEffect(Unit unit);

        protected abstract void StartEffect(Unit unit);

        protected abstract void EndEffect(Unit unit);

        protected string EffectDurationMessage(string unitName)
        {
            string message = $"{Name} ({unitName}):";
            if (CurrentDuration > 0)
            {
                message += $" осталось ходов: {CurrentDuration}";
            }
            else
            {
                message += " последний ход!";
            }

            return message;
        }

        public virtual string Description()
        {
            string message = $"Шанс наложить эффект ({Name})";
            return message;
        }

        public abstract string DescriptionEffect();

        public string GetName()
        {
            return Name;
        }

        abstract public BaseEffect Clone();
	}

    public enum EffectType
    {
        Buff,
        Debuff,
        Stun,
    }
}
