using CleanVibe.Api.Infrastructure;
using CleanVibe.Application;
using CleanVibe.Application.Features.Products;
using CleanVibe.Application.Features.Products.Commands.CreateProduct;
using CleanVibe.Application.Features.Products.Queries.GetProductById;
using CleanVibe.Infrastructure;
using CleanVibe.Persistence;
using Microsoft.AspNetCore.Http.HttpResults;
using MediatR;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);

var app = builder.Build();

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "CleanVibe API v1");
    });
}

app.MapScalarApiReference("/scalar/v1", options =>
{
    options.WithOpenApiRoutePattern("/openapi/{documentName}.json");
});

app.UseHttpsRedirection();

app.MapOpenApi();

app.MapPost("/products", async (CreateProductRequest body, IMediator mediator, CancellationToken cancellationToken) =>
    {
        ProductDto dto = await mediator.Send(
            new CreateProductCommand(body.Name, body.Price),
            cancellationToken);

        return TypedResults.Ok(dto);
    })
    .Produces<ProductDto>(StatusCodes.Status200OK);

app.MapGet("/products/{id:guid}",
    async Task<Results<Ok<ProductDto>, NotFound>> (Guid id, IMediator mediator, CancellationToken cancellationToken) =>
    {
        ProductDto? product = await mediator.Send(
            new GetProductByIdQuery(id),
            cancellationToken);

        return product is null ? TypedResults.NotFound() : TypedResults.Ok(product);
    })
    .Produces<ProductDto>(StatusCodes.Status200OK)
    .Produces(StatusCodes.Status404NotFound);

app.Run();

internal sealed record CreateProductRequest(string Name, decimal Price);
