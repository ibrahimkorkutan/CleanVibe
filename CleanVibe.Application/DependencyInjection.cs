using System.Reflection;
using CleanVibe.Application.Common.Behaviors;
using CleanVibe.Application.Common.Mapping;
using FluentValidation;
using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace CleanVibe.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        var mapsterConfig = TypeAdapterConfig.GlobalSettings;
        MapsterConfiguration.RegisterProductMappings(mapsterConfig);
        services.AddSingleton(mapsterConfig);

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            cfg.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
        });

        return services;
    }
}
