using System.Net;
using System.ComponentModel.DataAnnotations;
using Sitecore.Feature.LayoutSimulator.Attributes;
using Sitecore.Feature.LayoutSimulator.Configuration;

namespace Sitecore.Feature.LayoutSimulator.Models
{
    /// <summary>
    /// The model provides two properties that expose different versions of the same data, the layout xml. The
    /// reason for this is to avoid resolving request validation errors through setting <httpRuntime ... requestValidationMode="2.0"/>
    /// in the web.config. This appears to be a prerequisite in order to make use of the [AllowHtml] data annotation.
    /// https://kb.sitecore.net/articles/031258
    /// https://msdn.microsoft.com/en-us/library/hh882339(v=vs.110).aspx
    /// </summary>
    public class FormModel
    {
        public string ServiceRoute
        {
            get
            {
                return $"{Settings.ServiceRoutePrefix}/1/simulate";
            }
        }

        public string HostPageUrl { get; set; }

        [Required]
        public string EncodedLayoutXml { get; set; }

        [Required]
        [MustBeMvcLayoutDefinition]
        public string LayoutXml
        {
            get
            {
                if (string.IsNullOrEmpty(EncodedLayoutXml))
                {
                    return EncodedLayoutXml;
                }
                string decoded = HtmlDecodeCharacter(EncodedLayoutXml, '<');
                return HtmlDecodeCharacter(decoded, '>');
            }
        }

        private string HtmlDecodeCharacter(string encodedText, char c)
        {
            string encodedChar = WebUtility.HtmlEncode(c.ToString());
            return encodedText.Replace(encodedChar, c.ToString());
        }
    }
}