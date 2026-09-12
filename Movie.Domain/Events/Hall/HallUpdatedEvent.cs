using Movie.Domain.Common;

namespace Movie.Domain.Events.Hall
{
    public sealed class HallUpdatedEvent : DomainEvent
    {
        public Guid HallId { get; }

        public HallUpdatedEvent(Guid hallId)
        {
            HallId = hallId;
        }
    }
}
