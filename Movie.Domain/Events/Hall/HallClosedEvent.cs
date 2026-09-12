using Movie.Domain.Common;

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
