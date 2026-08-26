using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Application.Features.Movies.Commands.CreateMovie
{
    public sealed class CreateMovieCommandValidator
        : AbstractValidator<CreateMovieCommand>
    {
        public CreateMovieCommandValidator()
        {
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

            RuleFor(x => x.ReleaseDate)
                .NotEmpty();

            RuleFor(x => x.LanguageId)
                .NotEmpty();
        }
    }
}
