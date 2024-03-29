﻿using System.Xml.Serialization;

namespace CrossCutting.SerializationModels
{
    public class Destinatario
    {
        public string CNPJ { get; set; }

        public string CPF { get; set; }

        public string xNome { get; set; }

        [XmlElement("enderDest")]
        public Endereco Endereco { get; set; }

        public string email { get; set; }
    }
}
