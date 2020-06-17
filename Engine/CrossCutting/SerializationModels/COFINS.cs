using System.Xml.Serialization;

namespace CrossCutting.SerializationModels
{
    public class COFINS
    {
        [XmlElement("COFINSAliq")]
        public COFINSAliq COFINSAliq { get; set; }
    }
}
