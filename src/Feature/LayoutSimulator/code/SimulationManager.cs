using System;
using Sitecore.Feature.LayoutSimulator.Data;
using Sitecore.Mvc.Extensions;

namespace Sitecore.Feature.LayoutSimulator
{
    public class SimulationManager
    {
        private SimulatedLayout _simulatedLayout;

        public SimulationManager(string hostPageUrl, string layoutToSimulate)
        {
            if (hostPageUrl.IsWhiteSpaceOrNull())
            {
                hostPageUrl = Configuration.Settings.GetDefaultSimulationHostPageUrl(Context.Database);
            }

            if (layoutToSimulate.IsWhiteSpaceOrNull())
            {
                throw new ArgumentException("The layoutToSimulate parameter must not be null or whitespace.");
            }

            _simulatedLayout = new SimulatedLayout
            {
                HostPageUrl = hostPageUrl,
                LayoutDefinition = layoutToSimulate,
                Cacheable = true
            };
        }

        public SimulateResult RunSimulation()
        {
            SimulateResult result = new SimulateResult
            {
                Success = true,
                Message = string.Format("Layout simulation completed."),
                SimulatedHtml = GetSimulatedHtml()
            };
            return result;
        }

        private string GetSimulatedHtml()
        {
            // Generate a unique id and pass it to the host page url as a query string parameter
            string simulatedLayoutId = Guid.NewGuid().ToString();
            string url = AddIdToQueryString(_simulatedLayout.HostPageUrl, simulatedLayoutId);

            // NOTE: cannot use Session to pass objects in memory since pipeline processors 
            // execute as a different user than the current user.
           
            // Add layout definition object to custom Sitecore cache
            CacheManager cm = new CacheManager();
            CacheManager.cache.SetCacheValue(cm.GetCacheKey(simulatedLayoutId), _simulatedLayout);

            string html = new System.Net.WebClient().DownloadString(url);
            return html;
        }

        private string AddIdToQueryString(string url, string simulatedLayoutId)
        {
            Text.UrlString modifiedUrl = new Sitecore.Text.UrlString(url);
            modifiedUrl.Add(Configuration.Settings.QueryStringKey, simulatedLayoutId.ToString());
            return modifiedUrl.ToString();
        }
    }
}