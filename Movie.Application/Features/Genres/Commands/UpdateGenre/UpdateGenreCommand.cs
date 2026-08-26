using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Application.Features.Genres.Commands.UpdateGenre
{
    public sealed record UpdateGenreCommand
        (Guid Id,
        string Name)
        : IRequest;
}
