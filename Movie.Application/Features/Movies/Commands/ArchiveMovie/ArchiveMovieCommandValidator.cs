using FluentValidation;

namespace Movie.Application.Features.Movies.Commands.ArchiveMovie
{
    public sealed class ArchiveMovieCommandValidator
        : AbstractValidator<ArchiveMovieCommand>
    {
        public ArchiveMovieCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();
        }
    }
}
