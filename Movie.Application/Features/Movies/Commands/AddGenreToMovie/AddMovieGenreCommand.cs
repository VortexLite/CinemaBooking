using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Application.Features.Movies.Commands.AddGenreToMovie
{
    public sealed record AddMovieGenreCommand(
        Guid MovieId,
        Guid GenreId)
        : IRequest;
}
