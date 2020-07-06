using System.ComponentModel.DataAnnotations;

namespace CrossCutting.ResultModels.Commom
{
    public partial class TotalBase
    {
        /// <summary>
        /// Quantidade total de saídas destinadas a contribuintes do Simples Nacional
        /// Somatória dos campos M13 [QTD_SAÍDA], menos a somatória dos campos M13d[QTD_DEVOLVIDA], 
        /// dos produtos comercializados nas operações de saídas para contribuinte do simples nacional
        /// </summary>
        [Required]
        [RegularExpression(@"^(0|-?\d{0,9}(\.\d{0,2})?)$")]
        public double QTD_TOT_SAIDA { get; set; }

        /// <summary>
        /// Valor total do ICMS efetivo nas saídas para consumidor final
        /// Somatória dos campos G15 [VL_ICMS_EFETIVO]
        /// menos a somatória dos campos G15d[VL_ICMS_EFETIVO]
        /// </summary>
        [Required]
        [RegularExpression(@"^(0|-?\d{0,9}(\.\d{0,2})?)$")]
        public double VL_TOT_ICMS_EFETIVO { get; set; }

        /// <summary>
        /// Apuração do ICMS ST a recuperar ou a ressarcir
        /// Valor obtido pela multiplicação do campo L02 [TOT_QTD_SAÍDA] pelo campo L03[VL_ICMSST_UNIT_ENTR]
        /// </summary>
        [Required]
        [RegularExpression(@"^(0|-?\d{0,9}(\.\d{0,2})?)$")]
        public double APUR_ICMSST_RECUPERAR_RESSARCIR { get; set; }
    }
}
