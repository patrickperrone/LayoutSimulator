using FluentAssertions;
using Xunit;
using Sitecore.Feature.LayoutSimulator.Models;
using Sitecore.Feature.LayoutSimulator.Tests.Attributes;
using Sitecore.FakeDb;
using Sitecore.Data;
using Sitecore.Feature.LayoutSimulator.Tests.TestData;
using Sitecore.FakeDb.AutoFixture;
using Sitecore.Data.Items;

namespace Sitecore.Feature.LayoutSimulator.Tests.Models
{
    public class SimulateRequestTests : ModelTests
    {
        [Theory, AutoDbData]
        public void Model_IsInvalid_WhenLayoutItemIsNull(Db db)
        {
            string text = @"
<r><d id=""{FE5D7FDF-89C0-4D99-9AA3-B5FBD009C9F3}"" l=""{ED043C36-CD48-4EDF-A288-EC404EE1CD42}"">
</d></r>";
            SimulateRequest sut = new SimulateRequest
            {
                LayoutToSimulate = text
            };
            IsModelValid(sut).Should().BeFalse();
        }

        [Theory, AutoDbData]
        public void Model_IsValid_WhenLayoutItemIsMvc(Db db, [Content] Layout_Template template)
        {
            ID layoutItemId = new ID("{ED043C36-CD48-4EDF-A288-EC404EE1CD42}");
            db.Add(new DbItem("layout item", layoutItemId, template.ID)
            {
                {template.PathFieldName, "/Views/LayoutSimulator/BlankLayout.cshtml"}
            });
            Item layoutItem = db.GetItem(layoutItemId);
            string text = @"
<r><d id=""{FE5D7FDF-89C0-4D99-9AA3-B5FBD009C9F3}"" l=""{ED043C36-CD48-4EDF-A288-EC404EE1CD42}"">
</d></r>";
            SimulateRequest sut = new SimulateRequest
            {
                LayoutToSimulate = text
            };

            IsModelValid(sut).Should().BeTrue();
        }

        [Theory, AutoDbData]
        public void Model_IsInvalid_WhenLayoutHasNoPathFieldValue(Db db, [Content] Layout_Template template)
        {
            ID layoutItemId = new ID("{ED043C36-CD48-4EDF-A288-EC404EE1CD42}");
            db.Add(new DbItem("layout item", layoutItemId, template.ID)
            {
                {template.PathFieldName, ""}
            });
            Item layoutItem = db.GetItem(layoutItemId);
            string text = @"
<r><d id=""{FE5D7FDF-89C0-4D99-9AA3-B5FBD009C9F3}"" l=""{ED043C36-CD48-4EDF-A288-EC404EE1CD42}"">
</d></r>";
            SimulateRequest sut = new SimulateRequest
            {
                LayoutToSimulate = text
            };

            IsModelValid(sut).Should().BeFalse();
        }

        [Theory, AutoDbData]
        public void Model_IsInvalid_WhenPathFieldIsAspx(Db db, [Content] Layout_Template template, string filePath)
        {
            ID layoutItemId = new ID("{ED043C36-CD48-4EDF-A288-EC404EE1CD42}");
            db.Add(new DbItem("layout item", layoutItemId, template.ID)
            {
                {template.PathFieldName, "/some.aspx"}
            });
            Item layoutItem = db.GetItem(layoutItemId);
            string text = @"
<r><d id=""{FE5D7FDF-89C0-4D99-9AA3-B5FBD009C9F3}"" l=""{ED043C36-CD48-4EDF-A288-EC404EE1CD42}"">
</d></r>";
            SimulateRequest sut = new SimulateRequest
            {
                LayoutToSimulate = text
            };

            IsModelValid(sut).Should().BeFalse();
        }

        [Theory, AutoDbData]
        public void Model_IsInvalid_WhenPathFieldIsXaml(Db db, [Content] Layout_Template template, string filePath)
        {
            ID layoutItemId = new ID("{ED043C36-CD48-4EDF-A288-EC404EE1CD42}");
            db.Add(new DbItem("layout item", layoutItemId, template.ID)
            {
                {template.PathFieldName, "~/xaml/something"}
            });
            Item layoutItem = db.GetItem(layoutItemId);
            string text = @"
<r><d id=""{FE5D7FDF-89C0-4D99-9AA3-B5FBD009C9F3}"" l=""{ED043C36-CD48-4EDF-A288-EC404EE1CD42}"">
</d></r>";
            SimulateRequest sut = new SimulateRequest
            {
                LayoutToSimulate = text
            };

            IsModelValid(sut).Should().BeFalse();
        }

        [Theory, AutoDbData]
        public void Model_IsValid(Db db, [Content] Layout_Template template, string filePath)
        {
            ID layoutItemId = new ID("{ED043C36-CD48-4EDF-A288-EC404EE1CD42}");
            db.Add(new DbItem("layout item", layoutItemId, template.ID)
            {
                {template.PathFieldName, "/view.cshtml"}
            });
            Item layoutItem = db.GetItem(layoutItemId);
            string text = @"
<r><d id=""{FE5D7FDF-89C0-4D99-9AA3-B5FBD009C9F3}"" l=""{ED043C36-CD48-4EDF-A288-EC404EE1CD42}"">
<r id=""{1149A3C7-C69A-4E6B-A6C3-32687EC2F8D4}"" par=""UseStaticPlaceholderNames&amp;Type=bg-secondary&amp;Media&amp;Parallax"" ph=""page-layout"" uid=""{F4FE27AF-B867-4E72-8808-843D5781A355}"" />
</d></r>";
            SimulateRequest sut = new SimulateRequest
            {
                LayoutToSimulate = text
            };

            IsModelValid(sut).Should().BeTrue();
        }
    }
}
