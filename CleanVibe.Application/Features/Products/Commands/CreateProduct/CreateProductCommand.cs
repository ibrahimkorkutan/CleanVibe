using MediatR;

namespace CleanVibe.Application.Features.Products.Commands.CreateProduct;

public sealed record CreateProductCommand(string Name, decimal Price) : IRequest<CreateProductResponse>;
