using CleanVibe.Application.Features.Products;
using MediatR;

namespace CleanVibe.Application.Features.Products.Commands.CreateProduct;

public sealed record CreateProductCommand(string Name, decimal Price) : IRequest<ProductDto>;
