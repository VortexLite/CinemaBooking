using Movie.Domain.Common;

namespace Movie.Domain.Events.Hall
{
    public sealed class HallCreatedEvent : DomainEvent
    {
        public Guid HallId { get; }

        public HallCreatedEvent(Guid hallId)
        {
            HallId = hallId;
        }
    }
}
