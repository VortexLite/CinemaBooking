using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Application.Features.Movies.Commands.ArchiveMovie
{
    public sealed class ArchiveMovieCommandValidator
        : AbstractValidator<ArchiveMovieCommand>
    {
        public ArchiveMovieCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();
        }
    }
}
