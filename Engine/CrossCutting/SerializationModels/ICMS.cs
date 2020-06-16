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

        [XmlElement("ICMSSN101")]
        public ICMSSN101 ICMSSN101 { get; set; }

        [XmlElement("ICMSSN102")]
        public ICMSSN102 ICMSSN102 { get; set; }

        [XmlElement("ICMSSN103")]
        public ICMSSN103 ICMSSN103 { get; set; }

        [XmlElement("ICMSSN201")]
        public ICMSSN201 ICMSSN201 { get; set; }

        [XmlElement("ICMSSN202")]
        public ICMSSN202 ICMSSN202 { get; set; }

        [XmlElement("ICMSSN203")]
        public ICMSSN203 ICMSSN203 { get; set; }

        [XmlElement("ICMSSN300")]
        public ICMSSN300 ICMSSN300 { get; set; }

        [XmlElement("ICMSSN400")]
        public ICMSSN400 ICMSSN400 { get; set; }

        [XmlElement("ICMSSN500")]
        public ICMSSN500 ICMSSN500 { get; set; }

        [XmlElement("ICMSSN900")]
        public ICMSSN900 ICMSSN900 { get; set; }
    }
}
