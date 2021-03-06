using System;
using System.Collections.Generic;
using DomainHTN.Conditions;

namespace DomainHTN.Tasks
{
    public abstract class CompoundTask : ICompoundTask
    {
        public string Name { get; set; }
        public ICompoundTask Parent { get; set; }
        public List<ICondition> Conditions { get; } = new List<ICondition>();
        public List<ITask> Subtasks { get; } = new List<ITask>();

        public virtual DecompositionStatus OnIsValidFailed(IContext ctx)
        {
            return DecompositionStatus.Failed;
        }


        public ITask AddCondition(ICondition condition)
        {
            Conditions.Add(condition);
            return this;
        }

        public ICompoundTask AddSubtask(ITask subtask)
        {
            Subtasks.Add(subtask);
            return this;
        }

        public DecompositionStatus Decompose(IContext ctx, int startIndex, out Queue<ITask> result)
        {
            if (ctx.LogDecomposition) ctx.CurrentDecompositionDepth++;
            var status = OnDecompose(ctx, startIndex, out result);
            if (ctx.LogDecomposition) ctx.CurrentDecompositionDepth--;
            return status;
        }

        protected abstract DecompositionStatus OnDecompose(IContext ctx, int startIndex, out Queue<ITask> result);

        protected abstract DecompositionStatus OnDecomposeTask(IContext ctx, ITask task, int taskIndex, int[] oldStackDepth, out Queue<ITask> result);

        protected abstract DecompositionStatus OnDecomposeCompoundTask(IContext ctx, ICompoundTask task, int taskIndex, int[] oldStackDepth, out Queue<ITask> result);

        protected abstract DecompositionStatus OnDecomposeSlot(IContext ctx, Slot task, int taskIndex, int[] oldStackDepth, out Queue<ITask> result);

        public virtual bool IsValid(IContext ctx)
        {
            foreach (var condition in Conditions)
            {
                var result = condition.IsValid(ctx);
                if (ctx.LogDecomposition) Log(ctx, $"PrimitiveTask.IsValid:{(result ? "Success" : "Failed")}:{condition.Name} is{(result ? "" : " not")} valid!", result ? ConsoleColor.DarkGreen : ConsoleColor.DarkRed);
                if (result == false)
                {
                    return false;
                }
            }

            return true;
        }

        protected virtual void Log(IContext ctx, string description, ConsoleColor color = ConsoleColor.White)
        {
            ctx.Log(Name, description, ctx.CurrentDecompositionDepth, this, color);
        }
    }
}