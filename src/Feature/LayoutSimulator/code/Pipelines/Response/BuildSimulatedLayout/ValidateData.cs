using System;
using System.Web;
using System.Collections.Specialized;
using Sitecore.Diagnostics;
using Sitecore.Mvc.Extensions;
using Sitecore.Feature.LayoutSimulator.Extensions;

namespace Sitecore.Feature.LayoutSimulator.Pipelines.Response.BuildSimulatedLayout
{
    public class ValidateData : BuildSimulatedLayoutProcessor
    {
        public override void Process(BuildSimulatedLayoutArgs args)
        {
            Assert.ArgumentNotNull(args, "args");

            args.ApplySimulatedDefinition = false;

            if (args.OriginalPageDefinition == null || args.QueryStringParameters == null)
            {
                args.AbortPipeline();
                return;
            }

            if (args.QueryStringParameters.Get(Configuration.Settings.QueryStringKey).IsEmptyOrNull())
            {
                args.AbortPipeline();
                return;
            }

            if (!ValidQueryStringParameter(args.QueryStringParameters))
            {
                args.AbortPipeline();
                return;
            }
        }

        public bool ValidQueryStringParameter(NameValueCollection parameters)
        {
            foreach (var kvp in parameters.ToKeyValues())
            {
                if (kvp.Key.Equals(Configuration.Settings.QueryStringKey))
                {
                    Guid result = new Guid();
                    if (!Guid.TryParse(kvp.Value.ToString(), out result))
                    {
                        Log.Warn($"Value of [{Configuration.Settings.QueryStringKey}] must be a Guid".ToStringForLogging(), this);
                        return false;
                    }
                }
            }

            return true;
        }
    }
}