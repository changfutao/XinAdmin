using FreeRedis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Xin.Infrastructure.Cache
{
    public class RedisCache : ICache
    {
        private readonly RedisClient _redisClient;

        public RedisCache(RedisClient redisClient)
        {
            _redisClient = redisClient;
        }
        public long Del(params string[] key)
        {
            return _redisClient.Del(key);
        }

        public Task<long> DelAsync(params string[] key)
        {
            return _redisClient.DelAsync(key);
        }

        public async Task<long> DelByPatternAsync(string pattern)
        {
            if (pattern == null)
                return default;

            pattern = Regex.Replace(pattern, @"\{.*\}", "*");

            var keys = await _redisClient.KeysAsync(pattern);
            if (keys != null && keys.Length > 0)
            {
                return await _redisClient.DelAsync(keys);
            }

            return default;
        }

        public bool Exists(string key)
        {
           return _redisClient.Exists(key);
        }

        public Task<bool> ExistsAsync(string key)
        {
            return _redisClient.ExistsAsync(key);
        }

        public string? Get(string key)
        {
            return _redisClient.Get(key);
        }

        public T? Get<T>(string key)
        {
            return _redisClient.Get<T>(key);
        }

        public Task<string?> GetAsync(string key)
        {
           return _redisClient.GetAsync(key);
        }

        public Task<T> GetAsync<T>(string key)
        {
            return _redisClient.GetAsync<T>(key);
        }

        public async Task<T?> GetOrSetAsync<T>(string key, Func<Task<T>> func, TimeSpan? expire = null)
        {
            if (await ExistsAsync(key))
            {
                try
                {
                    return await _redisClient.GetAsync<T>(key);
                }
                catch
                {
                    await _redisClient.DelAsync(key);
                }
            }
            var result = await func.Invoke();
            if (expire.HasValue)
            {
                await _redisClient.SetAsync(key, result, expire.Value);
            }
            else
            {
                await _redisClient.SetAsync(key, result);
            }
            return result;
        }

        public bool Set(string key, object value)
        {
            _redisClient.Set(key, value);
            return true;
        }

        public bool Set(string key, object value, TimeSpan expire)
        {
            _redisClient.Set(key, value, expire);
            return true;
        }

        public Task<bool> SetAsync(string key, object value)
        {
            _redisClient.SetAsync(key, value);
            return Task.FromResult(true);
        }

        public Task<bool> SetAsync(string key, object value, TimeSpan expire)
        {
            _redisClient.SetAsync(key, value, expire);
            return Task.FromResult(true);
        }
    }
}
