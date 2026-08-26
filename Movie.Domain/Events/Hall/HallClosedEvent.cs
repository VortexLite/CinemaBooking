using Movie.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Domain.Events.Hall
{
    public sealed class HallClosedEvent : DomainEvent
    {
        public Guid HallId { get; }

        public HallClosedEvent(Guid hallId)
        {
            HallId = hallId;
        }
    }
}
