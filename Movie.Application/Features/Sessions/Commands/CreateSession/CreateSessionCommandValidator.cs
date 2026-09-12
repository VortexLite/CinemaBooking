using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Application.Features.Sessions.Commands.CreateSession
{
    public sealed class CreateSessionCommandValidator
        : AbstractValidator<CreateSessionCommand>
    {
        public CreateSessionCommandValidator()
        {
            RuleFor(x => x.MovieId)
                .NotEmpty();

            RuleFor(x => x.HallId)
                .NotEmpty();

            RuleFor(x => x.StartTime)
                .NotEmpty();

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
