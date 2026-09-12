using FluentValidation;

namespace Movie.Application.Features.Languages.Commands.DeleteLanguage
{
    public sealed class DeleteLanguageCommandValidator
        : AbstractValidator<DeleteLanguageCommand>
    {
        public DeleteLanguageCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();
        }
    }
}
