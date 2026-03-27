namespace CleanVibe.Domain.Common;

public abstract class BaseEntity<TKey> : IAuditEntity
    where TKey : notnull
{
    public TKey Id { get; set; } = default!;

    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
}
