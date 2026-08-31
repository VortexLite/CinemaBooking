using Movie.Domain.Enums;

namespace Movie.Api.Contracts.Movies
{
    public sealed class GetMoviesRequest
    {
        public string? Search { get; init; }
        public MovieStatus? Status { get; init; }
        public Guid? GenreId { get; init; }
        public Guid? LanguageId { get; init; }
        public bool IncludeDeleted { get; init; } = false;
        public int Page { get; init; } = 1;
        public int PageSize { get; init; } = 10;
    }
}
