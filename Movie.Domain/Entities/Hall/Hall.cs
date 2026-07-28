using Movie.Domain.Common;
using Movie.Domain.Enums;
using Movie.Domain.Events.Hall;
using Movie.Domain.Extensions;

namespace Movie.Domain.Entities.Hall
{
    public sealed partial class Hall : AggregateRoot
    {
        public string Name { get; private set; } = string.Empty;
        public ushort Rows { get; private set; }
        public ushort Columns { get; private set; }
        public HallStatus Status { get; private set; }

        public ICollection<Seat.Seat> Seats { get; }
            = new List<Seat.Seat>();

        public ICollection<Session.Session> Sessions { get; }
            = new List<Session.Session>();

        private Hall() { }
        public Hall(
            string name,
            ushort rows,
            ushort columns)
        {
            if (rows == 0)
                throw new DomainException("Rows cannot be zero.");

            if (columns == 0)
                throw new DomainException("Columns cannot be zero.");

            Name = name;
            Rows = rows;
            Columns = columns;

            Raise(new HallCreatedEvent(Id));
        }
    }
}
