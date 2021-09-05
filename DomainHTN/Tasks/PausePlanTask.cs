using System;
using System.Collections.Generic;
using DomainHTN.Conditions;

namespace DomainHTN.Tasks
{
    public class PausePlanTask : ITask
    {
        public string Name { get; set; }
        public ICompoundTask Parent { get; set; }
        public List<ICondition> Conditions { get; } = null;
        public List<IEffect> Effects { get; } = null;

        public DecompositionStatus OnIsValidFailed(IContext ctx)
        {
            return DecompositionStatus.Failed;
        }

        public ITask AddCondition(ICondition condition)
        {
            throw new Exception("Pause Plan tasks does not support conditions.");
        }

        public ITask AddEffect(IEffect effect)
        {
            throw new Exception("Pause Plan tasks does not support effects.");
        }

        public void ApplyEffects(IContext ctx)
        {
        }

        public bool IsValid(IContext ctx)
        {
            if (ctx.LogDecomposition) Log(ctx, $"PausePlanTask.IsValid:Success!");
            return true;
        }

        protected virtual void Log(IContext ctx, string description)
        {
            ctx.Log(Name, description, ctx.CurrentDecompositionDepth, this, ConsoleColor.Green);
        }
    }
}