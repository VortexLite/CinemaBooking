using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Application.Features.Movies.Commands.RestoreMovie
{
    public sealed record RestoreMovieCommand(Guid Id)
        : IRequest;
}
