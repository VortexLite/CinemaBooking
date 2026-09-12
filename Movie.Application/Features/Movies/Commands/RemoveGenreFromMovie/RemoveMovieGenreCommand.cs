using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Application.Features.Movies.Commands.RemoveGenreFromMovie
{
    public sealed record RemoveMovieGenreCommand(
        Guid MovieId,
        Guid GenreId)
        : IRequest;
}
