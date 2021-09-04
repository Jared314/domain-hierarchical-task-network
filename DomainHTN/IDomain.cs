using System.Collections.Generic;
using DomainHTN.Tasks;

namespace DomainHTN
{
    public interface IDomain
    {
        TaskRoot Root { get; }
        void Add(ICompoundTask parent, ITask subtask);
        void Add(ICompoundTask parent, Slot slot);
    }
}