using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movie.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Infrastructure.Persistence.Extensions
{
    public static class EntityConfigurationExtensions
    {
        public static void ConfigureAudit<TEntity>(
        this EntityTypeBuilder<TEntity> builder)
        where TEntity : AuditableEntity
        {
            builder.Property(x => x.CreatedAt)
                .IsRequired();

            builder.Property(x => x.UpdatedAt);
        }
    }
}
