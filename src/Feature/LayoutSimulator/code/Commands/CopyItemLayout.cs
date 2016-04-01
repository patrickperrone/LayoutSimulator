using System.Web;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Shell.Framework.Commands;

namespace Sitecore.Feature.LayoutSimulator.Commands
{
    public class CopyItemLayout : Command
    {
        public override void Execute(CommandContext context)
        {
            Assert.ArgumentNotNull(context, "context");

            if (context.Items.Length == 1 && context.Items[0] != null)
            {
                Sitecore.Data.Fields.Field innerField = new Sitecore.Data.Fields.LayoutField(context.Items[0]).InnerField;
                HttpContext.Current.Session[Configuration.Constants.CopyItemLayoutSessionKey] = Sitecore.Data.Fields.LayoutField.GetFieldValue(innerField);

                string pageUrl = Configuration.Settings.GetDefaultLayoutBuilderPageUrl(context.Items[0].Database);
                if (string.IsNullOrWhiteSpace(pageUrl))
                {
                    Item settingsItem = Configuration.Settings.GetFeatureSettingsItem(context.Items[0].Database);
                    Context.ClientPage.ClientResponse.Alert($"Check the value of Layout Builder page on the {settingsItem.Name} item at {settingsItem.Paths.FullPath}");
                }
                else
                {
                    Context.ClientPage.ClientResponse.Eval($"window.open('{pageUrl}','_blank')");
                }
            }
        }
    }
}