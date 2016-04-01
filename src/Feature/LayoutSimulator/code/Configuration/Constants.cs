using Sitecore.Data;

namespace Sitecore.Feature.LayoutSimulator.Configuration
{
    public struct Constants
    {
        public static readonly string LayoutSimulateMvcPipelineName = "LayoutSimulator.mvc.buildSimulatedLayout";

        public static readonly string CopyItemLayoutSessionKey = "Command_CopyItemLayout_LayoutXml";

        public static readonly string SimulatedLayoutObjectSessionKey = "SimulatedLayoutObject_SessionKey";

        public struct Items
        {
            public struct Settings
            {
                public static readonly ID ID = new ID("{91CC8A3D-2515-4333-9208-76DDDD34B232}");
            }
        }
    }
}