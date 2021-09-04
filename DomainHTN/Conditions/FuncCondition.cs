using System;

namespace DomainHTN.Conditions
{
    public class FuncCondition<T> : ICondition where T : IContext
    {
        private readonly Func<T, bool> _func;

        public FuncCondition(string name, Func<T, bool> func)
        {
            Name = name;
            _func = func;
        }

        public string Name { get; }

        public bool IsValid(IContext ctx)
        {
            if (ctx is T c)
            {
                var result = _func?.Invoke(c) ?? false;
                if (ctx.LogDecomposition)
                    ctx.Log(Name, $"FuncCondition.IsValid:{result}", ctx.CurrentDecompositionDepth + 1, this,
                        result ? ConsoleColor.DarkGreen : ConsoleColor.DarkRed);
                return result;
            }

            throw new Exception("Unexpected context type!");
        }
    }
}