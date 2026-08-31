using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movie.Domain.Entities.Hall;
using Movie.Domain.Entities.MovieGenre;
using Movie.Domain.Entities.Seat;
using Movie.Domain.Entities.Session;
using Movie.Infrastructure.Persistence.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Infrastructure.Persistence.Configurations
{
    public class HallConfiguration : IEntityTypeConfiguration<Hall>
    {
        public void Configure(EntityTypeBuilder<Hall> builder)
        {
            builder.ToTable("Halls");

            builder.HasKey(x => x.Id);

            builder.ConfigureAudit();

            builder.Property(x => x.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.HasIndex(x => x.Name);

            builder.Property(x => x.Rows)
                .IsRequired();

            builder.Property(x => x.Columns)
                .IsRequired();

            builder.Property(x => x.Status)
                .HasConversion<byte>();

            builder.HasMany(x => x.Seats)
                .WithOne(x => x.Hall)
                .HasForeignKey(x => x.HallId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Sessions)
                .WithOne(x => x.Hall)
                .HasForeignKey(x => x.HallId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Navigation(x => x.Seats)
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.Navigation(x => x.Sessions)
                .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
