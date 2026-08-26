using Mapster;
using MediatR;
using Movie.Application.Abstractions.Interfaces;
using Movie.Application.Common.Interfaces;
using Shared.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Application.Features.Movies.Queries.GetMovie
{
    public sealed class GetMovieQueryHandler
        : IRequestHandler<GetMovieQuery, GetMovieResponse>
    {
        private readonly IMovieQueries _movieQueries;

        public GetMovieQueryHandler(
            IMovieQueries movieQueries)
        {
            _movieQueries = movieQueries;
        }

        public async Task<GetMovieResponse> Handle(
            GetMovieQuery request, 
            CancellationToken cancellationToken)
        {

            return await _movieQueries.GetMovieAsync(request.Id, cancellationToken)
                ?? throw new NotFoundException(
                nameof(Domain.Entities.Movie.Movie),
                request.Id);
        }
    }
}
