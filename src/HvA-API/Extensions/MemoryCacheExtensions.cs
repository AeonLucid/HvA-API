﻿using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace HvA_API.Extensions
{
    public static class MemoryCacheExtensions
    {

        public static async Task<T> GetValueAsync<T>(this IMemoryCache cache, string cacheKey, Func<Task<T>> retrieveData, TimeSpan cacheTime)
        {
            T result;

            if (cache.TryGetValue(cacheKey, out result)) return result;

            result = await retrieveData();

            cache.Set(cacheKey, result, new MemoryCacheEntryOptions().SetAbsoluteExpiration(cacheTime));

            return result;
        }

    }
}
