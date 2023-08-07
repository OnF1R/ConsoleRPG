using ConsoleRPG.Effects;

namespace ConsoleRPG.Interfaces
{
    internal interface IAppyStatusEffectEntity : IEntity
    {
        public Dictionary<BaseEffect, double> GetEffects();
    }
}
