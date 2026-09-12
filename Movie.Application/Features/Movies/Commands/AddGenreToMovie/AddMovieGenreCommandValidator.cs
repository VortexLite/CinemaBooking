using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Application.Features.Movies.Commands.AddGenreToMovie
{
    public sealed class AddMovieGenreCommandValidator
        : AbstractValidator<AddMovieGenreCommand>
    {
        public AddMovieGenreCommandValidator()
        {
            RuleFor(x => x.MovieId)
                .NotEmpty();

            RuleFor(x => x.GenreId)
                .NotEmpty();
        }
    }
}
