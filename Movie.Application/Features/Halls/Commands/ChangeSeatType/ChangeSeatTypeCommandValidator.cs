using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Application.Features.Halls.Commands.ChangeSeatType
{
    public sealed class ChangeSeatTypeCommandValidator
        : AbstractValidator<ChangeSeatTypeCommand>
    {
        public ChangeSeatTypeCommandValidator()
        {
            RuleFor(x => x.HallId)
            .NotEmpty();

            RuleFor(x => x.SeatId)
                .NotEmpty();

            RuleFor(x => x.Type)
                .IsInEnum();
        }
    }
}
