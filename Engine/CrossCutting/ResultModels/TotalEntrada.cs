using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CrossCutting.ResultModels
{
    /// <summary>
    /// Este registro deve ser informado para identificar a totalização das notas fiscais de entrada declaradas no registro 1110, deduzidas das devoluções
    /// ocorridas no próprio mês da aquisição, do produto identificado no registro 1000
    /// </summary>
    public partial class TotalEntrada
    {
        /// <summary>
        /// Texto fixo contendo 1100
        /// </summary>
        [MaxLength(4)]
        [MinLength(4)]
        [DefaultValue("1100")]
        public string REG { get; set; }

        /// <summary>
        /// Quantidade total do item adquirido no período.
        /// Somatória dos campos NFeEntrada.QTD_ENTRADA, menos a somatória dos campos NFeEntradaDevol.QTD_DEVOLVIDA.
        /// Equação: QTD_TOT_ENTRADA = ∑[NFeEntrada.QTD_ENTRADA] - ∑[NFeEntradaDevol.QTD_DEVOLVIDA]
        /// </summary>
        [Required]
        [RegularExpression(@"^(0|-?\d{0,9}(\.\d{0,3})?)$")]
        public double QTD_TOT_ENTRADA { get; set; }

        /// <summary>
        /// Menor valor unitário do item adquirido no período.
        /// Deverá identificar o menor valor de aquisição dentre os produtos declarados no campo NFeEntrada.VL_UNIT_ITEM.
        /// </summary>
        [Required]
        [RegularExpression(@"^(0|-?\d{0,9}(\.\d{0,2})?)$")]
        public double MENOR_VL_UNIT_ITEM { get; set; }

        /// <summary>
        /// Valor unitário médio da base de cálculo do ICMS ST.
        /// Resultado da somatória dos campos NFeEntrada.VL_BC_ICMS_ST,
        /// menos a somatória dos campos NFeEntradaDevol.VL_BC_ICMS_ST,
        /// dividido pelo campo TotalEntrada.QTD_TOT_ENTRADA
        /// </summary>
        [Required]
        [RegularExpression(@"^(0|-?\d{0,9}(\.\d{0,2})?)$")]
        public double VL_BC_ICMSST_UNIT_MED { get; set; }

        /// <summary>
        /// Valor total do ICMS do item suportado na entrada.
        /// Somatória dos campos NFeEntrada.VL_ICMS_SUPORT_ENTR,
        /// menos a somatória dos campos NFeEntradaDevol.VL_ICMS_SUPORT_ENTR
        /// </summary>
        [Required]
        [RegularExpression(@"^(0|-?\d{0,9}(\.\d{0,2})?)$")]
        public double VL_TOT_ICMS_SUPORT_ENTR { get; set; }

        /// <summary>
        /// Valor unitário médio do ICMS suportado na entrada.
        /// É o resultado da divisão do campo TotalEntrada.VL_TOT_ICMS_SUPORT_ENTR pelo campo TotalEntrada.QTD_TOT_ENTRADA
        /// </summary>
        [Required]
        [RegularExpression(@"^(0|-?\d{0,9}(\.\d{0,2})?)$")]
        public double VL_UNIT_MED_ICMS_SUPORT_ENTR { get; set; }
    }
}
