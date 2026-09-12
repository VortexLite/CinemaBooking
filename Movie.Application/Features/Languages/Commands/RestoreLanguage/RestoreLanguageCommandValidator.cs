using FluentValidation;

namespace Movie.Application.Features.Languages.Commands.RestoreLanguage
{
    public sealed class RestoreLanguageCommandValidator
        : AbstractValidator<RestoreLanguageCommand>
    {
        public RestoreLanguageCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();
        }
    }
}
