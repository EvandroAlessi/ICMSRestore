﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

namespace Dominio
{
    public partial class ItemFiltrado
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public int ProcessoID { get; set; }

        [Required]
        public int ItemID { get; set; }

        public string cProd { get; set; }

        [Required]
        public double vProd { get; set; }

        public string xProd { get; set; }

        [Required]
        public int NCM { get; set; }

        [Required]
        public int CFOP { get; set; }

        public string uCom { get; set; }

        public double? qCom { get; set; }

        public double? vUnCom { get; set; }

        public int? orig { get; set; }

        public int? CST { get; set; }

        public double? vBC { get; set; }

        public double? pICMS { get; set; }

        public double? vICMS { get; set; }

        public int cNF { get; set; }

        public int? CSOSN { get; set; }

        public bool Entrada { get; set; }

        public int nNF { get; set; }

        [Required]
        public DateTime dhEmi { get; set; }

        public DateTime? dhSaiEnt { get; set; }



        public string CNPJ { get; set; }
        public string UF { get; set; }
        public string CNPJ_DEST { get; set; }
        public string UF_DEST { get; set; }
        public string IE { get; set; }
        public string xNome { get; set; }
        public string Chave { get; set; }


        public int nItem { get; set; }



        //MAPEAR
        public string cEAN { get; set; }
    }
}
