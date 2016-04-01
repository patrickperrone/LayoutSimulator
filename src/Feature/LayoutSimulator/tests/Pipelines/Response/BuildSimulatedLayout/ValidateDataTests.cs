using FluentAssertions;
using Ploeh.AutoFixture.Xunit2;
using Sitecore.Feature.LayoutSimulator.Pipelines.Response.BuildSimulatedLayout;
using System;
using System.Collections.Specialized;
using Xunit;

namespace Sitecore.Feature.LayoutSimulator.Tests.Pipelines
{
    public class ValidateDataTests
    {
        [Fact]
        public void Processor_AbortsPipeline_WhenOriginalPageDefinitionIsNull()
        {
            var pipelineArgs = new BuildSimulatedLayoutArgs
            {
                OriginalPageDefinition = null,
                ApplySimulatedDefinition = false,
                QueryStringParameters = new NameValueCollection()
            };
            var proc = new ValidateData();

            proc.Process(pipelineArgs);

            pipelineArgs.Aborted.Should().BeTrue();
            pipelineArgs.ApplySimulatedDefinition.Should().BeFalse();
        }

        [Fact]
        public void Processor_AbortsPipeline_WhenQueryStringParametersIsNull()
        {
            var pipelineArgs = new BuildSimulatedLayoutArgs
            {
                OriginalPageDefinition = new Mvc.Presentation.PageDefinition(),
                ApplySimulatedDefinition = false,
                QueryStringParameters = null
            };
            var proc = new ValidateData();

            proc.Process(pipelineArgs);

            pipelineArgs.Aborted.Should().BeTrue();
            pipelineArgs.ApplySimulatedDefinition.Should().BeFalse();
        }

        [Theory, AutoData]
        public void Processor_AbortsPipeline_WhenQueryStringKeyIsMissing(string key1, string value1, string key2, string value2)
        {
            NameValueCollection nvc = new NameValueCollection();
            nvc.Add(key1, value1);
            nvc.Add(key2, value2);
            var pipelineArgs = new BuildSimulatedLayoutArgs
            {
                OriginalPageDefinition = new Mvc.Presentation.PageDefinition(),
                ApplySimulatedDefinition = false,
                QueryStringParameters = nvc
            };
            var proc = new ValidateData();

            proc.Process(pipelineArgs);

            pipelineArgs.Aborted.Should().BeTrue();
            pipelineArgs.ApplySimulatedDefinition.Should().BeFalse();
        }

        [Theory, AutoData]
        public void Processor_AbortsPipeline_WhenQueryStringParameterNotAGuid(string key1, string value1)
        {
            NameValueCollection nvc = new NameValueCollection();
            nvc.Add(key1, value1);
            nvc.Add(LayoutSimulator.Configuration.Settings.QueryStringKey, "not a guid");
            var pipelineArgs = new BuildSimulatedLayoutArgs
            {
                OriginalPageDefinition = new Mvc.Presentation.PageDefinition(),
                ApplySimulatedDefinition = false,
                QueryStringParameters = nvc
            };
            var proc = new ValidateData();

            proc.Process(pipelineArgs);

            pipelineArgs.Aborted.Should().BeTrue();
            pipelineArgs.ApplySimulatedDefinition.Should().BeFalse();
        }

        [Theory, AutoData]
        public void Processor_ContinuesPipeline_WhenQueryStringParameterIsAGuid(string key1, string value1)
        {
            NameValueCollection nvc = new NameValueCollection();
            nvc.Add(key1, value1);
            nvc.Add(LayoutSimulator.Configuration.Settings.QueryStringKey, Guid.NewGuid().ToString());
            var pipelineArgs = new BuildSimulatedLayoutArgs
            {
                OriginalPageDefinition = new Mvc.Presentation.PageDefinition(),
                ApplySimulatedDefinition = false,
                QueryStringParameters = nvc
            };
            var proc = new ValidateData();

            proc.Process(pipelineArgs);

            pipelineArgs.Aborted.Should().BeFalse();
            pipelineArgs.ApplySimulatedDefinition.Should().BeFalse();
        }
    }
}
