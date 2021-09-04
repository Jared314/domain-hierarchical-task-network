using System;
using System.Linq.Expressions;

namespace DomainHTN.Conditions
{
    public class ExpressionCondition<T> : ICondition where T : class, IContext
    {
        private readonly Expression<Func<T, bool>> _expression;
        private Func<T, bool> _compiledExpression = null;
        public string Name { get; }
        
        public ExpressionCondition(string name, Expression<Func<T, bool>> e)
        {
            Name = name;
            _expression = e;
        }
        
        public bool IsValid(IContext ctx)
        {
            if (_compiledExpression == null)
                _compiledExpression = _expression.Compile();
            return _compiledExpression(ctx as T);
        }
    }
}