using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movie.Domain.Entities.Genre;
using Movie.Domain.Entities.MovieGenre;
using Movie.Domain.Entities.Session;
using Movie.Infrastructure.Persistence.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Infrastructure.Persistence.Configurations
{
    public class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.ToTable("Genres");

            builder.HasKey(x => x.Id);

            builder.ConfigureAudit();

            builder.Property(x => x.Name)
                .HasMaxLength(100)
                .IsRequired();
            
            builder.HasIndex(x => x.Name)
                .IsUnique();

            builder.Metadata
                .FindNavigation(nameof(Genre.MovieGenres))!
                .SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
