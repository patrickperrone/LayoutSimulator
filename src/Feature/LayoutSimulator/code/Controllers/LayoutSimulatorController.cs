using System;
using System.Net;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Http;
using Sitecore.Feature.LayoutSimulator.Data;
using Sitecore.Feature.LayoutSimulator.Models;
using Sitecore.Services.Infrastructure.Web.Http;
using Sitecore.Feature.LayoutSimulator.Extensions;
using Sitecore.Services.Core;
using Sitecore.Feature.LayoutSimulator.Configuration;

namespace Sitecore.Feature.LayoutSimulator.Controllers
{
	[ServicesController(Settings.ServiceUniqueName)]
	public class LayoutSimulatorController : ServicesApiController
	{
		[HttpPost]
		public IHttpActionResult Simulate([FromBody] SimulateRequest request)
		{
			try
			{
				SimulateResult result;
				Diagnostics.Log.Info("Received request to simulate layout.".ToStringForLogging(), this);

				if (request == null)
				{
					result = new SimulateResult
					{
						Success = false,
						Message = "SimulateRequest cannot be null. Ensure that JSON is valid and non-empty."
					};
				}
				else
				{
					var validationResults = new List<ValidationResult>();
					var context = new ValidationContext(request, null, null);
					bool isValid = Validator.TryValidateObject(request, context, validationResults, true);

					if (isValid)
					{
						SimulationManager sm = new SimulationManager(request.HostPageUrl, request.LayoutToSimulate);
						result = sm.RunSimulation();
					}
					else
					{
						result = new SimulateResult
						{
							Success = false,
							Message = string.Join(" | ", validationResults.Select(v => v.ErrorMessage))
						};
					}
				}

				Diagnostics.Log.Info(result.Message.ToStringForLogging(), this);
				return new SimulateActionResult(result, Request, HttpStatusCode.OK);
			}
			catch (Exception exc)
			{
				string message = $"{exc.Message}\r\n{exc.StackTrace}";
				SimulateResult result = new SimulateResult
				{
					Success = false,
					Message = message
				};
				Diagnostics.Log.Error(message.ToStringForLogging(), this);
				return new SimulateActionResult(result, Request, HttpStatusCode.OK);
			}
		}

		[HttpGet]
		public IHttpActionResult About()
		{
			AboutResult result = new AboutResult
			{
				Success = true,
				Message = string.Format("Layout Simulator is installed.")
			};

			return new AboutActionResult(result, Request, HttpStatusCode.OK);
		}
	}
}