using Mapster;
using Movie.Application.Features.Movies.Queries.GetMovie;
using Movie.Application.Features.Movies.Queries.GetMovies;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Application.Common.Mapping
{
    public sealed class MovieMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Domain.Entities.Movie.Movie, GetMovieResponse>()

                .Map(dest => dest.LanguageName,
                     src => src.Language.Name)

                .Map(dest => dest.Status,
                     src => src.Status.ToString())

                .Map(dest => dest.Genres,
                     src => src.MovieGenres
                               .Select(x => x.Genre.Name));



            config.NewConfig<Domain.Entities.Movie.Movie, GetMoviesResponse>()

                .Map(dest => dest.LanguageName,
                     src => src.Language.Name)

                .Map(dest => dest.Status,
                     src => src.Status.ToString());
        }
    }
}
