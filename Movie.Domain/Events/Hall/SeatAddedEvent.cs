using Movie.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Domain.Events.Hall
{
    public sealed class SeatAddedEvent : DomainEvent
    {
        public Guid HallId { get; }
        public Guid SeatId { get; }
        
        public SeatAddedEvent(Guid hallId, Guid seatId)
        {
            HallId = hallId;
            SeatId = seatId;
        }
    }
}
