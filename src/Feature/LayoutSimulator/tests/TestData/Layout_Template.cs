using Sitecore.Data;
using Sitecore.FakeDb;

namespace Sitecore.Feature.LayoutSimulator.Tests.TestData
{
    public class Layout_Template : DbTemplate
    {
        private static ID TemplateId { get; } = new ID("{3A45A723-64EE-4919-9D41-02FD40FD1466}");
        public string PathFieldName { get; } = "Path";
        public ID PathFieldId { get; } = new ID("{A036B2BC-BA04-44F6-A75F-BAE6CD242ABF}");

        public Layout_Template()
            : base("Layout", TemplateId)
        {
            Add(new DbField(PathFieldName, PathFieldId));
        }
    }
}
