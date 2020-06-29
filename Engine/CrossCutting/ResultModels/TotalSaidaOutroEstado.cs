using CrossCutting.ResultModels.Commom;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CrossCutting.ResultModels
{
    /// <summary>
    /// Este registro deve ser informado para identificar a totalização das notas fiscais de saída emitidas para outros estados, declaradas no registro 1310 do
    /// produto identificado no registro 1000.
    /// </summary>
    public partial class TotalSaidaOutroEstado : TotalBase
    {
        /// <summary>
        /// Texto fixo contendo 1300
        /// </summary>
        [MaxLength(4)]
        [MinLength(4)]
        [DefaultValue("1300")]
        public string REG { get; set; }

        /// <summary>
        /// Valor de confronto do ICMS das entradas
        /// É o resultado da multiplicação do campo H02[QTD_TOT_SAÍDA] 
        /// pelo campo D06[VL_UNIT_MED_ICMS_SUPORT_ENTR] do registro 1100
        /// </summary>
        [Required]
        [RegularExpression(@"^(0|-?\d{0,9}(\.\d{0,2})?)$")]
        public double VL_CONFRONTO_ICMS_ENTRADA { get; set; }

        /// <summary>
        /// Resultado do valor a recuperar ou a ressarcir
        /// Obtido pela diferença positiva do valor do campo H04[VL_CONFRONTO_ICMS_ENTRADA] 
        /// pelo campo H03[VL_TOT_ICMS_EFETIVO]
        /// </summary>
        [Required]
        [RegularExpression(@"^(0|-?\d{0,9}(\.\d{0,2})?)$")]
        public double RESULT_RECUPERAR_RESSARCIR { get; set; }

        /// <summary>
        /// Apuração do FECOP a ressarcir
        /// É o resultado da multiplicação dos campos D04[VL_BC_ICMSST_UNIT_MED], B11[ALIQ_FECOP] e H02[QTD_TOT_SAIDA]
        /// Equação: H07 = (D04* B11) * H02
        /// </summary>
        [Required]
        [RegularExpression(@"^(0|-?\d{0,9}(\.\d{0,2})?)$")]
        public double APUR_FECOP_RESSARCIR { get; set; }
    }
}
