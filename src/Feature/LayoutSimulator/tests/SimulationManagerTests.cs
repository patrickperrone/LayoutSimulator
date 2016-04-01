using FluentAssertions;
using System;
using Xunit;

namespace Sitecore.Feature.LayoutSimulator.Tests
{
    public class SimulationManagerTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void SimulationManagerInstantiation_ThrowsExceptionWhenLayoutToSimulateParameterIs(string layoutToSimulate)
        {
            try
            {
                new SimulationManager("http://foo/bar", layoutToSimulate);
            }
            catch (Exception ex)
            {
                ex.Should().BeOfType<ArgumentException>();
            }
        }
    }
}
