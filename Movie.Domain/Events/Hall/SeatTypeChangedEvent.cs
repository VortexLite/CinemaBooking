using Movie.Domain.Common;
using Movie.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Domain.Events.Hall
{
    public sealed class SeatTypeChangedEvent : DomainEvent
    {
        public Guid SeatId { get; }

        public SeatType SeatType { get; }

        public SeatTypeChangedEvent(Guid seatId, SeatType seatType)
        {
            SeatId = seatId;
            SeatType = seatType;
        }
    }
}
