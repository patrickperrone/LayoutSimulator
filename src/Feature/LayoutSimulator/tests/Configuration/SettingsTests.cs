using FluentAssertions;
using NSubstitute;
using Ploeh.AutoFixture.AutoNSubstitute;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.FakeDb;
using Sitecore.FakeDb.AutoFixture;
using Sitecore.Feature.LayoutSimulator.Tests.Attributes;
using Sitecore.Feature.LayoutSimulator.Tests.TestData;
using Sitecore.Links;
using System;
using Xunit;

namespace Sitecore.Feature.LayoutSimulator.Tests.Configuration
{
    public class SettingsTests
    {
        [Theory, AutoDbData]
        public void GetFeatureSettingsItem_ReturnsItem(Db db, [Content] Simulator_Settings_Template template)
        {
            var settingsID = LayoutSimulator.Configuration.Constants.Items.Settings.ID;
            db.Add(new DbItem("settingsitem", settingsID, template.ID));
            Item settingsItem = db.GetItem(settingsID);

            LayoutSimulator.Configuration.Settings.GetFeatureSettingsItem(settingsItem.Database).Should().NotBeNull();
        }

        [Theory, AutoDbData]
        public void GetFeatureSettingsItem_ThrowsException_WhenTemplateIdIsInvalid(Db db, [Content] Simulator_Settings_Template template)
        {
            var settingsID = LayoutSimulator.Configuration.Constants.Items.Settings.ID;
            var wrongTemplateId = ID.NewID;
            db.Add(new DbItem("settingsitem", settingsID, wrongTemplateId));
            Item settingsItem = db.GetItem(settingsID);

            try
            {
                LayoutSimulator.Configuration.Settings.GetFeatureSettingsItem(settingsItem.Database);
            }
            catch (Exception ex)
            {
                ex.Should().BeOfType<ApplicationException>().Which.Message.Should().Contain("is not derived from template");
            }
        }

        [Theory, AutoDbData]
        public void GetFeatureSettingsItem_ThrowsException_WhenSettingsItemHasWrongId(Db db, [Content] Simulator_Settings_Template template)
        {
            var wrongId = ID.NewID;
            db.Add(new DbItem("settingsitem", wrongId, template.ID));
            Item settingsItem = db.GetItem(wrongId);

            try
            {
                LayoutSimulator.Configuration.Settings.GetFeatureSettingsItem(settingsItem.Database);
            }
            catch (Exception ex)
            {
                ex.Should().BeOfType<ApplicationException>().Which.Message.Should().Be("Could not find Layout Simulator's settings items.");
            }
        }

        [Theory, AutoDbData]
        public void GetDefaultSimulationHostPageUrl_ThrowsException_WhenFieldHasNoValue(Db db, [Content] Simulator_Settings_Template template)
        {
            var settingsID = LayoutSimulator.Configuration.Constants.Items.Settings.ID;
            db.Add(new DbItem("settingsitem", settingsID, template.ID));
            Item settingsItem = db.GetItem(settingsID);

            try
            {
                string url = LayoutSimulator.Configuration.Settings.GetDefaultSimulationHostPageUrl(settingsItem.Database);
            }
            catch (Exception ex)
            {
                ex.Should().BeOfType<ApplicationException>().Which.Message.Should().Contain("is undefined");
            }
        }

        [Theory, AutoDbData]
        public void GetDefaultSimulationHostPageUrl_ReturnsHostPageUrl(Db db, [Content] Item hostPage, 
            [Content] Simulator_Settings_Template template,
            [Substitute] LinkProvider provider, string expectedUrl)
        {
            var settingsID = LayoutSimulator.Configuration.Constants.Items.Settings.ID;
            db.Add(new DbItem("settingsitem", settingsID, template.ID)
            {
                {template.DefaultSimulationHostPageFieldName, hostPage.ID.ToString()}
            });
            Item settingsItem = db.GetItem(settingsID);

            provider = Substitute.For<Sitecore.Links.LinkProvider>();
            provider
                .GetItemUrl(hostPage, Arg.Is<Sitecore.Links.UrlOptions>(x => x.AlwaysIncludeServerUrl))
                .Returns(expectedUrl);

            using (new FakeDb.Links.LinkProviderSwitcher(provider))
            {
                LayoutSimulator.Configuration.Settings.GetDefaultSimulationHostPageUrl(settingsItem.Database).Should().Be(expectedUrl);
            }
        }

        [Theory, AutoDbData]
        public void GetDefaultLayoutBuilderPageUrl_ThrowsException_WhenFieldHasNoValue(Db db, [Content] Simulator_Settings_Template template)
        {
            var settingsID = LayoutSimulator.Configuration.Constants.Items.Settings.ID;
            db.Add(new DbItem("settingsitem", settingsID, template.ID));
            Item settingsItem = db.GetItem(settingsID);

            try
            {
                string url = LayoutSimulator.Configuration.Settings.GetDefaultLayoutBuilderPageUrl(settingsItem.Database);
            }
            catch (Exception ex)
            {
                ex.Should().BeOfType<ApplicationException>().Which.Message.Should().Contain("is undefined");
            }
        }

        [Theory, AutoDbData]
        public void GetDefaultLayoutBuilderPageUrl_ReturnsExpectedUrl(Db db, [Content] Item builderPage, [Content] Simulator_Settings_Template template)
        {
            var settingsID = LayoutSimulator.Configuration.Constants.Items.Settings.ID;
            db.Add(new DbItem("settingsitem", settingsID, template.ID)
            {
                {template.DefaultLayoutBuilderPageFieldName, builderPage.ID.ToString()}
            });
            Item settingsItem = db.GetItem(settingsID);

            string expectedUrl = builderPage.ID.ToString();
            LayoutSimulator.Configuration.Settings.GetDefaultLayoutBuilderPageUrl(settingsItem.Database).Should().Be(expectedUrl);
        }
    }
}
