using CleanVibe.Application.Common.Interfaces;
using CleanVibe.Application.Features.Products;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanVibe.Application.Features.Products.Queries.GetProductById;

public sealed class GetProductByIdQueryHandler(
    IApplicationDbContext context,
    ICacheService cacheService) : IRequestHandler<GetProductByIdQuery, ProductDto?>
{
    private static readonly TimeSpan ProductCacheDuration = TimeSpan.FromMinutes(5);

    public async Task<ProductDto?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var cacheKey = $"products:{request.Id}";

        ProductDto? cachedProduct = await cacheService.GetAsync<ProductDto>(cacheKey, cancellationToken);
        if (cachedProduct is not null)
        {
            return cachedProduct;
        }

        var productEntity = await context.Products
            .AsNoTracking()
            .Where(p => p.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (productEntity is null)
        {
            return null;
        }

        ProductDto product = productEntity.Adapt<ProductDto>();
        await cacheService.SetAsync(cacheKey, product, ProductCacheDuration, cancellationToken);

        return product;
    }
}
