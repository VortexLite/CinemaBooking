using Movie.Domain.Enums;

namespace Movie.Api.Contracts.Sessions
{
    public sealed record CreateSessionRequest(
        Guid MovieId,
        Guid HallId,
        DateTime StartTime,
        decimal Price,
        ProjectionType ProjectionType,
        SubtitleType SubtitleType,
        SoundFormat SoundFormat);
}
