using MediatR;
using Movie.Domain.Enums;

namespace Movie.Application.Features.Sessions.Commands.UpdateSession
{
    public sealed record UpdateSessionCommand(
        Guid Id,
        Guid HallId,
        DateTime StartTime,
        decimal Price,
        ProjectionType ProjectionType,
        SubtitleType SubtitleType,
        SoundFormat SoundFormat)
        : IRequest;
}
