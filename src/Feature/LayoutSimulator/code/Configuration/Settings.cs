using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Feature.LayoutSimulator.Extensions;
using System;

namespace Sitecore.Feature.LayoutSimulator.Configuration
{
	public class Settings
	{
		public const string ServiceUniqueName = "LayoutSimulator/Simulator";

		public static string ServiceRoutePrefix
		{
			get
			{
				return $"/sitecore/api/ssc/{ServiceUniqueName}";
			}
		}

		public static string QueryStringKey
		{
			get
			{
				return Sitecore.Configuration.Settings.GetSetting("Sitecore.Feature.LayoutSimulator.QueryStringParameterKey.SimulatorLayoutId", "simulatorid");
			}
		}

		public static long CacheSize
		{
			get
			{
				return StringUtil.ParseSizeString(Sitecore.Configuration.Settings.GetSetting("Sitecore.Feature.LayoutSimulator.LayoutDefinitionCache.MaxSize", "256KB"));
			}
		}

		public static string CacheKeyPrefix
		{
			get
			{
				return "LayoutSimulator_";
			}
		}

		public static string LogMessagePrefix
		{
			get
			{
				return "[Sitecore.Features.LayoutSimulator]:";
			}
		}

		public static Item GetFeatureSettingsItem(Sitecore.Data.Database database)
		{
			Assert.ArgumentNotNull(database, "database");

			Item settingsItem = database.GetItem(Configuration.Constants.Items.Settings.ID);
			if (settingsItem == null)
			{
				throw new ApplicationException("Could not find Layout Simulator's settings items.");
			}

			if (!settingsItem.IsDerived(Templates.Simulator_Settings.ID))
			{
				throw new ApplicationException($"Item at {settingsItem.Paths.FullPath} is not derived from template {Templates.Simulator_Settings.ID}");
			}

			return settingsItem;
		}

		public static string GetDefaultSimulationHostPageUrl(Sitecore.Data.Database database)
		{
			string pageUrl = string.Empty;
			Item settingsItem = GetFeatureSettingsItem(database);

			if (settingsItem.FieldHasValue(Templates.Simulator_Settings.Fields.Default_Simulation_Host_Page))
			{
				Item hostPage = settingsItem.GetDropLinkSelectedItem(Templates.Simulator_Settings.Fields.Default_Simulation_Host_Page);
				pageUrl = hostPage.GetAbsoluteUrl();
			}
			else
			{
				string fieldName = settingsItem.Fields[Templates.Simulator_Settings.Fields.Default_Simulation_Host_Page].Name;
				throw new ApplicationException($"{fieldName} is undefined on {settingsItem.Name} item at {settingsItem.Paths.FullPath}");
			}

			return pageUrl;
		}

		public static string GetDefaultLayoutBuilderPageUrl(Sitecore.Data.Database database)
		{
			string pageUrl = string.Empty;
			Item settingsItem = GetFeatureSettingsItem(database);

			if (settingsItem.FieldHasValue(Templates.Simulator_Settings.Fields.Default_Layout_Builder_Page))
			{
				pageUrl = settingsItem.GetString(Templates.Simulator_Settings.Fields.Default_Layout_Builder_Page);

			}
			else
			{
				string fieldName = settingsItem.Fields[Templates.Simulator_Settings.Fields.Default_Layout_Builder_Page].Name;
				throw new ApplicationException($"{fieldName} is undefined on {settingsItem.Name} item at {settingsItem.Paths.FullPath}");
			}

			return pageUrl;
		}
	}
}