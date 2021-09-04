namespace DomainHTN.Conditions
{
    public interface ICondition
    {
        string Name { get; }
        bool IsValid(IContext ctx);
    }
}