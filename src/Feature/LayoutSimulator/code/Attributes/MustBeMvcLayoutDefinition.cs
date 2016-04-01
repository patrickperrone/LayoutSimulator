using System;
using System.ComponentModel.DataAnnotations;
using Sitecore.Layouts;
using Sitecore.Xml.Serialization;
using Sitecore.Data;
using Sitecore.Data.Items;
using System.IO;
using Sitecore.Mvc.Configuration;
using Sitecore.Mvc.Extensions;

namespace Sitecore.Feature.LayoutSimulator.Attributes
{
    public class MustBeMvcLayoutDefinition : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string errorMessage = FormatErrorMessage(validationContext.DisplayName);

            if (value == null)
            {
                return new ValidationResult(errorMessage);
            }

            string xml = System.Convert.ToString(value);
            if (xml.Length != 0)
            {
                try
                {
                    LayoutDefinition definition = XmlSerializable.LoadXml(xml, typeof(LayoutDefinition)) as LayoutDefinition;
                    if (definition.Devices.Count > 0)
                    {
                        DeviceDefinition device = definition.Devices[0] as DeviceDefinition;
                        ID devid = new ID(device.Layout);

                        if (!IsMvcLayout(devid, Context.Database))
                        {
                            errorMessage = $"Unable to identify an MVC Layout {device.Layout}.";
                            return new ValidationResult(errorMessage);
                        }
                    }
                    else
                    {
                        errorMessage = "LayoutDefinition did not contain a valid DeviceDefintion.";
                        return new ValidationResult(errorMessage);
                    }

                    return ValidationResult.Success;
                }
                catch (Exception exc)
                {
                    errorMessage = $"{validationContext.DisplayName} could not be deserialized into a LayoutDefinition. {exc.Message}";
                    return new ValidationResult(errorMessage);
                }
            }

            return new ValidationResult(errorMessage);
        }

        private bool IsMvcLayout(ID itemId, Database database)
        {
            Item item = database.GetItem(itemId);
            if (item == null)
            {
                return false;
            }

            LayoutItem layoutItem = new LayoutItem(item);            
            if (layoutItem == null || layoutItem.FilePath.Length == 0)
            {
                return false;
            }

            string filePath = layoutItem.FilePath;
            if (filePath.IsWhiteSpaceOrNull())
            {
                return false;
            }

            if (filePath.IndexOf("~/xaml/", StringComparison.InvariantCulture) >= 0)
            {
                // Layout is a XamlControl
                return false;
            }

            string extension;
            try
            {
                extension = Path.GetExtension(filePath);
            }
            catch
            {
                extension = string.Empty;
            }

            return MvcSettings.IsViewExtension(extension) || !filePath.IsAbsoluteViewPath();
        }
    }
}