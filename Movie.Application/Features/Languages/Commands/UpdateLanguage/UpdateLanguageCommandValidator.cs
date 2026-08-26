using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Application.Features.Languages.Commands.UpdateLanguage
{
    public sealed class UpdateLanguageCommandValidator
        : AbstractValidator<UpdateLanguageCommand>
    {
        public UpdateLanguageCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();

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
