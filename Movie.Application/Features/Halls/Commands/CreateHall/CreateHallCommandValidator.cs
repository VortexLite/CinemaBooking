using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Application.Features.Halls.Commands.CreateHall
{
    public sealed class CreateHallCommandValidator
        : AbstractValidator<CreateHallCommand>
    {
        public CreateHallCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Rows)
                .GreaterThan((ushort)0)
                .LessThanOrEqualTo((ushort)100);

            RuleFor(x => x.Columns)
                .GreaterThan((ushort)0)
                .LessThanOrEqualTo((ushort)100);
        }
    }
}
