namespace Movie.Application.Features.Halls.Queries.GetHalls
{
    public sealed class GetHallsResponse
    {
        public Guid Id { get; init; }

        public string Name { get; init; } = string.Empty;

        public ushort Rows { get; init; }

        public ushort Columns { get; init; }

        public int SeatsCount { get; init; }
    }
}
