using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Cache.Redis
{
    public class RedisCacheService : ICacheService
    {
        private readonly IDatabase _cache;
        private readonly IConverter _converter;

        public RedisCacheService(IDatabase cache,
            IConverter converter)
        {
            _cache = cache;
            _converter = converter;
        }

        public async Task<T> Get<T>(string key)
        {
            string serializedValue = await _cache.StringGetAsync(key);
            if (serializedValue == RedisValue.Null)
            {
                return default;
            }
            return _converter.FromCacheType<T>(serializedValue);
        }

        public async Task<IEnumerable<T>> Get<T>(IEnumerable<string> keys)
        {
            var redisKeys = keys.Select(m => new RedisKey(m)).ToArray();
            var serializedValue = await _cache.StringGetAsync(redisKeys);
            var arr = new List<T>();
            foreach (var item in serializedValue)
            {
                if (item != RedisValue.Null)
                {
                    var t = _converter.FromCacheType<T>(item);
                    arr.Add(t);
                }
            }
            return arr;
        }

        public async Task Set<T>(string key, T value)
        {
            string serializedValue = _converter.ToCacheType<T>(value) as string;
            await _cache.StringSetAsync(key, serializedValue);
        }

        public async Task Set<T>(Dictionary<string, T> dict)
        {
            var objs = new List<KeyValuePair<RedisKey, RedisValue>>();

            foreach (var key in dict.Keys)
            {
                var value = dict[key];
                string serializedValue = _converter.ToCacheType<T>(value) as string;
                objs.Add(new KeyValuePair<RedisKey, RedisValue>(key, serializedValue));
            }

            await _cache.StringSetAsync(objs.ToArray());
        }

        public async Task Delete(string key)
        {
            await _cache.KeyDeleteAsync(key);
        }
    }
}
