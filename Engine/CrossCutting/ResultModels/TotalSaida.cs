using CrossCutting.ResultModels.Commom;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CrossCutting.ResultModels
{
    /// <summary>
    /// Este registro deve ser informado para identificar a totalização das notas fiscais de saídas emitidas em operações internas de venda a consumidor final
    /// declaradas no registro 1210, deduzidas das devoluções ocorridas no próprio mês da venda, do produto identificado no registro 1000.
    /// </summary>
    public partial class TotalSaida : TotalBase
    {
        /// <summary>
        /// Texto fixo contendo 1200
        /// </summary>
        [MaxLength(4)]
        [MinLength(4)]
        [DefaultValue("1200")]
        public string REG { get; set; }

        /// <summary>
        /// Valor de confronto do ICMS das entradas 
        /// Obtido pela multiplicação do campo F02[QTD_TOT_SAÍDA] pelo campo D06[VL_UNIT_MED_ICMS_SUPORT_ENTR] do registro 1100
        /// </summary>
        [Required]
        [RegularExpression(@"^(0|-?\d{0,9}(\.\d{0,2})?)$")]
        public double VL_CONFRONTO_ICMS_ENTRADA { get; set; }

        /// <summary>
        /// Resultado do valor a recuperar ou a ressarcir 
        /// Obtido pela diferença positiva do valor do campo F04[VL_CONFRONTO_ICMS_ENTRADA] pelo campo F03 [VL_TOT_ICMS_EFETIVO]
        /// Observação: Caso o resultado da subtração seja negativo, informar o campo com zero
        /// </summary>
        [Required]
        [RegularExpression(@"^(0|-?\d{0,9}(\.\d{0,2})?)$")]
        public double RESULT_RECUPERAR_RESSARCIR { get; set; }

        /// <summary>
        /// Resultado do valor a complementar
        /// Obtido pela diferença negativa do valor do campo F04[VL_CONFRONTO_ICMS_ENTRADA] pelo campo F03[VL_TOT_ICMS_EFETIVO]
        /// Observação: Caso o resultado da subtração seja positiva, informar o campo com zero
        /// </summary>
        [Required]
        [RegularExpression(@"^(0|-?\d{0,9}(\.\d{0,2})?)$")]
        public double RESULT_COMPLEMENTAR { get; set; }

        /// <summary>
        /// Apuração do ICMS ST a complementar
        /// Obtido a partir da equação:
        /// RESULT_COMPLEMENTAR * ((ALIQ_ICMS_ITEM – ALIQ_FECOP) / ALIQ_ICMS_ITEM)
        /// </summary>
        [Required]
        [RegularExpression(@"^(0|-?\d{0,9}(\.\d{0,2})?)$")]
        public double APUR_ICMSST_COMPLEMENTAR { get; set; }

        /// <summary>
        /// Apuração do FECOP a ressarcir
        /// Obtido a partir da equação: 
        /// RESULT_RECUPERAR_RESSARCIR * (ALIQ_FECOP) / ALIQ_ICMS_ITEM)
        /// </summary>
        [Required]
        [RegularExpression(@"^(0|-?\d{0,9}(\.\d{0,2})?)$")]
        public double APUR_FECOP_RESSARCIR { get; set; }

        /// <summary>
        /// Apuração do FECOP a complementar
        /// Obtido a partir da equação:
        /// RESULT_COMPLEMENTAR * (ALIQ_FECOP) / ALIQ_ICMS_ITEM)
        /// </summary>
        [Required]
        [RegularExpression(@"^(0|-?\d{0,9}(\.\d{0,2})?)$")]
        public double APUR_FECOP_COMPLEMENTAR { get; set; }
    }
}
