using System.Collections.Generic;
using System.Xml.Serialization;

namespace CrossCutting.SerializationModels
{
    [XmlRoot("NFe", Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public class NFe
    {

        [XmlElement(ElementName = "infNFe")]
        public InfNFe InformacoesNFe { get; set; }

        public class InfNFe
        {
            [XmlIgnore]
            private string _chave;

            [XmlAttribute("Id")]
            public string Chave
            {
                get { return _chave; }
                set { _chave = value.Replace("NFe", ""); }
            }

            [XmlElement("ide")]
            public Identificacao Identificacao { get; set; }

            [XmlElement("emit")]
            public Emitente Emitente { get; set; }

            [XmlElement("dest")]
            public Destinatario Destinatario { get; set; }

            [XmlElement("total")]
            public Total Total { get; set; }

            [XmlElement("det")]
            public List<Detalhe> Detalhe { get; set; }
        }
    }
}
