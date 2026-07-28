using Movie.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Domain.Events.Hall
{
    public sealed class HallCreatedEvent : DomainEvent
    {
        public Guid HallId { get; }

        public HallCreatedEvent(Guid hallId)
        {
            HallId = hallId;
        }
    }
}
