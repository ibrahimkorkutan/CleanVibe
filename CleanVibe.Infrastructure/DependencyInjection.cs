using CleanVibe.Application.Common.Interfaces;
using CleanVibe.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CleanVibe.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddSingleton<IDateTimeService, DateTimeService>();

        return services;
    }
}
