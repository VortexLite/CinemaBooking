using Movie.Domain.Common;

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
