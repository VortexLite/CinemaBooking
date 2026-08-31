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
            Validate(name, rows, columns);

            Name = NormalizeName(name);
            Rows = rows;
            Columns = columns;

            GenerateSeats();

            Raise(new HallCreatedEvent(Id));
        }
    }
}
