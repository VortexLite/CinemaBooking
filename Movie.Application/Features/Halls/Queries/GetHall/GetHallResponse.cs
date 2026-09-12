namespace Movie.Application.Features.Halls.Queries.GetHall
{
    public sealed class GetHallResponse
    {
        public Guid Id { get; init; }

        public string Name { get; init; } = string.Empty;

        public ushort Rows { get; init; }

        public ushort Columns { get; init; }

        public int SeatsCount { get; init; }

        public List<GetHallSeatResponse> Seats { get; init; } = [];
    }
}
