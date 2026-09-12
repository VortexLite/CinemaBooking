using Movie.Domain.Common;

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
