using Microsoft.Extensions.Caching.Memory;

namespace CommissionManagement.Services.Security
{
    public class RequestFloodGuardService : IRequestFloodGuardService
    {
        private readonly IMemoryCache _cache;

        public RequestFloodGuardService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public bool IsDuplicate(string fingerprint, TimeSpan ttl)
        {
            if (_cache.TryGetValue(fingerprint, out _))
                return true;

            _cache.Set(fingerprint, true, ttl);
            return false;
        }

        public bool TryGet<T>(string key, out T? value)
        {
            if (_cache.TryGetValue(key, out var cached) && cached is T typed)
            {
                value = typed;
                return true;
            }

            value = default;
            return false;
        }

        public void Set<T>(string key, T value, TimeSpan ttl)
        {
            _cache.Set(key, value, ttl);
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }
    }
}