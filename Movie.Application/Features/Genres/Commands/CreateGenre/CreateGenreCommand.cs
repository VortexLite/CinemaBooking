using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Application.Features.Genres.Commands.CreateGenre
{
    public sealed record CreateGenreCommand(
        string Name)
        : IRequest<Guid>;
}
