using CleanVibe.Application.Common.Interfaces;

namespace CleanVibe.Infrastructure.Services;

public sealed class DateTimeService : IDateTimeService
{
    public DateTimeOffset UtcNow => DateTimeOffset.UtcNow;
}
