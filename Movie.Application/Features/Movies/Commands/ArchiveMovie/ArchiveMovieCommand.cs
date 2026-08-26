using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Application.Features.Movies.Commands.ArchiveMovie
{
    public sealed record ArchiveMovieCommand(Guid Id)
        : IRequest;
}
