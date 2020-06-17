using System.Xml.Serialization;

namespace CrossCutting.SerializationModels
{
    public class PIS
    {
        [XmlElement("PISAliq")]
        public PISAliq PISAliq { get; set; }
    }
}
