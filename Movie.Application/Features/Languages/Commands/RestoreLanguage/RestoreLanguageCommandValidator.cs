using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

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
