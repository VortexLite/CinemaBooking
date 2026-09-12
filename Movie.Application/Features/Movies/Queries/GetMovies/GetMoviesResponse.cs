namespace Movie.Application.Features.Movies.Queries.GetMovies
{
    public sealed class GetMoviesResponse
    {
        public Guid Id { get; init; }

        public string Title { get; init; } = string.Empty;

        public string PosterUrl { get; init; } = string.Empty;

        public int DurationMinutes { get; init; }

        public DateOnly ReleaseDate { get; init; }

        public string Status { get; init; } = string.Empty;

        public string LanguageName { get; init; } = string.Empty;
    }
}
