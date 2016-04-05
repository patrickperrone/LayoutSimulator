using Sitecore.Mvc.Helpers;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Sitecore.Feature.LayoutSimulator.HtmlHelpers
{
    public class CustomSitecoreHelper : SitecoreHelper
    {
        public CustomSitecoreHelper(System.Web.Mvc.HtmlHelper htmlHelper)
            : base(htmlHelper)
        {
        }
    
        public override HtmlString FormHandler()
        {
            string controller = this.GetValueFromCurrentRendering("Controller");
            string action = this.GetValueFromCurrentRendering("Controller Action");

            if (string.IsNullOrEmpty(controller))
            {
                return new HtmlString(string.Empty);
            }
            else
            {
                controller = controller.Split(',')[0];
            }

            string str = HtmlHelper.Hidden("hfController", controller).ToString();
            if (!string.IsNullOrEmpty(action))
            {
                str += HtmlHelper.Hidden("hfAction", action).ToString();
            }

            return new HtmlString(str);
        }
    }
}