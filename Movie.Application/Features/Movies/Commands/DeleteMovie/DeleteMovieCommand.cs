using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Application.Features.Movies.Commands.DeleteMovie
{
    public sealed record DeleteMovieCommand(Guid Id)
        : IRequest;
}
