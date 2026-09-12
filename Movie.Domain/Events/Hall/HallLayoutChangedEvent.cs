using Movie.Domain.Common;

namespace Movie.Domain.Events.Hall
{
    public sealed class HallLayoutChangedEvent : DomainEvent
    {
        public Guid HallId { get; }
        public ushort Rows { get; }
        public ushort Columns { get; }

        public HallLayoutChangedEvent(Guid hallId, ushort rows, ushort columns)
        {
            HallId = hallId;
            Rows = rows;
            Columns = columns;
        }
    }
}
