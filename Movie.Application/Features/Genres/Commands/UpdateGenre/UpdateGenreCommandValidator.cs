using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Application.Features.Genres.Commands.UpdateGenre
{
    public sealed class UpdateGenreCommandValidator
        : AbstractValidator<UpdateGenreCommand>
    {
        public UpdateGenreCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();

            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100);
        }
    }
}
