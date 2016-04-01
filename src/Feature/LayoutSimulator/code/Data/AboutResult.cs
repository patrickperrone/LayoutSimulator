using System.Runtime.Serialization;

namespace Sitecore.Feature.LayoutSimulator.Data
{
    [DataContract]
    public class AboutResult
    {
        [DataMember]
        public bool Success { get; set; }
        [DataMember]
        public string Message { get; set; }
    }
}