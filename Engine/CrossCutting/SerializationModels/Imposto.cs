using System.Xml.Serialization;

namespace CrossCutting.SerializationModels
{
    public class Imposto
    {
        [XmlElement("ICMS")]
        public ICMS ICMS { get; set; }

        [XmlElement("IPI")]
        public IPI IPI { get; set; }

        [XmlElement("PIS")]
        public PIS PIS { get; set; }

        [XmlElement("COFINS")]
        public COFINS COFINS { get; set; }
    }
}
