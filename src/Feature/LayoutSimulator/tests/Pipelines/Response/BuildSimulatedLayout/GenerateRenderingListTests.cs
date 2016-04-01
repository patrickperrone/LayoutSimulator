using FluentAssertions;
using Sitecore.Feature.LayoutSimulator.Pipelines.Response.BuildSimulatedLayout;
using System.Collections.Specialized;
using System.Xml.Linq;
using Xunit;

namespace Sitecore.Feature.LayoutSimulator.Tests.Pipelines.Response.BuildSimulatedLayout
{
    public class GenerateRenderingListTests
    {
        [Theory]
        [InlineData(@"
<r><d id=""{FE5D7FDF-89C0-4D99-9AA3-B5FBD009C9F3}"" l=""{ED043C36-CD48-4EDF-A288-EC404EE1CD42}"">
<r ds=""{439EC71A-4231-45C8-B075-975BD41099A7}"" id=""{BDD730EB-F808-4C65-B94A-D2F03672EAFB}"" ph=""col-narrow-2_0f43a3b5-7236-40e6-b86e-5a52b75bbd8b"" uid=""{6A48C9D4-62D2-4007-A253-8D3D9E6E29E3}"" />
</d></r>
")]
        [InlineData(@"
<r><d id=""{FE5D7FDF-89C0-4D99-9AA3-B5FBD009C9F3}"" l=""{ED043C36-CD48-4EDF-A288-EC404EE1CD42}"">
<r id=""{E7AE3F87-CF66-40F2-A9F5-33ECF08BC777}"" ph=""head"" uid=""{E6CA18D8-321D-4105-9C38-0622E94BE400}"" />
</d></r>
")]
        [InlineData(@"
<r><d id=""{FE5D7FDF-89C0-4D99-9AA3-B5FBD009C9F3}"" l=""{ED043C36-CD48-4EDF-A288-EC404EE1CD42}"">
<r id=""{1149A3C7-C69A-4E6B-A6C3-32687EC2F8D4}"" par=""UseStaticPlaceholderNames&amp;Type=bg-secondary&amp;Media&amp;Parallax"" ph=""page-layout"" uid=""{F4FE27AF-B867-4E72-8808-843D5781A355}"" /></d></r>
")]
        public void Processor_SimulatedRenderingsCountIs2For(string layoutToSimulate)
        {
            var pipelineArgs = new BuildSimulatedLayoutArgs
            {
                OriginalPageDefinition = null,
                ApplySimulatedDefinition = false,
                QueryStringParameters = new NameValueCollection(),
                LayoutToSimulate = XElement.Parse(layoutToSimulate)
            };
            var proc = new GenerateRenderingList();

            proc.Process(pipelineArgs);

            pipelineArgs.Aborted.Should().BeFalse();
            pipelineArgs.ApplySimulatedDefinition.Should().BeTrue();
            pipelineArgs.SimulatedRenderings.Count.Should().Be(2);
        }
    }
}
