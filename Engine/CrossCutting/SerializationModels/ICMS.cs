using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace CrossCutting.SerializationModels
{
    public class ICMS
    {
        [XmlElement("ICMS00")]
        public ICMS00 ICMS00 { get; set; }
    }
}
