using Movie.Domain.Enums;

namespace Movie.Application.Features.Sessions.Queries.GetSession
{
    public sealed class GetSessionResponse
    {
        public Guid Id { get; init; }

        public Guid MovieId { get; init; }

        public string MovieTitle { get; init; } = string.Empty;

        public Guid HallId { get; init; }

        public string HallName { get; init; } = string.Empty;

        public DateTime StartTime { get; init; }

        public DateTime EndTime { get; init; }

        public decimal Price { get; init; }

        public int SeatsCount { get; init; }

        public SessionStatus Status { get; init; }

        public ProjectionType ProjectionType { get; init; }

        public SubtitleType SubtitleType { get; init; }

        public SoundFormat SoundFormat { get; init; }
    }
}
