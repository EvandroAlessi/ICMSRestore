using System.Xml.Serialization;

namespace CrossCutting.SerializationModels
{
    public class Total
    {
        [XmlElement("ICMSTot")]
        public ICMSTot ICMSTot { get; set; }
    }
}