using Sitecore.Caching;
using Sitecore.Feature.LayoutSimulator.Data;

namespace Sitecore.Feature.LayoutSimulator
{
    public class LayoutDefinitionCache : CustomCache
    {
        public LayoutDefinitionCache(long maxSize) : base("LayoutSimulate.LayoutDefinitionCache", maxSize)
        {
        }

        public SimulatedLayout GetCacheValue(string cacheKey)
        {
            return GetObject(cacheKey) as SimulatedLayout;
        }

        public void SetCacheValue(string cacheKey, SimulatedLayout value)
        {
            SetObject(cacheKey, value);
        }
    }

    public class CacheManager
    {
        public static readonly LayoutDefinitionCache cache = new LayoutDefinitionCache(Configuration.Settings.CacheSize);

        public string GetCacheKey(string simulatedLayoutId)
        {
            return $"{Configuration.Settings.CacheKeyPrefix}{simulatedLayoutId}";
        }
    }
}