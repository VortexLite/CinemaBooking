using Movie.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Domain.Events.Hall
{
    public sealed class SeatRemovedEvent : DomainEvent
    {
        public Guid HallId { get; }
        public Guid SeatId { get; }
        public SeatRemovedEvent(Guid hallId, Guid seatId)
        {
            HallId = hallId;
            SeatId = seatId;
        }
    }
}
