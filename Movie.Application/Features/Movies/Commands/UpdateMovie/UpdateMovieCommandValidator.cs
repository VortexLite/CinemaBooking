using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Application.Features.Movies.Commands.UpdateMovie
{
    public sealed class UpdateMovieCommandValidator
        : AbstractValidator<UpdateMovieCommand>
    {
        public UpdateMovieCommandValidator()
        {
            RuleFor(x => x.Id)
            .NotEmpty();

            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(3000);

            RuleFor(x => x.DurationMinutes)
                .GreaterThan(0);

            RuleFor(x => x.AgeRestriction)
                .InclusiveBetween((byte)0, (byte)21);

            RuleFor(x => x.PosterUrl)
                .NotEmpty();

            RuleFor(x => x.LanguageId)
                .NotEmpty();
        }
    }
}
