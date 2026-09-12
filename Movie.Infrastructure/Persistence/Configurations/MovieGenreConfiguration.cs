using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movie.Domain.Entities.MovieGenre;

namespace Movie.Infrastructure.Persistence.Configurations
{
    public class MovieGenreConfiguration : IEntityTypeConfiguration<MovieGenre>
    {
        public void Configure(EntityTypeBuilder<MovieGenre> builder)
        {
            builder.ToTable("MovieGenres");

            builder.HasKey(x => new
            {
                x.MovieId,
                x.GenreId
            });

            builder.Property(x => x.MovieId)
                .IsRequired();

            builder.Property(x => x.GenreId)
                .IsRequired();

            builder.HasOne(x => x.Movie)
                .WithMany(x => x.MovieGenres)
                .HasForeignKey(x => x.MovieId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Genre)
                .WithMany(x => x.MovieGenres)
                .HasForeignKey(x => x.GenreId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
