using FluentValidation;

namespace Movie.Application.Features.Languages.Commands.CreateLanguage
{
    public sealed class CreateLanguageCommandValidator
        : AbstractValidator<CreateLanguageCommand>
    {
        public CreateLanguageCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.NativeName)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.IsoCode)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(10);
        }
    }
}
