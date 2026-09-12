using MediatR;
using Movie.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Application.Features.Sessions.Commands.CreateSession
{
    public sealed record CreateSessionCommand(
        Guid MovieId,
        Guid HallId,
        DateTime StartTime,
        decimal Price,
        ProjectionType ProjectionType,
        SubtitleType SubtitleType,
        SoundFormat SoundFormat)
        : IRequest<Guid>;
}
