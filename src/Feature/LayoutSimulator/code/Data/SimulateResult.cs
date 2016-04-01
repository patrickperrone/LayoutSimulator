using System.Runtime.Serialization;

namespace Sitecore.Feature.LayoutSimulator.Data
{
    [DataContract]
    public class SimulateResult
    {
        [DataMember]
        public bool Success { get; set; }
        [DataMember]
        public string Message { get; set; }
        [DataMember]
        public string SimulatedHtml { get; set; }
    }
}