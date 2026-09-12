using FluentValidation;

namespace Movie.Application.Features.Movies.Commands.RestoreMovie
{
    public sealed class RestoreMovieCommandValidator
        : AbstractValidator<RestoreMovieCommand>
    {
        public RestoreMovieCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();
        }
    }
}
