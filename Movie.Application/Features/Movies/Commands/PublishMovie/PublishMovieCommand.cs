using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Application.Features.Movies.Commands.PublishMovie
{
    public sealed record PublishMovieCommand(Guid Id)
        : IRequest;
}
