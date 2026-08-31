using Movie.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Domain.Events.Hall
{
    public sealed class HallDeletedEvent : DomainEvent
    {
        public Guid HallId { get; }

        public HallDeletedEvent(Guid hallId)
        {
            HallId = hallId;
        }
    }
}
