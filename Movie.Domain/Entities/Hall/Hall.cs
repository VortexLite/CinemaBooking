using Movie.Domain.Common;
using Movie.Domain.Enums;
using Movie.Domain.Events.Hall;
using Movie.Domain.Exceptions;

namespace Movie.Domain.Entities.Hall
{
    public sealed partial class Hall : AggregateRoot
    {
        public string Name { get; private set; } = string.Empty;
        public ushort Rows { get; private set; }
        public ushort Columns { get; private set; }
        public HallStatus Status { get; private set; }

        private readonly List<Seat.Seat> _seats = [];
        public IReadOnlyCollection<Seat.Seat> Seats
            => _seats;

        private readonly List<Session.Session> _sessions = [];
        public IReadOnlyCollection<Session.Session> Sessions
            => _sessions;

        private Hall() { }
        public Hall(
            string name,
            ushort rows,
            ushort columns)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Name is required.");
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
