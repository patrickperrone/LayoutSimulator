using FluentAssertions;
using Xunit;
using Sitecore.Feature.LayoutSimulator.Models;
using Sitecore.Feature.LayoutSimulator.Tests.Attributes;
using Sitecore.FakeDb;
using Sitecore.FakeDb.AutoFixture;
using Sitecore.Feature.LayoutSimulator.Tests.TestData;
using Sitecore.Data;
using Sitecore.Data.Items;

namespace Sitecore.Feature.LayoutSimulator.Tests.Models
{
    public class FormModelTests : ModelTests
    {
        [Fact]
        public void LayoutXml_IsEmpty()
        {
            FormModel sut = new FormModel();
            sut.EncodedLayoutXml = string.Empty;
            sut.LayoutXml.Should().Be(string.Empty);
        }

        [Fact]
        public void LayoutXml_HasMultipleDecodedCharacters()
        {
            FormModel sut = new FormModel();
            sut.EncodedLayoutXml = "&lt; &gt; &lt;&lt;&gt;&gt;";
            sut.LayoutXml.Should().Be("< > <<>>");
        }

        [Fact]
        public void LayoutXml_HasEncodedAmpersandCharacter()
        {
            FormModel sut = new FormModel();
            sut.EncodedLayoutXml = "&lt; par=\"key1=value1&amp;key2=value2\"&gt;";
            sut.LayoutXml.Should().Be("< par=\"key1=value1&amp;key2=value2\">");
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
            FormModel sut = new FormModel
            {
                EncodedLayoutXml = text
            };

            IsModelValid(sut).Should().BeTrue();
        }
    }
}
