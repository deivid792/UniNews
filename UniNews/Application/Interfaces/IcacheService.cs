namespace Uninews.Application.Interfaces;

public interface IcacheService
{
    Task<T?> GetAsync<T>(string key);

    Task<T> SetAsync<T>(string key, T value, TimeSpan? timeout = null);

    Task RemoveAsync(string key);
}