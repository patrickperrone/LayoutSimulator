using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Sitecore.Mvc.Configuration;
using Sitecore.Mvc.Extensions;
using Sitecore.Mvc.Presentation;
using Sitecore.Diagnostics;
using Sitecore.Feature.LayoutSimulator.Extensions;

namespace Sitecore.Feature.LayoutSimulator.Pipelines.Response.BuildSimulatedLayout
{
    public class GenerateRenderingList : BuildSimulatedLayoutProcessor
    {
        public override void Process(BuildSimulatedLayoutArgs args)
        {
            Assert.ArgumentNotNull(args.LayoutToSimulate, "LayoutToSimulate");

            List<Rendering> list =  GetRenderings(args.LayoutToSimulate).ToList<Rendering>();
            args.SimulatedRenderings = list;
            args.ApplySimulatedDefinition = true;
            Log.Info($"Simulated rendering count (including layout) is {args.SimulatedRenderings.Count.ToString()}".ToStringForLogging(), this);
        }

        public virtual IEnumerable<Rendering> GetRenderings(XElement layoutDefinition)
        {
            XmlBasedRenderingParser parser = MvcSettings.GetRegisteredObject<XmlBasedRenderingParser>();
            foreach (XElement xelement in layoutDefinition.Elements((XName)"d"))
            {
                Guid deviceId = Mvc.Extensions.StringExtensions.ToGuid(XmlExtensions.GetAttributeValueOrEmpty(xelement, "id"));
                Guid layoutId = Mvc.Extensions.StringExtensions.ToGuid(XmlExtensions.GetAttributeValueOrEmpty(xelement, "l"));
                yield return this.CreateRendering(xelement, deviceId, layoutId, "Layout", parser);
                foreach (XElement renderingNode in xelement.Elements((XName)"r"))
                    yield return this.CreateRendering(renderingNode, deviceId, layoutId, renderingNode.Name.LocalName, parser);
            }
        }

        public virtual Rendering CreateRendering(XElement renderingNode, Guid deviceId, Guid layoutId, string renderingType, XmlBasedRenderingParser parser)
        {
            Rendering rendering = parser.Parse(renderingNode, false);
            rendering.DeviceId = deviceId;
            rendering.LayoutId = layoutId;
            if (renderingType != null)
                rendering.RenderingType = renderingType;
            return rendering;
        }
    }
}