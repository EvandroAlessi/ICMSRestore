using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Dominio
{
    public partial class NFe
    {
        #region Identificacao

        public int cUF { get; set; }
        public int cNF { get; set; }
        public string natOp { get; set; }
        public int indPag { get; set; }
        public string mod { get; set; }
        public int serie { get; set; }
        public string nNF { get; set; }
        public DateTime dhEmi { get; set; }
        public DateTime dhSaiEnt { get; set; }

        #endregion

        #region Emitente

        public string CNPJ { get; set; }

        public string xNome { get; set; }

        public string xFant { get; set; }

        #region Endereco

        public string xLgr { get; set; }
        public string nro { get; set; }
        public string xBairro { get; set; }
        public string cMun { get; set; }
        public string xMun { get; set; }
        public string UF { get; set; }
        public string CEP { get; set; }
        public int cPais { get; set; }
        public string xPais { get; set; }

        #endregion

        public string IE { get; set; }

        public string IEST { get; set; }

        public int CRT { get; set; }

        #endregion

        #region Destinatario

        public string CNPJ_DEST { get; set; }

        public string CPF_DEST { get; set; }

        public string xNome_DEST { get; set; }

        #region Endereco

        public string xLgr_DEST { get; set; }
        public string nro_DEST { get; set; }
        public string xBairro_DEST { get; set; }
        public string cMun_DEST { get; set; }
        public string xMun_DEST { get; set; }
        public string UF_DEST { get; set; }
        public string CEP_DEST { get; set; }
        public int cPais_DEST { get; set; }
        public string xPais_DEST { get; set; }

        #endregion

        public string email_DEST { get; set; }

        #endregion

        public int ProcessoID { get; set; }

        [NotMapped]
        [JsonIgnore]
        public List<Item> Itens { get; set; }
    }
}
