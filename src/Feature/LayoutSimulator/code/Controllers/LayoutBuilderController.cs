using System.Globalization;
using System.Net;
using System.Web.Mvc;
using Sitecore.Feature.LayoutSimulator.Models;
using Sitecore.Feature.LayoutSimulator.Attributes;

namespace Sitecore.Feature.LayoutSimulator.Controllers
{
    //TODO: add a host page url field to form
    public class LayoutBuilderController : Controller
    {
        string ItemLayoutFromSession
        {
            get
            {
                return (string)Session[Configuration.Constants.CopyItemLayoutSessionKey] ?? string.Empty;
            }
            set
            {
                Session[Configuration.Constants.CopyItemLayoutSessionKey] = value;
            }
        }

        [HttpGet]
        public ActionResult Index()
        {
            FormModel model = new FormModel
            {
                EncodedLayoutXml = ItemLayoutFromSession,
                HostPageUrl = Configuration.Settings.GetDefaultSimulationHostPageUrl(Context.Database)
            };

            ItemLayoutFromSession = string.Empty;

            return View("~/Views/LayoutSimulator/SubmitLayoutXml.cshtml", model);
        }

        [HttpPost]
        [ValidateFormHandler]
        public ActionResult Index(FormModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.SetModelValue("EncodedLayoutXml", new ValueProviderResult(WebUtility.HtmlDecode(model.EncodedLayoutXml), WebUtility.HtmlDecode(model.EncodedLayoutXml), CultureInfo.InvariantCulture));
                return View("~/Views/LayoutSimulator/SubmitLayoutXml.cshtml", model);
            }

            return View("~/Views/LayoutSimulator/PostToService.cshtml", model);
        }
    }
}