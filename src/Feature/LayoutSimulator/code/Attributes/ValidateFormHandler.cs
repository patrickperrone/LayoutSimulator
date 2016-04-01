using System.Reflection;
using System.Web.Mvc;

namespace Sitecore.Feature.LayoutSimulator.Attributes

{
    /// <summary>
    /// Hat tip to Kevin Brechbühl: https://ctor.io/posting-forms-in-sitecore-controller-renderings-another-perspective/
    /// </summary>
    public class ValidateFormHandler : ActionMethodSelectorAttribute
    {
        public override bool IsValidForRequest(ControllerContext controllerContext, MethodInfo methodInfo)
        {
            var controller = controllerContext.HttpContext.Request.Form["hfController"];
            var action = controllerContext.HttpContext.Request.Form["hfAction"];

            return !string.IsNullOrWhiteSpace(controller)
                    && !string.IsNullOrWhiteSpace(action)
                    && controller == controllerContext.Controller.GetType().Name
                    && action == methodInfo.Name;
        }
    }
}