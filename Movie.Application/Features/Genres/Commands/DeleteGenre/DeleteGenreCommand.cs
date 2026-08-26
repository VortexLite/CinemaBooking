using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Application.Features.Genres.Commands.DeleteGenre
{
    public sealed record DeleteGenreCommand(Guid Id)
        : IRequest;
}
