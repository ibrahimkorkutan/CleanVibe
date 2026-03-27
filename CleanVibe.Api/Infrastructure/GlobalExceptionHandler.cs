using System.Net.Mime;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;

namespace CleanVibe.Api.Infrastructure;

public sealed class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is ValidationException validationException)
        {
            var errors = validationException.Errors
                .Select(e => new ValidationError(e.PropertyName, e.ErrorMessage))
                .ToList();

            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            httpContext.Response.ContentType = MediaTypeNames.Application.Json;

            await httpContext.Response.WriteAsJsonAsync(
                new ValidationProblemBody(errors),
                cancellationToken);
            return true;
        }

        logger.LogError(exception, "Unhandled exception");

        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        httpContext.Response.ContentType = MediaTypeNames.Application.Json;

        await httpContext.Response.WriteAsJsonAsync(
            new ServerErrorBody(
                "Internal Server Error",
                "An unexpected error occurred."),
            cancellationToken);

        return true;
    }

    private sealed record ValidationError(string PropertyName, string Message);

    private sealed record ValidationProblemBody(IReadOnlyList<ValidationError> Errors);

    private sealed record ServerErrorBody(string Title, string Message);
}
