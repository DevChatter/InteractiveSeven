namespace InteractiveSeven.Core.Events
{
    public interface IHandle<in T> where T : BaseDomainEvent
    {
        void Handle(T domainEvent);
    }
}