using FluentValidation;

namespace Movie.Application.Features.Movies.Commands.RemoveGenreFromMovie
{
    public sealed class RemoveMovieGenreCommandValidator
        : AbstractValidator<RemoveMovieGenreCommand>
    {
        public RemoveMovieGenreCommandValidator()
        {
            RuleFor(x => x.MovieId)
                .NotEmpty();

            RuleFor(x => x.GenreId)
                .NotEmpty();
        }
    }
}
