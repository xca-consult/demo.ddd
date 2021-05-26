using System.Collections.Generic;


namespace Demo.DDD.Domain
{
    public interface IDispatchableAggregate : IAggregate
    {
        IEnumerable<IEvent> EmittedEvents { get; }
    }
}