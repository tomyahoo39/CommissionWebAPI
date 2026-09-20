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
    }
}