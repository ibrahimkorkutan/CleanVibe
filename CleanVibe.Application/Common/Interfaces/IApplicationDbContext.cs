using CleanVibe.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CleanVibe.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Product> Products { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
