using Movie.Domain.Enums;

namespace Movie.Api.Contracts.Halls
{
    public sealed class ChangeSeatTypeRequest
    {
        public SeatType Type { get; init; }
    }
}
