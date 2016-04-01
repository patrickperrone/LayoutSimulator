using System;
using System.Web;
using System.Xml.Linq;
using Sitecore.Feature.LayoutSimulator.Data;
using Sitecore.Diagnostics;
using Sitecore.Feature.LayoutSimulator.Extensions;

namespace Sitecore.Feature.LayoutSimulator.Pipelines.Response.BuildSimulatedLayout
{
    public class HydrateLayoutDefinition : BuildSimulatedLayoutProcessor
    {
        public override void Process(BuildSimulatedLayoutArgs args)
        {
            SimulatedLayout simulatedLayout = GetFromMemory();

            if (simulatedLayout == null)
            {
                args.AbortPipeline();
                return;
            }

            try
            {
                args.LayoutToSimulate = XElement.Parse(simulatedLayout.LayoutDefinition);
            }
            catch (Exception exc)
            {
                Log.Error($"Failed to convert SimulatedLayout object to xml. {exc.Message}".ToStringForLogging(), this);
                args.AbortPipeline();
                return;
            }
        }

        private SimulatedLayout GetFromMemory()
        {
            var parameters = HttpUtility.ParseQueryString(HttpContext.Current.Request.QueryString.ToString());
            CacheManager cm = new CacheManager();
            string cacheKey = cm.GetCacheKey(parameters[Configuration.Settings.QueryStringKey]);
            SimulatedLayout cacheValue = CacheManager.cache.GetCacheValue(cacheKey) as SimulatedLayout;

            if (cacheValue == null)
            {
                Log.Error($"Failed to find SimulatedLayout object in cache with cacheKey [{cacheKey}]".ToStringForLogging(), this);
            }

            return cacheValue;
        }
    }
}