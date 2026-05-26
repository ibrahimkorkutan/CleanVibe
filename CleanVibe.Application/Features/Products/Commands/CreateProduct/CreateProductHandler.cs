using CleanVibe.Application.Common.Interfaces;
using CleanVibe.Application.Features.Products;
using CleanVibe.Domain.Entities;
using Mapster;
using MediatR;

namespace CleanVibe.Application.Features.Products.Commands.CreateProduct;

public sealed class CreateProductHandler(IApplicationDbContext context)
    : IRequestHandler<CreateProductCommand, ProductDto>
{
    public async Task<ProductDto> Handle(
        CreateProductCommand request,
        CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Price = request.Price,
        };

        context.Products.Add(product);
        await context.SaveChangesAsync(cancellationToken);

        return product.Adapt<ProductDto>();
    }
}
