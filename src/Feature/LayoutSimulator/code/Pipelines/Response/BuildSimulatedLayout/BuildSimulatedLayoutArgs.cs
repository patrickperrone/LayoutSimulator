using System.Collections.Generic;
using System.Xml.Linq;
using Sitecore.Pipelines;
using Sitecore.Mvc.Presentation;
using System.Collections.Specialized;

namespace Sitecore.Feature.LayoutSimulator.Pipelines.Response.BuildSimulatedLayout
{
    public class BuildSimulatedLayoutArgs : PipelineArgs
    {
        public PageDefinition OriginalPageDefinition { get; set; }
        public bool ApplySimulatedDefinition { get; set; }
        public XElement LayoutToSimulate { get; set; }
        public List<Rendering> SimulatedRenderings { get; set; }
        public NameValueCollection QueryStringParameters { get; set; }
    }
}