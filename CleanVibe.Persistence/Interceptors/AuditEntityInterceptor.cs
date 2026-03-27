using CleanVibe.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace CleanVibe.Persistence.Interceptors;

public sealed class AuditEntityInterceptor : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        ApplyAudit(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        ApplyAudit(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private static void ApplyAudit(DbContext? context)
    {
        if (context is null)
            return;

        var now = DateTimeOffset.UtcNow;
        const string systemUser = "System";

        foreach (var entry in context.ChangeTracker.Entries())
        {
            if (entry.Entity is not IAuditEntity audit)
                continue;

            switch (entry.State)
            {
                case EntityState.Added:
                    audit.CreatedAt = now;
                    audit.CreatedBy = systemUser;
                    break;
                case EntityState.Modified:
                    audit.UpdatedAt = now;
                    audit.UpdatedBy = systemUser;
                    break;
            }
        }
    }
}
