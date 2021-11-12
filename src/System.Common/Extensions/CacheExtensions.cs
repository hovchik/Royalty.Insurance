using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace System.Common.Extensions
{
    public static class CacheExtensions
    {
        public static async Task<T> GetOrCreateAsync<T>(this IMemoryCache cache, int key) where T : new()
        {
            return await cache.GetOrCreateAsync(key,
                entry =>
                {
                    if (entry.Value != null)
                    {
                        return Task.FromResult((T)entry.Value);
                    }

                    return Task.FromResult(new T());
                });
        }

        public static async Task<List<T>> AddValue<T>(this IMemoryCache cache, int key, T value)
            where T : new()
        {
            List<T> values = await cache.GetOrCreateAsync<List<T>>(key);
            values.Add(value);

            return values;
        }

        public static async Task<List<T>> Remove<T>(this IMemoryCache cache, int key, T value)
            where T : new()
        {
            List<T> values = await cache.GetOrCreateAsync<List<T>>(key);
            values.Remove(value);

            return values;
        }
    }
}
