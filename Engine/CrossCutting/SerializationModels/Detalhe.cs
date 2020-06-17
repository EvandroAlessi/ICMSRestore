using System.Xml.Serialization;

namespace CrossCutting.SerializationModels
{
    public class Detalhe
    {
        [XmlAttribute("nItem")]
        public int nItem { get; set; }

        [XmlElement("prod")]
        public Produto Produto { get; set; }

        [XmlElement("imposto")]
        public Imposto Imposto { get; set; }
    }
}
