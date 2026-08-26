using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Application.Features.Movies.Commands.DeleteMovie
{
    public sealed class DeleteMovieCommandValidator
        : AbstractValidator<DeleteMovieCommand>
    {
        public DeleteMovieCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();
        }
    }
}
