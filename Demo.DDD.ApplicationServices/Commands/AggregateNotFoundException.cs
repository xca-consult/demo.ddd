using System;
using System.Runtime.Serialization;

namespace Demo.DDD.ApplicationServices.Commands
{
    [Serializable]
    public class AggregateNotFoundException : NotFoundException
    {
        public AggregateNotFoundException(Guid aggregateId) : base($"Aggregate with id: {aggregateId} not found")
        {
        }
        protected AggregateNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}