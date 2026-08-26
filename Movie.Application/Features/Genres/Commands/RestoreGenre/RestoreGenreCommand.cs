using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Application.Features.Genres.Commands.RestoreGenre
{
    public sealed record RestoreGenreCommand(Guid Id)
        : IRequest;
}
