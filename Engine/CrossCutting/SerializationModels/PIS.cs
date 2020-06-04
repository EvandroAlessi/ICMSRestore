using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace CrossCutting.SerializationModels
{
    public class PIS
    {
        [XmlElement("PISAliq")]
        public PISAliq PISAliq { get; set; }
    }
}
