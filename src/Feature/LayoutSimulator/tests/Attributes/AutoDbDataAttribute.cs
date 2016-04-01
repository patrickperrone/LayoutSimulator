using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoNSubstitute;
using Ploeh.AutoFixture.Xunit2;
using Sitecore.FakeDb.AutoFixture;

namespace Sitecore.Feature.LayoutSimulator.Tests.Attributes
{
    internal class AutoDbDataAttribute : AutoDataAttribute
    {
        public AutoDbDataAttribute()
              : base(new Fixture()
                .Customize(new AutoNSubstituteCustomization())
                .Customize(new AutoDbCustomization()))
        {
        }
    }
}
