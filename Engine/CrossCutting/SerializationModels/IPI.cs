using System.Xml.Serialization;

namespace CrossCutting.SerializationModels
{
    public class IPI
    {
        public int cEnq { get; set; }

        [XmlElement("IPINT")]
        public IPINT IPINT { get; set; }
    }
}
