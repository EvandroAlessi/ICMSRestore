using CrossCutting.ResultModels.Commom;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CrossCutting.ResultModels
{
    /// <summary>
    /// Este registro deve conter todas as notas fiscais de entrada da mercadoria declaradas no registro 1000, no período de referência. Se a quantidade
    /// declarada no mês de referência for insuficiente para acobertar o total das saídas declaradas nos registros 1200, 1300, 1400 e 1500, o contribuinte deverá
    /// retroagir aos meses anteriores até obter a quantidade suficiente para acobertar a quantidade das saídas da mesma mercadoria.
    /// </summary>
    public partial class NFeEntrada : InfoBase
    {
        /// <summary>
        /// Texto fixo contendo 1110
        /// </summary>
        [MaxLength(4)]
        [MinLength(4)]
        [DefaultValue("1110")]
        public string REG { get; set; }

        /// <summary>
        /// Quantidade do item adquirido 
        /// Informar a quantidade do item adquirido, 
        /// convertido na mesma unidade de medida declarada no campo B09[UNID_ITEM] do Registro 1000
        /// </summary>
        [Required]
        [RegularExpression(@"^(0|-?\d{0,9}(\.\d{0,3})?)$")]
        public double QTD_ENTRADA { get; set; }

        /// <summary>
        /// Valor unitário do item
        /// Informar o valor unitário líquido de aquisição do item
        /// convertido na mesma unidade de medida declarada no campo B09[UNID_ITEM] do Registro 1000
        /// </summary>
        [Required]
        [RegularExpression(@"^(0|-?\d{0,9}(\.\d{0,2})?)$")]
        public double VL_UNIT_ITEM { get; set; }

        /// <summary>
        /// Base de cálculo do ICMS ST
        /// Informar o valor da base de cálculo utilizada para o cálculo do ICMS ST
        /// </summary>
        [Required]
        [RegularExpression(@"^(0|-?\d{0,9}(\.\d{0,2})?)$")]
        public double VL_BC_ICMS_ST { get; set; }

        /// <summary>
        /// Valor do ICMS do item suportado na entrada
        /// Corresponde ao valor total do imposto suportado pelo
        /// contribuinte substituído, abrangendo o imposto incidente
        /// na operação própria do substituto e o retido por ST, incluída a parcela do FECOP, se houver.
        /// </summary>
        [Required]
        [RegularExpression(@"^(0|-?\d{0,9}(\.\d{0,2})?)$")]
        public double VL_ICMS_SUPORT_ENTR { get; set; }
    }
}
