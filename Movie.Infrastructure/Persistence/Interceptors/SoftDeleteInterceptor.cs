using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Movie.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Infrastructure.Persistence.Interceptors
{
    public sealed class SoftDeleteInterceptor : SaveChangesInterceptor
    {
        private static void UpdateSoftDelete(DbContext? context)
        {
            if (context is null)
                return;

            var utcNow = DateTime.UtcNow;

            foreach (var entry in context.ChangeTracker.Entries<ISoftDelete>())
            {
                if (entry.State != EntityState.Deleted)
                    continue;

                entry.State = EntityState.Modified;

                entry.Property(x => x.IsDeleted).CurrentValue = true;

                entry.Property(x => x.DeletedAt).CurrentValue = utcNow;
            }
        }

        public override InterceptionResult<int> SavingChanges(
            DbContextEventData eventData,
            InterceptionResult<int> result)
        {
            UpdateSoftDelete(eventData.Context);

            return base.SavingChanges(eventData, result);
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            UpdateSoftDelete(eventData.Context);

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}
