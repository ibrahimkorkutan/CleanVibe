using System.Text.Json;
using CleanVibe.Application.Common.Interfaces;
using StackExchange.Redis;

namespace CleanVibe.Infrastructure.Services;

public sealed class RedisCacheService(IConnectionMultiplexer connectionMultiplexer) : ICacheService
{
    private static readonly JsonSerializerOptions SerializerOptions = new(JsonSerializerDefaults.Web);
    private readonly IDatabase _database = connectionMultiplexer.GetDatabase();

    public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        RedisValue value = await _database.StringGetAsync(key);
        if (!value.HasValue)
        {
            return default;
        }

        return JsonSerializer.Deserialize<T>(value.ToString(), SerializerOptions);
    }

    public async Task SetAsync<T>(
        string key,
        T value,
        TimeSpan? expiry = null,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        string serializedValue = JsonSerializer.Serialize(value, SerializerOptions);
        var redisExpiry = expiry.HasValue ? (Expiration)expiry.Value : default(Expiration);
        await _database.StringSetAsync(key, serializedValue, redisExpiry);
    }

    public async Task RemoveAsync(string key, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        await _database.KeyDeleteAsync(key);
    }
}
