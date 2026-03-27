namespace CleanVibe.Application.Common.Interfaces;

public interface IDateTimeService
{
    DateTimeOffset UtcNow { get; }
}
