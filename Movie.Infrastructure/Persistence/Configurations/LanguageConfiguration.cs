using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movie.Domain.Entities.Language;
using Movie.Infrastructure.Persistence.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Infrastructure.Persistence.Configurations
{
    public sealed class LanguageConfiguration : IEntityTypeConfiguration<Language>
    {
        public void Configure(EntityTypeBuilder<Language> builder)
        {
            builder.ToTable("Languages");

            builder.HasKey(x => x.Id);

            builder.ConfigureAudit();

            builder.Property(x => x.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.NativeName)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.IsoCode)
                .HasMaxLength(5)
                .IsRequired();

            builder.HasIndex(x => x.IsoCode)
                .IsUnique();

            builder.HasMany(x => x.Movies)
                .WithOne(x => x.Language)
                .HasForeignKey(x => x.LanguageId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
