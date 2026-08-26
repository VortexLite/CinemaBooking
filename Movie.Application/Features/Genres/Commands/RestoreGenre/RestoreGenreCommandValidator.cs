using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Application.Features.Genres.Commands.RestoreGenre
{
    public sealed class RestoreGenreCommandValidator
        : AbstractValidator<RestoreGenreCommand>
    {
        public RestoreGenreCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();
        }
    }
}
