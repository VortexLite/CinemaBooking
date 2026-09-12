using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Movie.Domain.Common;

namespace Movie.Infrastructure.Persistence.Interceptors
{
    public sealed class AuditableEntityInterceptor : SaveChangesInterceptor
    {
        private static void UpdateAudit(DbContext? context)
        {
            if (context is null)
                return;

            var utcNow = DateTime.UtcNow;

            foreach (var entry in context.ChangeTracker.Entries<IAuditable>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:

                        entry.Property(nameof(IAuditable.CreatedAt)).CurrentValue = utcNow;

                        break;

                    case EntityState.Modified:

                        entry.Property(nameof(IAuditable.UpdatedAt)).CurrentValue = utcNow;

                        break;
                }
            }
        }

        public override InterceptionResult<int> SavingChanges(
            DbContextEventData eventData,
            InterceptionResult<int> result)
        {
            UpdateAudit(eventData.Context);

            return base.SavingChanges(eventData, result);
        }

        public override ValueTask<InterceptionResult<int>>
            SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            UpdateAudit(eventData.Context);

            return base.SavingChangesAsync(
                eventData,
                result,
                cancellationToken);
        }
    }
}
