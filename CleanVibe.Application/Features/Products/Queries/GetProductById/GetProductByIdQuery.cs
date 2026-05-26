using CleanVibe.Application.Features.Products;
using MediatR;

namespace CleanVibe.Application.Features.Products.Queries.GetProductById;

public sealed record GetProductByIdQuery(Guid Id) : IRequest<ProductDto?>;
