using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Application.Features.Movies.Commands.RestoreMovie
{
    public sealed class RestoreMovieCommandValidator
        : AbstractValidator<RestoreMovieCommand>
    {
        public RestoreMovieCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();
        }
    }
}
