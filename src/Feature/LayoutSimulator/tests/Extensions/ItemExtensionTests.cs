using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.FakeDb;
using Sitecore.FakeDb.AutoFixture;
using Sitecore.Feature.LayoutSimulator.Tests.Attributes;
using Xunit;
using Sitecore.Feature.LayoutSimulator.Extensions;
using FluentAssertions;
using Ploeh.AutoFixture.AutoNSubstitute;
using Sitecore.Links;
using NSubstitute;
using Sitecore.Feature.LayoutSimulator.Tests.TestData;

namespace Sitecore.Feature.LayoutSimulator.Tests.Extensions
{
    public class ItemExtensionTests
    {
        [Theory, AutoDbData]
        public void IsDerived_ReturnsFalse(Db db, [Content] Simulator_Settings_Template template)
        {
            var newID = LayoutSimulator.Configuration.Constants.Items.Settings.ID;
            var wrongTemplateId = ID.NewID;
            db.Add(new DbItem("settingsitem", newID, wrongTemplateId));
            Item settingsItem = db.GetItem(newID);

            settingsItem.IsDerived(template.ID).Should().BeFalse();
        }

        [Theory, AutoDbData]
        public void IsDerived_ReturnsTrue(Db db, [Content] Simulator_Settings_Template template)
        {
            var newID = LayoutSimulator.Configuration.Constants.Items.Settings.ID;
            db.Add(new DbItem("settingsitem", newID, template.ID));
            Item settingsItem = db.GetItem(newID);

            settingsItem.IsDerived(template.ID).Should().BeTrue();
        }

        [Theory, AutoDbData]
        public void GetAbsoluteUrl_ReturnsExpectedUrl(Db db, [Content] Item item,
            [Content] Simulator_Settings_Template template,
            [Substitute] LinkProvider provider, string expectedUrl)
        {
            provider = Substitute.For<Sitecore.Links.LinkProvider>();
            provider
                .GetItemUrl(item, Arg.Is<Sitecore.Links.UrlOptions>(x => x.AlwaysIncludeServerUrl))
                .Returns(expectedUrl);

            using (new FakeDb.Links.LinkProviderSwitcher(provider))
            {
                item.GetAbsoluteUrl().Should().Be(expectedUrl);
            }
        }
    }
}
