using Sitecore.Caching;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Linq;

namespace Sitecore.Feature.LayoutSimulator.Data
{
    [Serializable()]
    public class SimulatedLayout : ICacheable
    {
        public string LayoutDefinition { get; set; }

        public string HostPageUrl { get; set; }

        public bool Cacheable { get; set; }

        public bool Immutable
        {
            get { return true; }
        }

        public event DataLengthChangedDelegate DataLengthChanged;

        public long GetDataLength()
        {

            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, this);
                return ms.ToArray().Length;
            }
        }
    }
}