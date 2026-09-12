using Movie.Domain.Common;

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
