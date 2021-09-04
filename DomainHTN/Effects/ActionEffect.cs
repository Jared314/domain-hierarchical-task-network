using System;

namespace DomainHTN.Effects
{
    public class ActionEffect<T> : IEffect where T : IContext
    {
        private readonly Action<T, EffectType> _action;

        public ActionEffect(string name, EffectType type, Action<T, EffectType> action)
        {
            Name = name;
            Type = type;
            _action = action;
        }

        public string Name { get; }
        public EffectType Type { get; }

        public void Apply(IContext ctx)
        {
            if (ctx is T c)
            {
                if (ctx.LogDecomposition)
                    ctx.Log(Name, $"ActionEffect.Apply:{Type}", ctx.CurrentDecompositionDepth + 1, this);
                _action?.Invoke(c, Type);
            }
            else
                throw new Exception("Unexpected context type!");
        }
    }
}