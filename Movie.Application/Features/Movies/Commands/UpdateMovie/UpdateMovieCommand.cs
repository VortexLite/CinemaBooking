using MediatR;

namespace Movie.Application.Features.Movies.Commands.UpdateMovie
{
    public sealed record UpdateMovieCommand(
        Guid Id,
        string Title,
        string Description,
        int DurationMinutes,
        byte AgeRestriction,
        string PosterUrl,
        string? TrailerUrl,
        DateOnly ReleaseDate,
        Guid LanguageId)
        : IRequest;
}
