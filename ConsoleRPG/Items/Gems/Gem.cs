using ConsoleRPG.Effects;
using ConsoleRPG.Interfaces;

namespace ConsoleRPG.Items.Gems
{
    [Serializable]
    abstract internal class Gem : Item, IDamageDealerEntity, IAppyStatusEffectEntity
    {
        public Gem(int level) : base(level)
        {
            IsStacable = false;
            IsEquapable = true;
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
    }
}
