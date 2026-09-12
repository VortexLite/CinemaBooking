using MediatR;
using Movie.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

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
