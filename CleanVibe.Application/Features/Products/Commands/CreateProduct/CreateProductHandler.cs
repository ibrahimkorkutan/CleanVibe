using CleanVibe.Application.Common.Interfaces;
using CleanVibe.Domain.Entities;
using MediatR;

namespace CleanVibe.Application.Features.Products.Commands.CreateProduct;

public sealed class CreateProductHandler(IApplicationDbContext context)
    : IRequestHandler<CreateProductCommand, CreateProductResponse>
{
    public async Task<CreateProductResponse> Handle(
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

        return new CreateProductResponse(
            product.Id,
            product.Name,
            product.Price,
            product.StockQuantity,
            product.CreatedAt,
            product.UpdatedAt,
            product.CreatedBy,
            product.UpdatedBy);
    }
}
