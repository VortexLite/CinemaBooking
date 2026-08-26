using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Application.Features.Genres.Commands.CreateGenre
{
    public sealed class CreateGenreCommandValidator
        : AbstractValidator<CreateGenreCommand>
    {
        public CreateGenreCommandValidator() 
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100);
        }
    }
}
