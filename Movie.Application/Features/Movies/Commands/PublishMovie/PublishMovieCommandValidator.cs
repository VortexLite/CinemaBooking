using FluentValidation;

namespace Movie.Application.Features.Movies.Commands.PublishMovie
{
    public sealed class PublishMovieCommandValidator
        : AbstractValidator<PublishMovieCommand>
    {
        public PublishMovieCommandValidator() 
        {
            RuleFor(x => x.Id)
                .NotEmpty();
        }
    }
}
