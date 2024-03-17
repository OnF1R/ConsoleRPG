using ConsoleRPG.Effects;
using ConsoleRPG.Interfaces;
using ConsoleRPG.Items.Gems;

namespace ConsoleRPG
{
    [Serializable]
    abstract internal class Weapon : Item, IDamageDealerEntity, IAppyStatusEffectEntity
    {
        //public virtual Gem[] Gems { get; set; }

        public Weapon(int level) : base(level)
        {
            this.IsStacable = false;
            this.IsEquapable = true;
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
