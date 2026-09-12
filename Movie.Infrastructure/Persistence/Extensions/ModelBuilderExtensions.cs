using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Movie.Domain.Common;
using System.Linq.Expressions;

namespace Movie.Infrastructure.Persistence.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void ApplySoftDeleteQueryFilter(
        this ModelBuilder modelBuilder)
        {
            foreach (IMutableEntityType entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (!typeof(ISoftDelete).IsAssignableFrom(entityType.ClrType))
                    continue;

                var parameter = Expression.Parameter(entityType.ClrType, "e");

                var body = Expression.Equal(
                    Expression.Property(parameter, nameof(ISoftDelete.IsDeleted)),
                    Expression.Constant(false));

                var lambda = Expression.Lambda(body, parameter);

                modelBuilder.Entity(entityType.ClrType)
                    .HasQueryFilter(lambda);
            }
        }
    }
}
