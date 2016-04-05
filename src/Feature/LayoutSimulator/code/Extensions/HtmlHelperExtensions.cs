using Sitecore.Feature.LayoutSimulator.HtmlHelpers;
using Sitecore.Mvc.Helpers;

namespace Sitecore.Feature.LayoutSimulator.Extensions
{
    public static class HtmlHelper
    {
        public static CustomSitecoreHelper LayoutSimulator(this System.Web.Mvc.HtmlHelper htmlHelper)
        {
            // get the helper from current thread
            var threadData = ThreadHelper.GetThreadData<CustomSitecoreHelper>();
            if (threadData != null) return threadData;

            // create new helper if needed
            var helper = new CustomSitecoreHelper(htmlHelper);
            ThreadHelper.SetThreadData(helper);
            return helper;
        }
    }
}