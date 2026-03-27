using System.Reflection;
using CleanVibe.Application.Common.Interfaces;
using CleanVibe.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CleanVibe.Persistence.Contexts;

public class AppDbContext : DbContext, IApplicationDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
