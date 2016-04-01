using System.Web;
using Sitecore.Diagnostics;
using Sitecore.Mvc.Pipelines.Response.BuildPageDefinition;
using Sitecore.Pipelines;
using Sitecore.Feature.LayoutSimulator.Extensions;

namespace Sitecore.Feature.LayoutSimulator.Pipelines.Response.BuildPageDefinition
{
    public class ProcessSimulatedLayoutDefinition : BuildPageDefinitionProcessor
    {
        public override void Process(BuildPageDefinitionArgs args)
        {
            Assert.ArgumentNotNull((object)args, "args");

            var pipelineArgs = new BuildSimulatedLayout.BuildSimulatedLayoutArgs
            {
                OriginalPageDefinition = args.Result,
                ApplySimulatedDefinition = false,
                QueryStringParameters = HttpUtility.ParseQueryString(HttpContext.Current.Request.QueryString.ToString())
            };

            Log.Info($"Starting pipeline {Configuration.Constants.LayoutSimulateMvcPipelineName}".ToStringForLogging(), this);
            CorePipeline.Run(Configuration.Constants.LayoutSimulateMvcPipelineName, pipelineArgs);

            if (pipelineArgs.ApplySimulatedDefinition)
            {
                Log.Info("Applying simulated layout's renderings to host page.".ToStringForLogging(), this);
                args.Result.Renderings = pipelineArgs.SimulatedRenderings;
            }
            else
            {
                Log.Warn("There was a problem simulating layout. Host page will be processed normally without a simulated layout definition.".ToStringForLogging(), this);
            }
        }
    }
}