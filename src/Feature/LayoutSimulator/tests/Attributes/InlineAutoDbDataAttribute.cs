using Ploeh.AutoFixture.Xunit2;

namespace Sitecore.Feature.LayoutSimulator.Tests.Attributes
{
    internal class InlineAutoDbDataAttribute : InlineAutoDataAttribute
    {
        public InlineAutoDbDataAttribute(params object[] values)
          : base(new AutoDbDataAttribute(), values)
        {
        }
    }
}
