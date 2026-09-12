using MediatR;
using Movie.Application.Common.Interfaces;

namespace Movie.Application.Features.Genres.Queries.GetGenres
{
    public sealed class GetGenresQueryHandler
        : IRequestHandler<GetGenresQuery, List<GetGenresResponse>>
    {
        private readonly IGenreQueries _genreQueries;

        public GetGenresQueryHandler(IGenreQueries genreQueries)
        {
            _genreQueries = genreQueries;
        }

        public Task<List<GetGenresResponse>> Handle(GetGenresQuery request, 
            CancellationToken cancellationToken)
        {
            return _genreQueries.GetGenresAsync(request.IncludeDeleted, cancellationToken);
        }
    }
}
