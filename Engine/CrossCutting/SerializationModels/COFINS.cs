using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace CrossCutting.SerializationModels
{
    public class COFINS
    {
        [XmlElement("COFINSAliq")]
        public COFINSAliq COFINSAliq { get; set; }
    }
}
