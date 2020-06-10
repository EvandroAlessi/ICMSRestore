using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace Dominio
{
    public partial class Item
    {
        [Key]
        public int ID { get; set; }

        public int nItem { get; set; }

        [Required]
        public int NFeID { get; set; }

        #region Produto

        [Required]
        public string cProd { get; set; }
        public string cEAN { get; set; }

        [Required]
        public string xProd { get; set; }

        [Required]
        public int NCM { get; set; }

        [Required]
        public int CFOP { get; set; }
        public string uCom { get; set; }
        public double? qCom { get; set; }
        public double? vUnCom { get; set; }

        #endregion

        #region Imposto

        #region ICMS -> ICMS00

        public int? orig { get; set; }

        [Required]
        public int? CST { get; set; }
        public int? modBC { get; set; }
        public double? vBC { get; set; }
        public double? pICMS { get; set; }
        public double? vICMS { get; set; }

        #endregion

        #region IPI -> IPINT

        public int? cEnq { get; set; }

        public string CST_IPI { get; set; }

        #endregion

        #region PIS -> PISAliq

        public string CST_PIS { get; set; }
        public double? vBC_PIS { get; set; }
        public double? pPIS { get; set; }
        public double? vPIS { get; set; }

        #endregion

        #region COFINS -> COFINSAliq

        public string CST_COFINS { get; set; }
        public double? vBC_COFINS { get; set; }
        public double? pCOFINS { get; set; }
        public double? vCOFINS { get; set; }

        #endregion 

        #endregion
    }
}
