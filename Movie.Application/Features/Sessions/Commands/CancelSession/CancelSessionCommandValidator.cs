using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Application.Features.Sessions.Commands.CancelSession
{
    public sealed class CancelSessionCommandValidator
        : AbstractValidator<CancelSessionCommand>
    {
        public CancelSessionCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();

            RuleFor(x => x.Reason)
                .NotEmpty()
                .MaximumLength(500);
        }
    }
}
