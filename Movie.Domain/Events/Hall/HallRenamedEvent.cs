using Movie.Domain.Common;

namespace Movie.Domain.Events.Hall
{
    public sealed class HallRenamedEvent : DomainEvent
    {
        public Guid HallId { get; }
        public string Name { get; }

        public HallRenamedEvent(Guid hallId, string name)
        {
            HallId = hallId;
            Name = name;
        }
    }
}
