using System;
using System.Collections.Generic;

namespace GameHall.SharedKernel.Core
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }

        public List<BaseDomainEvent> Events = new List<BaseDomainEvent>();
    }

    public abstract class BaseAggregateEntity:BaseEntity
    {
        public abstract void AddEvent(BaseDomainEvent domainEvent);
    }
}