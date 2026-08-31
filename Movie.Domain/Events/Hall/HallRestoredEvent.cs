using Movie.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Domain.Events.Hall
{
    public sealed class HallRestoredEvent : DomainEvent
    {
        public Guid HallId { get; }

        public HallRestoredEvent(Guid hallId)
        {
            HallId = hallId;
        }
    }
}
