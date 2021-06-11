using System;
using System.Collections.Generic;
using GameHall.SharedKernel.Core;

namespace GameHall.SharedKernel.Infrastructure.DataStorage
{
    public class EventStore:IEventStore
    {
        public static string StreamId(BaseEntity baseEntity)
        {
            return $"{baseEntity.GetType().Name}-{baseEntity.Id}";
        }
        public static string StreamId(Guid id)
        {
            return $"Account-{id}";
        }

        public void SaveEvents(Guid aggregateId, IEnumerable<BaseDomainEvent> events, int expectedVersion)
        {
            throw new NotImplementedException();
        }

        public List<BaseDomainEvent> GetEventsForAggregate(Guid aggregateId)
        {
            throw new NotImplementedException();
        }
    }
}