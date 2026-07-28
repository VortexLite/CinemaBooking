using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Domain.Common
{
    public class DomainEvent : IDomainEvent
    {
        public Guid EventId { get; } = Guid.NewGuid();
        public DateTime OccurredOn { get; } = DateTime.UtcNow;
    }
}
