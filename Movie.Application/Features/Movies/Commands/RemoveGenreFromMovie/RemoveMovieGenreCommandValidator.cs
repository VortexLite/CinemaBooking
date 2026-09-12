using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Application.Features.Movies.Commands.RemoveGenreFromMovie
{
    public sealed class RemoveMovieGenreCommandValidator
        : AbstractValidator<RemoveMovieGenreCommand>
    {
        public RemoveMovieGenreCommandValidator()
        {
            RuleFor(x => x.MovieId)
                .NotEmpty();

            RuleFor(x => x.GenreId)
                .NotEmpty();
        }
    }
}
