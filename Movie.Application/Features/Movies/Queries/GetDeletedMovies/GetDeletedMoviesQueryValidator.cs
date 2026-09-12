using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Application.Features.Movies.Queries.GetDeletedMovies
{
    public sealed class GetDeletedMoviesQueryValidator
        : AbstractValidator<GetDeletedMoviesQuery>
    {
        public GetDeletedMoviesQueryValidator()
        {
            RuleFor(x => x.Page)
                .GreaterThan(0);

            RuleFor(x => x.PageSize)
                .InclusiveBetween(1, 100);
        }
    }
}
