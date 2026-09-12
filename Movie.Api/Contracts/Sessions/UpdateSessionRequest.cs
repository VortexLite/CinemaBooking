using Movie.Domain.Enums;

namespace Movie.Api.Contracts.Sessions
{
    public sealed record UpdateSessionRequest(
        Guid HallId,
        DateTime StartTime,
        decimal Price,
        ProjectionType ProjectionType,
        SubtitleType SubtitleType,
        SoundFormat SoundFormat);
}
