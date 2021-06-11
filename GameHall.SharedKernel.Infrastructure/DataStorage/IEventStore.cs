using System;
using System.Collections.Generic;
using GameHall.SharedKernel.Core;

namespace GameHall.SharedKernel.Infrastructure.DataStorage
{
    public interface IEventStore
    {
        void SaveEvents(Guid aggregateId, IEnumerable<BaseDomainEvent> events, int expectedVersion);
        List<BaseDomainEvent> GetEventsForAggregate(Guid aggregateId);
    }
}