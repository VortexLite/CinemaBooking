using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Application.Features.Genres.Commands.DeleteGenre
{
    public sealed class DeleteGenreCommandValidator
        : AbstractValidator<DeleteGenreCommand>
    {
        public DeleteGenreCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();
        }
    }
}
