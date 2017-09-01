using Sitecore.Feature.LayoutSimulator.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Sitecore.Feature.LayoutSimulator.Models
{
	public class SimulateRequest
	{
		[Required]
		[MustBeMvcLayoutDefinition]
		public string LayoutToSimulate { get; set; }

		public string HostPageUrl { get; set; }
	}
}