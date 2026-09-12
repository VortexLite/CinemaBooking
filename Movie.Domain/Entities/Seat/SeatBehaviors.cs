using Movie.Domain.Enums;

namespace Movie.Domain.Entities.Seat
{
    public sealed partial class Seat
    {
        public void ChangeType(SeatType seatType)
        {
            if (SeatType == seatType)
                return;

            SeatType = seatType;
        }
    }
}
