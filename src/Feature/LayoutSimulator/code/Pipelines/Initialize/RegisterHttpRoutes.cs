using System.Web.Http;
using Sitecore.Pipelines;
using System.Web.Mvc;

namespace Sitecore.Feature.LayoutSimulator.Pipelines.Initialize
{
    //public class RegisterHttpRoutes
    //{
    //    public void Process(PipelineArgs args)
    //    {
    //        GlobalConfiguration.Configure(Configure);
    //    }
    //    protected void Configure(HttpConfiguration configuration)
    //    {
    //        var routes = configuration.Routes;
    //        routes.MapHttpRoute("Sitecore.Feature.LayoutSimulator.Simulate", $"{Configuration.Settings.RoutePrefix}/simulate/{{hostPageUrl}}", new
    //        {
    //            controller = "LayoutSimulator",
    //            action = "Simulate",
    //            hostPageUrl = UrlParameter.Optional
    //        });

    //        routes.MapHttpRoute("Sitecore.Feature.LayoutSimulator.About", $"{Configuration.Settings.RoutePrefix}/about", new
    //        {
    //            controller = "LayoutSimulator",
    //            action = "About"
    //        });

    //    }
    //}
}