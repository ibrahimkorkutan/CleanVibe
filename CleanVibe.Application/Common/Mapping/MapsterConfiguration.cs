using CleanVibe.Application.Features.Products;
using CleanVibe.Domain.Entities;
using Mapster;

namespace CleanVibe.Application.Common.Mapping;

internal static class MapsterConfiguration
{
    public static void RegisterProductMappings(TypeAdapterConfig config)
    {
        config.NewConfig<Product, ProductDto>();
    }
}
