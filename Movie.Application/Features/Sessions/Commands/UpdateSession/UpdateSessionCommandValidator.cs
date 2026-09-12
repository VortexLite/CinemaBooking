using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Application.Features.Sessions.Commands.UpdateSession
{
    public sealed class UpdateSessionCommandValidator
        : AbstractValidator<UpdateSessionCommand>
    {
        public UpdateSessionCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();

            RuleFor(x => x.HallId)
                .NotEmpty();

            RuleFor(x => x.StartTime)
                .NotEmpty()
                .Must(x => x.Kind == DateTimeKind.Utc);

            RuleFor(x => x.Price)
                .GreaterThan(0);

            RuleFor(x => x.ProjectionType)
                .IsInEnum();

            RuleFor(x => x.SubtitleType)
                .IsInEnum();

            RuleFor(x => x.SoundFormat)
                .IsInEnum();
        }
    }
}
