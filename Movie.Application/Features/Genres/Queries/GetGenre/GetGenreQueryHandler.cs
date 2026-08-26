using MediatR;
using Movie.Application.Common.Interfaces;
using Movie.Domain.Entities.Genre;
using Shared.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Application.Features.Genres.Queries.GetGenre
{
    public sealed class GetGenreQueryHandler
        : IRequestHandler<GetGenreQuery, GetGenreResponse>
    {
        private readonly IGenreQueries _genreQueries;

        public GetGenreQueryHandler(IGenreQueries genreQueries)
        {
            _genreQueries = genreQueries;
        }

        public async Task<GetGenreResponse> Handle(GetGenreQuery request, 
            CancellationToken cancellationToken)
        {
            return await _genreQueries.GetGenreAsync(request.Id,
                cancellationToken)
                ?? throw new NotFoundException(nameof(Genre),
                request.Id);
        }
    }
}
