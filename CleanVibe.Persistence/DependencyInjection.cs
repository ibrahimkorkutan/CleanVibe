using CleanVibe.Application.Common.Interfaces;
using CleanVibe.Persistence.Contexts;
using CleanVibe.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanVibe.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistenceServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var databaseName = configuration["Persistence:InMemoryDatabaseName"]
            ?? throw new InvalidOperationException(
                "Configuration key 'Persistence:InMemoryDatabaseName' is missing.");

        services.AddDbContext<AppDbContext>((_, options) =>
        {
            options.UseInMemoryDatabase(databaseName);
            options.AddInterceptors(new AuditEntityInterceptor());
        });

        services.AddScoped<IApplicationDbContext>(sp => sp.GetRequiredService<AppDbContext>());

        return services;
    }
}
