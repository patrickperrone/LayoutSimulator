using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Data.Managers;
using Sitecore.Links;
using Sitecore.Resources.Media;

namespace Sitecore.Feature.LayoutSimulator.Extensions
{
    public static class ItemExtensions
    {
        public static bool FieldHasValue(this Item item, ID fieldID)
        {
            return item.Fields[fieldID] != null && item.Fields[fieldID].HasValue && !string.IsNullOrWhiteSpace(item.Fields[fieldID].Value);
        }

        public static Item GetDropLinkSelectedItem(this Item item, ID fieldId)
        {
            return new InternalLinkField(item.Fields[fieldId]).TargetItem;
        }

        public static string GetString(this Item item, ID fieldID)
        {
            return item.Fields[fieldID].Value;
        }

        public static string GetAbsoluteUrl(this Item item)
        {
            UrlOptions options = new UrlOptions
            {
                AlwaysIncludeServerUrl = true,
                SiteResolving = true,
                LanguageEmbedding = LanguageEmbedding.AsNeeded
            };

            MediaUrlOptions mediaOptions = new MediaUrlOptions
            {
                AlwaysIncludeServerUrl = true
            };

            return !IsMedia(item) ? LinkManager.GetItemUrl(item, options) : MediaManager.GetMediaUrl(item, mediaOptions);
        }

        public static string GetUrl(this Item item)
        {
            return !IsMedia(item) ? LinkManager.GetItemUrl(item) : MediaManager.GetMediaUrl(item);
        }

        public static bool IsMedia(this Item item)
        {
            return item.Paths.IsMediaItem;
        }

        public static bool IsDerived(this Item item, ID templateId)
        {
            if (item == null)
            {
                return false;
            }

            return !templateId.IsNull && item.IsDerived(item.Database.Templates[templateId]);
        }

        private static bool IsDerived(this Item item, Item templateItem)
        {
            if (item == null)
            {
                return false;
            }

            if (templateItem == null)
            {
                return false;
            }

            var itemTemplate = TemplateManager.GetTemplate(item);
            return itemTemplate != null && (itemTemplate.ID == templateItem.ID || itemTemplate.DescendsFrom(templateItem.ID));
        }
    }
}