using MediatR;

namespace Movie.Application.Features.Movies.Commands.CreateMovie
{
    public sealed record CreateMovieCommand(
        string Title,
        string Description,
        int DurationMinutes,
        byte AgeRestriction,
        string PosterUrl,
        string? TrailerUrl,
        DateOnly ReleaseDate,
        Guid LanguageId)
        : IRequest<Guid>;
}