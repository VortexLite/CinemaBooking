using Movie.Domain.Enums;
using Movie.Domain.Events.Hall;
using Movie.Domain.Exceptions;

namespace Movie.Domain.Entities.Hall
{
    public sealed partial class Hall
    {
        public void Update(
            string name,
            ushort rows,
            ushort columns)
        {
            Validate(name, rows, columns);

            string normalizedName = NormalizeName(name);

            bool nameChanged = Name != normalizedName;

            bool layoutChanged = Rows != rows || Columns != columns;

            if (!nameChanged && !layoutChanged)
                return;

            Name = normalizedName;

            if (layoutChanged)
                ResizeLayout(rows, columns);

            Raise(new HallUpdatedEvent(Id));
        }

        public void ChangeSeatType(Guid seatId, SeatType seatType)
        {
            var seat = _seats.FirstOrDefault(x => x.Id == seatId);

            if (seat is null)
                throw new DomainException("Seat does not belong to this hall.");

            if (seat.SeatType == seatType)
                return;

            seat.ChangeType(seatType);

            Raise(new HallUpdatedEvent(Id));
        }

        public void Delete()
        {
            if (IsDeleted)
                return;

            MarkDeleted();

            Raise(new HallDeletedEvent(Id));
        }

        public void Restore()
        {
            if (!IsDeleted)
                return;

            MarkRestored();

            Raise(new HallRestoredEvent(Id));
        }

        private void GenerateSeats()
        {
            for (ushort row = 1; row <= Rows; row++)
            {
                for (ushort number = 1; number <= Columns; number++)
                {
                    _seats.Add(new Seat.Seat(
                            Id,
                            row,
                            number,
                            SeatType.Standard));
                }
            }
        }

        private void ResizeLayout(ushort newRows, ushort newColumns)
        {
            var seatsToRemove = _seats
                .Where(x =>
                    x.Row > newRows ||
                    x.Number > newColumns)
                .ToList();

            foreach (var seat in seatsToRemove)
                _seats.Remove(seat);

            HashSet<(ushort Row, ushort Number)> existingSeats = _seats
                .Select(x => (x.Row, x.Number))
                .ToHashSet();

            for (ushort row = 1; row <= newRows; row++)
            {
                for (ushort number = 1; number <= newColumns; number++)
                {
                    if (existingSeats.Contains((row, number)))
                        continue;

                    var seat = new Seat.Seat(
                        Id,
                        row,
                        number,
                        SeatType.Standard);

                    _seats.Add(seat);

                    existingSeats.Add((row, number));
                }
            }

            Rows = newRows;
            Columns = newColumns;
        }

        private static string NormalizeName(string name)
        {
            return name.Trim();
        }
    }
}
