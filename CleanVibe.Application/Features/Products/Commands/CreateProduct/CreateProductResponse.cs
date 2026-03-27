namespace CleanVibe.Application.Features.Products.Commands.CreateProduct;

public sealed record CreateProductResponse(
    Guid Id,
    string Name,
    decimal Price,
    int StockQuantity,
    DateTimeOffset CreatedAt,
    DateTimeOffset? UpdatedAt,
    string? CreatedBy,
    string? UpdatedBy);
