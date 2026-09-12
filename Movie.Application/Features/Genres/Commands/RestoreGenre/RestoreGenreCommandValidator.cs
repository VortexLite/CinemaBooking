using FluentValidation;

namespace Movie.Application.Features.Genres.Commands.RestoreGenre
{
    public sealed class RestoreGenreCommandValidator
        : AbstractValidator<RestoreGenreCommand>
    {
        public RestoreGenreCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();
        }
    }
}
