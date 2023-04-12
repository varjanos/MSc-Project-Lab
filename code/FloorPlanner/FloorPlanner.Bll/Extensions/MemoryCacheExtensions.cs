using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;

namespace FloorPlanner.Bll.Extensions;

public static class MemoryCacheExtensions
{
    private const string Postfix = "_CancellationTokenSource";

    public static async Task<TItem> GetOrCreateWithErrorHandlingAsync<TItem>(this IMemoryCache cache, string key, Func<ICacheEntry, Task<TItem>> factory, MemoryCacheEntryOptions options)
    {
        if (options == null)
        {
            throw new ArgumentNullException(nameof(options));
        }

        var task = cache.GetOrCreateAsync(key, entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = options.AbsoluteExpirationRelativeToNow;
            entry.SlidingExpiration = options.SlidingExpiration;
            entry.Priority = options.Priority;

            entry.AddExpirationToken(cache.CreateToken(key));
            var item = factory(entry);

            entry.Size ??= 1; // if the factory has not set a size then default is 1

            return item;
        });

        try
        {
            return await task;
        }
        catch
        {
            cache.Remove(key);
            throw;
        }
    }

    public static IChangeToken CreateToken(this IMemoryCache memoryCache, string key)
    {
        var tokenSourceKey = key + Postfix;

        var source = memoryCache.Set(tokenSourceKey, new CancellationTokenSource(), new MemoryCacheEntryOptions { Size = 1 });

        return new CancellationChangeToken(source.Token);
    }

    public static bool Invalidate(this IMemoryCache memoryCache, string key)
    {
        var tokenSourceKey = key + Postfix;

        if (!memoryCache.TryGetValue(tokenSourceKey, out CancellationTokenSource source))
        {
            return false;
        }

        source.Cancel();
        memoryCache.Remove(key);
        memoryCache.Remove(tokenSourceKey);

        return true;
    }
}