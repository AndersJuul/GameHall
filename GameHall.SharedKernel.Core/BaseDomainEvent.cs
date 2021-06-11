using System;

namespace GameHall.SharedKernel.Core
{
    public abstract class BaseDomainEvent
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime DateOccurred { get; protected set; } = DateTime.UtcNow;
    }
}