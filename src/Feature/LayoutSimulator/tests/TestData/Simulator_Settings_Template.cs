using Sitecore.Data;
using Sitecore.FakeDb;

namespace Sitecore.Feature.LayoutSimulator.Tests.TestData
{
    public class Simulator_Settings_Template : DbTemplate
    {
        public string DefaultSimulationHostPageFieldName { get; } = "Default Simulation Host Page";
        public ID DefaultSimulationHostPageFieldId { get; } = Templates.Simulator_Settings.Fields.Default_Simulation_Host_Page;
        public string DefaultLayoutBuilderPageFieldName { get; } = "Default Layout Builder Page";
        public ID DefaultLayoutBuilderPageFieldId { get; } = Templates.Simulator_Settings.Fields.Default_Layout_Builder_Page;

        public Simulator_Settings_Template()
            : base("Simulator Settings", Templates.Simulator_Settings.ID)
        {
            Add(new DbField(DefaultSimulationHostPageFieldName, DefaultSimulationHostPageFieldId));
            Add(new DbField(DefaultLayoutBuilderPageFieldName, DefaultLayoutBuilderPageFieldId));
        }
    }
}
