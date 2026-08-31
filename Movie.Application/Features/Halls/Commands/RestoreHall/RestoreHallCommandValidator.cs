using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Application.Features.Halls.Commands.RestoreHall
{
    public sealed class RestoreHallCommandValidator
        : AbstractValidator<RestoreHallCommand>
    {
        public RestoreHallCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();
        }
    }
}
