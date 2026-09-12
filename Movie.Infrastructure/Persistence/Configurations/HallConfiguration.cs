using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movie.Domain.Entities.Hall;
using Movie.Infrastructure.Persistence.Extensions;

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
