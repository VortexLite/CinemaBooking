using Movie.Domain.Common;
using Movie.Domain.Enums;

namespace Movie.Domain.Entities.Seat
{
    public sealed class Seat : AggregateRoot
    {
        public Guid HallId { get; private set; }
        public Hall.Hall Hall { get; private set; } = null!;
        public ushort Row { get; private set; }
        public ushort Number { get; private set; }
        public SeatType SeatType { get; private set; }

        private Seat() { }
        public static Seat Create(
            Guid hallId,
            ushort row,
            ushort number,
            SeatType seatType)
        {
            return new Seat
            {
                HallId = hallId,
                Row = row,
                Number = number,
                SeatType = seatType
            };
        }
    }
}
